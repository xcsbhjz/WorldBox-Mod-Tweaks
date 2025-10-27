using System.Collections; 
using System.Collections.Generic;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using ai.behaviours;
using HarmonyLib;
using NeoModLoader.api.attributes;
using NeoModLoader.General;
using UnityEngine;
using ReflectionUtility;
using UnityEngine.UI;
using ai;
using System.Reflection;
using System.Reflection.Emit;
using VideoCopilot.code.utils;

// 导入神通命名类的命名空间
using XianTu.code;
using InterestingTrait.code.Config;

namespace XianTu.code
{
    internal class patch
    {
        // 标记窗口初始化状态的静态变量
        public static bool window_creature_info_initialized;
        private static bool _initialized = false;

        // 为UnitWindow的OnEnable方法添加前缀补丁，用于显示功法和职业信息
        [Hotfixable]
        [HarmonyPrefix, HarmonyPatch(typeof(UnitWindow), nameof(UnitWindow.OnEnable))]
        private static void OnEnable_prefix(UnitWindow __instance)
        {
            if (!(__instance.actor?.isAlive() ?? false)) return;

            Text info_text = null;
            if (!_initialized)
            {
                _initialized = true;
                var obj = new GameObject("GongFaInfo", typeof(Text), typeof(ContentSizeFitter));
                obj.transform.SetParent(__instance.transform.Find("Background"));
                obj.transform.localPosition = new(250, 0);
                obj.transform.localScale = Vector3.one; 
                obj.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize; 
                info_text = obj.GetComponent<Text>(); 
                info_text.font = LocalizedTextManager.current_font; 
                info_text.resizeTextForBestFit = true; 
                info_text.resizeTextMinSize = 1; 
                info_text.resizeTextMaxSize = 8; 
            } 
            else 
            { 
                info_text = __instance.transform.Find("Background/GongFaInfo").GetComponent<Text>(); 
            } 

            var sb = new StringBuilder(); 
            
            // 添加五行属性或灵根信息
            float age = (float)__instance.actor.getAge();
            bool hasCheckedSpiritualRoot = __instance.actor.HasCheckedSpiritualRoot();
            
            // 添加世界信息 - 角色>100岁且拥有XianTu9境界时显示
            if (age > 10f && (__instance.actor.hasTrait("XianTu9") || __instance.actor.hasTrait("XianTu91")))
            {
                // 根据角色拥有的特质显示对应的世界等级
                string worldLevelName = "小世界"; // 默认小世界
                if (__instance.actor.hasTrait("FaXiang93"))
                {
                    worldLevelName = "混沌世界";
                }
                else if (__instance.actor.hasTrait("FaXiang92"))
                {
                    worldLevelName = "大千世界";
                }
                else if (__instance.actor.hasTrait("FaXiang91"))
                {
                    worldLevelName = "小千世界";
                }
                else if (__instance.actor.hasTrait("FaXiang9"))
                {
                    worldLevelName = "小世界";
                }
                sb.AppendLine($"世界等级：{worldLevelName}");
                
                // 凡俗人口：等于角色自身真元XianTu值的0.01倍，并限制上限为99999999
                float mortalPopulation = Mathf.Min(__instance.actor.GetXianTu() * 1f, 99999999f);
                sb.AppendLine($"凡俗人口：{Mathf.FloorToInt(mortalPopulation)}");
                
                // 世界强者各境界数量
                sb.AppendLine("世界强者：");
                sb.AppendLine($"  一阶-锻体：{Mathf.FloorToInt(__instance.actor.GetWorldStrongFirst())}");
                sb.AppendLine($"  二阶-炼炁：{Mathf.FloorToInt(__instance.actor.GetWorldStrongSecond())}");
                sb.AppendLine($"  三阶-紫府：{Mathf.FloorToInt(__instance.actor.GetWorldStrongThird())}");
                sb.AppendLine($"  四阶-元神：{Mathf.FloorToInt(__instance.actor.GetWorldStrongFourth())}");
                sb.AppendLine($"  五阶-法相：{Mathf.FloorToInt(__instance.actor.GetWorldStrongFifth())}");
                sb.AppendLine($"  六阶-飞升：{Mathf.FloorToInt(__instance.actor.GetWorldStrongSixth())}");
                
                // 添加七阶、八阶、九阶强者显示（根据世界等级显示）
                if (__instance.actor.hasTrait("FaXiang91") || __instance.actor.hasTrait("FaXiang92") || __instance.actor.hasTrait("FaXiang93"))
                {
                    sb.AppendLine($"  七阶-仙人：{Mathf.FloorToInt(__instance.actor.GetWorldStrongSeventh())}");
                }
                
                if (__instance.actor.hasTrait("FaXiang92") || __instance.actor.hasTrait("FaXiang93"))
                {
                    sb.AppendLine($"  八阶-准圣：{Mathf.FloorToInt(__instance.actor.GetWorldStrongEighth())}");
                }
                
                if (__instance.actor.hasTrait("FaXiang93"))
                {
                    sb.AppendLine($"  九阶-圣人：{Mathf.FloorToInt(__instance.actor.GetWorldStrongNinth())}");
                }
                
                sb.AppendLine();
            }
            
            if (!hasCheckedSpiritualRoot && Mathf.FloorToInt(age) < 4)
            {
                // 4岁前显示五行属性，竖排显示
                sb.AppendLine("五行属性:");
                sb.AppendLine($"  金: {Mathf.FloorToInt(__instance.actor.GetMetal())}");
                sb.AppendLine($"  木: {Mathf.FloorToInt(__instance.actor.GetWood())}");
                sb.AppendLine($"  水: {Mathf.FloorToInt(__instance.actor.GetWater())}");
                sb.AppendLine($"  火: {Mathf.FloorToInt(__instance.actor.GetFire())}");
                sb.AppendLine($"  土: {Mathf.FloorToInt(__instance.actor.GetEarth())}");
                sb.AppendLine();
            }
            else if (hasCheckedSpiritualRoot)
            {
                // 4岁后显示灵根信息
                sb.AppendLine("灵根信息:");
                sb.AppendLine($"  {__instance.actor.GetSpiritualRootName()}");
                sb.AppendLine();
            }
            
            // 添加功法信息 
            sb.AppendLine($"功法层次: {__instance.actor.GetGongFaLevelName()}"); 
            sb.AppendLine($"功法名称: {__instance.actor.GetGongFaName()}"); 
            sb.AppendLine($"功法点: {Mathf.FloorToInt(__instance.actor.GetGongFaDian())}/{__instance.actor.GetGongFaPointLimit()}"); 
            // 添加神通信息
            sb.AppendLine($"神通名称: {__instance.actor.GetShenTongName()}"); 
            sb.AppendLine($"神通层次: {__instance.actor.GetShenTongLevelName()}"); 
            sb.AppendLine($"推演点: {Mathf.FloorToInt(__instance.actor.GetTuiYanDian())}/{__instance.actor.GetTuiYanDianLimit()}"); 
            
            // 添加职业信息 - 三千大道职业
            sb.AppendLine("职业信息:"); 
            if (__instance.actor.hasTrait("XiuxianLiuyi1")) 
                sb.AppendLine("  " + __instance.actor.GetCareerTitle()); 
            else if (__instance.actor.hasTrait("XiuxianLiuyi2")) 
                sb.AppendLine("  " + __instance.actor.GetCareerTitle()); 
            else if (__instance.actor.hasTrait("XiuxianLiuyi3")) 
                sb.AppendLine("  " + __instance.actor.GetCareerTitle()); 
            else if (__instance.actor.hasTrait("XiuxianLiuyi4")) 
                sb.AppendLine("  " + __instance.actor.GetCareerTitle()); 
            else if (__instance.actor.hasTrait("XiuxianLiuyi5")) 
                sb.AppendLine("  " + __instance.actor.GetCareerTitle()); 
            else if (__instance.actor.hasTrait("XiuxianLiuyi6")) 
                sb.AppendLine("  " + __instance.actor.GetCareerTitle()); 
            else if (__instance.actor.hasTrait("XiuxianLiuyi7")) 
                sb.AppendLine("  " + __instance.actor.GetCareerTitle()); 
            else if (__instance.actor.hasTrait("XiuxianLiuyi8")) 
                sb.AppendLine("  " + __instance.actor.GetCareerTitle()); 
            else if (__instance.actor.hasTrait("XiuxianLiuyi9")) 
                sb.AppendLine("  " + __instance.actor.GetCareerTitle()); 
            else 
                sb.AppendLine("  无"); 
            
            // 添加职业进度信息
            sb.AppendLine("职业进度:"); 
            if (__instance.actor.hasTrait("XiuxianLiuyi1") || __instance.actor.hasTrait("XiuxianLiuyi2") || 
                __instance.actor.hasTrait("XiuxianLiuyi3") || __instance.actor.hasTrait("XiuxianLiuyi4") || 
                __instance.actor.hasTrait("XiuxianLiuyi5") || __instance.actor.hasTrait("XiuxianLiuyi6") ||
                __instance.actor.hasTrait("XiuxianLiuyi7") || __instance.actor.hasTrait("XiuxianLiuyi8") ||
                __instance.actor.hasTrait("XiuxianLiuyi9")) 
            {
                sb.AppendLine("  " + __instance.actor.GetCareerProgressText()); 
            }
            else
            {
                sb.AppendLine("  无"); 
            }
                
            info_text.text = sb.ToString();
        }

