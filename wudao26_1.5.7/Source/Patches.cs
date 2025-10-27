using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ai;
using ai.behaviours;
using HarmonyLib;
using NeoModLoader.api.attributes;
using NeoModLoader.General;
using strings;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CustomModT001;

internal static class Patches
{
    // 添加概率常量
    private const float PROBABILITY_HIGH_TALENT = 0.7f;
    private const float PROBABILITY_UNIQUE = 0.0001f;
    private const float PROBABILITY_EXCELLENT = 0.0011f;
    private const float PROBABILITY_HIGH = 0.0511f;
    private const float PROBABILITY_DEFECTIVE = 0.1011f; // 添加新资质"残缺"，概率5%
    private const float PROBABILITY_LOW = 0.3011f; // 粗劣概率降低至20%
    
    internal static bool window_favorites_global_mode;
    private static int  window_favorites_global_object_count = 10;
    [HarmonyPrefix, HarmonyPatch(typeof(MapBox), nameof(MapBox.checkObjectsToDestroy))]
    private static void MapBox_checkObjectsToDestroy_prefix(MapBox __instance)
    {
        foreach (var unit in __instance.units.getSimpleList())
        {
            if (unit.isAlive() && unit.hasTrait(Traits.war_ashes.id))
            {
                int x = unit.current_tile.x;
                int y = unit.current_tile.y;
                int new_dead_count = __instance.units._to_destroy_objects.Count(a =>
                    Mathf.Abs(a.current_tile.x - x) < 7 && Mathf.Abs(a.current_tile.y - y) < 7);

                unit.IncWarAshesCount(new_dead_count);
            }
        }
    }
    private static bool window_creature_info_initialized;
    [HarmonyPostfix, HarmonyPatch(typeof(BabyHelper), nameof(BabyHelper.canMakeBabies))]
    private static void BabyHelper_canMakeBabies_postfix(ref bool __result, Actor pActor)
    {
        __result &= !pActor.hasTrait(Traits.summoned.id);
    }
    [HarmonyPostfix]
    [HarmonyPatch(typeof(City), nameof(City.addCapturePoints), [typeof(Kingdom), typeof(int)])]
    private static void City_addCapturePoints_postfix(City __instance)
    {
        if (!ModConfigHelper.WarRuleCitySurrender)
        {
            __instance._capturing_units.Clear();
        }
    }
    
    [HarmonyTranspiler, HarmonyPatch(typeof(BaseSimObject), nameof(BaseSimObject.canAttackTarget))]
    private static IEnumerable<CodeInstruction> BaseSimObject_canAttackTarget(IEnumerable<CodeInstruction> codes, ILGenerator generator)
    {
        var list = codes.ToList();

        var start_idx = 0;
        do
        {
            var world_law_idx =
                list.FindIndex(start_idx, x =>
                    x.opcode == OpCodes.Ldfld &&
                    (x.operand as FieldInfo)?.Name == nameof(WorldLawLibrary.world_law_angry_civilians));
            if (world_law_idx < 0)
            {
                break;
            }

            var jump_target = list[world_law_idx + 2];

            var jump_target_code = list.Find(x => x.labels.Contains((Label)jump_target.operand));

            var new_label = generator.DefineLabel();
            jump_target_code.labels.Add(new_label);
            list.InsertRange(world_law_idx + 3, new List<CodeInstruction>()
            {
                new(OpCodes.Call,
                    AccessTools.Method(typeof(ModConfigHelper), nameof(ModConfigHelper.AllowWarRuleCitySurrender))),
                new(jump_target.opcode, new_label)
            });
            start_idx = world_law_idx + 1;
        } while (true);
        return list;
    }
    
