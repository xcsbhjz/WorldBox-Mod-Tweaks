using UnityEngine;
using UnityEngine.UI;
using VideoCopilot.code.utils;
using System;
using NeoModLoader.General;

namespace XianTu.code
{
    public class LeiJieMechanism
{
    // 触发筑基晋升金丹失败后的雷劫
        public static void TriggerZhuangJiToJinDanLeiJie(Actor actor)
        {
            if (actor == null)
                return;
            
        // 移除雷劫效果显示
        
        // 根据道基等级设置不同的扣血量，道基不再降级
        if (actor.hasTrait("DaoJi4"))
        {
            // DaoJi4（极品道基）突破失败扣30%血量
            actor.restoreHealth((int)(-actor.data.health * 0.3f));
        }
        else if (actor.hasTrait("DaoJi3"))
        {
            // DaoJi3（上品道基）突破失败扣50%血量
            actor.restoreHealth((int)(-actor.data.health * 0.5f));
        }
        else if (actor.hasTrait("DaoJi2"))
        {
            // DaoJi2（中品道基）突破失败扣70%血量
            actor.restoreHealth((int)(-actor.data.health * 0.7f));
        }
        else if (actor.hasTrait("DaoJi1"))
        {
            // DaoJi1（下品道基）突破失败扣90%血量
            actor.restoreHealth((int)(-actor.data.health * 0.9f));
        }
        else
            {
                // 虽然晋升失败，但并未引发雷劫
            }
    }

    // 触发金丹晋升元婴失败后的雷劫
        public static void TriggerJinDanToYuanYingLeiJie(Actor actor)
        {
            if (actor == null)
                return;
            
        // 移除雷劫效果显示
        
        // 根据道基等级设置不同的扣血量，道基不再降级
        if (actor.hasTrait("DaoJi8"))
        {
            // DaoJi8（极品道基）突破失败扣30%血量
            actor.restoreHealth((int)(-actor.data.health * 0.3f));
        }
        else if (actor.hasTrait("DaoJi7"))
        {
            // DaoJi7（上品道基）突破失败扣50%血量
            actor.restoreHealth((int)(-actor.data.health * 0.5f));
        }
        else if (actor.hasTrait("DaoJi6"))
        {
            // DaoJi6（中品道基）突破失败扣70%血量
            actor.restoreHealth((int)(-actor.data.health * 0.7f));
        }
        else if (actor.hasTrait("DaoJi5"))
        {
            // DaoJi5（下品道基）突破失败扣90%血量
            actor.restoreHealth((int)(-actor.data.health * 0.9f));
        }
        else
            {
                // 虽然晋升失败，但并未引发雷劫
            }
    }

    // 触发元婴晋升化神失败后的雷劫
        public static void TriggerYuanYingToHuaShenLeiJie(Actor actor)
        {
            if (actor == null)
                return;
            
        // 移除雷劫效果显示
        
        // 根据道基等级设置不同的扣血量，道基不再降级
        if (actor.hasTrait("DaoJi93"))
        {
            // DaoJi93（极品道基）突破失败扣30%血量
            actor.restoreHealth((int)(-actor.data.health * 0.3f));
        }
        else if (actor.hasTrait("DaoJi92"))
        {
            // DaoJi92（上品道基）突破失败扣50%血量
            actor.restoreHealth((int)(-actor.data.health * 0.5f));
        }
        else if (actor.hasTrait("DaoJi91"))
        {
            // DaoJi91（中品道基）突破失败扣70%血量
            actor.restoreHealth((int)(-actor.data.health * 0.7f));
        }
        else if (actor.hasTrait("DaoJi9"))
        {
            // DaoJi9（下品道基）突破失败扣90%血量
            actor.restoreHealth((int)(-actor.data.health * 0.9f));
        }
        else
            {
                // 虽然晋升失败，但并未引发雷劫
            }
    }

    // 显示雷劫效果方法已移除

    // 触发化神晋升合体失败后的雷劫
    public static void TriggerHuaShenToHeTiLeiJie(Actor actor)
    {
        if (actor == null)
            return;
            
        // 移除雷劫效果显示
        
        // 根据道基等级设置不同的扣血量，道基不再降级
        if (actor.hasTrait("DaoJi97"))
        {
            // DaoJi97（极品道基）突破失败扣30%血量
            actor.restoreHealth((int)(-actor.data.health * 0.3f));
        }
        else if (actor.hasTrait("DaoJi96"))
        {
            // DaoJi96（上品道基）突破失败扣50%血量
            actor.restoreHealth((int)(-actor.data.health * 0.5f));
        }
        else if (actor.hasTrait("DaoJi95"))
        {
            // DaoJi95（中品道基）突破失败扣70%血量
            actor.restoreHealth((int)(-actor.data.health * 0.7f));
        }
        else if (actor.hasTrait("DaoJi94"))
        {
            // DaoJi94（下品道基）突破失败扣90%血量
            actor.restoreHealth((int)(-actor.data.health * 0.9f));
        }
        else
            {
                // 虽然晋升失败，但并未引发雷劫
            }
    }