        // 为UnitWindow的OnEnable方法添加后缀补丁
        [HarmonyPostfix]
        [HarmonyPatch(typeof(UnitWindow), nameof(UnitWindow.OnEnable))]
        private static void WindowCreatureInfo_OnEnable_postfix(UnitWindow __instance)
        {
            // 如果实例的actor为空或已死亡，则直接返回
            if (__instance.actor == null || !__instance.actor.isAlive())
                return;
            
            // 添加延迟初始化
            __instance.StartCoroutine(DelayedInit(__instance));
        }

        // 延迟初始化方法，确保原生UI完成初始化后再执行自定义UI初始化
        private static IEnumerator DelayedInit(UnitWindow window)
        {
            // 等待一帧确保原生UI完成初始化
            yield return null;
    
            // 如果窗口信息未初始化，则进行初始化
            if (!window_creature_info_initialized) {
                UnitWindowStatsIcon.Initialize(window);
                window_creature_info_initialized = true;
            }
    
            // 启用窗口统计图标
            UnitWindowStatsIcon.OnEnable(window, window.actor);
        }

        // 为MapAction的checkLightningAction方法添加前缀补丁
        [HarmonyPrefix, HarmonyPatch(typeof(MapAction), "checkLightningAction")]
        static bool checkLightningAction(Vector2Int pPos, int pRad)
        {
            bool flag = false;
            // 获取世界中所有单位的列表
            List<Actor> simpleList = World.world.units.getSimpleList();
            
            // 遍历所有单位，检查是否在闪电范围内
            for (int i = 0; i < simpleList.Count; i++)
            {
                Actor actor = simpleList[i];
                if (Toolbox.DistVec2(actor.current_tile.pos, pPos) <= (float)pRad)
                {
                    // 如果是神之手指，则执行相应动作
                    if (actor.asset.flag_finger)
                    {
                        actor.getActorComponent<GodFinger>().lightAction();
                    }
                    else
                    {
                        // 如果不是不朽且随机概率为20%，设置标志位
                        if (!flag && !actor.hasTrait("immortal") && Randy.randomChance(0.2f))
                        {
                            flag = true;
                        }
                    }
                }
            }
            
            // 返回false跳过原方法执行
            return false;
        }

        // 为Actor的makeStunned方法添加前缀补丁
        [HarmonyPrefix, HarmonyPatch(typeof(Actor), "makeStunned")]
        static bool makeStunned(Actor __instance, float pTime = 5f)
        {
            // 随机增加眩晕时间的10%
            pTime += Randy.randomFloat(0f, pTime * 0.1f);
            // 取消所有行为
            __instance.cancelAllBeh();
            // 设置等待时间
            __instance.makeWait(pTime);
            
            // 如果没有防火特质，则添加眩晕状态效果，否则不添加眩晕状态效果
            if (!__instance.hasTrait("fire_proof"))
            {
                if (__instance.addStatusEffect("stunned", pTime, true))
                {
                    __instance.finishAngryStatus();
                }
            }
            
            // 返回false跳过原方法执行
            return false;
        }

        // 为ActionLibrary的addStunnedEffectOnTarget20方法添加前缀补丁
        [HarmonyPrefix, HarmonyPatch(typeof(ActionLibrary), "addStunnedEffectOnTarget20")]
        // 添加眩晕效果到目标（20%概率）
        static bool addStunnedEffectOnTarget20_Patch(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标是否有效（未死亡、非建筑）
            if (pTarget.isRekt() || pTarget.isBuilding())
            {
                return false;
            }

            // 检查随机概率（20%）
            if (!Randy.randomChance(0.2f))
            {
                return false;
            }

            // 检查目标是否具有防火特质
            if (pTarget.isActor() && pTarget.a.hasTrait("fire_proof"))
            {
                return false;
            }

            // 调用眩晕方法
            return ActionLibrary.addStunnedEffectOnTarget(pSelf, pTarget, pTile);
        }

        // 为Actor的getHit方法添加前缀补丁
        [HarmonyPrefix, HarmonyPatch(typeof(Actor), "getHit")]
        static bool actorGetHit_prefix(
            Actor __instance,
            ref float pDamage,
            bool pFlash,
            AttackType pAttackType,
            BaseSimObject pAttacker,
            bool pSkipIfShake,
            bool pMetallicWeapon,
            bool pCheckDamageReduction = true)
        {
            // 记录最后一次攻击类型
            __instance._last_attack_type = pAttackType;
            
            // 闪避机制
            if (pAttacker is Actor attackerActor && __instance.isAlive())
            {
                // 获取攻击者的准确率和目标的闪避率
                float attackerAccuracy = attackerActor.stats["Accuracy"];
                float targetDodge = __instance.stats["Dodge"];
                // 计算有效闪避率，确保在0-100之间
                float effectiveDodge = Mathf.Clamp(targetDodge - attackerAccuracy, 0f, 100f);

                // 如果随机概率超过有效闪避率，则闪避成功
                if (Randy.randomChance((float)(effectiveDodge / 100f)))
                {
                    __instance.startColorEffect(ActorColorEffect.White);
                    return false;
                }
            }
        
            // 死亡处理
            if (!__instance.hasHealth())
            {
                __instance.batch.c_check_deaths.Add(__instance);
            }
            
            // 检测目标是否会被击杀，如果是且攻击者是Actor，则给予击杀奖励
            if (pAttacker is Actor killerActor && __instance.isAlive() && __instance.data.health <= pDamage)
            {
                // 目标将被击杀，给予攻击者奖励
                killerActor.GetRewardForKilling(__instance);
            }
            
            // 返回true继续执行原方法
            return true;
        }
        
