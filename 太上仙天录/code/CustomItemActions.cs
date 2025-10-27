using ai;
using UnityEngine;
using VideoCopilot.code.utils;

namespace XianTu.code
{
    internal class CustomItemActions
    {
        // 火灵剑特殊攻击效果
        public static bool fireSpiritSwordAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pWorldTile)
        {
            if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
                return false;
            
            if (Randy.randomChance(0.1f))
            {
                // 释放火焰伤害
                ActionLibrary.castFire(pSelf, pTarget, pWorldTile);
                return true;
            }
            
            return false;
        }

        // 水灵剑特殊攻击效果
        public static bool waterSpiritSwordAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pWorldTile)
        {
            if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
                return false;
            
            if (Randy.randomChance(0.12f))
            {
                // 添加减速效果
                ActionLibrary.addSlowEffectOnTarget(pSelf, pTarget, pWorldTile);
                return true;
            }
            
            return false;
        }

        // 紫霄剑特殊攻击效果
        public static bool purpleCloudSwordAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pWorldTile)
        {
            if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
                return false;
            
            if (Randy.randomChance(0.15f))
            {
                // 添加眩晕效果
                ActionLibrary.addStunnedEffectOnTarget(pSelf, pTarget, pWorldTile);
                return true;
            }
            
            return false;
        }

        // 太初剑特殊攻击效果
        public static bool primalSwordAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pWorldTile)
        {
            if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
                return false;
            
            if (Randy.randomChance(0.2f))
            {
                // 释放火焰伤害
                ActionLibrary.castFire(pSelf, pTarget, pWorldTile);
                // 添加眩晕效果
                ActionLibrary.addStunnedEffectOnTarget(pSelf, pTarget, pWorldTile);
                return true;
            }
            
            return false;
        }
    }
}