    // 检查并设置血脉始祖称号
    [HarmonyPostfix]
    [HarmonyPatch(typeof(Actor), nameof(Actor.updateStats))]
    private static void CheckBloodAncestor(Actor __instance)
    {
        if (__instance == null)
            return;
        
        // 条件1: 血脉天赋为0
        int extraTalent = 0;
        __instance.data.get("extra_talent_inheritance", out extraTalent, 0);
        if (extraTalent != 0)
            return;
        
        // 条件2: 武道等级达到11级（至臻境界）
        if (__instance.GetCultisysLevel() >= 11)
        {
            __instance.data.set("is_blood_ancestor", true);
        }
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(BabyMaker), nameof(BabyMaker.makeBaby))]
    private static void BabyMaker_makeBaby_postfix(Actor __result, Actor pParent1, Actor pParent2)
    {
        // 添加null检查
        if (__result == null)
            return;
            
        // 添加父母同时为null的检查
        if (pParent1 == null && pParent2 == null)
            return;
            
        // 缓存父母的等级，减少重复计算
        int parent1Level = pParent1?.GetCultisysLevel() ?? 0;
        int parent2Level = pParent2?.GetCultisysLevel() ?? 0;
            
        // 处理support_future特质遗传
        if (pParent1?.hasTrait(Traits.support_future) ?? false)
        {
            __result.addTrait(Traits.all_enabled_traits.GetRandom());
        }

        if (pParent2?.hasTrait(Traits.support_future) ?? false)
        {
            __result.addTrait(Traits.all_enabled_traits.GetRandom());
        }

        // 实现超凡阶级遗传机制：从父母双方中选取较高的额外天赋值
        int parent1BaseExtra = 0; // 父母1的基础额外天赋值（不包含境界加成）
        int parent2BaseExtra = 0; // 父母2的基础额外天赋值（不包含境界加成）
        int parent1LevelBonus = 0; // 父母1的境界加成
        int parent2LevelBonus = 0; // 父母2的境界加成

        // 获取父母数据
        GetParentData(pParent1, out parent1BaseExtra, out bool isParent1Ancestor, out parent1LevelBonus);
        GetParentData(pParent2, out parent2BaseExtra, out bool isParent2Ancestor, out parent2LevelBonus);

        // 随机选择父母其中一方的基础额外天赋值遗传
        int extraTalent;
        int parent1TotalExtra = parent1BaseExtra; // 先只使用基础天赋值
        int parent2TotalExtra = parent2BaseExtra; // 先只使用基础天赋值
        int higherTalent = Math.Max(parent1TotalExtra, parent2TotalExtra);
        int lowerTalent = Math.Min(parent1TotalExtra, parent2TotalExtra);
        bool inheritedFromParent1 = false; // 标记是否遗传自父方
        bool inheritedFromParent2 = false; // 标记是否遗传自母方

        // 如果父母都是血脉始祖，统一随机选择一方作为来源
        if (isParent1Ancestor && isParent2Ancestor)
        {
            bool chooseParent1 = UnityEngine.Random.value < 0.5f;
            extraTalent = chooseParent1 ? parent1TotalExtra : parent2TotalExtra;
            inheritedFromParent1 = chooseParent1;
            inheritedFromParent2 = !chooseParent1;
        }
        else
        {
            float randomChance = UnityEngine.Random.value;
            float randomChoice = UnityEngine.Random.value;

            // 计算基础概率和天赋差值影响
            float baseProbability = 0.7f; // 基础概率保持60%
            int talentDiff = Math.Abs(parent1TotalExtra - parent2TotalExtra);
            // 每相差5000，降低10%概率，但最低不低于20%
            float probabilityReduction = Math.Min((talentDiff / 5000f) * 0.1f, 0.5f);
            float finalProbability = baseProbability - probabilityReduction;
            
            ChooseInheritanceSource(parent1TotalExtra, parent2TotalExtra, 
                                   randomChance <= finalProbability, 
                                   randomChoice,
                                   out extraTalent, out inheritedFromParent1, out inheritedFromParent2);
        }

        // 第一阶段：先计算遗传后需要减少的数值
        // 当后代单位额外天赋大于0时，额外天赋均下降15%，如果额外天赋数低于2000，则改为下降200
        // 如果父母任意一方有血脉始祖称号，则不会下降天赋
        if (extraTalent > 0 && !(isParent1Ancestor || isParent2Ancestor))
        {
            if (extraTalent < 2000)
            {
                extraTalent = Math.Max(extraTalent - 200, 0); // 确保不低于0
            }
            else if (extraTalent > 20000)
            {
                extraTalent = Math.Max((int)(extraTalent * 0.7f), 0); // 下降30%，确保不低于0
            }
            else
            {
                extraTalent = Math.Max((int)(extraTalent * 0.85f), 0); // 下降15%，确保不低于0
            }
        }

        // 第二阶段：再加上根据父母境界所增加的数值
        // 根据遗传来源添加对应的境界加成
        if (inheritedFromParent1)
        {
            extraTalent += parent1LevelBonus;
        }
        else if (inheritedFromParent2)
        {
            extraTalent += parent2LevelBonus;
        }

        // 修改血脉资质判定逻辑
        string bloodlineQuality = "平庸"; // 默认为平庸
        
        if (extraTalent > 0)
        {
            float rand = UnityEngine.Random.value;
            if (rand < PROBABILITY_UNIQUE)
            {
                extraTalent = (int)(extraTalent * 10.0f);
                bloodlineQuality = "无一";
            }
            else if (rand < PROBABILITY_EXCELLENT)
            {
                extraTalent = (int)(extraTalent * 5.0f);
                bloodlineQuality = "极品";
            }
            else if (rand < PROBABILITY_HIGH)
            {
                extraTalent = (int)(extraTalent * 2.0f);
                bloodlineQuality = "优质";
            }
            else if (rand < PROBABILITY_DEFECTIVE) // 添加"残缺"资质判定
            {
                extraTalent = Math.Max((int)(extraTalent * 0.25f), 1); // 降低血脉天赋75%
                bloodlineQuality = "残缺";
            }
            else if (rand < PROBABILITY_LOW)
            {
                extraTalent = Math.Max((int)(extraTalent * 0.5f), 1);
                bloodlineQuality = "粗劣";
            }
            // 剩余70%概率：平庸，数值不变
        }

        // 存储额外天赋值
        if (extraTalent > 0)
        {
            __result.data.set("extra_talent_inheritance", extraTalent);
            // 存储资质类型
            __result.data.set("bloodline_quality", bloodlineQuality);
            
            // 当资质为"无一"或"极品"时自动收藏并修改名称
            if (bloodlineQuality == "无一" || bloodlineQuality == "极品")
            {
                // 自动收藏
                if (!__result.data.favorite)
                {
                    __result.data.favorite = true;
                }
                
                // 修改单位名称，添加后缀（不移除现有后缀）
                string currentName = __result.getName();
                if (!currentName.Contains($"-{bloodlineQuality}"))
                {
                    // 使用name属性设置名称
                    __result.data.name = currentName + $"-{bloodlineQuality}";
                }
            }
        }
        
        // 设置血脉称号：当单位获得"血脉始祖"时创建血脉
        string bloodlineName = "";
        
        // 根据天赋数值来源选择对应的血脉名称
        if (inheritedFromParent1 && pParent1 != null)
        {
            pParent1.data.get("bloodline_name", out bloodlineName, "");
        }
        else if (inheritedFromParent2 && pParent2 != null)
        {
            pParent2.data.get("bloodline_name", out bloodlineName, "");
        }
        
        // 如果父母都没有血脉名称，但有血脉始祖，创建血脉
        // 移除DEBUG条件编译指令，使代码在所有模式下都能执行
        if (string.IsNullOrEmpty(bloodlineName) && (isParent1Ancestor || isParent2Ancestor))
        {
            if (inheritedFromParent1 && pParent1 != null)
            {
                bloodlineName = pParent1.name.Contains('-') ? pParent1.name.Substring(pParent1.name.IndexOf('-') + 1) : pParent1.name;
            }
            else if (inheritedFromParent2 && pParent2 != null)
            {
                bloodlineName = pParent2.name.Contains('-') ? pParent2.name.Substring(pParent2.name.IndexOf('-') + 1) : pParent2.name;
            }
        }
        
        // 只有当子单位有血脉天赋或满足血脉始祖条件时，才设置血脉名称
        if (!string.IsNullOrEmpty(bloodlineName) && (extraTalent > 0 || __result.GetCultisysLevel() >= 11))
        {
            __result.data.set("bloodline_name", bloodlineName);
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(StatusLibrary), nameof(StatusLibrary.burningEffect))]
    private static bool StatusLibrary_burningEffect_prefix(BaseSimObject pTarget)
    {
        if (pTarget.isActor() && pTarget.a.GetCultisysLevel() >= 5) return false;

        return true;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Actor), nameof(Actor.addTrait), new Type[] { typeof(ActorTrait) , typeof(bool)})]
    private static bool ActorBase_addTrait_prefix(Actor __instance, ActorTrait pTrait)
    {
        if (__instance.GetCultisysLevel() < 5) return true;
        return !Cultisys.TraitsBlacklist.Contains(pTrait.id);
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Actor), nameof(Actor.addStatusEffect))]
    private static bool Actor_addStatusEffect_prefix(Actor __instance, StatusAsset pStatusAsset)
    {
        if (__instance.a.GetCultisysLevel() < 5) return true;
        return !Cultisys.StatusesBlacklist.Contains(pStatusAsset.id);
    }
    [HarmonyPostfix, HarmonyPatch(typeof(BaseSimObject), nameof(BaseSimObject.removeFinishedStatusEffect))]
    private static void BaseSimObject_cleanupStatusEffects_postfix(BaseSimObject __instance,
        Status pStatusData)
    {
        pStatusData.asset.GetExtend().action_finished?.Invoke(__instance, pStatusData.asset);
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(BaseSimObject), nameof(BaseSimObject.addStatusEffect), [typeof(StatusAsset), typeof(float), typeof(bool)])]
    private static bool BaseSimObject_addStatusEffect_prefix(BaseSimObject __instance, StatusAsset pStatusAsset,
                                                             ref float     pOverrideTimer)
    {
        if (pOverrideTimer < 0)
        {
            pOverrideTimer = pStatusAsset.duration;
        }

        if (pStatusAsset.base_stats.hasTag(S_Tag.immovable))
        {
            pOverrideTimer *= 1 + __instance.stats[Stats.control_get.id];
            if (pOverrideTimer < 0) return false;
        }

        pOverrideTimer *= 1 + __instance.stats[Stats.status_time_get.id];
        return pOverrideTimer > 0;
    }
