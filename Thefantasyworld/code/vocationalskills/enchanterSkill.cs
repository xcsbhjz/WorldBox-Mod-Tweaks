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
    internal class enchanterSkill
    {
        private static Dictionary<long, double> _enchanter3Cooldowns = new Dictionary<long, double>();
        private const double ENCHANTER3_COOLDOWN = 0.2; // 冷却时间（游戏时间）

        public static bool attack_enchanter3(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    long attackerId = attacker.getID();
                    // 检查冷却时间
                    if (_enchanter3Cooldowns.TryGetValue(attackerId, out double lastUsedTime))
                    {
                        if (World.world.getCurWorldTime() - lastUsedTime < ENCHANTER3_COOLDOWN)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _enchanter3Cooldowns[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 1; // 投射物数量
                    float spreadAngle = 1f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "freeze_orb",                  // 投射物資產 ID
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

        public static bool attack_enchanter4(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pSelf == null || pTarget == null)
            {
                return false;
            }

            // 2. 隨機分配攻擊
            // 隨機選擇要觸發的攻擊效果
            float randomValue = UnityEngine.Random.value;
            if (randomValue <= 0.50f)
            {
                // 50% 機率觸發 attack1_enchanter4
                return attack1_enchanter4(pSelf, pTarget, pTile);
            }
            else
            {
                // 另外 50% 機率觸發 attack2_enchanter4
                return attack2_enchanter4(pSelf, pTarget, pTile);
            }
        }

        private static Dictionary<long, double> _enchanter4Cooldowns1 = new Dictionary<long, double>();
        private const double ENCHANTER4_COOLDOWN1 = 0.2; // 冷却时间（游戏时间）

        public static bool attack1_enchanter4(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    long attackerId = attacker.getID();
                    // 检查冷却时间
                    if (_enchanter4Cooldowns1.TryGetValue(attackerId, out double lastUsedTime))
                    {
                        if (World.world.getCurWorldTime() - lastUsedTime < ENCHANTER4_COOLDOWN1)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _enchanter4Cooldowns1[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 2; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "freeze_orb",                  // 投射物資產 ID
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

        private static Dictionary<long, double> _enchanter4Cooldowns2 = new Dictionary<long, double>();
        private const double ENCHANTER4_COOLDOWN2 = 0.2; // 冷却时间（游戏时间）

        public static bool attack2_enchanter4(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    long attackerId = attacker.getID();
                    // 检查冷却时间
                    if (_enchanter4Cooldowns2.TryGetValue(attackerId, out double lastUsedTime))
                    {
                        if (World.world.getCurWorldTime() - lastUsedTime < ENCHANTER4_COOLDOWN2)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _enchanter4Cooldowns2[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 2; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "fx_wind_trail_t",                  // 投射物資產 ID
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

        public static bool attack_enchanter5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pSelf == null || pTarget == null)
            {
                return false;
            }

            float randomValue = UnityEngine.Random.value;
            if (randomValue <= 0.33f)
            {
                // 約33% 機率觸發 attack1_enchanter5
                return attack1_enchanter5(pSelf, pTarget, pTile);
            }
            else if (randomValue <= 0.66f)
            {
                // 約33% 機率觸發 attack2_enchanter5
                return attack2_enchanter5(pSelf, pTarget, pTile);
            }
            else
            {
                // 約34% 機率觸發 attack3_enchanter5
                return attack3_enchanter5(pSelf, pTarget, pTile);
            }
        }

        private static Dictionary<long, double> _enchanter5Cooldowns1 = new Dictionary<long, double>();
        private const double ENCHANTER5_COOLDOWN1 = 0.2; // 冷却时间（游戏时间）

        public static bool attack1_enchanter5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    long attackerId = attacker.getID();
                    // 检查冷却时间
                    if (_enchanter5Cooldowns1.TryGetValue(attackerId, out double lastUsedTime))
                    {
                        if (World.world.getCurWorldTime() - lastUsedTime < ENCHANTER5_COOLDOWN1)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _enchanter5Cooldowns1[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 4; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "freeze_orb",                  // 投射物資產 ID
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

        private static Dictionary<long, double> _enchanter5Cooldowns2 = new Dictionary<long, double>();
        private const double ENCHANTER5_COOLDOWN2 = 0.2; // 冷却时间（游戏时间）

        public static bool attack2_enchanter5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    long attackerId = attacker.getID();
                    // 检查冷却时间
                    if (_enchanter5Cooldowns2.TryGetValue(attackerId, out double lastUsedTime))
                    {
                        if (World.world.getCurWorldTime() - lastUsedTime < ENCHANTER5_COOLDOWN2)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _enchanter5Cooldowns2[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 4; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "fx_wind_trail_t",                  // 投射物資產 ID
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

        private static Dictionary<long, double> _enchanter5Cooldowns3 = new Dictionary<long, double>();
        private const double ENCHANTER5_COOLDOWN3 = 0.2; // 冷却时间（游戏时间）

        public static bool attack3_enchanter5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    long attackerId = attacker.getID();
                    // 检查冷却时间
                    if (_enchanter5Cooldowns3.TryGetValue(attackerId, out double lastUsedTime))
                    {
                        if (World.world.getCurWorldTime() - lastUsedTime < ENCHANTER5_COOLDOWN3)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _enchanter5Cooldowns3[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 4; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "fireball",                  // 投射物資產 ID
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

         public static bool attack_enchanter6(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pSelf == null || pTarget == null)
            {
                return false;
            }

            // 使用随机索引从方法列表中抽取执行
            int randomIndex = UnityEngine.Random.Range(0, 4);
            
            switch(randomIndex)
            {
                case 0:
                    return attack1_enchanter6(pSelf, pTarget, pTile);
                case 1:
                    return attack2_enchanter6(pSelf, pTarget, pTile);
                case 2:
                    return attack3_enchanter6(pSelf, pTarget, pTile);
                case 3:
                    return CastMeteoriteBasedOnDamage1(pSelf, pTarget, pTile);
                default:
                    return false;
            }
        }

        private static Dictionary<long, double> _enchanter6Cooldowns1 = new Dictionary<long, double>();
        private const double ENCHANTER6_COOLDOWN1 = 0.2; // 冷却时间（游戏时间）

        public static bool attack1_enchanter6(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    long attackerId = attacker.getID();
                    // 检查冷却时间
                    if (_enchanter6Cooldowns1.TryGetValue(attackerId, out double lastUsedTime))
                    {
                        if (World.world.getCurWorldTime() - lastUsedTime < ENCHANTER6_COOLDOWN1)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _enchanter6Cooldowns1[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 8; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "freeze_orb",                  // 投射物資產 ID
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

        private static Dictionary<long, double> _enchanter6Cooldowns2 = new Dictionary<long, double>();
        private const double ENCHANTER6_COOLDOWN2 = 0.2; // 冷却时间（游戏时间）

        public static bool attack2_enchanter6(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
            long attackerId = attacker.getID();
            // 检查冷却时间
            if (_enchanter6Cooldowns2.TryGetValue(attackerId, out double lastUsedTime))
            {
                if (World.world.getCurWorldTime() - lastUsedTime < ENCHANTER6_COOLDOWN2)
                {
                    return false; // 冷却中，无法使用
                }
            }
            // 更新最后使用时间
            _enchanter6Cooldowns2[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 8; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "fx_wind_trail_t",                  // 投射物資產 ID
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

        private static Dictionary<long, double> _enchanter6Cooldowns3 = new Dictionary<long, double>();
        private const double ENCHANTER6_COOLDOWN3 = 0.2; // 冷却时间（游戏时间）

        public static bool attack3_enchanter6(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
            long attackerId = attacker.getID();
            // 检查冷却时间
            if (_enchanter6Cooldowns3.TryGetValue(attackerId, out double lastUsedTime))
            {
                if (World.world.getCurWorldTime() - lastUsedTime < ENCHANTER6_COOLDOWN3)
                {
                    return false; // 冷却中，无法使用
                }
            }
            // 更新最后使用时间
            _enchanter6Cooldowns3[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 8; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "fireball",                  // 投射物資產 ID
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

        public static bool attack_enchanter7(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pSelf == null || pTarget == null)
            {
                return false;
            }

            // 使用随机索引从方法列表中抽取执行
            int randomIndex = UnityEngine.Random.Range(0, 5);
            
            switch(randomIndex)
            {
                case 0:
                    return attack1_enchanter7(pSelf, pTarget, pTile);
                case 1:
                    return attack2_enchanter7(pSelf, pTarget, pTile);
                case 2:
                    return attack3_enchanter7(pSelf, pTarget, pTile);
                case 3:
                    return CastMeteoriteBasedOnDamage(pSelf, pTarget, pTile);
                case 4:
                    return CastLightningBasedOnDamage(pSelf, pTarget, pTile);
                default:
                    return false;
            }
        }

        private static Dictionary<long, double> _enchanter7Cooldowns1 = new Dictionary<long, double>();
        private const double ENCHANTER7_COOLDOWN1 = 0.2; // 冷却时间（游戏时间）

        public static bool attack1_enchanter7(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
            long attackerId = attacker.getID();
            // 检查冷却时间
            if (_enchanter7Cooldowns1.TryGetValue(attackerId, out double lastUsedTime))
            {
                if (World.world.getCurWorldTime() - lastUsedTime < ENCHANTER7_COOLDOWN1)
                {
                    return false; // 冷却中，无法使用
                }
            }
            // 更新最后使用时间
            _enchanter7Cooldowns1[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 16; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "freeze_orb",                  // 投射物資產 ID
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

        private static Dictionary<long, double> _enchanter7Cooldowns2 = new Dictionary<long, double>();
        private const double ENCHANTER7_COOLDOWN2 = 0.2; // 冷却时间（游戏时间）

        public static bool attack2_enchanter7(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
            long attackerId = attacker.getID();
            // 检查冷却时间
            if (_enchanter7Cooldowns2.TryGetValue(attackerId, out double lastUsedTime))
            {
                if (World.world.getCurWorldTime() - lastUsedTime < ENCHANTER7_COOLDOWN2)
                {
                    return false; // 冷却中，无法使用
                }
            }
            // 更新最后使用时间
            _enchanter7Cooldowns2[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 16; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "fx_wind_trail_t",                  // 投射物資產 ID
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

        private static Dictionary<long, double> _enchanter7Cooldowns3 = new Dictionary<long, double>();
        private const double ENCHANTER7_COOLDOWN3 = 0.2; // 冷却时间（游戏时间）

        public static bool attack3_enchanter7(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
            long attackerId = attacker.getID();
            // 检查冷却时间
            if (_enchanter7Cooldowns3.TryGetValue(attackerId, out double lastUsedTime))
            {
                if (World.world.getCurWorldTime() - lastUsedTime < ENCHANTER7_COOLDOWN3)
                {
                    return false; // 冷却中，无法使用
                }
            }
            // 更新最后使用时间
            _enchanter7Cooldowns3[attackerId] = World.world.getCurWorldTime();
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 16; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "fireball",                  // 投射物資產 ID
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

        // 闪电冷却时间（游戏时间）
        private const double Lightning_CooldownSeconds = 0.2;
        private static readonly Dictionary<long, double> _lightningLastTime = new Dictionary<long, double>();

        // 陨石冷却时间（游戏时间）
        private const double Meteorite_CooldownSeconds = 0.2;
        private static readonly Dictionary<long, double> _meteoriteLastTime = new Dictionary<long, double>();


        /// <summary>
        /// 施放基于单位自身伤害属性的中型闪电，直接使用changeHealth扣血
        /// </summary>
        /// <param name="pCaster">施放闪电的单位</param>
        /// <param name="pTarget">目标</param>
        /// <param name="pTile">目标位置</param>
        /// <returns>是否成功施放</returns>
        public static bool CastLightningBasedOnDamage(BaseSimObject pCaster, BaseSimObject pTarget, WorldTile pTile)
        {
            // 检查参数是否有效且pCaster必须是Actor类型
            if (pCaster == null || !pCaster.isActor() || pTile == null)
                return false;
                
            // 将BaseSimObject转换为Actor
            Actor casterActor = pCaster.a;
            long casterId = casterActor.getID();

            double now = World.world.getCurWorldTime();
            // 检查冷却
            if (_lightningLastTime.TryGetValue(casterId, out var lastCastTime) && (now - lastCastTime) < Lightning_CooldownSeconds)
                return false;

            // 生成中型闪电效果 - 增大pScale值以扩大视觉效果
            float pScale = 2f; // 从2f增加到4f，这样视觉效果半径会从30增加到60
            BaseEffect lightningEffect = EffectsLibrary.spawnAtTile("fx_lightning_medium", pTile, pScale);
            if (lightningEffect == null)
                return false;

            // 设置闪电方向随机翻转
            lightningEffect.sprite_renderer.flipX = Randy.randomBool();

            // 计算闪电半径 - 直接指定想要的半径值
            int desiredRadius = 60; // 我们想要的实际范围半径
            int radiusSquared = desiredRadius * desiredRadius;

            // 获取攻击者的伤害属性
            float unitDamage = GetActorDamage(casterActor);

            // 在原有伤害计算基础上再乘以10倍
            float finalDamage = unitDamage * 1024f;

            // 遍历范围内的所有单位，直接使用changeHealth扣血
            List<Actor> allUnits = World.world.units.getSimpleList();
            for (int i = 0; i < allUnits.Count; i++)
            {
                Actor targetActor = allUnits[i];
                // 检查单位是否在闪电范围内
                if (Toolbox.SquaredDistVec2(targetActor.current_tile.pos, pTile.pos) <= radiusSquared)
                {
                    // 避免对施法者自身造成伤害
                        if (targetActor == casterActor)
                            continue;

                    // 应用与DamageandDamageMitigation.cs中相同的伤害计算逻辑，使用10倍伤害
                        ApplyLightningDamage(casterActor, targetActor, finalDamage);
                }
            }

            // 仍然应用闪电对世界的视觉和环境效果
            TerraformOptions lightningOptions = AssetManager.terraform.get("lightning_normal");
            lightningOptions.damage = 0; // 不通过原版系统造成伤害
            lightningOptions.set_fire = true; // 确保设置火焰效果

            // 使用我们自己的方式应用大范围效果，避免Brush类的限制
            ApplyLargeAreaEffect(pTile, desiredRadius, lightningOptions, pCaster);

            // 检查龙卷风击中效果
            MapAction.checkTornadoHit(pTile.pos, desiredRadius);

            // 更新冷却时间
            _lightningLastTime[casterId] = now;

            return true;
        }

        // 在类的顶部添加静态缓存字典
        private static Dictionary<int, List<Vector2Int>> _cachedRadiusPatterns = new Dictionary<int, List<Vector2Int>>();

        /// <summary>
        /// 自定义大范围效果应用方法，避免Brush类对半径的限制
        /// </summary>
        private static void ApplyLargeAreaEffect(WorldTile pCenterTile, int pRadius, TerraformOptions pOptions, BaseSimObject pByWho)
        {
            if (pOptions.shake)
            {
                World.world.startShake(pOptions.shake_duration, pOptions.shake_interval, pOptions.shake_intensity, false, true);
            }

            if (pOptions.apply_force)
            {
                World.world.applyForceOnTile(pCenterTile, pRadius, pOptions.force_power, true, pOptions.damage, pOptions.ignore_kingdoms, pByWho, pOptions, false);
            }

            // 获取或创建缓存的半径模式
            List<Vector2Int> affectedTileOffsets;
            if (!_cachedRadiusPatterns.TryGetValue(pRadius, out affectedTileOffsets))
            {
                affectedTileOffsets = new List<Vector2Int>();
                int radiusSquared = pRadius * pRadius;

                // 一次性计算并缓存所有受影响的瓦片相对偏移量
                for (int x = -pRadius; x <= pRadius; x++)
                {
                    for (int y = -pRadius; y <= pRadius; y++)
                    {
                        if (x * x + y * y <= radiusSquared)
                        {
                            affectedTileOffsets.Add(new Vector2Int(x, y));
                        }
                    }
                }
                _cachedRadiusPatterns[pRadius] = affectedTileOffsets;
            }

            // 预先计算一些常用值，避免在循环内重复计算
            Vector2Int centerPos = pCenterTile.pos;
            bool removeTornado = pOptions.remove_tornado;
            bool addBurned = pOptions.add_burned;
            bool setFire = pOptions.set_fire;
            bool lightningEffect = pOptions.lightning_effect;
            int addHeat = pOptions.add_heat;

            // 使用缓存的瓦片位置
            foreach (Vector2Int offset in affectedTileOffsets)
            {
                int x = centerPos.x + offset.x;
                int y = centerPos.y + offset.y;

                // 快速检查边界，提前跳过超出地图范围的瓦片
                if (x < 0 || x >= MapBox.width || y < 0 || y >= MapBox.height)
                    continue;

                // 获取瓦片并检查有效性
                WorldTile tTile = World.world.GetTileSimple(x, y);
                if (tTile == null)
                    continue;

                // 应用各种效果
                if (removeTornado)
                {
                    MapAction.tryRemoveTornadoFromTile(tTile);
                }

                if (addBurned && !tTile.Type.liquid)
                {
                    tTile.setBurned(-1);
                }

                // 添加火焰效果
                if (setFire)
                {
                    tTile.startFire(true);
                }

                if (lightningEffect)
                {
                    // 应用闪电特效到瓦片
                    if (tTile.Type.lava && tTile.heat > 20)
                    {
                        MapAction.decreaseTile(tTile, true, "flash");
                        if (Randy.randomChance(0.9f))
                        {
                            int tExtra = tTile.heat / 10;
                            World.world.drop_manager.spawnParabolicDrop(tTile, "lava", 0f, 0.15f, 33f + (float)(tExtra * 2), 1f, 40f + (float)tExtra, -1f);
                        }
                    }

                    if (tTile.Type.layer_type == TileLayerType.Ocean)
                    {
                        MapAction.removeLiquid(tTile);
                        if (Randy.randomChance(0.8f))
                        {
                            World.world.drop_manager.spawnParabolicDrop(tTile, "rain", 0f, 1f, 66f, 1f, 45f, -1f);
                        }
                    }

                    // 建筑物效果 - 直接摧毁建筑
                    if (tTile.hasBuilding())
                    {
                        // 生成视觉效果
                        if (tTile.building.asset.spawn_drops)
                        {
                            tTile.building.spawnBurstSpecial(10);
                        }

                        // 直接摧毁建筑，使用游戏内置方法处理完整的摧毁流程
                        tTile.building.startDestroyBuilding();
                    }
                }

                // 应用热量
                if (addHeat != 0)
                {
                    World.world.heat.addTile(tTile, addHeat);
                }
            }

            // 重置重绘计时器
            World.world.resetRedrawTimer();
        }

        public static bool CastMeteoriteBasedOnDamage1(BaseSimObject pCaster, BaseSimObject pTarget, WorldTile pTile)
        {
            // 检查参数是否有效且pCaster必须是Actor类型
            if (pCaster == null || !pCaster.isActor() || pTile == null)
                return false;
                
            // 将BaseSimObject转换为Actor
            Actor casterActor = pCaster.a;
            long casterId = casterActor.getID();

            double now = World.world.getCurWorldTime();
            // 检查冷却
            if (_meteoriteLastTime.TryGetValue(casterId, out var lastCastTime) && (now - lastCastTime) < Meteorite_CooldownSeconds)
                return false;

            // 生成陨石效果 - 使用自定义的方向控制
            WorldTile targetTile = pTile;

            {
                // 创建自定义陨石对象而不是使用静态方法
                BaseEffect meteoriteEffect = EffectsLibrary.spawn("fx_meteorite", targetTile, "meteorite", null, 0f, -1f, -1f, casterActor);
                if (meteoriteEffect is Meteorite meteorite)
                {
                    // 获取施法者位置和目标位置
                    Vector2 casterPos = casterActor.current_tile.pos;
                    Vector2 targetPos = targetTile.pos;

                    // 根据施法者相对于目标的位置设置陨石初始位置
                    if (casterPos.x > targetPos.x) // 施法者在目标右侧
                    {
                        // 从右侧向左飞行：初始位置在目标右侧200f，上方200f
                        meteorite.current_position.x = targetPos.x + 200f;
                    }
                    else // 施法者在左侧或同一位置
                    {
                        // 从左侧向右飞行：初始位置在目标左侧200f，上方200f
                        meteorite.current_position.x = targetPos.x - 200f;
                    }

                    // 设置Y轴位置（始终在目标上方200f）
                    meteorite.current_position.y = targetPos.y + 200f;

                    // 直接设置mainSprite的localPosition，确保Z轴位置固定为较低值
                    // 这样在地图缩小时，陨石也能保持在合适的渲染层级
                    Vector3 mainSpritePos = new Vector3(
                        meteorite.current_position.x,
                        meteorite.current_position.y,
                        -5f  // 固定Z轴位置为-5，确保陨石显示在大多数物体前面
                    );
                    meteorite.mainSprite.transform.localPosition = mainSpritePos;

                    // 更新阴影位置
                    meteorite.shadowSprite.transform.localPosition = new Vector2(
                        meteorite.current_position.x,
                        meteorite.shadowSprite.transform.localPosition.y
                    );
                }
            }

            // 计算陨石半径和伤害
            int desiredRadius = 15; // 陨石的影响范围
            int radiusSquared = desiredRadius * desiredRadius;

            // 获取攻击者的伤害属性
            float unitDamage = GetActorDamage(casterActor);

            // 在原有伤害计算基础上再乘以10倍
            float finalDamage = unitDamage * 512f;

            // 遍历范围内的所有单位，直接使用changeHealth扣血
            List<Actor> allUnits = World.world.units.getSimpleList();
            for (int i = 0; i < allUnits.Count; i++)
            {
                Actor targetActor = allUnits[i];
                // 检查单位是否在陨石范围内
                if (Toolbox.SquaredDistVec2(targetActor.current_tile.pos, targetTile.pos) <= radiusSquared)
                {
                    // 避免对施法者自身造成伤害
                        if (targetActor == casterActor)
                            continue;

                    // 应用与闪电相同的伤害计算逻辑，使用10倍伤害
                        ApplyLightningDamage(casterActor, targetActor, finalDamage);
                }
            }

            // 更新冷却时间
            _meteoriteLastTime[casterId] = now;

            return true;
        }

        /// <summary>
        /// 施放基于单位自身伤害属性的陨石，直接使用changeHealth扣血
        /// </summary>
        /// <param name="pCaster">施放陨石的单位</param>
        /// <param name="pTarget">目标</param>
        /// <param name="pTile">目标位置</param>
        /// <returns>是否成功施放</returns>
        public static bool CastMeteoriteBasedOnDamage(BaseSimObject pCaster, BaseSimObject pTarget, WorldTile pTile)
        {
            // 检查参数是否有效且pCaster必须是Actor类型
            if (pCaster == null || !pCaster.isActor() || pTile == null)
                return false;
                
            // 将BaseSimObject转换为Actor
            Actor casterActor = pCaster.a;
            long casterId = casterActor.getID();

            double now = World.world.getCurWorldTime();
            // 检查冷却
            if (_meteoriteLastTime.TryGetValue(casterId, out var lastCastTime) && (now - lastCastTime) < Meteorite_CooldownSeconds)
                return false;

            // 生成陨石效果 - 使用自定义的方向控制
            WorldTile targetTile = pTile;

            {
                // 创建自定义陨石对象而不是使用静态方法
                BaseEffect meteoriteEffect = EffectsLibrary.spawn("fx_meteorite", targetTile, "meteorite", null, 0f, -1f, -1f, casterActor);
                if (meteoriteEffect is Meteorite meteorite)
                {
                    // 获取施法者位置和目标位置
                    Vector2 casterPos = casterActor.current_tile.pos;
                    Vector2 targetPos = targetTile.pos;

                    // 根据施法者相对于目标的位置设置陨石初始位置
                    if (casterPos.x > targetPos.x) // 施法者在目标右侧
                    {
                        // 从右侧向左飞行：初始位置在目标右侧200f，上方200f
                        meteorite.current_position.x = targetPos.x + 200f;
                    }
                    else // 施法者在左侧或同一位置
                    {
                        // 从左侧向右飞行：初始位置在目标左侧200f，上方200f
                        meteorite.current_position.x = targetPos.x - 200f;
                    }

                    // 设置Y轴位置（始终在目标上方200f）
                    meteorite.current_position.y = targetPos.y + 200f;

                    // 直接设置mainSprite的localPosition，确保Z轴位置固定为较低值
                    // 这样在地图缩小时，陨石也能保持在合适的渲染层级
                    Vector3 mainSpritePos = new Vector3(
                        meteorite.current_position.x,
                        meteorite.current_position.y,
                        -5f  // 固定Z轴位置为-5，确保陨石显示在大多数物体前面
                    );
                    meteorite.mainSprite.transform.localPosition = mainSpritePos;

                    // 更新阴影位置
                    meteorite.shadowSprite.transform.localPosition = new Vector2(
                        meteorite.current_position.x,
                        meteorite.shadowSprite.transform.localPosition.y
                    );
                }
            }

            // 计算陨石半径和伤害
            int desiredRadius = 15; // 陨石的影响范围
            int radiusSquared = desiredRadius * desiredRadius;

            // 获取攻击者的伤害属性
            float unitDamage = GetActorDamage(casterActor);

            // 在原有伤害计算基础上再乘以10倍
            float finalDamage = unitDamage * 1024f;

            // 遍历范围内的所有单位，直接使用changeHealth扣血
            List<Actor> allUnits = World.world.units.getSimpleList();
            for (int i = 0; i < allUnits.Count; i++)
            {
                Actor targetActor = allUnits[i];
                // 检查单位是否在陨石范围内
                if (Toolbox.SquaredDistVec2(targetActor.current_tile.pos, targetTile.pos) <= radiusSquared)
                {
                    // 避免对施法者自身造成伤害
                        if (targetActor == casterActor)
                            continue;

                    // 应用与闪电相同的伤害计算逻辑，使用10倍伤害
                        ApplyLightningDamage(casterActor, targetActor, finalDamage);
                }
            }

            // 更新冷却时间
            _meteoriteLastTime[casterId] = now;

            return true;
        }

        /// <summary>
        /// 应用闪电伤害，使用传入的pBaseDamage作为实际伤害值
        /// </summary>
        private static void ApplyLightningDamage(Actor pAttacker, Actor pTarget, float pBaseDamage)
        {
            if (pAttacker == null || pTarget == null || !pTarget.isAlive())
                return;

            // 设置最后攻击类型与攻击者
            pTarget._last_attack_type = AttackType.Other;
            pTarget.attackedBy = pAttacker;

            // 使用传入的pBaseDamage作为伤害值
            int magicDamage = (int)pBaseDamage;
            
            // 确保至少造成1点伤害
            magicDamage = Mathf.Max(1, magicDamage);

            // 直接调用changeHealth扣血
            // 应用伤害
            pTarget.changeHealth(-magicDamage);
            // 检查死亡
            if (!pTarget.hasHealth())
            {
                pTarget.batch.c_check_deaths.Add(pTarget);
                pTarget.checkCallbacksOnDeath();
            }
        }


        /// <summary>
        /// 获取单位的伤害属性
        /// </summary>
        private static float GetActorDamage(Actor pActor)
        {
            if (pActor == null || pActor.stats == null)
                return 1f;

            // 仅返回damage属性值
            return Mathf.Max(1, pActor.stats["damage"]); // 确保至少有1点伤害
        }
    }
}