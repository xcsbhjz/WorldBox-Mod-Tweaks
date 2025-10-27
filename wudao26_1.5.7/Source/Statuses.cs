using CustomModT001.Abstract;
using strings;
using UnityEngine;

namespace CustomModT001;

public class Statuses : ExtendLibrary<StatusAsset, Statuses>
{
    public static StatusAsset active_unbreakable_heart { get; private set; }
    public static StatusAsset active_undead_war_god       { get; private set; }
    public static StatusAsset active_empty_feathered      { get; private set; }
    public static StatusAsset active_demon_embrace        { get; private set; }
    public static StatusAsset active_kill_time            { get; private set; }
    public static StatusAsset active_sharp_blade_waltz    { get; private set; }
    public static StatusAsset active_death_hatred         { get; private set; }
    public static StatusAsset active_hope_graffiti        { get; private set; }
    public static StatusAsset active_erosion              { get; private set; }
    public static StatusAsset active_flexible_dog         { get; private set; }
    public static StatusAsset active_quick_attack         { get; private set; }
    public static StatusAsset active_super_quick_attack   { get; private set; }
    public static StatusAsset active_run_man              { get; private set; }
    public static StatusAsset active_runningman { get; private set; }
    public static StatusAsset active_stable               { get; private set; }
    public static StatusAsset active_stable_like_mountain { get; private set; }
    public static StatusAsset active_thirsty              { get; private set; }
    public static StatusAsset active_super_thirsty        { get; private set; }
    public static StatusAsset active_cyan_fury            { get; private set; }
    public static StatusAsset active_thirsty_bait         { get; private set; }
    public static StatusAsset active_thirsty_bait_source         { get; private set; }
    public static StatusAsset active_legion_dawn          { get; private set; }
    public static StatusAsset active_legion_shield        { get; private set; }
    public static StatusAsset active_armor_piercing { get; private set; }
    public static StatusAsset active_weakened             { get; private set; }
    public static StatusAsset active_boss { get; private set; }
    public static StatusAsset confined                    { get; private set; }
    public static StatusAsset dizzy                       { get; private set; }
    public static StatusAsset stoic                       { get; private set; }
    public static StatusAsset limited_madness { get; private set; }
    public static StatusAsset active_servant_master       { get; private set; }
    public static StatusAsset active_holy_dawn            { get; private set; }
    public static StatusAsset active_spirit_sword         { get; private set; }

    protected override void OnInit()
    {
        RegisterAssets(ModClass.asset_id_prefix);
        AddEffects();
    }

    protected override StatusAsset Add(StatusAsset asset)
    {
        asset.locale_id = asset.id;
        asset.locale_description = asset.id + "_info";
        asset.path_icon =
            $"inmny/custommodt001/{asset.id.Replace($"{ModClass.asset_id_prefix}.", "").Replace("active_", "")}";
        return base.Add(asset);
    }

