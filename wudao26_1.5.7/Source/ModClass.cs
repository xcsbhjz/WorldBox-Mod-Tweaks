using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using NeoModLoader.api;
using NeoModLoader.General;
using NeoModLoader.General.UI.Tab;
using NeoModLoader.ui;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // 添加DOTween命名空间引用

namespace CustomModT001;

public class ModClass : BasicMod<ModClass>, IReloadable
{
    internal const string asset_id_prefix = "inmny.custommodt001";
    internal const string trait_group_id  = $"{asset_id_prefix}.group";
    
    // 标记是否为血脉始祖
    private Dictionary<string, bool> isAncestorBloodline;

    public void Reload()
    {
    }

    protected override void OnModLoad()
    {
        // 在方法开始处添加DOTween容量设置
        DOTween.SetTweensCapacity(3000, 100);
        
        AssetManager.trait_groups.add(new ActorTraitGroupAsset
        {
            id = trait_group_id,
            name = trait_group_id,
            color = Toolbox.colorToHex(Color.yellow)
        });
        new ActorTasks().Init();
        new ActorJobs().Init();
        new MapIcons().Init();
        new Terraforms().Init();
        new Stats().Init();
        new Statuses().Init();
        new Traits().Init();
        new Actors().Init();
        new Tooltips().Init();
        new TraitPairs().Init();
        try
        {
            Harmony.CreateAndPatchAll(typeof(Patches), asset_id_prefix);
            Harmony.CreateAndPatchAll(typeof(NamePrefixPatch), asset_id_prefix);
        }
        catch (Exception e)
        {
            do
            {
                Debug.LogError(e);
                e = e.InnerException;
            } while (e != null);
        }

        // 初始化RealmStatisticsWindow窗口
        AbstractWindow<RealmStatisticsWindow>.CreateAndInit("RealmStatistics");

        ScrollWindow intro_window =
            WindowCreator.CreateEmptyWindow($"{asset_id_prefix}.intro", $"{asset_id_prefix}.ui.intro");
        intro_window.scrollRect.gameObject.SetActive(true);

        var content_transform =
            intro_window.scrollRect.transform.Find("Viewport/Content").GetComponent<RectTransform>();
        content_transform.pivot = new Vector2(0.5f,    1);
        content_transform.sizeDelta = new Vector2(180, 0);
        var layout_group = content_transform.gameObject.AddComponent<VerticalLayoutGroup>();
        layout_group.childControlHeight = false;
        layout_group.childControlWidth = false;
        layout_group.childAlignment = TextAnchor.UpperCenter;
        layout_group.childScaleHeight = false;
        layout_group.childScaleWidth = false;
        content_transform.gameObject.AddComponent<ContentSizeFitter>().verticalFit =
            ContentSizeFitter.FitMode.PreferredSize;
        var intro = new GameObject("IntroText", typeof(RectTransform), typeof(Text), typeof(ContentSizeFitter));
        var intro_rect = intro.GetComponent<RectTransform>();
        intro_rect.SetParent(content_transform);
        intro_rect.localPosition = new Vector3(0, 0);
        intro_rect.localScale = new Vector3(1,    1);
        intro_rect.pivot = new Vector2(0.5f,    1);
        intro_rect.sizeDelta = new Vector2(180, 0);
        var fitter = intro.GetComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        var intro_text = intro.GetComponent<Text>();
        intro_text.font = LocalizedTextManager.current_font;
        intro_text.fontSize = 10;
        intro_text.text = @"
功能:
    1. 世界排行榜(嵌在收藏夹中)
    2. 境界查看(人物界面鼠标悬停于头像)
    3. 关闭城市投降(模组列表->模组设置)
设定:
    1. 天赋: 数值与境界提升速度成正比
    2. 额外免伤: 累乘作用于所有非真实 伤害
    3. 蓝量: 用于释放技能
    4. 蓝量恢复: 每10秒恢复量
    5. 吸血: 按造成最终伤害回复生命
    6. 境界: 依次为凡人, 入品, 后天, 先天, 至臻, 超凡, 镇神
";

        ScrollWindow.showWindow(intro_window.screen_id);
        var tab = TabManager.CreateTab(asset_id_prefix, asset_id_prefix, "",
            SpriteTextureLoader.getSprite("inmny/custommodt001/cultilevel"));
        tab.SetLayout(new List<string>()
        {
            "tab"
        });
        
        tab.AddPowerButton("tab", PowerButtonCreator.CreateSimpleButton($"{asset_id_prefix}.config", () =>
        {
            ModConfigureWindow.ShowWindow(GetConfig());
        }, SpriteTextureLoader.getSprite("ui/icons/iconOptions")));
        tab.AddPowerButton("tab", PowerButtonCreator.CreateSimpleButton($"{asset_id_prefix}.top", ()=>
        {
            Patches.window_favorites_global_mode = true;
            ScrollWindow.showWindow("favorites");
        }, SpriteTextureLoader.getSprite("ui/icons/iconCommunity")));

        // 添加境界统计按钮
            UnityEngine.Events.UnityAction showTongjiWindow = () =>
            {
                RealmStatisticsWindow.ShowWindow(asset_id_prefix);
            };
            tab.AddPowerButton("tab", PowerButtonCreator.CreateSimpleButton($"{asset_id_prefix}.tongji", showTongjiWindow, SpriteTextureLoader.getSprite("inmny/custommodt001/tongji")));

        // 添加血脉统计按钮
        UnityEngine.Events.UnityAction showBloodlineWindow = () =>
        {
            ScrollWindow bloodline_window = WindowCreator.CreateEmptyWindow($"{asset_id_prefix}.bloodline", $"{asset_id_prefix}.bloodline");
            if (bloodline_window == null)
            {
                Debug.LogError("创建血脉统计窗口失败: window对象为null");
                return;
            }
            bloodline_window.scrollRect.gameObject.SetActive(true);

            bloodline_window.transform.Find("Header/Title")?.GetComponent<LocalizedText>()?.setKeyAndUpdate($"{asset_id_prefix}.bloodline");

            // 统计所有生物的武道血脉
            var bloodlineCounts = new Dictionary<string, int>();
            var bloodlineToUnits = new Dictionary<string, List<Actor>>();
            var bloodlineFirstAppearance = new Dictionary<string, int>(); // 存储血脉首次出现的年份
            isAncestorBloodline = new Dictionary<string, bool>(); // 初始化类级别变量，标记是否为血脉始祖

            if (World.world?.units != null)
            {
                foreach (var unit in World.world.units)
                {
                    if (unit?.data != null && unit.asset.civ)
                    {
                        string bloodlineName = "";
                        bool isBloodAncestor = false;
                        
                        // 检查是否为血脉始祖
                        unit.data.get("is_blood_ancestor", out isBloodAncestor, false);
                        
                        // 获取已有的血脉名称
                        unit.data.get("bloodline_name", out bloodlineName, "");
                        
                        // 如果是血脉始祖但没有血脉名称，使用单位名称作为血脉名称
                        if (isBloodAncestor && string.IsNullOrEmpty(bloodlineName))
                        {
                            bloodlineName = unit.name.Contains('-') ? unit.name.Substring(unit.name.IndexOf('-') + 1) : unit.name;
                        }
                        
                        if (!string.IsNullOrEmpty(bloodlineName))
                        {
                            // 声明birthYear变量，使其在整个if语句中可访问
                            int birthYear = 0;
                            unit.data.get("birth_year", out birthYear, 0);
                             
                            if (!bloodlineCounts.ContainsKey(bloodlineName))
                            {
                                bloodlineCounts[bloodlineName] = 0;
                                bloodlineToUnits[bloodlineName] = new List<Actor>();
                                // 获取血脉首次出现年份
                                bloodlineFirstAppearance[bloodlineName] = birthYear;
                                // 标记是否为血脉始祖
                                isAncestorBloodline[bloodlineName] = isBloodAncestor;
                            }
                            else if (isBloodAncestor && !isAncestorBloodline[bloodlineName])
                            {
                                // 更新为血脉始祖标记
                                isAncestorBloodline[bloodlineName] = true;
                            }
                            
                            bloodlineCounts[bloodlineName]++;
                            bloodlineToUnits[bloodlineName].Add(unit.a);

                            // 更新血脉首次出现年份（取最早的）
                            if (birthYear < bloodlineFirstAppearance[bloodlineName] || bloodlineFirstAppearance[bloodlineName] == 0)
                            {
                                bloodlineFirstAppearance[bloodlineName] = birthYear;
                            }
                        }
                    }
                }
            }

            var scrollRect = bloodline_window.scrollRect;
            if (scrollRect != null)
            {
                var content = scrollRect.transform.Find("Viewport/Content");
                if (content != null)
                {
                    var content_transform = content.GetComponent<RectTransform>();
                    if (content_transform != null)
                    {
                        content_transform.pivot = new Vector2(0.5f, 1);
                        content_transform.anchorMin = new Vector2(0, 1);
                        content_transform.anchorMax = new Vector2(1, 1);
                        content_transform.anchoredPosition = new Vector2(0, 0);

                        foreach (Transform child in content)
                            UnityEngine.Object.Destroy(child.gameObject);

                        // 创建统计表格
                        float yOffset = -20f;
                        var font = UnityEngine.Resources.GetBuiltinResource<UnityEngine.Font>("Arial.ttf");
                        int visibleBloodlineCount = 0;

                        // 创建排序按钮
                        CreateSortButtons(content, font, ref yOffset, bloodline_window, bloodlineCounts, bloodlineToUnits, bloodlineFirstAppearance, isAncestorBloodline);

                        // 先显示血脉始祖的血脉（置顶），然后按名称排序显示其他血脉
                        var sortedBloodlines = bloodlineCounts.Keys
                            .OrderByDescending(name => isAncestorBloodline.ContainsKey(name) && isAncestorBloodline[name])
                            .ThenBy(name => name);
                        
                        foreach (var bloodline in sortedBloodlines)
                        {
                            int count = bloodlineCounts[bloodline];
                            if (count > 0)
                            {
                                visibleBloodlineCount++;
                                int appearanceYear = bloodlineFirstAppearance.ContainsKey(bloodline) ? bloodlineFirstAppearance[bloodline] : 0;

                                // 创建血脉名称按钮
                                CreateBloodlineButton(content, font, bloodline, count, yOffset, bloodlineToUnits[bloodline], appearanceYear);

                                yOffset -= 40f;
                            }
                        }

                        // 根据血脉数量调整窗口高度
                        float windowHeight = Math.Max(300, (visibleBloodlineCount + 1) * 60); // +1 是为了排序按钮留出空间
                        content_transform.sizeDelta = new Vector2(0, windowHeight);
                    }
                }
            }

            if (!string.IsNullOrEmpty(bloodline_window.screen_id))
                ScrollWindow.showWindow(bloodline_window.screen_id);
            else
                Debug.LogError("血脉统计窗口ID为空");
        };
        tab.AddPowerButton("tab", PowerButtonCreator.CreateSimpleButton($"{asset_id_prefix}.bloodline", showBloodlineWindow, SpriteTextureLoader.getSprite("inmny/custommodt001/bloodline")));

        // 重置内容面板的辅助方法
        void ResetContentPanel(RectTransform contentTransform)
        {
            contentTransform.pivot = new Vector2(0.5f, 1);
            contentTransform.anchorMin = new Vector2(0, 1);
            contentTransform.anchorMax = new Vector2(1, 1);
            contentTransform.anchoredPosition = new Vector2(0, 0);

            // 清除所有子对象
            foreach (Transform child in contentTransform)
                UnityEngine.Object.Destroy(child.gameObject);
        }

        // 创建UI文本辅助方法
        void CreateRealmText(Transform parent, UnityEngine.Font font, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPosition, string text, UnityEngine.TextAnchor alignment)
        {
            // 手动创建文本元素显示统计数据
            UIHelpers.CreateStandardText(parent, text, alignment, anchorMin, anchorMax, anchoredPosition);
        }

        // 创建排序按钮
        void CreateSortButtons(Transform parent, UnityEngine.Font font, ref float yOffset, ScrollWindow window, Dictionary<string, int> bloodlineCounts, Dictionary<string, List<Actor>> bloodlineToUnits, Dictionary<string, int> bloodlineFirstAppearance, Dictionary<string, bool> isAncestorBloodline)
        {
            // 创建排序按钮容器
            var sortContainer = new UnityEngine.GameObject("SortButtons");
            sortContainer.transform.SetParent(parent);
            sortContainer.transform.localScale = Vector3.one;

            var rect = sortContainer.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(0.5f, 1);
            rect.anchoredPosition = new Vector2(0, yOffset);
            rect.sizeDelta = new Vector2(-20f, 35f);

            // 添加背景
            var background = sortContainer.AddComponent<Image>();
            background.color = new UnityEngine.Color(0.3f, 0.3f, 0.3f, 0.7f);

            // 创建按年数排序按钮
            var yearButton = new UnityEngine.GameObject("SortByYearButton");
            yearButton.transform.SetParent(sortContainer.transform);
            yearButton.transform.localScale = Vector3.one;

            var yearRect = yearButton.AddComponent<RectTransform>();
            yearRect.anchorMin = new Vector2(0.1f, 0);
            yearRect.anchorMax = new Vector2(0.45f, 1);
            yearRect.pivot = new Vector2(0.5f, 0.5f);
            yearRect.anchoredPosition = new Vector2(0, 0);
            yearRect.sizeDelta = new Vector2(0, 25f);

            var yearButtonComp = yearButton.AddComponent<Button>();
            yearButtonComp.onClick.AddListener(() =>
            {
                SortByYear(window, bloodlineCounts, bloodlineToUnits, bloodlineFirstAppearance, isAncestorBloodline);
            });

            var yearText = yearButton.AddComponent<UnityEngine.UI.Text>();
            yearText.text = "按存续时长排序";
            yearText.font = font;
            yearText.fontSize = 12;
            yearText.color = UnityEngine.Color.white;
            yearText.alignment = UnityEngine.TextAnchor.MiddleCenter;

            // 创建按人数排序按钮
            var countButton = new UnityEngine.GameObject("SortByCountButton");
            countButton.transform.SetParent(sortContainer.transform);
            countButton.transform.localScale = Vector3.one;

            var countRect = countButton.AddComponent<RectTransform>();
            countRect.anchorMin = new Vector2(0.55f, 0);
            countRect.anchorMax = new Vector2(0.9f, 1);
            countRect.pivot = new Vector2(0.5f, 0.5f);
            countRect.anchoredPosition = new Vector2(0, 0);
            countRect.sizeDelta = new Vector2(0, 25f);

            var countButtonComp = countButton.AddComponent<Button>();
            countButtonComp.onClick.AddListener(() =>
            {
                SortByCount(window, bloodlineCounts, bloodlineToUnits, bloodlineFirstAppearance, isAncestorBloodline);
            });

            var countText = countButton.AddComponent<UnityEngine.UI.Text>();
            countText.text = "按人数统计排序";
            countText.font = font;
            countText.fontSize = 12;
            countText.color = UnityEngine.Color.white;
            countText.alignment = UnityEngine.TextAnchor.MiddleCenter;

            yOffset -= 40f;
        }

        // 按年数排序
        void SortByYear(ScrollWindow window, Dictionary<string, int> bloodlineCounts, Dictionary<string, List<Actor>> bloodlineToUnits, Dictionary<string, int> bloodlineFirstAppearance, Dictionary<string, bool> isAncestorBloodline)
        {
            RebuildBloodlineList(window, bloodlineCounts, bloodlineToUnits, bloodlineFirstAppearance, isAncestorBloodline, true, false);
        }

        // 按人数排序
        void SortByCount(ScrollWindow window, Dictionary<string, int> bloodlineCounts, Dictionary<string, List<Actor>> bloodlineToUnits, Dictionary<string, int> bloodlineFirstAppearance, Dictionary<string, bool> isAncestorBloodline)
        {
            RebuildBloodlineList(window, bloodlineCounts, bloodlineToUnits, bloodlineFirstAppearance, isAncestorBloodline, false, true);
        }

        // 重建血脉列表
        void RebuildBloodlineList(ScrollWindow window, Dictionary<string, int> bloodlineCounts, Dictionary<string, List<Actor>> bloodlineToUnits, Dictionary<string, int> bloodlineFirstAppearance, Dictionary<string, bool> isAncestorBloodline, bool sortByYear, bool sortByCount)
        {
            var scrollRect = window.scrollRect;
            if (scrollRect != null)
            {
                var content = scrollRect.transform.Find("Viewport/Content");
                if (content != null)
                {
                    var content_transform = content.GetComponent<RectTransform>();
                    if (content_transform != null)
                    {
                        // 保留排序按钮，删除其他所有子对象
                        List<Transform> childrenToKeep = new List<Transform>();
                        foreach (Transform child in content)
                        {
                            if (child.name == "SortButtons")
                            {
                                childrenToKeep.Add(child);
                            }
                            else
                            {
                                UnityEngine.Object.Destroy(child.gameObject);
                            }
                        }

                        // 创建统计表格
                        float yOffset = -60f; // 从排序按钮下方开始
                        var font = UnityEngine.Resources.GetBuiltinResource<UnityEngine.Font>("Arial.ttf");
                        int visibleBloodlineCount = 0;

                        // 创建总血脉条数显示
                        var totalCountObj = new UnityEngine.GameObject("TotalBloodlineCount");
                        totalCountObj.transform.SetParent(content);
                        totalCountObj.transform.localScale = Vector3.one;

                        var totalCountRect = totalCountObj.AddComponent<RectTransform>();
                        totalCountRect.anchorMin = new Vector2(0, 1);
                        totalCountRect.anchorMax = new Vector2(1, 1);
                        totalCountRect.pivot = new Vector2(0.5f, 1);
                        totalCountRect.anchoredPosition = new Vector2(0, yOffset);
                        totalCountRect.sizeDelta = new Vector2(-20f, 25f);

                        var totalCountText = totalCountObj.AddComponent<UnityEngine.UI.Text>();
                        totalCountText.text = "总血脉条数: " + bloodlineCounts.Count;
                        totalCountText.font = font;
                        totalCountText.fontSize = 12;
                        totalCountText.color = new UnityEngine.Color(0.9f, 0.9f, 0.2f, 1); // 浅黄色
                        totalCountText.alignment = UnityEngine.TextAnchor.MiddleCenter;

                        yOffset -= 30f;

                        IEnumerable<string> sortedBloodlines = bloodlineCounts.Keys;

                        if (sortByYear)
                        {
                            // 按存续时长排序（最长的在前），但血脉始祖始终置顶
                            sortedBloodlines = sortedBloodlines
                                .OrderByDescending(name => isAncestorBloodline.ContainsKey(name) && isAncestorBloodline[name])
                                .ThenByDescending(name => 
                                {
                                    // 完全复制UI显示中的年数计算逻辑
                                    int firstAppearanceYear = 0;
                                    if (bloodlineFirstAppearance.ContainsKey(name))
                                    {
                                        firstAppearanceYear = bloodlineFirstAppearance[name];
                                    }
                                    
                                    // 如果首次出现年份无效，尝试从单位数据中查找
                                    if (firstAppearanceYear <= 0 && bloodlineToUnits.ContainsKey(name) && bloodlineToUnits[name] != null && bloodlineToUnits[name].Count > 0)
                                    {
                                        // 查找最早的单位创建年份
                                        firstAppearanceYear = int.MaxValue;
                                        foreach (var unit in bloodlineToUnits[name])
                                        {
                                            if (unit != null && unit.data != null)
                                            {
                                                int birthYear = 0;
                                                unit.data.get("birth_year", out birthYear, 0);
                                                if (birthYear > 0 && birthYear < firstAppearanceYear)
                                                {
                                                    firstAppearanceYear = birthYear;
                                                }
                                            }
                                        }
                                        
                                        // 如果还是没找到有效年份，尝试使用单位的创建时间
                                        if (firstAppearanceYear == int.MaxValue)
                                        {
                                            float earliestCreationTime = float.MaxValue;
                                            foreach (var unit in bloodlineToUnits[name])
                                            {
                                                if (unit != null && unit.data != null && unit.data.created_time < earliestCreationTime)
                                                {
                                                    earliestCreationTime = (float)unit.data.created_time;
                                                }
                                            }
                                            
                                            if (earliestCreationTime < float.MaxValue)
                                            {
                                                firstAppearanceYear = Date.getYear((double)earliestCreationTime);
                                            }
                                        }
                                    }
                                    
                                    // 计算存续时长
                                    if (firstAppearanceYear > 0)
                                    {
                                        float firstAppearanceTime = (float)((float)(firstAppearanceYear - 1) * 60.0f);
                                        return Date.getYearsSince((double)firstAppearanceTime);
                                    }
                                    return 0;
                                });
                        }
                        else if (sortByCount)
                        {
                            // 按人数统计排序（最多的在前），简单直接的从大到小排序
                            sortedBloodlines = sortedBloodlines
                                .Where(name => bloodlineCounts.ContainsKey(name)) // 确保只包含有计数的血脉
                                .OrderByDescending(name => bloodlineCounts[name]) // 只按人数从大到小排序
                                .ThenBy(name => name); // 添加名称作为第二排序键，确保结果稳定
                        }
                        else
                        {
                            // 默认按名称排序，血脉始祖始终置顶
                            sortedBloodlines = sortedBloodlines
                                .OrderByDescending(name => isAncestorBloodline.ContainsKey(name) && isAncestorBloodline[name])
                                .ThenBy(name => name);
                        }

                        foreach (var bloodline in sortedBloodlines)
                        {
                            int count = bloodlineCounts[bloodline];
                            if (count > 0)
                            {
                                visibleBloodlineCount++;
                                int appearanceYear = bloodlineFirstAppearance.ContainsKey(bloodline) ? bloodlineFirstAppearance[bloodline] : 0;

                                // 创建血脉名称按钮
                                CreateBloodlineButton(content, font, bloodline, count, yOffset, bloodlineToUnits[bloodline], appearanceYear);

                                yOffset -= 40f;
                            }
                        }

                        // 根据血脉数量调整窗口高度
                        float windowHeight = Math.Max(300, (visibleBloodlineCount + 1) * 60); // +1 是为了排序按钮留出空间
                        content_transform.sizeDelta = new Vector2(0, windowHeight);
                    }
                }
            }
        }

        // 创建血脉按钮辅助方法
        void CreateBloodlineButton(Transform parent, UnityEngine.Font font, string bloodlineName, int count, float yOffset, List<Actor> units, int appearanceYear)
        {
            // 创建按钮容器
            var buttonObj = new UnityEngine.GameObject("BloodlineButton");
            buttonObj.transform.SetParent(parent);
            buttonObj.transform.localScale = Vector3.one;

            var rect = buttonObj.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(0.5f, 1);
            rect.anchoredPosition = new Vector2(0, yOffset);
            rect.sizeDelta = new Vector2(-20f, 35f);

            // 添加背景
            var background = buttonObj.AddComponent<Image>();
            // 统一使用灰色背景，与血脉单位列表保持一致
            background.color = new UnityEngine.Color(0.2f, 0.2f, 0.2f, 0.7f); // 统一的灰色背景

            // 添加按钮组件
            var button = buttonObj.AddComponent<Button>();
            button.onClick.AddListener(() =>
            {
                // 点击血脉名称时，显示拥有该血脉的所有单位
                ShowBloodlineUnits(bloodlineName, units);
            });

            // 添加血脉名称文本
            var nameTextObj = new UnityEngine.GameObject("BloodlineName");
            nameTextObj.transform.SetParent(buttonObj.transform);
            nameTextObj.transform.localScale = Vector3.one;

            var nameRect = nameTextObj.AddComponent<RectTransform>();
            nameRect.anchorMin = new Vector2(0, 0);
            nameRect.anchorMax = new Vector2(0.5f, 1);
            nameRect.pivot = new Vector2(0, 0.5f);
            nameRect.anchoredPosition = new Vector2(10f, 0);
            nameRect.sizeDelta = new Vector2(0, 0);

            var nameText = nameTextObj.AddComponent<UnityEngine.UI.Text>();
            nameText.text = bloodlineName + "血脉";
            nameText.font = font;
            nameText.fontSize = 14;
            nameText.color = UnityEngine.Color.white;
            nameText.alignment = UnityEngine.TextAnchor.MiddleLeft;

            // 添加出现年数文本
            var yearTextObj = new UnityEngine.GameObject("BloodlineYear");
            yearTextObj.transform.SetParent(buttonObj.transform);
            yearTextObj.transform.localScale = Vector3.one;

            var yearRect = yearTextObj.AddComponent<RectTransform>();
            yearRect.anchorMin = new Vector2(0.5f, 0);
            yearRect.anchorMax = new Vector2(0.7f, 1);
            yearRect.pivot = new Vector2(0.5f, 0.5f);
            yearRect.anchoredPosition = new Vector2(0, 0);
            yearRect.sizeDelta = new Vector2(0, 0);

            var yearText = yearTextObj.AddComponent<UnityEngine.UI.Text>();
            
            // 使用游戏标准的年龄计算方式
            // 首先检查appearanceYear是否有效，如果无效则遍历单位查找最早的创建时间
            int firstAppearanceYear = appearanceYear;
            if (firstAppearanceYear <= 0 && units != null && units.Count > 0)
            {
                // 查找最早的单位创建年份
                firstAppearanceYear = int.MaxValue;
                foreach (var unit in units)
                {
                    if (unit != null && unit.data != null)
                    {
                        int birthYear = 0;
                        unit.data.get("birth_year", out birthYear, 0);
                        if (birthYear > 0 && birthYear < firstAppearanceYear)
                        {
                            firstAppearanceYear = birthYear;
                        }
                    }
                }
                
                // 如果还是没找到有效年份，尝试使用单位的创建时间
                if (firstAppearanceYear == int.MaxValue)
                {
                    float earliestCreationTime = float.MaxValue;
                    foreach (var unit in units)
                    {
                        if (unit != null && unit.data != null && unit.data.created_time < earliestCreationTime)
                        {
                            earliestCreationTime = (float)unit.data.created_time; // 添加显式类型转换，将可能的double类型转换为float
                        }
                    }
                    
                    if (earliestCreationTime < float.MaxValue)
                    {
                        firstAppearanceYear = Date.getYear((double)earliestCreationTime);
                    }
                }
            }
            
            // 使用游戏标准的方式计算血脉出现年数
            string yearTextValue = "未知年";
            if (firstAppearanceYear > 0)
            {
                // 获取血脉首次出现的游戏时间
                float firstAppearanceTime = (float)((float)(firstAppearanceYear - 1) * 60.0f); // 确保所有计算都是float类型，添加额外的(float)转换
                int yearsSinceAppearance = Date.getYearsSince((double)firstAppearanceTime);
                yearTextValue = $"{yearsSinceAppearance}年";
            }
            
            yearText.text = yearTextValue;
            yearText.font = font;
            yearText.fontSize = 12;
            yearText.color = UnityEngine.Color.yellow;
            yearText.alignment = UnityEngine.TextAnchor.MiddleCenter;

            // 添加数量文本
            var countTextObj = new UnityEngine.GameObject("BloodlineCount");
            countTextObj.transform.SetParent(buttonObj.transform);
            countTextObj.transform.localScale = Vector3.one;

            var countRect = countTextObj.AddComponent<RectTransform>();
            countRect.anchorMin = new Vector2(0.7f, 0);
            countRect.anchorMax = new Vector2(1, 1);
            countRect.pivot = new Vector2(1, 0.5f);
            countRect.anchoredPosition = new Vector2(-10f, 0);
            countRect.sizeDelta = new Vector2(0, 0);

            var countText = countTextObj.AddComponent<UnityEngine.UI.Text>();
            // 统一显示白色文字，去掉"始祖"标识
            countText.text = count.ToString();
            countText.color = UnityEngine.Color.white;
            countText.fontSize = 14;
            countText.font = font;
            countText.alignment = UnityEngine.TextAnchor.MiddleRight;
        }

        // 显示拥有特定血脉的单位
        void ShowBloodlineUnits(string bloodlineName, List<Actor> units)
        {
            ScrollWindow units_window = WindowCreator.CreateEmptyWindow($"{asset_id_prefix}.bloodline_units", $"{asset_id_prefix}.bloodline_units");
            if (units_window == null)
            {
                Debug.LogError("创建血脉单位窗口失败: window对象为null");
                return;
            }
            units_window.scrollRect.gameObject.SetActive(true);

            units_window.transform.Find("Header/Title")?.GetComponent<LocalizedText>()?.setKeyAndUpdate($"血脉: {bloodlineName} (共{units.Count}人)");

            var scrollRect = units_window.scrollRect;
            if (scrollRect != null)
            {
                var content = scrollRect.transform.Find("Viewport/Content");
                if (content != null)
                {
                    var content_transform = content.GetComponent<RectTransform>();
                    if (content_transform != null)
                    {
                        content_transform.pivot = new Vector2(0.5f, 1);
                        content_transform.anchorMin = new Vector2(0, 1);
                        content_transform.anchorMax = new Vector2(1, 1);
                        content_transform.anchoredPosition = new Vector2(0, 0);

                        foreach (Transform child in content)
                            UnityEngine.Object.Destroy(child.gameObject);

                        // 创建排序按钮容器
                        var sortButtonsObj = new UnityEngine.GameObject("SortButtons");
                        sortButtonsObj.transform.SetParent(content);
                        sortButtonsObj.transform.localScale = Vector3.one;

                        var sortButtonsRect = sortButtonsObj.AddComponent<RectTransform>();
                        sortButtonsRect.anchorMin = new Vector2(0, 1);
                        sortButtonsRect.anchorMax = new Vector2(1, 1);
                        sortButtonsRect.pivot = new Vector2(0.5f, 1);
                        sortButtonsRect.anchoredPosition = new Vector2(0, -20f);
                        sortButtonsRect.sizeDelta = new Vector2(-20f, 35f);

                        var sortButtonsBackground = sortButtonsObj.AddComponent<Image>();
                        sortButtonsBackground.color = new UnityEngine.Color(0.1f, 0.1f, 0.1f, 0.7f);

                        // 创建排序按钮
                        var font = UnityEngine.Resources.GetBuiltinResource<UnityEngine.Font>("Arial.ttf");
                        CreateSortButtonsForUnits(sortButtonsObj.transform, font, units, content, content_transform, bloodlineName);
                    }
                }
            }

            if (!string.IsNullOrEmpty(units_window.screen_id))
                ScrollWindow.showWindow(units_window.screen_id);
            else
                Debug.LogError("血脉单位窗口ID为空");
        }

        // 创建单位列表排序按钮
        void CreateSortButtonsForUnits(Transform parent, UnityEngine.Font font, List<Actor> units, Transform content, RectTransform content_transform, string bloodlineName)
        {
            // 按姓名排序按钮
            var nameSortButton = new UnityEngine.GameObject("NameSortButton");
            nameSortButton.transform.SetParent(parent);
            nameSortButton.transform.localScale = Vector3.one;

            var nameSortRect = nameSortButton.AddComponent<RectTransform>();
            nameSortRect.anchorMin = new Vector2(0, 0);
            nameSortRect.anchorMax = new Vector2(1f/3f, 1);
            nameSortRect.pivot = new Vector2(0.5f, 0.5f);
            nameSortRect.anchoredPosition = new Vector2(0, 0);
            nameSortRect.sizeDelta = new Vector2(-7.5f, -5f);

            var nameSortBackground = nameSortButton.AddComponent<Image>();
            nameSortBackground.color = new UnityEngine.Color(0.3f, 0.3f, 0.3f, 0.7f);

            var nameSortBtn = nameSortButton.AddComponent<Button>();
            nameSortBtn.onClick.AddListener(() =>
            {
                SortUnitsByName(content, content_transform, units, font);
            });

            // 创建文本子对象
            var nameSortTextObj = new UnityEngine.GameObject("SortText");
            nameSortTextObj.transform.SetParent(nameSortButton.transform);
            nameSortTextObj.transform.localScale = Vector3.one;
            
            var nameSortTextRect = nameSortTextObj.AddComponent<RectTransform>();
            nameSortTextRect.anchorMin = new Vector2(0, 0);
            nameSortTextRect.anchorMax = new Vector2(1, 1);
            nameSortTextRect.pivot = new Vector2(0.5f, 0.5f);
            nameSortTextRect.anchoredPosition = new Vector2(0, 0);
            nameSortTextRect.sizeDelta = new Vector2(0, 0);

            var nameSortText = nameSortTextObj.AddComponent<UnityEngine.UI.Text>();
            nameSortText.text = "姓名排序";
            nameSortText.font = font;
            nameSortText.fontSize = 12;
            nameSortText.color = UnityEngine.Color.white;
            nameSortText.alignment = UnityEngine.TextAnchor.MiddleCenter;

            // 按境界排序按钮
            var realmSortButton = new UnityEngine.GameObject("RealmSortButton");
            realmSortButton.transform.SetParent(parent);
            realmSortButton.transform.localScale = Vector3.one;

            var realmSortRect = realmSortButton.AddComponent<RectTransform>();
            realmSortRect.anchorMin = new Vector2(1f/3f, 0);
            realmSortRect.anchorMax = new Vector2(2f/3f, 1);
            realmSortRect.pivot = new Vector2(0.5f, 0.5f);
            realmSortRect.anchoredPosition = new Vector2(0, 0);
            realmSortRect.sizeDelta = new Vector2(-15f, -5f);

            var realmSortBackground = realmSortButton.AddComponent<Image>();
            realmSortBackground.color = new UnityEngine.Color(0.3f, 0.3f, 0.3f, 0.7f);

            var realmSortBtn = realmSortButton.AddComponent<Button>();
            realmSortBtn.onClick.AddListener(() =>
            {
                SortUnitsByRealm(content, content_transform, units, font);
            });

            // 创建文本子对象
            var realmSortTextObj = new UnityEngine.GameObject("SortText");
            realmSortTextObj.transform.SetParent(realmSortButton.transform);
            realmSortTextObj.transform.localScale = Vector3.one;
            
            var realmSortTextRect = realmSortTextObj.AddComponent<RectTransform>();
            realmSortTextRect.anchorMin = new Vector2(0, 0);
            realmSortTextRect.anchorMax = new Vector2(1, 1);
            realmSortTextRect.pivot = new Vector2(0.5f, 0.5f);
            realmSortTextRect.anchoredPosition = new Vector2(0, 0);
            realmSortTextRect.sizeDelta = new Vector2(0, 0);

            var realmSortText = realmSortTextObj.AddComponent<UnityEngine.UI.Text>();
            realmSortText.text = "境界排序";
            realmSortText.font = font;
            realmSortText.fontSize = 12;
            realmSortText.color = UnityEngine.Color.white;
            realmSortText.alignment = UnityEngine.TextAnchor.MiddleCenter;

            // 按血脉天赋排序按钮
            var bloodlineTalentSortButton = new UnityEngine.GameObject("BloodlineTalentSortButton");
            bloodlineTalentSortButton.transform.SetParent(parent);
            bloodlineTalentSortButton.transform.localScale = Vector3.one;

            var bloodlineTalentSortRect = bloodlineTalentSortButton.AddComponent<RectTransform>();
            bloodlineTalentSortRect.anchorMin = new Vector2(2f/3f, 0);
            bloodlineTalentSortRect.anchorMax = new Vector2(1, 1);
            bloodlineTalentSortRect.pivot = new Vector2(0.5f, 0.5f);
            bloodlineTalentSortRect.anchoredPosition = new Vector2(0, 0);
            bloodlineTalentSortRect.sizeDelta = new Vector2(-7.5f, -5f);

            var bloodlineTalentSortBackground = bloodlineTalentSortButton.AddComponent<Image>();
            bloodlineTalentSortBackground.color = new UnityEngine.Color(0.3f, 0.3f, 0.3f, 0.7f);

            var bloodlineTalentSortBtn = bloodlineTalentSortButton.AddComponent<Button>();
            bloodlineTalentSortBtn.onClick.AddListener(() =>
            {
                SortUnitsByBloodlineTalent(content, content_transform, units, font, bloodlineName);
            });

            // 创建文本子对象
            var bloodlineTalentSortTextObj = new UnityEngine.GameObject("SortText");
            bloodlineTalentSortTextObj.transform.SetParent(bloodlineTalentSortButton.transform);
            bloodlineTalentSortTextObj.transform.localScale = Vector3.one;
            
            var bloodlineTalentSortTextRect = bloodlineTalentSortTextObj.AddComponent<RectTransform>();
            bloodlineTalentSortTextRect.anchorMin = new Vector2(0, 0);
            bloodlineTalentSortTextRect.anchorMax = new Vector2(1, 1);
            bloodlineTalentSortTextRect.pivot = new Vector2(0.5f, 0.5f);
            bloodlineTalentSortTextRect.anchoredPosition = new Vector2(0, 0);
            bloodlineTalentSortTextRect.sizeDelta = new Vector2(0, 0);

            var bloodlineTalentSortText = bloodlineTalentSortTextObj.AddComponent<UnityEngine.UI.Text>();
            bloodlineTalentSortText.text = "天赋排序";
            bloodlineTalentSortText.font = font;
            bloodlineTalentSortText.fontSize = 12;
            bloodlineTalentSortText.color = UnityEngine.Color.white;
            bloodlineTalentSortText.alignment = UnityEngine.TextAnchor.MiddleCenter;

            // 初始按姓名排序显示
            SortUnitsByName(content, content_transform, units, font);
        }

        // 按姓名排序单位
        void SortUnitsByName(Transform content, RectTransform content_transform, List<Actor> units, UnityEngine.Font font)
        {
            // 保留排序按钮，删除其他所有子对象
            List<Transform> childrenToKeep = new List<Transform>();
            foreach (Transform child in content)
            {
                if (child.name == "SortButtons")
                {
                    childrenToKeep.Add(child);
                }
                else
                {
                    UnityEngine.Object.Destroy(child.gameObject);
                }
            }

            // 创建单位列表
            float yOffset = -60f; // 从排序按钮下方开始
            
            // 先显示血脉始祖单位（置顶）
            foreach (var unit in units.Where(u => IsBloodAncestor(u)).OrderBy(u => u.name))
            {
                // 创建单位信息按钮
                CreateUnitInfoButton(content, font, unit, yOffset);
                yOffset -= 40f;
            }
            
            // 然后显示其他单位
            foreach (var unit in units.Where(u => !IsBloodAncestor(u)).OrderBy(u => u.name))
            {
                // 创建单位信息按钮
                CreateUnitInfoButton(content, font, unit, yOffset);
                yOffset -= 40f;
            }

            // 根据单位数量调整窗口高度
            float windowHeight = Math.Max(300, units.Count * 60 + 60); // +60 是为了排序按钮留出空间
            content_transform.sizeDelta = new Vector2(0, windowHeight);
        }

        // 按境界排序单位
        void SortUnitsByRealm(Transform content, RectTransform content_transform, List<Actor> units, UnityEngine.Font font)
        {
            // 保留排序按钮，删除其他所有子对象
            List<Transform> childrenToKeep = new List<Transform>();
            foreach (Transform child in content)
            {
                if (child.name == "SortButtons")
                {
                    childrenToKeep.Add(child);
                }
                else
                {
                    UnityEngine.Object.Destroy(child.gameObject);
                }
            }

            // 先显示血脉始祖单位（置顶）
            float yOffset = -60f; // 从排序按钮下方开始
            foreach (var unit in units.Where(u => IsBloodAncestor(u)).OrderByDescending(u => u.GetCultisysLevel()).ThenBy(u => u.name))
            {
                // 创建单位信息按钮
                CreateUnitInfoButton(content, font, unit, yOffset);
                yOffset -= 40f;
            }
            
            // 然后显示其他单位
            foreach (var unit in units.Where(u => !IsBloodAncestor(u)).OrderByDescending(u => u.GetCultisysLevel()).ThenBy(u => u.name))
            {
                // 创建单位信息按钮
                CreateUnitInfoButton(content, font, unit, yOffset);
                yOffset -= 40f;
            }

            // 根据单位数量调整窗口高度
            float windowHeight = Math.Max(300, units.Count * 60 + 60); // +60 是为了排序按钮留出空间
            content_transform.sizeDelta = new Vector2(0, windowHeight);
        }

        // 判断单位是否为血脉始祖
        bool IsBloodAncestor(Actor unit)
        {
            if (unit == null || unit.data == null)
                return false;
            
            bool isBloodAncestor = false;
            unit.data.get("is_blood_ancestor", out isBloodAncestor, false);
            return isBloodAncestor;
        }

        // 按血脉天赋排序单位
        void SortUnitsByBloodlineTalent(Transform content, RectTransform content_transform, List<Actor> units, UnityEngine.Font font, string bloodlineName)
        {
            // 保留排序按钮，删除其他所有子对象
            List<Transform> childrenToKeep = new List<Transform>();
            foreach (Transform child in content)
            {
                if (child.name == "SortButtons")
                {
                    childrenToKeep.Add(child);
                }
                else
                {
                    UnityEngine.Object.Destroy(child.gameObject);
                }
            }

            // 先显示血脉始祖单位（置顶）
            float yOffset = -60f; // 从排序按钮下方开始
            foreach (var unit in units.Where(u => IsBloodAncestor(u)).OrderByDescending(u => GetBloodlineTalentValue(u, bloodlineName)).ThenBy(u => u.name))
            {
                // 创建单位信息按钮
                CreateUnitInfoButton(content, font, unit, yOffset);
                yOffset -= 40f;
            }
            
            // 然后显示其他单位
            foreach (var unit in units.Where(u => !IsBloodAncestor(u)).OrderByDescending(u => GetBloodlineTalentValue(u, bloodlineName)).ThenBy(u => u.name))
            {
                // 创建单位信息按钮
                CreateUnitInfoButton(content, font, unit, yOffset);
                yOffset -= 40f;
            }

            // 根据单位数量调整窗口高度
            float windowHeight = Math.Max(300, units.Count * 60 + 60); // +60 是为了排序按钮留出空间
            content_transform.sizeDelta = new Vector2(0, windowHeight);
        }

        // 创建单位信息按钮辅助方法
        void CreateUnitInfoButton(Transform parent, UnityEngine.Font font, Actor unit, float yOffset)
        {
            // 创建按钮容器
            var buttonObj = new UnityEngine.GameObject("UnitButton");
            buttonObj.transform.SetParent(parent);
            buttonObj.transform.localScale = Vector3.one;

            var rect = buttonObj.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(0.5f, 1);
            rect.anchoredPosition = new Vector2(0, yOffset);
            rect.sizeDelta = new Vector2(-20f, 35f);

            // 添加背景
            var background = buttonObj.AddComponent<Image>();
            background.color = new UnityEngine.Color(0.2f, 0.2f, 0.2f, 0.7f);

            // 添加按钮组件
            var button = buttonObj.AddComponent<Button>();
            button.onClick.AddListener(() =>
                        {
                            // 点击单位名称时，显示单位信息窗口
                            if (unit != null)
                            {
                                global::ActionLibrary.openUnitWindow(unit);
                            }
                        });

            // 添加单位名称文本
            var nameTextObj = new UnityEngine.GameObject("UnitName");
            nameTextObj.transform.SetParent(buttonObj.transform);
            nameTextObj.transform.localScale = Vector3.one;

            var nameRect = nameTextObj.AddComponent<RectTransform>();
            nameRect.anchorMin = new Vector2(0, 0);
            nameRect.anchorMax = new Vector2(0.5f, 1);
            nameRect.pivot = new Vector2(0, 0.5f);
            nameRect.anchoredPosition = new Vector2(10f, 0);
            nameRect.sizeDelta = new Vector2(0, 0);

            var nameText = nameTextObj.AddComponent<UnityEngine.UI.Text>();
            nameText.text = unit.name;
            nameText.font = font;
            nameText.fontSize = 14;
            nameText.color = UnityEngine.Color.white;
            nameText.alignment = UnityEngine.TextAnchor.MiddleLeft;

            // 添加单位境界文本
            string realmName = Cultisys.GetName(unit.GetCultisysLevel());
            var realmTextObj = new UnityEngine.GameObject("UnitRealm");
            realmTextObj.transform.SetParent(buttonObj.transform);
            realmTextObj.transform.localScale = Vector3.one;

            var realmRect = realmTextObj.AddComponent<RectTransform>();
            realmRect.anchorMin = new Vector2(0.5f, 0);
            realmRect.anchorMax = new Vector2(0.7f, 1);
            realmRect.pivot = new Vector2(0.5f, 0.5f);
            realmRect.anchoredPosition = new Vector2(0, 0);
            realmRect.sizeDelta = new Vector2(0, 0);

            var realmText = realmTextObj.AddComponent<UnityEngine.UI.Text>();
            realmText.text = realmName;
            realmText.font = font;
            realmText.fontSize = 14;
            realmText.color = UnityEngine.Color.white;
            realmText.alignment = UnityEngine.TextAnchor.MiddleRight;

            // 添加血脉天赋数值文本
            // 直接从unit对象获取血脉天赋值
            int talentValue = 0;
            if (unit != null)
            {
                // 尝试获取extra_talent_inheritance字段的值
                talentValue = GetBloodlineTalentValue(unit, "");
            }

            var talentTextObj = new UnityEngine.GameObject("UnitTalent");
            talentTextObj.transform.SetParent(buttonObj.transform);
            talentTextObj.transform.localScale = Vector3.one;

            var talentRect = talentTextObj.AddComponent<RectTransform>();
            talentRect.anchorMin = new Vector2(0.7f, 0);
            talentRect.anchorMax = new Vector2(1, 1);
            talentRect.pivot = new Vector2(1, 0.5f);
            talentRect.anchoredPosition = new Vector2(-10f, 0);
            talentRect.sizeDelta = new Vector2(0, 0);

            var talentText = talentTextObj.AddComponent<UnityEngine.UI.Text>();
            // 如果是血脉始祖，显示"血脉始祖"文本
            if (IsBloodAncestor(unit))
            {
                talentText.text = "血脉始祖";
                talentText.color = UnityEngine.Color.red; // 使用醒目的颜色
                talentText.fontSize = 12; // 缩小字体大小以避免换行
            }
            else
            {
                talentText.text = talentValue.ToString();
                talentText.color = UnityEngine.Color.yellow;
                talentText.fontSize = 14;
            }
            talentText.font = font;
            talentText.alignment = UnityEngine.TextAnchor.MiddleRight;
        }

        // 获取单位的血脉天赋数值
        int GetBloodlineTalentValue(Actor unit, string bloodlineName)
        {
            if (unit == null || unit.data == null)
                return 0;

            // 尝试从data中获取血脉天赋数值
            int talentValue = 0;
            
            try
            {
                // 从Patches.cs中看到，血脉天赋相关的字段是'extra_talent_inheritance'
                // 使用get方法获取这个字段的值（这是代码中统一使用的方法）
                unit.data.get("extra_talent_inheritance", out talentValue, 0);
                
                // 如果没有获取到值，尝试其他可能的字段
                if (talentValue == 0)
                {
                    // 尝试bloodline_talent字段
                    unit.data.get("bloodline_talent", out talentValue, 0);
                }
            }
            catch (Exception)
            {
                // 发生异常时保持talentValue为0
            }
            
            return talentValue;
        }
            
        tab.UpdateLayout();
    }
}