        // 为ActorTool的applyForceToUnit方法添加前缀补丁
        [HarmonyPrefix, HarmonyPatch(typeof(ActorTool), "applyForceToUnit")]
        public static bool ApplyForceToUnit_Postfix(Actor __instance, AttackData pData, BaseSimObject pTargetToCheck, float pMod = 1f, bool pCheckCancelJobOnLand = false)
        {
            // 计算力量值
            float tForce = pData.knockback * pMod;

            // 如果力量大于0且目标是Actor
            if (tForce > 0f && pTargetToCheck.isActor())
            {
                // 获取目标的抵抗值
                float resistValue = pTargetToCheck.a.stats["Resist"];
                // 计算实际力量，确保不小于0
                tForce = Mathf.Max(tForce - resistValue, 0);

                // 如果力量仍大于0，则应用力量
                if (tForce > 0f)
                {
                    Vector2 tPosStart = pTargetToCheck.cur_transform_position;
                    Vector2 tAttackVec = pData.hit_position;
                    pTargetToCheck.a.calculateForce(
                        tPosStart.x, tPosStart.y,
                        tAttackVec.x, tAttackVec.y,
                        tForce,
                        0f,
                        pCheckCancelJobOnLand
                    );
                }
            }
            
            // 返回false跳过原方法执行
            return false;
        }
    
