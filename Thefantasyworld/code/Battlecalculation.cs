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

namespace PeerlessThedayofGodswrath.code
{
    internal class Battlecalculation
    {
        // Harmony前缀补丁，在BaseSimObject的changeHealth方法执行前介入，实现多层伤害减免系统
        [HarmonyPrefix]
        [HarmonyPatch(typeof(BaseSimObject), nameof(BaseSimObject.changeHealth))]
        public static void MultiTenacityTrait_ChangeHealth(BaseSimObject __instance, ref int pValue)
        {
            // 只处理Actor类型单位，且是扣血情况（pValue < 0表示受到伤害）
            if (__instance is Actor actor && pValue < 0)
            {
                // 状态增伤逻辑：拥有特定状态时受到的伤害乘两倍
                // 可以根据实际需求修改这些状态名称
                if (actor.hasStatus("warlockstate5"))
                {
                    // 将伤害值乘以2（负数乘以2表示伤害增加）
                    pValue *= 2;
                    // 生成特效粒子表示受到了增伤效果
                    actor.spawnParticle(new Color(1f, 0.5f, 0.5f)); // 红色粒子效果
                }
                // 0. 免疫伤害：拥有新特质"damage_immunity_chance"的单位有50%的概率完全免疫伤害
                // 检测三个特质中的任意一个
                if (actor.hasTrait("Assassin5") || actor.hasTrait("Assassin6") || actor.hasTrait("Assassin7"))
                {
                    if (Randy.randomChance(0.5f))
                    {
                        // 完全免疫伤害，将伤害值设为0
                        pValue = 0;
                        // 生成特效粒子表示免疫
                        actor.spawnParticle(new Color(0.5f, 1f, 0.5f)); // 绿色粒子效果
                        return;
                    }
                }


                // 1. 状态概率免伤：单位有50%概率基于状态免疫伤害
                // 检查是否有特定的免伤状态（例如可以检查是否有"Assassinstate"类状态）
                if (actor.hasStatus("Assassinstate"))
                {
                    if (Randy.randomChance(0.5f))
                    {
                        // 完全免疫伤害，将伤害值设为0
                        pValue = 0;
                        // 生成特效粒子表示免疫（使用蓝色表示状态免伤）
                        actor.spawnParticle(new Color(0.5f, 0.8f, 1f)); // 蓝色粒子效果
                        return;
                    }
                }

                // 检查单位是否拥有受保护的特质（职业特质）
                if (HasProtectedTrait(actor))
                {
                    // 1. 第一层：特质减伤逻辑（职业专属减伤）
                    double traitRatio = GetTraitDamageRatio(actor);
                    if (traitRatio < 1.0) // 如果减伤比例小于1，表示有减伤效果
                    {
                        // 计算特质减免后的伤害：原始伤害 × 承受伤害比例
                        double damageAfterReduction = Math.Abs((double)pValue) * traitRatio;

                        // 如果减免后的伤害绝对值小于1，则归零（避免微小伤害）
                        if (Math.Abs(damageAfterReduction) < 1)
                        {
                            pValue = 0; // 伤害归零
                        }
                        else
                        {
                            // 使用向下取整确保伤害值为整数，并恢复负号
                            pValue = (int)Math.Floor(-damageAfterReduction);
                        }

                        // 如果伤害已经被特质减伤到0或以上，则直接返回（无需后续处理）
                        if (pValue >= 0)
                        {
                            return;
                        }
                    }

                    // 2. 第二层：魔法护盾减伤逻辑
                    if (actor.stats.hasStat("MagicShield"))
                    {
                        double damageAmount = Math.Abs((double)pValue);
                        float trueDamageShield = actor.stats["MagicShield"];

                        // 计算护盾可抵消的伤害比例：护盾值 / 伤害量，最大为100%
                        double shieldRatio = Math.Min(trueDamageShield / damageAmount, 1.0);

                        // 计算护盾减免后的伤害：剩余伤害 × (1 - 护盾抵消比例)
                        double damageAfterShield = damageAmount * (1 - shieldRatio);

                        // 如果减免后的伤害绝对值小于1，则归零
                        if (Math.Abs(damageAfterShield) < 1)
                        {
                            pValue = 0; // 伤害归零
                        }
                        else
                        {
                            // 向下取整并恢复负号
                            pValue = (int)Math.Floor(-damageAfterShield);
                        }

                        // 如果伤害已经被护盾减伤到0或以上，则直接返回
                        if (pValue >= 0)
                        {
                            return;
                        }
                    }

                    // 3. 第三层：魔力抵消伤害逻辑
                    int currentMana = actor.data.mana;

                    // 确保单位有魔力可以消耗
                    if (currentMana > 0)
                    {
                        double damageAmount = Math.Abs((double)pValue);

                        // 获取魔法应用效率属性，默认1.0
                        float magicApplication = 1.0f;
                        if (actor.stats.hasStat("MagicApplication"))
                        {
                            magicApplication = actor.stats["MagicApplication"];
                        }

                        // 计算魔力可抵消的伤害比例：(当前魔力 × 魔法效率) / 伤害量，最大为100%
                        double manaProtection = Math.Min(currentMana * magicApplication / damageAmount, 1.0);

                        // 计算魔力抵消后的伤害：剩余伤害 × (1 - 魔力抵消比例)
                        double damageAfterMana = damageAmount * (1 - manaProtection);

                        // 如果减免后的伤害绝对值小于1，则归零
                        if (Math.Abs(damageAfterMana) < 1)
                        {
                            pValue = 0; // 伤害归零
                        }
                        else
                        {
                            // 向下取整并恢复负号
                            pValue = (int)Math.Floor(-damageAfterMana);
                        }

                        // 消耗对应的魔力值：伤害量 × 魔力抵消比例 / 魔法效率
                        if (manaProtection > 0)
                        {
                            actor.spendMana((int)(damageAmount * manaProtection / magicApplication));
                        }
                    }
                }
            }
        }

