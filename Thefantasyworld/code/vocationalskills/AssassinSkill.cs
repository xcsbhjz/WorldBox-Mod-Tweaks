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
    internal class AssassinSkill
    {
        public static bool attack_Assassin3(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                // 10%概率触发特殊效果
                if (Randy.randomChance(0.1f))
                {
                    float attackDamage = attacker.stats["damage"];

                    int trueDamage = (int)(attackDamage * 96f);
                    if (trueDamage > 0 && targetActor.data.health > 0)
                    {
                        int finalDmg = Mathf.Max(1, trueDamage);
                        targetActor.changeHealth(-finalDmg);
                        if (!targetActor.hasHealth())
                        {
                            targetActor.batch.c_check_deaths.Add(targetActor);
                            targetActor.checkCallbacksOnDeath();
                        }
                    }
                }
            }
            return false;
        }

        public static bool attack_Assassin4(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                // 10%概率触发特殊效果
                if (Randy.randomChance(0.1f))
                {
                    float attackDamage = attacker.stats["damage"];

                    int trueDamage = (int)(attackDamage * 192f);
                    if (trueDamage > 0 && targetActor.data.health > 0)
                    {
                        int finalDmg = Mathf.Max(1, trueDamage);
                        targetActor.changeHealth(-finalDmg);
                        if (!targetActor.hasHealth())
                        {
                            targetActor.batch.c_check_deaths.Add(targetActor);
                            targetActor.checkCallbacksOnDeath();
                        }
                    }
                }
            }
            return false;
        }

        public static bool attack_Assassin5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                // 10%概率触发特殊效果
                if (Randy.randomChance(0.1f))
                {
                    float attackDamage = attacker.stats["damage"];

                    int trueDamage = (int)(attackDamage * 384f);
                    if (trueDamage > 0 && targetActor.data.health > 0)
                    {
                        int finalDmg = Mathf.Max(1, trueDamage);
                        targetActor.changeHealth(-finalDmg);
                        if (!targetActor.hasHealth())
                        {
                            targetActor.batch.c_check_deaths.Add(targetActor);
                            targetActor.checkCallbacksOnDeath();
                        }
                    }
                }
            }
            return false;
        }

        public static bool attack_Assassin6(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                // 10%概率触发特殊效果
                if (Randy.randomChance(0.1f))
                {
                    float attackDamage = attacker.stats["damage"];

                    int trueDamage = (int)(attackDamage * 768f);
                    if (trueDamage > 0 && targetActor.data.health > 0)
                    {
                        int finalDmg = Mathf.Max(1, trueDamage);
                        targetActor.changeHealth(-finalDmg);
                        if (!targetActor.hasHealth())
                        {
                            targetActor.batch.c_check_deaths.Add(targetActor);
                            targetActor.checkCallbacksOnDeath();
                        }
                    }
                }
            }
            return false;
        }

        public static bool attack_Assassin7(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                // 10%概率触发特殊效果
                if (Randy.randomChance(0.1f))
                {
                    float attackDamage = attacker.stats["damage"];

                    int trueDamage = (int)(attackDamage * 1536f);
                    if (trueDamage > 0 && targetActor.data.health > 0)
                    {
                        int finalDmg = Mathf.Max(1, trueDamage);
                        targetActor.changeHealth(-finalDmg);
                        if (!targetActor.hasHealth())
                        {
                            targetActor.batch.c_check_deaths.Add(targetActor);
                            targetActor.checkCallbacksOnDeath();
                        }
                    }
                }
            }
            return false;
        }

        public static bool Assassin7_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 8, 32f, false))
            {
                if (tActor.kingdom == pTarget.kingdom)
                {
                    tActor.addStatusEffect("Assassinstate", 30f);
                }
            }
            return true;
        }
    }
}