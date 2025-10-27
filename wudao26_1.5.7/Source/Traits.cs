using System;
using System.Collections.Generic;
using System.Linq;
using ai;
using CustomModT001.Abstract;
using NeoModLoader.api.attributes;
using strings;
using UnityEngine;

namespace CustomModT001;

public partial class Traits : ExtendLibrary<ActorTrait, Traits>
{
    public static HashSet<string> all_enabled_traits;
    public static HashSet<string> passive_traits;
    public static HashSet<string> active_traits;
    public static HashSet<string> all_traits;

    private static readonly BaseStats demon_sword_stats   = new();
    private static readonly BaseStats demon_crown_stats   = new();
    private static readonly BaseStats demon_armor_stats   = new();
    private static readonly BaseStats king_sword_stats    = new();
    private static readonly BaseStats king_crown_stats    = new();
    private static readonly BaseStats king_armor_stats    = new();
    private static readonly BaseStats emperor_heart_stats = new();

#region 辅助
public static ActorTrait summoned { get; private set; }
#endregion

protected override void OnInit()
{
    RegisterAssets(ModClass.asset_id_prefix);
    AddEffects();
    AddEffectsV101();
    AddEffectsV102();
    AddEffectsV103();

    // 召唤物特质核心设置 - 移除不存在的can_trigger_special属性
    summoned.group_id = S_TraitGroup.special;
    summoned.can_be_given = false;
    summoned.special_effect_interval = 1f; // 每秒触发一次扣血逻辑
    
    // 召唤物死亡时的处理逻辑
    summoned.action_death = (obj, tile) =>
    {
        if (obj == null || obj.a == null) return false;
        var owner = obj.a.GetOwner();
        if (owner == null) return false;

        if (owner.hasTrait(Traits.soul_return.id))
        {
            owner.UpdateSoulReturn();
        }
        return true;
    };  
// 每秒触发的特殊效果（包含扣血逻辑）
summoned.action_special_effect = (obj, tile) =>
{
    // 基础空引用检查
    if (obj == null || obj.a == null) return true;
    Actor summon = obj.a;
    
    // 检查召唤物存活状态及数据有效性
    if (!summon.isAlive() || summon.data == null) return true;

    // 核心：每秒失去最大生命值的5%（添加保底伤害）
    float maxHealth = summon.getMaxHealth();
    float damagePercent = 0.02f;
    float damage = Mathf.Max(1, maxHealth * damagePercent);
    
    // 执行扣血（使用真实伤害类型，忽略防御）
    summon.getHit(damage, false, pAttackType: AttackType.None, pAttacker: null, pSkipIfShake: true);
    
    // 扣血后检查是否已死亡，确保触发死亡逻辑
    if (summon.data.health <= 0 && summon.isAlive())
    {
        summon.checkCallbacksOnDeath();
        summon.die();
        return true; // 已死亡，提前退出
    }

    // 检查所有者状态
    Actor owner = summon.GetOwner();
    if (owner == null || !owner.isAlive()) // 所有者不存在或已死亡
    {
        if (!summon.hasTrait("madness") && summon.isAlive())
        {
            summon.checkCallbacksOnDeath();
            summon.die();
        }
        return true;
    }
    else // 所有者存在且存活
    {
        // 同步阵营
        summon.setKingdom(owner.kingdom);

        // 特质加成逻辑
        if (owner.hasTrait(empty_feathered.id))
        {
            owner.addStatusEffect(Statuses.active_empty_feathered.id);
        }
        
        if (owner.hasTrait(servant_master.id))
        {
            summon.addStatusEffect(Statuses.active_servant_master.id);
        }
    }

    return true;
}; // lambda表达式正确闭合
    // 后续特质集合初始化逻辑（保持不变）
    all_traits = System.Linq.Enumerable.ToHashSet(assets_added.Select(x => x.id));
    all_enabled_traits = new HashSet<string>(assets_added.Where(x => x.can_be_given && !x.GetExtend().disabled).Select(x => x.id));
    passive_traits = new HashSet<string>(all_enabled_traits.Where(id =>
    {
        var trait = AssetManager.traits.get(id);
        if (trait == null) return false;
        var te = trait.GetExtend();
        return te.trait_type == TraitType.Passive || (te.trait_type == TraitType.None && !te.HasCooldown);
    }));
    active_traits = new HashSet<string>(all_enabled_traits.Where(id =>
    {
        var trait = AssetManager.traits.get(id);
        if (trait == null) return false;
        var te = trait.GetExtend();
        return te.trait_type == TraitType.Active || (te.trait_type == TraitType.None && te.HasCooldown);
    }));
}