public class RealmStatisticsWindow : AbstractWindow<RealmStatisticsWindow>
{
    private static RectTransform _itemPrefab;
    private static ObjectPoolGenericMono<RectTransform> _itemPool;
    private string _assetIdPrefix;

    protected override void Init()
    {
        base.BackgroundTransform.Find("Scroll View").gameObject.SetActive(true);
        // 缩小滚动视图宽度以适应边框
        base.BackgroundTransform.Find("Scroll View").GetComponent<RectTransform>().sizeDelta = new Vector2(220f, 270f);
        base.BackgroundTransform.Find("Scroll View").localPosition = new Vector3(0f, -6f);
        // 调整Viewport以匹配缩小的滚动视图
        base.BackgroundTransform.Find("Scroll View/Viewport").GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 0f);
        base.BackgroundTransform.Find("Scroll View/Viewport").localPosition = new Vector3(-110f, 135f);
        
        // 设置垂直布局和内容大小适配
        VerticalLayoutGroup layout = base.ContentTransform.gameObject.AddComponent<VerticalLayoutGroup>();
        layout.childControlHeight = true;
        layout.childControlWidth = true;
        layout.childForceExpandHeight = false;
        layout.childForceExpandWidth = false;
        layout.childAlignment = TextAnchor.UpperCenter;
        // 减小内边距以增加可用空间
        layout.padding = new RectOffset(2, 2, 0, 0);
        
