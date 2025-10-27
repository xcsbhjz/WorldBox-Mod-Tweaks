using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ai;
using UnityEngine;
using AttributeExpansion.code.utils;
using ReflectionUtility;
using System.IO;

namespace PeerlessThedayofGodswrath.code
{
    internal class warlockSkill
    {
        // 用于存储每个目标对应的施加者信息的字典
        private static readonly Dictionary<BaseSimObject, Tuple<BaseSimObject, float>> _inflictorInfoMap = new Dictionary<BaseSimObject, Tuple<BaseSimObject, float>>();
        public static bool attack_warlockstate1(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 默认值
            float pDamage = 1f;
            BaseSimObject damageSource = null;
            
            // 从字典中获取施加者信息，如果存在则使用
            if (_inflictorInfoMap.TryGetValue(pTarget, out var inflictorInfo))
            {
                damageSource = inflictorInfo.Item1; // 施加者对象
                pDamage = inflictorInfo.Item2;      // 施加者伤害值
                
                // 检查施加者是否仍然有效
                if (damageSource == null || damageSource.isRekt())
                {
                    damageSource = null;
                }
            }

            // 目标生命值大于-1，则对目标施加伤害
            if (Randy.randomChance(1f) && pTarget.isActor() && pTarget.a.data.health > -1)
            {
                Actor targetActor = pTarget.a;
                Actor attacker = null;
                
                // 如果有有效的伤害来源，并且是Actor，设置攻击者信息
                if (damageSource != null && damageSource.isActor())
                {
                    attacker = damageSource.a;
                    targetActor._last_attack_type = AttackType.Weapon;
                    targetActor.attackedBy = damageSource;
                }
                
                // 使用changeHealth直接造成伤害，类似于AssassinSkill.cs中的处理方式
                int finalDmg = Mathf.Max(1, (int)pDamage);
                targetActor.changeHealth(-finalDmg);
                
                // 如果目标死亡，按照AssassinSkill.cs中的方式处理死亡
                if (!targetActor.hasHealth())
                {
                    targetActor.batch.c_check_deaths.Add(targetActor);
                    targetActor.checkCallbacksOnDeath();
                }
            }

            // 在目标位置生成表示感染的粒子效果
            pTarget.a.spawnParticle(Toolbox.color_infected);
            // 使目标开始震动，模拟反应
            pTarget.a.startShake(0.4f, 0.2f, true, false);
            // 返回true，表示效果已成功应用
            return true;
        }

        public static bool effect_warlock3(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 先检查自身是否处于战斗状态，如果不是则直接返回
            if (!pTarget.isActor() || !pTarget.a.has_attack_target)
            {
                return true;
            }
            
            // 设置伤害百分比系数（应用伤害属性的50%）
            float damagePercentage = 2.4f;
            
            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 2, 8f, false))
            {
                if (tActor.kingdom != pTarget.kingdom)
                {
                    // 存储施加者对象和伤害值
                    if (pTarget.a.stats != null)
                    {
                        // 获取施加者的伤害属性值，并应用百分比系数
                        float baseDamage = pTarget.a.stats["damage"];
                        float damageValue = baseDamage * damagePercentage; // 应用50%的伤害属性
                        // 将施加者对象和计算后的伤害值一起存储在字典中
                        _inflictorInfoMap[tActor] = new Tuple<BaseSimObject, float>(pTarget, damageValue);
                    }
                    else
                    {
                        // 如果不是有效的Actor，但仍然需要记录施加者
                        _inflictorInfoMap[tActor] = new Tuple<BaseSimObject, float>(pTarget, 1f);
                    }
                    
                    tActor.addStatusEffect("warlockstate1", 30f);
                }
            }
            return true;
        }

        public static bool effect_warlock4(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 先检查自身是否处于战斗状态，如果不是则直接返回
            if (!pTarget.isActor() || !pTarget.a.has_attack_target)
            {
                return true;
            }
            
            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 4, 16f, false))
            {
                if (tActor.kingdom != pTarget.kingdom)
                {
                    tActor.addStatusEffect("warlockstate2", 30f);
                }
            }
            return true;
        }

        public static bool effect_warlock5(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 先检查自身是否处于战斗状态，如果不是则直接返回
            if (!pTarget.isActor() || !pTarget.a.has_attack_target)
            {
                return true;
            }
            
            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 8, 32f, false))
            {
                if (tActor.kingdom != pTarget.kingdom)
                {
                    tActor.addStatusEffect("warlockstate3", 30f);
                }
            }
            return true;
        }

        public static bool effect_warlock6(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 先检查自身是否处于战斗状态，如果不是则直接返回
            if (!pTarget.isActor() || !pTarget.a.has_attack_target)
            {
                return true;
            }
            
            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 8, 32f, false))
            {
                if (tActor.kingdom != pTarget.kingdom)
                {
                    tActor.addStatusEffect("warlockstate4", 30f);
                }
            }
            return true;
        }

        public static bool effect_warlock7(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 先检查自身是否处于战斗状态，如果不是则直接返回
            if (!pTarget.isActor() || !pTarget.a.has_attack_target)
            {
                return true;
            }
            
            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 8, 32f, false))
            {
                if (tActor.kingdom != pTarget.kingdom)
                {
                    tActor.addStatusEffect("warlockstate1", 30f);
                    tActor.addStatusEffect("warlockstate2", 30f);
                    tActor.addStatusEffect("warlockstate3", 30f);
                    tActor.addStatusEffect("warlockstate4", 30f);
                    tActor.addStatusEffect("warlockstate5", 30f);
                }
            }
            return true;
        }
    }
}