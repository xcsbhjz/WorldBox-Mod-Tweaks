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
    internal class RangerSkill
    {
        // 存储每个攻击者的最后使用时间，实现冷却效果
        private static Dictionary<long, double> _Ranger3Cooldowns = new Dictionary<long, double>();
        private const double Ranger3_COOLDOWN = 1.0; // 冷却时间（游戏时间）

        public static bool attack_Ranger3(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    long attackerId = attacker.getID();
                    // 检查冷却时间
                    if (_Ranger3Cooldowns.TryGetValue(attackerId, out double lastUsedTime))
                    {
                        if (World.world.getCurWorldTime() - lastUsedTime < Ranger3_COOLDOWN)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _Ranger3Cooldowns[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 3; // 投射物数量
                    float spreadAngle = 3.0f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "MagicArrow",                  // 投射物資產 ID
                        pLaunchPosition: newPoint,      // 發射位置
                        pTargetPosition: spreadTarget,      // 目標位置
                        pTargetZ: 0.0f                      // 目標 Z 軸（通常為 0）
                    );
                    }

                    return true;
                }
            }
            return false;
        }

        private static Dictionary<long, double> _Ranger4Cooldowns = new Dictionary<long, double>();
        private const double Ranger4_COOLDOWN = 1.0; // 冷却时间（游戏时间）

        public static bool attack_Ranger4(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    long attackerId = attacker.getID();
                    // 检查冷却时间
                    if (_Ranger4Cooldowns.TryGetValue(attackerId, out double lastUsedTime))
                    {
                        if (World.world.getCurWorldTime() - lastUsedTime < Ranger4_COOLDOWN)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _Ranger4Cooldowns[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 6; // 投射物数量
                    float spreadAngle = 3.0f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "MagicArrow",                  // 投射物資產 ID
                        pLaunchPosition: newPoint,      // 發射位置
                        pTargetPosition: spreadTarget,      // 目標位置
                        pTargetZ: 0.0f                      // 目標 Z 軸（通常為 0）
                    );
                    }

                    return true;
                }
            }
            return false;
        }

        private static Dictionary<long, double> _Ranger5Cooldowns = new Dictionary<long, double>();
        private const double Ranger5_COOLDOWN = 1.0; // 冷却时间（游戏时间）

        public static bool attack_Ranger5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    long attackerId = attacker.getID();
                    // 检查冷却时间
                    if (_Ranger5Cooldowns.TryGetValue(attackerId, out double lastUsedTime))
                    {
                        if (World.world.getCurWorldTime() - lastUsedTime < Ranger5_COOLDOWN)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _Ranger5Cooldowns[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 12; // 投射物数量
                    float spreadAngle = 1.5f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "MagicArrow",                  // 投射物資產 ID
                        pLaunchPosition: newPoint,      // 發射位置
                        pTargetPosition: spreadTarget,      // 目標位置
                        pTargetZ: 0.0f                      // 目標 Z 軸（通常為 0）
                    );
                    }

                    return true;
                }
            }
            return false;
        }

        private static Dictionary<long, double> _Ranger6Cooldowns = new Dictionary<long, double>();
        private const double Ranger6_COOLDOWN = 1.0; // 冷却时间（游戏时间）

        public static bool attack_Ranger6(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    long attackerId = attacker.getID();
                    // 检查冷却时间
                    if (_Ranger6Cooldowns.TryGetValue(attackerId, out double lastUsedTime))
                    {
                        if (World.world.getCurWorldTime() - lastUsedTime < Ranger6_COOLDOWN)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _Ranger6Cooldowns[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 24; // 投射物数量
                    float spreadAngle = 1.5f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "MagicArrow",                  // 投射物資產 ID
                        pLaunchPosition: newPoint,      // 發射位置
                        pTargetPosition: spreadTarget,      // 目標位置
                        pTargetZ: 0.0f                      // 目標 Z 軸（通常為 0）
                    );
                    }

                    return true;
                }
            }
            return false;
        }

        private static Dictionary<long, double> _Ranger7Cooldowns = new Dictionary<long, double>();
        private const double Ranger7_COOLDOWN = 1.0; // 冷却时间（游戏时间）

        public static bool attack_Ranger7(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    long attackerId = attacker.getID();
                    // 检查冷却时间
                    if (_Ranger7Cooldowns.TryGetValue(attackerId, out double lastUsedTime))
                    {
                        if (World.world.getCurWorldTime() - lastUsedTime < Ranger7_COOLDOWN)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _Ranger7Cooldowns[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 48; // 投射物数量
                    float spreadAngle = 1.5f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "MagicArrow",                  // 投射物資產 ID
                        pLaunchPosition: newPoint,      // 發射位置
                        pTargetPosition: spreadTarget,      // 目標位置
                        pTargetZ: 0.0f                      // 目標 Z 軸（通常為 0）
                    );
                    }

                    return true;
                }
            }
            return false;
        }
    }
}