        // 为Actor的updateAge方法添加后缀补丁
        [HarmonyPostfix, HarmonyPatch(typeof(Actor), "updateAge")]
        static void updateWorldTime_XianTuPostfix(Actor __instance)
        {
            // 如果实例为空，直接返回
            if (__instance == null)
            {
                return;
            }

            // 获取实例的年龄
            float age = (float)__instance.getAge();
            // 检查是否具有各种特质
            bool hasjinSheng10 = __instance.hasTrait("TaiyiLg9");// 检查是否具有特质
            bool hasjinSheng9 = __instance.hasTrait("TaiyiLg8");
            bool hasYizhiYoncun = __instance.hasTrait("TaiyiLg1"); // 检查是否拥有“意志永存”特质
            
            // 角色1岁时获得1~100的随机悟性
            if (Mathf.FloorToInt(age) == 1 && __instance.GetWuXing() == 0)
            {
                float randomWuXing = UnityEngine.Random.Range(1f, 101f);
                __instance.SetWuXing(randomWuXing);
            }
            
            // 灵根属性生成逻辑
            // 1岁时生成初始五行属性
            if (Mathf.FloorToInt(age) == 1 && __instance.GetMetal() == 0)
            {
                __instance.SetMetal(UnityEngine.Random.Range(1f, 101f));
                __instance.SetWood(UnityEngine.Random.Range(1f, 101f));
                __instance.SetWater(UnityEngine.Random.Range(1f, 101f));
                __instance.SetFire(UnityEngine.Random.Range(1f, 101f));
                __instance.SetEarth(UnityEngine.Random.Range(1f, 101f));
            }
            
            // 2-4岁每年增加1-10点五行属性
            if (Mathf.FloorToInt(age) >= 2 && Mathf.FloorToInt(age) <= 4)
            {
                __instance.ChangeMetal(UnityEngine.Random.Range(1f, 101f));
                __instance.ChangeWood(UnityEngine.Random.Range(1f, 101f));
                __instance.ChangeWater(UnityEngine.Random.Range(1f, 101f));
                __instance.ChangeFire(UnityEngine.Random.Range(1f, 101f));
                __instance.ChangeEarth(UnityEngine.Random.Range(1f, 101f));
            }
            
            // 根据悟性每年增加功法进度
            if (Mathf.FloorToInt(age) >= 1)
            {
                float wuXing = __instance.GetWuXing();
                // 每年增加的功法进度等于悟性值的0.5倍
                float gongFaDianIncrease = wuXing * 0.9f;
                __instance.ChangeGongFaDian(gongFaDianIncrease);
                
                // 根据功法层次每年额外增加不同数量的资源
                float currentGongFaDian = __instance.GetGongFaDian();
                
                // 检查角色是否有功法（至少达到炼炁阶）
                if (currentGongFaDian >= 10)
                {
                    // 检查是否有灵根特质
                    bool hasLingGen = __instance.hasTrait("TaiyiLg2") || __instance.hasTrait("TaiyiLg3") || 
                                     __instance.hasTrait("TaiyiLg4") || __instance.hasTrait("TaiyiLg5") || 
                                     __instance.hasTrait("TaiyiLg6") || __instance.hasTrait("TaiyiLg7");
                    
                    // 检查是否有武道资质特质
                    bool hasWuDao = __instance.hasTrait("TyGengu1") || __instance.hasTrait("TyGengu2") || 
                                    __instance.hasTrait("TyGengu3") || __instance.hasTrait("TyGengu4");
                    
                    float resourceIncrease = 0f;
                    
                    // 根据功法层次确定增加的资源数量
                    if (currentGongFaDian >= 900000) // 大罗阶
                        resourceIncrease = 1024f;
                    else if (currentGongFaDian >= 600000) // 不朽阶
                        resourceIncrease = 512f;
                    else if (currentGongFaDian >= 300000) // 混沌阶
                        resourceIncrease = 256f;
                    else if (currentGongFaDian >= 100000) // 大道阶
                        resourceIncrease = 128f;
                    else if (currentGongFaDian >= 50000) // 羽化阶
                        resourceIncrease = 64f;
                    else if (currentGongFaDian >= 10000) // 法相阶
                        resourceIncrease = 32f;
                    else if (currentGongFaDian >= 6000) // 元神阶
                        resourceIncrease = 16f;
                    else if (currentGongFaDian >= 3000) // 道胎阶
                        resourceIncrease = 8f;
                    else if (currentGongFaDian >= 1000) // 紫府阶
                        resourceIncrease = 4f;
                    else if (currentGongFaDian >= 100) // 道基阶
                        resourceIncrease = 2f;
                    else if (currentGongFaDian >= 10) // 炼炁阶
                        resourceIncrease = 1f;
                    
                    // 根据特质类型增加相应资源
                    if (resourceIncrease > 0f)
                    {
                        if (hasLingGen) // 有灵根特质，增加基础真元
                        {
                            __instance.ChangeXianTu(resourceIncrease);
                        }
                        else if (hasWuDao) // 有武道资质，增加气血
                        {
                            __instance.ChangeQiXue(resourceIncrease);
                        }
                    }
                }
            }
            // 整数年龄时（每年）根据灵根类型增加不同数量的真元
            if (Mathf.FloorToInt(age) == age)
            {
                // 检查是否有灵根
                bool hasLingGen = __instance.hasTrait("TaiyiLg2") || __instance.hasTrait("TaiyiLg3") || 
                                 __instance.hasTrait("TaiyiLg4") || __instance.hasTrait("TaiyiLg5") || 
                                 __instance.hasTrait("TaiyiLg6") || __instance.hasTrait("TaiyiLg7");
                
                if (hasLingGen)
                {
                    string spiritualRootName = __instance.GetSpiritualRootName();
                    float xianTuIncrease = 0f;
                    
                    // 根据灵根名称和特质判断灵根类型并增加相应真元
                    if (spiritualRootName.Contains("混沌灵根"))
                    {
                        xianTuIncrease = 3000f; // 混沌灵根每年增加3000点真元
                    }
                    else if (spiritualRootName.Contains("五行天灵根"))
                    {
                        xianTuIncrease = 1200f; // 五行天灵根每年增加1200点真元
                    }
                    else if (spiritualRootName.Contains("虚无灵根") || 
                             spiritualRootName.Contains("时间灵根") || 
                             spiritualRootName.Contains("太阴灵根") || 
                             spiritualRootName.Contains("纯阳灵根") || 
                             spiritualRootName.Contains("空间灵根"))
                    {
                        xianTuIncrease = 900f; // 特殊天灵根每年增加900点真元
                    }
                    else if (spiritualRootName.Contains("天灵根"))
                    {
                        xianTuIncrease = 700f; // 单一天灵根每年增加700点真元
                    }
                    else if (spiritualRootName.Contains("剑灵根") || 
                             spiritualRootName.Contains("风灵根") || 
                             spiritualRootName.Contains("冰灵根") || 
                             spiritualRootName.Contains("雷灵根") || 
                             spiritualRootName.Contains("地灵根"))
                    {
                        xianTuIncrease = 500f; // 变异灵根每年增加500点真元
                    }
                    else if (spiritualRootName.Contains("极品") && __instance.hasTrait("TaiyiLg5"))
                    {
                        xianTuIncrease = 300f; // 单属性极品灵根每年增加300点真元
                    }
                    else if (spiritualRootName.Contains("上品"))
                    {
                        xianTuIncrease = 100f; // 上品系列灵根每年增加100点真元
                    }
                    else if (spiritualRootName.Contains("中品"))
                    {
                        xianTuIncrease = 10f; // 中品系列灵根每年增加10点真元
                    }
                    else if (spiritualRootName.Contains("下品"))
                    {
                        xianTuIncrease = 1f; // 下品系列灵根每年增加1点真元
                    }
                    
                    // 增加相应数量的真元
                    if (xianTuIncrease > 0f)
                    {
                        __instance.ChangeXianTu(xianTuIncrease);
                    }
                }
            }

            // 检查是否拥有“武祖赐福”特质
            bool hasjinSheng8 = __instance.hasTrait("TaiyiLg7");
            if (hasjinSheng8 && XianTuConfig.ShouldAutoCollectForRealm("TaiyiLg7", true))
            {
                __instance.data.favorite = true;
            }

            // 出生时（年龄为1）有一定概率获得“意志永存”特质
            if (Mathf.FloorToInt(age) == 1 && !hasYizhiYoncun && Randy.randomChance(0.001f)) // 3% 的概率
            {
                __instance.addTrait("TaiyiLg1", false);
            }

            // 定义特定种族数组
            string[] basicRaces = { "orc", "human", "elf", "dwarf" };
            
            // 检查是否拥有仙途断绝特质
            bool hasTaiyiLg8 = __instance.hasTrait("TaiyiLg8");
            // 检查是否拥有帝血诅咒特质
            bool hasTaiyiLg91 = __instance.hasTrait("TaiyiLg91");

            // 4岁时进行天赋觉醒判定
            if (Mathf.FloorToInt(age) == 4 && 
                !HasAnyFlairTalen(__instance) &&
                (basicRaces.Contains(__instance.asset.id) || __instance.asset.id.StartsWith("civ_")) &&
                (!hasTaiyiLg91 || !hasTaiyiLg8) &&
                !__instance.HasCheckedSpiritualRoot())
            {
                // 首先判定觉醒仙道还是武道天赋
                // 随机决定觉醒方向：40%概率觉醒仙道，60%概率觉醒武道
                float randomDirection = UnityEngine.Random.value;
                bool isXianDaoDirection = randomDirection < 0.4f;
                
                if (isXianDaoDirection)
                {
                    // 如果拥有仙途断绝，跳过仙道天赋觉醒
                    if (!hasTaiyiLg8)
                    {
                        // 觉醒仙道天赋：使用五行属性觉醒灵根代码
                        DetermineSpiritualRoot(__instance);
                        __instance.SetHasCheckedSpiritualRoot(true);
                    }
                    else
                    {
                        // 拥有仙途断绝，不觉醒任何天赋，只是凡人
                        __instance.SetHasCheckedSpiritualRoot(true);
                    }
                }
                else
                {
                    // 如果拥有帝血诅咒，跳过武道天赋觉醒
                    if (hasTaiyiLg91)
                    {
                        // 拥有帝血诅咒，不觉醒任何天赋
                        __instance.SetHasCheckedSpiritualRoot(true);
                        return;
                    }
                    
                    // 觉醒武道天赋：标记已检查灵根但不添加灵根特质
                    __instance.SetHasCheckedSpiritualRoot(true);
                    // 继续执行原有的武道资质觉醒逻辑
                    // 如果已经拥有扮演法（因混沌帝血获得），则跳过此次随机晋升特质觉醒
                    // 如果已经拥有TyGengu1~4或TaiyiLg2~7中的任意一个特质，则跳过此次随机晋升特质觉醒
                    if (__instance.hasTrait("TyGengu1") || __instance.hasTrait("TyGengu2") || __instance.hasTrait("TyGengu3") || __instance.hasTrait("TyGengu4") || 
                        __instance.hasTrait("TaiyiLg2") || __instance.hasTrait("TaiyiLg3") || __instance.hasTrait("TaiyiLg4") || __instance.hasTrait("TaiyiLg5") || __instance.hasTrait("TaiyiLg6") || __instance.hasTrait("TaiyiLg7"))
                    {
                        return;
                    }


                // 确定角色拥有的最高等级血脉
                string highestBloodline = null;
                if (__instance.hasTrait("TyXuemai7")) highestBloodline = "TyXuemai7"; // 帝阶血脉
                else if (__instance.hasTrait("TyXuemai6")) highestBloodline = "TyXuemai6"; // 圣阶血脉
                else if (__instance.hasTrait("TyXuemai5")) highestBloodline = "TyXuemai5"; // 天阶血脉
                else if (__instance.hasTrait("TyXuemai4")) highestBloodline = "TyXuemai4"; // 地阶血脉
                else if (__instance.hasTrait("TyXuemai3")) highestBloodline = "TyXuemai3"; // 玄阶血脉
                else if (__instance.hasTrait("TyXuemai2")) highestBloodline = "TyXuemai2"; // 灵阶血脉
                else if (__instance.hasTrait("TyXuemai1")) highestBloodline = "TyXuemai1"; // 凡俗血脉

                // 100%概率触发觉醒，但有帝血诅咒时跳过血脉觉醒
                if (highestBloodline != null && !hasTaiyiLg91)
                {
                    // 生成随机数
                    float randomValue = UnityEngine.Random.value;
                    string selectedTrait = null; // 默认值设为null

                    // 根据不同等级血脉设置不同的觉醒概率分布（只觉醒武道资质，血脉不受仙途断绝限制）
                    switch (highestBloodline)
                    {
                        case "TyXuemai1": // 凡俗血脉 - 只能觉醒凡人之资或小有天资
                            if (randomValue < 0.7f)
                                selectedTrait = "TyGengu1"; // 70% 凡人之资
                            else
                                selectedTrait = "TyGengu2"; // 30% 小有天资
                            break;

                        case "TyXuemai2": // 灵阶血脉 - 主要觉醒小有天资，偶尔可觉醒凡人之资
                            if (randomValue < 0.3f)
                                selectedTrait = "TyGengu1"; // 30% 凡人之资
                            else
                                selectedTrait = "TyGengu2"; // 70% 小有天资
                            break;

                        case "TyXuemai3": // 玄阶血脉 - 主要觉醒武道奇才，可觉醒小有天资
                            if (randomValue < 0.3f)
                                selectedTrait = "TyGengu2"; // 30% 小有天资
                            else
                                selectedTrait = "TyGengu3"; // 70% 武道奇才
                            break;

                        case "TyXuemai4": // 地阶血脉 - 主要觉醒天生武骨，可觉醒武道奇才
                            if (randomValue < 0.3f)
                                selectedTrait = "TyGengu3"; // 30% 武道奇才
                            else
                                selectedTrait = "TyGengu4"; // 70% 天生武骨
                            break;

                        case "TyXuemai5": // 天阶血脉 - 必觉醒天生武骨
                            selectedTrait = "TyGengu4"; // 100% 天生武骨
                            break;

                        case "TyXuemai6": // 圣阶血脉 - 必觉醒天生武骨
                            selectedTrait = "TyGengu4"; // 100% 天生武骨
                            break;

                        case "TyXuemai7": // 帝阶血脉 - 必觉醒天生武骨
                            selectedTrait = "TyGengu4"; // 100% 天生武骨
                            break;
                    }

                    // 只有当selectedTrait不为null时才添加特质
                    if (selectedTrait != null)
                    {
                        __instance.addTrait(selectedTrait, false);
                    }
                }

                    else // 60%概率觉醒武道资质系列
                    {
                        // 定义各武道资质特质的权重
                        var wudaoWeights = new (string traitId, float weight)[]
                        {
                            ("TyGengu1", 64f),  // 凡人之资 (R1)
                            ("TyGengu2", 31f),  // 小有天资 (R1)
                            ("TyGengu3", 4.58f), // 武道奇才 (R2)
                            ("TyGengu4", 0.42f)  // 天生武骨 (R3)
                        };

                        // 计算总权重
                        float totalWeight = wudaoWeights.Sum(t => t.weight);

                        // 生成随机浮点数
                        float randomValue = UnityEngine.Random.Range(0f, totalWeight);

                        // 遍历权重选择特质
                        string selectedWudao = "TyGengu1"; // 默认值
                        foreach (var wudao in wudaoWeights)
                        {
                            if (randomValue < wudao.weight)
                            {
                                selectedWudao = wudao.traitId;
                                break;
                            }
                            randomValue -= wudao.weight;
                        }

                        // 添加选中的武道资质特质
                        __instance.addTrait(selectedWudao, false);
                    }
                }
            }

            // 特质增加序列值的处理
            var jinShengXianTuChanges = new Dictionary<string, (float, float)>
            {
                { "TaiyiLg2", (1.0f, 2.0f) },//魔药主材
                { "TaiyiLg3", (2.0f, 4.0f) },//完整魔药
                { "TaiyiLg4", (4.0f, 6.0f) },//消化法
                { "TaiyiLg5", (6.0f, 9.0f) },//扮演法
                { "TaiyiLg9", (-10.0f, -20.0f) },//灵性衰败
                { "TaiyiLg6", (9.0f, 12.0f) },//晋升仪式
                { "TaiyiLg7", (12.0f, 16.0f) },//旧日赐福
            };

            // 应用晋升特质对序列值的影响
            foreach (var change in jinShengXianTuChanges)
            {
                // 如果具有TaiyiLg9特质，并且当前特质是TaiyiLg2到TaiyiLg5，则跳过
                if ((hasjinSheng10 || hasjinSheng9) && (change.Key == "TaiyiLg2" || change.Key == "TaiyiLg3" || change.Key == "TaiyiLg4" || change.Key == "TaiyiLg5"))
                {
                    continue;
                }

                if (__instance.hasTrait(change.Key))
                {
                    // 在指定范围内随机增加序列值
                    float randomXianTuIncrease = UnityEngine.Random.Range(change.Value.Item1, change.Value.Item2);
                    __instance.ChangeXianTu(randomXianTuIncrease);
                }
            }

            // 年龄和概率条件增加特质的处理
            var XianTuTraitThresholds = new Dictionary<string, float>
            {
                { "XianTu1", 50f },
                { "XianTu2", 150f },
                { "XianTu3", 450f },
                { "XianTu4", 900f },
                { "XianTu5", 1800f },
                { "XianTu6", 4800f },
                { "XianTu7", 9900f },
                { "XianTu8", 90000f },
            };
            
            // 武道境界天人五衰触发阈值
            var WudaoTraitThresholds = new Dictionary<string, float>
            {
                { "TyWudao2", 50f },    // 先天
                { "TyWudao3", 70f },    // 真意
                { "TyWudao4", 90f },    // 天罡
                { "TyWudao5", 150f },   // 天罡境
                { "TyWudao6", 250f },   // 抱丹境
                { "TyWudao7", 300f },   // 神游境
                { "TyWudao8", 500f },   // 踏虚境
                { "TyWudao9", 900f },  // 天人境
                { "TyWudao91", 4500f }, // 圣躯境
                { "TyWudao92", 9000f } // 涅槃境
            };
            
            // 灵性衰败特质的触发概率
            const float jinSheng10Chance = 0.5f;
            
            // 处理仙道境界的天人五衰触发
            foreach (var threshold in XianTuTraitThresholds)
            {
                // 检查是否满足触发灵性衰败特质的条件
                if (__instance.hasTrait(threshold.Key) && age > threshold.Value && Randy.randomChance(jinSheng10Chance) && !hasYizhiYoncun)
                {
                    __instance.addTrait("TaiyiLg9", false);
                }
            }
            
            // 处理武道境界的天人五衰触发
            foreach (var threshold in WudaoTraitThresholds)
            {
                // 检查是否满足触发灵性衰败特质的条件
                if (__instance.hasTrait(threshold.Key) && age > threshold.Value && Randy.randomChance(jinSheng10Chance) && !hasYizhiYoncun)
                {
                    __instance.addTrait("TaiyiLg9", false);
                }
            }

            // 定义各境界的最大序列值
            var grades = new Dictionary<string, float>
            {
                { "XianTu1", 100f },
                { "XianTu2", 1000f },
                { "XianTu3", 10000f },
                { "XianTu4", 100000f },
                { "XianTu5", 300000f },
                { "XianTu6", 600000f },
                { "XianTu7", 1000000f },
                { "XianTu8", 3000000f },
                { "XianTu9", 6000000f },
                { "TaiYiyinji", 6000000f },
                { "XianTu91", 10000000f },
            };
            
            // 根据境界更新序列值上限
            foreach (var grade in grades)
            {
                UpdateXianTuBasedOnGrade(__instance, grade.Key, grade.Value);
            }
            
            // 根据仙途境界每年获得对应的灵石
            if (__instance.hasTrait("XianTu1")) // 练气期
            {
                __instance.ChangeLingShi(1f); // 每年获得1块下品灵石
            }
            else if (__instance.hasTrait("XianTu2")) // 筑基期
            {
                __instance.ChangeLingShi(10f); // 每年获得10块下品灵石
            }
            else if (__instance.hasTrait("XianTu3")) // 金丹期
            {
                __instance.ChangeLingShi(100f); // 每年获得100块下品灵石
            }
            else if (__instance.hasTrait("XianTu4")) // 元婴期
            {
                __instance.ChangeLingShi(1000f); // 每年获得1000块下品灵石
            }
            else if (__instance.hasTrait("XianTu5")) // 化神期
            {
                __instance.ChangeZhongPin(1f); // 每年获得1块中品灵石
            }
            else if (__instance.hasTrait("XianTu6")) // 合体期
            {
                __instance.ChangeZhongPin(10f); // 每年获得10块中品灵石
            }
            else if (__instance.hasTrait("XianTu7")) // 大乘期
            {
                __instance.ChangeZhongPin(100f); // 每年获得100块中品灵石
            }
            else if (__instance.hasTrait("XianTu8")) // 地仙期
            {
                __instance.ChangeZhongPin(1000f); // 每年获得1000块中品灵石
            }
            else if (__instance.hasTrait("XianTu9")) // 仙王境
            {
                __instance.ChangeShangPin(1f); // 每年获得1块上品灵石
            }
            
            // 根据武道境界每年获得对应的灵石（仅对没有修仙境界的角色生效）
            else if (__instance.hasTrait("TyWudao3")) // 换血境，对应练气期
            {
                __instance.ChangeLingShi(1f); // 每年获得1块下品灵石
            }
            else if (__instance.hasTrait("TyWudao4")) // 后天极境，介于换血境和先天境之间
            {
                __instance.ChangeLingShi(5f); // 每年获得5块下品灵石
            }
            else if (__instance.hasTrait("TyWudao5")) // 先天境，对应筑基期
            {
                __instance.ChangeLingShi(10f); // 每年获得10块下品灵石
            }
            else if (__instance.hasTrait("TyWudao6")) // 抱丹境，对应金丹期
            {
                __instance.ChangeLingShi(100f); // 每年获得100块下品灵石
            }
            else if (__instance.hasTrait("TyWudao7")) // 神游境，对应元婴期
            {
                __instance.ChangeLingShi(1000f); // 每年获得1000块下品灵石
            }
            else if (__instance.hasTrait("TyWudao8")) // 明意境，对应化神期
            {
                __instance.ChangeZhongPin(1f); // 每年获得1块中品灵石
            }
            else if (__instance.hasTrait("TyWudao9")) // 融天境，对应合体期
            {
                __instance.ChangeZhongPin(10f); // 每年获得10块中品灵石
            }
            else if (__instance.hasTrait("TyWudao91")) // 山海境，对应大乘期
            {
                __instance.ChangeZhongPin(100f); // 每年获得100块中品灵石
            }
            else if (__instance.hasTrait("TyWudao92")) // 帝境，对应地仙期
            {
                __instance.ChangeZhongPin(1000f); // 每年获得1000块中品灵石
            }

            // 道果系列特质每年增加100真元XianTu
            for (int i = 1; i <= 95; i++)
            {
                if (__instance.hasTrait("DaoGuo" + i))
                {
                    __instance.ChangeXianTu(1000f); // 每年增加100真元XianTu
                    break; // 只增加一次，即使拥有多个道果特质
                }
            }

            // 根据悟性每年增加职业进度
            __instance.UpdateCareerProgressByWuXing();
            
            // 根据悟性每年增加推演点
            __instance.UpdateTuiYanDianByWuXing();
            
            // 根据职业等级每年增加对应的灵石
            __instance.AddCareerResourceByLevel();
            
            // 检查并执行武道境界晋升
            WudaoPromotion.CheckWudaoPromotion(__instance);

            // 根据武道境界或修仙境界授予对应的血脉特质
            GrantBloodlineByRealm(__instance);
        }
        
