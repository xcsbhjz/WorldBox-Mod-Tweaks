using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

namespace XianTu.code
{
    public static class NotificationHelper
    {
        // 境界名称映射表：特质ID -> 中文境界名称
        private static readonly Dictionary<string, string> RealmNameMap = new Dictionary<string, string>
        {
            { "XianTu1", "练气" },
            { "XianTu2", "筑基" },
            { "XianTu3", "金丹" },
            { "XianTu4", "元婴" },
            { "XianTu5", "化神" },
            { "XianTu6", "合体" },
            { "XianTu7", "大乘" },
            { "XianTu8", "半仙" },
            { "XianTu9", "天仙" },
            { "XianTu91", "金仙" },
            { "XianTu92", "太乙玉仙" },
            { "XianTu93", "大罗金仙" },
        };


        // 金色颜色定义
        private static readonly Color GoldColor = new Color(1f, 0.843f, 0f, 1f);

        public static void ShowBreakthroughNotification(Actor actor, string oldTrait, string newTrait)
        {
            string oldRealmName = GetRealmName(oldTrait);
            string newRealmName = GetRealmName(newTrait);
            
            string message = $"{actor.getName()} 已从 {oldRealmName} 突破到 {newRealmName}！";
            Debug.Log(message);
            
            // 创建提示UI
            GameObject notificationObj = new GameObject("BreakthroughNotification");
            notificationObj.transform.SetParent(GetCanvas().transform, false);
            
            // 设置全局缩放
            CanvasScaler scaler = notificationObj.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.matchWidthOrHeight = 0.5f; // 根据需要调整匹配宽度或高度的比例
            
            // 文本组件
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(notificationObj.transform, false);
            Text text = textObj.AddComponent<Text>();
            
            // 尝试加载Arial字体，如果失败则使用备用方案
            Font font = TryLoadFont("Arial.ttf") ?? TryLoadFont("Arial") ?? CreateDefaultFont();
            
            text.font = font;
            text.text = message;
            text.alignment = TextAnchor.MiddleCenter;
            text.fontSize = 12; // 减小字体大小
            text.color = GoldColor;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Truncate;
            text.resizeTextForBestFit = true;
            text.resizeTextMaxSize = 16; // 降低最大字体限制
            text.resizeTextMinSize = 8;
            
            // 添加外发光效果
            Shadow shadow = textObj.AddComponent<Shadow>();
            shadow.effectColor = new Color(0f, 0f, 0f, 0.7f);
            shadow.effectDistance = new Vector2(2f, -2f);
            
            Outline outline = textObj.AddComponent<Outline>();
            outline.effectColor = new Color(0f, 0f, 0f, 0.8f);
            outline.effectDistance = new Vector2(1.5f, 1.5f);
            
            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchorMin = new Vector2(1.8f, 4.2f); // 调整到屏幕上部但更居中
            textRect.anchorMax = new Vector2(1.8f, 4.2f);
            textRect.pivot = new Vector2(0.5f, 0.5f);
            textRect.sizeDelta = new Vector2(1800, 60); // 增加宽度，减小高度
            
            // 添加淡入淡出效果
            BreakthroughNotification component = notificationObj.AddComponent<BreakthroughNotification>();
            component.Init(text);
        }



        private static Font TryLoadFont(string fontName)
        {
            try
            {
                Font font = Resources.GetBuiltinResource<Font>(fontName);
                if (font != null)
                {
                    Debug.Log($"成功加载字体: {fontName}");
                    return font;
                }
                Debug.LogWarning($"无法加载字体: {fontName}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"加载字体时出错: {fontName}, 错误: {e.Message}");
            }
            return null;
        }
        
        private static Font CreateDefaultFont()
        {
            Debug.LogWarning("使用空字体作为后备方案，文本可能不会显示");
            return new Font("Arial");
        }
        
        private static string GetRealmName(string traitId)
        {
            if (RealmNameMap.TryGetValue(traitId, out string realmName))
            {
                return realmName;
            }
            return traitId;
        }
        
        private static Canvas GetCanvas()
        {
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject("Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 1000;
                canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();
            }
            return canvas;
        }
    }
    
    public class BreakthroughNotification : MonoBehaviour
    {
        private Text textComponent;
        private float showDuration = 3f;
        private float fadeDuration = 0.5f;
        
        public void Init(Text text)
        {
            textComponent = text;
            
            // 初始透明度为0
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 0);
            
            // 强制更新布局
            Canvas.ForceUpdateCanvases();
            
            // 启动淡入动画
            StartCoroutine(FadeIn());
        }
        
        IEnumerator FadeIn()
        {
            float elapsed = 0;
            while (elapsed < fadeDuration)
            {
                float alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);
                textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, alpha);
                
                // 同时淡入阴影和描边
                foreach (var effect in textComponent.GetComponents<BaseMeshEffect>())
                {
                    if (effect is Shadow shadow)
                    {
                        shadow.effectColor = new Color(shadow.effectColor.r, shadow.effectColor.g, shadow.effectColor.b, alpha * 0.7f);
                    }
                    else if (effect is Outline outline)
                    {
                        outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, alpha * 0.8f);
                    }
                }
                
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            // 保持显示
            yield return new WaitForSeconds(showDuration);
            
            // 启动淡出动画
            StartCoroutine(FadeOut());
        }
        
        IEnumerator FadeOut()
        {
            float elapsed = 0;
            while (elapsed < fadeDuration)
            {
                float alpha = Mathf.Lerp(1, 0, elapsed / fadeDuration);
                textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, alpha);
                
                // 同时淡出阴影和描边
                foreach (var effect in textComponent.GetComponents<BaseMeshEffect>())
                {
                    if (effect is Shadow shadow)
                    {
                        shadow.effectColor = new Color(shadow.effectColor.r, shadow.effectColor.g, shadow.effectColor.b, alpha * 0.7f);
                    }
                    else if (effect is Outline outline)
                    {
                        outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, alpha * 0.8f);
                    }
                }
                
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            // 销毁对象
            Destroy(gameObject);
        }
    }
}