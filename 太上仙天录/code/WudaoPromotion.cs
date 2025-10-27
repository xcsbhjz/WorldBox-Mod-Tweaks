using UnityEngine;
using VideoCopilot.code.utils;
using System.Collections.Generic;
using System;
using InterestingTrait.code.Config;
using XianTu.code;
using HarmonyLib;
using NeoModLoader.api.attributes;
using NeoModLoader.General;
using UnityEngine;
using ReflectionUtility;
using System.Reflection;
using System.Reflection.Emit;

// 导入必要的命名空间
using static StatusLibrary;
using static BaseStats;

// 定义新的命名空间以避免冲突
namespace XianTu.code.WudaoExtend
{
    // 用于扩展状态效果
    public delegate void StatusFinishedDelegate(BaseSimObject obj, StatusAsset status_effect);
    
    public class StatusExtend
    {
        public StatusFinishedDelegate action_finished;
    }
}

// 为StatusAsset添加扩展方法支持
static class StatusAssetExtensions
{
    // 用于存储StatusAsset和其扩展的映射
    private static Dictionary<StatusAsset, XianTu.code.WudaoExtend.StatusExtend> statusExtensions = new Dictionary<StatusAsset, XianTu.code.WudaoExtend.StatusExtend>();
    
    // 获取StatusAsset的扩展
    public static XianTu.code.WudaoExtend.StatusExtend GetExtend(this StatusAsset status)
    {
        if (!statusExtensions.ContainsKey(status))
        {
            statusExtensions[status] = new XianTu.code.WudaoExtend.StatusExtend();
        }
        return statusExtensions[status];
    }
}

public class WudaoPromotion
{
    // 定义各武道境界的气血上限
    public static readonly Dictionary<string, float> wudaoQiXueMax = new Dictionary<string, float>
    {
        { "TyWudao1", 10f },
        { "TyWudao2", 30f },
        { "TyWudao3", 50f },
        { "TyWudao4", 100f },
        { "TyWudao5", 200f },
        { "TyWudao6", 500f },
        { "TyWudao7", 1000f },
        { "TyWudao8", 3000f },
        { "TyWudao9", 5000f },
        { "TyWudao91", 10000f },
        { "TyWudao92", 100000f },
        { "TyWudao93", 500000f },
        { "TyWudao94", 1000000f }
    };
    
    // 静态构造函数，用于初始化状态效果
    static WudaoPromotion()
    {
        // 初始化极尽升华状态
        InitializeJiJinShengHuaStatus();
        
        // 注册Harmony补丁
        try
        {
            Harmony harmony = new Harmony("XianTu.WudaoPromotion");
            harmony.PatchAll();
        }
        catch (Exception e)
        {
            Debug.LogError("[太上仙天录] Harmony补丁注册失败: " + e.Message);
        }
    }
    
