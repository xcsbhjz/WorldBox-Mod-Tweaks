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
    internal class pastorSkill
    {
        public static bool effect_pastor3(BaseSimObject pTarget, WorldTile pTile = null)
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
                    if (!tActor.hasMaxHealth())
                    {
                        tActor.restoreHealth(16);
                        tActor.spawnParticle(Toolbox.color_heal);
                    }
                }
            }
            return true;
        }

        public static bool effect_pastor4(BaseSimObject pTarget, WorldTile pTile = null)
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
                    if (!tActor.hasMaxHealth())
                    {
                        tActor.restoreHealth(32);
                        tActor.spawnParticle(Toolbox.color_heal);
                    }
                }
            }

            return true;
        }

        public static bool effect_pastor5(BaseSimObject pTarget, WorldTile pTile = null)
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
                    if (!tActor.hasMaxHealth())
                    {
                        tActor.restoreHealth(64);
                        tActor.spawnParticle(Toolbox.color_heal);
                    }
                }
            }

            return true;
        }

        public static bool effect_pastor6(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 先检查自身是否处于战斗状态，如果不是则直接返回
            if (!pTarget.isActor() || !pTarget.a.has_attack_target)
            {
                return true;
            }

            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 16, 64f, false))
            {
                if (tActor.kingdom == pTarget.kingdom)
                {
                    if (!tActor.hasMaxHealth())
                    {
                        tActor.restoreHealth(128);
                        tActor.spawnParticle(Toolbox.color_heal);
                    }
                }
            }

            return true;
        }

        public static bool effect_pastor7(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 先检查自身是否处于战斗状态，如果不是则直接返回
            if (!pTarget.isActor() || !pTarget.a.has_attack_target)
            {
                return true;
            }

            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 32, 128f, false))
            {
                if (tActor.kingdom == pTarget.kingdom)
                {
                    if (!tActor.hasMaxHealth())
                    {
                        tActor.restoreHealth(256);
                        tActor.spawnParticle(Toolbox.color_heal);
                    }
                }
            }

            return true;
        }
    }
}