        // 灵根判定方法
        static void DetermineSpiritualRoot(Actor actor)
        {
            // 获取五行属性值
            float metal = actor.GetMetal();
            float wood = actor.GetWood();
            float water = actor.GetWater();
            float fire = actor.GetFire();
            float earth = actor.GetEarth();
            
            // 存储五行属性和对应的名称
            var attributes = new Dictionary<string, float>
            {
                { "金", metal },
                { "木", wood },
                { "水", water },
                { "火", fire },
                { "土", earth }
            };
            
            // 获取最高属性
            var highestAttr = attributes.OrderByDescending(a => a.Value).FirstOrDefault();
            float highestValue = highestAttr.Value;
            string highestElement = highestAttr.Key;
            
            // 计算有多少属性达到极品或天灵根
            int excellentCount = attributes.Count(a => a.Value >= 361 && a.Value <= 390);
            int celestialCount = attributes.Count(a => a.Value >= 391 && a.Value <= 400);
            int allCelestialCount = attributes.Count(a => a.Value >= 400);
            
            string spiritualRootName = "灵根不显";
            string traitId = "";
            
            // 根据最高属性值判定灵根品质和名称
            if (highestValue >= 391 && highestValue <= 400) // 天灵根范围
            {
                if (celestialCount >= 5 && allCelestialCount >= 5) // 五行天灵根，且全部>=120
                {
                    spiritualRootName = "混沌灵根";
                    traitId = "TaiyiLg7";
                }
                else if (celestialCount >= 5) // 五行天灵根
                {
                    spiritualRootName = "五行天灵根";
                    traitId = "TaiyiLg7";
                }
                else if (celestialCount >= 2) // 2-4个天灵根
                {
                    // 取最高的两个属性
                    var topTwoAttrs = attributes.OrderByDescending(a => a.Value).Take(2).ToList();
                    string dominantElement = topTwoAttrs[0].Key;
                    
                    // 根据主导元素命名特殊灵根
                    switch (dominantElement)
                    {
                        case "金":
                            spiritualRootName = "虚无灵根";
                            break;
                        case "木":
                            spiritualRootName = "时间灵根";
                            break;
                        case "水":
                            spiritualRootName = "太阴灵根";
                            break;
                        case "火":
                            spiritualRootName = "纯阳灵根";
                            break;
                        case "土":
                            spiritualRootName = "空间灵根";
                            break;
                    }
                    traitId = "TaiyiLg7";
                }
                else // 单一天灵根
                {
                    spiritualRootName = highestElement + "天灵根";
                    traitId = "TaiyiLg7";
                }
            }
            else if (highestValue >= 361 && highestValue <= 390) // 极品灵根范围
            {
                if (excellentCount >= 2) // 变异灵根
                {
                    // 根据主导元素命名变异灵根
                    switch (highestElement)
                    {
                        case "金":
                            spiritualRootName = "剑灵根";
                            break;
                        case "木":
                            spiritualRootName = "风灵根";
                            break;
                        case "水":
                            spiritualRootName = "冰灵根";
                            break;
                        case "火":
                            spiritualRootName = "雷灵根";
                            break;
                        case "土":
                            spiritualRootName = "地灵根";
                            break;
                    }
                    traitId = "TaiyiLg6";
                }
                else // 单属性极品灵根
                {
                    spiritualRootName = "极品" + highestElement + "灵根";
                    traitId = "TaiyiLg5";
                }
            }
            else if (highestValue >= 301 && highestValue <= 360) // 上品灵根
            {
                spiritualRootName = "上品" + highestElement + "灵根";
                traitId = "TaiyiLg4";
            }
            else if (highestValue >= 101 && highestValue <= 200) // 中品灵根
            {
                spiritualRootName = "中品" + highestElement + "灵根";
                traitId = "TaiyiLg3";
            }
            else if (highestValue >= 5 && highestValue <= 100) // 下品灵根
            {
                spiritualRootName = "下品" + highestElement + "灵根";
                traitId = "TaiyiLg2";
            }
            else // 无灵根
            {
                spiritualRootName = "废灵根";
                traitId = null; // 无灵根时不添加任何特质
            }
            
            // 设置灵根名称
            actor.SetSpiritualRootName(spiritualRootName);
            
            // 添加对应的灵根特质
            if (!string.IsNullOrEmpty(traitId) && !actor.hasTrait(traitId))
            {
                actor.addTrait(traitId, false);
            }
        }
        
