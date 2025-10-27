using System.Collections.Generic;
using strings;
using UnityEngine;

namespace CustomModT001;

public partial class Traits
{
    public static ActorTrait attack_stilling { get; private set; }
    public static ActorTrait defence_stilling { get; private set; }
    public static ActorTrait thorn_shell { get; private set; }
    public static ActorTrait resilience { get; private set; }
    public static ActorTrait nation_fortune { get; private set; }
    public static ActorTrait destined { get; private set; }
    public static ActorTrait single_one { get; private set; }
    public static ActorTrait war_ashes { get; private set; }
    public static ActorTrait grass_can_talk { get; private set; }
    public static ActorTrait houseback { get; private set; }
    public static ActorTrait summoner { get; private set; }
    public static ActorTrait snow_struggling { get; private set; }
    public static ActorTrait countless_trials { get; private set; }
    public static ActorTrait soul_return { get; private set; }
    public static ActorTrait werewolf_appearance { get; private set; }
    public static ActorTrait diligent { get; private set; }
    private void AddEffectsV102()
    {
        attack_stilling.base_stats[S.damage] = 20000;
        defence_stilling.base_stats[S.armor] = 10;
        resilience.GetExtend().conditional_basestats = a => new BaseStats()
        {
            [Stats.control_get.id] = -a.stats[S.intelligence] * 2
        };
        nation_fortune.special_effect_interval = 10;
        nation_fortune.action_special_effect = (target, tile) =>
        {
            if (target.kingdom == null || target.kingdom.units.Count <= 1000) return false;

            target.a.restoreHealth(500);
            return true;
        };
        nation_fortune.GetExtend().conditional_basestats = a =>
        {
            if (a.kingdom == null) return null;
            var pop = a.kingdom.units.Count;
            var stats = new BaseStats();

            stats[S.health] = 400 * pop;
            stats[S.damage] = pop;
            if (pop > 1000)
            {
                stats[Stats.addition_armor.id] = 0.8f;
                stats[Stats.life_steal.id] = 0.05f;
            }
            else if (pop > 500)
            {
                stats[Stats.addition_armor.id] = 0.25f;
                stats[Stats.life_steal.id] = 0.02f;
            }

            return stats;
        };
        single_one.GetExtend().conditional_basestats = a =>
        {
            var kingdom = a.kingdom;
            if (kingdom == null) return null;
            if (kingdom.units.Count >= 100) return null;
            return new BaseStats()
            {
                [S.multiplier_damage] = 12,
                [S.multiplier_health] = 12,
                [Stats.life_steal.id] = 0.05f
            };
        };
        single_one.special_effect_interval = 1;
        single_one.action_special_effect = (obj, tile) =>
        {
            if (obj.kingdom == null) return false;
            var count = obj.kingdom.units.Count;
            if (count >= 1000)
            {
                var health_to_lose = Mathf.Min(100, obj.getData().health - obj.stats[S.health] * 0.05f);
                if (health_to_lose < 0) return false;
                obj.getHit(health_to_lose, false, AttackType.None, null, false);
                return true;
            }

            if (count >= 100)
            {
                obj.a.restoreHealth(100);
            }
            else
            {
                obj.a.restoreHealth(50);
            }

            return true;
        };
        /*
        war_ashes.special_effect_interval = 1;
        war_ashes.action_special_effect = (obj, _) =>
        {
            int new_dead_count = 0;
            int half_edge = 7;
            int x = obj.currentTile.x;
            int y = obj.currentTile.y;
            for (int i = -half_edge; i <= half_edge; i++)
            for (int j = -half_edge; j <= half_edge; j++)
            {
                WorldTile tile = World.world.GetTile(x + i, y + j);
                if (tile == null) continue;
                foreach (Actor unit in tile._units)
                {
                    if (unit == null || unit.isAlive()) continue;
                    if (World.world.units._to_destroy_objects.Contains(unit))
                    {
                        new_dead_count++;
                    }
                }
            }

            obj.a.IncWarAshesCount(new_dead_count);
            return true;
        };*/
        war_ashes.GetExtend().conditional_basestats = a =>
        {
            var count = a.GetWarAshesCount();
            return new BaseStats()
            {
                [S.health] = 50 * count,
                [S.damage] = 0.1f * count,
                [S.armor] = 0.001f * count
            };
        };
        grass_can_talk.action_special_effect = (obj, _) =>
        {
            int half_edge = 9;
            int x = obj.current_tile.x;
            int y = obj.current_tile.y;
            for (int i = -half_edge; i <= half_edge; i++)
            for (int j = -half_edge; j <= half_edge; j++)
            {
                WorldTile tile = World.world.GetTile(x + i, y + j);
                if (tile == null) continue;
                if (tile.building == null || !tile.building.isAlive()) continue;
                if (tile.building.asset.building_type == BuildingType.Building_Tree)
                {
                    obj.a.SetGrassCanTalkState(true);
                    obj.a.restoreHealth(10);
                    return true;
                }
            }
            obj.a.SetGrassCanTalkState(false);
            return true;
        };
        grass_can_talk.special_effect_interval = 1;
        grass_can_talk.GetExtend().conditional_basestats = a =>
        {
            if (a.GetGrassCanTalkState())
            {
                return new BaseStats()
                {
                    [Stats.dodge.id] = 25,
                    [Stats.addition_armor.id] = 0.25f,
                    [S.armor] = 25
                };
            }

            return null;
        };
        destined.GetExtend().conditional_basestats = a =>
        {
            var talent = a.GetTalent();
            var stats = new BaseStats();
            if (talent > 10000)
            {
                stats[Stats.addition_armor.id] = 0.5f;
            }

            stats[S.health] = 100 * talent;
            stats[S.damage] = 0.05f * talent;
            stats[S.armor] = 0.001f * talent;
            return stats;
        };
        houseback.SetupSkill(800);
        houseback.action_attack_target = (actor, target, _) =>
        {
            if (actor.a.TraitUnderCooldown(houseback.id) ||
                !actor.a.TryTakeMana(houseback.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(houseback);

            var stats = new BaseStats();
            stats.mergeStats(AssetManager.actor_library.get(SA.living_house).base_stats);
            stats[S.health] = 100000;
            stats[S.damage] = 1000;
            int half_edge = 5;
            int x = actor.current_tile.x;
            int y = actor.current_tile.y;
            for (int i = -half_edge; i <= half_edge; i++)
            for (int j = -half_edge; j <= half_edge; j++)
            {
                WorldTile tile = World.world.GetTile(x + i, y + j);
                if (tile == null) continue;
                if (tile.building == null || !tile.building.isAlive() || tile.building.isRuin() || tile.building.isUnderConstruction()) continue;
                if (tile.building.asset.can_be_living_house)
                {
                    Actor summoned_actor = World.world.units.createNewUnit(SA.living_house, tile, pSpawnHeight:0f);
                    summoned_actor.data.set("special_sprite_id", tile.building.asset.id);
                    summoned_actor.data.set("special_sprite_index", tile.building.animData_index);
                    tile.building.removeBuildingFinal();
                    summoned_actor.startColorEffect(ActorColorEffect.White);
                    summoned_actor.addTrait(summoned.id);
                    summoned_actor.SetSummonSource(houseback.id);
                    summoned_actor.SetOwner(actor.a);
                    summoned_actor.OverwriteStats(stats);
                    summoned_actor.event_full_stats = true;
                    summoned_actor.setStatsDirty();
                }
            }
            
            return true;
        };
        snow_struggling.SetupSkill(400);
        snow_struggling.action_special_effect = (actor, tile) =>
        {
            if (actor.a.TraitUnderCooldown(snow_struggling.id) ||
                !actor.a.TryTakeMana(snow_struggling.GetExtend().mana_cost)) return true;
            actor.a.StartCooldown(snow_struggling);

            for (int i = 0; i < 1; i++)
            {
                var new_actor = World.world.units.createNewUnit(Actors.ice_demon_summoned.id, tile);
                new_actor.SetSummonSource(snow_struggling.id);
                new_actor.SetOwner(actor.a);
                var stats = new BaseStats();
                stats.mergeStats(new_actor.stats);
                stats[S.health] = actor.getMaxHealth() *0.25f;
                new_actor.OverwriteStats(stats);
                new_actor.event_full_stats = true;
                new_actor.setStatsDirty();
            }

            return true;
        };
        countless_trials.action_get_hit = (trait_owner, attacker, _) =>
        {
            trait_owner.a.UpdateCountlessTrials();
            return false;
        };
        countless_trials.GetExtend().conditional_basestats = a =>
        {
            return new BaseStats()
            {
                [S.armor] = Mathf.Max(80f, a.GetCountlessTrials())
            };
        };
        soul_return.base_stats[S.health] = 1000;
        soul_return.GetExtend().conditional_basestats = a =>
        {
            var count = a.GetSoulReturnCount();
            return new BaseStats()
            {
                [S.health] = 8000 * count,
                [S.damage] = 5 * count,
                [S.armor] = 0.5f * count
            };
        };
        werewolf_appearance.special_effect_interval = 10;
        werewolf_appearance.action_special_effect = (obj, tile) =>
        {
            if (werewolf_appearance_eras.Contains(World.world_era))
            {
                obj.a.restoreHealth(500);
            }

            return true;
        };
        werewolf_appearance.GetExtend().conditional_basestats = a =>
        {
            if (werewolf_appearance_eras.Contains(World.world_era))
            {
                return new BaseStats()
                {
                    [S.multiplier_health] = 10,
                    [S.multiplier_damage] = 5f,
                    [Stats.life_steal.id] = 0.01f,
                    [Stats.addition_armor.id] = 50
                };
            }

            return null;
        };
        werewolf_appearance.GetExtend().final_conditional_basestats = a =>
        {
            if (werewolf_appearance_eras.Contains(World.world_era))
            {
                return new BaseStats()
                {
                    [S.multiplier_health] = 10,
                    [S.multiplier_damage] = 5f,
                    [Stats.life_steal.id] = 0.01f,
                    [Stats.addition_armor.id] = 50
                };
            }

            return new BaseStats()
            {
                [S.stewardship] = -0.8f * a.stats[S.stewardship],
                [S.intelligence] = -0.8f * a.stats[S.intelligence],
                [S.warfare] = -0.8f * a.stats[S.warfare],
                [S.health] = -0.8f * a.stats[S.health],
                [S.damage] = -0.8f * a.stats[S.damage],
                [S.attack_speed] = -0.8f * a.stats[S.attack_speed],
                [S.speed] = -0.95f * a.stats[S.speed],
            };
        };
        diligent.base_stats[S.health] = 200000;
        diligent.base_stats[S.armor] = 10;
        diligent.base_stats[S.damage] = 500;
        diligent.GetExtend().disabled = true;
    }

    internal static HashSet<WorldAgeAsset> werewolf_appearance_eras = new HashSet<WorldAgeAsset>()
        {
            AssetManager.era_library.get(S.age_moon),
            AssetManager.era_library.get(S.age_despair),
            AssetManager.era_library.get(S.age_dark),
            AssetManager.era_library.get(S.age_ash),
            AssetManager.era_library.get(S.age_tears),
        };
}