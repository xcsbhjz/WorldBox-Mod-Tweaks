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
    internal class ValiantGeneralSkill
    {  
        public static bool attack_valiantgeneral3(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                int fixedDamage = 2;

                if (fixedDamage > 0 && targetActor.data.health > 0)
                {
                    targetActor.changeHealth(-fixedDamage);
                    if (!attacker.hasMaxHealth())
                    {
                        attacker.restoreHealth(2);
                    }
                    if (!targetActor.hasHealth())
                    {
                        targetActor.batch.c_check_deaths.Add(targetActor);
                        targetActor.checkCallbacksOnDeath();
                    }
                }
            }
            return false;
        }

        public static bool attack_valiantgeneral4(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                int fixedDamage = 4;

                if (fixedDamage > 0 && targetActor.data.health > 0)
                {
                    targetActor.changeHealth(-fixedDamage);
                    if (!attacker.hasMaxHealth())
                    {
                        attacker.restoreHealth(4);
                    }
                    if (!targetActor.hasHealth())
                    {
                        targetActor.batch.c_check_deaths.Add(targetActor);
                        targetActor.checkCallbacksOnDeath();
                    }
                }
            }
            return false;
        }

        public static bool attack_valiantgeneral5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                int fixedDamage = 8;

                if (fixedDamage > 0 && targetActor.data.health > 0)
                {
                    targetActor.changeHealth(-fixedDamage);
                    if (!attacker.hasMaxHealth())
                    {
                        attacker.restoreHealth(8);
                    }
                    if (!targetActor.hasHealth())
                    {
                        targetActor.batch.c_check_deaths.Add(targetActor);
                        targetActor.checkCallbacksOnDeath();
                    }
                }
            }
            return false;
        }

        public static bool attack_valiantgeneral6(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                int fixedDamage = 16;

                if (fixedDamage > 0 && targetActor.data.health > 0)
                {
                    targetActor.changeHealth(-fixedDamage);
                    if (!attacker.hasMaxHealth())
                    {
                        attacker.restoreHealth(16);
                    }
                    if (!targetActor.hasHealth())
                    {
                        targetActor.batch.c_check_deaths.Add(targetActor);
                        targetActor.checkCallbacksOnDeath();
                    }
                }
            }
            return false;
        }

        public static bool attack_valiantgeneral7(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                int fixedDamage = 32;

                if (fixedDamage > 0 && targetActor.data.health > 0)
                {
                    targetActor.changeHealth(-fixedDamage);
                    if (!attacker.hasMaxHealth())
                    {
                        attacker.restoreHealth(32);
                    }
                    if (!targetActor.hasHealth())
                    {
                        targetActor.batch.c_check_deaths.Add(targetActor);
                        targetActor.checkCallbacksOnDeath();
                    }
                }
            }
            return false;
        }
    }
}