        // 获取法师职业的承受伤害比例（数字越小减伤越高）
        private static double GetEnchanterRatio(Actor actor)
        {
            if (actor.hasTrait("enchanter7")) return 0.01171875; // 7级：承受1%伤害（减伤99%）
            if (actor.hasTrait("enchanter6")) return 0.0234375;  // 6级：承受2%伤害（减伤98%）
            if (actor.hasTrait("enchanter5")) return 0.046875;   // 5级：承受5%伤害（减伤95%）
            if (actor.hasTrait("enchanter4")) return 0.09375;    // 4级：承受9%伤害（减伤91%）
            if (actor.hasTrait("enchanter3")) return 0.1875;     // 3级：承受19%伤害（减伤81%）
            if (actor.hasTrait("enchanter2")) return 0.375;      // 2级：承受37.5%伤害（减伤62.5%）
            if (actor.hasTrait("enchanter1")) return 0.75;       // 1级：承受75%伤害（减伤25%）
            return 1.0; // 没有该职业特质，承受100%伤害
        }

        // 获取牧师职业的承受伤害比例
        private static double GetPastorRatio(Actor actor)
        {
            if (actor.hasTrait("pastor7")) return 0.0121875; // 7级：承受1.21875%伤害（减伤98.78125%）
            if (actor.hasTrait("pastor6")) return 0.024375;  // 6级：承受2.4375%伤害（减伤97.5625%）
            if (actor.hasTrait("pastor5")) return 0.04875;   // 5级：承受4.875%伤害（减伤95.125%）
            if (actor.hasTrait("pastor4")) return 0.0975;    // 4级：承受9.75%伤害（减伤90.25%）
            if (actor.hasTrait("pastor3")) return 0.195;     // 3级：承受19.5%伤害（减伤80.5%）
            if (actor.hasTrait("pastor2")) return 0.39;      // 2级：承受39%伤害（减伤61%）
            if (actor.hasTrait("pastor1")) return 0.78;       // 1级：承受78%伤害（减伤22%）
            return 1.0;
        }

        // 获取骑士职业的承受伤害比例
        private static double GetKnightRatio(Actor actor)
        {
            if (actor.hasTrait("knight7")) return 0.0109375; // 7级：承受1.09375%伤害（减伤98.90625%）
            if (actor.hasTrait("knight6")) return 0.021875;  // 6级：承受2.1875%伤害（减伤97.8125%）
            if (actor.hasTrait("knight5")) return 0.04375;   // 5级：承受4.375%伤害（减伤95.625%）
            if (actor.hasTrait("knight4")) return 0.0875;    // 4级：承受8.75%伤害（减伤91.25%）
            if (actor.hasTrait("knight3")) return 0.175;     // 3级：承受17.5%伤害（减伤82.5%）
            if (actor.hasTrait("knight2")) return 0.35;      // 2级：承受35%伤害（减伤65%）
            if (actor.hasTrait("knight1")) return 0.70;       // 1级：承受70%伤害（减伤30%）
            return 1.0;
        }