    // 触发合体晋升大乘失败后的雷劫
    public static void TriggerHeTiToDaChengLeiJie(Actor actor)
    {
        if (actor == null)
            return;

        if (actor.hasTrait("FaXiang4"))
        {
            // FaXiang4（极品法相）突破失败扣30%血量
            actor.restoreHealth((int)(-actor.data.health * 0.3f));
        }
        else if (actor.hasTrait("FaXiang3"))
        {
            // FaXiang3（上品法相）突破失败扣50%血量
            actor.restoreHealth((int)(-actor.data.health * 0.5f));
        }
        else if (actor.hasTrait("FaXiang2"))
        {
            // FaXiang2（中品法相）突破失败扣70%血量
            actor.restoreHealth((int)(-actor.data.health * 0.7f));
        }
        else if (actor.hasTrait("FaXiang1"))
        {
            // FaXiang1（下品法相）突破失败扣90%血量
            actor.restoreHealth((int)(-actor.data.health * 0.9f));
        }
        else
            {
                // 虽然晋升失败，但并未引发雷劫
            }
    }

    // 触发大乘晋升地仙失败后的雷劫
    public static void TriggerDaChengToDiXianLeiJie(Actor actor)
    {
        if (actor == null)
            return;
            
        // 显示雷劫效果
        ShowLeiJieEffect(actor);
        
        // 根据法相等级设置不同的扣血量，法相不再降级
        if (actor.hasTrait("FaXiang8"))
        {
            // FaXiang8（极品法相）突破失败扣30%血量
            Debug.Log(string.Format(LM.Get("leiJie_survive"), actor.getName()));
            // 通知UI已移除
            actor.restoreHealth((int)(-actor.data.health * 0.3f));
        }
        else if (actor.hasTrait("FaXiang7"))
        {
            // FaXiang7（上品法相）突破失败扣50%血量
            Debug.Log(string.Format(LM.Get("leiJie_survive"), actor.getName()));
            CreateNotificationUI(string.Format(LM.Get("leiJie_survive"), actor.getName()));
            actor.restoreHealth((int)(-actor.data.health * 0.5f));
        }
        else if (actor.hasTrait("FaXiang6"))
        {
            // FaXiang6（中品法相）突破失败扣70%血量
            Debug.Log(string.Format(LM.Get("leiJie_survive"), actor.getName()));
            CreateNotificationUI(string.Format(LM.Get("leiJie_survive"), actor.getName()));
            actor.restoreHealth((int)(-actor.data.health * 0.7f));
        }
        else if (actor.hasTrait("FaXiang5"))
        {
            // FaXiang5（下品法相）突破失败扣90%血量
            Debug.Log(string.Format(LM.Get("leiJie_survive"), actor.getName()));
            CreateNotificationUI(string.Format(LM.Get("leiJie_survive"), actor.getName()));
            actor.restoreHealth((int)(-actor.data.health * 0.9f));
        }
        else
        {
            Debug.Log(string.Format("{0} 虽然晋升失败，但并未引发雷劫。", actor.getName()));
            // 通知UI已移除
        }
    }

    // 杀死角色 - 参考诡秘之主体系实现
    private static void KillActor(Actor actor, string deathReasonKey)
    {
        if (actor == null || actor.data.health <= 0)
            return;
            
        string deathReason = LM.Get(deathReasonKey);
        
        // 移除死亡通知
        
        // 使用游戏原生的死亡机制
        // 调用actor.die()方法处理死亡，这将触发游戏中的死亡逻辑链
        try
        {
            actor.die();
        }
        catch (Exception e)
        {
            // 异常处理：如果die()方法不可用，退回到清空血量的方式
            Debug.LogWarning("调用actor.die()方法失败，使用替代方案: " + e.Message);
            // 模拟死亡效果：清空血量
            actor.restoreHealth(-actor.data.health);
        }
        }

        // 实现CreateNotificationUI方法以解决编译错误
        private static void CreateNotificationUI(string message) {
            Debug.Log("[Notification] " + message);
        }
        
        // 实现ShowLeiJieEffect方法以解决编译错误
        private static void ShowLeiJieEffect(Actor actor) {
            Debug.Log("[LeiJieEffect] 显示雷劫效果 for " + actor.getName());
        }
    }
}