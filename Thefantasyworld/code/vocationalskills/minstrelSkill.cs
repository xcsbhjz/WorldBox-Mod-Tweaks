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
using System.Runtime.CompilerServices;
using System.Reflection;

namespace PeerlessThedayofGodswrath.code
{
    internal class minstrelSkill
    {
        public static bool effect_minstrel3(BaseSimObject pTarget, WorldTile pTile = null)
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
                    tActor.addStatusEffect("minstrelstate1", 30f);
                }
            }
            return true;
        }

        public static bool effect_minstrel4(BaseSimObject pTarget, WorldTile pTile = null)
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
                    tActor.addStatusEffect("minstrelstate2", 30f);
                }
            }
            return true;
        }

        public static bool effect_minstrel5(BaseSimObject pTarget, WorldTile pTile = null)
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
                    tActor.addStatusEffect("minstrelstate3", 30f);
                }
            }
            return true;
        }

        public static bool effect_minstrel6(BaseSimObject pTarget, WorldTile pTile = null)
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
                    tActor.addStatusEffect("minstrelstate4", 30f);
                }
            }
            return true;
        }

        public static bool effect_minstrel7(BaseSimObject pTarget, WorldTile pTile = null)
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
                    tActor.addStatusEffect("minstrelstate5", 30f);
                }
            }
            return true;
        }

        // 陨石冷却时间（秒）
        private const float Meteorite_CooldownSeconds = 1f;
        private static readonly Dictionary<Actor, float> _meteoriteLastTime = new Dictionary<Actor, float>();

        private static Dictionary<int, List<Vector2Int>> _cachedRadiusPatterns = new Dictionary<int, List<Vector2Int>>();

        public static bool attack_minstrel7(BaseSimObject pCaster, BaseSimObject pTarget, WorldTile pTile)
        {
            // 检查参数是否有效且 pCaster 必须是 Actor 类型
            if (pCaster == null || !pCaster.isActor() || pTile == null)
                return false;

            // 转换为 Actor
            Actor casterActor = pCaster.a;

            float now = Time.time;
            // 冷却检查
            if (_meteoriteLastTime.TryGetValue(casterActor, out var lastCastTime) &&
                (now - lastCastTime) < Meteorite_CooldownSeconds)
                return false;

            // 目标瓦片
            WorldTile targetTile = pTile;

            // 计算发射起点（保持你原来从目标上方 200f，左右 ±200f 的视觉入射方向）
            Vector2 casterPos = casterActor.current_tile.pos;
            Vector2 targetPos = targetTile.pos;

            // 原来：左右 ±200f + 上方 200f
            // 现在：直接在目标正上方 200f
            Vector3 launchPos = new Vector3(targetPos.x, targetPos.y + 50f, 0f);

            Vector3 targetWorldPos = new Vector3(targetPos.x, targetPos.y, 0f);

            // 1) 生成“陨石抛射物”（若没有此资源则回退为 fireball）
            string[] meteorCandidates = { "Thesunsets" }; // 按需填你的实际抛射物 ID
            object proj = null;
            foreach (var id in meteorCandidates)
            {
                // spawn 抛射物（pTargetObject 传 pTarget 可跟踪目标对象，不需要就传 null）
                proj = World.world.projectiles.spawn(
                    pInitiator: casterActor,
                    pTargetObject: pTarget,        // 可为 null，只按位置飞行
                    pAssetID: id,
                    pLaunchPosition: launchPos,
                    pTargetPosition: targetWorldPos,
                    pTargetZ: 0f
                );
                if (proj != null) break; // 找到可用的抛射物资源则终止
            }

            // 如果上述候选都失败，使用 fireball 作为兜底
            if (proj == null)
            {
                proj = World.world.projectiles.spawn(
                    pInitiator: casterActor,
                    pTargetObject: pTarget,
                    pAssetID: "Thesunsets",
                    pLaunchPosition: launchPos,
                    pTargetPosition: targetWorldPos,
                    pTargetZ: 0f
                );
            }

            // 2) 计算范围与伤害（保持你的原逻辑：立即在目标位置结算，不等待命中）
            int desiredRadius = 30; // 陨石的影响范围
            int radiusSquared = desiredRadius * desiredRadius;

            // 获取攻击者的伤害属性
            float unitDamage = GetActorDamage(casterActor);
            // 伤害倍率（保持 410 倍）
            float finalDamage = unitDamage * 410f;

            // 遍历单位并直接扣血
            List<Actor> allUnits = World.world.units.getSimpleList();
            for (int i = 0; i < allUnits.Count; i++)
            {
                Actor targetActor = allUnits[i];
                if (targetActor == null || targetActor.current_tile == null) continue;

                // 检查单位是否在范围内
                if (Toolbox.SquaredDistVec2(targetActor.current_tile.pos, targetTile.pos) <= radiusSquared)
                {
                    // 避免对施法者自身造成伤害
                    if (targetActor == casterActor)
                        continue;

                    // 应用自定义伤害
                    ApplyLightningDamage(casterActor, targetActor, finalDamage);
                }
            }

            // 3) 更新冷却
            _meteoriteLastTime[casterActor] = now;

            return true;
        }

        /// <summary>
        /// 自定义大范围效果应用方法，避免Brush类对半径的限制
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

        private static float GetActorDamage(Actor pActor)
        {
            if (pActor == null || pActor.stats == null)
                return 1f;

            // 仅返回damage属性值
            return Mathf.Max(1, pActor.stats["damage"]); // 确保至少有1点伤害
        }
    }
}