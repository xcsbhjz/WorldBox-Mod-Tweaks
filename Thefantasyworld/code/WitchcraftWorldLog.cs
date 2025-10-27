using UnityEngine;
using System;

public class ThefantasyworldWorldLog
{
    public const string Thefantasyworld_LOG_GROUP = "Thefantasyworld";
    public const string Thefantasyworld_HISTORY_GROUP_ICON_PATH = "ui/icon";
    
    public static void Init()
    {
        RegisterThefantasyworldHistoryGroup();
    }
    
    private static void RegisterThefantasyworldHistoryGroup()
    {
        if (AssetManager.history_groups == null)
        {
            return;
        }
        
        bool groupExists = false;
        foreach (HistoryGroupAsset group in AssetManager.history_groups.list)
        {
            if (group.id == Thefantasyworld_LOG_GROUP)
            {
                groupExists = true;
                break;
            }
        }
        
        if (!groupExists)
        {
            HistoryGroupAsset ThefantasyworldHistoryGroup = new HistoryGroupAsset
            {
                id = Thefantasyworld_LOG_GROUP,
                icon_path = Thefantasyworld_HISTORY_GROUP_ICON_PATH
            };
            
            AssetManager.history_groups.add(ThefantasyworldHistoryGroup);
        }
    }
}