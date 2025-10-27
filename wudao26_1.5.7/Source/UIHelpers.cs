using UnityEngine;
using UnityEngine.UI;
using NeoModLoader.General;
using System;

namespace CustomModT001;

public static class UIHelpers
{
    // 统一的样式设置
    public static class Styles
    {
        public static readonly Color BackgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.7f);
        public static readonly Color HeaderBackgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.7f);
        public static readonly Color SortButtonBackgroundColor = new Color(0.3f, 0.3f, 0.3f, 0.7f);
        public static readonly Color TextColor = Color.white;
        public static readonly int NormalFontSize = 14;
        public static readonly int SmallFontSize = 12;
        public static readonly Font DefaultFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
    }

    // 创建标准的滚动窗口
    public static ScrollWindow CreateStandardScrollWindow(string id, string titleKey)
    {
        ScrollWindow window = WindowCreator.CreateEmptyWindow(id, titleKey);
        if (window == null)
        {
            Debug.LogError($"创建窗口失败: {id}");
            return null;
        }
        
        window.scrollRect.gameObject.SetActive(true);
        window.transform.Find("Header/Title")?.GetComponent<LocalizedText>()?.setKeyAndUpdate(titleKey);
        
        return window;
    }

    // 创建标准的文本对象
    public static Text CreateStandardText(Transform parent, string text, TextAnchor alignment, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPosition)
    {
        var textObj = new GameObject("Text");
        textObj.transform.SetParent(parent);
        textObj.transform.localScale = Vector3.one;

        var rect = textObj.AddComponent<RectTransform>();
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.pivot = new Vector2(alignment == TextAnchor.MiddleLeft || alignment == TextAnchor.LowerLeft || alignment == TextAnchor.UpperLeft ? 0 : 
                              alignment == TextAnchor.MiddleRight || alignment == TextAnchor.LowerRight || alignment == TextAnchor.UpperRight ? 1 : 0.5f, 
                              alignment == TextAnchor.UpperLeft || alignment == TextAnchor.UpperCenter || alignment == TextAnchor.UpperRight ? 1 : 
                              alignment == TextAnchor.LowerLeft || alignment == TextAnchor.LowerCenter || alignment == TextAnchor.LowerRight ? 0 : 0.5f);
        rect.anchoredPosition = anchoredPosition;
        rect.sizeDelta = new Vector2(0, 0);

        var textComp = textObj.AddComponent<Text>();
        textComp.text = text;
        textComp.font = Styles.DefaultFont;
        textComp.fontSize = Styles.NormalFontSize;
        textComp.color = Styles.TextColor;
        textComp.alignment = alignment;

        return textComp;
    }

    // 创建标准的按钮
    public static Button CreateStandardButton(Transform parent, string text, UnityEngine.Events.UnityAction onClickAction, Vector2 anchorMin, Vector2 anchorMax)
    {
        var buttonObj = new GameObject("Button");
        buttonObj.transform.SetParent(parent);
        buttonObj.transform.localScale = Vector3.one;

        var rect = buttonObj.AddComponent<RectTransform>();
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = new Vector2(0, 0);
        rect.sizeDelta = new Vector2(0, 25f);

        var background = buttonObj.AddComponent<Image>();
        background.color = Styles.SortButtonBackgroundColor;

        var button = buttonObj.AddComponent<Button>();
        button.onClick.AddListener(onClickAction);

        var textComp = CreateStandardText(buttonObj.transform, text, TextAnchor.MiddleCenter, new Vector2(0, 0), new Vector2(1, 1), new Vector2(0, 0));
        textComp.fontSize = Styles.SmallFontSize;

        return button;
    }

    // 创建标准的容器
    public static GameObject CreateStandardContainer(Transform parent, Vector2 anchoredPosition, float height)
    {
        var containerObj = new GameObject("Container");
        containerObj.transform.SetParent(parent);
        containerObj.transform.localScale = Vector3.one;

        var rect = containerObj.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(1, 1);
        rect.pivot = new Vector2(0.5f, 1);
        rect.anchoredPosition = anchoredPosition;
        rect.sizeDelta = new Vector2(-20f, height);

        var background = containerObj.AddComponent<Image>();
        background.color = Styles.BackgroundColor;

        return containerObj;
    }

    // 调整内容面板高度
    public static void AdjustContentHeight(RectTransform contentTransform, int itemCount, float itemHeight, float additionalSpace = 0)
    {
        float windowHeight = Math.Max(300, itemCount * itemHeight + additionalSpace);
        contentTransform.sizeDelta = new Vector2(0, windowHeight);
    }
}