    // 初始化极尽升华状态
    private static void InitializeJiJinShengHuaStatus()
    {
        try
        {
            // 检查状态是否已经存在
            if (AssetManager.status.get("JiJinShengHua") == null)
            {
                // 创建极尽升华状态
                StatusAsset JiJinShengHua = new StatusAsset();
                JiJinShengHua.id = "JiJinShengHua";
                JiJinShengHua.locale_id = "status_title_JiJinShengHua";
                JiJinShengHua.locale_description = "status_desc_JiJinShengHua";
                JiJinShengHua.path_icon = "Ring/JiJinShengHua";
                
                // 设置状态效果：增加50%攻击和50%生命
                JiJinShengHua.base_stats[CustomBaseStatsConstant.MultiplierDamage] = 5f; // 50%攻击力加成
                JiJinShengHua.base_stats[CustomBaseStatsConstant.MultiplierHealth] = 5f; // 50%生命值加成
                
                // 设置状态持续时间为30秒
                JiJinShengHua.duration = 120f;
                
                // 设置状态结束时的回调
                // 移除对不存在方法的调用
                
                // 添加到游戏中
                AssetManager.status.add(pAsset: JiJinShengHua);
                
                Debug.Log("[太上仙天录] 极尽升华状态初始化完成");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[太上仙天录] 极尽升华状态初始化失败: " + e.Message);
        }
    }
    
    // 极尽升华状态结束时的回调
    private static void OnJiJinShengHuaFinished(BaseSimObject obj, StatusAsset status_effect)
    {
        if (obj == null || !obj.isActor()) return;
        
        Actor actor = obj.a;
        
        // 状态结束后死亡
        if (actor.isAlive())
        {
            actor.die();
        }
    }
    
    // 武道前缀池
    private static readonly string[] _wudaoPrefixes = new string[]
    {
        "混元","太初","太玄","太始","太易","太清","玉清","上清","虚无","混沌","金庭","玉虚","紫霄","青冥","玄黄","鸿蒙",
        "乾元","坤元","离火","坎水","震雷","巽风","艮山","兑泽","太极","两仪","四象","八卦","九宫","十方","无量","无极",
        "元始","道德","灵宝","长生","不死","永恒","不朽","不灭","天罡","地煞","紫微","勾陈","玄武","朱雀","青龙","白虎",
        "麒麟","凤凰","神龟","蛟龙","鲲鹏","饕餮","混沌","穷奇","梼杌","赑屃","狴犴","睚眦","椒图","蒲牢","螭吻","狻猊",
        "趴蝮","负屃","霸下","貔貅","朝天","黄泉","九泉","幽都","冥府","地府","阴曹","冥司","幽府","幽宫","冥宫","幽殿",
        "冥殿","幽狱","冥狱","幽牢","冥牢","幽界","冥界","幽壤","冥壤","幽泉","冥泉","幽渊","冥渊","幽壑","冥壑","幽海",
        "冥海","幽湖","冥湖","幽溪","冥溪","幽涧","冥涧","幽山","冥山","幽岩","冥岩","幽石","冥石","幽林","冥林","幽草",
        "冥草","幽花","冥花","幽鸟","冥鸟","幽兽","冥兽","幽魂","冥魂","幽灵","冥灵","幽鬼","冥鬼","幽神","冥神","幽吏",
        "冥吏","幽差","冥差","阎罗","阎王","判官","奈何","忘川","彼岸","三途","鬼门","黄泉","望乡","森罗","枉死","地狱",
        "寒狱","沸狱","拔舌","铁树","盘狱","蒸狱","柱狱","刀狱","碓狱","裂狱","凌迟","斩狱","首狱","道藏","金简","无生",
        "玉牒","金策","玉书","天书","堪舆","符玄","玄机","奥秘","灵泽","玄泽","天浆","甘泽","嘉澍","甘霖","膏雨","瑞雨",
        "祥雨","喜雨","廉纤","轻丝","银竹","白雨","陵雨","暴雨","雷雨","风雨","霜雨","朝雨","暮雨","夜雨","东风",
        "南风","西风","北风","金风","朔风","花信","扶摇","灵籁","商风","暴风","疾风","霜露","雾霞","虹霓","雷电","云雪",
        "雹霰","雾凇","霜凇","冰凌","晴阴","晦明","霁雾","云开","雪霁","元日","元正","元春","元朔","岁首","岁旦","正旦",
        "正朝","新正","岁朝","端月","孟春","仲春","季春","春阳","阳春","芳春","青春","三春","九春","韵节","淑节","苍灵",
        "清和","朱明","炎夏","三夏","九夏","槐序","长夏","季夏","炎节","朱节","孟秋","仲秋","金秋","金天","三秋","九秋",
        "素秋","商秋","商节","三冬","九冬","玄冬","嘉平","岁除","除夜","元夜","元宵","上元","寒食","清明","上巳","端午",
        "端阳","重午","七夕","乞巧","中元","中秋","仲秋","月节","登高","重阳","重九","亚岁","琼浆","玉液","醍醐","流霞",
        "霞液","碧霞","紫云","白云","甘露","蓝英","绿华","嘉草","清友","瑶草","不夜","晚甘","王孙","余甘","苦口","云雾",
        "黄醅","白堕","壶觞","绿蚁","浊贤","忘忧","般若","扫愁","钓诗","曲秀","琼琚","琼瑶","琼玖","璆琳","琳琅","玉环",
        "玉瑛","玉琪","玉琦","玉璐","玉珂","玉瑾","玉瑜","玉瑗","玉璧","玉琮","玉璋","玉圭","玉璜","金钗","金簪","金珮",
        "金环","金玦","金钏","金镯","金珰","金瑛","金琪","金琦","金璐","金珂","金瑾","金瑜","银钗","银簪","银珮","银环",
        "银玦","银钏","银镯","银珰","银瑛","银琪","银琦","银璐","银珂","珍珠","明珠","夜明","明月","辟寒","辟暑","辟尘",
        "羲和","望舒","青女","东君","飞廉","勾陈","螣蛇","朱雀","玄武","青龙","白虎","岁星","荧惑","镇星","太白","辰星",
        "天皇","地皇","人皇","玉皇","紫微","勾陈","后土","文昌","武曲","天枢","天璇","天玑","天权","玉衡","开阳","摇光",
        "太乙","太极","仙禄","神禄","仙籍","神籍","仙骨","神骨","仙心","神心","仙性","神性","仙缘","神缘","仙根","神根",
        "仙界","瑶天","玉坛","璇霄","丹台","云阶","月地","神霄","绛阙","仙山","楼阁","悬圃","阆风","玄都","紫府","玉楼",
        "玄室","瑶池","翠水","弱渊","仙洲","灵洲","神洲","仙岛","灵岛","神岛","仙山","灵山","神山","仙府","灵府","神府",
        "仙宫","灵宫","神宫","仙殿","灵殿","神殿","仙观","灵观","神观","仙坛","灵坛","神坛","仙祠","灵祠","神祠","仙洞",
        "灵洞","神洞","仙窟","灵窟","神窟","仙泉","灵泉","神泉","仙池","灵池","神池","仙涧","灵涧","神涧","仙芝","灵芝",
        "灵符","神符","沧溟","沧渊","沧瀛","洪溟","九溟","溟蒙","鹏溟","翠微","崇阿","峭崿","崔嵬","玉嶂","清瑶","寒晶",
        "云梦","甘露","元酒","青罗","天藏","孟津","云梦","星质","云根","山骨","岱舆","员峤","方壶","瀛洲","蓬莱","不周",
        "瑶界","瑶天","玉坛","阆苑","玄洲","炎洲","长州","元洲","流洲","生洲","凤麟","祖洲","昆仑","玄圃","瑶池","翠水",
        "弱水","神渊","灵渊","碧潭","紫渊","丹溪","玉涧","琼川","瑶溪","灵泉","玉泉","仙涧","神川","碧流","紫澜","苍澜",
        "玄涛","沧波","洪涛","惊涛","骇浪","狂澜","碧波","清澜","澄波","静波","流波","逝波","浩波","烟波","碧落","青冥",
        "坤灵","扶光","望舒","星汉","北辰","穹苍","玄穹","苍昊","丹曦","婵娟","银汉","绛河","玉宇","灵曜","大矩","方仪",
        "后土","坤仪","金轮","冰镜","琼钩","玉鉴","金蟾","冰轮","白榆","应星","云川","银湾","落晖","西景","夕晖","扶摇",
        "灵籁","商寒","轻丝","银竹","灵泽","陵雨","白雨","跳珠","玄泽","天鼓","霹雳","连鼓","神斧","霆霓","玉虎","列缺",
        "飞电","飞火","天笑","天闪","纤凝","碧烟","天波","岚霏","玉叶","雯华","虹霓","玉桥","天弓","帝弓","彩练","水桩",
        "文虹","旱龙","寒酥","玉絮","六出","凝雨","素尘","冷絮","玉尘","瑞白","青女","威屑","青文","山巾","流岚","浮岚",
        "岚烟","春阳","阳春","芳春","青春","艳阳","三春","九春","阳节","昭节","韵节","淑节","苍灵","玄夜","宵分","道衍",
        "仙穹","神墟","魔渊","圣垣","灵墟","真境","虚极","元冥","化境","道枢","仙宸","神庭","魔境","圣墟","灵渊","真玄",
        "虚穹","元霄","化枢","道寰","仙墟","神渊","魔垣","圣境","灵穹","真冥","虚渊","元宸","化墟","道境","仙渊","神垣",
        "魔穹","圣玄","灵冥","真渊","虚宸","元墟","化境","道玄","仙冥","神穹","魔渊","圣墟","灵宸","真墟","虚境","元玄",
        "化枢","道渊","仙穹","神墟","魔境","圣垣","灵虚","真境","虚极","元冥","化境","道枢","仙宸","神庭","魔渊","圣墟",
        "灵渊","真玄","虚穹","元霄","化枢","道寰","仙墟","神渊","魔垣","圣境","灵穹","真冥","虚渊","元宸","化墟","道境",
        "仙渊","神垣","魔穹","圣玄","灵冥","真渊","虚宸","元墟","化境","道玄","仙冥","羲和","望舒","金轮","冰镜","琼钩",
        "玉鉴","金蟾","冰轮","婵娟","桂魄","素娥","飞镜","银蟾","玉盘","玉弓","悬弓","大明","太阴","夕照","朝暾","角宿",
        "亢宿","氐宿","房宿","心宿","尾宿","箕宿","斗宿","牛宿","女宿","虚宿","危宿","室宿","壁宿","奎宿","娄宿","胃宿",
        "昴宿","毕宿","觜宿","参宿","井宿","鬼宿","柳宿","星宿","张宿","翼宿","轸宿","岁星","荧惑","镇星","太白","辰星",
        "北辰","勾陈","螣蛇","朱雀","玄武","青龙","白虎","天枢","天璇","天玑","天权","玉衡","开阳","摇光","紫微","文昌",
        "武曲","华盖","三台","太微","天市","天狼","南门","天津","星汉","银汉","绛河","银湾","云汉","天河","天汉","星桥",
        "穹苍","玄穹","苍昊","玉宇","碧落","青冥","九霄","九天","中天","天际","天陲","星野","分野","星躔","斗建","岁差",
        "星陨","飞星","彗星","孛星","客星","瑞星","妖星","合璧","连珠","五星","日月","日蚀","月蚀","天狗","食月","极光",
        "天河","斗转","北辰","众星","星罗","星天","星月","沉星","残月","神芝","灵玉","仙宝","灵宝","神宝","仙符","参商",
        "太乙","太一","太上","太阴","太阳","紫霄","碧霄","云霄","青霞","玄霄","九霄","丹霞","云岭","苍梧","昆仑","青城",
        "罗浮","终南","王屋","天台","崂山","龙虎","茅山","武当","崆峒","中条","华山","泰山","嵩山","衡山","恒山","九华",
        "黄山","庐山","雁荡","武夷","普陀","峨眉","五台","三清","玉虚","碧游","玄都","金庭","玉庭","紫庭","黄庭","青庭",
        "玄庭","神庭","道庭","仙庭","灵庭","金阙","玉阙","紫阙","青阙","玄阙","神阙","道阙","仙阙","灵阙","天阙","金章",
        "玉章","紫章","青章","玄章","神章","道章","仙章","灵章","天章","金篆","玉篆","紫篆","青篆","玄篆","神篆","道篆",
        "仙篆","灵篆","天篆","金台","玉台","紫台","青台","玄台","神台","道台","仙台","灵台","洞天","紫阳","青冥","玄元", 
        "玉清","灵虚","清虚","太玄","紫虚","玉虚","赤霄","玉玦","玉簪","玉钗","玉珮","玉佩","玉钏","玉镯","玉珰","圣王",
        "璇玑","内景","太祖","青莲","无定","三世","太极","八极","烛龙","禅音","灭魔","九阳","寒阴","灵蛇","封魂","困妖",
        "罗刹","灵宝","金阙","玄都","浮花","长春","雷电","狮啸","百里","菩提","困佛","困仙","无形","星枢","无妄","九转",
        "玄冥","困魔","霸王","七弦","火龙","希夷","绝情","封龙","望月","养神","分花","八脉","六阴","八目","帝女","灵猴",
        "清净","北斗","地仙","五阳","天罡","凤凰","戮魂","风雷","昆仑","天府","欢喜","护龙","空冥","东海","素问","禹余",
        "天山","凌虚","化骨","游身","叱灵","锁天","来去","无终","混天","洞玄","九华","天兵","遁甲","六虚","崆峒","东华",
        "素女","守龙","金玉","归藏","如意","雌雄","千里","逍遥","宝色","泰山","五火","万里","仙虎","辟邪","风火","修罗",
        "养龙","两仪","化血","天火","掠影","苍狼","一阳","少阳","戮佛","仙猿","养魂","普贤","金蛇","失魂","诛魔","化魂",
        "白虎","玉虚","洞虚","通身","轮回","涅槃","万花","天魔","灵悟","坐忘","天仙","终南","太乙","护魔","摄魂","三尸",
        "玄微","戮魔","锁魔","戮仙","百仙","琼花","九鼎","抱元","灵兔","白虹","应龙","浮光","四海","混沌","失魂","天宝",
        "长生","擒龙","轩辕","须弥","鴛鴦","太始","天蝉","烟雨","移星","柔云","异相","合欢","九曜","八方","昆吾","慈悲",
        "无根","大衍","狮吼","白玉","太初","问情","紫阳","金身","开碑","乘龙","元始","素银","光相","开弓","素光","冥河",
        "青云","点苍","九宫","三元","火元","太元","灵鹤","护神","百魔","照天","罗烟","灵龟","桃花","一元","玄门","晶炎",
        "离梦","五色","八卦","一字","迷仙","衡山","紫微","九阴","易筋","荡魔","擒龙","伏仙","地宝","守龙","炼虚","开山",
        "六弦","控鹤","听雪","枯荣","异相","莲华","七香","天魔","伏龙","护天","清微","紫霞","养仙","玄阴","定慧","白云",
        "三才","诛神","还魂","灵枢","狂龙","镇魔","南海","大赤","天师","北海","缠丝","困天","混世","炼气","纯阴","混元",
        "莲花","灵鳌","斩妖","黑木","青焰","千机","残虹","太一","朝元","封神","哀牢","六丁","太元","绝天","七煞","泣灵",
        "养魔","三危","八卦","归元","迷仙","碎玉","困神","无上","七杀","天地","子母","青花","太阴","万刃","三清","太行",
        "紫云","西海","仙鹤","南华","四海","蓬莱","神龙","定慧","万剑","芙蓉","修罗","金阙","七宝","无量","飞燕","沛然",
        "定海","炼血","古佛","黑石","无影","照天","通天","勾魂","百花","达摩","毒龙","焰光","七巧","极乐","日月","玄武",
        "九炼","罡风","灵丹","离尘","道德","五龙","百炼","陨玉","百合","灭神","华山","噬魂","五行","千钧","十方","长生",
        "五雷","飞渡","天照","上清","阴阳","浑元","两界","神火","截天","灭地","五阴","缥缈","大荒","戮天","天宫","紫霄",
        "四象","昇阳","摘星","玉清","无涯","三生","归一","少阴","度难","照妖","封魔","真武","静禅","迷踪","化煞","封仙",
        "玄天","鸿运","赤霄","百毒","化影","神宝","降魔","无为","般若","南斗","恒山","正气","问天","百灵","诛妖","金霞",
        "金砂","纯阳","伏天","九幽","无相","无色","霹雳","九天","太微","天青","担山","参合","地煞","玉京","阳神","武神",
        "诛妖","伏魔","七星","养志","寒风","缠丝","沛然"
    };
    
    // 武道境界后缀映射
    private static readonly Dictionary<string, string> _wudaoRealmTitleSuffixMap = new Dictionary<string, string>
    {
        { "TyWudao7", "宗师" },   // 踏虚为尊者
        { "TyWudao8", "大宗师" },   // 明我为半圣
        { "TyWudao9", "尊者" },   // 山海为大圣
        { "TyWudao91", "半圣" },  // 圣躯为准帝
        { "TyWudao92", "大圣" },  // 证帝为大帝
        { "TyWudao93", "准帝" },  // 圣躯为圣躯
        { "ZhiZunYinJi", "至尊" },  // 圣躯为圣躯
        { "TyWudao94", "大帝" }   // 帝境为帝境
    };
    
    // 生成动态尊号
    private static string GenerateDynamicWudaoTitle(string traitId)
    {      
        if (_wudaoRealmTitleSuffixMap.TryGetValue(traitId, out string suffix))
        {
            // 对于山海境界（TyWudao9）及以上境界，随机选择2个不重复的前缀
            if (traitId == "TyWudao92" || traitId == "TyWudao93" || traitId == "TyWudao94" || traitId == "ZhiZunYinJi")
            {
                int firstIndex = UnityEngine.Random.Range(0, _wudaoPrefixes.Length);
                int secondIndex;
                // 确保选择的两个前缀不重复
                do
                {
                    secondIndex = UnityEngine.Random.Range(0, _wudaoPrefixes.Length);
                }
                while (firstIndex == secondIndex);
                
                string firstPrefix = _wudaoPrefixes[firstIndex];
                string secondPrefix = _wudaoPrefixes[secondIndex];
                return $"{firstPrefix}{secondPrefix}{suffix}";
            }
            else
            {
                // 其他境界保持原有逻辑，随机选择一个前缀
                string prefix = _wudaoPrefixes[UnityEngine.Random.Range(0, _wudaoPrefixes.Length)];
                return $"{prefix}{suffix}";
            }
        }
        return string.Empty;
    }
    
    // 应用武道尊号
    public static void ApplyWudaoTitle(Actor actor, string newTrait)
    {
        string currentName = actor.getName();
        
        // 1. 分离境界后缀（最后一个连字符后的内容）
        int lastDashIndex = currentName.LastIndexOf('-');
        string suffixPart = lastDashIndex > 0 
            ? currentName.Substring(lastDashIndex)
            : "";
        
        string basePart = lastDashIndex > 0 
            ? currentName.Substring(0, lastDashIndex).Trim()
            : currentName;
        
        // 2. 移除旧尊号（如果有）
        int dotIndex = basePart.IndexOf('·');
        if (dotIndex > 0)
        {
            basePart = basePart.Substring(dotIndex + 1).Trim();
        }
        
        // 3. 生成新尊号
        string title = GenerateDynamicWudaoTitle(newTrait);
        
        // 4. 设置新名称：新尊号 + · + 基础名称 + 境界后缀
        if (!string.IsNullOrEmpty(title))
        {
            actor.setName($"{title}·{basePart}{suffixPart}");
        }
    }
    // 检查并执行武道境界晋升
    public static void CheckWudaoPromotion(Actor actor)
    {
        // 获取当前气血值
        float qiXue = actor.GetQiXue();
        
        // 检查是否可以晋升炼皮（TyWudao1）- 已有更高境界则跳过
        if (!actor.hasTrait("TyWudao1") && !actor.hasTrait("TyWudao2") && !actor.hasTrait("TyWudao3") && !actor.hasTrait("TyWudao4") && !actor.hasTrait("TyWudao5") && !actor.hasTrait("TyWudao6") && !actor.hasTrait("TyWudao7") && !actor.hasTrait("TyWudao8") && !actor.hasTrait("TyWudao9") && !actor.hasTrait("TyWudao91") && !actor.hasTrait("TyWudao92") && !actor.hasTrait("TyWudao93") && !actor.hasTrait("TyWudao94") && qiXue >= 10f && !XianTuConfig.LimitWuDaoLianPi)
        {
            // 只要气血达到10点，就自动晋升为后天武者
            actor.addTrait("TyWudao1", false);
            // 添加后缀
            actor.UpdateNameSuffix("炼皮");
        }
        
        // 检查是否可以晋升锻骨（TyWudao2）- 已有更高境界则跳过
        if (actor.hasTrait("TyWudao1") && !actor.hasTrait("TyWudao2") && !actor.hasTrait("TyWudao3") && !actor.hasTrait("TyWudao4") && !actor.hasTrait("TyWudao5") && !actor.hasTrait("TyWudao6") && !actor.hasTrait("TyWudao7") && !actor.hasTrait("TyWudao8") && !actor.hasTrait("TyWudao9") && !actor.hasTrait("TyWudao91") && !actor.hasTrait("TyWudao92") && !actor.hasTrait("TyWudao93") && !actor.hasTrait("TyWudao94") && qiXue >= 50f && !XianTuConfig.LimitWuDaoDuanGu)
        {
            // 消耗50点气血
            actor.ChangeQiXue(-50f);
            
            // 根据武道资质决定晋升概率
            float promotionChance = 0.9f; // 基础概率20%
            
            // 根据资质提高晋升概率
            if (actor.hasTrait("TyGengu1")) promotionChance += 0.1f;  // 凡人之资 +10%
            if (actor.hasTrait("TyGengu2")) promotionChance += 0.2f;  // 小有天资 +20%
            if (actor.hasTrait("TyGengu3")) promotionChance += 0.3f;  // 武道奇才 +30%
            if (actor.hasTrait("TyGengu4")) promotionChance += 0.4f;  // 天生武骨 +40%
            
            // 保证概率不超过100%
            promotionChance = Mathf.Min(1f, promotionChance);
            
            // 如果有天人五衰特质，概率减半
            if (actor.hasTrait("TaiyiLg9"))
            {
                promotionChance *= 0.5f;
            }
            
            // 如果有天命印记特质，必定成功
            if (actor.hasTrait("TianDaoyinji"))
            {
                promotionChance = 1f;
            }
            
            if (UnityEngine.Random.value <= promotionChance)
            {
                actor.removeTrait("TyWudao1");
                actor.addTrait("TyWudao2", false);
                // 更新后缀
                actor.UpdateNameSuffix("锻骨");
            }
        
        }
        
        // 检查是否可以晋升炼脏（TyWudao3）- 已有更高境界则跳过
        if (actor.hasTrait("TyWudao2") && !actor.hasTrait("TyWudao3") && !actor.hasTrait("TyWudao4") && !actor.hasTrait("TyWudao5") && !actor.hasTrait("TyWudao6") && !actor.hasTrait("TyWudao7") && !actor.hasTrait("TyWudao8") && !actor.hasTrait("TyWudao9") && !actor.hasTrait("TyWudao91") && !actor.hasTrait("TyWudao92") && !actor.hasTrait("TyWudao93") && !actor.hasTrait("TyWudao94") && qiXue >= 150f && !XianTuConfig.LimitWuDaoHuanXue)
        {
            // 消耗100点气血
            actor.ChangeQiXue(-15f);
            
            // 根据武道资质决定晋升概率
            float promotionChance = 0.8f; // 基础概率10%
            
            // 根据资质提高晋升概率
            if (actor.hasTrait("TyGengu1")) promotionChance += 0.1f;  // 凡人之资 +10%
            if (actor.hasTrait("TyGengu2")) promotionChance += 0.2f;  // 小有天资 +20%
            if (actor.hasTrait("TyGengu3")) promotionChance += 0.3f;  // 武道奇才 +30%
            if (actor.hasTrait("TyGengu4")) promotionChance += 0.4f;  // 天生武骨 +50%
            
            // 保证概率不超过100%
            promotionChance = Mathf.Min(1f, promotionChance);
            
            // 如果有天人五衰特质，概率减半
            if (actor.hasTrait("TaiyiLg9"))
            {
                promotionChance *= 0.5f;
            }
            
            // 如果有天命印记特质，必定成功
            if (actor.hasTrait("TianDaoyinji"))
            {
                promotionChance = 1f;
            }
            
            if (UnityEngine.Random.value <= promotionChance)
            {
                actor.removeTrait("TyWudao2");
                actor.addTrait("TyWudao3", false);
                // 更新后缀
                actor.UpdateNameSuffix("换血");
            }
        }

        // 检查是否可以晋升武道大宗师（TyWudao4）- 已有更高境界则跳过
        if (actor.hasTrait("TyWudao3") && !actor.hasTrait("TyWudao4") && !actor.hasTrait("TyWudao5") && !actor.hasTrait("TyWudao6") && !actor.hasTrait("TyWudao7") && !actor.hasTrait("TyWudao8") && !actor.hasTrait("TyWudao9") && !actor.hasTrait("TyWudao91") && !actor.hasTrait("TyWudao92") && !actor.hasTrait("TyWudao93") && !actor.hasTrait("TyWudao94") && qiXue >= 250f && !XianTuConfig.LimitWuDaoLianZang)
        {
            // 消耗50点气血
            actor.ChangeQiXue(-25f);
            
            // 根据武道资质决定晋升概率
            float promotionChance = 0.7f; // 基础概率5%
            
            // 根据资质提高晋升概率
            if (actor.hasTrait("TyGengu1")) promotionChance += 0.1f;  // 凡人之资 +10%
            if (actor.hasTrait("TyGengu2")) promotionChance += 0.2f;  // 小有天资 +20%
            if (actor.hasTrait("TyGengu3")) promotionChance += 0.3f; // 武道奇才 +35%
            if (actor.hasTrait("TyGengu4")) promotionChance += 0.4f;  // 天生武骨 +60%
            
            // 保证概率不超过100%
            promotionChance = Mathf.Min(1f, promotionChance);
            
            // 如果有天人五衰特质，概率减半
            if (actor.hasTrait("TaiyiLg9"))
            {
                promotionChance *= 0.5f;
            }
            
            // 如果有天命印记特质，必定成功
            if (actor.hasTrait("TianDaoyinji"))
            {
                promotionChance = 1f;
            }
            
            if (UnityEngine.Random.value <= promotionChance)
            {
                actor.removeTrait("TyWudao3");
                actor.addTrait("TyWudao4", false);
                // 更新后缀
                actor.UpdateNameSuffix("炼脏");
                
                // 应用武道尊号 - 大宗师

                
                // 真意境突破成功，随机增加1~5悟性
                int addIntelligence = UnityEngine.Random.Range(1, 6);
                actor.data.get(strings.S.intelligence, out float currentIntelligence, 0);
                currentIntelligence += addIntelligence;
                actor.data.set(strings.S.intelligence, currentIntelligence);
            }
        }

        // 检查是否可以晋升天人境（TyWudao5）
        if (actor.hasTrait("TyWudao4") && !actor.hasTrait("TyWudao5") && !actor.hasTrait("TyWudao6") && !actor.hasTrait("TyWudao7") && !actor.hasTrait("TyWudao8") && !actor.hasTrait("TyWudao9") && !actor.hasTrait("TyWudao91") && !actor.hasTrait("TyWudao92") && !actor.hasTrait("TyWudao93") && !actor.hasTrait("TyWudao94") && qiXue >= 500f && !XianTuConfig.LimitWuDaoTianRen)
        {
            // 消耗100点气血
            actor.ChangeQiXue(-50f);
            
            // 根据武道资质决定晋升概率
            float promotionChance = 0.5f; // 基础概率2%
            
            // 根据资质提高晋升概率
            if (actor.hasTrait("TyGengu1")) promotionChance += 0.1f;  // 凡人之资 +10%
            if (actor.hasTrait("TyGengu2")) promotionChance += 0.2f;  // 小有天资 +20%
            if (actor.hasTrait("TyGengu3")) promotionChance += 0.3f;  // 武道奇才 +40%
            if (actor.hasTrait("TyGengu4")) promotionChance += 0.5f;  // 天生武骨 +70%
            
            // 获取功法层次名称
            string gongFaLevelName = actor.GetGongFaLevelName();
            
            // 根据功法等级提高晋升概率
            if (gongFaLevelName == "道基阶") promotionChance += 0.1f;  // 道基功法 +10%
            if (gongFaLevelName == "紫府阶") promotionChance += 0.2f;  // 紫府功法 +20%
            if (gongFaLevelName == "道胎阶") promotionChance += 0.3f;  // 道胎功法 +30%
            if (gongFaLevelName == "元神阶") promotionChance += 0.4f;  // 元神功法 +40%
            if (gongFaLevelName == "法相阶") promotionChance += 0.5f;  // 法相功法 +50%
            if (gongFaLevelName == "羽化阶") promotionChance += 0.6f;  // 羽化功法 +60%
            if (gongFaLevelName == "大道阶") promotionChance += 0.7f;  // 大道功法 +70%
            
            // 保证概率不超过100%
            promotionChance = Mathf.Min(1f, promotionChance);
            
            // 如果有天人五衰特质，概率减半
            if (actor.hasTrait("TaiyiLg9"))
            {
                promotionChance *= 0.5f;
            }
            
            // 如果有天命印记特质，必定成功
            if (actor.hasTrait("TianDaoyinji"))
            {
                promotionChance = 1f;
            }
            
            if (UnityEngine.Random.value <= promotionChance)
            {
                actor.removeTrait("TyWudao4");
                actor.addTrait("TyWudao5", false);
                // 更新后缀
                actor.UpdateNameSuffix("先天");
                
                // 应用武道尊号 - 大宗师
                
                string[] negativeTraitsToRemove = { "stupid", "short_sighted", "soft_skin", "fragile_trait", "ugly", "skin_burns", "one_eyed", "madness", "TaiyiLg9" };
                foreach (string trait in negativeTraitsToRemove)
                {
                    if (actor.hasTrait(trait))
                    {
                        actor.removeTrait(trait);
                    }
                }
                
                // 突破成功后添加指定正面特质
                string[] positiveTraitsToAdd = { "genius", "eagle_eyed", "strong_minded", "lucky", "immune", "sunblessed", "attractive", "acid_proof", "fire_proof", "freeze_proof", "poison_immunity" };
                foreach (string trait in positiveTraitsToAdd)
                {
                    if (!actor.hasTrait(trait))
                    {
                        actor.addTrait(trait);
                    }
                }
                
                // 天人境界突破后，随机获得修仙职业（如果还没有的话）
                string[] xianTuTraits = { "XiuxianLiuyi1", "XiuxianLiuyi2", "XiuxianLiuyi3", "XiuxianLiuyi4", "XiuxianLiuyi5", "XiuxianLiuyi6", "XiuxianLiuyi7", "XiuxianLiuyi8", "XiuxianLiuyi9" };
                
                // 检查角色是否已经拥有任何一种修仙职业
                bool hasProfession = false;
                foreach (string trait in xianTuTraits)
                {
                    if (actor.hasTrait(trait))
                    {
                        hasProfession = true;
                        break;
                    }
                }
                
                // 只有当角色没有任何修仙职业时，才随机分配一个
                if (!hasProfession)
                {
                    int randomIndex = UnityEngine.Random.Range(0, xianTuTraits.Length);
                    string selectedXianTuTrait = xianTuTraits[randomIndex];
                    actor.addTrait(selectedXianTuTrait);
                }
                
                // 通玄境突破成功，随机增加1~10悟性
                int addWuXing = UnityEngine.Random.Range(1, 11);
                actor.ChangeWuXing(addWuXing);
            }
        }

        // 检查是否可以晋升神游境（TyWudao6）
        if (actor.hasTrait("TyWudao5") && !actor.hasTrait("TyWudao6") && !actor.hasTrait("TyWudao7") && !actor.hasTrait("TyWudao8") && !actor.hasTrait("TyWudao9") && !actor.hasTrait("TyWudao91") && !actor.hasTrait("TyWudao92") && !actor.hasTrait("TyWudao93") && !actor.hasTrait("TyWudao94") && qiXue >= 1000f && !XianTuConfig.LimitWuDaoShenYou)
        {
            // 消耗200点气血
            actor.ChangeQiXue(-100f);
            
            // 根据武道资质决定晋升概率
            float promotionChance = 0.4f; // 基础概率1%
            
            // 根据资质提高晋升概率
            if (actor.hasTrait("TyGengu1")) promotionChance += 0.1f;  // 凡人之资 +10%
            if (actor.hasTrait("TyGengu2")) promotionChance += 0.2f;  // 小有天资 +20%
            if (actor.hasTrait("TyGengu3")) promotionChance += 0.3f; // 武道奇才 +45%
            if (actor.hasTrait("TyGengu4")) promotionChance += 0.4f;  // 天生武骨 +80%
            
            // 获取功法层次名称
            string gongFaLevelName = actor.GetGongFaLevelName();
            
            // 根据功法等级提高晋升概率
            if (gongFaLevelName == "紫府阶") promotionChance += 0.1f;  // 紫府功法 +10%
            if (gongFaLevelName == "道胎阶") promotionChance += 0.2f;  // 道胎功法 +20%
            if (gongFaLevelName == "元神阶") promotionChance += 0.3f;  // 元神功法 +30%
            if (gongFaLevelName == "法相阶") promotionChance += 0.4f;  // 法相功法 +40%
            if (gongFaLevelName == "羽化阶") promotionChance += 0.5f;  // 羽化功法 +50%
            if (gongFaLevelName == "大道阶") promotionChance += 0.6f;  // 大道功法 +60%
            
            // 保证概率不超过100%
            promotionChance = Mathf.Min(1f, promotionChance);
            
            // 如果有天人五衰特质，概率减半
            if (actor.hasTrait("TaiyiLg9"))
            {
                promotionChance *= 0.5f;
            }
            
            // 如果有天命印记特质，必定成功
            if (actor.hasTrait("TianDaoyinji"))
            {
                promotionChance = 1f;
            }
            
            if (UnityEngine.Random.value <= promotionChance)
            {
                actor.removeTrait("TyWudao5");
                actor.addTrait("TyWudao6", false);
                // 更新后缀
                actor.UpdateNameSuffix("抱丹");
                

                
                // 天人境突破成功，随机增加1~50悟性
                int addWuXing = UnityEngine.Random.Range(1, 51);
                actor.ChangeWuXing(addWuXing);
                
                // 突破成功后消除指定负面特质
                string[] negativeTraitsToRemove = { "Stupid", "Short Sighted", "Soft Skin", "Fragile Trait", "Ugly", "Skin Burns", "One Eyed", "Madness", "TaiyiLg9" };
                foreach (string trait in negativeTraitsToRemove)
                {
                    if (actor.hasTrait(trait))
                    {
                        actor.removeTrait(trait);
                    }
                }
                
                // 突破成功后添加指定正面特质
                string[] positiveTraitsToAdd = { "Genius", "Eagle Eyed", "Strong Minded", "Lucky", "Immune", "Sunblessed", "Attractive", "Acid Proof", "Fire Proof", "Freeze Proof", "Poison Immunity" };
                foreach (string trait in positiveTraitsToAdd)
                {
                    if (!actor.hasTrait(trait))
                    {
                        actor.addTrait(trait);
                    }
                }
            }
        }

        // 检查是否可以晋升踏虚境（TyWudao7）
        if (actor.hasTrait("TyWudao6") && !actor.hasTrait("TyWudao7") && !actor.hasTrait("TyWudao8") && !actor.hasTrait("TyWudao9") && !actor.hasTrait("TyWudao91") && !actor.hasTrait("TyWudao92") && !actor.hasTrait("TyWudao93") && !actor.hasTrait("TyWudao94") && qiXue >= 2500f && !XianTuConfig.LimitWuDaoTaXu)
        {
            // 消耗500点气血
            actor.ChangeQiXue(-250f);
            
            // 根据武道资质决定晋升概率
            float promotionChance = 0.3f; // 基础概率0.8%
            
            // 根据资质提高晋升概率
            if (actor.hasTrait("TyGengu1")) promotionChance += 0.1f;  // 凡人之资 +10%
            if (actor.hasTrait("TyGengu2")) promotionChance += 0.2f;  // 小有天资 +20%
            if (actor.hasTrait("TyGengu3")) promotionChance += 0.3f;  // 武道奇才 +50%
            if (actor.hasTrait("TyGengu4")) promotionChance += 0.4f; // 天生武骨 +85%
            
            // 获取功法层次名称
            string gongFaLevelName = actor.GetGongFaLevelName();
            
            // 根据功法等级提高晋升概率
            if (gongFaLevelName == "道胎阶") promotionChance += 0.1f;  // 道胎功法 +10%
            if (gongFaLevelName == "元神阶") promotionChance += 0.2f;  // 元神功法 +20%
            if (gongFaLevelName == "法相阶") promotionChance += 0.3f;  // 法相功法 +30%
            if (gongFaLevelName == "羽化阶") promotionChance += 0.4f;  // 羽化功法 +40%
            if (gongFaLevelName == "大道阶") promotionChance += 0.5f;  // 大道功法 +50%
            
            // 保证概率不超过100%
            promotionChance = Mathf.Min(1f, promotionChance);
            // 如果有天人五衰特质，概率减半
            if (actor.hasTrait("TaiyiLg9"))
            {
                promotionChance *= 0.5f;
            }

            // 如果有天命印记特质，必定成功
            if (actor.hasTrait("TianDaoyinji"))
            {
                promotionChance = 1f;
            }
            
            if (UnityEngine.Random.value <= promotionChance)
            {
                actor.removeTrait("TyWudao6");
                actor.addTrait("TyWudao7", false);
                // 更新后缀
                actor.UpdateNameSuffix("神游");
                
                // 应用武道尊号 - 尊者
                ApplyWudaoTitle(actor, "TyWudao7");

                                string[] negativeTraitsToRemove = { "Stupid", "Short Sighted", "Soft Skin", "Fragile Trait", "Ugly", "Skin Burns", "One Eyed", "Madness", "TaiyiLg9" };
                foreach (string trait in negativeTraitsToRemove)
                {
                    if (actor.hasTrait(trait))
                    {
                        actor.removeTrait(trait);
                    }
                }
                
                // 突破成功后添加指定正面特质
                string[] positiveTraitsToAdd = { "Genius", "Eagle Eyed", "Strong Minded", "Lucky", "Immune", "Sunblessed", "Attractive", "Acid Proof", "Fire Proof", "Freeze Proof", "Poison Immunity" };
                foreach (string trait in positiveTraitsToAdd)
                {
                    if (!actor.hasTrait(trait))
                    {
                        actor.addTrait(trait);
                    }
                }
                // 神游境突破成功，随机增加1~50悟性
                int addWuXing = UnityEngine.Random.Range(1, 101);
                actor.ChangeWuXing(addWuXing);
            }
        }

        // 检查是否可以晋升明我境（TyWudao8）
        if (actor.hasTrait("TyWudao7") && !actor.hasTrait("TyWudao8") && !actor.hasTrait("TyWudao9") && !actor.hasTrait("TyWudao91") && !actor.hasTrait("TyWudao92") && !actor.hasTrait("TyWudao93") && !actor.hasTrait("TyWudao94") && qiXue >= 5000f && !XianTuConfig.LimitWuDaoMingWo)
        {
            // 消耗1000点气血
            actor.ChangeQiXue(-500f);
            
            // 根据武道资质决定晋升概率
            float promotionChance = 0.2f; // 基础概率0.5%
            
            // 根据资质提高晋升概率
            if (actor.hasTrait("TyGengu1")) promotionChance += 0.05f;  // 凡人之资 +10%
            if (actor.hasTrait("TyGengu2")) promotionChance += 0.1f;  // 小有天资 +20%
            if (actor.hasTrait("TyGengu3")) promotionChance += 0.15f; // 武道奇才 +55%
            if (actor.hasTrait("TyGengu4")) promotionChance += 0.2f;  // 天生武骨 +90%
            
            // 获取功法层次名称
            string gongFaLevelName = actor.GetGongFaLevelName();
            
            // 根据功法等级提高晋升概率
            if (gongFaLevelName == "元神阶") promotionChance += 0.1f;  // 元神功法 +10%
            if (gongFaLevelName == "法相阶") promotionChance += 0.2f;  // 法相功法 +20%
            if (gongFaLevelName == "羽化阶") promotionChance += 0.3f;  // 羽化功法 +30%
            if (gongFaLevelName == "大道阶") promotionChance += 0.4f;  // 大道功法 +40%
            
            // 保证概率不超过100%
            promotionChance = Mathf.Min(1f, promotionChance);
            // 如果有天人五衰特质，概率减半
            if (actor.hasTrait("TaiyiLg9"))
            {
                promotionChance *= 0.5f;
            }
            
            // 如果有天命印记特质，必定成功
            if (actor.hasTrait("TianDaoyinji"))
            {
                promotionChance = 1f;
            }
            
            if (UnityEngine.Random.value <= promotionChance)
            {
                actor.removeTrait("TyWudao7");
                actor.addTrait("TyWudao8", false);
                // 更新后缀
                actor.UpdateNameSuffix("明意");
                
                // 应用武道尊号 - 半圣
                ApplyWudaoTitle(actor, "TyWudao8");
                
                // 踏虚境突破成功，随机增加1~500悟性
                int addWuXing = UnityEngine.Random.Range(1, 61);
                actor.ChangeWuXing(addWuXing);
                
                // 突破成功后消除指定负面特质
                string[] negativeTraitsToRemove = { "Stupid", "Short Sighted", "Soft Skin", "Fragile Trait", "Ugly", "Skin Burns", "One Eyed", "Madness", "TaiyiLg9" };
                foreach (string trait in negativeTraitsToRemove)
                {
                    if (actor.hasTrait(trait))
                    {
                        actor.removeTrait(trait);
                    }
                }
                
                // 突破成功后添加指定正面特质
                string[] positiveTraitsToAdd = { "Genius", "Eagle Eyed", "Strong Minded", "Lucky", "Immune", "Sunblessed", "Attractive", "Acid Proof", "Fire Proof", "Freeze Proof", "Poison Immunity" };
                foreach (string trait in positiveTraitsToAdd)
                {
                    if (!actor.hasTrait(trait))
                    {
                        actor.addTrait(trait);
                    }
                }
            }
        }

        // 检查是否可以晋升山海境（TyWudao9）
        if (actor.hasTrait("TyWudao8") && !actor.hasTrait("TyWudao9") && !actor.hasTrait("TyWudao91") && !actor.hasTrait("TyWudao92") && !actor.hasTrait("TyWudao93") && !actor.hasTrait("TyWudao94") && qiXue >= 10000f && !XianTuConfig.LimitWuDaoShanHai)
        {
            // 消耗3000点气血
            actor.ChangeQiXue(-1000f);
            
            // 根据武道资质决定晋升概率
            float promotionChance = 0.1f; // 基础概率0.3%
            
            // 根据资质提高晋升概率
            if (actor.hasTrait("TyGengu1")) promotionChance += 0.05f;  // 凡人之资 +10%
            if (actor.hasTrait("TyGengu2")) promotionChance += 0.07f;  // 小有天资 +20%
            if (actor.hasTrait("TyGengu3")) promotionChance += 0.09f;  // 武道奇才 +60%
            if (actor.hasTrait("TyGengu4")) promotionChance += 0.1f; // 天生武骨 +95%
            
            // 获取功法层次名称
            string gongFaLevelName = actor.GetGongFaLevelName();
            
            // 根据功法等级提高晋升概率
            if (gongFaLevelName == "法相阶") promotionChance += 0.1f;  // 法相功法 +10%
            if (gongFaLevelName == "羽化阶") promotionChance += 0.2f;  // 羽化功法 +20%
            if (gongFaLevelName == "大道阶") promotionChance += 0.3f;  // 大道功法 +30%
            
            // 保证概率不超过100%
            promotionChance = Mathf.Min(1f, promotionChance);
            // 如果有天人五衰特质，概率减半
            if (actor.hasTrait("TaiyiLg9"))
            {
                promotionChance *= 0.5f;
            }
            
            // 如果有天命印记特质，必定成功
            if (actor.hasTrait("TianDaoyinji"))
            {
                promotionChance = 1f;
            }
            
            if (UnityEngine.Random.value <= promotionChance)
            {
                actor.removeTrait("TyWudao8");
                actor.addTrait("TyWudao9", false);
                // 更新后缀
                actor.UpdateNameSuffix("融天");
                
                // 应用武道尊号 - 大圣
                ApplyWudaoTitle(actor, "TyWudao9");
                
                // 明我境突破成功，随机增加1~1000悟性
                int addWuXing = UnityEngine.Random.Range(1, 501);
                actor.ChangeWuXing(addWuXing);
                
                // 突破成功后消除指定负面特质
                string[] negativeTraitsToRemove = { "Stupid", "Short Sighted", "Soft Skin", "Fragile Trait", "Ugly", "Skin Burns", "One Eyed", "Madness", "TaiyiLg9" };
                foreach (string trait in negativeTraitsToRemove)
                {
                    if (actor.hasTrait(trait))
                    {
                        actor.removeTrait(trait);
                    }
                }
                
                // 突破成功后添加指定正面特质
                string[] positiveTraitsToAdd = { "Genius", "Eagle Eyed", "Strong Minded", "Lucky", "Immune", "Sunblessed", "Attractive", "Acid Proof", "Fire Proof", "Freeze Proof", "Poison Immunity" };
                foreach (string trait in positiveTraitsToAdd)
                {
                    if (!actor.hasTrait(trait))
                    {
                        actor.addTrait(trait);
                    }
                }
                
                // 自动收藏 - 山海境
                if (XianTuConfig.ShouldAutoCollectForRealm("TyWudao8", XianTuConfig.AutoCollectWuDaoShanHai))
                {
                    actor.data.favorite = true;
                }
            }
        }

        // 检查是否可以晋升圣躯境（TyWudao91）
        if (actor.hasTrait("TyWudao9") && !actor.hasTrait("TyWudao91") && !actor.hasTrait("TyWudao92") && !actor.hasTrait("TyWudao93") && !actor.hasTrait("TyWudao94") && qiXue >= 25000f && !XianTuConfig.LimitWuDaoShengQu)
        {
            // 消耗5000点气血
            actor.ChangeQiXue(-2500f);
            
            // 根据武道资质决定晋升概率
            float promotionChance = 0.05f; // 基础概率0.1%
            
            // 根据资质提高晋升概率
            if (actor.hasTrait("TyGengu1")) promotionChance += 0.01f;  // 凡人之资 +10%
            if (actor.hasTrait("TyGengu2")) promotionChance += 0.03f;  // 小有天资 +20%
            if (actor.hasTrait("TyGengu3")) promotionChance += 0.06f; // 武道奇才 +65%
            if (actor.hasTrait("TyGengu4")) promotionChance += 0.1f; // 天生武骨 +99%
            
            // 获取功法层次名称
            string gongFaLevelName = actor.GetGongFaLevelName();
            
            // 根据功法等级提高晋升概率
            if (gongFaLevelName == "羽化阶") promotionChance += 0.1f;  // 羽化功法 +10%
            if (gongFaLevelName == "大道阶") promotionChance += 0.2f;  // 大道功法 +20%
            
            // 保证概率不超过100%
            promotionChance = Mathf.Min(1f, promotionChance);
            // 如果有天人五衰特质，概率减半
            if (actor.hasTrait("TaiyiLg9"))
            {
                promotionChance *= 0.5f;
            }
            
            // 如果有天命印记特质，必定成功
            if (actor.hasTrait("TianDaoyinji"))
            {
                promotionChance = 1f;
            }
            if (UnityEngine.Random.value <= promotionChance)
            {
                actor.removeTrait("TyWudao9");
                actor.addTrait("TyWudao91", false);
                // 更新后缀
                actor.UpdateNameSuffix("山海");
                
                // 应用武道尊号 - 准帝
                ApplyWudaoTitle(actor, "TyWudao91");
                
                // 圣躯境突破成功，随机增加1~5000悟性
                int addWuXing = UnityEngine.Random.Range(1, 1001);
                actor.ChangeWuXing(addWuXing);
                
                // 突破成功后消除指定负面特质
                string[] negativeTraitsToRemove = { "Stupid", "Short Sighted", "Soft Skin", "Fragile Trait", "Ugly", "Skin Burns", "One Eyed", "Madness", "TaiyiLg9" };
                foreach (string trait in negativeTraitsToRemove)
                {
                    if (actor.hasTrait(trait))
                    {
                        actor.removeTrait(trait);
                    }
                }
                
                // 突破成功后添加指定正面特质
                string[] positiveTraitsToAdd = { "Genius", "Eagle Eyed", "Strong Minded", "Lucky", "Immune", "Sunblessed", "Attractive", "Acid Proof", "Fire Proof", "Freeze Proof", "Poison Immunity" };
                foreach (string trait in positiveTraitsToAdd)
                {
                    if (!actor.hasTrait(trait))
                    {
                        actor.addTrait(trait);
                    }
                }
                
                // 自动收藏 - 圣躯境
                if (XianTuConfig.ShouldAutoCollectForRealm("TyWudao9", XianTuConfig.AutoCollectWuDaoShengQu))
                {
                    actor.data.favorite = true;
                }
            }
        }

        // 检查是否可以晋升证帝境（TyWudao92）
        if (actor.hasTrait("TyWudao91") && !actor.hasTrait("TyWudao92") && !actor.hasTrait("TyWudao93") && !actor.hasTrait("TyWudao94") && qiXue >= 50000f && !XianTuConfig.LimitWuDaoZhengDi)
        {
            // 消耗10000点气血
            actor.ChangeQiXue(-5000f);
            
            // 根据武道资质决定晋升概率
            float promotionChance = 0.01f; // 基础概率0.05%
            
            // 根据资质提高晋升概率
            if (actor.hasTrait("TyGengu1")) promotionChance += 0.01f;  // 凡人之资 +10%
            if (actor.hasTrait("TyGengu2")) promotionChance += 0.03f;  // 小有天资 +20%
            if (actor.hasTrait("TyGengu3")) promotionChance += 0.06f;  // 武道奇才 +70%
            if (actor.hasTrait("TyGengu4")) promotionChance += 0.1f;  // 天生武骨 +100%
            
            // 获取功法层次名称
            string gongFaLevelName = actor.GetGongFaLevelName();
            
            // 根据功法等级提高晋升概率
            if (gongFaLevelName == "大道阶") promotionChance += 0.1f;  // 大道功法 +10%
            
            // 保证概率不超过100%
            promotionChance = Mathf.Min(1f, promotionChance);
            // 如果有天人五衰特质，概率减半
            if (actor.hasTrait("TaiyiLg9"))
            {
                promotionChance *= 0.5f;
            }
            
                        // 如果有天命印记特质，必定成功
            if (actor.hasTrait("TianDaoyinji"))
            {
                promotionChance = 1f;
            }
            if (UnityEngine.Random.value <= promotionChance)
            {
                actor.removeTrait("TyWudao91");
                actor.addTrait("TyWudao92", false);
                // 更新后缀
                actor.UpdateNameSuffix("圣躯");
                
                // 应用武道尊号 - 涅槃至境
                ApplyWudaoTitle(actor, "TyWudao92");
                
                // 证帝境突破成功，随机增加1~10000悟性
                int addWuXing = UnityEngine.Random.Range(1, 10001);
                actor.ChangeWuXing(addWuXing);
                
                // 突破成功后消除指定负面特质
                string[] negativeTraitsToRemove = { "Stupid", "Short Sighted", "Soft Skin", "Fragile Trait", "Ugly", "Skin Burns", "One Eyed", "Madness", "TaiyiLg9" };
                foreach (string trait in negativeTraitsToRemove)
                {
                    if (actor.hasTrait(trait))
                    {
                        actor.removeTrait(trait);
                    }
                }
                
                // 突破成功后添加指定正面特质
                string[] positiveTraitsToAdd = { "Genius", "Eagle Eyed", "Strong Minded", "Lucky", "Immune", "Sunblessed", "Attractive", "Acid Proof", "Fire Proof", "Freeze Proof", "Poison Immunity" };
                foreach (string trait in positiveTraitsToAdd)
                {
                    if (!actor.hasTrait(trait))
                    {
                        actor.addTrait(trait);
                    }
                }
                
                // 自动收藏 - 证帝境
                if (XianTuConfig.ShouldAutoCollectForRealm("TyWudao91", XianTuConfig.AutoCollectWuDaoZhengDi))
                {
                    actor.data.favorite = true;
                }
            }
        }
        
        // 检查是否可以获得红尘道果（DaoGuo96）
        if (actor.hasTrait("TyWudao92") && qiXue >= 90000f)
        {
            // 检查是否已经拥有任何道果（使用统一的道果检查方法）
            bool hasAnyDaoGuo = traitAction.HasAnyDaoGuo(actor);
            
            // 如果没有道果且气血足够，则获得红尘道果
            if (!hasAnyDaoGuo)
            {
                // 消耗100000点气血
                actor.ChangeQiXue(-10000f);
                
                // 添加红尘道果
                actor.addTrait("DaoGuo96", false);
                // 更新后缀
                actor.UpdateNameSuffix("圣王");
            }
        }
        
        // 检查是否可以晋升至尊境（TyWudao93）
        if (actor.hasTrait("TyWudao92") && !actor.hasTrait("TyWudao93") && !actor.hasTrait("TyWudao94") && qiXue >= 100000f && !XianTuConfig.LimitWuDaoZunZhi)
        {
            // 消耗10000点气血
            actor.ChangeQiXue(-10000f);
            
            // 根据武道资质决定晋升概率
            float promotionChance = 0.005f; // 基础概率0.5%
            
            // 根据资质提高晋升概率
            if (actor.hasTrait("TyGengu1")) promotionChance += 0.01f;  // 凡人之资 +10%
            if (actor.hasTrait("TyGengu2")) promotionChance += 0.03f;  // 小有天资 +20%
            if (actor.hasTrait("TyGengu3")) promotionChance += 0.06f;  // 武道奇才 +70%
            if (actor.hasTrait("TyGengu4")) promotionChance += 0.1f;  // 天生武骨 +100%
            
            // 保证概率不超过100%
            promotionChance = Mathf.Min(1f, promotionChance);
            // 如果有天人五衰特质，概率减半
            if (actor.hasTrait("TaiyiLg9"))
            {
                promotionChance *= 0.5f;
            }
            
                        // 如果有天命印记特质，必定成功
            if (actor.hasTrait("TianDaoyinji"))
            {
                promotionChance = 1f;
            }
            if (UnityEngine.Random.value <= promotionChance)
            {
                actor.removeTrait("TyWudao92");
                actor.addTrait("TyWudao93", false);
                // 更新后缀
                actor.UpdateNameSuffix("至尊");
                
                // 应用武道尊号 - 圣躯
                ApplyWudaoTitle(actor, "TyWudao93");
                
                // 圣躯境突破成功，随机增加1~20000悟性
                int addWuXing = UnityEngine.Random.Range(1, 20001);
                actor.ChangeWuXing(addWuXing);
                
                // 突破成功后消除指定负面特质
                string[] negativeTraitsToRemove = { "Stupid", "Short Sighted", "Soft Skin", "Fragile Trait", "Ugly", "Skin Burns", "One Eyed", "Madness", "TaiyiLg9" };
                foreach (string trait in negativeTraitsToRemove)
                {
                    if (actor.hasTrait(trait))
                    {
                        actor.removeTrait(trait);
                    }
                }
                
                // 突破成功后添加指定正面特质
                string[] positiveTraitsToAdd = { "Genius", "Eagle Eyed", "Strong Minded", "Lucky", "Immune", "Sunblessed", "Attractive", "Acid Proof", "Fire Proof", "Freeze Proof", "Poison Immunity" };
                foreach (string trait in positiveTraitsToAdd)
                {
                    if (!actor.hasTrait(trait))
                    {
                        actor.addTrait(trait);
                    }
                }
                
                // 自动收藏 - 至尊境
                if (XianTuConfig.ShouldAutoCollectForRealm("TyWudao92", XianTuConfig.AutoCollectWuDaoZunZhi))
                {
                    actor.data.favorite = true;
                }
            }
        }
        // 检查是否可以获得天心印记（TianXinYinji）
        if (actor.hasTrait("TyWudao93") && qiXue >= 200000f && !actor.hasTrait("TianXinYinji"))
        {
            // 检查是否有其他角色已经拥有天心印记（使用特性系统确保唯一性）
            bool markExists = false;
            List<Actor> allActors = World.world.units.getSimpleList();
            foreach (Actor otherActor in allActors)
            {
                if (otherActor != actor && otherActor.hasTrait("TianXinYinji"))
                {
                    markExists = true;
                    break;
                }
            }
            
            // 如果没有其他角色拥有天心印记，则添加天心印记
            // 如果没有天心印记，且没有至尊印记，才能添加天心印记
            if (!markExists && !actor.hasTrait("ZhiZunYinJi"))
            {
                // 消耗20000点气血
                actor.ChangeQiXue(-20000f);
                
                // 添加天心印记
                actor.addTrait("TianXinYinji", false);
                // 只有当前后缀是"至尊"时，才改为"伪帝"
                string currentName = actor.getName();
                if (currentName.Contains("至尊"))
                {
                    currentName = currentName.Replace("至尊", "伪帝");
                    actor.setName(currentName);
                }
            }
        }
        // 检查是否可以晋升帝境（TyWudao94）
        if (actor.hasTrait("TyWudao93") && !actor.hasTrait("TyWudao94") && actor.hasTrait("TianXinYinji") && !HasZhiZunYinJi(actor) && qiXue >= 300000f && !XianTuConfig.LimitWuDaoDiJing)
        {
            // 消耗50000点气血
            actor.ChangeQiXue(-30000f);
            
            // 根据武道资质决定晋升概率
            float promotionChance = 0.001f; // 基础概率0.1%
            
            // 根据资质提高晋升概率
            if (actor.hasTrait("TyGengu1")) promotionChance += 0.01f;  // 凡人之资 +10%
            if (actor.hasTrait("TyGengu2")) promotionChance += 0.03f;  // 小有天资 +20%
            if (actor.hasTrait("TyGengu3")) promotionChance += 0.06f;  // 武道奇才 +70%
            if (actor.hasTrait("TyGengu4")) promotionChance += 0.1f;  // 天生武骨 +100%
            
            // 保证概率不超过100%
            promotionChance = Mathf.Min(1f, promotionChance);
            // 如果有天人五衰特质，概率减半
            if (actor.hasTrait("TaiyiLg9"))
            {
                promotionChance *= 0.5f;
            }
            
                        // 如果有天命印记特质，必定成功
            if (actor.hasTrait("TianDaoyinji"))
            {
                promotionChance = 1f;
            }
            if (UnityEngine.Random.value <= promotionChance)
            {
                actor.removeTrait("TyWudao93");
                actor.addTrait("TyWudao94", false);
                // 更新后缀
                actor.UpdateNameSuffix("帝境");
                
                // 应用武道尊号 - 帝境
                ApplyWudaoTitle(actor, "TyWudao94");
                
                // 帝境突破成功，随机增加1~50000悟性
                int addWuXing = UnityEngine.Random.Range(1, 50001);
                actor.ChangeWuXing(addWuXing);
                
                // 突破成功后消除指定负面特质
                string[] negativeTraitsToRemove = { "Stupid", "Short Sighted", "Soft Skin", "Fragile Trait", "Ugly", "Skin Burns", "One Eyed", "Madness", "TaiyiLg9" };
                foreach (string trait in negativeTraitsToRemove)
                {
                    if (actor.hasTrait(trait))
                    {
                        actor.removeTrait(trait);
                    }
                }
                
                // 突破成功后添加指定正面特质
                string[] positiveTraitsToAdd = { "Genius", "Eagle Eyed", "Strong Minded", "Lucky", "Immune", "Sunblessed", "Attractive", "Acid Proof", "Fire Proof", "Freeze Proof", "Poison Immunity" };
                foreach (string trait in positiveTraitsToAdd)
                {
                    if (!actor.hasTrait(trait))
                    {
                        actor.addTrait(trait);
                    }
                }
                
                // 自动收藏 - 帝境
                if (XianTuConfig.ShouldAutoCollectForRealm("TyWudao93", XianTuConfig.AutoCollectWuDaoDiJing))
                {
                    actor.data.favorite = true;
                }
            }
        }
        
        // 帝境强者3000岁获得至尊印记的逻辑
        float age = (float)actor.getAge();
        // 只有拥有天心印记的帝境强者才能在3000岁时转为至尊境
        if (Mathf.FloorToInt(age) >= 3000 && actor.hasTrait("TyWudao94") && actor.hasTrait("TianXinYinji"))
        {
            // 移除帝境特质
            if (actor.hasTrait("TyWudao94"))
            {
                actor.removeTrait("TyWudao94");
            }
            
            // 移除天心印记
            if (actor.hasTrait("TianXinYinji"))
            {
                actor.removeTrait("TianXinYinji");
            }
            
            // 添加至尊境特质
            if (!actor.hasTrait("TyWudao93"))
            {
                actor.addTrait("TyWudao93", false);
                // 更新后缀
                actor.UpdateNameSuffix("古皇");
            }
            
            // 添加至尊印记
            if (!actor.hasTrait("ZhiZunYinJi"))
            {
                actor.addTrait("ZhiZunYinJi", false);
                // 应用至尊印记尊号
                ApplyWudaoTitle(actor, "ZhiZunYinJi");
            }
        }
        
        // 结束CheckWudaoPromotion方法
    }
        
        // 拥有至尊印记不能晋升帝境的检查
        
        // 武道回血机制 - 根据气血值的0.1%进行回血
    public static bool WudaoRegen(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || !pTarget.isActor())
            return false;
        
        Actor actor = pTarget.a;
        if (actor.data.health < actor.getMaxHealth())
        {
            // 获取当前气血值
            float qiXue = actor.GetQiXue();
            // 计算回血量：气血值的0.1%
            int healAmount = Mathf.RoundToInt(qiXue * 0.001f);
            // 至少回复1点生命
            if (healAmount < 1) healAmount = 1; 
            
            // 恢复生命值
            actor.restoreHealth(healAmount);
            
            // 生成治疗效果粒子
            actor.spawnParticle(Toolbox.color_heal);
        }
        return true;
    }
        