        // 获取战士职业的承受伤害比例
        private static double GetWarriorRatio(Actor actor)
        {
            if (actor.hasTrait("valiantgeneral7")) return 0.0125; // 7级：承受1.25%伤害（减伤98.75%）
            if (actor.hasTrait("valiantgeneral6")) return 0.025;  // 6级：承受2.5%伤害（减伤97.5%）
            if (actor.hasTrait("valiantgeneral5")) return 0.05;   // 5级：承受5%伤害（减伤95%）
            if (actor.hasTrait("valiantgeneral4")) return 0.1;    // 4级：承受10%伤害（减伤90%）
            if (actor.hasTrait("valiantgeneral3")) return 0.2;     // 3级：承受20%伤害（减伤80%）
            if (actor.hasTrait("valiantgeneral2")) return 0.4;      // 2级：承受40%伤害（减伤60%）
            if (actor.hasTrait("valiantgeneral1")) return 0.8;       // 1级：承受80%伤害（减伤20%）
            return 1.0;
        }

        // 获取射手职业的承受伤害比例
        private static double GetRangerRatio(Actor actor)
        {
            if (actor.hasTrait("Ranger7")) return 0.0128125; // 7级：承受1.28125%伤害（减伤98.71875%）
            if (actor.hasTrait("Ranger6")) return 0.025625;  // 6级：承受2.5625%伤害（减伤97.4375%）
            if (actor.hasTrait("Ranger5")) return 0.05125;   // 5级：承受5.125%伤害（减伤94.875%）
            if (actor.hasTrait("Ranger4")) return 0.1025;    // 4级：承受10.25%伤害（减伤89.75%）
            if (actor.hasTrait("Ranger3")) return 0.205;     // 3级：承受20.5%伤害（减伤79.5%）
            if (actor.hasTrait("Ranger2")) return 0.41;      // 2级：承受41%伤害（减伤59%）
            if (actor.hasTrait("Ranger1")) return 0.82;       // 1级：承受82%伤害（减伤18%）
            return 1.0;
        }

        // 获取刺客职业的承受伤害比例
        private static double GetAssassinRatio(Actor actor)
        {
            if (actor.hasTrait("Assassin7")) return 0.01328125; // 7级：承受1.328125%伤害（减伤98.671875%）
            if (actor.hasTrait("Assassin6")) return 0.0265625;  // 6级：承受2.65625%伤害（减伤97.34375%）
            if (actor.hasTrait("Assassin5")) return 0.053125;   // 5级：承受5.3125%伤害（减伤94.6875%）
            if (actor.hasTrait("Assassin4")) return 0.10625;    // 4级：承受10.625%伤害（减伤89.375%）
            if (actor.hasTrait("Assassin3")) return 0.2125;     // 3级：承受21.25%伤害（减伤78.75%）
            if (actor.hasTrait("Assassin2")) return 0.425;      // 2级：承受42.5%伤害（减伤57.5%）
            if (actor.hasTrait("Assassin1")) return 0.85;       // 1级：承受85%伤害（减伤15%）
            return 1.0;
        }

        // 获取召唤师职业的承受伤害比例
        private static double GetSummonerRatio(Actor actor)
        {
            if (actor.hasTrait("Summoner7")) return 0.0125; // 7级：承受1.25%伤害（减伤98.75%）
            if (actor.hasTrait("Summoner6")) return 0.025;  // 6级：承受2.5%伤害（减伤97.5%）
            if (actor.hasTrait("Summoner5")) return 0.05;   // 5级：承受5%伤害（减伤95%）
            if (actor.hasTrait("Summoner4")) return 0.1;    // 4级：承受10%伤害（减伤90%）
            if (actor.hasTrait("Summoner3")) return 0.2;     // 3级：承受20%伤害（减伤80%）
            if (actor.hasTrait("Summoner2")) return 0.4;      // 2级：承受40%伤害（减伤60%）
            if (actor.hasTrait("Summoner1")) return 0.8;       // 1级：承受80%伤害（减伤20%）
            return 1.0;
        }

        // 获取吟游诗人职业的承受伤害比例
        private static double GetMinstrelRatio(Actor actor)
        {
            if (actor.hasTrait("minstrel7")) return 0.01265625; // 7级：承受1.265625%伤害（减伤98.734375%）
            if (actor.hasTrait("minstrel6")) return 0.0253125;  // 6级：承受2.53125%伤害（减伤97.46875%）
            if (actor.hasTrait("minstrel5")) return 0.050625;   // 5级：承受5.0625%伤害（减伤94.9375%）
            if (actor.hasTrait("minstrel4")) return 0.10125;    // 4级：承受10.125%伤害（减伤89.875%）
            if (actor.hasTrait("minstrel3")) return 0.2025;     // 3级：承受20.25%伤害（减伤79.75%）
            if (actor.hasTrait("minstrel2")) return 0.405;      // 2级：承受40.5%伤害（减伤59.5%）
            if (actor.hasTrait("minstrel1")) return 0.81;       // 1级：承受81%伤害（减伤19%）
            return 1.0;
        }

