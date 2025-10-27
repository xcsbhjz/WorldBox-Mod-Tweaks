using System;
using System.Collections.Generic;
using System.Linq;
using ai;
using HarmonyLib;
using NeoModLoader.api.attributes;
using NeoModLoader.General;
using UnityEngine;
using UnityEngine.UI;
using ai.behaviours;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using AttributeExpansion.code.utils;
using System.Text;
using System.Reflection;
using ReflectionUtility;
using System.IO;
using System.Text.RegularExpressions;
using System.Timers;

namespace PeerlessThedayofGodswrath.code
{
    internal class patch
    {

        public static Dictionary<string, float> traitsAndRatios = new Dictionary<string, float>
        {
            {"OrderofBeing1", 0.1f},
            {"OrderofBeing2", 0.2f},
            {"OrderofBeing3", 0.3f},
            {"OrderofBeing4", 0.4f},
        };

        [HarmonyPostfix, HarmonyPatch(typeof(Actor), nameof(Actor.newKillAction))]
        private static void newMechanism(Actor __instance, Actor pDeadUnit)
        {
            float highestRatio = 0f;
            bool hasFound = false;

            // 先检查是否具有相同特质，并找出最高比例
            foreach (var traitRatio in traitsAndRatios)
            {
                string trait = traitRatio.Key;
                float baseRatio = traitRatio.Value;

                if (HasAnyLevelOfTrait(pDeadUnit, trait) && HasAnyLevelOfTrait(__instance, trait))
                {
                    if (baseRatio > highestRatio)
                    {
                        highestRatio = baseRatio;
                        hasFound = true;
                    }
                }
            }

            // 如果没有找到相同特质匹配，则检查交叉特质
            if (!hasFound)
            {
                foreach (var traitRatio in traitsAndRatios)
                {
                    string trait = traitRatio.Key;
                    float baseRatio = traitRatio.Value;

                    foreach (var otherTraitRatio in traitsAndRatios)
                    {
                        if (trait != otherTraitRatio.Key && HasAnyLevelOfTrait(pDeadUnit, trait) && HasAnyLevelOfTrait(__instance, otherTraitRatio.Key))
                        {
                            float crossRatio = Math.Min(baseRatio, otherTraitRatio.Value);
                            if (crossRatio > highestRatio)
                            {
                                highestRatio = crossRatio;
                                hasFound = true;
                            }
                        }
                    }
                }
            }

            // 如果找到匹配的特质，应用最高掠夺比例
            if (hasFound && highestRatio > 0f)
            {
                float deadUnitcareerexperience = pDeadUnit.Getcareerexperience();
                float additionalWuLi = deadUnitcareerexperience * highestRatio;
                __instance.Changecareerexperience(additionalWuLi);
            }
        }

