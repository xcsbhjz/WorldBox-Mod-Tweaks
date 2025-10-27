using System.Collections.Generic;
using strings;
using UnityEngine;

namespace CustomModT001;

public partial class Traits
{
    public static ActorTrait final_body {get; private set;}
    public static ActorTrait final_defence {get; private set;}
    public static ActorTrait final_attack {get; private set;}
    public static ActorTrait final_speed {get; private set;}
    public static ActorTrait final_resilience { get; private set; }
    public static ActorTrait final_resistance { get; private set; }
    public static ActorTrait final_power { get; private set; }
    public static ActorTrait final_skill { get; private set; }
    
    public static ActorTrait buddha_appearances { get; private set; }
    public static ActorTrait ice_walker { get; private set; }
    public static ActorTrait druid { get; private set; }
    public static ActorTrait boss { get; private set; }
    public static ActorTrait exalted_armor { get; private set; }
    public static List<string> final_traits = new List<string>();
    private void AddEffectsV103()
    {
        final_body.base_stats[S.health] = 1000000;
        
        final_defence.base_stats[S.armor] = 80;
        final_attack.base_stats[S.damage] = 200000;
        final_speed.base_stats[S.attack_speed] = 300;
        final_speed.base_stats[Stats.dodge.id] = 25;

        final_resistance.action_get_hit = ((self, by, tile) =>
        {
            self.a.restoreHealth(800);
            return true;
        });
        final_skill.action_special_effect = (target, tile) =>
        {
            if (target.a.HasFinalSkill()) return false;
            TraitPairs.Instance.CheckAndAdd(target.a, out var trait);
            if (string.IsNullOrEmpty(trait)) return false;
            target.a.SetFinalSkill(trait);
            return true;
        };
        
        final_traits.AddRange([final_body.id, final_resistance.id, final_attack.id, final_defence.id, final_power.id, final_resilience.id, final_speed.id, final_skill.id]);
        foreach (var trait_id in final_traits)
        {
            if (string.IsNullOrEmpty(trait_id)) continue;
            var trait = AssetManager.traits.get(trait_id);
            trait.GetExtend().disabled = true;
        }

        buddha_appearances.special_effect_interval = 1;
        buddha_appearances.action_special_effect = (target, tile) =>
        {
            if (target.a.isHappy())
            {
                target.a.restoreHealth(1000);
            }

            return true;
        };
        buddha_appearances.GetExtend().conditional_basestats = a =>
        {
            switch (a.getHappinessRatio())
            {
                case < 0.25f:
                    return new BaseStats()
                    {
                        [S.multiplier_damage] = 0.5f,
                        [Stats.life_steal.id] = 0.05f
                    };
                case <0.5f:
                    return new BaseStats()
                    {
                        [S.multiplier_damage] = -0.5f,
                        [Stats.multiplier_armor.id] = -0.5f,
                        [S.multiplier_health] = -0.5f
                    };
                case <0.75f:
                    return new BaseStats()
                    {
                        [S.armor] = 50,
                        [S.speed] = -50
                    };
                case <=1f:
                    return new BaseStats()
                    {
                        [S.speed] = -0.5f,
                        [S.attack_speed] = -0.5f
                    };
            }

            return null;
        };
        druid.path_icon = "ui/icons/iconDruid";
        druid.special_effect_interval = 1;
        druid.action_special_effect = (obj, _) =>
        {
            var a = obj.a;
            if (a.GetSummonCount(druid.id) >= 5) return false;
            int half_edge = 2;
            int x = obj.current_tile.x;
            int y = obj.current_tile.y;
            for (int i = -half_edge; i <= half_edge; i++)
            for (int j = -half_edge; j <= half_edge; j++)
            {
                WorldTile tile = World.world.GetTile(x + i, y + j);
                if (tile == null) continue;
                foreach (Actor unit in tile._units)
                {
                    if (unit == null || unit.isAlive()) continue;
                    if (!unit.asset.default_animal) continue;
                    if (unit.hasTrait(Traits.summoned.id)) continue;

                    unit.addTrait(Traits.summoned.id);
                    unit.SetSummonSource(druid.id);
                    unit.SetOwner(a);
                    
                    if (a.GetSummonCount(druid.id) >= 5) return false;
                }
            }
            return true;
        };
        ice_walker.special_effect_interval = 1;
        ice_walker.action_special_effect = (target, tile) =>
        {
            World.world.loopWithBrush(target.current_tile, Brush.get(4, "circ_"), new PowerActionWithID(PowerLibrary.drawTemperatureMinus), "coldAura");
            if (target.current_tile.isFrozen())
            {
                target.a.restoreHealth(50);
            }

            return true;
        };
// 首先设置特质的禁用属性
exalted_armor.GetExtend().disabled = true;
exalted_armor.can_be_given = false;

// 然后定义action_get_hit方法
exalted_armor.action_get_hit = ((self, by, tile) =>
{
    // 先获取当前生命值，再加上250点恢复量
    self.a.setHealth(self.a.getHealth() + 250);
    return true;
});
        boss.SetupSkill(400);
        boss.action_attack_target = (self, target, tile) =>
        {
            if (self.a.TraitUnderCooldown(boss.id) ||
                !self.a.TryTakeMana(boss.GetExtend().mana_cost)) return true;
            self.a.StartCooldown(boss);

            self.a.addStatusEffect("frozen", 30);
            self.a.addStatusEffect(Statuses.active_boss.id);
            self.a.event_full_stats = true;

            return true;
        };
    }
}