        // 获取咒术师职业的承受伤害比例
        private static double GetWarlockRatio(Actor actor)
        {
            if (actor.hasTrait("warlock7")) return 0.01265625; // 7级：承受1.265625%伤害（减伤98.734375%）
            if (actor.hasTrait("warlock6")) return 0.0253125;  // 6级：承受2.53125%伤害（减伤97.46875%）
            if (actor.hasTrait("warlock5")) return 0.050625;   // 5级：承受5.0625%伤害（减伤94.9375%）
            if (actor.hasTrait("warlock4")) return 0.10125;    // 4级：承受10.125%伤害（减伤89.875%）
            if (actor.hasTrait("warlock3")) return 0.2025;     // 3级：承受20.25%伤害（减伤79.75%）
            if (actor.hasTrait("warlock2")) return 0.405;      // 2级：承受40.5%伤害（减伤59.5%）
            if (actor.hasTrait("warlock1")) return 0.81;       // 1级：承受81%伤害（减伤19%）
            return 1.0;
        }

        // 获取炼金术士职业的承受伤害比例
        private static double GetAlchemistRatio(Actor actor)
        {
            if (actor.hasTrait("alchemist7")) return 0.0125; // 7级：承受1.25%伤害（减伤98.75%）
            if (actor.hasTrait("alchemist6")) return 0.025;  // 6级：承受2.5%伤害（减伤97.5%）
            if (actor.hasTrait("alchemist5")) return 0.05;   // 5级：承受5%伤害（减伤95%）
            if (actor.hasTrait("alchemist4")) return 0.1;    // 4级：承受10%伤害（减伤90%）
            if (actor.hasTrait("alchemist3")) return 0.2;     // 3级：承受20%伤害（减伤80%）
            if (actor.hasTrait("alchemist2")) return 0.4;      // 2级：承受40%伤害（减伤60%）
            if (actor.hasTrait("alchemist1")) return 0.8;       // 1级：承受80%伤害（减伤20%）
            return 1.0;
        }

        // 获取野蛮人职业的承受伤害比例
        private static double GetBarbarianRatio(Actor actor)
        {
            if (actor.hasTrait("barbarian7")) return 0.0125; // 7级：承受1.25%伤害（减伤98.75%）
            if (actor.hasTrait("barbarian6")) return 0.025;  // 6级：承受2.5%伤害（减伤97.5%）
            if (actor.hasTrait("barbarian5")) return 0.05;   // 5级：承受5%伤害（减伤95%）
            if (actor.hasTrait("barbarian4")) return 0.1;    // 4级：承受10%伤害（减伤90%）
            if (actor.hasTrait("barbarian3")) return 0.2;     // 3级：承受20%伤害（减伤80%）
            if (actor.hasTrait("barbarian2")) return 0.4;      // 2级：承受40%伤害（减伤60%）
            if (actor.hasTrait("barbarian1")) return 0.8;       // 1级：承受80%伤害（减伤20%）
            return 1.0;
        }

        // 获取召唤兽职业的承受伤害比例
        private static double GetSummonedcreatureRatio(Actor actor)
        {
            if (actor.hasTrait("Summonedcreature5")) return 0.0125; // 5级：承受1.25%伤害（减伤98.75%）
            if (actor.hasTrait("Summonedcreature4")) return 0.025;  // 4级：承受2.5%伤害（减伤97.5%）
            if (actor.hasTrait("Summonedcreature3")) return 0.05;   // 3级：承受5%伤害（减伤95%）
            if (actor.hasTrait("Summonedcreature2")) return 0.1;    // 2级：承受10%伤害（减伤90%）
            if (actor.hasTrait("Summonedcreature1")) return 0.2;     // 1级：承受20%伤害（减伤80%）
            return 1.0;
        }

        // 主方法：获取角色的特质伤害承受比例（按职业优先级）
        private static double GetTraitDamageRatio(Actor actor)
        {
            if (actor == null) return 1.0;

            // 依次检查各个职业的承受比例，按优先级从高到低
            double ratio;

            // 法师职业（最高优先级）
            ratio = GetEnchanterRatio(actor);
            if (ratio < 1.0) return ratio;

            // 牧师职业
            ratio = GetPastorRatio(actor);
            if (ratio < 1.0) return ratio;

            // 骑士职业
            ratio = GetKnightRatio(actor);
            if (ratio < 1.0) return ratio;

            // 战士职业
            ratio = GetWarriorRatio(actor);
            if (ratio < 1.0) return ratio;

            // 射手职业
            ratio = GetRangerRatio(actor);
            if (ratio < 1.0) return ratio;

            // 刺客职业
            ratio = GetAssassinRatio(actor);
            if (ratio < 1.0) return ratio;

            // 召唤师职业
            ratio = GetSummonerRatio(actor);
            if (ratio < 1.0) return ratio;

            // 吟游诗人职业
            ratio = GetMinstrelRatio(actor);
            if (ratio < 1.0) return ratio;

            // 咒术师职业
            ratio = GetWarlockRatio(actor);
            if (ratio < 1.0) return ratio;

            // 炼金术士职业
            ratio = GetAlchemistRatio(actor);
            if (ratio < 1.0) return ratio;

            // 野蛮人职业（最低优先级）
            ratio = GetBarbarianRatio(actor);
            if (ratio < 1.0) return ratio;

            // 召唤兽职业（最低优先级）
            ratio = GetSummonedcreatureRatio(actor);
            if (ratio < 1.0) return ratio;

            // 默认无减伤，承受100%伤害
            return 1.0;
        }