    private void AddEffects()
    {
        bool over_half_health(Actor actor)
        {
            return actor.data.health / (float)actor.getMaxHealth() > 0.5f;
        }

        // 魔王之刃：当血量在百分之五十以上时，获得五倍攻击
        demon_sword_stats[S.multiplier_damage] = 5f;
        demon_sword.GetExtend()
            .SetConditionalBaseStats(demon_sword_stats, a => a.hasTrait(emperor_heart.id) || over_half_health(a));
        // 魔王冠冕：当血量在百分之五十以上时，获得两倍暴击率，两倍暴击伤害
        demon_crown_stats[S.multiplier_crit] = 2f;
        demon_crown_stats[S.critical_damage_multiplier] = 2f;
        demon_crown.GetExtend()
            .SetConditionalBaseStats(demon_crown_stats, a => a.hasTrait(emperor_heart.id) || over_half_health(a));
        // 魔王铠甲：当血量在百分之五十以上时，获得1.5倍护甲
        demon_armor_stats[Stats.multiplier_armor.id] = 1.5f;
        demon_armor.GetExtend()
            .SetConditionalBaseStats(demon_armor_stats, a => a.hasTrait(emperor_heart.id) || over_half_health(a));
        // 国王之剑：当血量在百分之五十以下时，获得五倍攻击
        king_sword_stats[S.multiplier_damage] = 5f;
        king_sword.GetExtend()
            .SetConditionalBaseStats(king_sword_stats, a => a.hasTrait(emperor_heart.id) || !over_half_health(a));
        // 国王冠冕：当血量在百分之五十以下时，获得两倍暴击率，两倍暴击伤害
        king_crown_stats[S.multiplier_crit] = 2f;
        king_crown_stats[S.critical_damage_multiplier] = 2f;
        king_crown.GetExtend()
            .SetConditionalBaseStats(king_crown_stats, a => a.hasTrait(emperor_heart.id) || !over_half_health(a));
        // 国王铠甲：当血量在百分之五十以下时，获得1.5倍攻击
        king_armor_stats[S.multiplier_damage] = 1.5f;
        king_armor.GetExtend()
            .SetConditionalBaseStats(king_armor_stats, a => a.hasTrait(emperor_heart.id) || !over_half_health(a));
        // 王者之心：魔王和国王系列特质，发挥作用时不在有血量限制，当同时拥有国王和魔王特质时，额外获得20倍攻击，3倍防御力，3倍暴击，5%吸血，同时获得50%额外免伤，和抗击退
        emperor_heart_stats[S.multiplier_damage] = 20f;
        emperor_heart_stats[Stats.multiplier_armor.id] = 3f;
        emperor_heart_stats[S.multiplier_crit] = 3f;
        emperor_heart_stats[S.multiplier_health] = 20;
        emperor_heart_stats[Stats.life_steal.id] = 0.05f;
        emperor_heart_stats[Stats.addition_armor.id] = 50f;
        emperor_heart_stats[S.knockback_reduction] = 1;

        bool HasKingTrait(Actor a)
        {
            return a.hasTrait(king_armor.id) || a.hasTrait(king_crown.id) || a.hasTrait(king_sword.id);
        }

        bool HasDemonTrait(Actor a)
        {
            return a.hasTrait(demon_armor.id) || a.hasTrait(demon_crown.id) || a.hasTrait(demon_sword.id);
        }

        int CountKingDemonTrait(Actor a)
        {
            int count = 0;
            if (a.hasTrait(king_armor.id)) count++;
            if (a.hasTrait(king_crown.id)) count++;
            if (a.hasTrait(king_sword.id)) count++;
            if (a.hasTrait(demon_armor.id)) count++;
            if (a.hasTrait(demon_crown.id)) count++;
            if (a.hasTrait(demon_sword.id)) count++;
            return count;
        }

        var emperor_heart_stats_2 = new BaseStats()
        {
            [S.multiplier_damage] = 5,
            [S.multiplier_health] = 5,
            [Stats.multiplier_armor.id] = 1.2f,
            [S.multiplier_crit] = 1.5f,
            [Stats.addition_armor.id] = 20,
        };
        var emperor_heart_stats_3 = new BaseStats()
        {
            [S.multiplier_damage] = 50,
            [S.multiplier_health] = 50,
            [Stats.multiplier_armor.id] = 3,
            [S.multiplier_crit] = 3,
            [Stats.life_steal.id] = 0.1f,
            [Stats.addition_armor.id] = 90,
            [S.knockback_reduction] = 0.9f
        };
        emperor_heart.GetExtend().conditional_basestats = a =>
        {
            var has_king_trait = HasKingTrait(a);
            var has_demon_trait = HasDemonTrait(a);
            if (has_king_trait && has_demon_trait)
            {
                return emperor_heart_stats_3;
            }

            if (has_king_trait || has_demon_trait)
            {
                return emperor_heart_stats_2;
            }

            return null;
        };
        emperor_heart.action_special_effect = (obj, tile) =>
        {
            var a = obj.a;
            if (HasDemonTrait(a) && HasKingTrait(a) && CountKingDemonTrait(a) >= 3)
            {
                a.addTrait(survival_of_civilization.id);
            }

            return true;
        };
        emperor_heart.base_stats[S.multiplier_damage] = 0.2f;
        emperor_heart.base_stats[S.multiplier_health] = 0.2f;

        // 亮出你的剑：手越短，伤害越高
        close_combat.GetExtend().conditional_basestats = a =>
        {
            if (a.hasTrait(far_combat.id)) return null;

            return new BaseStats
            {
                [S.multiplier_damage] = 100f / (5f + a.stats[S.range]),
                [Stats.life_steal.id] = a.getWeaponAsset()?.attack_type == WeaponType.Melee ? 0.02f : 0f
            };
        };
        // 猎手的狙击镜：手越长，伤害越高
        far_combat.GetExtend().conditional_basestats = a =>
        {
            if (a.hasTrait(close_combat.id)) return null;

            return new BaseStats
            {
                [S.multiplier_damage] = 0.1f * a.stats[S.range],
                [Stats.life_steal.id] = a.getWeaponAsset()?.attack_type == WeaponType.Range ? 0.15f : 0f
            };
        };
        // 恶魔拥抱：总是尝试添加一个效果，5秒计算一次，扣100点血量，加3点攻速，20攻击力，不攻击10秒后消失这个buff
        demon_embrace.special_effect_interval = 1f;
        demon_embrace.action_special_effect = (obj, _) =>
        {
            if (obj.a.GetContinueAttackTimes() == 0) return true;
            obj.a.ApplyStatusEffectTo(obj, Statuses.active_demon_embrace.id, 5, true);
            return true;
        };
        // 不灭之躯：每5秒，回复200血量
        immortal_body.special_effect_interval = 5f;
        immortal_body.action_special_effect = (obj, tile) =>
        {
            obj.a.restoreHealth(5000);
            return true;
        };
        // 刚化我心：攻击一百次后，获得10000点血量，仅能触发一次
        steel_heart.special_effect_interval = 10;
        steel_heart.action_special_effect = (obj, tile) =>
        {
            obj.a.restoreHealth(50);
            return true;
        };
        steel_heart.GetExtend()
            .SetConditionalBaseStats(new BaseStats { [S.health] = 200000 }, a => a.GetSteelHeartCount() >= 100);
        // 海洋龙魂：10秒回复 100蓝量
        ocean_loong_soul.special_effect_interval = 10f;
        ocean_loong_soul.action_special_effect = (obj, tile) =>
        {
            var a = obj.a;
            a.restoreHealth(500);
            a.RestoreMana(50000);
            if (a.hasTrait(loong_heart.id))
            {
                a.restoreHealth((int)(obj.stats[S.health]*0.01f));
            }
            return true;
        };
        // 天使之泪：每5秒，将20蓝量转化为400点生命
        angel_tear.special_effect_interval = 5f;
        angel_tear.action_special_effect = (obj, tile) =>
        {
            Actor actor = obj.a;
            if (actor.GetLastAttackTime() + 60 < World.world.getCurWorldTime()) return true;
            var mana = Mathf.Min(actor.GetMana(), actor.stats[S.mana]);
            var health_can_restore = actor.getMaxHealth() - actor.data.health;
            var health_to_restore = Mathf.Min(mana * 100, health_can_restore);
            var mana_to_take = health_to_restore / 100;
            actor.RestoreMana(-mana_to_take);
            actor.restoreHealth((int)health_to_restore);

            return true;
        };
        //狂风骤雨（主动技能）：以1%的攻击力，在4秒内，发动200次射击。（cd60s）
        squally_showers.SetupSkill(10);
        squally_showers.special_effect_interval = 0.02f;
        squally_showers.action_special_effect = (obj, tile) =>
        {
            Actor actor = obj.a;
            BaseSimObject target = actor.attack_target ?? actor.attackedBy;

            if (actor.GetSquallyShowersCount() >= 200 || target == null) return true;

            var target_size = target.stats[S.size];
            var count = Math.Min(50, 200 - actor.GetSquallyShowersCount());
            for (int i = 0; i < count; i++)
            {
                World.world.projectiles.spawn(actor, target, S_Projectile.arrow, actor.current_position,
                    target.current_position + new Vector2(Randy.randomFloat(-target_size, target_size),
                        Randy.randomFloat(-target_size, target_size)) * 2, target.getHeight());
            }
            actor.IncSquallyShowersCount(count);


            return true;
        };
        squally_showers.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(squally_showers.id) ||
                !actor.a.TryTakeMana(squally_showers.GetExtend().mana_cost)) return true;
            actor.a.SetupSquallyShowers();
            //actor.StartCoroutine(squally_showers_effect(actor.a, target));
            actor.a.StartCooldown(squally_showers);
            return true;
        };

        // 轰鸣之手（主动技能）：发动一道雷霆，范围（9x9），伤害为攻击力的200%
        roaring_hand.SetupSkill(10);
        roaring_hand.action_attack_target = (actor, target, tile) =>
        {
            if (actor.a.TraitUnderCooldown(roaring_hand.id) ||
                !actor.a.TryTakeMana(roaring_hand.GetExtend().mana_cost)) return true;
            var damage = actor.stats[S.damage] * 2;
            for (var i = -4; i <= 4; i++)
            for (var j = -4; j <= 4; j++)
            {
                WorldTile curr_tile = World.world.GetTile(tile.pos.x + i, tile.pos.y + j);
                if (curr_tile == null) continue;
                foreach (Actor unit in curr_tile._units)
                {
                    if (unit == null || !unit.isAlive()) continue;
                    if (actor.kingdom?.isEnemy(unit.kingdom) ?? true)
                        unit.getHit(damage, true, pAttackType: AttackType.Other, pAttacker: actor, pSkipIfShake:false);
                }
            }

            var radius = 4;
            EffectsLibrary.spawnAtTileRandomScale("fx_lightning_medium", tile, radius / 16f, radius / 14f);
            MapAction.damageWorld(tile, radius, AssetManager.terraform.get("lightning_normal"), actor);
            return true;
        };
        // 杀戮时刻（技能）：攻击提高二十倍十秒（cd60s）
        kill_time.SetupSkill(60);
        kill_time.action_attack_target = (actor, _, _) =>
        {
            if (actor.a.TraitUnderCooldown(kill_time.id) ||
                !actor.a.TryTakeMana(kill_time.GetExtend().mana_cost)) return true;
            actor.a.ApplyStatusEffectTo(actor, Statuses.active_kill_time.id, 5, true);
            actor.a.StartCooldown(kill_time);
            return true;
        };
        // 冰封之墓：攻击冰冻5秒，（cd20s）
        frozen_tomb.SetupSkill(20);
        frozen_tomb.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(frozen_tomb.id) ||
                !actor.a.TryTakeMana(frozen_tomb.GetExtend().mana_cost)) return true;
            actor.a.ApplyStatusEffectTo(target, "frozen", 5, true);
            actor.a.StartCooldown(frozen_tomb);
            return true;
        };
        // 蓄意轰拳（技能）：当生命值跌落到50%时，在前方召唤一次爆炸，伤害值为自身血量的百分之二十，为真伤，加个10s的冷却cd
        intentional_punching.SetupSkill(10);
        intentional_punching.action_get_hit = (actor, _, tile) =>
        {
            if (over_half_health(actor.a) || actor.a.TraitUnderCooldown(intentional_punching.id) ||
                !actor.a.TryTakeMana(intentional_punching.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(intentional_punching);

            EffectsLibrary.spawnAtTileRandomScale("fx_explosion_middle", tile, 0.45f, 0.6f);

            var damage = actor.stats[S.health] * 0.2f;

            var shield = actor.a.getMaxHealth() * 0.5f;
            actor.a.AddShield(new ShieldData
            {
                left_time = 10,
                value = shield
            });

            var radius = 2;
            using var list = new ListPool<Actor>();
            for (var i = -4; i <= 4; i++)
            for (var j = -4; j <= 4; j++)
            {
                WorldTile curr_tile = World.world.GetTile(tile.pos.x + i, tile.pos.y + j);
                if (curr_tile == null) continue;
                foreach (Actor unit in curr_tile._units)
                {
                    if (unit == null || !unit.isAlive()) continue;
                    list.Add(unit);
                }
            }
            


            for (var i = 0; i < list.Count(); i++)
            {
                Actor unit = ((IList<Actor>)list)[i];
                if (Toolbox.DistVec2(unit.current_tile.pos, tile.pos) > radius) continue;
                if (unit                                             == actor) continue;
                if (actor.kingdom?.isEnemy(unit.kingdom) ?? true)
                    unit.getHit(damage, true, pAttackType: AttackType.None, pAttacker: actor, pSkipIfShake:false);
            }

            MapAction.damageWorld(tile, radius, Terraforms.intentional_punching_terra, actor);

            return true;
        };
        // 狂暴之心（技能）：获取无敌状态5秒（cd 120s）
        crazy_heart.SetupSkill(120);
        crazy_heart.action_get_hit = (actor, _, _) =>
        {
            if (actor.a.TraitUnderCooldown(crazy_heart.id) ||
                !actor.a.TryTakeMana(crazy_heart.GetExtend().mana_cost)) return true;
            actor.a.AddInvincible(5);
            actor.a.StartCooldown(crazy_heart);
            return true;
        };

        // 利刃华尔兹（技能：）：获取无敌状态2秒，同时获取4秒+300的攻速(cd 80s)
        bool sharp_blade_waltz_effect(BaseSimObject actor, BaseSimObject _, WorldTile tile)
        {
            if (actor.a.TraitUnderCooldown(sharp_blade_waltz.id) ||
                !actor.a.TryTakeMana(sharp_blade_waltz.GetExtend().mana_cost)) return true;
            actor.a.AddInvincible(2);
            actor.a.ApplyStatusEffectTo(actor, Statuses.active_sharp_blade_waltz.id);
            actor.a.StartCooldown(sharp_blade_waltz);
            return true;
        }

        sharp_blade_waltz.SetupSkill(80);
        sharp_blade_waltz.action_get_hit = sharp_blade_waltz_effect;
        sharp_blade_waltz.action_attack_target = sharp_blade_waltz_effect;
        // 亡者的召唤（技能）：召唤三个骷髅跟随自己，骷髅不会被其他友军攻击（cd，1200s）
        summoning_dead.special_effect_interval = 1;
        summoning_dead.SetupSkill(600);
        summoning_dead.action_special_effect = (obj, tile) =>
        {
            if (obj.a.TraitUnderCooldown(summoning_dead.id) ||
                !obj.a.TryTakeMana(summoning_dead.GetExtend().mana_cost)) return true;
            var summon_stats = new BaseStats();
            summon_stats.mergeStats(Actors.skeleton_summoned.base_stats);
            summon_stats[S.health] = obj.stats[S.health] * 0.25f;
            summon_stats[S.damage] = obj.stats[S.damage] * 0.25f;
            for (var i = 0; i < 3; i++)
            {
                Actor summoned_unit = World.world.units.spawnNewUnit(Actors.skeleton_summoned.id, tile, true, pSpawnHeight:0);
                if (summoned_unit == null) continue;
                summoned_unit.SetOwner(obj.a);
                summoned_unit.OverwriteStats(summon_stats);
            }

            obj.a.StartCooldown(summoning_dead);
            return true;
        };
        // 奥术飞弹（技能）：攻击射出一颗火球
        arcane_missiles.SetupSkill(6);
        arcane_missiles.action_attack_target = (obj, target, tile) =>
        {
            if (obj.a.TraitUnderCooldown(arcane_missiles.id) ||
                !obj.a.TryTakeMana(arcane_missiles.GetExtend().mana_cost)) return true;
            World.world.projectiles.spawn(obj, target, S_Projectile.firebomb, obj.current_position,
                target.current_position);
            obj.a.StartCooldown(arcane_missiles);
            return true;
        };
        // 永恒禁锢：攻击禁锢5秒，（敌人移速为零），（cd8s）
        eternal_confine.SetupSkill(8);
        eternal_confine.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(eternal_confine.id) ||
                !actor.a.TryTakeMana(eternal_confine.GetExtend().mana_cost)) return true;
            actor.a.ApplyStatusEffectTo(target, Statuses.confined.id);
            actor.a.StartCooldown(eternal_confine);
            return true;
        };
        // 闷棍：（主动技能）：攻击对敌人造成2秒眩晕（cd 8s）
        staggering_blow.SetupSkill(8);
        staggering_blow.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(staggering_blow.id) ||
                !actor.a.TryTakeMana(staggering_blow.GetExtend().mana_cost)) return true;
            actor.a.ApplyStatusEffectTo(target, Statuses.dizzy.id);
            actor.a.StartCooldown(staggering_blow);
            return true;
        };
        // 爆裂黎明：（主动技能）召唤一颗攻击力x10的大范围伤害炸弹，眩晕大范围敌人5秒，同时之后召唤两个魂灵之影（恶魔）守护自己。（cd 60s）
        explosive_dawn.SetupSkill(60);
        explosive_dawn.action_attack_target = (actor, target, tile) =>
        {
            if (actor.a.TraitUnderCooldown(explosive_dawn.id) ||
                !actor.a.TryTakeMana(explosive_dawn.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(explosive_dawn);

            var damage = actor.stats[S.damage] * 10;
            var half_edge = 30;
            var x = target.current_tile.pos.x;
            var y = target.current_tile.pos.y;
            for (var i = -half_edge; i <= half_edge; i++)
            for (var j = -half_edge; j <= half_edge; j++)
            {
                WorldTile curr_tile = World.world.GetTile(x + i, y + j);
                if (curr_tile == null) continue;
                foreach (Actor unit in curr_tile._units)
                {
                    if (unit == null || !unit.isAlive()) continue;
                    if (actor.kingdom?.isEnemy(unit.kingdom) ?? true)
                    {
                        unit.getHit(damage, true, pAttackType: AttackType.Other, pAttacker: actor, pSkipIfShake:false);
                        actor.a.ApplyStatusEffectTo(unit, Statuses.dizzy.id, 5, true);
                    }
                }
            }

            EffectsLibrary.spawnAtTileRandomScale("fx_explosion_middle", tile, 0.45f, 0.6f);
            MapAction.damageWorld(tile, 6, Terraforms.intentional_punching_terra, actor);
            for (var i = 0; i < 2; i++)
            {
                Actor summoned_unit = World.world.units.spawnNewUnit(Actors.demon_summoned.id, tile, true, pSpawnHeight:0);
                if (summoned_unit == null) continue;
                summoned_unit.SetOwner(actor.a);
            }

            return true;
        };
        // 混沌之心：控制效果时长提高两倍
        chaos_heart.base_stats[Stats.status_time_give.id] = 2f;
        chaos_heart.base_stats[S.damage] = 50f;
        // 熵之力：自己受到，和对敌人造成的控制效果，时长提高五倍
        entropy_power.base_stats[Stats.control_get.id] = 8f;
        entropy_power.base_stats[Stats.control_give.id] = 8f;
        entropy_power.base_stats[S.damage] = 50f;
        // 秘术冲拳：每次攻击，让技能冷却cd减少一秒
        secret_punching.base_stats[S.attack_speed] = 10;
        secret_punching.action_attack_target = (actor, _, _) =>
        {
            foreach (var trait in actor.a.traits)
            {
                if (trait.GetExtend().HasCooldown) actor.a.DecreaseCooldown(trait.id, 1);
            }

            return true;
        };
        // 不坏之身：周围敌人越多，自身防御越高 
        indestructibility.GetExtend().conditional_basestats = a =>
        {
            var half_edge = 3;
            var x = a.current_tile.pos.x;
            var y = a.current_tile.pos.y;
            var count = 0;
            for (var i = -half_edge; i <= half_edge; i++)
            for (var j = -half_edge; j <= half_edge; j++)
            {
                WorldTile tile = World.world.GetTile(x + i, y + i);
                if (tile == null) continue;
                count += tile._units.Count;
            }

            return new BaseStats
            {
                [S.armor] = count
            };
        };
        // 岿然如大地：防御越高回血越快
        steadfastness.special_effect_interval = 10f;
        steadfastness.action_special_effect = (obj, _) =>
        {
            obj.a.restoreHealth((int)obj.a.stats[S.armor]);
            return true;
        };
        steadfastness.action_get_hit = (actor, _, _) =>
        {
            if (actor.stats[S.armor] >= 45)actor.a.restoreHealth(500);
            return true;
        };
        // 狂妄泰坦：身体变得巨大，血量增加8倍，攻击力提高8倍。
        arrogant_giant.base_stats[S.multiplier_health] = 5f;
        arrogant_giant.base_stats[S.multiplier_damage] = 5f;
        arrogant_giant.base_stats[S.scale] = 0.2f;
        arrogant_giant.base_stats[S.armor] = -99;
        // 渺小猎手：身体变得渺小，血量降低为四分之一，攻速增加20，攻击会对狂妄泰坦特质的持有者造成一万点伤害。
        little_hunter.GetExtend().final_basestats[S.multiplier_health] = -0.75f;
        little_hunter.base_stats[S.attack_speed] = 20f;
        little_hunter.action_attack_target = [Hotfixable](actor, target, _) =>
        {
            if (target.isActor() && (target.a.hasTrait(arrogant_giant.id) || target.a.hasTrait(feast.id)))
            {
                target.getHit(80000, pAttackType: AttackType.None, pAttacker: actor, pSkipIfShake: false);
            }

            return true;
        };
        // 护盾守驭：当血量跌落到百分之五十的时候，将百分之五十血量转化为护盾 获获得一个护盾，持续10秒，内置冷却15s
        shield_guarding.SetupSkill(15);
        shield_guarding.action_get_hit = (actor, _, _) =>
        {
            if (actor.getData().health / (float)actor.getMaxHealth() > 0.5f) return true;
            if (actor.a.TraitUnderCooldown(shield_guarding.id) ||
                !actor.a.TryTakeMana(shield_guarding.GetExtend().mana_cost)) return true;
            var shield = actor.a.getMaxHealth() * 0.5f;
            actor.a.AddShield(new ShieldData
            {
                left_time = 10,
                value = shield
            });
            actor.a.StartCooldown(shield_guarding);
            return true;
        };
        shield_guarding.GetExtend().disabled = true;
        // 护盾猛击：当自身拥有护盾时，下一次攻击，造成护盾值50%的伤害
        shield_attack.base_stats[S.damage] = 10;
        shield_attack.action_attack_target = (actor, target, _) =>
        {
            var shields = actor.a.GetShields();
            if (shields.Count == 0) return true;
            var shield_value = shields.Sum(x => x.value);
            target.getHit(shield_value * 10f, pAttacker: actor, pAttackType: AttackType.None,pSkipIfShake:false);
            return true;
        };
        // 湖中神盾：当自身获得护盾时，将护盾值的百分之十转化为永久血量 无限叠加
lake_shield.base_stats[S.health] = 200;

lake_shield.GetExtend().action_get_shield = [Hotfixable](Actor actor, ref ShieldData shield_data) =>
{
    actor.AddLakeShield(shield_data.value * 0.1f);
};
lake_shield.GetExtend().conditional_basestats = a => 
{
    float baseHealthBonus = a.GetLakeShield() * (1 + a.stats[Stats.stats_stacked_effect.id]);
    float healthCap = 2000000f;
    float finalHealthBonus = Mathf.Min(baseHealthBonus, healthCap);
    
    return new BaseStats 
    { 
        [S.health] = finalHealthBonus 
    };
};

        // 护盾裁决者：攻击持有护盾的目标时，对其造成等同护盾值的真伤
        shield_killer.base_stats[S.damage] = 10;
        shield_killer.action_attack_target = (actor, target, _) =>
        {
            if (!target.isActor()) return true;
            var shields = target.a.GetShields();
            if (shields.Count == 0) return true;
            var shield_value = shields.Sum(x => x.value) * 0.05f;
            target.a.getHit(shield_value, true, pAttackType: AttackType.None, pAttacker: actor, pSkipIfShake:false);
            return true;
        };
        // 踢踏舞：每次攻击造成获取50移速，停止攻击十秒后失去移速
        dance_attack.GetExtend().conditional_basestats = a =>
        {
            var times = a.GetContinueAttackTimes();
            if (times > 0)
                return new BaseStats
                {
                    [S.speed] = 50f * times * (a.stats[Stats.stats_stacked_effect.id] + 1)
                };
            return null;
        };
        // 舞中刃：每20移速获得20攻速
        sword_in_dance.GetExtend().conditional_basestats = a => new BaseStats
        {
            [S.attack_speed] = Mathf.Floor(a.stats[S.speed] / 20f) * 20f
        };
        // 刃中舞：每20移速获得20攻击
        dance_in_sword.GetExtend().conditional_basestats = a => new BaseStats
        {
            [S.damage] = Mathf.Floor(a.stats[S.speed] / 20f) * 20f
        };
        //关键暴击：获得百分之六十的暴击率
        vital_crit.base_stats[S.critical_chance] = 0.6f;
        //弱点解析：获取五倍暴击伤害
        weakness_analysis.base_stats[S.critical_damage_multiplier] = 20f;
        //会心防守：可以使用暴击几率来获取额外免伤，百分之百暴击时获得百分之五十额外免伤
        heart_defence.base_stats[S.armor] = 5;
        heart_defence.GetExtend().conditional_basestats = a => new BaseStats
        {
            [Stats.addition_armor.id] = Mathf.Clamp(a.stats[S.critical_chance] * 0.5f, 0, 0.8f) * 100f,
            [S.knockback_reduction] = a.stats[S.critical_chance] > 0.6f ? 1 : 0
        };
        // 未照耀的荣光：未发动攻击时，攻击力每十秒获得一次翻倍，翻倍四次后，停止翻倍，并将第一次攻击转化为真伤，第一次攻击后，所有增益消失，需要重新叠加（2倍，3倍，4倍，5倍）
        illuminated_glory.GetExtend().conditional_basestats = a =>
        {
            var last_attack_time = a.GetLastAttackTime();
            var curr_time = (float)MapBox.instance.getCurWorldTime();
            if (last_attack_time + 10 > curr_time) return null;

            return new BaseStats
            {
                [S.multiplier_damage] = Math.Min((int)((curr_time - last_attack_time) / 10), 4)
            };
        };
        //士兵证章：如果自己职位是士兵，则获得5倍攻击力提升！
        soldier_badge.base_stats[S.damage] = 10;
        soldier_badge.GetExtend().SetConditionalBaseStats(new BaseStats
        {
            [S.multiplier_damage] = 5f
        }, ActorExtendTools.HasSoldierBadge);
        //死仇时代的恨意:自身攻击力加5倍，攻击敌人为敌人附加5倍攻击力提升buff。
        death_hatred.base_stats[S.multiplier_damage] = 5;
        death_hatred.action_attack_target = (attacker, target, _) =>
        {
            attacker.a.ApplyStatusEffectTo(target, Statuses.active_death_hatred.id);
            return true;
        };
        // 希望时代的的涂鸦：自身攻击力变为二分之一，攻击敌人让其攻击力变为十分之一
        hope_graffiti.GetExtend().final_basestats[S.multiplier_damage] = -0.5f;
        hope_graffiti.action_attack_target = (attacker, target, _) =>
        {
            attacker.a.ApplyStatusEffectTo(target, Statuses.active_hope_graffiti.id);
            return true;
        };
        // 侵蚀：攻击时削减对方一半的防御
        erosion.action_attack_target = (attacker, target, _) =>
        {
            attacker.a.ApplyStatusEffectTo(target, Statuses.active_erosion.id);
            return true;
        };
        // 裁决使：对生命值跌到百分之二十以下的敌人，攻击会造成急死效果的斩杀。
        adjudication.action_attack_target = (attacker, target, _) =>
        {
            if (!target.isActor()) return true;
            if (target.a.data.health / (float)target.a.getMaxHealth() < 0.05f)
            {
                target.a.attackedBy = attacker;
                target.a.die(true);
            }

            return true;
        };
        self_destruct.action_death = (actor, tile) =>
        {
            var damage = actor.getMaxHealth() * 0.5f;
            actor.a.findCurrentTile();
            var half_edge = 2;
            var x = actor.current_tile.x;
            var y = actor.current_tile.y;
            for (var i = -half_edge; i <= half_edge; i++)
            for (var j = -half_edge; j <= half_edge; j++)
            {
                WorldTile t = World.world.GetTile(x + i, y + j);
                if (t == null) continue;
                var list = t._units;
                for (var k = 0; k < list.Count; k++)
                {
                    Actor unit = list[k];
                    if (unit == null || !unit.isAlive()) continue;
                    if (unit == actor) continue;
                    if (actor.kingdom?.isEnemy(unit.kingdom) ?? true)
                        unit.getHit(damage, true, pAttackType: AttackType.None, pAttacker: actor, pSkipIfShake:false);
                }
            }

            MapAction.damageWorld(tile, 10, Terraforms.intentional_punching_terra, actor);
            return true;
        };
        // 超凡邪恶：每击杀一个敌人，获得10攻击力
        extraordinary_evil.GetExtend().conditional_basestats = a => new BaseStats
        {
            [S.damage] = a.data.kills * 10 * (a.stats[Stats.stats_stacked_effect.id] + 1)
        };
        // 二重身：死后生成一个和自己属性一样，阵营一样，但没有任何特质的个体，无法修行任何体系
        doppelganger.action_death = (actor, tile) =>
        {
            Actor new_actor = World.world.units.spawnNewUnit(actor.a.asset.id, tile, true, pSpawnHeight:0);
            if (new_actor == null) return true;
            ActorTool.copyUnitToOtherUnit(actor.a, new_actor);
            new_actor.setKingdom(actor.kingdom);
            new_actor.traits.Clear();
            new_actor.addTrait(summoned.id);
            // 使用name属性替代setName方法
            new_actor.data.name = new_actor.getName() + "(二重身)";
            new_actor.OverwriteStats(actor.stats);
            new_actor.AddInvincible(20);
            return true;
        };
        //嗜血之约：当击杀数超过100，获取2000攻击力。
        bloodthirsty_covenant.GetExtend()
            .SetConditionalBaseStats(new BaseStats { [S.damage] = 8000 }, a => a.data.kills > 40);
        // 战刃饮血：攻击敌人百分之二十的伤害，转化为治疗效果
        sword_life_steal.base_stats[Stats.life_steal.id] = 0.1f;
        //星界躯体：初始20000生命，从入品境界开始，每级额外+5000生命
        star_body.GetExtend().conditional_basestats = a => {
            BaseStats stats = new BaseStats();
            stats[S.health] = 20000f;
            
            // 获取武道境界等级
            int wudaoLevel = 0;
            a.data.get($"{ModClass.asset_id_prefix}.wudao_level", out wudaoLevel, 0);
            
            // 入品境界对应的等级是2，从入品开始每增加一级境界额外获得5000生命
            if (wudaoLevel >= 2) {
                int extraLevel = wudaoLevel - 2;
                stats[S.health] += extraLevel * 5000f;
            }
            
            return stats;
        };
        //飞升之躯：基础40000生命，-200攻击，-20年寿命；从入品境界开始，每级额外+10000生命，-100攻击
        soar_body.GetExtend().conditional_basestats = a => {
            BaseStats stats = new BaseStats();
            stats[S.health] = 40000f;
            stats[S.damage] = -200f;
            stats[S.lifespan] = -20f;
            
            // 获取武道境界等级
            int wudaoLevel = 0;
            a.data.get($"{ModClass.asset_id_prefix}.wudao_level", out wudaoLevel, 0);
            
            // 入品境界对应的等级是2，从入品开始每增加一级境界额外获得10000生命和减少100攻击
            if (wudaoLevel >= 2) {
                int extraLevel = wudaoLevel - 2;
                stats[S.health] += extraLevel * 10000f;
                stats[S.damage] -= extraLevel * 50f;
            }
            
            return stats;
        };
        //星界思维：加1000蓝量
        star_mind.base_stats[S.mana] = 10000f;
        //飞升思维：加2000蓝量，-20年寿命，-40攻速
        soar_mind.base_stats[S.mana] = 20000f;
        soar_mind.base_stats[S.attack_speed] = -40f;
        soar_mind.base_stats[S.lifespan] = -20f;
        //大力：初始1000攻击，从入品境界开始，每级额外+500攻击
        strong.GetExtend().conditional_basestats = a => {
            BaseStats stats = new BaseStats();
            stats[S.damage] = 1000f;
            
            // 获取武道境界等级
            int wudaoLevel = 0;
            a.data.get($"{ModClass.asset_id_prefix}.wudao_level", out wudaoLevel, 0);
            
            // 入品境界对应的等级是2，从入品开始每增加一级境界额外获得500攻击
            if (wudaoLevel >= 2)
            {
                stats[S.damage] += (wudaoLevel - 1) * 500f;
            }
            return stats;
        };
        //大力出奇迹：初始2000攻击，-500生命，-30年寿命；从入品境界开始，每级额外+1000攻击，-250生命
        very_strong.GetExtend().conditional_basestats = a => {
            BaseStats stats = new BaseStats();
            stats[S.damage] = 2000f;
            stats[S.health] = -500f;
            stats[S.lifespan] = -30f;
            
            // 获取武道境界等级
            int wudaoLevel = 0;
            a.data.get($"{ModClass.asset_id_prefix}.wudao_level", out wudaoLevel, 0);
            
            // 入品境界对应的等级是2，从入品开始每增加一级境界额外获得1000攻击和减少250生命
            if (wudaoLevel >= 2)
            {
                stats[S.damage] += (wudaoLevel - 1) * 1000f;
                stats[S.health] += (wudaoLevel - 1) * -250f;
            }
            return stats;
        };
        //动力小子：加100移速
        fast.base_stats[S.speed] = 100f;
        //动力老子：加200移速，-40年寿命，-2000生命
        very_fast.base_stats[S.speed] = 200f;
        very_fast.base_stats[S.lifespan] = -40f;
        very_fast.base_stats[S.health] = -2000f;
        //神速：加40攻速
        quick.base_stats[S.attack_speed] = 40f;
        //神速力：加100攻速，-20年寿命，-300生命
        very_quick.base_stats[S.attack_speed] = 100f;
        very_quick.base_stats[S.lifespan] = -20f;
        very_quick.base_stats[S.health] = -300f;
        // 肉食者
        meat_eater.GetExtend().conditional_basestats = a => new BaseStats
        {
            [S.health] = a.data.kills * 200 * (a.stats[Stats.stats_stacked_effect.id] + 1)
        };
    }

    protected override ActorTrait Add(ActorTrait asset)
    {
        asset.group_id = ModClass.trait_group_id;
        asset.path_icon = $"inmny/custommodt001/{asset.id.Replace($"{ModClass.asset_id_prefix}.", "")}";
        asset.needs_to_be_explored = false;
        return base.Add(asset);
    }

    #region 魔王系列

    public static ActorTrait demon_sword   { get; private set; }
    public static ActorTrait demon_crown   { get; private set; }
    public static ActorTrait demon_armor   { get; private set; }
    public static ActorTrait king_sword    { get; private set; }
    public static ActorTrait king_crown    { get; private set; }
    public static ActorTrait king_armor    { get; private set; }
    public static ActorTrait emperor_heart { get; private set; }

    #endregion

    #region 远近搭配系列

    public static ActorTrait close_combat { get; private set; }
    public static ActorTrait far_combat   { get; private set; }

    #endregion

    #region 血蓝转换系列

    public static ActorTrait demon_embrace    { get; private set; }
    public static ActorTrait immortal_body    { get; private set; }
    public static ActorTrait steel_heart      { get; private set; }
    public static ActorTrait ocean_loong_soul { get; private set; }
    public static ActorTrait angel_tear       { get; private set; }

    #endregion

    #region 主动技能

    public static ActorTrait kill_time            { get; private set; }
    public static ActorTrait frozen_tomb          { get; private set; }
    public static ActorTrait intentional_punching { get; private set; }
    public static ActorTrait crazy_heart          { get; private set; }
    public static ActorTrait sharp_blade_waltz    { get; private set; }
    public static ActorTrait eternal_confine      { get; private set; }
    public static ActorTrait summoning_dead       { get; private set; }
    public static ActorTrait arcane_missiles      { get; private set; }
    public static ActorTrait staggering_blow      { get; private set; }
    public static ActorTrait explosive_dawn       { get; private set; }
    public static ActorTrait roaring_hand         { get; private set; }
    public static ActorTrait squally_showers      { get; private set; }

    #endregion

    #region 增幅系列

    public static ActorTrait chaos_heart     { get; private set; }
    public static ActorTrait entropy_power   { get; private set; }
    public static ActorTrait secret_punching { get; private set; }

    #endregion

    #region 防御系列

    public static ActorTrait indestructibility { get; private set; }
    public static ActorTrait steadfastness     { get; private set; }

    #endregion

    #region 巨人系列

    public static ActorTrait arrogant_giant { get; private set; }
    public static ActorTrait little_hunter  { get; private set; }

    #endregion

    #region 护盾流

    public static ActorTrait shield_guarding { get; private set; }
    public static ActorTrait shield_attack   { get; private set; }
    public static ActorTrait lake_shield     { get; private set; }
    public static ActorTrait shield_killer   { get; private set; }

    #endregion

    #region 移速流

    public static ActorTrait dance_attack   { get; private set; }
    public static ActorTrait sword_in_dance { get; private set; }
    public static ActorTrait dance_in_sword { get; private set; }

    #endregion

    #region 暴击流

    public static ActorTrait vital_crit        { get; private set; }
    public static ActorTrait weakness_analysis { get; private set; }
    public static ActorTrait heart_defence     { get; private set; }

    #endregion

    #region 杂项

    public static ActorTrait illuminated_glory     { get; private set; }
    public static ActorTrait soldier_badge         { get; private set; }
    public static ActorTrait death_hatred          { get; private set; }
    public static ActorTrait hope_graffiti         { get; private set; }
    public static ActorTrait erosion               { get; private set; }
    public static ActorTrait adjudication          { get; private set; }
    public static ActorTrait self_destruct         { get; private set; }
    public static ActorTrait extraordinary_evil    { get; private set; }
    public static ActorTrait doppelganger          { get; private set; }
    public static ActorTrait bloodthirsty_covenant { get; private set; }
    public static ActorTrait sword_life_steal      { get; private set; }
    public static ActorTrait star_body             { get; private set; }
    public static ActorTrait soar_body             { get; private set; }
    public static ActorTrait star_mind             { get; private set; }
    public static ActorTrait soar_mind             { get; private set; }
    public static ActorTrait strong                { get; private set; }
    public static ActorTrait very_strong           { get; private set; }
    public static ActorTrait fast                  { get; private set; }
    public static ActorTrait very_fast             { get; private set; }
    public static ActorTrait quick                 { get; private set; }
    public static ActorTrait very_quick            { get; private set; }
    public static ActorTrait meat_eater            { get; private set; }

    #endregion
}