        ContentSizeFitter fitter = base.ContentTransform.gameObject.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        
        // 创建列表项预设
        _createItemPrefab();
        _itemPool = new ObjectPoolGenericMono<RectTransform>(_itemPrefab, base.ContentTransform);
    }
    
    private static void _createItemPrefab()
    {
        GameObject item = new GameObject("RealmItem", new Type[] { typeof(Image), typeof(HorizontalLayoutGroup) });
        
        // 设置背景
        item.GetComponent<Image>().sprite = SpriteTextureLoader.getSprite("ui/special/windowInnerSliced");
        item.GetComponent<Image>().type = Image.Type.Sliced;
        
        // 设置水平布局
        HorizontalLayoutGroup layout = item.GetComponent<HorizontalLayoutGroup>();
        layout.childControlWidth = false;
        layout.childControlHeight = false;
        layout.childAlignment = TextAnchor.MiddleCenter;
        // 减小间距以节省空间
        layout.spacing = 1f;
        
        // 创建境界名称文本
        GameObject nameObj = new GameObject("RealmName", new Type[] { typeof(Text) });
        nameObj.transform.SetParent(item.transform);
        nameObj.transform.localScale = Vector3.one;
        Text nameText = nameObj.GetComponent<Text>();
        OT.InitializeCommonText(nameText);
        nameText.alignment = TextAnchor.MiddleLeft;
        nameText.resizeTextForBestFit = true;
        // 调整名称文本宽度
        nameText.GetComponent<RectTransform>().sizeDelta = new Vector2(110f, 24f);
        
        // 创建数量和百分比文本
        GameObject countObj = new GameObject("Count", new Type[] { typeof(Text) });
        countObj.transform.SetParent(item.transform);
        countObj.transform.localScale = Vector3.one;
        Text countText = countObj.GetComponent<Text>();
        OT.InitializeCommonText(countText);
        countText.alignment = TextAnchor.MiddleRight;
        countText.resizeTextForBestFit = true;
        // 调整数量文本宽度
        countText.GetComponent<RectTransform>().sizeDelta = new Vector2(90f, 24f);
        
        // 设置预设对象的RectTransform
        RectTransform itemRect = item.GetComponent<RectTransform>();
        itemRect.sizeDelta = new Vector2(220f, 24f); // 缩小项目宽度
        
        // 禁用预设对象
        item.SetActive(false);
        _itemPrefab = itemRect;
    }
    
    public static void ShowWindow(string pAssetIdPrefix)
    {
        AbstractWindow<RealmStatisticsWindow>.Instance._assetIdPrefix = pAssetIdPrefix;
        ScrollWindow.showWindow(AbstractWindow<RealmStatisticsWindow>.WindowId);
    }
    
    public override void OnNormalEnable()
    {
        // 清空对象池
        _itemPool.clear(true);
        
        // 统计所有生物的武道境界
        var realmCounts = new Dictionary<int, int>();
        int maxLevel = Cultisys.MaxLevel;
        string wudaoLevelKey = $"{_assetIdPrefix}.wudao_level";
        int totalPopulation = 0;
        
        if (World.world?.units != null)
        {
            foreach (var unit in World.world.units)
            {
                if (unit?.data != null && unit.asset.civ)
                {
                    int level = 0;
                    unit.data.get(wudaoLevelKey, out level, 0);
                    if (level <= maxLevel && level >= 0)
                    {
                        if (!realmCounts.ContainsKey(level))
                            realmCounts[level] = 0;
                        realmCounts[level]++;
                        totalPopulation++;
                    }
                }
            }
        }
        
        // 按境界等级排序并创建UI项
        foreach (var level in realmCounts.Keys.OrderBy(l => l))
        {
            int count = realmCounts[level];
            if (count > 0)
            {
                RectTransform item = _itemPool.getNext();
                
                // 设置文本内容
                Transform nameTransform = item.Find("RealmName");
                if (nameTransform != null)
                {
                    Text nameText = nameTransform.GetComponent<Text>();
                    if (nameText != null)
                    {
                        nameText.text = $"{Cultisys.GetName(level)} ({level})";
                    }
                }
                
                Transform countTransform = item.Find("Count");
                if (countTransform != null)
                {
                    Text countText = countTransform.GetComponent<Text>();
                    if (countText != null)
                    {
                        if (totalPopulation > 0)
                        {
                            float percentage = (float)count / totalPopulation;
                            // 确保百分比显示小数点后一位
                            countText.text = $"{count} ({(percentage * 100).ToString("F2")}%)";
                        }
                        else
                        {
                            countText.text = count.ToString();
                        }
                    }
                }
            }
        }
    }
}