        // 获取法师职业的伤害倍率（法伤百分比转换为小数倍率）
        private static float GetEnchanterMultiplier(Actor actor)
        {
            if (actor.hasTrait("enchanter7")) return 64f;  // 5120%
            if (actor.hasTrait("enchanter6")) return 32f;  // 2560%
            if (actor.hasTrait("enchanter5")) return 16f;  // 1280%
            if (actor.hasTrait("enchanter4")) return 8f;   // 640%
            if (actor.hasTrait("enchanter3")) return 4f;   // 320%
            if (actor.hasTrait("enchanter2")) return 2f;   // 160%
            if (actor.hasTrait("enchanter1")) return 1f;   // 80%
            return -1f; // 没有该职业特质
        }

        // 获取牧师职业的伤害倍率（法伤百分比转换为小数倍率）
        private static float GetPastorMultiplier(Actor actor)
        {
            if (actor.hasTrait("pastor7")) return 12.8f;   // 1280%
            if (actor.hasTrait("pastor6")) return 6.4f;    // 640%
            if (actor.hasTrait("pastor5")) return 3.2f;    // 320%
            if (actor.hasTrait("pastor4")) return 1.6f;    // 160%
            if (actor.hasTrait("pastor3")) return 0.8f;    // 80%
            if (actor.hasTrait("pastor2")) return 0.4f;    // 40%
            if (actor.hasTrait("pastor1")) return 0.2f;    // 20%
            return -1f; // 没有该职业特质
        }

        // 获取骑士职业的伤害倍率（法伤百分比转换为小数倍率）
        private static float GetKnightMultiplier(Actor actor)
        {
            if (actor.hasTrait("knight7")) return 19.2f;   // 1920%
            if (actor.hasTrait("knight6")) return 9.6f;    // 960%
            if (actor.hasTrait("knight5")) return 4.8f;    // 480%
            if (actor.hasTrait("knight4")) return 2.4f;    // 240%
            if (actor.hasTrait("knight3")) return 1.2f;    // 120%
            if (actor.hasTrait("knight2")) return 0.6f;    // 60%
            if (actor.hasTrait("knight1")) return 0.3f;    // 30%
            return -1f; // 没有该职业特质
        }

        // 获取战士职业的伤害倍率（法伤百分比转换为小数倍率）
        private static float GetWarriorMultiplier(Actor actor)
        {
            if (actor.hasTrait("valiantgeneral7")) return 19.2f;   // 1920%
            if (actor.hasTrait("valiantgeneral6")) return 9.6f;    // 960%
            if (actor.hasTrait("valiantgeneral5")) return 4.8f;    // 480%
            if (actor.hasTrait("valiantgeneral4")) return 2.4f;    // 240%
            if (actor.hasTrait("valiantgeneral3")) return 1.2f;    // 120%
            if (actor.hasTrait("valiantgeneral2")) return 0.6f;    // 60%
            if (actor.hasTrait("valiantgeneral1")) return 0.3f;    // 30%
            return -1f; // 没有该职业特质
        }

        // 获取射手职业的伤害倍率（法伤百分比转换为小数倍率）
        private static float GetRangerMultiplier(Actor actor)
        {
            if (actor.hasTrait("Ranger7")) return 22.4f;   // 2240%
            if (actor.hasTrait("Ranger6")) return 11.2f;   // 1120%
            if (actor.hasTrait("Ranger5")) return 5.6f;    // 560%
            if (actor.hasTrait("Ranger4")) return 2.8f;    // 280%
            if (actor.hasTrait("Ranger3")) return 1.4f;    // 140%
            if (actor.hasTrait("Ranger2")) return 0.7f;    // 70%
            if (actor.hasTrait("Ranger1")) return 0.35f;   // 35%
            return -1f; // 没有该职业特质
        }

