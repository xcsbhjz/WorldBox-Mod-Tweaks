using NeoModLoader.api.attributes;
using NeoModLoader.General;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using AttributeExpansion.code.utils;

namespace PeerlessThedayofGodswrath.code
{
    public class UnitWindowStatsIcon
    {
        public static void OnEnable(UnitWindow window, Actor actor)
        {
            window.setIconValue("careerexperience", actor.Getcareerexperience());
            window.showInfo();
        }
        public static StatsIconData[] StatsIconDatas = {
            new ("careerexperience", "ui/careerexperience.png",true),
        };
        public static StatsIcon icon;
        public static Transform content_transform;


        public static void Initialize(UnitWindow window)
        {
            window
                .gameObject.transform.Find("Background/Scroll View")
                .GetComponent<ScrollRect>()
                .enabled = true;
            window
                .gameObject.transform.Find("Background/Scroll View/Viewport")
                .GetComponent<Mask>()
                .enabled = true;
            window
                .gameObject.transform.Find("Background/Scroll View/Viewport")
                .GetComponent<Image>()
                .enabled = true;

            content_transform = window.gameObject.transform.Find(
                "Background/Scroll View/Viewport/Content/content_more_icons"
            );

            content_transform.gameObject.AddOrGetComponent<StatsIconContainer>();


            var originalParent = content_transform.GetChild(4);

            var newStatsIconGroup = GameObject.Instantiate(originalParent);
            for (int i = newStatsIconGroup.transform.childCount - 1; i >= 0; i--)
            {
                Transform child = newStatsIconGroup.transform.GetChild(i);
                if (child.name != "i_kills")
                {
                    GameObject.Destroy(child.gameObject); // 销毁子对象
                }
            }

            var i_kills = newStatsIconGroup.Find("i_kills");


            int index = 0;
            foreach (var iconData in StatsIconDatas)
            {
                if (!iconData.is_show)
                {
                    continue;
                }
                if (index == 5)
                {
                    break;
                }
                var base_icon = GameObject.Instantiate(i_kills, originalParent);
                icon = base_icon.GetComponent<StatsIcon>();
                var icon_text = base_icon.GetComponent<TipButton>();
                icon_text.textOnClick = LM.Get("statsIcon_" + iconData.name);
                icon.name = iconData.name;
                icon.getIcon().sprite = Resources.Load<Sprite>(iconData.iconPath);
                index++;
            }


            GameObject.DestroyImmediate(i_kills.gameObject);
            newStatsIconGroup.name = "test_new_icon";
            newStatsIconGroup.SetParent(content_transform);
            newStatsIconGroup.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public class StatsIconData
    {
        public string name;
        public string iconPath;
        public bool is_show = false;

        public StatsIconData(string name, string iconPath, bool is_show = false)
        {
            this.name = name;
            this.iconPath = iconPath;
            this.is_show = is_show;
        }
    }
}