        // 世界强者晋升机制
        // 只有角色拥有XianTu9境界且年龄>100岁时才处理
        public static void UpdateWorldStrongPopulation(Actor actor)
        {
            if (actor == null || !actor.isAlive()) return;
            
            float age = (float)actor.getAge();
            
            if ((actor.hasTrait("XianTu9") || actor.hasTrait("XianTu91")) && age > 10f)
            {
                // 获取当前凡俗人口（等于角色的真元值的0.1倍），并限制上限为99999999
                float mortalPopulation = Mathf.Min(actor.GetXianTu() * 0.5f, 99999999f);
                
                // 1. 从凡俗人口晋升一阶-锻体
                float promoteToFirst = Mathf.Floor(mortalPopulation * 0.001f);
                // 确保是整数
                promoteToFirst = Mathf.RoundToInt(promoteToFirst);
                actor.ChangeWorldStrongFirst(promoteToFirst);
                
                // 2. 一阶-锻体晋升二阶-炼炁
                float firstLevelCount = actor.GetWorldStrongFirst();
                float promoteToSecond = 0f;
                // 一阶人口至少达到100万开始晋升
                if (firstLevelCount >= 100000f)
                {
                    promoteToSecond = Mathf.Floor(firstLevelCount * 0.001f);
                    // 确保是整数
                    promoteToSecond = Mathf.RoundToInt(promoteToSecond);
                    actor.ChangeWorldStrongFirst(-promoteToSecond);
                    actor.ChangeWorldStrongSecond(promoteToSecond);
                }
                
                // 3. 二阶-炼炁晋升三阶-紫府
                float secondLevelCount = actor.GetWorldStrongSecond();
                float promoteToThird = 0f;
                // 二阶人口至少达到10万开始晋升
                if (secondLevelCount >= 100000f)
                {
                    promoteToThird = Mathf.Floor(secondLevelCount * 0.001f);
                    // 确保是整数
                    promoteToThird = Mathf.RoundToInt(promoteToThird);
                    actor.ChangeWorldStrongSecond(-promoteToThird);
                    actor.ChangeWorldStrongThird(promoteToThird);
                }
                
                // 4. 三阶-紫府晋升四阶-元神
                float thirdLevelCount = actor.GetWorldStrongThird();
                float promoteToFourth = 0f;
                // 三阶人口至少达到1万开始晋升
                if (thirdLevelCount >= 100000f)
                {
                    promoteToFourth = Mathf.Floor(thirdLevelCount * 0.001f);
                    // 确保是整数
                    promoteToFourth = Mathf.RoundToInt(promoteToFourth);
                    actor.ChangeWorldStrongThird(-promoteToFourth);
                    actor.ChangeWorldStrongFourth(promoteToFourth);
                }
                
                // 5. 四阶-元神晋升五阶-法相
                float fourthLevelCount = actor.GetWorldStrongFourth();
                float promoteToFifth = 0f;
                // 四阶人口至少达到1千开始晋升
                if (fourthLevelCount >= 100000f)
                {
                    promoteToFifth = Mathf.Floor(fourthLevelCount * 0.0001f);
                    // 确保是整数
                    promoteToFifth = Mathf.RoundToInt(promoteToFifth);
                    actor.ChangeWorldStrongFourth(-promoteToFifth);
                    actor.ChangeWorldStrongFifth(promoteToFifth);
                }
                
                // 6. 五阶-法相晋升六阶-飞升
                float fifthLevelCount = actor.GetWorldStrongFifth();
                float promoteToSixth = 0f;
                // 五阶人口至少达到100开始晋升
                if (fifthLevelCount >= 100000f)
                {
                    promoteToSixth = Mathf.Floor(fifthLevelCount * 0.0001f);
                    // 确保是整数
                    promoteToSixth = Mathf.RoundToInt(promoteToSixth);
                    actor.ChangeWorldStrongFifth(-promoteToSixth);
                    actor.ChangeWorldStrongSixth(promoteToSixth);
                }
                
                // 7. 世界晋升系统：小世界 -> 小千世界 -> 大千世界 -> 混沌世界
                float sixthLevelCount = actor.GetWorldStrongSixth();
                
                // 7.1 当六阶强者达到3000时，小世界晋升小千世界
                if (sixthLevelCount >= 3000f && actor.hasTrait("FaXiang9") && !actor.hasTrait("FaXiang91"))
                {
                    // 世界晋升：小世界 -> 小千世界
                    actor.removeTrait("FaXiang9");
                    actor.addTrait("FaXiang91");
                }
                
                // 7.2 六阶-飞升晋升七阶-仙人（需要小千世界，且六阶强者至少10000）
                float promoteToSeventh = 0f;
                if (sixthLevelCount >= 100000f && actor.hasTrait("FaXiang91"))
                {
                    promoteToSeventh = Mathf.Floor(sixthLevelCount * 0.001f);
                    promoteToSeventh = Mathf.RoundToInt(promoteToSeventh);
                    actor.ChangeWorldStrongSixth(-promoteToSeventh);
                    actor.ChangeWorldStrongSeventh(promoteToSeventh);
                }
                
                float seventhLevelCount = actor.GetWorldStrongSeventh();
                
                // 7.3 当七阶强者达到3000时，小千世界晋升大千世界
                if (seventhLevelCount >= 3000f && actor.hasTrait("FaXiang91") && !actor.hasTrait("FaXiang92"))
                {
                    // 世界晋升：小千世界 -> 大千世界
                    actor.removeTrait("FaXiang91");
                    actor.addTrait("FaXiang92");
                }
                
                // 7.4 七阶-仙人晋升八阶-准圣（需要大千世界，且七阶强者至少10000）
                float promoteToEighth = 0f;
                if (seventhLevelCount >= 100000f && actor.hasTrait("FaXiang92"))
                {
                    promoteToEighth = Mathf.Floor(seventhLevelCount * 0.001f);
                    promoteToEighth = Mathf.RoundToInt(promoteToEighth);
                    actor.ChangeWorldStrongSeventh(-promoteToEighth);
                    actor.ChangeWorldStrongEighth(promoteToEighth);
                }
                
                float eighthLevelCount = actor.GetWorldStrongEighth();
                
                // 7.5 当八阶强者达到3000时，大千世界晋升混沌世界
                if (eighthLevelCount >= 3000f && actor.hasTrait("FaXiang92") && !actor.hasTrait("FaXiang93"))
                {
                    // 世界晋升：大千世界 -> 混沌世界
                    actor.removeTrait("FaXiang92");
                    actor.addTrait("FaXiang93");
                }
                
                // 7.6 八阶-准圣晋升九阶-圣人（需要混沌世界，且八阶强者至少10000）
                float promoteToNinth = 0f;
                if (eighthLevelCount >= 100000f && actor.hasTrait("FaXiang93"))
                {
                    promoteToNinth = Mathf.Floor(eighthLevelCount * 0.00001f);
                    promoteToNinth = Mathf.RoundToInt(promoteToNinth);
                    actor.ChangeWorldStrongEighth(-promoteToNinth);
                    actor.ChangeWorldStrongNinth(promoteToNinth);
                }
                
                // 8. 各阶强者和凡俗人口上限限制
                // 8.1 圣人(九阶)上限3000
                float ninthLevelCount = actor.GetWorldStrongNinth();
                if (ninthLevelCount > 3000f)
                {
                    actor.SetWorldStrongNinth(3000f);
                }
                
                // 8.2 八阶准圣上限129600
                eighthLevelCount = actor.GetWorldStrongEighth();
                if (eighthLevelCount > 129600f)
                {
                    actor.SetWorldStrongEighth(129600f);
                }
                
                // 8.3 七阶仙人上限129600
                seventhLevelCount = actor.GetWorldStrongSeventh();
                if (seventhLevelCount > 999999f)
                {
                    actor.SetWorldStrongSeventh(999999f);
                }
                
                // 8.4 六阶飞升上限129600
                sixthLevelCount = actor.GetWorldStrongSixth();
                if (sixthLevelCount > 9999999f)
                {
                    actor.SetWorldStrongSixth(9999999f);
                }
                
                // 8.5 五阶法相上限129600
                fifthLevelCount = actor.GetWorldStrongFifth();
                if (fifthLevelCount > 99999999f)
                {
                    actor.SetWorldStrongFifth(99999999f);
                }
                
                // 8.6 四阶元神上限129600
                fourthLevelCount = actor.GetWorldStrongFourth();
                if (fourthLevelCount > 99999999f)
                {
                    actor.SetWorldStrongFourth(99999999f);
                }
                
                // 8.7 三阶紫府上限129600
                thirdLevelCount = actor.GetWorldStrongThird();
                if (thirdLevelCount > 99999999f)
                {
                    actor.SetWorldStrongThird(99999999f);
                }
                
                // 8.8 二阶炼炁上限129600
                secondLevelCount = actor.GetWorldStrongSecond();
                if (secondLevelCount > 99999999f)
                {
                    actor.SetWorldStrongSecond(99999999f);
                }
                
                // 8.9 一阶锻体上限129600
                firstLevelCount = actor.GetWorldStrongFirst();
                if (firstLevelCount > 99999999f)
                {
                    actor.SetWorldStrongFirst(99999999f);
                }
                

            }
        }