        // 获取刺客职业的伤害倍率（法伤百分比转换为小数倍率）
        private static float GetAssassinMultiplier(Actor actor)
        {
            if (actor.hasTrait("Assassin7")) return 44.8f;   // 4480%
            if (actor.hasTrait("Assassin6")) return 22.4f;   // 2240%
            if (actor.hasTrait("Assassin5")) return 11.2f;   // 1120%
            if (actor.hasTrait("Assassin4")) return 5.6f;    // 560%
            if (actor.hasTrait("Assassin3")) return 2.8f;    // 280%
            if (actor.hasTrait("Assassin2")) return 1.4f;    // 140%
            if (actor.hasTrait("Assassin1")) return 0.7f;    // 70%
            return -1f; // 没有该职业特质
        }

        // 获取召唤师职业的伤害倍率（法伤百分比转换为小数倍率）
        private static float GetSummonerMultiplier(Actor actor)
        {
            if (actor.hasTrait("Summoner7")) return 16f;    // 1600%
            if (actor.hasTrait("Summoner6")) return 8f;     // 800%
            if (actor.hasTrait("Summoner5")) return 4f;     // 400%
            if (actor.hasTrait("Summoner4")) return 2f;     // 200%
            if (actor.hasTrait("Summoner3")) return 1f;     // 100%
            if (actor.hasTrait("Summoner2")) return 0.5f;   // 50%
            if (actor.hasTrait("Summoner1")) return 0.25f;  // 25%
            return -1f; // 没有该职业特质
        }

        // 获取吟游诗人职业的伤害倍率（法伤百分比转换为小数倍率）
        private static float GetMinstrelMultiplier(Actor actor)
        {
            if (actor.hasTrait("minstrel7")) return 17.92f;   // 1792%
            if (actor.hasTrait("minstrel6")) return 8.96f;    // 896%
            if (actor.hasTrait("minstrel5")) return 4.48f;    // 448%
            if (actor.hasTrait("minstrel4")) return 2.24f;    // 224%
            if (actor.hasTrait("minstrel3")) return 1.12f;    // 112%
            if (actor.hasTrait("minstrel2")) return 0.56f;    // 56%
            if (actor.hasTrait("minstrel1")) return 0.28f;    // 28%
            return -1f; // 没有该职业特质
        }

        // 获取咒术师职业的伤害倍率（法伤百分比转换为小数倍率）
        private static float GetWarlockMultiplier(Actor actor)
        {
            if (actor.hasTrait("warlock7")) return 38.4f;    // 3840%
            if (actor.hasTrait("warlock6")) return 19.2f;    // 1920%
            if (actor.hasTrait("warlock5")) return 9.6f;     // 960%
            if (actor.hasTrait("warlock4")) return 4.8f;     // 480%
            if (actor.hasTrait("warlock3")) return 2.4f;     // 240%
            if (actor.hasTrait("warlock2")) return 1.2f;     // 120%
            if (actor.hasTrait("warlock1")) return 0.6f;     // 60%
            return -1f; // 没有该职业特质
        }

        // 获取炼金术士职业的伤害倍率（法伤百分比转换为小数倍率）
        private static float GetAlchemistMultiplier(Actor actor)
        {
            if (actor.hasTrait("alchemist7")) return 25.6f;   // 2560%
            if (actor.hasTrait("alchemist6")) return 12.8f;   // 1280%
            if (actor.hasTrait("alchemist5")) return 6.4f;    // 640%
            if (actor.hasTrait("alchemist4")) return 3.2f;    // 320%
            if (actor.hasTrait("alchemist3")) return 1.6f;    // 160%
            if (actor.hasTrait("alchemist2")) return 0.8f;    // 80%
            if (actor.hasTrait("alchemist1")) return 0.4f;    // 40%
            return -1f; // 没有该职业特质
        }

        // 获取野蛮人职业的伤害倍率（法伤百分比转换为小数倍率）
        private static float GetBarbarianMultiplier(Actor actor)
        {
            if (actor.hasTrait("barbarian7")) return 9.6f;    // 960%
            if (actor.hasTrait("barbarian6")) return 4.8f;    // 480%
            if (actor.hasTrait("barbarian5")) return 2.4f;    // 240%
            if (actor.hasTrait("barbarian4")) return 1.2f;    // 120%
            if (actor.hasTrait("barbarian3")) return 0.6f;    // 60%
            if (actor.hasTrait("barbarian2")) return 0.3f;    // 30%
            if (actor.hasTrait("barbarian1")) return 0.15f;   // 15%
            return -1f; // 没有该职业特质
        }