    // 各个武道境界的专属回血方法
    public static bool Wudao1_Regen(BaseSimObject pTarget, WorldTile pTile = null)
    {
        return WudaoRegen(pTarget, pTile);
    }
        
    public static bool Wudao2_Regen(BaseSimObject pTarget, WorldTile pTile = null)
    {
        return WudaoRegen(pTarget, pTile);
    }
        
    public static bool Wudao3_Regen(BaseSimObject pTarget, WorldTile pTile = null)
    {
        return WudaoRegen(pTarget, pTile);
    }
        
    public static bool Wudao4_Regen(BaseSimObject pTarget, WorldTile pTile = null)
    {
        return WudaoRegen(pTarget, pTile);
    }
        
    public static bool Wudao5_Regen(BaseSimObject pTarget, WorldTile pTile = null)
    {
        return WudaoRegen(pTarget, pTile);
    }
        
    public static bool Wudao6_Regen(BaseSimObject pTarget, WorldTile pTile = null)
    {
        return WudaoRegen(pTarget, pTile);
    }
        
    public static bool Wudao7_Regen(BaseSimObject pTarget, WorldTile pTile = null)
    {
        return WudaoRegen(pTarget, pTile);
    }
        
    public static bool Wudao8_Regen(BaseSimObject pTarget, WorldTile pTile = null)
    {
        return WudaoRegen(pTarget, pTile);
    }
        
