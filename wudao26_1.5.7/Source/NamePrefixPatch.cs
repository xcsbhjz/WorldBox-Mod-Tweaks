using HarmonyLib;
using NeoModLoader.api.attributes;
using UnityEngine;

namespace CustomModT001;

public class NamePrefixPatch
{
    // 为Actor添加获取带等级前缀名称的方法
    [HarmonyPostfix]
    [HarmonyPatch(typeof(Actor), nameof(Actor.getName))]
    private static void Actor_getName_postfix(Actor __instance, ref string __result)
    {
        int level = __instance.GetCultisysLevel();
        // 只有14级(超凡)及以上才添加前缀
        if (level >= 14)
        {
            string levelName = Cultisys.GetName(level).Replace("-", "");
            // 检查并移除已存在的前缀
            if (__result.Contains("-"))
            {
                int hyphenIndex = __result.IndexOf('-');
                string existingPrefix = __result.Substring(0, hyphenIndex);
                // 检查是否是等级前缀
                if (IsLevelPrefix(existingPrefix))
                {
                    __result = __result.Substring(hyphenIndex + 1);
                }
            }
            __result = $"{levelName}-{__result}";
        }
    }

    // 判断是否是有效的等级前缀
    private static bool IsLevelPrefix(string prefix)
    {
        // 获取所有等级名称并检查是否匹配
        for (int i = 14; i <= 27; i++)
        {
            string levelName = Cultisys.GetName(i).Replace("-", "");
            if (prefix == levelName)
            {
                return true;
            }
        }
        return false;
    }

    // 同时修改UI中显示的单位名称
    [HarmonyPostfix]
    [HarmonyPatch(typeof(TooltipLibrary), nameof(TooltipLibrary.showActor))]
    private static void TooltipLibrary_showActor_modifyName(Tooltip pTooltip, TooltipData pData)
    {
        // 查找并修改tooltip中的名称文本
        if (pTooltip.name != null && pTooltip.name.text != null)
        {
            int level = pData.actor.GetCultisysLevel();
            if (level >= 14)
            {
                string levelName = Cultisys.GetName(level).Replace("-", "");
                // 检查并移除已存在的前缀
                if (pTooltip.name.text.Contains("-"))
                {
                    int hyphenIndex = pTooltip.name.text.IndexOf('-');
                    string existingPrefix = pTooltip.name.text.Substring(0, hyphenIndex);
                    // 检查是否是等级前缀
                    if (IsLevelPrefix(existingPrefix))
                    {
                        pTooltip.name.text = pTooltip.name.text.Substring(hyphenIndex + 1);
                    }
                }
                pTooltip.name.text = $"{levelName}-{pTooltip.name.text}";
            }
        }

    }
}