        // 获取召唤师职业的伤害倍率（法伤百分比转换为小数倍率）
        private static float GetSummonedcreatureMultiplier(Actor actor)
        {
            if (actor.hasTrait("Summonedcreature5")) return 16f;    // 1600%
            if (actor.hasTrait("Summonedcreature4")) return 8f;     // 800%
            if (actor.hasTrait("Summonedcreature3")) return 4f;     // 400%
            if (actor.hasTrait("Summonedcreature2")) return 2f;     // 200%
            if (actor.hasTrait("Summonedcreature1")) return 1f;     // 100%
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

            multiplier = GetRangerMultiplier(actor);
            if (multiplier >= 0f) return multiplier;

            multiplier = GetAssassinMultiplier(actor);
            if (multiplier >= 0f) return multiplier;

            multiplier = GetSummonerMultiplier(actor);
            if (multiplier >= 0f) return multiplier;

            multiplier = GetMinstrelMultiplier(actor);
            if (multiplier >= 0f) return multiplier;

            multiplier = GetWarlockMultiplier(actor);
            if (multiplier >= 0f) return multiplier;

            multiplier = GetAlchemistMultiplier(actor);
            if (multiplier >= 0f) return multiplier;

            multiplier = GetBarbarianMultiplier(actor);
            if (multiplier >= 0f) return multiplier;

            multiplier = GetSummonedcreatureMultiplier(actor);
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
                    // 简化计算：伤害属性乘特质倍率
                    double calc = (double)damage * (double)attackerMultiplier;

                    // 再加上固定法伤
                    calc += attacker.stats["Fixedwound"];

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

        private static readonly HashSet<string> RangerSkill = new()
        {
            "Ranger5", "Ranger6", "Ranger7"
        };
        private static bool RangerSkillCaveHeaven(Actor actor)
        {
            if (actor == null) return false;
            return RangerSkill.Any(trait => actor.hasTrait(trait));
        }
        
        // 用于追踪游侠技能的冷却时间
        private static readonly Dictionary<long, double> lastRangerSkillActivationTime = new Dictionary<long, double>();
        private const double rangerSkillCooldown = 120; // 600秒游戏时间冷却

        [HarmonyPrefix, HarmonyPatch(typeof(Actor), nameof(Actor.checkDeath))]
        private static bool Actor_CheckDeath_RangerSkillCaveHeaven(Actor __instance)
        {
            if (!__instance.hasHealth() && __instance.isAlive())
            {
                if (RangerSkillCaveHeaven(__instance))
                {
                    long actorId = __instance.getID();
                    double currentWorldTime = World.world.getCurWorldTime();
                    
                    // 检查是否在冷却时间内
                    if (!lastRangerSkillActivationTime.TryGetValue(actorId, out double lastActivationTime) || 
                        currentWorldTime - lastActivationTime >= rangerSkillCooldown)
                    {
                        string grottoTrait = RangerSkill.FirstOrDefault(t => __instance.hasTrait(t));
                        __instance.setHealth(__instance.getMaxHealth(), true);
                        __instance.setMana(__instance.getMaxMana(), true);
                        Vector3 actorPos = new Vector3(__instance.current_position.x, __instance.current_position.y, 0f);
                        EffectsLibrary.spawnExplosionWave(actorPos, 0.05f, 6f);
                        MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionSmall", __instance.current_position.x, __instance.current_position.y, false, false);
                        
                        // 更新激活时间
                        lastRangerSkillActivationTime[actorId] = currentWorldTime;
                        return false;
                    }
                }
            }
            return true;
        }

        private static readonly Dictionary<long, double> lastHealthRegenWorldTimeDict = new Dictionary<long, double>();
        private static readonly Dictionary<long, double> lastMagicRegenWorldTimeDict = new Dictionary<long, double>();

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Actor), "b6_updateAI")]
        public static void Actor_updateAI_HealthRegen_ByRestoreHealthAttribute(Actor __instance)
        {
            if (__instance == null || !__instance.isAlive())
            {
                if (__instance != null)
                {
                    lastHealthRegenWorldTimeDict.Remove(__instance.getID());
                    lastMagicRegenWorldTimeDict.Remove(__instance.getID());
                }
                return;
            }

            long actorId = __instance.getID();
            double currentWorldTime = World.world.getCurWorldTime();
            const double regenInterval = 1.0; // 1秒游戏时间作为冷却间隔

            // 处理生命值回复
            float restoreHealthValue = 0f;
            if (__instance.stats != null && __instance.stats.hasStat("Restorehealth"))
            {
                restoreHealthValue = __instance.stats["Restorehealth"];
            }

            if (restoreHealthValue > 0f)
            {
                // 检查上次回血时间
                if (!lastHealthRegenWorldTimeDict.TryGetValue(actorId, out double lastHealthRegenTime))
                {
                    lastHealthRegenWorldTimeDict[actorId] = currentWorldTime;
                }
                else if (currentWorldTime - lastHealthRegenTime >= regenInterval)
                {
                    if (!__instance.hasMaxHealth())
                    {
                        // 根据Restorehealth属性值回血
                        int healthToRestore = Mathf.CeilToInt(restoreHealthValue);
                        __instance.restoreHealth(healthToRestore);

                        // 生成治疗粒子效果
                        if (!__instance.hasMaxHealth())
                        {
                            __instance.spawnParticle(Toolbox.color_heal);
                        }
                    }

                    // 更新上次回血时间
                    lastHealthRegenWorldTimeDict[actorId] = currentWorldTime;
                }
            }

            // 处理魔力回复
            float restoreMagicValue = 0f;
            if (__instance.stats != null && __instance.stats.hasStat("MagicReply"))
            {
                restoreMagicValue = __instance.stats["MagicReply"];
            }

            if (restoreMagicValue > 0f)
            {
                // 检查上次回魔时间
                if (!lastMagicRegenWorldTimeDict.TryGetValue(actorId, out double lastMagicRegenTime))
                {
                    lastMagicRegenWorldTimeDict[actorId] = currentWorldTime;
                }
                else if (currentWorldTime - lastMagicRegenTime >= regenInterval)
                {
                    // 使用与traitAction.cs中相同的方式添加魔力
                    if (__instance.data.mana < __instance.getMaxMana())
                    {
                        // 根据MagicReply属性值回魔
                        int magicToRestore = Mathf.CeilToInt(restoreMagicValue);
                        __instance.addMana(magicToRestore);

                        // 生成魔力恢复粒子效果（使用蓝色表示魔法）
                        __instance.spawnParticle(new Color(0f, 0.5f, 1f));
                    }

                    // 更新上次回魔时间
                    lastMagicRegenWorldTimeDict[actorId] = currentWorldTime;
                }
            }
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

        [HarmonyPrefix]
        [HarmonyPatch(typeof(MapBox), nameof(MapBox.checkAttackFor))]
        public static bool MapBox_checkAttackFor_AbsoluteHit_Prefix(AttackData pData, BaseSimObject pTargetToCheck, ref AttackDataResult __result)
        {
            if (pTargetToCheck == null || !pTargetToCheck.isAlive()) 
            {
                __result = AttackDataResult.Continue;
                return false;
            }
            
            Actor owner = null;
            try
            {
                if (pData.initiator != null && pData.initiator.isActor()) 
                    owner = pData.initiator.a;
            }
            catch { /* ignore */ }

            if (owner == null || !HasProtectedTrait(owner))
                return true;

            try { __result = MapBox.applyAttack(pData, pTargetToCheck); }
            catch { __result = AttackDataResult.Continue; }
            return false;
        }

        private static readonly HashSet<string> ProtectedQWE = new()
        {
            // 法师特质
            "enchanter1","enchanter2","enchanter3","enchanter4","enchanter5","enchanter6","enchanter7",
            // 牧师特质
            "pastor1","pastor2","pastor3","pastor4","pastor5","pastor6","pastor7",
            // 骑士特质
            "knight1","knight2","knight3","knight4","knight5","knight6","knight7",
            // 战士特质
            "valiantgeneral1","valiantgeneral2","valiantgeneral3","valiantgeneral4","valiantgeneral5","valiantgeneral6","valiantgeneral7",
            // 射手特质
            "Ranger1","Ranger2","Ranger3","Ranger4","Ranger5","Ranger6","Ranger7",
            // 刺客特质
            "Assassin1","Assassin2","Assassin3","Assassin4","Assassin5","Assassin6","Assassin7",
            // 召唤师特质
            "Summoner1","Summoner2","Summoner3","Summoner4","Summoner5","Summoner6","Summoner7",
            // 吟游诗人特质
            "minstrel1","minstrel2","minstrel3","minstrel4","minstrel5","minstrel6","minstrel7",
            // 咒术师特质
            "warlock1","warlock2","warlock3","warlock4","warlock5","warlock6","warlock7",
            // 炼金术士特质
            "alchemist1","alchemist2","alchemist3","alchemist4","alchemist5","alchemist6","alchemist7",      
            // 野蛮人特质
            "barbarian1","barbarian2","barbarian3","barbarian4","barbarian5","barbarian6","barbarian7",
            // 受保护的召唤兽特质
            "Summonedcreature1","Summonedcreature2","Summonedcreature3","Summonedcreature4","Summonedcreature5"
        };
        private static bool HasProtectedTrait(Actor actor)
        {
            if (actor == null) return false;
            return ProtectedQWE.Any(trait => actor.hasTrait(trait));
        }
    }
}