    public static bool Wudao9_Regen(BaseSimObject pTarget, WorldTile pTile = null)
    {
        return WudaoRegen(pTarget, pTile);
    }
        
    public static bool Wudao91_Regen(BaseSimObject pTarget, WorldTile pTile = null)
    {
        return WudaoRegen(pTarget, pTile);
    }
        
    public static bool Wudao92_Regen(BaseSimObject pTarget, WorldTile pTile = null)
    {
        return WudaoRegen(pTarget, pTile);
    }
    
    public static bool Wudao93_Regen(BaseSimObject pTarget, WorldTile pTile = null)
    {
        return WudaoRegen(pTarget, pTile);
    }
    
    public static bool Wudao94_Regen(BaseSimObject pTarget, WorldTile pTile = null)
    {
        return WudaoRegen(pTarget, pTile);
    }
    
    // 检查角色是否拥有至尊印记
    public static bool HasZhiZunYinJi(Actor actor)
    {
        return actor != null && actor.hasTrait("ZhiZunYinJi");
    }
    
    // 检查极尽升华触发条件的方法 - 用于绑定到特质
    public static bool CheckJiJinShengHua(BaseSimObject pTarget, WorldTile pTile = null)
    {
        if (pTarget == null || !pTarget.isActor())
            return false;
        
        Actor actor = pTarget.a;
        // 检查目标是否掉到半血以下
        if (actor != null && actor.data.health < actor.getMaxHealth() * 0.5f && actor.isAlive())
        {
            // 检查是否是拥有至尊印记的至尊境强者且还未触发过极尽升华
            if (HasZhiZunYinJi(actor) && actor.hasTrait("TyWudao93") && !actor.hasTrait("TyWudao94"))
            {
                try
                {
                    // 移除至尊印记和至尊境特质
                    actor.removeTrait("ZhiZunYinJi");
                    actor.removeTrait("TyWudao93");
                    
                    // 添加帝境特质
                    actor.addTrait("TyWudao94");
                    
                    // 更新名称后缀为"古帝"
                    string oldName = actor.getName();
                    if (oldName.Contains("古皇"))
                    {
                        oldName = oldName.Replace("古皇", "古帝");
                        actor.setName(oldName);
                    }
                    
                    // 应用武道尊号
                    ApplyWudaoTitle(actor, "TyWudao94");
                    
                    // 添加极尽升华状态
                    StatusAsset JiJinShengHua = AssetManager.status.get("JiJinShengHua");
                    if (JiJinShengHua != null)
                    {
                        actor.addStatusEffect(JiJinShengHua.id, JiJinShengHua.duration, false);
                    }

                    // 恢复满血
                    actor.setHealth(actor.getMaxHealth(), true);

                    // 记录日志
                    Debug.Log("[太上仙天录] " + actor.getName() + " 燃尽一切，极尽升华！");
                    return true;
                }
                catch (Exception e)
                {
                    Debug.LogError("[太上仙天录] 极尽升华状态激活失败: " + e.Message);
                    return false;
                }
            }
        }
        return false;
    }
    
    // 注意：Harmony补丁触发机制已被移除，现在通过特质绑定的CheckJiJinShengHua方法触发极尽升华
    // 移除原因：特质绑定机制更符合半血触发需求，且触发频率更高
}