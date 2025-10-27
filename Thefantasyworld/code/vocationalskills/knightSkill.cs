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
    internal class knightSkill
    {
        public static bool effect_knight3(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 先检查自身是否处于战斗状态，如果不是则直接返回
            if (!pTarget.isActor() || !pTarget.a.has_attack_target)
            {
                return true;
            }

            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 2, 8f, false))
            {
                if (tActor.kingdom == pTarget.kingdom)
                {
                    tActor.addStatusEffect("shield", 30f);
                }
            }
            return true;
        }

        public static bool effect_knight4(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 先检查自身是否处于战斗状态，如果不是则直接返回
            if (!pTarget.isActor() || !pTarget.a.has_attack_target)
            {
                return true;
            }

            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 4, 16f, false))
            {
                if (tActor.kingdom == pTarget.kingdom)
                {
                    tActor.addStatusEffect("shield", 30f);
                }
            }
            return true;
        }

        public static bool effect_knight5(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 先检查自身是否处于战斗状态，如果不是则直接返回
            if (!pTarget.isActor() || !pTarget.a.has_attack_target)
            {
                return true;
            }

            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 8, 32f, false))
            {
                if (tActor.kingdom == pTarget.kingdom)
                {
                    tActor.addStatusEffect("shield", 30f);
                }
            }
            return true;
        }

        public static bool effect_knight6(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 先检查自身是否处于战斗状态，如果不是则直接返回
            if (!pTarget.isActor() || !pTarget.a.has_attack_target)
            {
                return true;
            }

            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 8, 32f, false))
            {
                if (tActor.kingdom == pTarget.kingdom)
                {
                    tActor.addStatusEffect("shield", 30f);
                }
            }
            return true;
        }

        public static bool effect_knight7(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 先检查自身是否处于战斗状态，如果不是则直接返回
            if (!pTarget.isActor() || !pTarget.a.has_attack_target)
            {
                return true;
            }

            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 8, 32f, false))
            {
                if (tActor.kingdom == pTarget.kingdom)
                {
                    tActor.addStatusEffect("shield", 30f);
                }
            }
            return true;
        }

        public static bool knight5_Attack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor knight = pSelf.a;      // 骑士（特质拥有者）
                Actor attacker = pTarget.a;  // 攻击者

                // 获取骑士的伤害值
                float knightDamage = knight.stats["damage"];

                int reflectDamage = (int)(knightDamage * 4.8f);
                if (reflectDamage > 0 && attacker.data.health > 0)
                {
                    int finalDmg = Mathf.Max(1, reflectDamage);
                    attacker.changeHealth(-finalDmg);
                    if (!attacker.hasHealth())
                    {
                        attacker.batch.c_check_deaths.Add(attacker);
                        attacker.checkCallbacksOnDeath();
                    }
                }
            }
            return false;
        }

        public static bool knight6_Attack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor knight = pSelf.a;      // 骑士（特质拥有者）
                Actor attacker = pTarget.a;  // 攻击者

                // 获取骑士的伤害值
                float knightDamage = knight.stats["damage"];

                int reflectDamage = (int)(knightDamage * 9.6f);
                if (reflectDamage > 0 && attacker.data.health > 0)
                {
                    int finalDmg = Mathf.Max(1, reflectDamage);
                    attacker.changeHealth(-finalDmg);
                    if (!attacker.hasHealth())
                    {
                        attacker.batch.c_check_deaths.Add(attacker);
                        attacker.checkCallbacksOnDeath();
                    }
                }
            }
            return false;
        }

        public static bool knight7_Attack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor knight = pSelf.a;      // 骑士（特质拥有者）
                Actor attacker = pTarget.a;  // 攻击者

                // 获取骑士的伤害值
                float knightDamage = knight.stats["damage"];

                int reflectDamage = (int)(knightDamage * 19.2f);
                if (reflectDamage > 0 && attacker.data.health > 0)
                {
                    int finalDmg = Mathf.Max(1, reflectDamage);
                    attacker.changeHealth(-finalDmg);
                    if (!attacker.hasHealth())
                    {
                        attacker.batch.c_check_deaths.Add(attacker);
                        attacker.checkCallbacksOnDeath();
                    }
                }
            }
            return false;
        }

        public static bool Trial_knight7(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor knight = pTarget.a;      // 骑士（特质拥有者）
            float knightDamage = knight.stats["damage"];

            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 16, 64f, false))
            {
                if (tActor.kingdom != pTarget.kingdom && tActor.data.health > 0)
                {
                    // 基于伤害属性计算法伤
                    int magicDamage = (int)(knightDamage * 40f);
                    int finalDmg = Mathf.Max(1, magicDamage);

                    // 设置攻击者为骑士（特质拥有者）
                    tActor._last_attack_type = AttackType.Weapon;
                    tActor.attackedBy = pTarget;

                    TrialAnimation(tActor);

                    // 使用changeHealth直接造成伤害
                    tActor.changeHealth(-finalDmg);

                    // 检查目标是否死亡
                    if (!tActor.hasHealth())
                    {
                        tActor.batch.c_check_deaths.Add(tActor);
                        tActor.checkCallbacksOnDeath();
                    }
                }
            }
            return true;
        }

        private static void TrialAnimation(Actor targetActor)
        {
            // 获取目标位置
            Vector2 targetPosition = targetActor.current_position;

            // 设置动画缩放
            float animationScale = targetActor.actor_scale * 1f;

            // 使用正确的资源路径格式
            string animationPath = "TrialAnimation";

            EffectsLibrary.spawnSlash(
                targetPosition,
                animationPath,
                animationScale
            );
        }
    }
}