        // 根据武道境界或修仙境界授予对应的血脉特质
        static void GrantBloodlineByRealm(Actor actor)
        {
            if (actor == null || !actor.isAlive()) return;

            // 定义血脉特质ID与境界的对应关系
            string[] bloodlineTraits = { "TyXuemai2", "TyXuemai3", "TyXuemai4", "TyXuemai5", "TyXuemai6", "TyXuemai7" };
            string[] realmTraits = { "XianTu1", "XianTu2", "XianTu3", "XianTu4", "XianTu5", "XianTu6" };
            string[] realmNames = { "练气期", "筑基期", "金丹期", "元婴期", "化神期", "合体期" };
            string[] bloodlineNames = { "练气血脉", "筑基血脉", "金丹血脉", "元婴血脉", "化神血脉", "道君血脉" };

            // 检查是否已经拥有任何血脉特质
            bool hasAnyBloodline = false;
            for (int i = 1; i <= 7; i++)
            {
                if (actor.hasTrait("TyXuemai" + i))
                {
                    hasAnyBloodline = true;
                    break;
                }
            }

            // 1. 检查武道宗师以上境界（TyWudao2或TyWudao3），如果有且没有血脉特质，则授予凡俗血脉(TyXuemai1)
            if ((actor.hasTrait("TyWudao2") || actor.hasTrait("TyWudao3")) && !hasAnyBloodline)
            {
                actor.addTrait("TyXuemai1", false);
                return; // 已经授予血脉，退出方法
            }

            // 2. 根据修仙境界授予对应的血脉特质
            for (int i = realmTraits.Length - 1; i >= 0; i--)
            {
                if (actor.hasTrait(realmTraits[i]))
                {
                    // 如果已经拥有当前境界或更高境界的血脉，则不再授予
                    if (hasAnyBloodline)
                    {
                        bool hasHigherOrEqualBloodline = false;
                        for (int j = i; j < bloodlineTraits.Length; j++)
                        {
                            if (actor.hasTrait(bloodlineTraits[j]))
                            {
                                hasHigherOrEqualBloodline = true;
                                break;
                            }
                        }
                        if (hasHigherOrEqualBloodline)
                        {
                            break;
                        }
                        // 如果拥有较低境界的血脉，则移除并授予当前境界的血脉
                        for (int j = 0; j < i; j++)
                        {
                            if (actor.hasTrait(bloodlineTraits[j]))
                            {
                                actor.removeTrait(bloodlineTraits[j]);
                            }
                        }
                    }
                    // 授予当前境界对应的血脉
                    actor.addTrait(bloodlineTraits[i], false);
                    break;
                }
            }
        }

