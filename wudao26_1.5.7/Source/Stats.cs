using CustomModT001.Abstract;
using strings;

namespace CustomModT001;

public class Stats : ExtendLibrary<BaseStatAsset, Stats>
{
    private const string        stat_id_prefix = ModClass.asset_id_prefix;
    public static BaseStatAsset life_steal           { get; private set; }
    public static BaseStatAsset dodge           { get; private set; }
    public static BaseStatAsset addition_armor       { get; private set; }
    public static BaseStatAsset multiplier_armor { get; private set; }
    public static BaseStatAsset mana_regen           { get; private set; }
    public static BaseStatAsset control_give         { get; private set; }
    public static BaseStatAsset control_get          { get; private set; }
    public static BaseStatAsset status_time_give     { get; private set; }
    public static BaseStatAsset status_time_get      { get; private set; }
    public static BaseStatAsset stats_stacked_effect { get; private set; }
    [AssetId(S.knockback_reduction)]
    public static BaseStatAsset knockback_reduction { get; private set; }

    protected override void OnInit()
    {
        RegisterAssets(stat_id_prefix);

        life_steal.show_as_percents = true;
        life_steal.tooltip_multiply_for_visual_number = 100;
        addition_armor.show_as_percents = true;
        multiplier_armor.show_as_percents = true;
        multiplier_armor.tooltip_multiply_for_visual_number = 100;
        multiplier_armor.multiplier = true;
        multiplier_armor.main_stat_to_multiply = S.armor;
        control_give.show_as_percents = true;
        control_give.tooltip_multiply_for_visual_number = 100;
        control_get.show_as_percents = true;
        control_get.tooltip_multiply_for_visual_number = 100;
        status_time_give.show_as_percents = true;
        status_time_give.tooltip_multiply_for_visual_number = 100;
        status_time_get.show_as_percents = true;
        status_time_get.tooltip_multiply_for_visual_number = 100;
        stats_stacked_effect.show_as_percents = true;
        stats_stacked_effect.tooltip_multiply_for_visual_number = 100;

        cached_library.get(S.health).normalize = true;
        cached_library.get(S.health).normalize_min = 0;
        cached_library.get(S.health).normalize_max = int.MaxValue - 1;

        cached_library.get(Stats.dodge.id).hidden = false;
        cached_library.get(Stats.dodge.id).ignore = false;
    }
}