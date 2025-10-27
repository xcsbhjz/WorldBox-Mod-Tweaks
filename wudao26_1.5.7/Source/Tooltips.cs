using CustomModT001.Abstract;
using NeoModLoader.General;

namespace CustomModT001;

public class Tooltips : ExtendLibrary<TooltipAsset, Tooltips>
{
    public static TooltipAsset cultisys { get; private set; }
    public static TooltipAsset common   { get; private set; }

    protected override void OnInit()
    {
        RegisterAssets(ModClass.asset_id_prefix);
        cultisys.callback = ShowCultisys;
        common.callback = ShowCommon;
    }

    private void ShowCommon(Tooltip tooltip, string type, TooltipData data)
    {
        var tip_name = data.tip_name;
        if (LocalizedTextManager.stringExists(tip_name)) tip_name = LM.Get(tip_name);

        tooltip.name.text = tip_name;

        var tip_description = data.tip_description;
        if (LocalizedTextManager.stringExists(tip_description)) tip_description = LM.Get(tip_description);

        tooltip.setDescription(tip_description);

        var tip_description2 = data.tip_description_2;
        if (LocalizedTextManager.stringExists(tip_description2)) tip_description2 = LM.Get(tip_description2);

        tooltip.setBottomDescription(tip_description2);
    }

    private static string GetTitle(Actor actor)
    {
        // 检查是否有血脉始祖称号
        bool isBloodAncestor = false;
        actor.data.get("is_blood_ancestor", out isBloodAncestor, false);
        if (isBloodAncestor)
        {
            return LM.Get("inmny.custommodt001.blood_ancestor");
        }
        
        // 检查是否有血脉称号
        string bloodlineName = "";
        actor.data.get("bloodline_name", out bloodlineName, "");
        if (!string.IsNullOrEmpty(bloodlineName))
        {
            return $"{bloodlineName}{LM.Get("inmny.custommodt001.bloodline")}";
        }
        
        return null;
    }
    
    private static void ShowCultisys(Tooltip tooltip, string type, TooltipData data)
    {
        Actor actor = data.actor;

        var level = actor.GetCultisysLevel();
        tooltip.name.text = Cultisys.GetName(level);
        if (level < Cultisys.MaxLevel)
        {
            tooltip.setDescription($"{ModClass.asset_id_prefix}.ui.cultiprogress".Localize()+$": {actor.GetExp():N0}/{Cultisys.LevelExpRequired[level]:N0}");
        }

        // 显示基础天赋值
        tooltip.addLineIntText($"{ModClass.asset_id_prefix}.ui.base_talent", (int)actor.GetTalent());
        
        // 显示额外天赋值
        int extraTalent = 0;
        actor.data.get("extra_talent_inheritance", out extraTalent, 0);
        tooltip.addLineIntText($"{ModClass.asset_id_prefix}_ui_extra_talent", extraTalent);
        
        // 显示血脉资质 - 仅当血脉天赋大于0时显示
        if (extraTalent > 0)
        {
            string bloodlineQuality = "平庸";
            actor.data.get("bloodline_quality", out bloodlineQuality, "平庸");
            
            // 根据资质类型设置不同颜色
            string colorTag = "#FFFFFF";
            switch (bloodlineQuality)
            {
                case "粗劣":
                    colorTag = "#FFFFFF";
                    break;
                case "残缺":
                    colorTag = "#CCCCCC"; // 淡灰色
                    break;
                case "平庸":
                    colorTag = "#55FF55";
                    break;
                case "优质":
                    colorTag = "#5555FF";
                    break;
                case "极品":
                    colorTag = "#FFAA00";
                    break;
                case "无一":
                    colorTag = "#FF5555";
                    break;
            }
            
            // 正确的方式：将颜色作为参数传递，而不是在值中包含颜色标签
            // 将硬编码的"血脉资质"替换为本地化文本键
            tooltip.addLineText($"{ModClass.asset_id_prefix}.ui.bloodline_quality", bloodlineQuality, colorTag);
        }
        
        // 显示称号
        string title = GetTitle(actor);
        if (!string.IsNullOrEmpty(title))
        {
            tooltip.addLineText($"{ModClass.asset_id_prefix}.ui.title", title);
        }
        BaseStatsHelper.showBaseStats(tooltip.stats_description, tooltip.stats_values, Cultisys.LevelStats[level]);
    }
}