/*
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(WindowFavorites), nameof(WindowFavorites.create))]
    private static IEnumerable<CodeInstruction> WindowFavorites_create_transpiler(IEnumerable<CodeInstruction> codes)
    {
        var list = codes.ToList();

        var idx = list.FindIndex(x =>
            x.opcode == OpCodes.Callvirt && (x.operand as MemberInfo)?.Name == nameof(SortButton.click));
        list.InsertRange(idx, new List<CodeInstruction>
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Call, AccessTools.Method(typeof(Patches), nameof(WindowFavorites_create_postfix)))
        });


        return list;
    }
    [HarmonyPostfix, HarmonyPatch(typeof(WindowListBase<PrefabUnitElement, Actor>), nameof(WindowListBase<PrefabUnitElement, Actor>.OnDisable))]
    private static void WindowListBase_OnDisable_postfix()
    {
        window_favorites_global_mode = false;
    }

    private static void WindowFavorites_create_postfix(WindowFavorites __instance)
    {
        __instance.addSortButton("inmny/custommodt001/talent", $"{ModClass.asset_id_prefix}.ui.sort_by_talent",
            () => { __instance.current_sort = (a1, a2) => a2.GetTalent().CompareTo(a1.GetTalent()); });
        __instance.addSortButton("inmny/custommodt001/cultilevel", $"{ModClass.asset_id_prefix}.ui.sort_by_cultilevel",
            () =>
            {
                __instance.current_sort = (a1, a2) =>
                {
                    var level_res = a2.GetCultisysLevel().CompareTo(a1.GetCultisysLevel());
                    if (level_res != 0) return level_res;
                    return a2.GetExp().CompareTo(a1.GetExp());
                };
            });

        var mode_switch = new GameObject("ModeSwitch", typeof(Image), typeof(Button));
        mode_switch.transform.SetParent(__instance.transform);
        mode_switch.transform.localScale = Vector3.one;
        mode_switch.transform.localPosition = new Vector3(-122, 85);
        mode_switch.GetComponent<RectTransform>().sizeDelta = new Vector2(28, 28);
        var image = mode_switch.GetComponent<Image>();
        image.sprite = SpriteTextureLoader.getSprite("ui/icons/iconAge");
        var button = mode_switch.GetComponent<Button>();
        button.OnHover(show_tip);

        void show_tip()
        {
            Tooltip.show(mode_switch, Tooltips.common.id, new TooltipData
            {
                tip_name = $"{ModClass.asset_id_prefix}.ui.mode_switch",
                tip_description = $"显示前{window_favorites_global_object_count}",
                tip_description_2 = "滚轮调节数量"
            });
        }

        button.onClick.AddListener(() =>
        {
            window_favorites_global_mode = !window_favorites_global_mode;
            __instance.show();
        });
        var event_trigger = mode_switch.AddComponent<EventTrigger>();
        var scroll_callback = new EventTrigger.TriggerEvent();
        scroll_callback.AddListener(data =>
        {
            if (data is not PointerEventData pointer_event_data) return;
            if (pointer_event_data.scrollDelta.y > 0)
                window_favorites_global_object_count++;
            else if (pointer_event_data.scrollDelta.y < 0) window_favorites_global_object_count--;

            window_favorites_global_object_count = Math.Max(1, window_favorites_global_object_count);
            __instance.show();
            show_tip();
        });
        event_trigger.triggers.Add(new EventTrigger.Entry
        {
            eventID = EventTriggerType.Scroll,
            callback = scroll_callback
        });
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(WindowFavorites), nameof(WindowFavorites.getObjects))]
    private static void WindowFavorites_getObjects_postfix(WindowFavorites __instance, List<Actor> __result)
    {
        if (!window_favorites_global_mode) return;
        foreach (Actor actor in World.world.units)
            if (actor.isAlive() && !actor.data.favorite)
                __result.Add(actor);

        __result.Sort(__instance.current_sort);
        if (__instance._currentSortButton != null && __instance._currentSortButton.getState() == SortButtonState.Down)
            __result.Reverse();

        if (__result.Count > window_favorites_global_object_count)
            __result.RemoveRange(window_favorites_global_object_count,
                __result.Count - window_favorites_global_object_count);
    }
*/
    [HarmonyPostfix]
    [HarmonyPatch(typeof(BaseStats), nameof(BaseStats.mergeStats))]
    private static void BaseStats_mergeStats_postfix(BaseStats __instance, BaseStats pStats)
    {
        var id = Stats.addition_armor.id;
        var add = pStats[id];
        if (add == 0) return;
        var curr = __instance[id];
        var origin = curr - add;
        __instance[id] = origin + (100 - origin) * add / 100f;
    }
    [HarmonyPrefix,HarmonyPatch(typeof(ActorTool), nameof(ActorTool.applyForceToUnit))]
    private static bool ActorBase_addForce_prefix(AttackData pData, BaseSimObject pTargetToCheck, ref float pMod)
    {
        pMod = 1 - pTargetToCheck.stats[S.knockback_reduction];
        if (pTargetToCheck.a == null) return true;
        if (pTargetToCheck.a.GetCultisysLevel() >= 5 || pTargetToCheck.a.hasTrait(Traits.final_resilience.id))
        {
            pMod = 0;
        }

        return true;
    }
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(Actor), nameof(Actor.updateStats))]
    private static IEnumerable<CodeInstruction> Actor_updateStats_transpiler(IEnumerable<CodeInstruction> codes)
    {
        var list = codes.ToList();

        var addition_stats_idx = list.FindIndex(x =>
            x.opcode == OpCodes.Callvirt && (x.operand as MemberInfo)?.Name == nameof(BaseStats.normalize)) - 2;
        var add_codes = new List<CodeInstruction>
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Call, AccessTools.Method(typeof(Patches), nameof(addition_stats)))
        };
        list[addition_stats_idx].MoveLabelsTo(add_codes[0]);
        list.InsertRange(addition_stats_idx, add_codes);


        var post_stats_idx = list.FindIndex(x =>
            x.opcode == OpCodes.Callvirt && (x.operand as MemberInfo)?.Name == nameof(BaseStats.checkMultipliers)) + 1;
        list.InsertRange(post_stats_idx, new List<CodeInstruction>
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Call, AccessTools.Method(typeof(Patches), nameof(post_stats)))
        });