        // 根据境界更新序列值上限的方法
        static void UpdateXianTuBasedOnGrade(Actor actor, string traitName, float maxXianTu)
        {
            if (actor.hasTrait(traitName))
            {
                float currentXianTu = actor.GetXianTu();
                // 设置序列值为当前值和最大值中的较小值
                float newValue = Mathf.Min(maxXianTu, currentXianTu);
                actor.SetXianTu(newValue);
            }
        }

        // 定义包含晋升、天赋和武道特质的数组
        private static readonly string[] FlairjinShengTraits = new[] { 
            "TaiyiLg2", "TaiyiLg3", "TaiyiLg4", "TaiyiLg5",  // 灵根系列
            "flair1", "flair2", "flair3", "flair4", "flair5", "flair6", "flair7", // 天赋系列
            "TyGengu1", "TyGengu2", "TyGengu3", "TyGengu4",  // 武道资质系列
            "TyWudao1", "TyWudao2", "TyWudao3", "TyWudao4", "TyWudao5", "TyWudao6", "TyWudao7", "TyWudao8", "TyWudao9", "TyWudao91", "TyWudao92"                // 武道境界系列
        };
        
        // 检查是否具有任何晋升或天赋特质的方法
        private static bool HasAnyFlairTalen(Actor actor)
        {
            foreach (var trait in FlairjinShengTraits)
            {
                if (actor.hasTrait(trait))
                {
                    return true;
                }
            }
            return false;
        }
        

    }
}