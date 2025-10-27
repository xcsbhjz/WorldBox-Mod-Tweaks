using System.Linq;
using NeoModLoader.api.attributes;
using NeoModLoader.General;
using Newtonsoft.Json;
using strings;
using UnityEngine;

namespace CustomModT001;

public partial class Traits
{
    public static ActorTrait like_star { get; private set; }
    public static ActorTrait unbreakable_heart { get; private set; }
    public static ActorTrait critor            { get; private set; }
    public static ActorTrait survival_of_civilization { get; private set; }
    public static ActorTrait brave                    { get; private set; }
    public static ActorTrait holy_dawn_redemption     { get; private set; }
    public static ActorTrait unrepentant_refactoring  { get; private set; }
    public static ActorTrait unbreakable_grip         { get; private set; }
    public static ActorTrait support_future           { get; private set; }
    public static ActorTrait both_attack_armor        { get; private set; }
    public static ActorTrait unmovable_like_mountain  { get; private set; }
    public static ActorTrait feast                    { get; private set; }

    public static ActorTrait stand_firm { get; private set; }

    //public static ActorTrait age_cicada               { get; private set; }
    public static ActorTrait gin_cup               { get; private set; }
    public static ActorTrait mountain_loong_soul   { get; private set; }
    public static ActorTrait purgatory_loong_soul  { get; private set; }
    public static ActorTrait skies_loong_soul      { get; private set; }
    public static ActorTrait loong_heart           { get; private set; }
    public static ActorTrait empty_feathered       { get; private set; }
    public static ActorTrait unbreakable_life      { get; private set; }
    public static ActorTrait puppetry_master       { get; private set; }
    public static ActorTrait mysterious_mage       { get; private set; }
    public static ActorTrait servant_master        { get; private set; }
    public static ActorTrait silver_slash          { get; private set; }
    public static ActorTrait longer_then_stronger  { get; private set; }
    public static ActorTrait bastard               { get; private set; }
    public static ActorTrait sign_in               { get; private set; }
    public static ActorTrait stacked_horned_dragon { get; private set; }
    public static ActorTrait emperor_badge         { get; private set; }
    public static ActorTrait emperor_guarder       { get; private set; }
    public static ActorTrait kings_broken_sword    { get; private set; }
    public static ActorTrait survivor_contract     { get; private set; }
    public static ActorTrait end_of_time           { get; private set; }
    public static ActorTrait double_sword          { get; private set; }
    public static ActorTrait undead_war_god        { get; private set; }
    public static ActorTrait flexible_dog          { get; private set; }
    public static ActorTrait treading_snow         { get; private set; }
    public static ActorTrait langli_bai_tiao       { get; private set; }
    public static ActorTrait rise_from_ashes       { get; private set; }
    public static ActorTrait son_of_wall           { get; private set; }
    public static ActorTrait commander_in_chief    { get; private set; }

    public static ActorTrait enhanced_attack       { get; private set; }
    public static ActorTrait super_enhanced_attack { get; private set; }
    public static ActorTrait split_soul_attack     { get; private set; }
    public static ActorTrait quick_attack          { get; private set; }
    public static ActorTrait super_quick_attack    { get; private set; }
    public static ActorTrait run_man               { get; private set; }
    public static ActorTrait runningman            { get; private set; }
    public static ActorTrait stable                { get; private set; }
    public static ActorTrait stable_like_mountain  { get; private set; }
    public static ActorTrait thirsty               { get; private set; }
    public static ActorTrait super_thirsty         { get; private set; }
    public static ActorTrait shield_protect        { get; private set; }
    public static ActorTrait super_shield_protect  { get; private set; }
    public static ActorTrait justice_fist          { get; private set; }
    public static ActorTrait justice_punching      { get; private set; }

    public static ActorTrait cyan_fury         { get; private set; }
    public static ActorTrait thirsty_bait      { get; private set; }
    public static ActorTrait banshee_wail      { get; private set; }
    public static ActorTrait healing_starlight { get; private set; }
    public static ActorTrait mercy_lighthouse  { get; private set; }
    public static ActorTrait spirit_sword      { get; private set; }
    public static ActorTrait snowstorm         { get; private set; }
    public static ActorTrait legion_dawn       { get; private set; }
    public static ActorTrait legion_shield     { get; private set; }
    public static ActorTrait armor_piercing    { get; private set; }

    public static ActorTrait weaken { get; private set; }
    public static ActorTrait yuanhe { get; private set; }

    //public static ActorTrait twin              { get; private set; }
    public static ActorTrait stoic { get; private set; }

    public static ActorTrait echo { get; private set; }
    //public static ActorTrait phantom        { get; private set; }
    //public static ActorTrait active_phantom { get; private set; }

    private static ActorData Copy(ActorData data)
    {
        var copied_data = JsonConvert.DeserializeObject<ActorData>(JsonConvert.SerializeObject(data));
        if (copied_data.custom_data_bool?.dict == null) copied_data.custom_data_bool = null;

        if (copied_data.custom_data_float?.dict == null) copied_data.custom_data_float = null;

        if (copied_data.custom_data_int?.dict == null) copied_data.custom_data_int = null;

        if (copied_data.custom_data_string?.dict == null) copied_data.custom_data_string = null;

        return copied_data;
    }