/*
        for (var i = 1; i < list.Count; i++)
        {
            CodeInstruction ldstr = list[i - 1];
            CodeInstruction call = list[i];
            if (ldstr.opcode == OpCodes.Ldstr && (string)ldstr.operand == "madness" && call.opcode == OpCodes.Call &&
                (call.operand as MemberInfo)?.Name == nameof(Actor.hasTrait))
                call.operand = AccessTools.Method(typeof(Patches), nameof(HasMadness));
        }*/
        var clear_trait_timers_idx = list.FindIndex(x =>
            x.opcode == OpCodes.Callvirt && (x.operand as MethodInfo)?.Name == "Clear" && (x.operand as MethodInfo)?.DeclaringType == typeof(Dictionary<BaseAugmentationAsset, double>));
        list[clear_trait_timers_idx].operand = AccessTools.Method(typeof(Patches), nameof(empty_Dictionary_action));

        return list;
    }

    private static void empty_Dictionary_action(Dictionary<ActorTrait, double> _)
    {
        
    }
    private static bool HasMadness(Actor actor, string _)
    {
        return actor.hasTrait("madness") || actor.hasStatus(Statuses.limited_madness.id);
    }

    private static void addition_stats(Actor actor_base)
    {
        if (actor_base.a == null || actor_base.stats == null) return;
        bool stoic = actor_base.a.hasTrait(Traits.final_resilience.id);
        bool has_overwrite_stats = actor_base.a.HasOverwriteStats();
        if (!has_overwrite_stats)
        {
            var level = actor_base.a.GetCultisysLevel();
            if (level >= 0) actor_base.stats?.mergeStats(Cultisys.LevelStats[level]);

            foreach (var trait in actor_base.traits)
            {
                TraitExtend trait_extend = trait.GetExtend();
                BaseStats conditional_stats = trait_extend.conditional_basestats?.Invoke(actor_base.a);
                if (conditional_stats != null)
                    actor_base.stats?.mergeStats(conditional_stats);
            }
        }

        if (stoic && actor_base.stats != null)
        {
            actor_base.stats._tags?.Remove("immovable");
            actor_base.stats._tags?.Remove("frozen_ai");
            actor_base.stats._tags?.Remove("stop_idle_animation");
        }
        if (!actor_base.hasAnyStatusEffect()) return;
        if (actor_base.a == null) return;
        if (actor_base._active_status_dict != null)
            foreach (var status_data in actor_base._active_status_dict.Values)
            {
                if (status_data?.asset == null) continue;
                StatusExtend status_extend = status_data.asset.GetExtend();
                if (status_data.asset.id == Statuses.stoic.id) stoic = true;
                if (!has_overwrite_stats)
                {
                    BaseStats conditional_stats = status_extend?.conditional_basestats?.Invoke(actor_base.a);
                    if (conditional_stats != null)
                        actor_base.stats?.mergeStats(conditional_stats);
                }
            }

        if (stoic && actor_base.stats != null)
        {
            actor_base.stats._tags?.Remove("immovable");
            actor_base.stats._tags?.Remove("frozen_ai");
            actor_base.stats._tags?.Remove("stop_idle_animation");
        }
    }

    private static void post_stats(Actor actor_base)
    {
        if (actor_base.a == null) return;
        // 检查stats是否已初始化，不再尝试赋值
        if (actor_base.stats == null) return;
        BaseStats overwrite_stats = actor_base.a.GetOverwriteStats();
        if (overwrite_stats != null)
        {
            actor_base.stats?.mergeStats(overwrite_stats);
            return;
        }
        foreach (var trait in actor_base.traits)
        {
            TraitExtend trait_extend = trait.GetExtend();
            if (trait_extend.final_basestats == null) continue;
            // 2. 添加空传播运算符
            actor_base.stats?.ApplyMods(trait_extend.final_basestats);
        }

        if (actor_base._active_status_dict != null)
            foreach (var status_data in actor_base._active_status_dict.Values)
            {
                StatusExtend status_extend = status_data.asset.GetExtend();
                if (status_extend.final_basestats == null) continue;
                // 2. 添加空传播运算符
                actor_base.stats?.ApplyMods(status_extend.final_basestats);
            }

        foreach (var trait in actor_base.traits)
        {
            TraitExtend trait_extend = trait.GetExtend();
            if (trait_extend.final_basestats != null)
            {
                // 2. 添加空传播运算符
                actor_base.stats?.mergeStats(trait_extend.final_basestats);
            }

            if (trait_extend.final_conditional_basestats != null)
            {
                var stats = trait_extend.final_conditional_basestats(actor_base.a);
                // 3. 验证条件统计值不为null
                if (stats != null)
                    actor_base.stats?.mergeStats(stats);
            }
        }

        if (actor_base._active_status_dict != null)
            foreach (var status_data in actor_base._active_status_dict.Values)
            {
                StatusExtend status_extend = status_data.asset.GetExtend();
                if (status_extend.final_basestats == null) continue;
                // 2. 添加空传播运算符
                actor_base.stats?.mergeStats(status_extend.final_basestats);
            }
    }
    [HarmonyPostfix, HarmonyPatch(typeof(BatchActors), nameof(BatchActors.u8_checkUpdateTimers))]
    private static void BatchActors_updateTimers_postfix(BatchActors __instance)
    {
        if (!__instance.check(__instance._cur_container))
        {
            return;
        }
        if (World.world.isPaused())
        {
            return;
        }
        var elapsed = __instance._elapsed;
        foreach (var a in __instance._array)
            {
                if (a?.data == null || a.traits == null || !a.isAlive()) continue;
                Actor_u9_checkUpdateTimers_postfix(a, elapsed);
            }
        //Parallel.ForEach(__instance._array, a => Actor_u9_checkUpdateTimers_postfix(a, elapsed));
    }
    //[HarmonyPostfix, Hotfixable]
    //[HarmonyPatch(typeof(Actor), nameof(Actor.updateParallelChecks))]
    private static void Actor_u9_checkUpdateTimers_postfix(Actor __instance, float pElapsed)
    {
        if (Randy.randomChance(0.1f))
        {
            foreach (var trait in __instance.traits)
            {
                if (trait.GetExtend().conditional_basestats != null) __instance.setStatsDirty();

                if (!trait.GetExtend().HasCooldown) continue;
                __instance.DecreaseCooldown(trait.id, pElapsed * 10);
            }
            if (__instance._active_status_dict != null)
            {
                foreach (var status_data in __instance._active_status_dict.Values)
                {
                    StatusExtend status_extend = status_data.asset.GetExtend();
                    if (status_extend.conditional_basestats != null) __instance.setStatsDirty();
                }
            }
            
            var mana_regen = __instance.stats["inmny.custommodt001.mana_regen"] * pElapsed * 10;
            if (mana_regen < 1)
            {
                if (Randy.randomChance(mana_regen))
                {
                    mana_regen = 1;
                }
            }
            __instance.RestoreMana(mana_regen);
        }
        if (!__instance.HasShield()) return;
        // 创建护盾列表的副本以避免并发修改异常
        var shields = __instance.GetShields().ToList();
        if (shields.Count == 0) return;
        
        // 使用临时列表存储有效护盾
        var validShields = new List<ShieldData>();
        foreach (var shield in shields)
        {
            // 创建护盾数据的副本以避免修改共享对象
            var updatedShield = new ShieldData
            {
                left_time = shield.left_time - pElapsed,
                value = shield.value
            };
            
            if (updatedShield.left_time > 0 && updatedShield.value > 0)
            {
                validShields.Add(updatedShield);
            }
        }
        
        __instance.UpdateShields(validShields);
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Actor), nameof(Actor.getHit))]
    private static bool Actor_getHit_prefix(Actor __instance, ref float pDamage, ref AttackType pAttackType,
                                            BaseSimObject pAttacker)
    {
        if (pAttacker != null && pAttacker.isActor())
        {
            var a = pAttacker.a;
            if (a.hasTrait(Traits.war_ashes.id) && a.GetWarAshesCount() > 100) pAttackType = AttackType.None;
            if (a.hasTrait(Traits.steel_heart.id)) a.IncSteelHeartCount();

            if (a.hasTrait(Traits.illuminated_glory.id))
                if (a.GetLastAttackTime() + 10 < World.world.getCurWorldTime())
                    pAttackType = AttackType.None;

            if (a.hasTrait(Traits.final_power.id)) pAttackType = AttackType.None;
            if ((a.hasTrait(Traits.survival_of_civilization.id) ||
                 a.asset.id.StartsWith($"{ModClass.asset_id_prefix}.demon_king")) &&
                __instance.hasTrait(Traits.survival_of_civilization.id))
            {
                pDamage = 0;
            }

            var weapon = a.getWeapon();
            if (weapon is { data: { asset_id: "sword", material: "wood", name: "灵剑" } } &&
                __instance.hasStatus(Statuses.active_spirit_sword.id))
            {
                pDamage = 0;
            }

            // 高境界增伤逻辑 - 仅对敌方有效
            if (a.GetCultisysLevel() >= 14 && pAttackType != AttackType.None &&
                (__instance.kingdom == null || a.kingdom == null || __instance.kingdom.isEnemy(a.kingdom)))
            {
                float armorValue = Cultisys.LevelStats[__instance.GetCultisysLevel()][Stats.addition_armor.id];
                float denominator = Math.Max(100 - armorValue, 1f); // 确保分母至少为1，避免除以0或负数
                pDamage *= 100f / denominator;
            }

        }

        if (pAttacker != null && pAttackType != AttackType.None)
            if (Randy.randomChance(0.01f * __instance.stats[Stats.dodge.id]))
                return false;

        if (pAttacker?.isActor() ?? false) pAttacker.a.SetLastAttackTime((float)World.world.getCurWorldTime());

        // 对于橡皮擦工具等非角色攻击，不应用护甲减免
        if (pAttackType == AttackType.None) return true;
        // 仅对角色发起的攻击应用护甲减免
        if (pAttacker != null && pAttacker.isActor())
            pDamage *= 1 - __instance.stats[Stats.addition_armor.id] / 100;
        return true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(Actor), nameof(Actor.newKillAction))]
    private static void Actor_newKillAction_postfix(Actor __instance, Actor pDeadUnit)
    {
        __instance.IncExp(200+ModConfigHelper.GetKillExpK() * pDeadUnit.GetExp());
        if (__instance.hasTrait(Traits.meat_eater.id))
            __instance.restoreHealth((int)(__instance.stats[S.health] * 0.01f));
        if (__instance.hasTrait(Traits.summoner.id))
        {
            if (__instance.GetSummonCount(Traits.summoner.id) < 100)
            {
                var new_actor =
                    World.world.units.spawnNewUnit(Actors.skeleton_summoned.id, pDeadUnit.current_tile, true, pSpawnHeight:0);
                if (new_actor != null)
                {
                    var stats = new BaseStats();
                    foreach (var s in __instance.stats._stats_list)
                    {
                        stats[s.id] = s.value * 0.1f;
                    }
                    new_actor.OverwriteStats(stats);
                    new_actor.event_full_stats = true;
                    new_actor.SetSummonSource(Traits.summoner.id);
                    new_actor.SetOwner(__instance);
                }
            }
        }
        else if (__instance.hasTrait(Traits.summoned.id))
        {
            var owner = __instance.GetOwner();
            if (owner != null && owner.hasTrait(Traits.summoner.id) && owner.GetSummonCount(Traits.summoner.id) < 100)
            {
                var new_actor =
                    World.world.units.spawnNewUnit(Actors.skeleton_summoned.id, pDeadUnit.current_tile, true, pSpawnHeight:0);
                if (new_actor != null)
                {
                    var stats = new BaseStats();
                    foreach (var s in owner.stats._stats_list)
                    {
                        stats[s.id] = s.value * 0.1f;
                    }
                    new_actor.OverwriteStats(stats);
                    new_actor.event_full_stats = true;
                    new_actor.SetSummonSource(Traits.summoner.id);
                    new_actor.SetOwner(owner);
                }
            }
        }
        if (__instance.hasTrait(Traits.puppetry_master.id) && __instance.GetSummonCount(Traits.puppetry_master.id) < 5)
        {
            Actor new_actor = World.world.units.spawnNewUnit(pDeadUnit.a.asset.id, pDeadUnit.current_tile, true, pSpawnHeight:0);
            if (new_actor != null)
            {
                ActorTool.copyUnitToOtherUnit(pDeadUnit.a, new_actor);
                new_actor.traits.Clear();
                new_actor.addTrait(Traits.summoned);
                new_actor.SetSummonSource(Traits.puppetry_master.id);
                new_actor.SetOwner(__instance);
                var stats = new BaseStats();
                stats.mergeStats(pDeadUnit.stats);
                stats[S.health] *= 0.5f;
                stats[S.damage] *= 0.5f;
                new_actor.OverwriteStats(stats);
            }
        }

        if (__instance.hasTrait(Traits.mysterious_mage.id) && __instance.GetSummonCount(Traits.mysterious_mage.id) < 5)
        {
            Actor new_actor = World.world.units.spawnNewUnit(pDeadUnit.a.asset.id, pDeadUnit.current_tile, true, pSpawnHeight:0);
            if (new_actor != null)
            {
                ActorTool.copyUnitToOtherUnit(pDeadUnit.a, new_actor);
                new_actor.addTrait(Traits.summoned);
                new_actor.SetSummonSource(Traits.mysterious_mage.id);
                new_actor.SetOwner(__instance);
            }
        }

        if (__instance.HasUndeadWarGodTriggerd() && __instance.hasTrait(Traits.loong_heart.id))
        {
            var count = __instance.IncreaseUndeadWarGodCount();
            if (count >= 10)
            {
                __instance.RemoveUndeadWarGodTrigger();
            }
        }
    }
    [HarmonyPrefix]
    [HarmonyPatch(typeof(SelectedUnitTab), nameof(SelectedUnitTab.showStatBars))]
    private static bool SelectedUnitTab_showStatBars_postfix(SelectedUnitTab __instance)
    {
        var unit = SelectedUnit.unit;
        var mask = __instance._bar_health.mask;
        var bar = __instance._bar_health.bar;
        var shield_bar = mask.Find("ShieldBar");
        if (!unit.HasShield())
        {
            if (shield_bar != null) shield_bar.gameObject.SetActive(false);
            return true;
        }
        var shield_val = unit.GetShields().Sum(x => x.value);
        if (shield_val < 1f)
        {
            if (shield_bar != null) shield_bar.gameObject.SetActive(false);
            return true;
        }
        var total = Mathf.Max(unit.getMaxHealth(), shield_val + unit.getHealth());
        __instance._bar_health.setBar(unit.getHealth(), total, $"+{shield_val:N0}/{unit.getMaxHealth()}", false, false, true, 0.25f);

        if (shield_bar == null)
        {
            shield_bar = new GameObject("ShieldBar", typeof(Image)).transform;
            shield_bar.SetParent(mask.transform);
            shield_bar.localScale = Vector3.one;
            shield_bar.GetComponent<RectTransform>().SetPivot(PivotPresets.MiddleLeft);
            shield_bar.GetComponent<Image>().color = Color.gray;
        }

        if (!shield_bar.gameObject.activeSelf)
        {
            shield_bar.gameObject.SetActive(true);
        }

        var rect = shield_bar.GetComponent<RectTransform>();
        rect.localPosition = new(unit.getHealth() / total * mask.rect.width - (__instance._bar_health.GetComponent<RectTransform>().sizeDelta.x + mask.sizeDelta.x) * 0.5f, 0, 0);
        rect.sizeDelta = new(mask.rect.width * shield_val / total, bar.rect.height);
        
        return false;
    }
    [HarmonyPrefix]
    [HarmonyPatch(typeof(UnitHealthBarElement), nameof(UnitHealthBarElement.showContent))]
    private static bool UnitHealthBarElement_showContent_postfix(UnitHealthBarElement __instance)
    {
        var unit = __instance.actor;
        var mask = __instance._health.mask;
        var bar = __instance._health.bar;
        var shield_bar = mask.Find("ShieldBar");
        if (!unit.HasShield())
        {
            if (shield_bar != null) shield_bar.gameObject.SetActive(false);
            return true;
        }
        var shield_val = unit.GetShields().Sum(x => x.value);
        if (shield_val < 1f)
        {
            if (shield_bar != null) shield_bar.gameObject.SetActive(false);
            return true;
        }
        var total = Mathf.Max(unit.getMaxHealth(), shield_val + unit.getHealth());
        __instance._health.setBar(unit.getHealth(), total, $"+{shield_val:N0}/{unit.getMaxHealth()}");

        if (shield_bar == null)
        {
            shield_bar = new GameObject("ShieldBar", typeof(Image)).transform;
            shield_bar.SetParent(mask.transform);
            shield_bar.localScale = Vector3.one;
            shield_bar.GetComponent<RectTransform>().SetPivot(PivotPresets.MiddleLeft);
            shield_bar.GetComponent<Image>().color = Color.gray;
        }

        if (!shield_bar.gameObject.activeSelf)
        {
            shield_bar.gameObject.SetActive(true);
        }

        var rect = shield_bar.GetComponent<RectTransform>();
        rect.localPosition = new(unit.getHealth() / total * mask.rect.width - (__instance._health.GetComponent<RectTransform>().sizeDelta.x + mask.sizeDelta.x) * 0.5f, 0, 0);
        rect.sizeDelta = new(mask.rect.width * shield_val / total, bar.rect.height);
        
        return false;
    }
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Actor), nameof(Actor.checkCallbacksOnDeath))]
    private static void Actor_checkCallbacksOnDeath(Actor __instance)
    {
        Actor owner = __instance.GetOwner();
        if (owner != null)
            if (owner.hasTrait(Traits.unrepentant_refactoring.id))
            {
                var half_edge = 1;
                var x = __instance.current_tile.x;
                var y = __instance.current_tile.y;
                for (var i = -half_edge; i <= half_edge; i++)
                for (var j = -half_edge; j <= half_edge; j++)
                {
                    WorldTile tile = World.world.GetTile(x + i, y + j);
                    if (tile == null) continue;
                    var list = tile._units;
                    for (var k = 0; k < list.Count; k++)
                    {
                        Actor unit = list[k];
                        if (unit == null || !unit.isAlive()) continue;
                        if (__instance.kingdom?.isEnemy(unit.kingdom) ?? true)
                            __instance.ApplyStatusEffectTo(unit, Statuses.dizzy.id, 50, true);
                    }
                }
            }
        
        // 时光之终末
        if (__instance.hasTrait(Traits.end_of_time.id) )
        {
            var chance = 0.8f;
            if (__instance.hasTrait(Traits.undead_war_god.id) || __instance.hasTrait(Traits.self_destruct.id) ||
                __instance.hasTrait(Traits.doppelganger.id))
            {
                chance = 0.9f;
            }
            if (Randy.randomChance(chance))
            {
                __instance.data.health = __instance.getMaxHealth();
                if (__instance.hasTrait(Traits.undead_war_god.id))
                {
                    if (__instance.hasTrait(Traits.loong_heart.id))
                    {
                        __instance.addStatusEffect(Statuses.active_undead_war_god.id, 90);
                    }
                    else
                    {
                        __instance.addStatusEffect(Statuses.active_undead_war_god.id);
                    }
                }

                if (__instance.hasTrait(Traits.self_destruct.id))
                {
                    Traits.self_destruct.action_death(__instance, __instance.current_tile);
                }

                if (__instance.hasTrait(Traits.doppelganger.id))
                {
                    Traits.doppelganger.action_death(__instance, __instance.current_tile);
                }
                __instance.setAlive(true);

                return;
            }
        }

        // 亡灵战神
        if (__instance.hasTrait(Traits.undead_war_god.id) && !__instance.HasUndeadWarGodTriggerd())
        {
            if (__instance.hasTrait(Traits.loong_heart.id))
            {
                __instance.addStatusEffect(Statuses.active_undead_war_god.id, 90);
            }
            else
            {
                __instance.addStatusEffect(Statuses.active_undead_war_god.id);
            }
            
            __instance.event_full_stats = true;
            __instance.setStatsDirty();
            __instance.setAlive(true);
            __instance.SetUndeadWarGodTriggerd();
            __instance.updateStats();
            return;
        }
    }
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Actor), nameof(Actor.die))]
    private static bool Actor_killHimself(Actor __instance)
    {
        if (!__instance.isAlive()) return true;
        __instance.DecOwnerSummonSource();

        // 春秋蝉
        /*
        if (__instance.hasTrait(Traits.age_cicada.id) && !__instance.TraitUnderCooldown(Traits.age_cicada.id))
        {
            __instance.ResetExp();
            __instance.ResetTalent();
            __instance.ResetCultisysLevel();
            __instance.finishAllStatusEffects();
            __instance.AddInvincible(20);
            __instance.StartCooldown(Traits.age_cicada);
            return false;
        }*/
        // 二重身是真正死后触发

        return true;
    }

    private static float calc_shield(Actor actor, BaseSimObject attacker, AttackType type, float damage)
    {
        if (type == AttackType.None) return damage;
        var shields = actor.GetShields();
        for (var i = 0; i < shields.Count; i++)
        {
            ShieldData shield = shields[i];
            if (shield.value > damage)
            {
                shield.value -= damage;
                shields[i] = shield;
                damage = 0;
                break;
            }

            damage -= shield.value;
            shields.RemoveAt(i);
            i--;
        }

        actor.UpdateShields(shields);
        if (actor.hasTrait(Traits.defence_stilling.id) || (attacker != null && attacker.isActor() && attacker.a.hasTrait(Traits.attack_stilling.id)))
        {
            damage = Mathf.Min(damage, actor.stats[S.health] * 0.1f);
        }
        return damage;
    }

    [HarmonyTranspiler]
    [HarmonyPatch(typeof(Actor), nameof(Actor.getHit))]
    private static IEnumerable<CodeInstruction> Actor_getHit_transpiler(IEnumerable<CodeInstruction> codes)
    {
        var list = codes.ToList();
#if CULTIWAY
#else
        for (var i = 1; i < list.Count; i++)
        {
            CodeInstruction ldc = list[i - 1];
            CodeInstruction starg = list[i];
            if (ldc.opcode == OpCodes.Ldc_R4 && starg.opcode == OpCodes.Starg_S)
            {
                ldc.operand = 0.0f;
                break;
            }
        }
#endif
        for (var i = 1; i < list.Count; i++)
        {
            CodeInstruction mul = list[i - 1];
            CodeInstruction starg = list[i];
            if (mul.opcode == OpCodes.Mul && starg.opcode == OpCodes.Starg_S)
            {
                var insert_idx = i + 1;
                CodeInstruction old_code = list[insert_idx];
                list.InsertRange(insert_idx, new[]
                {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldarg_S, 4),
                    new CodeInstruction(OpCodes.Ldarg_3),
                    new CodeInstruction(OpCodes.Ldarg_S, 1),
                    new CodeInstruction(OpCodes.Call,    AccessTools.Method(typeof(Patches), nameof(calc_shield))),
                    new CodeInstruction(OpCodes.Starg_S, 1)
                });
                old_code.MoveLabelsTo(list[insert_idx]);
                break;
            }
        }

        for (var i = 0; i < list.Count - 1; i++)
        {
            CodeInstruction ldc = list[i];
            CodeInstruction stfld = list[i + 1];
            if (ldc.opcode                          == OpCodes.Ldc_R4 && stfld.opcode == OpCodes.Stfld &&
                (stfld.operand as MemberInfo)?.Name == nameof(Actor.timer_action))
            {
                ldc.operand = 0.0f;
                break;
            }
        }

        var life_steal_idx = list.FindIndex(x => (x.operand as MethodInfo)?.Name == nameof(BaseSimObject.changeHealth)) + 1;
        list.InsertRange(life_steal_idx, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_S, 4),
            new CodeInstruction(OpCodes.Ldarg_S, 1),
            new CodeInstruction(OpCodes.Call,    AccessTools.Method(typeof(Patches), nameof(apply_life_steal))),
            new CodeInstruction(OpCodes.Ldarg_0),
            new CodeInstruction(OpCodes.Ldarg_S, 4),
            new CodeInstruction(OpCodes.Ldarg_3),
            new CodeInstruction(OpCodes.Ldarg_S, 1),
            new CodeInstruction(OpCodes.Call,    AccessTools.Method(typeof(Patches), nameof(on_gethit_end))),
        });


        return list;
    }

    private static void on_gethit_end(Actor actor, BaseSimObject attacker, AttackType type, float damage)
    {
        if (attacker == null || !attacker.isAlive()) return;
        if (actor.hasTrait(Traits.thorn_shell.id))
        {
            EventManager.EnqueueDamageEvent(actor, attacker, actor.stats[S.damage] * 0.1f, AttackType.Other);
        }
    }
    private static void apply_life_steal(BaseSimObject attacker, float damage)
    {
        if (attacker == null || !attacker.isAlive()) return;
        var health_steal = damage * attacker.stats[Stats.life_steal.id];
        if (health_steal > 0)
        {
            if (attacker.isActor())
            {
                attacker.a.restoreHealth((int)health_steal);
            }
            else
            {
                attacker.getData().health += (int)health_steal;
            }
        }
        if (attacker.isActor())
        {
            var a = attacker.a;
            var owner = a.GetOwner();
            if (owner != null && owner.hasTrait(Traits.empty_feathered.id))
            {
                owner.restoreHealth((int)(damage * 0.05f));
            }
        }
    }

    [HarmonyPostfix, Hotfixable]
    [HarmonyPatch(typeof(Actor), nameof(Actor.updateAge))]
    private static void Actor_updateAge_postfix(Actor __instance)
    {
        if (__instance.hasTrait(Traits.sign_in.id) && !__instance.TraitUnderCooldown(Traits.sign_in.id))
        {
            __instance.StartCooldown(Traits.sign_in);
            var traits = Traits.all_enabled_traits.Except(__instance.traits.Select(x=>x.id)).ToList();
            if (traits.Count > 0)
            {
                var trait_to_give = traits.GetRandom();
                __instance.addTrait(trait_to_give, true);
            }
        }
        if (__instance.hasTrait(Traits.summoned.id)) return;
        var level = __instance.GetCultisysLevel();
        // 检查是否达到14级(超凡)及以上且未被收藏，如果是则自动收藏
        if (level >= 14 && !__instance.data.favorite)
        {
            __instance.data.favorite = true;
        }

        if (level >= Cultisys.MaxLevel) return;
        var talent = __instance.GetTalent();
        
        // 获取额外天赋值
        __instance.data.get("extra_talent_inheritance", out int extraTalent, 0);
        
        // 计算总经验值增长 (基础天赋 + 额外天赋)
        float totalExpGain = (talent + extraTalent) * ModConfigHelper.GetTalentMultiplier();
        __instance.IncExp(totalExpGain);
        
        if (__instance.GetExp() >= Cultisys.LevelExpRequired[level])
        {
            __instance.LevelUp();
            __instance.ResetExp();
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(ActorTraitButton), nameof(ActorTraitButton.tooltipDataBuilder))]
    private static void ActorTraitButton_tooltipDataBuilder_postfix(ActorTraitButton __instance,
        ref TooltipData __result)
    {
        string appended_text = append_text(__instance);
        if (string.IsNullOrEmpty(appended_text)) return;
        __result.tip_description_2 += '\n' + appended_text;
    }
    [Hotfixable]
    private static string append_text(ActorTraitButton trait_button)
    {
        if (SelectedUnit.unit == null) return null;
        ActorTrait trait = trait_button.augmentation_asset;
        TraitExtend trait_extend = trait.GetExtend();

        string text = null;
        if (trait.id == Traits.final_skill.id)
        {
            if (SelectedUnit.unit.HasFinalSkill())
            {
                var skill = SelectedUnit.unit.GetFinalSkill();
                text += $"\n契合特质: {LM.Get("trait_" + skill)}";
            }
        }
        if (trait.id == Traits.summoned.id)
        {
            var owner_name = SelectedUnit.unit.GetOwner()?.getName() ?? "已死亡";
            text = $"所有者: {owner_name}";
        }

        if (!trait_extend.HasCooldown) return text;
        var cooldown = SelectedUnit.unit.GetCooldown(trait.id);
        if (text == null)
            text = $"冷却: {cooldown:F1}s";
        else
            text += $"\n冷却: {cooldown:F1}s";

        return text;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(TooltipLibrary), nameof(TooltipLibrary.showActor))]
    private static void TooltipLibrary_showActor_postfix(Tooltip pTooltip, TooltipData pData)
    {
        pTooltip.addLineBreak();

        pTooltip.addLineText($"{ModClass.asset_id_prefix}.ui.cultilevel",
            Cultisys.GetName(pData.actor.GetCultisysLevel()));
        pTooltip.addLineText($"{ModClass.asset_id_prefix}.ui.talent", ((int)pData.actor.GetTalent()).ToString());
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(TooltipLibrary), nameof(TooltipLibrary.showTrait))]
    private static void TooltipLibrary_showTrait_transpiler(Tooltip pTooltip, TooltipData pData)
    {
        append_trait_stats(pTooltip, pData.trait, pData);
    }

    private static void append_trait_stats(Tooltip tooltip, ActorTrait trait, TooltipData data)
    {
        if (SelectedUnit.unit == null)
        {
            BaseStatsHelper.showBaseStats(tooltip.stats_description, tooltip.stats_values, trait.base_stats, false);
            return;
        }

        TraitExtend trait_extend = trait.GetExtend();

        BaseStats conditional_stats = trait_extend.conditional_basestats?.Invoke(SelectedUnit.unit);
        if (conditional_stats != null && conditional_stats._stats_list.Count > 0)
        {
            if  (tooltip.stats_description.text.Length > 0)
                BaseStatsHelper.addLineBreak(tooltip.stats_description, tooltip.stats_values);
            BaseStatsHelper.addStatValues(tooltip.stats_description, tooltip.stats_values, "额外加成:", "");
            BaseStatsHelper.showBaseStats(tooltip.stats_description, tooltip.stats_values, conditional_stats);
        }

        var final_stats = trait_extend.final_basestats;
        if (final_stats != null && final_stats._stats_list.Count > 0)
        {
            if  (tooltip.stats_description.text.Length > 0)
                BaseStatsHelper.addLineBreak(tooltip.stats_description, tooltip.stats_values);
            BaseStatsHelper.addStatValues(tooltip.stats_description, tooltip.stats_values, "最终加成:", "");
            BaseStatsHelper.showBaseStats(tooltip.stats_description, tooltip.stats_values, final_stats);
        }

        if (string.IsNullOrEmpty(data.tip_description_2)) return;
        tooltip.addDescription("\n");
        tooltip.addDescription(data.tip_description_2);
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(UnitWindow), nameof(UnitWindow.OnEnable))]
    private static void WindowCreatureInfo_OnEnable_postfix(UnitWindow __instance)
    {
        if (__instance.actor == null || !__instance.actor.isAlive()) return;
        if (!window_creature_info_initialized)
        {
            __instance.StartCoroutine(new WaitUntil(() =>
            {
                if (UnitWindowHelper.Initialize(__instance))
                {
                    window_creature_info_initialized = true;
                }

                return window_creature_info_initialized;
            }));
        }
    }