        // 辅助方法：检查角色是否拥有指定特质的任意等级
        private static bool HasAnyLevelOfTrait(Actor actor, string traitBaseName)
        {
            // 检查基础特质
            if (actor.hasTrait(traitBaseName)) return true;

            // 检查带等级的特质（例如pastor1, pastor2等）
            for (int i = 1; i <= 7; i++)
            {
                if (actor.hasTrait($"{traitBaseName}{i}")) return true;
            }

            return false;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(Actor), "updateAge")]
        public static void updateWorldTime_KnightPostfix(Actor __instance)
        {
            if (__instance == null)
            {
                return;
            }

            float age = (float)__instance.getAge();
            bool hasTalent6 = __instance.hasTrait("RankTalentst92");// 检查是否具有特质

            // 检查种族是否在限定范围内
            string[] basicRaces = { "orc", "human", "elf", "dwarf" };
            if (basicRaces.Contains(__instance.asset.id) || __instance.asset.id.StartsWith("civ_"))
            {
                // 4岁时获得随机特质
                if (Mathf.FloorToInt(age) == 4000000 && !hasTalent6 &&
                !HasAnyFlairTalen(__instance) &&
                Randy.randomChance(1f))
                {
                    var talentWeights = new (string traitId, int weight)[]
                    {
                        ("RankTalentst92", 10000),
                        ("RankTalentst1", 0),
                        ("RankTalentst2", 0),
                        ("RankTalentst3", 0),
                        ("RankTalentst4", 0),
                        ("RankTalentst5", 0),
                    };

                    // 计算总权重
                    int totalWeight = talentWeights.Sum(t => t.weight);

                    // 生成随机数
                    int randomValue = UnityEngine.Random.Range(0, totalWeight);

                    // 遍历权重选择特质
                    string selectedTalent = "talent1"; // 默认值
                    foreach (var talent in talentWeights)
                    {
                        if (randomValue < talent.weight)
                        {
                            selectedTalent = talent.traitId;
                            break;
                        }
                        randomValue -= talent.weight;
                    }

                    __instance.addTrait(selectedTalent, false);
                }
            }

            var talentKnightChanges = new Dictionary<string, (float, float)>
            {
                { "RankTalentst1", (1f, 2f) },
                { "RankTalentst2", (2f, 4f) },
                { "RankTalentst3", (4f, 6f) },
                { "RankTalentst4", (6f, 8f) },
                { "RankTalentst5", (8f, 10f) },
            };

            foreach (var change in talentKnightChanges)
            {
                // 如果具有talent6特质，并且当前特质是RankTalentst1到RankTalentst5，则跳过
                if ((hasTalent6) && (change.Key == "RankTalentst1" || change.Key == "RankTalentst2" || change.Key == "RankTalentst3" || change.Key == "RankTalentst4" || change.Key == "RankTalentst5"))
                {
                    continue;
                }

                if (__instance.hasTrait(change.Key))
                {
                    float randomKnightIncrease = UnityEngine.Random.Range(change.Value.Item1, change.Value.Item2);
                    __instance.Changecareerexperience(randomKnightIncrease);
                }
            }

            var grades = new Dictionary<string, float>
            {


            };
            foreach (var grade in grades)
            {
                UpdateKnightBasedOnGrade(__instance, grade.Key, grade.Value);
            }
        }
        private static void UpdateKnightBasedOnGrade(Actor actor, string traitName, float maxKnight)
        {
            if (actor.hasTrait(traitName))
            {
                float currentKnight = actor.Getcareerexperience();
                float newValue = Mathf.Min(maxKnight, currentKnight);
                actor.Setcareerexperience(newValue);
            }
        }
        private static readonly string[] FlairTalentTraits = new[] { "RankTalentst1", "RankTalentst2", "RankTalentst3", "RankTalentst4", "RankTalentst5", "RankTalentst6", "RankTalentst7", "RankTalentst8", "RankTalentst9", "RankTalentst91" };
        private static bool HasAnyFlairTalen(Actor actor)
        {
            foreach (var trait in FlairTalentTraits)
            {
                if (actor.hasTrait(trait))
                {
                    return true;
                }
            }
            return false;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Actor), nameof(Actor.checkNaturalDeath))]
        public static bool Actor_checkNaturalDeath_Prefix(Actor __instance, ref bool __result)
        {
            if (!WorldLawLibrary.world_law_old_age.isEnabled())
            {
                __result = false;
                return false;
            }
            if (__instance.hasTrait("immortal"))
            {
                __result = false;
                return false;
            }
            float tAge = (float)__instance.getAge();
            float tLifespan = __instance.stats["lifespan"];
            if (tLifespan == 0f)
            {
                __result = false;
                return false;
            }
            if (tAge <= tLifespan)
            {
                __result = false;
                return false;
            }
            float tOverAge = tAge - tLifespan;
            float tSeverity = 5f;
            if (Randy.randomChance(Mathf.Clamp(1f / (1f + Mathf.Exp(-tSeverity * (tOverAge / tLifespan - 0.5f))), 0f, 0.9f)))
            {
                // 改为造成最大血量10000000倍的伤害
                float maxHealth = __instance.getMaxHealth();
                __instance.getHit(maxHealth * 10000000f, true, AttackType.Age);
                __result = true;
                return false;
            }
            __result = false;
            return false;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(MapAction), "checkLightningAction")]
        public static bool checkLightningAction(Vector2Int pPos, int pRad, Actor pActor = null, bool pCheckForImmortal = false, bool pCheckMayIInterrupt = false)
        {
            bool flag = false;
            List<Actor> simpleList = World.world.units.getSimpleList();
            for (int i = 0; i < simpleList.Count; i++)
            {
                Actor actor = simpleList[i];
                if (Toolbox.DistVec2(actor.current_tile.pos, pPos) <= (float)pRad)
                {
                    if (actor.asset.flag_finger)
                    {
                        actor.getActorComponent<GodFinger>().lightAction();
                    }
                    else
                    {
                        if (!flag && !actor.hasTrait("immortal") && Randy.randomChance(0.2f))
                        {
                            flag = true;
                        }

                        Achievement may_i_interrupt = AchievementLibrary.may_i_interrupt;
                        BehaviourTaskActor task = actor.ai.task;
                        may_i_interrupt.checkBySignal((task != null) ? task.id : null);
                    }
                }
            }
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(BehDragonSleepy), nameof(BehDragonSleepy.execute))]
        public static bool Prefix_BehDragonSleepy_execute(Actor pActor)
        {
            // 检查是否拥有特定特质，这里可以添加多个特质ID
            // 如果拥有这些特质，则不积累困意值（跳过原方法）
            if (pActor.hasTrait("Summonedcreature5"))
            {
                return false; // 跳过原方法，不增加困意值
            }

            return true; // 正常执行原方法
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Dragon), nameof(Dragon.landAttackTiles))]
        public static bool PrefixLandAttackTiles(Dragon __instance, WorldTile pTile, ref HashSet<WorldTile> __result)
        {
            // 获取龙的角色实例
            Actor dragonActor = __instance.actor;

            // 检查是否有特定特质
            bool hasSpecialTrait = false;
            int rangeX = 10; // 水平范围
            int rangeY = 12; // 垂直范围
            float maxDistance = 9f; // 最大距离

            // 根据特质调整攻击范围
            if (dragonActor.hasTrait("Summonedcreature5")) // 龙神特质：超大范围
            {
                hasSpecialTrait = true;
                rangeX = 60;
                rangeY = 72;
                maxDistance = 54f;
            }

            // 如果没有特殊特质，执行原方法
            if (!hasSpecialTrait)
            {
                return true;
            }

            // 使用缓存机制
            if (__instance._landAttackPosCheck == pTile)
            {
                __instance._landAttackCache++;
                __result = __instance._landAttackTiles;
                return false;
            }

            // 生成修改后的攻击范围
            __instance._landAttackCache = 0;
            __instance._landAttackTiles.Clear();
            __instance._landAttackPosCheck = pTile;

            for (int yy = 0; yy < rangeY; yy++)
            {
                for (int xx = 0; xx < rangeX * 2; xx++)
                {
                    WorldTile tTile = World.world.GetTile(pTile.pos.x + xx - rangeX, pTile.pos.y - yy + 1);
                    if (tTile != null && Toolbox.Dist(pTile.pos.x, pTile.pos.y, tTile.pos.x, tTile.pos.y) <= maxDistance)
                    {
                        __instance._landAttackTiles.Add(tTile);
                    }
                }
            }

            __result = __instance._landAttackTiles;
            return false; // 跳过原方法，使用我们的自定义结果
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Dragon), nameof(Dragon.attackRange))]
        public static bool PrefixAttackRange(Dragon __instance, bool flip, ref HashSet<WorldTile> __result)
        {
            // 获取龙的角色实例
            Actor dragonActor = __instance.actor;

            // 检查是否有特定特质
            bool hasSpecialTrait = false;
            int rangeX = 35; // 水平范围
            int rangeY = 4;  // 垂直范围
            int offsetFlip = -25;  // 翻转时的偏移
            int offsetNoFlip = 20; // 未翻转时的偏移
            int centerOffset = 15; // 中心偏移
            int yOffset = 2;       // Y轴偏移

            // 根据特质调整攻击范围
            if (dragonActor.hasTrait("Summonedcreature5")) // 龙神特质：超大范围
            {
                hasSpecialTrait = true;
                rangeX = 210;   // 水平范围扩大
                rangeY = 24;   // 垂直范围扩大
                offsetFlip = -55;
                offsetNoFlip = 50;
                centerOffset = 45;
                yOffset = 4;
            }

            // 如果没有特殊特质，执行原方法
            if (!hasSpecialTrait)
            {
                return true;
            }

            // 使用缓存机制
            if (flip)
            {
                if (__instance._slideAttackPosCheckFlip == __instance.actor.current_tile)
                {
                    __instance._slideAttackTilesFlipCache++;
                    __result = __instance._slideAttackTilesFlip;
                    return false;
                }
                __instance._slideAttackTilesFlipCache = 0;
                __instance._slideAttackTilesFlip.Clear();
                __instance._slideAttackPosCheckFlip = __instance.actor.current_tile;
            }
            else
            {
                if (__instance._slideAttackPosCheckNoFlip == __instance.actor.current_tile)
                {
                    __instance._slideAttackTilesNoFlipCache++;
                    __result = __instance._slideAttackTilesNoFlip;
                    return false;
                }
                __instance._slideAttackTilesNoFlipCache = 0;
                __instance._slideAttackTilesNoFlip.Clear();
                __instance._slideAttackPosCheckNoFlip = __instance.actor.current_tile;
            }

            // 根据朝向确定X轴偏移
            int tXOffset = flip ? offsetFlip : offsetNoFlip;

            // 生成自定义范围的攻击瓦片集合
            for (int yy = 0; yy < rangeY; yy++)
            {
                for (int xx = 0; xx < rangeX; xx++)
                {
                    WorldTile tTile = World.world.GetTile(
                        __instance.actor.current_tile.x + xx - centerOffset + tXOffset,
                        __instance.actor.current_tile.y - yy + yOffset
                    );
                    if (tTile != null)
                    {
                        if (flip)
                        {
                            __instance._slideAttackTilesFlip.Add(tTile);
                        }
                        else
                        {
                            __instance._slideAttackTilesNoFlip.Add(tTile);
                        }
                    }
                }
            }

            // 设置结果并跳过原方法
            __result = flip ? __instance._slideAttackTilesFlip : __instance._slideAttackTilesNoFlip;
            return false;
        }


        [HarmonyPrefix, HarmonyPatch(typeof(ActionLibrary), "showWhisperTip")]
        public static bool Prefix(string pText)
        {
            // 自定义逻辑：显示停留时间为x秒的提示信息
            string text = LocalizedTextManager.getText(pText, null);
            if (Config.whisper_A != null)
            {
                text = text.Replace("$kingdom_A$", Config.whisper_A.name);
            }
            if (Config.whisper_B != null)
            {
                text = text.Replace("$kingdom_B$", Config.whisper_B.name);
            }
            WorldTip.showNow(text, false, "top", 15f);


            // 如果不需要跳过原方法，则返回true.
            return false;
        }
    }
}