    private void AddEffectsV101()
    {
    /*
            active_phantom.group_id = ActorTraitGroupLibrary.special.id;
            active_phantom.can_be_given = false;
            active_phantom.special_effect_interval = 12;
            active_phantom.action_special_effect = (actor, _) =>
            {
                actor.a.killHimself(true, AttackType.None, false, false, false);
                return true;
            };
            phantom.SetupSkill(40);
            phantom.action_get_hit = (actor, _, _) =>
            {
                if (actor.a.TraitUnderCooldown(phantom.id) ||
                    !actor.a.TryTakeMana(phantom.GetExtend().mana_cost)) return true;
                actor.a.StartCooldown(phantom);


                Actor summoned = World.world.units.spawnNewUnit(actor.a.asset.id, actor.currentTile, true, 0);
                if (summoned == null) return true;
                ActorData data = Copy(actor.a.data);
                data.id = summoned.data.id;
                data.cityID = "";
                data.clan = "";
                data.culture = "";

                summoned.setData(data);
                summoned.addTrait(active_phantom.id);
                if (summoned.city != null)
                    summoned.joinCity(actor.city);
                summoned.setKingdom(actor.kingdom);
                if (actor.a.getCulture() != null)
                    summoned.setCulture(actor.a.getCulture());
                summoned.event_full_heal = true;
                summoned.updateStats();

                return true;
            };*/
        end_of_time.GetExtend().conditional_basestats = a =>
        {
            if (a.hasTrait(survivor_contract.id))
            {
                return new BaseStats()
                {
                    [S.multiplier_health] = 1,

                };
            }

            return null;
        };
        like_star.GetExtend().conditional_basestats = a => new BaseStats()
        {
            [Stats.dodge.id] = 0.01f * a.stats[S.speed] + 10
        };
        unbreakable_heart.SetupSkill(120);
        unbreakable_heart.action_get_hit = (actor, _, _) =>
        {
            if (actor.a.TraitUnderCooldown(unbreakable_heart.id) ||
                !actor.a.TryTakeMana(unbreakable_heart.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(unbreakable_heart);
            actor.a.AddShield(new ShieldData
            {
                left_time = 60,
                value = actor.getMaxHealth() * 0.5f
            });
            actor.a.ApplyStatusEffectTo(actor, Statuses.active_unbreakable_heart.id);
            actor.a.ApplyStatusEffectTo(actor, Statuses.stoic.id, 60, true);
            return true;
        };
        unbreakable_heart.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(unbreakable_heart.id) ||
                !actor.a.TryTakeMana(unbreakable_heart.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(unbreakable_heart);
            actor.a.AddShield(new ShieldData
            {
                left_time = 60,
                value = actor.getMaxHealth() * 0.5f
            });
            actor.a.ApplyStatusEffectTo(actor, Statuses.active_unbreakable_heart.id);
            actor.a.ApplyStatusEffectTo(actor, Statuses.stoic.id, 60, true);
            return true;
        };
        echo.SetupSkill(5);
        echo.action_special_effect = (obj, _) =>
        {
            Actor a = obj.a;
            if (a.TraitUnderCooldown(echo.id)) return true;

            var trait_to_finish = "";
            foreach (var trait in a.traits)
            {
                if (trait == echo) continue;
                if (a.TraitUnderCooldown(trait.id)) trait_to_finish = trait.id;
            }

            if (string.IsNullOrEmpty(trait_to_finish)) return true;
            if (!a.TryTakeMana(echo.GetExtend().mana_cost)) return true;
            a.StartCooldown(echo, Mathf.Max(echo.GetExtend().cooldown, a.GetCooldown(trait_to_finish)));
            a.FinishCooldown(trait_to_finish);

            return true;
        };
        echo.GetExtend().disabled = true;
        critor.GetExtend().conditional_basestats = a =>
        {
            if (a.stats[S.critical_chance] >= 0.9f)
                return new BaseStats
                {
                    [Stats.life_steal.id] = 2
                };

            return null;
        };
        stoic.SetupSkill(60);
        stoic.action_get_hit = (actor, _, _) =>
        {
            var a = actor.a;
            if (a.TraitUnderCooldown(stoic.id) ||
                !a.TryTakeMana(stoic.GetExtend().mana_cost)) return true;
            a.StartCooldown(stoic);
            a.ApplyStatusEffectTo(a, Statuses.stoic.id);
            return true;
        };
        spirit_sword.SetupSkill(60);
        spirit_sword.action_get_hit = [Hotfixable](actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(spirit_sword.id) ||
                !actor.a.TryTakeMana(spirit_sword.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(spirit_sword);

            int half_edge = 9;
            int x = actor.current_tile.x;
            int y = actor.current_tile.y;
            for (int i = -half_edge; i <= half_edge; i++)
            for (int j = -half_edge; j <= half_edge; j++)
            {
                WorldTile tile = World.world.GetTile(x + i, y + j);
                if (tile == null) continue;
                foreach (Actor unit in tile._units)
                {
                    if (unit == null || !unit.isAlive()) continue;
                    if (unit.hasWeapon()) continue;
                    if (!unit.asset.use_items || !unit.asset.take_items) continue;
                    var item = World.world.items.generateItem(AssetManager.items.get(S_Item.sword_wood),
                        pWho: actor.a.getName());
                    item.data.name = "灵剑";
                    unit.equipment.weapon.setItem(item, unit);
                    unit.setItemSpriteRenderDirty();
                }
            }

            actor.a.ApplyStatusEffectTo(actor, Statuses.active_spirit_sword.id);

            return true;
        }; /*
        twin.SetupSkill(99999999);
        twin.action_special_effect = (actor, _) =>
        {
            var a = actor.a;
            if (a.TraitUnderCooldown(twin.id) ||
                !a.TryTakeMana(twin.GetExtend().mana_cost)) return true;
            a.StartCooldown(twin);

            var summoned = World.world.units.spawnNewUnit(a.asset.id, a.currentTile, true, 0);
            ActorTool.copyUnitToOtherUnit(a, summoned);
            a.setStatsDirty();
            a.updateStats();
            if (a.city != null)
                summoned.joinCity(a.city);
            summoned.setKingdom(a.kingdom);
            summoned.removeTrait(twin.id);

            return true;
        };*/
        survival_of_civilization.SetupSkill(1200);
        survival_of_civilization.can_be_given = false;
        survival_of_civilization.can_be_removed = false;
        survival_of_civilization.action_special_effect = (obj, tile) =>
        {
            var a = obj.a;
            if (a.TraitUnderCooldown(survival_of_civilization.id) ||
                !a.TryTakeMana(survival_of_civilization.GetExtend().mana_cost)) return true;
            a.StartCooldown(survival_of_civilization);

            Actor summoned;
            summoned = World.world.units.spawnNewUnit(Actors.demon_king_1.id, tile, true, pSpawnHeight:0);
            summoned.SetOwner(a);
            summoned.data.name = LM.Get(summoned.asset.name_locale);
            summoned = World.world.units.spawnNewUnit(Actors.demon_king_2.id, tile, true, pSpawnHeight:0);
            summoned.SetOwner(a);
            summoned.data.name = LM.Get(summoned.asset.name_locale);
            summoned = World.world.units.spawnNewUnit(Actors.demon_king_3.id, tile, true, pSpawnHeight:0);
            summoned.SetOwner(a);
            summoned.data.name = LM.Get(summoned.asset.name_locale);
            summoned = World.world.units.spawnNewUnit(Actors.demon_king_4.id, tile, true, pSpawnHeight:0);
            summoned.SetOwner(a);
            summoned.data.name = LM.Get(summoned.asset.name_locale);

            return true;
        };
        brave.base_stats[S.damage] = 10;
        holy_dawn_redemption.SetupSkill(200);
        holy_dawn_redemption.can_be_given = false;
        holy_dawn_redemption.action_attack_target = holy_dawn_action;

        bool holy_dawn_action(BaseSimObject user, BaseSimObject _, WorldTile tile)
        {
            var a = user.a;
            if (a.TraitUnderCooldown(holy_dawn_redemption.id) ||
                !a.TryTakeMana(holy_dawn_redemption.GetExtend().mana_cost)) return true;
            a.StartCooldown(holy_dawn_redemption);

            int half_edge = 15;
            int x = a.current_tile.x;
            int y = a.current_tile.y;
            for (int i = -half_edge; i <= half_edge; i++)
            for (int j = -half_edge; j <= half_edge; j++)
            {
                WorldTile t = World.world.GetTile(x + i, y + j);
                if (t == null) continue;
                foreach (Actor unit in tile._units)
                {
                    if (unit == null || !unit.isAlive()) continue;
                    if (a.kingdom?.isEnemy(unit.kingdom) ?? true) continue;
                    a.ApplyStatusEffectTo(unit, Statuses.active_holy_dawn.id);
                    unit.AddShield(new()
                    {
                        left_time = 100,
                        value = 10000
                    });
                }
            }

            return true;
        }

        unrepentant_refactoring.base_stats[S.health] = 500;
        unbreakable_grip.action_attack_target = (actor, _, _) =>
        {
            actor.a.IncUnbreakableGripCount();
            actor.a.restoreHealth(200);
            return true;
        };
        unbreakable_grip.GetExtend().conditional_basestats = a => new BaseStats
        {
            [S.health] = a.GetUnbreakableGripCount() * 5 * (a.stats[Stats.stats_stacked_effect.id] + 1)
        };
        both_attack_armor.special_effect_interval = 240;
        both_attack_armor.action_special_effect = (actor, tile) =>
        {
            actor.a.UpdateBothAttackArmor();
            return true;
        };
        both_attack_armor.GetExtend().conditional_basestats = a => new BaseStats
        {
            [S.health] = a.GetBothAttackArmor() * 12.5f
        };
        unmovable_like_mountain.action_get_hit = [Hotfixable](actor, _, _) =>
        {
            actor.a.IncUnmovableLikeMountainCount();
            return true;
        };
        unmovable_like_mountain.GetExtend().conditional_basestats = a => new BaseStats
        {
            [S.health] = a.GetUnmovableLikeMountainCount() * 2 * (a.stats[Stats.stats_stacked_effect.id] + 1)
        };
        feast.GetExtend().conditional_basestats = a =>
        {
            if (a.hasTrait(stacked_horned_dragon.id))
                return new BaseStats
                {
                    [S.health] = a.data.kills * 5000 * (a.stats[Stats.stats_stacked_effect.id] + 1)
                };

            return new BaseStats
            {
                [S.health] = a.data.kills                 * 5000 * (a.stats[Stats.stats_stacked_effect.id] + 1),
                [Stats.addition_armor.id] = -a.data.kills * 5    * (a.stats[Stats.stats_stacked_effect.id] + 1),
                // ReSharper disable once PossibleLossOfFraction
                [S.scale] = a.data.kills / 20 * 0.01f * (a.stats[Stats.stats_stacked_effect.id] + 1)
            };
        };
        stand_firm.GetExtend().conditional_basestats = a =>
        {
            var hp = a.data.health / (float)a.getMaxHealth();
            if (hp > 0.8f) return null;

            var life_steal = 0f;
            if (hp > 0.5f)
                life_steal = 0.05f;
            else if (hp > 0.15f)
                life_steal = 0.1f;
            else
                life_steal = 0.4f;

            return new BaseStats
            {
                [Stats.life_steal.id] = life_steal
            };
        };
        //age_cicada.SetupSkill(100 * 12 * 5, 0);
        gin_cup.GetExtend().conditional_basestats = a =>
        {
            if (a.city == null) return null;
            var gold = a.city.amount_gold;
            var stats = new BaseStats
            {
                [S.attack_speed] = gold
            };
            if (gold > 100) stats[Stats.life_steal.id] = 0.1f;

            return stats;
        };
        mountain_loong_soul.special_effect_interval = 10;
        mountain_loong_soul.action_special_effect = (actor, tile) =>
        {
            //if (actor.a.GetLastAttackTime() + 10 < (float)MapBox.instance.getCurWorldTime()) return true;
            actor.a.AddShield(new ShieldData
            {
                value = actor.a.hasTrait(loong_heart.id) ? 5000 : 1000,
                left_time = 10
            });
            return true;
        };
        purgatory_loong_soul.base_stats[S.multiplier_damage] = 1;
        purgatory_loong_soul.base_stats[Stats.life_steal.id] = 0.03f;
        purgatory_loong_soul.action_attack_target = (obj, target, tile) =>
        {
            var a = obj.a;
            if (a.hasTrait(loong_heart.id))
            {
                target.getHit(target.stats[S.health] * 0.01f, pAttackType: AttackType.None, pAttacker: obj,
                    pSkipIfShake: false);
                return true;
            }

            return false;
        };
        skies_loong_soul.base_stats[S.speed] = 200;
        skies_loong_soul.base_stats[Stats.dodge.id] = 20;
        skies_loong_soul.GetExtend().conditional_basestats = a =>
        {
            if (a.hasTrait(loong_heart.id))
            {
                return new BaseStats()
                {
                    [Stats.dodge.id] = 25
                };
            }

            return null;
        };
        loong_heart.base_stats[S.multiplier_health] = 0.8f;
        loong_heart.GetExtend().conditional_basestats = a =>
        {
            var loong_soul_count = a.GetLoongSoulCount();
            if (loong_soul_count == 0) return null;
            return new BaseStats
            {
                [S.multiplier_health] = 10 * loong_soul_count,
                [S.multiplier_damage] = 10 * loong_soul_count
            };
        };
        loong_heart.special_effect_interval = 5;
        loong_heart.action_special_effect = (actor, tile) =>
        {
            actor.a.restoreHealth(10000 * actor.a.GetLoongSoulCount());
            return true;
        };
        loong_heart.action_death = (actor, tile) =>
        {
            var a = actor.a;
            if (a.GetLoongSoulCount() < 2) return false;

            if (a.asset.id == "dragon") return false;
            var dragon = World.world.units.spawnNewUnit("dragon", actor.current_tile);
            foreach (var trait in a.traits)
            {
                dragon.addTrait(trait);
            }

            dragon.OverwriteStats(a.stats);
            dragon.updateStats();
            return true;
        };
        empty_feathered.base_stats[S.health] = 1000;
        unbreakable_life.GetExtend().conditional_basestats = a =>
        {
            var child_count = a.current_children_count;
            if (child_count == 0) return null;
            return new BaseStats
            {
                [S.health] = 800 * child_count * (a.stats[Stats.stats_stacked_effect.id] + 1),
                [S.damage] = 100 * child_count * (a.stats[Stats.stats_stacked_effect.id] + 1),
                [S.armor] = 0.2f * child_count * (a.stats[Stats.stats_stacked_effect.id] + 1)
            };
        };
        mysterious_mage.GetExtend().final_basestats[S.multiplier_health] = -0.25f;
        mysterious_mage.GetExtend().final_basestats[Stats.multiplier_armor.id] = -0.25f;
        servant_master.base_stats[S.health] = 1000;
        silver_slash.base_stats[S.multiplier_damage] = 9;
        silver_slash.base_stats[Stats.life_steal.id] = 0.02f;
        silver_slash.base_stats[S.armor] = -75;
        longer_then_stronger.base_stats[S.lifespan] = 999999;
        longer_then_stronger.GetExtend().conditional_basestats = a =>
        {
            var age = a.data.getAge();

            return new BaseStats
            {
                [S.damage] = age * 5f   * (a.stats[Stats.stats_stacked_effect.id] + 1),
                [S.speed] = age  * 0.5f * (a.stats[Stats.stats_stacked_effect.id] + 1)
            };
        };
        bastard.base_stats[S.lifespan] = 999999;
        bastard.GetExtend().conditional_basestats = a =>
        {
            var age = a.data.getAge();
            return new BaseStats
            {
                [S.health] = age * 200f  * (a.stats[Stats.stats_stacked_effect.id] + 1),
                [S.armor] = age  * 0.05f * (a.stats[Stats.stats_stacked_effect.id] + 1)
            };
        };
        sign_in.base_stats[S.health] = -3000;
        sign_in.base_stats[S.lifespan] = 999999;
        sign_in.SetupSkill(6000, 0);
        sign_in.GetExtend().disabled = true;
        sign_in.can_be_given = false;
        stacked_horned_dragon.base_stats[S.health] = 400;
        stacked_horned_dragon.base_stats[Stats.stats_stacked_effect.id] = 20;
        emperor_badge.base_stats[S.health] = 1000;
        emperor_badge.GetExtend().conditional_basestats = a =>
        {
            if (a.HasEmperorBadge())
            {
                a.addTrait(emperor_guarder.id);
                return new BaseStats
                {
                    [S.health] = 150000,
                    [S.armor] = 50,
                    [Stats.life_steal.id] = 0.1f
                };
            }

            return null;
        };
        emperor_guarder.can_be_given = false;
        emperor_guarder.can_be_removed = false;
        emperor_guarder.SetupSkill(240);
        emperor_guarder.action_special_effect = (obj, tile) =>
        {
            var a = obj.a;
            if (a.TraitUnderCooldown(emperor_guarder.id) ||
                !a.TryTakeMana(emperor_guarder.GetExtend().mana_cost)) return true;
            a.StartCooldown(emperor_guarder);

            Actor summoned = World.world.units.spawnNewUnit(obj.a.asset.id, tile, true, pSpawnHeight:0);
            if (summoned == null) return true;
            summoned.addTrait(Traits.summoned.id);
            summoned.SetOwner(a);
            var stats = summoned.stats;
            stats[S.health] = 100000;
            stats[S.damage] = 1;
            summoned.setProfession(UnitProfession.Warrior);
            summoned.OverwriteStats(stats);
            summoned.setStatsDirty();
            summoned.event_full_stats = true;

            return true;
        };
        kings_broken_sword.base_stats[S.damage] = 100;
        kings_broken_sword.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.data.kills > 10) target.getHit(target.stats[S.health] * 0.02f, pAttacker: actor, pSkipIfShake:false);

            return true;
        };
        kings_broken_sword.GetExtend().conditional_basestats = a =>
        {
            if (a.data.kills > 100)
                return new BaseStats
                {
                    [Stats.life_steal.id] = 0.05f
                };

            return null;
        };
        survivor_contract.SetupSkill(100);
        survivor_contract.special_effect_interval = 1;
        survivor_contract.action_special_effect = (actor, tile) =>
        {
            if (actor.getData().health / actor.stats[S.health] > 0.2f) return false;
            if (actor.a.TraitUnderCooldown(survivor_contract.id) ||
                !actor.a.TryTakeMana(survivor_contract.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(survivor_contract);
            actor.a.IncSurvivorContractCount();
            return true;
        };
        survivor_contract.GetExtend().conditional_basestats = a =>
        {
            var count = System.Math.Min(a.GetSurvivorContractCount(), 5); // 增加上限至5层
            if (count == 0) return null;
            return new BaseStats
            {
                [S.multiplier_health] = count * 0.2f * (a.stats[Stats.stats_stacked_effect.id] + 1),
            };
        };
        double_sword.base_stats[S.multiplier_damage] = -0.5f;
        double_sword.base_stats[S.attack_speed] = 200;
        double_sword.action_attack_target = (actor, target, _) =>
        {
            target.getHit(1000, pAttackType: AttackType.None, pAttacker: actor, pSkipIfShake:false);
            return true;
        };
        undead_war_god.action_special_effect = (actor, _) =>
        {
            if (actor.a.HasUndeadWarGodTriggerd() && !actor.hasStatus(Statuses.active_undead_war_god.id))
            {
                actor.a.checkCallbacksOnDeath();
                actor.a.die();
                return true;
            }

            return true;
        };
        undead_war_god.action_attack_target = (actor, target, _) =>
        {
            if (!actor.hasStatus(Statuses.active_undead_war_god.id)) return true;
            target.getHit(500, false, AttackType.None, actor, false);
            return true;
        };
        // 基础属性：20攻击速度，20移动速度
        flexible_dog.base_stats[S.attack_speed] = 20;
        flexible_dog.base_stats[S.speed] = 20;
        
        // 动态属性：每击杀10人增加10攻击速度和10移动速度（最高100点）
        // 当攻击速度达到100点时，额外获得50%暴击率和100%暴击伤害
        flexible_dog.GetExtend().conditional_basestats = a =>
        {
            // 计算击杀奖励（每10人）
            var killCount = a.data.kills;
            var bonusLevel = Mathf.FloorToInt(killCount / 10);
            var bonusAttackSpeed = Mathf.Min(bonusLevel * 10, 100); // 额外获得上限100点
            var bonusSpeed = Mathf.Min(bonusLevel * 10, 100);
            
            var stats = new BaseStats
            {
                [S.attack_speed] = bonusAttackSpeed,
                [S.speed] = bonusSpeed
            };
            
            // 当攻击速度达到100点时，添加暴击属性
            if (bonusAttackSpeed >= 120)
            {
                stats[S.critical_chance] = 0.5f; // 50%暴击率
                stats[S.critical_damage_multiplier] = 1f; // 100%暴击伤害
            }
            
            return stats;
        };
        treading_snow.base_stats[S.attack_speed] = 30;
        treading_snow.GetExtend().conditional_basestats = a =>
        {
            if (a.current_tile.isFrozen() || World.world_era.global_freeze_world)
                return new BaseStats
                {
                    [Stats.dodge.id] = 95
                };

            return null;
        };
        langli_bai_tiao.base_stats[S.health] = 500;
        langli_bai_tiao.special_effect_interval = 1;
        langli_bai_tiao.action_special_effect = (actor, _) =>
        {
            if (actor.current_tile.Type.ocean)
            {
                actor.a.AddInvincible(2);
                actor.a.restoreHealth(1000);
            }

            return true;
        };
        rise_from_ashes.base_stats[S.health] = 100;
        rise_from_ashes.GetExtend().SetConditionalBaseStats(new BaseStats
        {
            [Stats.addition_armor.id] = 50
        }, a => a.hasStatus("burning"));
        rise_from_ashes.special_effect_interval = 1;
        rise_from_ashes.action_special_effect = (actor, _) =>
        {
            actor.a.restoreHealth(2000);
            return true;
        };
        son_of_wall.GetExtend().SetConditionalBaseStats(new BaseStats
        {
            [S.multiplier_health] = 5,
            [S.multiplier_damage] = 5
        }, a => a.current_tile.zone.city?.kingdom == a.kingdom);
        commander_in_chief.base_stats[S.health] = 2000;
        commander_in_chief.GetExtend().conditional_basestats = a =>
            a.hasTrait(legion_dawn.id) || a.hasTrait((legion_shield.id))
                ? new BaseStats()
                {
                    [S.health] = 20000
                }
                : null;
        commander_in_chief.action_special_effect = (actor, _) =>
        {
            if (actor.a.hasTrait(legion_dawn.id) || actor.a.hasTrait((legion_shield.id)))
            {
                actor.a.addTrait(holy_dawn_redemption.id);
                return true;
            }

            return true;
        };
        enhanced_attack.SetupSkill(5, 5);
        enhanced_attack.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(enhanced_attack.id) ||
                !actor.a.TryTakeMana(enhanced_attack.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(enhanced_attack);
            target.getHit(5 * actor.stats[S.damage], pAttacker: actor, pSkipIfShake:false);
            return true;
        };
        super_enhanced_attack.SetupSkill(5, 5);
        super_enhanced_attack.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(super_enhanced_attack.id) || actor.getData().health < 200 ||
                !actor.a.TryTakeMana(super_enhanced_attack.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(super_enhanced_attack);
            target.getHit(10 * actor.stats[S.damage], pAttacker: actor, pSkipIfShake:false);
            actor.a.getHit(200, true, pAttackType: AttackType.None, pSkipIfShake: false);
            return true;
        };
        split_soul_attack.SetupSkill(5, 5);
        split_soul_attack.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(split_soul_attack.id) ||
                !actor.a.TryTakeMana(split_soul_attack.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(split_soul_attack);
            target.getHit(actor.stats[S.damage], pAttackType: AttackType.None, pAttacker: actor, pSkipIfShake:false);
            return true;
        };
        quick_attack.SetupSkill(30);
        quick_attack.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(quick_attack.id) ||
                !actor.a.TryTakeMana(quick_attack.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(quick_attack);
            actor.a.ApplyStatusEffectTo(actor, Statuses.active_quick_attack.id);
            return true;
        };
        super_quick_attack.SetupSkill(30);
        super_quick_attack.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(super_quick_attack.id) ||
                !actor.a.TryTakeMana(super_quick_attack.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(super_quick_attack);
            actor.a.ApplyStatusEffectTo(actor, Statuses.active_super_quick_attack.id);
            return true;
        };
        run_man.SetupSkill(30);
        run_man.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(run_man.id) ||
                !actor.a.TryTakeMana(run_man.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(run_man);
            actor.a.ApplyStatusEffectTo(actor, Statuses.active_run_man.id);
            return true;
        };
        run_man.action_get_hit = (actor, attacker, _) =>
        {
            if (actor.a.TraitUnderCooldown(run_man.id) ||
                !actor.a.TryTakeMana(run_man.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(run_man);
            actor.a.ApplyStatusEffectTo(actor, Statuses.active_run_man.id);
            return true;
        };
        runningman.SetupSkill(30);
        runningman.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(runningman.id) || actor.getData().health <= 200 ||
                !actor.a.TryTakeMana(runningman.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(runningman);
            actor.a.ApplyStatusEffectTo(actor, Statuses.active_runningman.id);
            actor.a.getHit(200, true, pAttackType: AttackType.None, pSkipIfShake: false);
            return true;
        };
        runningman.action_get_hit = (actor, attacker, _) =>
        {
            if (actor.a.TraitUnderCooldown(runningman.id) || actor.getData().health <= 200 ||
                !actor.a.TryTakeMana(runningman.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(runningman);
            actor.a.ApplyStatusEffectTo(actor, Statuses.active_runningman.id);
            actor.a.getHit(200, true, pAttackType: AttackType.None, pSkipIfShake: false);
            return true;
        };
        stable.SetupSkill(60);
        stable.action_get_hit = (actor, attacker, _) =>
        {
            if (actor.a.TraitUnderCooldown(stable.id) ||
                !actor.a.TryTakeMana(stable.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(stable);
            actor.a.ApplyStatusEffectTo(actor, Statuses.active_stable.id);
            return true;
        };
        stable_like_mountain.SetupSkill(60);
        stable_like_mountain.action_get_hit = (actor, attacker, _) =>
        {
            if (actor.a.TraitUnderCooldown(stable_like_mountain.id) ||
                !actor.a.TryTakeMana(stable_like_mountain.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(stable_like_mountain);
            actor.a.ApplyStatusEffectTo(actor, Statuses.active_stable_like_mountain.id);
            return true;
        };
        thirsty.SetupSkill(20, 50);
        thirsty.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(thirsty.id) ||
                !actor.a.TryTakeMana(thirsty.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(thirsty);
            actor.a.ApplyStatusEffectTo(actor, Statuses.active_thirsty.id);
            return true;
        };
        super_thirsty.SetupSkill(20, 100);
        super_thirsty.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(super_thirsty.id) || actor.getData().health <= 500 ||
                !actor.a.TryTakeMana(super_thirsty.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(super_thirsty);
            actor.a.ApplyStatusEffectTo(actor, Statuses.active_super_thirsty.id);
            return true;
        };
        shield_protect.SetupSkill(20, 5);
        shield_protect.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(shield_protect.id) ||
                !actor.a.TryTakeMana(shield_protect.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(shield_protect);
            actor.a.AddShield(new ShieldData
            {
                left_time = 10, value = actor.stats[S.health] * 0.1f
            });
            return true;
        };
        super_shield_protect.SetupSkill(20, 5);
        super_shield_protect.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(super_shield_protect.id) || actor.getData().health <= 500 ||
                !actor.a.TryTakeMana(super_shield_protect.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(super_shield_protect);
            actor.a.AddShield(new ShieldData
            {
                left_time = 10, value = actor.stats[S.health] * 0.2f
            });
            return true;
        };
        justice_fist.SetupSkill(30);
        justice_fist.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(justice_fist.id) ||
                !actor.a.TryTakeMana(justice_fist.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(justice_fist);
            var half_edge = 6;
            var x = target.current_tile.x;
            var y = target.current_tile.y;
            var damage = actor.stats[S.damage];
            for (var i = -half_edge; i <= half_edge; i++)
            for (var j = -half_edge; j <= half_edge; j++)
            {
                WorldTile tile = World.world.GetTile(x + i, y + j);
                if (tile == null) continue;
                foreach (Actor unit in tile._units)
                {
                    if (unit == null || !unit.isAlive()) continue;
                    if (actor.kingdom?.isEnemy(unit.kingdom) ?? true)
                        unit.getHit(damage, true, pAttackType: AttackType.Other, pAttacker: actor, pSkipIfShake:false);
                }
            }

            MapAction.damageWorld(target.current_tile, half_edge, Terraforms.intentional_punching_terra, actor);
            return true;
        };
        justice_punching.SetupSkill(120);
        justice_punching.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(justice_punching.id) ||
                !actor.a.TryTakeMana(justice_punching.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(justice_punching);
            var half_edge = 12;
            var x = target.current_tile.x;
            var y = target.current_tile.y;
            var damage = actor.stats[S.damage];
            for (var i = -half_edge; i <= half_edge; i++)
            for (var j = -half_edge; j <= half_edge; j++)
            {
                WorldTile tile = World.world.GetTile(x + i, y + j);
                if (tile == null) continue;
                foreach (Actor unit in tile._units)
                {
                    if (unit == null || !unit.isAlive()) continue;
                    if (actor.kingdom?.isEnemy(unit.kingdom) ?? true)
                        unit.getHit(damage, true, pAttackType: AttackType.Other, pAttacker: actor, pSkipIfShake:false);
                }
            }

            MapAction.damageWorld(target.current_tile, half_edge, Terraforms.intentional_punching_terra, actor);
            return true;
        };
        cyan_fury.SetupSkill(40);
        cyan_fury.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(cyan_fury.id) ||
                !actor.a.TryTakeMana(cyan_fury.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(cyan_fury);

            var target_size = target.stats[S.size];
            for (var i = 0; i < 10; i++)
            {
                World.world.projectiles.spawn(actor, target, S_Projectile.firebomb, actor.current_position,
                    target.current_position + new Vector2(Randy.randomFloat(-target_size, target_size),
                        Randy.randomFloat(-target_size, target_size)) * 2, target.getHeight());
            }

            actor.a.ApplyStatusEffectTo(actor, Statuses.active_cyan_fury.id);
            return true;
        };
        thirsty_bait.SetupSkill(5, 5);
        thirsty_bait.action_attack_target = (actor, target, _) =>
        {
            //if (target.hasStatus(Statuses.active_thirsty_bait.id)) actor.a.restoreHealth(actor.getMaxHealth() / 10);

            if (actor.a.TraitUnderCooldown(thirsty_bait.id) ||
                !actor.a.TryTakeMana(thirsty_bait.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(thirsty_bait);
            actor.a.ApplyStatusEffectTo(target, Statuses.active_thirsty_bait.id);
            actor.a.ApplyStatusEffectTo(actor, Statuses.active_thirsty_bait_source.id);
            return true;
        };
        banshee_wail.SetupSkill(20);
        banshee_wail.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(banshee_wail.id) ||
                !actor.a.TryTakeMana(banshee_wail.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(banshee_wail);
            var half_edge = 6;
            var x = actor.current_tile.x;
            var y = actor.current_tile.y;
            for (var i = -half_edge; i <= half_edge; i++)
            for (var j = -half_edge; j <= half_edge; j++)
            {
                WorldTile tile = World.world.GetTile(x + i, y + j);
                if (tile == null) continue;
                foreach (Actor unit in tile._units)
                {
                    if (unit == null || !unit.isAlive()) continue;
                    if (actor.kingdom?.isEnemy(unit.kingdom) ?? true)
                        actor.a.ApplyStatusEffectTo(unit, Statuses.dizzy.id, 1, true);
                }
            }

            return true;
        };
        healing_starlight.SetupSkill(60);
        healing_starlight.action_get_hit = (actor, attacker, _) =>
        {
            if (actor.a.TraitUnderCooldown(healing_starlight.id) ||
                !actor.a.TryTakeMana(healing_starlight.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(healing_starlight);
            var half_edge = 3;
            var x = actor.current_tile.x;
            var y = actor.current_tile.y;
            var health_restore = (int)actor.stats[S.damage];
            for (var i = -half_edge; i <= half_edge; i++)
            for (var j = -half_edge; j <= half_edge; j++)
            {
                WorldTile tile = World.world.GetTile(x + i, y + j);
                if (tile == null) continue;
                foreach (Actor unit in tile._units)
                {
                    if (unit == null || !unit.isAlive()) continue;
                    if (actor.kingdom?.isEnemy(unit.kingdom) ?? true) continue;
                    unit.restoreHealth(health_restore);
                }
            }

            return true;
        };
        mercy_lighthouse.SetupSkill(60);
        mercy_lighthouse.action_get_hit = (actor, attacker, _) =>
        {
            if (actor.a.TraitUnderCooldown(mercy_lighthouse.id) ||
                !actor.a.TryTakeMana(mercy_lighthouse.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(mercy_lighthouse);
            var half_edge = 3;
            var x = actor.current_tile.x;
            var y = actor.current_tile.y;
            var health_restore = (int)actor.stats[S.damage] * 10;
            for (var i = -half_edge; i <= half_edge; i++)
            for (var j = -half_edge; j <= half_edge; j++)
            {
                WorldTile tile = World.world.GetTile(x + i, y + j);
                if (tile == null) continue;
                foreach (Actor unit in tile._units)
                {
                    if (unit == null || !unit.isAlive()) continue;
                    unit.restoreHealth(health_restore);
                    unit.addStatusEffect("enchanted");
                    unit.removeTrait("madness");
                }
            }

            return true;
        };
        snowstorm.SetupSkill(30);
        snowstorm.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(snowstorm.id) ||
                !actor.a.TryTakeMana(snowstorm.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(snowstorm);
            var half_edge = 6;
            var x = target.current_tile.x;
            var y = target.current_tile.y;
            for (var i = -half_edge; i <= half_edge; i++)
            for (var j = -half_edge; j <= half_edge; j++)
            {
                WorldTile tile = World.world.GetTile(x + i, y + j);
                if (tile == null) continue;
                foreach (Actor unit in tile._units)
                {
                    if (unit == null || !unit.isAlive()) continue;
                    if (actor.kingdom?.isEnemy(unit.kingdom) ?? true)
                        actor.a.ApplyStatusEffectTo(unit, "frozen", 1, true);
                }
            }

            return true;
        };
        legion_dawn.SetupSkill(20);
        legion_dawn.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(legion_dawn.id) ||
                !actor.a.TryTakeMana(legion_dawn.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(legion_dawn);
            var half_edge = 3;
            var x = actor.current_tile.x;
            var y = actor.current_tile.y;
            for (var i = -half_edge; i <= half_edge; i++)
            for (var j = -half_edge; j <= half_edge; j++)
            {
                WorldTile tile = World.world.GetTile(x + i, y + j);
                if (tile == null) continue;
                foreach (Actor unit in tile._units)
                {
                    if (unit == null || !unit.isAlive()) continue;
                    if (actor.kingdom?.isEnemy(unit.kingdom) ?? true) continue;
                    actor.a.ApplyStatusEffectTo(unit, Statuses.active_legion_dawn.id);
                }
            }

            return true;
        };
        legion_shield.SetupSkill(20);
        legion_shield.action_get_hit = (actor, _, _) =>
        {
            if (actor.a.TraitUnderCooldown(legion_shield.id) ||
                !actor.a.TryTakeMana(legion_shield.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(legion_shield);
            var half_edge = 3;
            var x = actor.current_tile.x;
            var y = actor.current_tile.y;
            for (var i = -half_edge; i <= half_edge; i++)
            for (var j = -half_edge; j <= half_edge; j++)
            {
                WorldTile tile = World.world.GetTile(x + i, y + j);
                if (tile == null) continue;
                foreach (Actor unit in tile._units)
                {
                    if (unit == null || !unit.isAlive()) continue;
                    if (actor.kingdom?.isEnemy(unit.kingdom) ?? true) continue;
                    actor.a.ApplyStatusEffectTo(unit, Statuses.active_legion_shield.id);
                }
            }

            return true;
        };
        armor_piercing.SetupSkill(20);
        armor_piercing.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(armor_piercing.id) ||
                !actor.a.TryTakeMana(armor_piercing.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(armor_piercing);
            var half_edge = 3;
            var x = actor.current_tile.x;
            var y = actor.current_tile.y;
            for (var i = -half_edge; i <= half_edge; i++)
            for (var j = -half_edge; j <= half_edge; j++)
            {
                WorldTile tile = World.world.GetTile(x + i, y + j);
                if (tile == null) continue;
                foreach (Actor unit in tile._units)
                {
                    if (unit == null || !unit.isAlive()) continue;
                    if (actor.kingdom?.isEnemy(unit.kingdom) ?? true)
                        actor.a.ApplyStatusEffectTo(unit, Statuses.active_armor_piercing.id);
                }
            }

             return true;
        };
        weaken.SetupSkill(20);
        weaken.action_get_hit = (actor, _, _) =>
{
    if (actor.a.TraitUnderCooldown(weaken.id) ||
        !actor.a.TryTakeMana(weaken.GetExtend().mana_cost)) return true;
    actor.a.StartCooldown(weaken);
    var half_edge = 3;
    var x = actor.current_tile.x;
    var y = actor.current_tile.y;
    for (var i = -half_edge; i <= half_edge; i++)
    for (var j = -half_edge; j <= half_edge; j++)
    {
        WorldTile tile = World.world.GetTile(x + i, y + j);
        if (tile == null) continue;
        foreach (Actor unit in tile._units)
        {
            if (unit == null || !unit.isAlive()) continue;
            if (actor.kingdom?.isEnemy(unit.kingdom) ?? true)
                actor.a.ApplyStatusEffectTo(unit, Statuses.active_weakened.id);
                }
            }

             return true;
        };
        yuanhe.GetExtend().conditional_basestats = a =>
        {
            // 假设年龄是ActorData的age字段，直接访问
            var age = a.data.getAge();
            var damage_bonus = (float)(a.data.kills * 2f);
            var health_bonus = (float)(age * 20f);
            return new BaseStats
            {
                
                // 伤害加成最多不超过 500
                [S.damage] = Mathf.Min(damage_bonus, 500f),
                // 生命加成最多不超过 20000
                [S.health] = Mathf.Min(health_bonus, 10000f)
            };
        };
    }
} // 类结尾花括号后去掉分号