/*
    [HarmonyPostfix]
    [HarmonyPatch(typeof(TraitsContainer<ActorTrait, ActorTraitButton>), nameof(ActorTraitsContainer.loadActiveTrait))]
    private static void WindowCreatureInfo_loadTraitButton_prefix(TraitsContainer<ActorTrait, ActorTraitButton> __instance, ActorTrait pTraitAsset)
    {
        if (Traits.all_traits.Contains(pTraitAsset.id))
        {
            __instance._traits[pTraitAsset].gameObject.SetActive(false);
        }
    }
*/
    [HarmonyPrefix]
    [HarmonyPatch(typeof(CombatActionLibrary), nameof(CombatActionLibrary.attackRangeAction))]
    private static bool CombatActionLibrary_attackRangeAction_prefix(ref AttackData pData, ref bool __result)
    {
        if (!AssetManager.projectiles.dict.ContainsKey(pData.initiator.a.getWeaponAsset().projectile))
        {
            __result = false;
            return false;
        }
        return true;
    }
    [HarmonyTranspiler, HarmonyPatch(typeof(Actor), nameof(Actor.u7_checkAugmentationEffects))]
    private static IEnumerable<CodeInstruction> Actor_u7_checkTraitEffects(IEnumerable<CodeInstruction> codes)
    {
        var list = new List<CodeInstruction>(codes);

        list[3].opcode = OpCodes.Nop;

        return list;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(City), nameof(City.hasBooksToRead))]
    private static bool City_hasBooksToRead_prefix(City __instance, Actor pActor)
    {
        // 检查pActor和__instance是否为null，防止空引用异常
        if (pActor == null || __instance == null)
            return false; // 直接返回false，不执行原方法
        return true; // 继续执行原方法
    }
    
    // 修改为正确的方法名getDamaged
    // 新增辅助方法
    private static void ChooseInheritanceSource(int parent1Value, int parent2Value, bool chooseHigher, 
                                           float randomValue,
                                           out int chosenValue, out bool fromParent1, out bool fromParent2)
    {
        if (parent1Value == parent2Value)
        {
            // 值相同时使用预生成的随机数
            bool chooseParent1 = randomValue < 0.5f;
            chosenValue = parent1Value;
            fromParent1 = chooseParent1;
            fromParent2 = !chooseParent1;
        }
        else
        {
            bool parent1IsBetter = parent1Value > parent2Value;
            bool selectParent1 = chooseHigher ? parent1IsBetter : !parent1IsBetter;
            
            chosenValue = selectParent1 ? parent1Value : parent2Value;
            fromParent1 = selectParent1;
            fromParent2 = !selectParent1;
        }
    }
    
    // 移到类级别的辅助方法
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CalculateLevelBonus(Actor parent)
    {
        if (parent == null) return 0;
        int level = parent.GetCultisysLevel();
        if (level >= 11 && level < 14) return 500;
        if (level >= 14 && level < 17) return 1000;
        if (level >= 17 && level < 20) return 2000;
        if (level >= 20 && level < 23) return 3000;
        if (level >= 23 && level < 26) return 5000; // 破界境界
        if (level >= 26) return 10000; // 镇神境界
        return 0;
    }
    
    // 获取父母数据的辅助方法，减少多次data.get调用
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void GetParentData(Actor parent, out int extraTalent, out bool isAncestor, out int levelBonus)
    {
        extraTalent = 0;
        isAncestor = false;
        levelBonus = 0;
        
        if (parent != null)
        {
            parent.data.get("extra_talent_inheritance", out extraTalent, 0);
            parent.data.get("is_blood_ancestor", out isAncestor, false);
            levelBonus = CalculateLevelBonus(parent);
        }
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Item), nameof(Item.getDamaged))]
    private static bool Item_getDamaged_prefix(Item __instance, int pDamage)
    {
        // 如果配置了武器不损失耐久度，则跳过原方法的执行
        if (ModConfigHelper.WeaponNoDurabilityLoss)
        {
            return false; // 不执行原方法
        }
        
        // 否则继续执行原方法
        return true;
    }
}