    private void AddEffects()
    {
        limited_madness.duration = 600;
        limited_madness.path_icon = "ui/Icons/iconMadness";

        active_unbreakable_heart.duration = 60;
        active_unbreakable_heart.base_stats[S.multiplier_damage] = 5;
        active_unbreakable_heart.action_interval = 10;
        active_unbreakable_heart.action = (obj, tile) =>
        {
            obj.getHit(1000, pAttackType: AttackType.None, pSkipIfShake: false);
            return true;
        };

        active_undead_war_god.duration = 30;
        active_undead_war_god.base_stats[S.multiplier_damage] = 50;
        active_undead_war_god.base_stats[S.multiplier_health] = 100;
        active_undead_war_god.base_stats[S.attack_speed] = 200;
        active_undead_war_god.base_stats[Stats.control_get.id] = -0.8f;

        active_empty_feathered.duration = 5;
        active_empty_feathered.base_stats[S.multiplier_health] = 7;
        active_empty_feathered.base_stats[S.multiplier_damage] = 7;

        active_demon_embrace.action_interval = 5f;
        active_demon_embrace.duration = 10f;
        active_demon_embrace.action = (obj, tile) =>
        {
            var health_to_take = Mathf.FloorToInt(obj.stats[S.health] * 0.01f /100f) * 100f;
            if (obj.getData().health > health_to_take)
            {
                obj.a.IncDemonEmbraceCount(Mathf.FloorToInt(health_to_take / 100f));
                obj.getHit(health_to_take, false, pAttackType: AttackType.None, pSkipIfShake: false);
            }

            return true;
        };
        active_demon_embrace.GetExtend().action_finished = (obj, effect) => { obj.a.ResetDemonEmbraceCount(); };
        active_demon_embrace.GetExtend().conditional_basestats = a =>
        {
            var count = a.GetDemonEmbraceCount();
            return new BaseStats
            {
                [S.attack_speed] = count,
                [S.damage] = count
            };
        };


        active_kill_time.duration = 10f;
        active_kill_time.base_stats[S.multiplier_damage] = 20;

        active_sharp_blade_waltz.duration = 4;
        active_sharp_blade_waltz.base_stats[S.attack_speed] = 300;

        active_death_hatred.duration = 100;
        active_death_hatred.base_stats[S.multiplier_damage] = 5;

        active_hope_graffiti.duration = 100;
        active_hope_graffiti.GetExtend().final_basestats[S.multiplier_damage] = -0.9f;

        active_erosion.duration = 100;
        active_erosion.GetExtend().final_basestats[Stats.multiplier_armor.id] = -0.5f;

        active_flexible_dog.duration = 15;
        active_flexible_dog.base_stats[S.speed] = 300;
        active_flexible_dog.base_stats[Stats.dodge.id] = 90;

        active_quick_attack.duration = 10;
        active_quick_attack.base_stats[S.attack_speed] = 100;

        active_super_quick_attack.duration = 10;
        active_super_quick_attack.base_stats[S.attack_speed] = 200;
        active_super_quick_attack.base_stats[S.multiplier_damage] = -0.2f;

        active_run_man.duration = 10;
        active_run_man.base_stats[S.speed] = 200;

        active_runningman.duration = 10;
        active_runningman.base_stats[S.speed] = 400;

        active_stable.duration = 20;
        active_stable.base_stats[S.armor] = 20;

        active_stable_like_mountain.duration = 10;
        active_stable_like_mountain.base_stats[S.armor] = 80;

        active_thirsty.duration = 1.5f;
        active_super_thirsty.base_stats[Stats.life_steal.id] = 1;

        active_super_thirsty.duration = 5;
        active_super_thirsty.base_stats[Stats.life_steal.id] = 1;
        active_super_thirsty.action_interval = 1;
        active_super_thirsty.action = (obj, tile) =>
        {
            obj.getHit(100, pAttackType: AttackType.None, pSkipIfShake: false);
            return true;
        };

        active_cyan_fury.duration = 5;
        active_cyan_fury.base_stats[Stats.life_steal.id] = 0.5f;

        active_thirsty_bait.duration = 10;
        active_thirsty_bait.path_icon = "inmny/custommodt001/active_thirsty_bait";
        active_thirsty_bait.action_interval = 1;
        active_thirsty_bait.action = (obj, tile) =>
        {
            obj.getHit(100, pAttackType: AttackType.None, pSkipIfShake: false);
            return true;
        };
        
        active_thirsty_bait_source.duration = 10;
        active_thirsty_bait_source.path_icon = "inmny/custommodt001/active_thirsty_bait";
        active_thirsty_bait_source.action_interval = 1;
        active_thirsty_bait_source.action = (obj, tile) =>
        {
            if (obj.isActor())
                obj.a.restoreHealth(400);
            return true;
        };

        active_legion_dawn.duration = 5;
        active_legion_dawn.base_stats[S.multiplier_damage] = 0.5f;

        active_legion_shield.duration = 5;
        active_legion_shield.base_stats[S.armor] = 10;

        active_armor_piercing.duration = 5;
        active_armor_piercing.base_stats[Stats.multiplier_armor.id] = -0.2f;

        active_weakened.duration = 5;
        active_weakened.base_stats[S.multiplier_damage] = -0.2f;
        active_weakened.path_icon = "inmny/custommodt001/weaken";

        confined.duration = 5f;
        confined.base_stats[S.speed] = -1000000;
        confined.base_stats.addTag(S_Tag.immovable);
        confined.base_stats.addTag(S_Tag.frozen_ai);
        confined.base_stats.addTag(S_Tag.stop_idle_animation);

        dizzy.duration = 2f;
        dizzy.base_stats.addTag(S_Tag.immovable);
        dizzy.base_stats.addTag(S_Tag.frozen_ai);
        dizzy.base_stats.addTag(S_Tag.stop_idle_animation);

        stoic.duration = 10;

        active_servant_master.duration = 5;
        active_servant_master.base_stats[S.multiplier_damage] = 5;
        active_servant_master.base_stats[S.multiplier_health] = 5;
        active_servant_master.action_interval = 1;
        active_servant_master.action = (obj, tile) =>
        {
            if (obj.a.GetOwner() == null) obj.a.addTrait("madness");

            return true;
        };

        active_holy_dawn.duration = 100;
        active_holy_dawn.base_stats[S.multiplier_damage] = 5f;
        active_holy_dawn.base_stats[S.armor] = 40f;

        active_spirit_sword.duration = 20;

        active_boss.base_stats[S.multiplier_health] = 10;
        active_boss.base_stats[S.multiplier_damage] = 10;
        active_boss.base_stats[S.armor] = 30;
        active_boss.duration = 360;
    }
}