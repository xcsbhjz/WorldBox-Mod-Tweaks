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

namespace PeerlessThedayofGodswrath.code
{
    internal class patch
    {

        public static string[] traits =
        {
            "occupation",
        };

        [HarmonyPostfix, HarmonyPatch(typeof(Actor), nameof(Actor.newKillAction))]
        private static void newMechanism(Actor __instance, Actor pDeadUnit)
        {
            foreach (string trait in traits)
            {
                if (pDeadUnit.hasTrait(trait) && __instance.hasTrait(trait))
                {
                    float deadUnitcareerexperience = pDeadUnit.Getcareerexperience();
                    float additionalWuLi = deadUnitcareerexperience * UnityEngine.Random.Range(0.1f, 0.1f);
                    __instance.Changecareerexperience(additionalWuLi);
                }
                else
                {
                    // 检查交叉特质
                    foreach (string otherTrait in traits)
                    {
                        if (trait != otherTrait && pDeadUnit.hasTrait(trait) && __instance.hasTrait(otherTrait))
                        {
                            float deadUnitcareerexperience = pDeadUnit.Getcareerexperience();
                            float additionalWuLi = deadUnitcareerexperience * UnityEngine.Random.Range(0.1f, 0.1f);
                            __instance.Changecareerexperience(additionalWuLi);
                        }
                    }
                }
            }
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
                if (Mathf.FloorToInt(age) == 400000000 && !hasTalent6 &&
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
                { "RankTalentst5", (8f, 10f) }
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

        [HarmonyPrefix]
        [HarmonyPatch(typeof(BaseSimObject), nameof(BaseSimObject.changeHealth))]
        public static void MultiTenacityTrait_ChangeHealth(BaseSimObject __instance, ref int pValue)
        {
            if (__instance is Actor actor && pValue < 0)
            {
                if (HasProtectedTrait(actor))
                {
                    // 1. 特质减伤逻辑
                    float traitReductionMultiplier = GetTraitDamageReduction(actor);
                    if (traitReductionMultiplier > 0)
                    {
                        int damageAmount = Math.Abs(pValue);
                        int traitReduction = (int)(damageAmount * traitReductionMultiplier);
                        pValue += traitReduction;
                        
                        // 如果伤害已经被特质减伤到0或以上，则直接返回
                        if (pValue >= 0)
                        {
                            return;
                        }
                    }

                    if (actor.stats.hasStat("MagicShield"))
                    {
                        int damageAmount = Math.Abs(pValue);
                        float trueDamageShield = actor.stats["MagicShield"];
                        int shieldReduction = Math.Min((int)trueDamageShield, damageAmount);

                        if (shieldReduction > 0)
                        {
                            pValue += shieldReduction;

                            if (pValue >= 0)
                            {
                                return;
                            }
                        }
                    }

                    int currentMana = actor.data.mana;

                    if (currentMana > 0)
                    {
                        int damageAmount = Math.Abs(pValue);

                        float magicApplication = 1.0f;
                        if (actor.stats.hasStat("MagicApplication"))
                        {
                            magicApplication = actor.stats["MagicApplication"];
                        }
                        int manaToSpend = Math.Min(currentMana, (int)(damageAmount / magicApplication));

                        if (manaToSpend > 0)
                        {
                            int damageReduction = (int)(manaToSpend * magicApplication);
                            actor.spendMana(manaToSpend);
                            pValue += damageReduction;
                        }
                    }
                }
            }
        }
        
        // 获取法师职业的伤害减免倍率
        private static float GetEnchanterReduction(Actor actor)
        {
            if (actor.hasTrait("enchanter5")) return 0.8f; 
            if (actor.hasTrait("enchanter4")) return 0.4f; 
            if (actor.hasTrait("enchanter3")) return 0.2f;
            if (actor.hasTrait("enchanter2")) return 0.2f;
            if (actor.hasTrait("enchanter1")) return 0.2f;
            return -1f; // 没有该职业特质
        }

        // 获取牧师职业的伤害减免倍率
        private static float GetPastorReduction(Actor actor)
        {
            if (actor.hasTrait("pastor5")) return 0.48f;
            if (actor.hasTrait("pastor4")) return 0.24f; 
            if (actor.hasTrait("pastor3")) return 0.12f;
            if (actor.hasTrait("pastor2")) return 0.12f;
            if (actor.hasTrait("pastor1")) return 0.12f;
            return -1f; // 没有该职业特质
        }

        // 获取骑士职业的伤害减免倍率
        private static float GetKnightReduction(Actor actor)
        {
            if (actor.hasTrait("knight5")) return 0.7f;
            if (actor.hasTrait("knight4")) return 0.36f; 
            if (actor.hasTrait("knight3")) return 0.18f;
            if (actor.hasTrait("knight2")) return 0.18f;
            if (actor.hasTrait("knight1")) return 0.18f;
            return -1f; // 没有该职业特质
        }

        // 获取战士职业的伤害减免倍率
        private static float GetWarriorReduction(Actor actor)
        {
            if (actor.hasTrait("valiantgeneral5")) return 0.4f; 
            if (actor.hasTrait("valiantgeneral4")) return 0.2f; 
            if (actor.hasTrait("valiantgeneral3")) return 0.1f;
            if (actor.hasTrait("valiantgeneral2")) return 0.1f;
            if (actor.hasTrait("valiantgeneral1")) return 0.1f;
            return -1f; // 没有该职业特质
        }

        // 获取射手职业的伤害减免倍率
        private static float GetShooterReduction(Actor actor)
        {
            if (actor.hasTrait("shooter5")) return 0.32f;
            if (actor.hasTrait("shooter4")) return 0.16f; 
            if (actor.hasTrait("shooter3")) return 0.08f;
            if (actor.hasTrait("shooter2")) return 0.08f;
            if (actor.hasTrait("shooter1")) return 0.08f;
            return -1f; // 没有该职业特质
        }

        // 获取刺客职业的伤害减免倍率
        private static float GetAssassinReduction(Actor actor)
        {
            if (actor.hasTrait("assassin5")) return 0.24f; 
            if (actor.hasTrait("assassin4")) return 0.12f; 
            if (actor.hasTrait("assassin3")) return 0.06f;
            if (actor.hasTrait("assassin2")) return 0.06f;
            if (actor.hasTrait("assassin1")) return 0.06f;
            return -1f; // 没有该职业特质
        }

        // 主方法：获取角色的特质伤害减免倍率
        private static float GetTraitDamageReduction(Actor actor)
        {
            if (actor == null) return 0f;

            // 依次检查各个职业的减伤倍率
            float reduction;

            reduction = GetEnchanterReduction(actor);
            if (reduction >= 0f) return reduction;

            reduction = GetPastorReduction(actor);
            if (reduction >= 0f) return reduction;

            reduction = GetKnightReduction(actor);
            if (reduction >= 0f) return reduction;

            reduction = GetWarriorReduction(actor);
            if (reduction >= 0f) return reduction;

            reduction = GetShooterReduction(actor);
            if (reduction >= 0f) return reduction;

            reduction = GetAssassinReduction(actor);
            if (reduction >= 0f) return reduction;

            // 默认无减伤
            return 0f;
        }
        

        // 获取法师职业的伤害倍率
        private static float GetEnchanterMultiplier(Actor actor)
        {
            if (actor.hasTrait("enchanter5")) return 1.2f;
            if (actor.hasTrait("enchanter4")) return 0.8f;  // 魔法攻击型，高倍率
            if (actor.hasTrait("enchanter3")) return 0.6f;
            if (actor.hasTrait("enchanter2")) return 0.6f;
            if (actor.hasTrait("enchanter1")) return 0.6f;
            return -1f; // 没有该职业特质
        }

        // 获取牧师职业的伤害倍率
        private static float GetPastorMultiplier(Actor actor)
        {
            if (actor.hasTrait("pastor5")) return 0.3f;
            if (actor.hasTrait("pastor4")) return 0.2f;  // 辅助型，低倍率
            if (actor.hasTrait("pastor3")) return 0.15f;
            if (actor.hasTrait("pastor2")) return 0.15f;
            if (actor.hasTrait("pastor1")) return 0.15f;
            return -1f; // 没有该职业特质
        }

        // 获取骑士职业的伤害倍率
        private static float GetKnightMultiplier(Actor actor)
        {
            if (actor.hasTrait("knight5")) return 0.6f;
            if (actor.hasTrait("knight4")) return 0.5f;  // 防御型，较低倍率
            if (actor.hasTrait("knight3")) return 0.3f;
            if (actor.hasTrait("knight2")) return 0.3f;
            if (actor.hasTrait("knight1")) return 0.23f;
            return -1f; // 没有该职业特质
        }

        // 获取战士职业的伤害倍率
        private static float GetWarriorMultiplier(Actor actor)
        {
            if (actor.hasTrait("valiantgeneral5")) return 0.7f;
            if (actor.hasTrait("valiantgeneral4")) return 0.55f;  // 平衡型，中等倍率
            if (actor.hasTrait("valiantgeneral3")) return 0.35f;
            if (actor.hasTrait("valiantgeneral2")) return 0.35f;
            if (actor.hasTrait("valiantgeneral1")) return 0.35f;
            return -1f; // 没有该职业特质
        }

        // 获取射手职业的伤害倍率
        private static float GetShooterMultiplier(Actor actor)
        {
            if (actor.hasTrait("shooter5")) return 0.8f;
            if (actor.hasTrait("shooter4")) return 0.6f;  // 远程攻击型，中高倍率
            if (actor.hasTrait("shooter3")) return 0.4f;
            if (actor.hasTrait("shooter2")) return 0.4f;
            if (actor.hasTrait("shooter1")) return 0.4f;
            return -1f; // 没有该职业特质
        }

        // 获取刺客职业的伤害倍率
        private static float GetAssassinMultiplier(Actor actor)
        {
            if (actor.hasTrait("assassin5")) return 1f;  // 高爆发型，高倍率
            if (actor.hasTrait("assassin4")) return 0.7f;
            if (actor.hasTrait("assassin3")) return 0.5f;
            if (actor.hasTrait("assassin2")) return 0.5f;
            if (actor.hasTrait("assassin1")) return 0.5f;
            return -1f; // 没有该职业特质
        }

        // 主方法：获取角色的伤害倍率
        private static float GetSwordImmortalMultiplier(Actor actor)
        {
            if (actor == null) return 1f;

            // 依次检查各个职业的倍率
            float multiplier;

            multiplier = GetEnchanterMultiplier(actor);
            if (multiplier >= 0f) return multiplier;

            multiplier = GetPastorMultiplier(actor);
            if (multiplier >= 0f) return multiplier;

            multiplier = GetKnightMultiplier(actor);
            if (multiplier >= 0f) return multiplier;

            multiplier = GetWarriorMultiplier(actor);
            if (multiplier >= 0f) return multiplier;

            multiplier = GetShooterMultiplier(actor);
            if (multiplier >= 0f) return multiplier;

            multiplier = GetAssassinMultiplier(actor);
            if (multiplier >= 0f) return multiplier;

            // 默认倍率
            return 1f;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Actor), "getHit")]
        [HarmonyPriority(Priority.First)]
        public static bool SwordImmortal_actorGetHit_prefix(
            Actor __instance,
            ref float pDamage,
            bool pFlash,
            AttackType pAttackType,
            BaseSimObject pAttacker,
            bool pSkipIfShake,
            bool pMetallicWeapon)
        {
            // 设置最后攻击类型和攻击者信息
            __instance._last_attack_type = pAttackType;
            __instance.attackedBy = null;
            if (pAttacker != null && !pAttacker.isRekt() && pAttacker != __instance)
                __instance.attackedBy = pAttacker;

            // 闪避系统
            if (pAttacker is Actor attackerActor && __instance.isAlive())
            {
                float attackerhitthetarget = attackerActor.stats["hitthetarget"];
                float targetDodgeEvade = __instance.stats["DodgeEvade"];
                float effectiveDodgeEvade = Mathf.Clamp(targetDodgeEvade - attackerhitthetarget, 0f, 100f);

                if (Randy.randomChance(effectiveDodgeEvade / 100f))
                {
                    __instance.startColorEffect(ActorColorEffect.White);
                    return false;
                }
            }

            // 检查攻击者是否存在且有效
            if (pAttacker != null && pAttacker.a != null && pAttacker.a.stats != null)
            {
                Actor attacker = pAttacker.a;
                // 获取攻击者伤害属性
                float damage = attacker.stats["damage"];

                // 获取攻击者特质倍率
                float attackerMultiplier = GetSwordImmortalMultiplier(attacker);

                // 只有拥有特质时才进行特殊伤害计算
                if (attackerMultiplier >= 0f)
                {
                    // 简化计算：只保留伤害属性乘特质倍率
                    double calc = (double)damage * (double)attackerMultiplier;

                    // 安全阈值处理
                    if (calc > int.MaxValue) calc = int.MaxValue;
                    if (calc < 0) calc = 0;

                    int magicDamage = (int)calc; // <1 会变成 0

                    // 实际造成伤害
                    __instance.changeHealth(-magicDamage);
                    __instance.timer_action = 0.002f;

                    if (pFlash) __instance.startColorEffect(ActorColorEffect.Red);
                    __instance.startShake(0.3f, 0.1f, true, true);

                    // 检查单位是否死亡
                    if (!__instance.hasHealth())
                    {
                        __instance.batch.c_check_deaths.Add(__instance);
                        __instance.checkCallbacksOnDeath();
                    }
                }
            }
            return true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Actor), nameof(Actor.calculateForce))]
        public static bool CalculateForce_Protection(Actor __instance, float pStartX, float pStartY, float pTargetX, float pTargetY, float pForceAmountDirection, float pForceHeight = 0f, bool pCheckCancelJobOnLand = false)
        {
            return !HasProtectedTrait(__instance);
        }


        [HarmonyPrefix, HarmonyPatch(typeof(Actor), nameof(Actor.applyRandomForce))]
        public static bool ApplyRandomForce_Protection(Actor __instance, float pMinHeight = 1.5f, float pMaxHeight = 2f)
        {
            return !HasProtectedTrait(__instance);
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Actor), nameof(Actor.makeSleep))]
        public static bool MakeSleep_Protection(Actor __instance, float pTime)
        {
            return !HasProtectedTrait(__instance);
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(Actor), nameof(Actor.makeStunned))]
        public static bool MakeStunned_Protection(Actor __instance, float pTime = 5f)
        {
            return !HasProtectedTrait(__instance);
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Actor), nameof(Actor.addInjuryTrait))]
        private static bool PreventInjuryForProtectedTraits(Actor __instance, string pTraitID)
        {
            if (HasProtectedTrait(__instance) && (pTraitID == "crippled" || pTraitID == "eyepatch"))
            {
                return false;
            }
            return true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Actor), nameof(Actor.addStatusEffect), typeof(StatusAsset), typeof(float), typeof(bool))]
        public static bool AddStatusEffect_Protection(Actor __instance, StatusAsset pStatusAsset, float pOverrideTimer = 0f, bool pColorEffect = true)
        {
            if (pStatusAsset.id == "surprised" || pStatusAsset.id == "stunned" || pStatusAsset.id == "sleeping")
            {
                return !HasProtectedTrait(__instance);
            }
            return true;
        }

        // 锁定特定特质的耐力为最大值
        [HarmonyPostfix, HarmonyPatch(typeof(Actor), nameof(Actor.updateStamina))]
        public static void LockStaminaForSpecialTrait(Actor __instance)
        {
            // 拥有受保护特质时，耐力锁定为最大值
            if (HasProtectedTrait(__instance))
            {
                __instance.setStamina(__instance.getMaxStamina(), true);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(BehFinishReading), "checkSpecialBookRewards")]
        private static bool BehFinishReading_checkSpecialBookRewards_Prefix(Actor pActor, Book pBook)
        {
            // 检查角色是否有受保护的职业特质
            if (HasProtectedTrait(pActor))
            {
                // 如果角色有受保护的职业特质，跳过所有混乱类语言特质的触发
                foreach (LanguageTrait tTrait in pBook.getLanguage().getTraits())
                {
                    if (tTrait.group_id == "chaos" && tTrait.read_book_trait_action != null)
                    {
                        // 有受保护的职业特质，不执行混乱类特质的动作
                        continue;
                    }

                    // 非混乱类特质正常执行
                    if (tTrait.read_book_trait_action != null)
                    {
                        tTrait.read_book_trait_action(pActor, tTrait, pBook);
                    }
                }

                // 跳过原始方法
                return false;
            }

            // 没有受保护的职业特质，继续执行原始方法
            return true;
        }

        // 为氏族特质死亡束缚特质添加职业特质保护检查
        [HarmonyPrefix]
        [HarmonyPatch(typeof(BehClanChiefCheckMembersToKill), "execute")]
        private static bool BehClanChiefCheckMembersToKill_execute_Prefix(Actor pActor, ref BehResult __result)
        {
            // 检查角色是否有受保护的职业特质
            if (HasProtectedTrait(pActor))
            {
                // 如果角色有受保护的职业特质，跳过氏族特质死亡束缚特质的执行
                __result = BehResult.Continue;
                return false;
            }

            // 没有受保护的职业特质，继续执行原始方法
            return true;
        }

        private static readonly HashSet<string> ProtectedQWE = new()
        {
            // 法师特质
            "enchanter1","enchanter2","enchanter3","enchanter4","enchanter5",
            // 牧师特质
            "pastor1","pastor2","pastor3","pastor4","pastor5",
            // 骑士特质
            "knight1","knight2","knight3","knight4","knight5",
            // 战士特质
            "valiantgeneral1","valiantgeneral2","valiantgeneral3","valiantgeneral4","valiantgeneral5",
            // 射手特质
            "shooter1","shooter2","shooter3","shooter4","shooter5",
            // 刺客特质
            "assassin1","assassin2","assassin3","assassin4","assassin5"
        };
        private static bool HasProtectedTrait(Actor actor)
        {
            if (actor == null) return false;
            return ProtectedQWE.Any(trait => actor.hasTrait(trait));
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Actor), nameof(Actor.checkNaturalDeath))]
        public static bool checkNaturalDeath_Prefix(Actor __instance, ref bool __result)
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
                // 改为造成最大血量100倍的伤害
                float maxHealth = __instance.getMaxHealth();
                __instance.getHit(maxHealth * 100f, true, AttackType.Age);
                __result = true;
                return false;
            }
            __result = false;
            return false;
        }
    }
}