using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ai;
using UnityEngine;
using VideoCopilot.code.utils;
using InterestingTrait.code.Config;
// using LeiJieMechanism.code;  // 已移除错误引用，因为LeiJieMechanism类实际在XianTu.code命名空间下

namespace XianTu.code
{
    internal class traitAction
    {
        public static bool IsXianTu1To12(Actor a)
        {
            for (int i = 1; i <= 12; i++)
            {
                if (a.hasTrait($"XianTu{i}") || a.hasTrait($"XianTu{i}+"))
                {
                    return true;
                }
            }
            return false;
        }
        private static readonly Dictionary<string, string> _xianTuSuffixMap = new Dictionary<string, string>
{
    {"XianTu1", "练气"},
    {"XianTu2", "辟谷"},
    {"DaoJi2", "筑基"},
    {"DaoJi3", "紫府"},
    {"XianTu3", "煞丹"},
    {"DaoJi5", "结丹"},
    {"DaoJi6", "金丹"},
    {"XianTu4", "结婴"},
    {"DaoJi9", "元婴"},
    {"DaoJi91", "婴变"},
    {"XianTu5", "元神"},
    {"DaoJi95", "分神"},
    {"DaoJi96", "化神"},
    {"XianTu6", "洞虚"},
    {"FaXiang2", "炼虚"},
    {"FaXiang3", "合体"},
    {"XianTu7", "三灾"},
    {"FaXiang6", "九难"},
    {"FaXiang7", "十劫"},
    {"XianTu8", "真仙"},
    {"XianTu9", "金仙"},
    {"TaiYiyinji", "太乙"},
    {"XianTu91", "大罗"},
};

        // 统一的尊号前缀池
        private static readonly string[] _titlePrefixes = new string[]
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
        "寒狱","沸狱","剥皮","拔舌","铁树","盘狱","蒸狱","柱狱","刀狱","碓狱","裂狱","凌迟","斩狱","首狱","道藏","金简",
        "玉牒","金策","玉书","天书","堪舆","符玄","玄机","奥秘","灵泽","玄泽","天浆","甘泽","嘉澍","甘霖","膏雨","瑞雨",
        "祥雨","喜雨","廉纤","轻丝","跳珠","银竹","白雨","陵雨","暴雨","雷雨","风雨","霜雨","朝雨","暮雨","夜雨","东风",
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

        // 境界对应的尊号后缀
        private static readonly Dictionary<string, string> _realmTitleSuffixMap = new Dictionary<string, string>
        {
            {"XianTu5", "真人"},  // 化神境后缀
            {"XianTu6", "真君"},  // 合体境后缀
            {"XianTu7", "道君"},  // 大乘境后缀
            {"XianTu8", "仙君"},  // 地仙境后缀
            {"XianTu9", "天尊"},  // 地仙境后缀
            {"XianTu91", "道祖"},  // 地仙境后缀
        };

        // 生成动态尊号
        private static string GenerateDynamicTitle(string traitId)
        {
            if (_realmTitleSuffixMap.TryGetValue(traitId, out string suffix))
            {
                // 对于化神境(XianTu5)及以上境界，随机选择2个不重复的前缀
                if (traitId == "XianTu7" || traitId == "XianTu8" || traitId == "XianTu9" || traitId == "XianTu91")
                {
                    int firstIndex = UnityEngine.Random.Range(0, _titlePrefixes.Length);
                    int secondIndex;
                    // 确保选择的两个前缀不重复
                    do
                    {
                        secondIndex = UnityEngine.Random.Range(0, _titlePrefixes.Length);
                    } while (firstIndex == secondIndex);
                    
                    string firstPrefix = _titlePrefixes[firstIndex];
                    string secondPrefix = _titlePrefixes[secondIndex];
                    return $"{firstPrefix}{secondPrefix}{suffix}";
                }
                else
                {
                    // 其他境界保持原有逻辑，随机选择一个前缀
                    string prefix = _titlePrefixes[UnityEngine.Random.Range(0, _titlePrefixes.Length)];
                    return $"{prefix}{suffix}";
                }
            }
            return string.Empty;
        }
// 尊号字典（从金丹开始的修仙境界）
private static readonly Dictionary<string, string[]> _xianTuTitlesMap = new Dictionary<string, string[]>
{
    // 金丹境尊号
    {"XianTu3", new string[] {
    }},
    
    // 元婴境尊号
    {"XianTu4", new string[] {
    }},
    
    // 化神境尊号
    {"XianTu5", new string[] {
    }},
    
    // 返虚境尊号
    {"XianTu6", new string[] {
    }},
    
    // 合体境尊号
    {"XianTu7", new string[] {
    }},
    
    // 大乘境尊号
    {"XianTu8", new string[] {
    }},
};
private static void UpdateNameSuffix(Actor actor, string newTrait)
{
    if (_xianTuSuffixMap.TryGetValue(newTrait, out string suffix))
    {
        string currentName = actor.getName();
        
        // 1. 分离基础名称（包括尊号）和旧境界后缀
        int lastDashIndex = currentName.LastIndexOf('-');
        string basePart = lastDashIndex >= 0 
            ? currentName.Substring(0, lastDashIndex).Trim() 
            : currentName;
        
        // 2. 设置新名称：基础名称 + 新境界后缀
        actor.setName($"{basePart}-{suffix}");
    }
}

private static void ApplyXianTuTitle(Actor actor, string newTrait)
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
            
            // 3. 生成或选择新尊号
            string title = string.Empty;
            
            // 优先使用动态尊号生成系统
            title = GenerateDynamicTitle(newTrait);
            
            // 如果动态生成失败，回退到原有的尊号字典
            if (string.IsNullOrEmpty(title) && _xianTuTitlesMap.TryGetValue(newTrait, out string[] titles))
            {
                title = titles[UnityEngine.Random.Range(0, titles.Length)];
            }
            
            // 4. 设置新名称：新尊号 + · + 基础名称 + 境界后缀
            if (!string.IsNullOrEmpty(title))
            {
                actor.setName($"{title}·{basePart}{suffixPart}");
            }
        }

        public static bool TrueDamage1_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;
                Actor targetActor = pTarget.a;
                /* 暂时隐藏XianTu9~93相关代码
                // 武域境(XianTu9)、合道境(XianTu91)、斩我境(XianTu92)、武极境(XianTu93)均无视此真伤
                if (targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91") || 
                    targetActor.hasTrait("XianTu92") || targetActor.hasTrait("XianTu93"))
                */
                {
                    return false;
                }
                // 获取攻击者的攻击力
                float attackDamage = attacker.stats["damage"];
                int trueDamage = (int)(attackDamage * 0.09f); 

                // 确保至少造成1点伤害
                if (trueDamage > 0 && targetActor.data.health > 0)
                {
                    // 施加真实伤害（受位格减伤限制）
                    ActorExtensions.ApplyTrueDamageWithRealmReduction(targetActor, trueDamage, attacker);
                }
                // 可以添加一些视觉效果，例如粒子效果
                AssetManager.terraform.get("lightning_normal").apply_force = false;
                MapBox.spawnLightningMedium(pTile, 0.05f);
            }
            return false;
        }
        public static bool TrueDamage2_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;
                Actor targetActor = pTarget.a;
                /* 暂时隐藏XianTu9~93相关代码
                // 序列二_天使(XianTu9)、序列一_天使(XianTu91)、天使之王(XianTu92)、真神(XianTu93)均无视此真伤
        if (targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91") ||
        targetActor.hasTrait("XianTu92") || targetActor.hasTrait("XianTu93"))
                */
                {
                    return false;
                }
                // 获取攻击者的攻击力
                float attackDamage = attacker.stats["damage"];
                int trueDamage = (int)(attackDamage * 0.1f); 

                // 确保至少造成1点伤害
                if (trueDamage > 0 && targetActor.data.health > 0)
                {
                    // 施加真实伤害（受位格减伤限制）
                    ActorExtensions.ApplyTrueDamageWithRealmReduction(targetActor, trueDamage, attacker);
                }
                // 可以添加一些视觉效果，例如粒子效果
                AssetManager.terraform.get("lightning_normal").apply_force = false;
                MapBox.spawnLightningMedium(pTile, 0.06f); 
            }
            return false;
        }
        public static bool TrueDamage3_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;
                Actor targetActor = pTarget.a;
                /* 暂时隐藏XianTu9~93相关代码
                if (targetActor.hasTrait("XianTu91") || targetActor.hasTrait("XianTu92") || targetActor.hasTrait("XianTu93"))
                */
                {
                    return false;
                }
                // 获取攻击者的攻击力
                float attackDamage = attacker.stats["damage"];
                int trueDamage = (int)(attackDamage * 0.11f); 

                // 确保至少造成1点伤害
                if (trueDamage > 0 && targetActor.data.health > 0)
                {
                    // 施加真实伤害（受位格减伤限制）
                    ActorExtensions.ApplyTrueDamageWithRealmReduction(targetActor, trueDamage, attacker);
                }
                // 可以添加一些视觉效果，例如粒子效果
                AssetManager.terraform.get("lightning_normal").apply_force = false;
                MapBox.spawnLightningMedium(pTile, 0.07f); 
            }
            return false;
        }
        public static bool TrueDamage4_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;
                Actor targetActor = pTarget.a;
                /* 暂时隐藏XianTu9~93相关代码
                // 天使之王(XianTu92)、真神(XianTu93)无视此真伤
        if (targetActor.hasTrait("XianTu92") || targetActor.hasTrait("XianTu93"))
                */
                {
                    return false;
                }
                // 获取攻击者的攻击力
                float attackDamage = attacker.stats["damage"];
                int trueDamage = (int)(attackDamage * 0.12f); 

                // 确保至少造成1点伤害
                if (trueDamage > 0 && targetActor.data.health > 0)
                {
                    // 施加真实伤害（受位格减伤限制）
                    ActorExtensions.ApplyTrueDamageWithRealmReduction(targetActor, trueDamage, attacker);
                }
                // 可以添加一些视觉效果，例如粒子效果
                AssetManager.terraform.get("lightning_normal").apply_force = false;
                MapBox.spawnLightningMedium(pTile, 0.08f); 
            }
            return false;
        }
        public static bool TrueDamage5_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;
                Actor targetActor = pTarget.a;
                /* 暂时隐藏XianTu9~93相关代码
                // 真神(XianTu93)无视此真伤
        if (targetActor.hasTrait("XianTu93"))
                */
                {
                    return false;
                }
                // 获取攻击者的攻击力
                float attackDamage = attacker.stats["damage"];
                int trueDamage = (int)(attackDamage * 0.13f); 

                // 确保至少造成1点伤害
                if (trueDamage > 0 && targetActor.data.health > 0)
                {
                    // 施加真实伤害（受位格减伤限制）
                    ActorExtensions.ApplyTrueDamageWithRealmReduction(targetActor, trueDamage, attacker);
                }
                // 可以添加一些视觉效果，例如粒子效果
                AssetManager.terraform.get("lightning_normal").apply_force = false;
                MapBox.spawnLightningMedium(pTile, 0.16f); 
            }
            return false;
        }
        // 道果系列特质的10倍生命真实伤害
        public static bool DaoGuoTrueDamage_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;
                Actor targetActor = pTarget.a;
                
                // 获取攻击者的最大生命值，并计算10倍的真实伤害
                float maxHealth = attacker.stats["health"];
                int trueDamage = (int)(maxHealth * 10f); 

                // 确保至少造成1点伤害
                if (trueDamage > 0 && targetActor.data.health > 0)
                {
                    // 施加真实伤害（受位格减伤限制）
                    ActorExtensions.ApplyTrueDamageWithRealmReduction(targetActor, trueDamage, attacker);
                }
                // 添加视觉效果
                AssetManager.terraform.get("lightning_normal").apply_force = false;
                MapBox.spawnLightningMedium(pTile, 0.2f);
            }
            return false;
        }

        public static bool TrueDamage6_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;
                Actor targetActor = pTarget.a;
                // 真神(XianTu93)无视此真伤
        if (targetActor.hasTrait("XianTu93"))
                {
                    return false;
                }
                // 获取攻击者的攻击力
                float attackDamage = attacker.stats["damage"];
                int trueDamage = (int)(attackDamage * 0.14f); 

                // 确保至少造成1点伤害
                if (trueDamage > 0 && targetActor.data.health > 0)
                {
                    // 施加真实伤害（受位格减伤限制）
                    ActorExtensions.ApplyTrueDamageWithRealmReduction(targetActor, trueDamage, attacker);
                }
                // 可以添加一些视觉效果，例如粒子效果
                AssetManager.terraform.get("lightning_normal").apply_force = false;
                MapBox.spawnLightningMedium(pTile, 0.32f); 
            }
            return false;
        }
        public static bool TrueDamage7_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;
                Actor targetActor = pTarget.a;
                // 获取攻击者的攻击力
                float attackDamage = attacker.stats["damage"];
                int trueDamage = (int)(attackDamage * 0.15f); 

                // 确保至少造成1点伤害
                if (trueDamage > 0 && targetActor.data.health > 0)
                {
                    int actualDamage = Mathf.Min(trueDamage, targetActor.data.health);
                    targetActor.restoreHealth(-Mathf.Max(1, actualDamage));
                }
                // 可以添加一些视觉效果，例如粒子效果
                AssetManager.terraform.get("lightning_normal").apply_force = false;
                MapBox.spawnLightningMedium(pTile, 0.64f); 
            }
            return false;
        }

        public static bool fire1_attackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                pTile = pTarget.current_tile;
            }

            if (pTile == null)
            {
                return false;
            }

            {
                EffectsLibrary.spawn("fx_bomb_flash", pTile,"Bomb", null, 0f, -1f, -1f);
            }

            return true;
        }

        public static bool fire2_attackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)
            {
                pTile = pTarget.current_tile;
            }

            if (pTile == null)
            {
                return false;
            }

            {
                EffectsLibrary.spawn("fx_napalm_flash", pTile,"NapalmBomb", null, 0f, -1f, -1f);
            }

            return true;
        }

        public static bool TrueDamageByXianTu1_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 1. 检查目标是否有效（存在且是生物单位）
            if (pTarget == null || !pTarget.isActor() || pSelf == null || !pSelf.isActor()) 
                return false;
    
            Actor attacker = pSelf.a;
            Actor targetActor = pTarget.a;
    
            // 2. 获取攻击者的序列值
            float xianTuValue = attacker.GetXianTu();

            // 3. 计算真实伤害（示例：序列值的20%作为基础伤害）
            float baseDamage = xianTuValue * 0.2f;
    
            // 4. 添加伤害波动（±10%随机波动）
            float randomFactor = UnityEngine.Random.Range(0.9f, 1.1f);
            int trueDamage = Mathf.RoundToInt(baseDamage * randomFactor);
    
            /* 暂时隐藏XianTu9~93相关代码
            // 5. 特殊规则：根据敌人境界减免伤害
            if (targetActor.hasTrait("XianTu93")) // 真神减免90%伤害
                trueDamage = (int)(trueDamage * 0.1f);
            else if (targetActor.hasTrait("XianTu92")) // 天使之王减免50%伤害
                trueDamage = (int)(trueDamage * 0.5f);
            */
    
            // 6. 施加真实伤害（无视防御）
            if (targetActor.data.health > trueDamage)
            {
                targetActor.restoreHealth(-trueDamage);
            }

            AssetManager.terraform.get("lightning_normal").apply_force = false;
            MapBox.spawnLightningMedium(pTile, 0.16f);
    
            return true;
        }

        public static bool TrueDamageByXianTu2_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 1. 检查目标是否有效（存在且是生物单位）
            if (pTarget == null || !pTarget.isActor() || pSelf == null || !pSelf.isActor()) 
                return false;
    
            Actor attacker = pSelf.a;
            Actor targetActor = pTarget.a;
    
            // 2. 获取攻击者的序列值
            float xianTuValue = attacker.GetXianTu();

            // 3. 计算真实伤害（示例：序列值的30%作为基础伤害）
            float baseDamage = xianTuValue * 0.3f;
    
            // 4. 添加伤害波动（±10%随机波动）
            float randomFactor = UnityEngine.Random.Range(0.9f, 1.1f);
            int trueDamage = Mathf.RoundToInt(baseDamage * randomFactor);
    
            /* 暂时隐藏XianTu9~93相关代码
            // 5. 特殊规则：根据敌人境界减免伤害
            if (targetActor.hasTrait("XianTu93")) // 真神减免50%伤害
                trueDamage = (int)(trueDamage * 0.5f);
            */
    
            // 6. 施加真实伤害（无视防御）
            if (targetActor.data.health > trueDamage)
            {
                targetActor.restoreHealth(-trueDamage);
            }

            AssetManager.terraform.get("lightning_normal").apply_force = false;
            MapBox.spawnLightningMedium(pTile, 0.32f);
    
            return true;
        }

        public static bool TrueDamageByXianTu3_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 1. 检查目标是否有效（存在且是生物单位）
            if (pTarget == null || !pTarget.isActor() || pSelf == null || !pSelf.isActor()) 
                return false;
    
            Actor attacker = pSelf.a;
            Actor targetActor = pTarget.a;
    
            // 2. 获取攻击者的序列值
            float xianTuValue = attacker.GetXianTu();

            // 3. 计算真实伤害（示例：序列值的50%作为基础伤害）
            float baseDamage = xianTuValue * 0.5f;
    
            // 4. 添加伤害波动（±10%随机波动）
            float randomFactor = UnityEngine.Random.Range(0.9f, 1.1f);
            int trueDamage = Mathf.RoundToInt(baseDamage * randomFactor);
    
            // 5. 施加真实伤害（无视防御）
            if (targetActor.data.health > trueDamage)
            {
                targetActor.restoreHealth(-trueDamage);
            }

            AssetManager.terraform.get("lightning_normal").apply_force = false;
            MapBox.spawnLightningMedium(pTile, 0.64f);
    
            return true;
        }

        public static bool MartialGodTrueDamage_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;
                Actor targetActor = pTarget.a;
                
                // 2. 获取攻击者的序列值
                float xianTuValue = attacker.GetXianTu();

                // 3. 计算真实伤害（示例：序列值的500%作为基础伤害）
                float baseDamage = xianTuValue * 5f;
    
                // 4. 添加伤害波动（±10%随机波动）
                float randomFactor = UnityEngine.Random.Range(0.9f, 1.1f);
                int trueDamage = Mathf.RoundToInt(baseDamage * randomFactor);
    
                // 5. 施加真实伤害（无视防御）
                if (targetActor.data.health > trueDamage)
                {
                    targetActor.restoreHealth(-trueDamage);
                }
                else
               {
                    // 如果伤害超过目标血量，直接击杀
                    targetActor.restoreHealth(-targetActor.data.health);
                }
            }

            return false;
        }

        public static bool TaiChuTrueDamage_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;
                Actor targetActor = pTarget.a;
                
                // 2. 获取攻击者的序列值
                float xianTuValue = attacker.GetXianTu();

                // 3. 计算真实伤害（示例：序列值的550%作为基础伤害）
                float baseDamage = xianTuValue * 5.5f;
    
                // 4. 添加伤害波动（±10%随机波动）
                float randomFactor = UnityEngine.Random.Range(0.9f, 1.1f);
                int trueDamage = Mathf.RoundToInt(baseDamage * randomFactor);
    
                // 5. 施加真实伤害（无视防御）
                if (targetActor.data.health > trueDamage)
                {
                    targetActor.restoreHealth(-trueDamage);
                }
                else
               {
                    // 如果伤害超过目标血量，直接击杀
                    targetActor.restoreHealth(-targetActor.data.health);
                }
            }

            return false;
        }

        public static bool YunXingTrueDamage_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor() || pSelf == null || !pSelf.isActor())
                return false;

            Actor attacker = pSelf.a;
            Actor targetActor = pTarget.a;
    
            // 获取攻击者当前生命值
            float currentHealth = attacker.data.health;
    
            // 计算真实伤害 = 攻击者生命值的4%
            int trueDamage = (int)(currentHealth / 25);
    
            // 确保至少造成1点伤害
            if (trueDamage < 1) trueDamage = 1;
    
            // 施加真实伤害（受位格减伤限制）
                ActorExtensions.ApplyTrueDamageWithRealmReduction(targetActor, trueDamage, attacker);

            return true;
        }

        public static bool WanZhanTrueDamage_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor() || pSelf == null || !pSelf.isActor())
                return false;

            Actor attacker = pSelf.a;
            Actor targetActor = pTarget.a;
    
            // 获取攻击者当前生命值
            float currentHealth = attacker.data.health;
    
            // 计算真实伤害 = 攻击者生命值的5%
            int trueDamage = (int)(currentHealth / 20);
    
            // 确保至少造成1点伤害
            if (trueDamage < 1) trueDamage = 1;
    
            // 施加真实伤害（受位格减伤限制）
                ActorExtensions.ApplyTrueDamageWithRealmReduction(targetActor, trueDamage, attacker);

            return true;
        }

        public static bool BloodSeaTrueDamage_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;
                Actor targetActor = pTarget.a;
                
                float killCount = attacker.data.kills;
                int trueDamage =  (int)(killCount* 50f); 

                if (targetActor.data.health > trueDamage)
                {
                    targetActor.restoreHealth(-trueDamage);
                }
                else
               {
                    // 如果伤害超过目标血量，直接击杀
                    targetActor.restoreHealth(-targetActor.data.health);
                }
            }
            return false;
        }

        public static bool ArmorBasedTrueDamage_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;
                Actor targetActor = pTarget.a;
        
                // 获取攻击者的防御值（armor属性）
                float armorValue = attacker.stats[strings.S.armor];
        
                // 计算真实伤害 = 防御值 * 100
                int trueDamage = Mathf.RoundToInt(armorValue * 100f);
        
                // 确保至少造成1点伤害
                if (trueDamage > 0 && targetActor.data.health > 0)
                {
                    // 施加真实伤害（受位格减伤限制）
                ActorExtensions.ApplyTrueDamageWithRealmReduction(targetActor, trueDamage, attacker);
                }
            }
            return false; // 允许后续攻击动作继续执行
        }

        public static bool ManLongTrueDamage_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;
                Actor targetActor = pTarget.a;
        
                float healthValue = attacker.stats[strings.S.health];
        
                int trueDamage = Mathf.RoundToInt(healthValue * 0.01f);
        
                // 确保至少造成1点伤害
                if (trueDamage > 0 && targetActor.data.health > 0)
                {
                    // 施加真实伤害（受位格减伤限制）
                ActorExtensions.ApplyTrueDamageWithRealmReduction(targetActor, trueDamage, attacker);
                }
            }
            return false; // 允许后续攻击动作继续执行
        }

        public static bool XingChenTrueDamage_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;
                Actor targetActor = pTarget.a;
        
                float staminaValue = attacker.stats[strings.S.stamina];
        
                int trueDamage = Mathf.RoundToInt(staminaValue * 3f);
        
                // 确保至少造成1点伤害
                if (trueDamage > 0 && targetActor.data.health > 0)
                {
                    // 施加真实伤害（受位格减伤限制）
                ActorExtensions.ApplyTrueDamageWithRealmReduction(targetActor, trueDamage, attacker);
                }
            }
            return false; // 允许后续攻击动作继续执行
        }

        public static bool HunDunTrueDamage_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;
                Actor targetActor = pTarget.a;
        
                float speedValue = attacker.stats[strings.S.speed];
        
                int trueDamage = Mathf.RoundToInt(speedValue * 50f);
        
                // 确保至少造成1点伤害
                if (trueDamage > 0 && targetActor.data.health > 0)
                {
                    // 施加真实伤害（受位格减伤限制）
                ActorExtensions.ApplyTrueDamageWithRealmReduction(targetActor, trueDamage, attacker);
                }
            }
            return false; // 允许后续攻击动作继续执行
        }

        // 全局饱食度维护方法 - 所有符合条件的角色都会保持饱食度100%
        public static bool MaintainFullNutrition(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTarget.isActor())
            {
                Actor actor = pTarget.a;
                // 仙道合体XianTu6及以上修士（包含XianTu6-XianTu93）
                bool isHighLevelDaoist = false;
                for (int i = 6; i <= 93; i++)
                {
                    if (actor.hasTrait("XianTu" + i))
                    {
                        isHighLevelDaoist = true;
                        break;
                    }
                }
                
                // 武道山海TyWudao91以上修士
                bool isHighLevelWarrior = actor.hasTrait("TyWudao91") || 
                                         actor.hasTrait("TyWudao92") || 
                                         actor.hasTrait("TyWudao93") || 
                                         actor.hasTrait("TyWudao94");
                
                if (isHighLevelDaoist || isHighLevelWarrior)
                {
                    // 保持饱食度100%
                    actor.data.nutrition = 100;
                }
            }
            return true;
        }      

        public static bool XianTu1_effectAction(BaseSimObject pTarget, WorldTile pTile = null) 
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;

            if (a.GetXianTu() <= 9.99)
            {
                return false;
            }

            // 检查练气境突破限制
            if (XianTuConfig.LimitLianQi)
            {
                return false;
            }

            string[] forbiddenTraits = { "XianTu2", "XianTu3", "XianTu4", "XianTu5", "XianTu6", "XianTu7", "XianTu8", "XianTu9", "XianTu91", "XianTu92", "XianTu93" };
            foreach (string trait in forbiddenTraits)
            {
                if (pTarget.a.hasTrait(trait))
                {
                    return false;
                }
            }

            // 突破练气期需要至少炼炁以上功法
            string gongFaLevelName = a.GetGongFaLevelName();
            if (gongFaLevelName != "炼炁阶" && gongFaLevelName != "道基阶" && gongFaLevelName != "紫府阶" && gongFaLevelName != "道胎阶" && gongFaLevelName != "元神阶" && gongFaLevelName != "法相阶" && gongFaLevelName != "羽化阶" && gongFaLevelName != "大道阶")
            {
                return false;
            }

            upTrait(
                "特质",
                "XianTu1",
                a,
                new string[]
                {
                    "tumorInfection",
                    "cursed",
                    "infected",
                    "mushSpores",
                    "plague",
                    "madness"
                },
                new string[] { "特质" }
            );
            UpdateNameSuffix(a, "XianTu1");
            
            // 晋升练气期后随机获得一个修仙职业
            string[] professions = { "XiuxianLiuyi1", "XiuxianLiuyi2", "XiuxianLiuyi3", "XiuxianLiuyi4", "XiuxianLiuyi5", "XiuxianLiuyi6", "XiuxianLiuyi7", "XiuxianLiuyi8", "XiuxianLiuyi9" };
            // 检查角色是否已经拥有任何一种修仙职业
            bool hasProfession = false;
            foreach (string profession in professions)
            {
                if (a.hasTrait(profession))
                {
                    hasProfession = true;
                    break;
                }
            }
            
            // 只有当角色没有任何修仙职业时，才随机分配一个
            if (!hasProfession)
            {
                int randomIndex = UnityEngine.Random.Range(0, professions.Length);
                string selectedProfession = professions[randomIndex];
                a.addTrait(selectedProfession);
            }

            // 练气期增加1~5点悟性，只增加一次
            if (!a.hasTrait("XianTu1_WuXingIncreased"))
            {
                int wuXingIncrease = UnityEngine.Random.Range(1, 6); // 1-5
                a.ChangeWuXing(wuXingIncrease);
                a.addTrait("XianTu1_WuXingIncreased");
            }

            return true;
        }



        public static bool XianTu2_effectAction(BaseSimObject pTarget, WorldTile pTile = null) 
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            if (a.GetXianTu() <= 99.99)
            {
                return false;
            }

            // 检查筑基境突破限制
            if (XianTuConfig.LimitZhuJi)
            {
                return false;
            }

            // 突破筑基期需要至少道基以上功法
            string gongFaLevelName = a.GetGongFaLevelName();
            if (gongFaLevelName != "道基阶" && gongFaLevelName != "紫府阶" && gongFaLevelName != "道胎阶" && gongFaLevelName != "元神阶" && gongFaLevelName != "法相阶" && gongFaLevelName != "羽化阶" && gongFaLevelName != "大道阶")
            {
                return false;
            }

            a.ChangeXianTu(-50);
            double successRate = 0.1;
            if (a.hasTrait("TaiyiLg2"))
            {
                successRate = 0.01;
            }
            else if (a.hasTrait("TaiyiLg3"))
            {
                successRate = 0.05;
            }
            else if (a.hasTrait("TaiyiLg4"))
            {
                successRate = 0.1;
            }
            else if (a.hasTrait("TaiyiLg5"))
            {
                successRate = 0.2;
            }
            else if (a.hasTrait("TaiyiLg6"))
            {
                successRate = 0.3;
            }
            else if (a.hasTrait("TaiyiLg7"))
            {
                successRate = 0.4;
            }
            
            // 检查是否拥有筑基丹，增加50%成功率
            if (a.hasTrait("TupoDanyao1"))
            {
                successRate += 0.5;
            }
            
            // 不同阶别功法对突破筑基期概率的加成
            if (gongFaLevelName == "道基阶")
            {
                successRate += 0.1;
            }
            else if (gongFaLevelName == "紫府阶")
            {
                successRate += 0.2;
            }
            else if (gongFaLevelName == "道胎阶")
            {
                successRate += 0.3;
            }
            else if (gongFaLevelName == "元神阶")
            {
                successRate += 0.4;
            }
            else if (gongFaLevelName == "法相阶")
            {
                successRate += 0.5;
            }
            else if (gongFaLevelName == "羽化阶")
            {
                successRate += 0.6;
            }
            else if (gongFaLevelName == "大道阶")
            {
                successRate += 0.7;
            }

            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f);
            // 如果有天人五衰特质，概率减半
            if (a.hasTrait("TaiyiLg9"))
            {
                successRate *= 0.5f;
            }

            // 如果有天命印记特质，必定成功
            if (a.hasTrait("TianDaoyinji"))
            {
                successRate = 1.0;
            }
            if (randomValue > successRate)
            {
                // 无论成功与否，都删除筑基丹
                if (a.hasTrait("TupoDanyao1"))
                {
                    a.removeTrait("TupoDanyao1");
                }
                return false; // 随机数大于成功率，则操作失败
            }
            
            // 无论成功与否，都删除筑基丹
            if (a.hasTrait("TupoDanyao1"))
            {
                a.removeTrait("TupoDanyao1");
            }

            upTrait(
                "XianTu1",
                "XianTu2",
                a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores" }
            );
            UpdateNameSuffix(a, "XianTu2");

            // 突破筑基成功后只获得DaoJi1特质
            if (!a.hasTrait("DaoJi1") && !a.hasTrait("DaoJi2") && !a.hasTrait("DaoJi3") && !a.hasTrait("DaoJi4"))
            {
                a.addTrait("DaoJi1");
            }

            // 筑基期增加1~10点悟性，只增加一次
            if (!a.hasTrait("XianTu2_WuXingIncreased"))
            {
                int wuXingIncrease = UnityEngine.Random.Range(5, 11); // 1-10
                a.ChangeWuXing(wuXingIncrease);
                a.addTrait("XianTu2_WuXingIncreased");
            }

            // 突破成功后消除指定负面特质
            string[] negativeTraitsToRemove = { "stupid", "short_sighted", "soft_skin", "fragile_trait", "ugly", "skin_burns", "one_eyed", "madness", "TaiyiLg9" };
            foreach (string trait in negativeTraitsToRemove)
            {
                if (a.hasTrait(trait))
                {
                    a.removeTrait(trait);
                }
            }

            // 突破成功后添加指定正面特质
            string[] positiveTraitsToAdd = { "genius", "eagle_eyed", "strong_minded", "lucky", "immune", "sunblessed", "attractive", "acid_proof", "fire_proof", "freeze_proof", "poison_immunity" };
            foreach (string trait in positiveTraitsToAdd)
            {
                if (!a.hasTrait(trait))
                {
                    a.addTrait(trait);
                }
            }

            return true;
        }

        // 道基晋升逻辑 - 在筑基晋升判定之下，金丹晋升判定之上
        public static void PromoteDaoJi(Actor a)
        {
            // 300真元时，将DaoJi1晋升为DaoJi2
            if (a.GetXianTu() >= 300 && a.GetXianTu() < 600 && a.hasTrait("DaoJi1") && !a.hasTrait("DaoJi2") && !a.hasTrait("DaoJi3") && !a.hasTrait("DaoJi4"))
            {
                // 先消耗50真元
                a.ChangeXianTu(-50);
                
                double promotionRate = 0.0;
                
                // 根据灵根设置不同概率
                if (a.hasTrait("TaiyiLg2")) // 五灵根
                {
                    promotionRate = 0.1;
                }
                else if (a.hasTrait("TaiyiLg3")) // 四灵根
                {
                    promotionRate = 0.2;
                }
                else if (a.hasTrait("TaiyiLg4")) // 三灵根
                {
                    promotionRate = 0.3;
                }
                else if (a.hasTrait("TaiyiLg5")) // 二灵根
                {
                    promotionRate = 0.4;
                }
                else if (a.hasTrait("TaiyiLg6")) // 单灵根
                {
                    promotionRate = 0.5;
                }
                else if (a.hasTrait("TaiyiLg7")) // 天灵根
                {
                    promotionRate = 0.6;
                }
                else
                {
                    promotionRate = 0.1; // 默认概率
                }
                
                // 进行晋升判定
                if (UnityEngine.Random.Range(0.0f, 1.0f) <= promotionRate)
                {
                    a.removeTrait("DaoJi1");
                    a.addTrait("DaoJi2");
                    UpdateNameSuffix(a, "DaoJi2");
                }
            }
            // 600真元时，将DaoJi2晋升为DaoJi3
            else if (a.GetXianTu() >= 600 && a.hasTrait("DaoJi2") && !a.hasTrait("DaoJi3") && !a.hasTrait("DaoJi4"))
            {
                // 先消耗100真元
                a.ChangeXianTu(-100);
                
                double promotionRate = 0.0;
                
                // 根据灵根设置不同概率
                if (a.hasTrait("TaiyiLg2")) // 五灵根
                {
                    promotionRate = 0.05;
                }
                else if (a.hasTrait("TaiyiLg3")) // 四灵根
                {
                    promotionRate = 0.1;
                }
                else if (a.hasTrait("TaiyiLg4")) // 三灵根
                {
                    promotionRate = 0.15;
                }
                else if (a.hasTrait("TaiyiLg5")) // 二灵根
                {
                    promotionRate = 0.25;
                }
                else if (a.hasTrait("TaiyiLg6")) // 单灵根
                {
                    promotionRate = 0.35;
                }
                else if (a.hasTrait("TaiyiLg7")) // 天灵根
                {
                    promotionRate = 0.5;
                }
                else
                {
                    promotionRate = 0.1; // 默认概率
                }
                
                // 进行晋升判定
                if (UnityEngine.Random.Range(0.0f, 1.0f) <= promotionRate)
                {
                    a.removeTrait("DaoJi2");
                    a.addTrait("DaoJi3");
                    UpdateNameSuffix(a, "DaoJi3");
                }
            }
        }

        public static bool XianTu3_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            // 在金丹晋升判定前先进行道基晋升判定
            PromoteDaoJi(a);

            if (a.GetXianTu() <= 999.99)
            {
                return false;
            }

            // 检查金丹境突破限制
            if (XianTuConfig.LimitJinDan)
            {
                return false;
            }

            // 突破金丹期需要至少紫府以上功法
            string gongFaLevelName = a.GetGongFaLevelName();
            if (gongFaLevelName != "紫府阶" && gongFaLevelName != "道胎阶" && gongFaLevelName != "元神阶" && gongFaLevelName != "法相阶" && gongFaLevelName != "羽化阶" && gongFaLevelName != "大道阶")
            {
                return false;
            }

            a.ChangeXianTu(-999);
            double successRate = 0.2;
            if (a.hasTrait("DaoJi3"))
            {
                successRate = 0.1;
            }
            else if (a.hasTrait("DaoJi4"))
            {
                successRate = 0.2;
            }
            
            // 检查是否拥有化金丹，增加40%成功率
            if (a.hasTrait("TupoDanyao2"))
            {
                successRate += 0.4;
            }
            
            // 不同阶别功法对突破金丹期概率的加成
            if (gongFaLevelName == "紫府阶")
            {
                successRate += 0.1;
            }
            else if (gongFaLevelName == "道胎阶")
            {
                successRate += 0.2;
            }
            else if (gongFaLevelName == "元神阶")
            {
                successRate += 0.3;
            }
            else if (gongFaLevelName == "法相阶")
            {
                successRate += 0.4;
            }
            else if (gongFaLevelName == "羽化阶")
            {
                successRate += 0.5;
            }
            else if (gongFaLevelName == "大道阶")
            {
                successRate += 0.6;
            }

            // 添加灵根对突破概率的影响
            if (a.hasTrait("TaiyiLg2")) // 杂灵根
            {
                successRate -= 0.3;
            }
            else if (a.hasTrait("TaiyiLg3")) // 四灵根
            {
                successRate -= 0.2;
            }
            else if (a.hasTrait("TaiyiLg4")) // 三灵根
            {
                successRate -= 0.1;
            }
            else if (a.hasTrait("TaiyiLg5")) // 二灵根
            {
                successRate += 0.01;
            }
            else if (a.hasTrait("TaiyiLg6")) // 单灵根
            {
                successRate += 0.05;
            }
            else if (a.hasTrait("TaiyiLg7")) // 天灵根
            {
                successRate += 0.1;
            }

            // 确保成功率在合理范围内
            successRate = Math.Max(0.0, Math.Min(1.0, successRate));

            // 如果有天人五衰特质，概率减半
            if (a.hasTrait("TaiyiLg9"))
            {
                successRate *= 0.5;
            }
            
            // 如果有天命印记特质，必定成功
            if (a.hasTrait("TianDaoyinji"))
            {
                successRate = 1.0;
            }

            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f);
            if (randomValue > successRate)
            {
                // 无论成功与否，都删除化金丹
                if (a.hasTrait("TupoDanyao2"))
                {
                    a.removeTrait("TupoDanyao2");
                }
                // 晋升失败，触发雷劫（如果没有不朽道体）
                if (!a.hasTrait("TaiyiLg1"))
                {
                    LeiJieMechanism.TriggerJinDanToYuanYingLeiJie(a);
                }
                // 突破失败，删除DaoJi3，添加DaoJi4
                if (a.hasTrait("DaoJi3"))
                {
                    a.removeTrait("DaoJi3");
                    a.addTrait("DaoJi4");
                }
                // 突破失败，更新后缀为虚丹
                string currentName = a.getName();
                int lastDashIndex = currentName.LastIndexOf('-');
                string basePart = lastDashIndex >= 0 ? currentName.Substring(0, lastDashIndex).Trim() : currentName;
                a.setName($"{basePart}-虚丹");
                return false; // 随机数大于成功率，则操作失败
            }
            
            // 无论成功与否，都删除化金丹
            if (a.hasTrait("TupoDanyao2"))
            {
                a.removeTrait("TupoDanyao2");
            }
            string[] optionalTraits = { "dash", "block", "dodge", "backstep", "deflect_projectile" };
            int randomIndex = UnityEngine.Random.Range(0, optionalTraits.Length);
            string selectedTrait = optionalTraits[randomIndex];

            upTrait(
                "XianTu2",
                "XianTu3",
                a,
                new string[]
                {
                    "tumorInfection",
                    "cursed",
                    "infected",
                    "mushSpores",
                    "XianTu22"
                },
                new string[] { selectedTrait }
            );
            UpdateNameSuffix(a, "XianTu3");

            // 移除现有的所有道基特质
            a.removeTrait("DaoJi1");
            a.removeTrait("DaoJi2");
            a.removeTrait("DaoJi3");
            a.removeTrait("DaoJi4");
            
            // 金丹突破成功后只获得DaoJi5特质
            a.addTrait("DaoJi5");

            // 金丹期增加1~50点悟性，只增加一次
            if (!a.hasTrait("XianTu3_WuXingIncreased"))
            {
                int wuXingIncrease = UnityEngine.Random.Range(20, 51); // 1-50
                a.ChangeWuXing(wuXingIncrease);
                a.addTrait("XianTu3_WuXingIncreased");
            }

            // 突破成功后消除指定负面特质
            string[] negativeTraitsToRemove = { "Stupid", "Short Sighted", "Soft Skin", "Fragile Trait", "Ugly", "Skin Burns", "One Eyed", "Madness", "TaiyiLg9" };
            foreach (string trait in negativeTraitsToRemove)
            {
                if (a.hasTrait(trait))
                {
                    a.removeTrait(trait);
                }
            }

            // 突破成功后添加指定正面特质
            string[] positiveTraitsToAdd = { "genius", "eagle eyed", "strong minded", "lucky", "immune", "sunblessed", "attractive", "acid proof", "fire proof", "freeze proof", "poison immunity" };
            foreach (string trait in positiveTraitsToAdd)
            {
                if (!a.hasTrait(trait))
                {
                    a.addTrait(trait);
                }
            }

            return true;
        }

        // 金丹期道基晋升方法
        public static void PromoteJinDanDaoJi(Actor a)
        {
            // 检查是否处于金丹期且持有DaoJi5或DaoJi6特质
            if (a.hasTrait("XianTu3") && (a.hasTrait("DaoJi5") || a.hasTrait("DaoJi6")))
            {
                // 3000真元时，尝试将DaoJi5晋升为DaoJi6
                if (a.GetXianTu() >= 3000 && !a.hasTrait("DaoJi6") && !a.hasTrait("DaoJi7"))
                {
                    // 先消耗50真元
                    a.ChangeXianTu(-1000);
                    
                    double successRate = 0.0;
                    
                    // 根据灵根类型确定晋升概率
                    if (a.hasTrait("TaiyiLg2")) // 五灵根
                    {
                        successRate = 0.1;
                    }
                    else if (a.hasTrait("TaiyiLg3")) // 四灵根
                    {
                        successRate = 0.2;
                    }
                    else if (a.hasTrait("TaiyiLg4")) // 三灵根
                    {
                        successRate = 0.3;
                    }
                    else if (a.hasTrait("TaiyiLg5")) // 二灵根
                    {
                        successRate = 0.4;
                    }
                    else if (a.hasTrait("TaiyiLg6")) // 单灵根
                    {
                        successRate = 0.5;
                    }
                    else if (a.hasTrait("TaiyiLg7")) // 天灵根
                    {
                        successRate = 0.6;
                    }
                    
                    double randomValue = UnityEngine.Random.Range(0.0f, 1.0f);
                    if (randomValue <= successRate)
                    {
                        a.removeTrait("DaoJi5");
                        a.addTrait("DaoJi6");
                        // 更新名称后缀
                        UpdateNameSuffix(a, "DaoJi6");
                    }
                }
                
                // 6000真元时，尝试将DaoJi6晋升为DaoJi7
                if (a.hasTrait("DaoJi6") && a.GetXianTu() >= 6000 && !a.hasTrait("DaoJi7"))
                {
                    // 先消耗100真元
                    a.ChangeXianTu(-1000);
                    
                    double successRate = 0.0;
                    
                    // 根据灵根类型确定晋升概率
                    if (a.hasTrait("TaiyiLg2")) // 五灵根
                    {
                        successRate = 0.05;
                    }
                    else if (a.hasTrait("TaiyiLg3")) // 四灵根
                    {
                        successRate = 0.1;
                    }
                    else if (a.hasTrait("TaiyiLg4")) // 三灵根
                    {
                        successRate = 0.15;
                    }
                    else if (a.hasTrait("TaiyiLg5")) // 二灵根
                    {
                        successRate = 0.25;
                    }
                    else if (a.hasTrait("TaiyiLg6")) // 单灵根
                    {
                        successRate = 0.35;
                    }
                    else if (a.hasTrait("TaiyiLg7")) // 天灵根
                    {
                        successRate = 0.5;
                    }
                    
                    double randomValue = UnityEngine.Random.Range(0.0f, 1.0f);
                    if (randomValue <= successRate)
                    {
                        a.removeTrait("DaoJi6");
                        a.addTrait("DaoJi7");
                        // 更新名称后缀
                        UpdateNameSuffix(a, "DaoJi7");
                    }
                }
            }
        }
        
        public static bool XianTu4_effectAction(BaseSimObject pTarget, WorldTile pTile = null) 
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            
            // 在元婴晋升判定前先进行金丹期道基晋升判定
            PromoteJinDanDaoJi(a);

            if (a.GetXianTu() <= 9999.99)
            {
                return false;
            }

            // 检查元婴境突破限制
            if (XianTuConfig.LimitYingYuan)
            {
                return false;
            }

            // 突破元婴期需要至少道胎以上功法
            string gongFaLevelName = a.GetGongFaLevelName();
            if (gongFaLevelName != "道胎阶" && gongFaLevelName != "元神阶" && gongFaLevelName != "法相阶" && gongFaLevelName != "羽化阶" && gongFaLevelName != "大道阶")
            {
                return false;
            }

            a.ChangeXianTu(-9999);
            double successRate = 0.2;
            if (a.hasTrait("DaoJi7"))
            {
                successRate = 0.1;
            }
            else if (a.hasTrait("DaoJi8"))
            {
                successRate = 0.2;
            }
            
            // 检查是否拥有结婴丹，增加30%成功率
            if (a.hasTrait("TupoDanyao3"))
            {
                successRate += 0.3;
            }
            
            // 不同阶别功法对突破元婴期概率的加成
            if (gongFaLevelName == "道胎阶")
            {
                successRate += 0.1;
            }
            else if (gongFaLevelName == "元神阶")
            {
                successRate += 0.2;
            }
            else if (gongFaLevelName == "法相阶")
            {
                successRate += 0.3;
            }
            else if (gongFaLevelName == "羽化阶")
            {
                successRate += 0.4;
            }
            else if (gongFaLevelName == "大道阶")
            {
                successRate += 0.5;
            }

            // 添加灵根对突破概率的影响
            if (a.hasTrait("TaiyiLg2")) // 杂灵根
            {
                successRate -= 0.3;
            }
            else if (a.hasTrait("TaiyiLg3")) // 四灵根
            {
                successRate -= 0.2;
            }
            else if (a.hasTrait("TaiyiLg4")) // 三灵根
            {
                successRate -= 0.1;
            }
            else if (a.hasTrait("TaiyiLg5")) // 二灵根
            {
                successRate += 0.01;
            }
            else if (a.hasTrait("TaiyiLg6")) // 单灵根
            {
                successRate += 0.05;
            }
            else if (a.hasTrait("TaiyiLg7")) // 天灵根
            {
                successRate += 0.1;
            }

            // 确保成功率在合理范围内
            successRate = Math.Max(0.0, Math.Min(1.0, successRate));

            // 如果有天人五衰特质，概率减半
            if (a.hasTrait("TaiyiLg9"))
            {
                successRate *= 0.5;
            }

            // 如果有天命印记特质，必定成功
            if (a.hasTrait("TianDaoyinji"))
            {
                successRate = 1.0;
            }
            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f);
            if (randomValue > successRate)
            {
                // 无论成功与否，都删除结婴丹
                if (a.hasTrait("TupoDanyao3"))
                {
                    a.removeTrait("TupoDanyao3");
                }
                // 晋升失败，触发雷劫（如果没有不朽道体）
                if (!a.hasTrait("TaiyiLg1"))
                {
                    LeiJieMechanism.TriggerJinDanToYuanYingLeiJie(a);
                }
                if (a.hasTrait("DaoJi7"))
                {
                    a.removeTrait("DaoJi7");
                    a.addTrait("DaoJi8");
                }
                // 突破失败，更新后缀为假婴
                string currentName = a.getName();
                int lastDashIndex = currentName.LastIndexOf('-');
                string basePart = lastDashIndex >= 0 ? currentName.Substring(0, lastDashIndex).Trim() : currentName;
                a.setName($"{basePart}-假婴");
                return false; // 随机数大于成功率，则操作失败
            }
            
            // 无论成功与否，都删除结婴丹
            if (a.hasTrait("TupoDanyao3"))
            {
                a.removeTrait("TupoDanyao3");
            }

            upTrait(
                "XianTu3",
                "XianTu4",
                a,
                new string[]
                {
                    "tumorInfection",
                    "cursed",
                    "infected",
                    "mushSpores"
                },
                new string[] { }
            );
            UpdateNameSuffix(a, "XianTu4");

            // 移除现有的所有金丹特质
            a.removeTrait("DaoJi5");
            a.removeTrait("DaoJi6");
            a.removeTrait("DaoJi7");
            a.removeTrait("DaoJi8");
            
            // 突破成功后只获得DaoJi9特质
            a.addTrait("DaoJi9");

            // 元婴期增加1~100点悟性，只增加一次
            if (!a.hasTrait("XianTu4_WuXingIncreased"))
            {
                int wuXingIncrease = UnityEngine.Random.Range(50, 101); // 1-100
                a.ChangeWuXing(wuXingIncrease);
                a.addTrait("XianTu4_WuXingIncreased");
            }

            // 突破成功后消除指定负面特质
            string[] negativeTraitsToRemove = { "Stupid", "Short Sighted", "Soft Skin", "Fragile Trait", "Ugly", "Skin Burns", "One Eyed", "Madness", "TaiyiLg9" };
            foreach (string trait in negativeTraitsToRemove)
            {
                if (a.hasTrait(trait))
                {
                    a.removeTrait(trait);
                }
            }

            // 突破成功后添加指定正面特质
            string[] positiveTraitsToAdd = { "genius", "eagle eyed", "strong minded", "lucky", "immune", "sunblessed", "attractive", "acid proof", "fire proof", "freeze proof", "poison immunity" };
            foreach (string trait in positiveTraitsToAdd)
            {
                if (!a.hasTrait(trait))
                {
                    a.addTrait(trait);
                }
            }

            return true;
        }

        public static void PromoteYuanYingDaoJi(Actor a)
        {
            // 30000真元时，将DaoJi9晋升为DaoJi91
            if (a.GetXianTu() >= 30000 && a.GetXianTu() < 60000 && a.hasTrait("DaoJi9") && !a.hasTrait("DaoJi91") && !a.hasTrait("DaoJi92"))
            {
                // 先消耗5000真元
                a.ChangeXianTu(-5000);
                
                double promotionRate = 0.0;
                
                // 根据灵根设置不同概率
                if (a.hasTrait("TaiyiLg2")) // 五灵根
                {
                    promotionRate = 0.05;
                }
                else if (a.hasTrait("TaiyiLg3")) // 四灵根
                {
                    promotionRate = 0.1;
                }
                else if (a.hasTrait("TaiyiLg4")) // 三灵根
                {
                    promotionRate = 0.15;
                }
                else if (a.hasTrait("TaiyiLg5")) // 二灵根
                {
                    promotionRate = 0.25;
                }
                else if (a.hasTrait("TaiyiLg6")) // 单灵根
                {
                    promotionRate = 0.35;
                }
                else if (a.hasTrait("TaiyiLg7")) // 天灵根
                {
                    promotionRate = 0.5;
                }
                else
                {
                    promotionRate = 0.1; // 默认概率
                }
                
                // 进行晋升判定
                if (UnityEngine.Random.Range(0.0f, 1.0f) <= promotionRate)
                {
                    a.removeTrait("DaoJi9");
                    a.addTrait("DaoJi91");
                    UpdateNameSuffix(a, "DaoJi91");
                }
                // 失败不返还真元
            }
            // 60000真元时，将DaoJi91晋升为DaoJi92
            else if (a.GetXianTu() >= 60000 && a.hasTrait("DaoJi91") && !a.hasTrait("DaoJi92"))
            {
                // 先消耗10000真元
                a.ChangeXianTu(-10000);
                
                double promotionRate = 0.0;
                
                // 根据灵根设置不同概率
                if (a.hasTrait("TaiyiLg2")) // 五灵根
                {
                    promotionRate = 0.025;
                }
                else if (a.hasTrait("TaiyiLg3")) // 四灵根
                {
                    promotionRate = 0.05;
                }
                else if (a.hasTrait("TaiyiLg4")) // 三灵根
                {
                    promotionRate = 0.075;
                }
                else if (a.hasTrait("TaiyiLg5")) // 二灵根
                {
                    promotionRate = 0.125;
                }
                else if (a.hasTrait("TaiyiLg6")) // 单灵根
                {
                    promotionRate = 0.175;
                }
                else if (a.hasTrait("TaiyiLg7")) // 天灵根
                {
                    promotionRate = 0.25;
                }
                else
                {
                    promotionRate = 0.05; // 默认概率
                }
                
                // 进行晋升判定
                if (UnityEngine.Random.Range(0.0f, 1.0f) <= promotionRate)
                {
                    a.removeTrait("DaoJi91");
                    a.addTrait("DaoJi92");
                    UpdateNameSuffix(a, "DaoJi92");
                }
                // 失败不返还真元
            }
        }

        public static bool XianTu5_effectAction(BaseSimObject pTarget, WorldTile pTile = null) 
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;

            // 在化神晋升判定前先进行元婴期道基晋升判定
            PromoteYuanYingDaoJi(a);

            if (a.GetXianTu() <= 99999.99)
            {
                return false;
            }

            // 检查化神境突破限制
            if (XianTuConfig.LimitHuaShen)
            {
                return false;
            }

            // 突破化神期需要至少元神以上功法
            string gongFaLevelName = a.GetGongFaLevelName();
            if (gongFaLevelName != "元神阶" && gongFaLevelName != "法相阶" && gongFaLevelName != "羽化阶" && gongFaLevelName != "大道阶")
            {
                return false;
            }

            a.ChangeXianTu(-99999);
            double successRate = 0.2;
            if (a.hasTrait("DaoJi92"))
            {
                successRate = 0.1;
            }
            else if (a.hasTrait("DaoJi93"))
            {
                successRate = 0.2;
            }
            
            // 检查是否拥有凝神丹，增加20%成功率
            if (a.hasTrait("TupoDanyao4"))
            {
                successRate += 0.2;
            }
            
            // 不同阶别功法对突破化神期概率的加成
            if (gongFaLevelName == "元神阶")
            {
                successRate += 0.1;
            }
            else if (gongFaLevelName == "法相阶")
            {
                successRate += 0.2;
            }
            else if (gongFaLevelName == "羽化阶")
            {
                successRate += 0.3;
            }
            else if (gongFaLevelName == "大道阶")
            {
                successRate += 0.4;
            }

            // 添加灵根对突破概率的影响
            if (a.hasTrait("TaiyiLg2")) // 杂灵根
            {
                successRate -= 0.3;
            }
            else if (a.hasTrait("TaiyiLg3")) // 四灵根
            {
                successRate -= 0.2;
            }
            else if (a.hasTrait("TaiyiLg4")) // 三灵根
            {
                successRate -= 0.1;
            }
            else if (a.hasTrait("TaiyiLg5")) // 二灵根
            {
                successRate += 0.01;
            }
            else if (a.hasTrait("TaiyiLg6")) // 单灵根
            {
                successRate += 0.05;
            }
            else if (a.hasTrait("TaiyiLg7")) // 天灵根
            {
                successRate += 0.1;
            }

            // 确保成功率在合理范围内
            successRate = Math.Max(0.0, Math.Min(1.0, successRate));

            // 如果有天人五衰特质，概率减半
            if (a.hasTrait("TaiyiLg9"))
            {
                successRate *= 0.5;
            }

            // 如果有天命印记特质，必定成功
            if (a.hasTrait("TianDaoyinji"))
            {
                successRate = 1.0;
            }
            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f);
            if (randomValue > successRate)
            {
                // 无论成功与否，都删除凝神丹
                if (a.hasTrait("TupoDanyao4"))
                {
                    a.removeTrait("TupoDanyao4");
                }
                // 晋升失败，触发雷劫（如果没有不朽道体）
                if (!a.hasTrait("TaiyiLg1"))
                {
                    LeiJieMechanism.TriggerYuanYingToHuaShenLeiJie(a);
                }
                if (a.hasTrait("DaoJi92"))
                {
                    a.removeTrait("DaoJi92");
                    a.addTrait("DaoJi93");
                }
                // 突破失败，更新后缀为出窍
                string currentName = a.getName();
                int lastDashIndex = currentName.LastIndexOf('-');
                string basePart = lastDashIndex >= 0 ? currentName.Substring(0, lastDashIndex).Trim() : currentName;
                a.setName($"{basePart}-出窍");
                return false;
            }
            
            // 无论成功与否，都删除凝神丹
            if (a.hasTrait("TupoDanyao4"))
            {
                a.removeTrait("TupoDanyao4");
            }
            string[] optionalTraits = { "dash", "block", "dodge", "backstep", "deflect_projectile" };
            string selectedTrait = null;
            // 检查是否所有可选特质都已被拥有
            bool allTraitsOwned = optionalTraits.All(trait => a.hasTrait(trait));
            if (!allTraitsOwned)
            {
                var availableTraits = optionalTraits.Where(t => !a.hasTrait(t)).ToList();
                selectedTrait = availableTraits[UnityEngine.Random.Range(0, availableTraits.Count)];
            }


            upTrait(
                "XianTu4",
                "XianTu5",
                a,
                new string[]
                {
                    "tumorInfection",
                    "cursed",
                    "infected",
                    "mushSpores"
                },
                (selectedTrait != null) ?
                new string[] { selectedTrait } :
                new string[] { }
            );
            UpdateNameSuffix(a, "XianTu5");
            ApplyXianTuTitle(a, "XianTu5");
            // 根据配置自动收藏化神境角色
            if (XianTuConfig.ShouldAutoCollectForRealm("XianTu5", XianTuConfig.AutoCollectHuaShen))
            {
                a.data.favorite = true;
            }
            // 化神成功后，移除所有元婴期道基特质，并只获得DaoJi94特质
            // 检查角色是否拥有DaoJi9~93特质
            if (a.hasTrait("DaoJi9"))
            {
                a.removeTrait("DaoJi9");
            }
            if (a.hasTrait("DaoJi91"))
            {
                a.removeTrait("DaoJi91");
            }
            if (a.hasTrait("DaoJi92"))
            {
                a.removeTrait("DaoJi92");
            }
            if (a.hasTrait("DaoJi93"))
            {
                a.removeTrait("DaoJi93");
            }
            // 只添加DaoJi94特质
            a.addTrait("DaoJi94");
            // 化神期增加1~500点悟性，只增加一次
            if (!a.hasTrait("XianTu5_WuXingIncreased"))
            {
                int wuXingIncrease = UnityEngine.Random.Range(250, 501); // 1-500
                a.ChangeWuXing(wuXingIncrease);
                a.addTrait("XianTu5_WuXingIncreased");
            }

            // 突破成功后消除指定负面特质
            string[] negativeTraitsToRemove = { "Stupid", "Short Sighted", "Soft Skin", "Fragile Trait", "Ugly", "Skin Burns", "One Eyed", "Madness", "TaiyiLg9" };
            foreach (string trait in negativeTraitsToRemove)
            {
                if (a.hasTrait(trait))
                {
                    a.removeTrait(trait);
                }
            }

            // 突破成功后添加指定正面特质
            string[] positiveTraitsToAdd = { "genius", "eagle eyed", "strong minded", "lucky", "immune", "sunblessed", "attractive", "acid proof", "fire proof", "freeze proof", "poison immunity" };
            foreach (string trait in positiveTraitsToAdd)
            {
                if (!a.hasTrait(trait))
                {
                    a.addTrait(trait);
                }
            }

            return true;
        }

        public static void PromoteHuaShenDaoJi(Actor a)
        {
            // 166666真元时，将DaoJi94晋升为DaoJi95
            if (a.GetXianTu() >= 166666 && a.GetXianTu() < 233332 && a.hasTrait("DaoJi94") && !a.hasTrait("DaoJi95") && !a.hasTrait("DaoJi96"))
            {
                // 先消耗部分真元
                a.ChangeXianTu(-16666);
                
                double promotionRate = 0.0;
                
                // 根据灵根设置不同概率
                if (a.hasTrait("TaiyiLg2")) // 五灵根
                {
                    promotionRate = 0.025;
                }
                else if (a.hasTrait("TaiyiLg3")) // 四灵根
                {
                    promotionRate = 0.05;
                }
                else if (a.hasTrait("TaiyiLg4")) // 三灵根
                {
                    promotionRate = 0.075;
                }
                else if (a.hasTrait("TaiyiLg5")) // 二灵根
                {
                    promotionRate = 0.125;
                }
                else if (a.hasTrait("TaiyiLg6")) // 单灵根
                {
                    promotionRate = 0.175;
                }
                else if (a.hasTrait("TaiyiLg7")) // 天灵根
                {
                    promotionRate = 0.25;
                }
                else
                {
                    promotionRate = 0.05; // 默认概率
                }
                
                // 检查是否有天命印记，必定成功
                if (a.hasTrait("TianDaoyinji"))
                {
                    promotionRate = 1.0;
                }
                
                // 进行晋升判定
                if (UnityEngine.Random.Range(0.0f, 1.0f) <= promotionRate)
                {
                    a.removeTrait("DaoJi94");
                    a.addTrait("DaoJi95");
                    UpdateNameSuffix(a, "DaoJi95");
                }
                // 失败不返还真元
            }
            // 233332真元时，将DaoJi95晋升为DaoJi96
            else if (a.GetXianTu() >= 233332 && a.hasTrait("DaoJi95") && !a.hasTrait("DaoJi96"))
            {
                // 先消耗部分真元
                a.ChangeXianTu(-23333);
                
                double promotionRate = 0.0;
                
                // 根据灵根设置不同概率
                if (a.hasTrait("TaiyiLg2")) // 五灵根
                {
                    promotionRate = 0.0125;
                }
                else if (a.hasTrait("TaiyiLg3")) // 四灵根
                {
                    promotionRate = 0.025;
                }
                else if (a.hasTrait("TaiyiLg4")) // 三灵根
                {
                    promotionRate = 0.0375;
                }
                else if (a.hasTrait("TaiyiLg5")) // 二灵根
                {
                    promotionRate = 0.0625;
                }
                else if (a.hasTrait("TaiyiLg6")) // 单灵根
                {
                    promotionRate = 0.0875;
                }
                else if (a.hasTrait("TaiyiLg7")) // 天灵根
                {
                    promotionRate = 0.125;
                }
                else
                {
                    promotionRate = 0.025; // 默认概率
                }
                
                // 检查是否有天命印记，必定成功
                if (a.hasTrait("TianDaoyinji"))
                {
                    promotionRate = 1.0;
                }
                
                // 进行晋升判定
                if (UnityEngine.Random.Range(0.0f, 1.0f) <= promotionRate)
                {
                    a.removeTrait("DaoJi95");
                    a.addTrait("DaoJi96");
                    UpdateNameSuffix(a, "DaoJi96");
                }
                // 失败不返还真元
            }
        }

        public static bool XianTu6_effectAction(BaseSimObject pTarget, WorldTile pTile = null) 
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            
            // 在合体晋升判定前先进行化神期道基晋升判定
            PromoteHuaShenDaoJi(a);
            // 检查气血值是否小于x，如果是，则趺落境界

            if (a.GetXianTu() <= 299999.99)
            {
                return false;
            }

            // 检查合体境突破限制
            if (XianTuConfig.LimitHeTi)
            {
                return false;
            }

            // 突破合体期需要至少法相以上功法
            string gongFaLevelName = a.GetGongFaLevelName();
            if (gongFaLevelName != "法相阶" && gongFaLevelName != "羽化阶" && gongFaLevelName != "大道阶")
            {
                return false;
            }

            a.ChangeXianTu(-299999);
            double successRate = 0.1;
            if (a.hasTrait("DaoJi96"))
            {
                successRate = 0.05;
            }
            else if (a.hasTrait("DaoJi97"))
            {
                successRate = 0.1;
            }
            
            // 检查是否拥有合体丹，增加15%成功率
            if (a.hasTrait("TupoDanyao5"))
            {
                successRate += 0.15;
            }
            
            // 不同阶别功法对突破合体期概率的加成
            if (gongFaLevelName == "法相阶")
            {
                successRate += 0.1;
            }
            else if (gongFaLevelName == "羽化阶")
            {
                successRate += 0.2;
            }
            else if (gongFaLevelName == "大道阶")
            {
                successRate += 0.3;
            }

            // 添加灵根对突破概率的影响
            if (a.hasTrait("TaiyiLg2")) // 杂灵根
            {
                successRate -= 0.3;
            }
            else if (a.hasTrait("TaiyiLg3")) // 四灵根
            {
                successRate -= 0.2;
            }
            else if (a.hasTrait("TaiyiLg4")) // 三灵根
            {
                successRate -= 0.1;
            }
            else if (a.hasTrait("TaiyiLg5")) // 二灵根
            {
                successRate += 0.01;
            }
            else if (a.hasTrait("TaiyiLg6")) // 单灵根
            {
                successRate += 0.05;
            }
            else if (a.hasTrait("TaiyiLg7")) // 天灵根
            {
                successRate += 0.1;
            }

            // 确保成功率在合理范围内
            successRate = Math.Max(0.0, Math.Min(1.0, successRate));

            // 如果有天人五衰特质，概率减半
            if (a.hasTrait("TaiyiLg9"))
            {
                successRate *= 0.5;
            }

            // 如果有天命印记特质，必定成功
            if (a.hasTrait("TianDaoyinji"))
            {
                successRate = 1.0;
            }
            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f);
            if (randomValue > successRate)
            {
                // 无论成功与否，都删除合体丹
                if (a.hasTrait("TupoDanyao5"))
                {
                    a.removeTrait("TupoDanyao5");
                }
                // 晋升失败，触发雷劫（如果没有不朽道体）
                if (!a.hasTrait("TaiyiLg1"))
                {
                    LeiJieMechanism.TriggerHuaShenToHeTiLeiJie(a);
                }
                if (a.hasTrait("DaoJi96"))
                {
                    a.removeTrait("DaoJi96");
                    a.addTrait("DaoJi97");
                }
                // 突破失败，更新后缀为炼虚
                string currentName = a.getName();
                int lastDashIndex = currentName.LastIndexOf('-');
                string basePart = lastDashIndex >= 0 ? currentName.Substring(0, lastDashIndex).Trim() : currentName;
                a.setName($"{basePart}-返虚");
                return false; // 随机数大于成功率，则操作失败
            }
            
            // 无论成功与否，都删除合体丹
            if (a.hasTrait("TupoDanyao5"))
            {
                a.removeTrait("TupoDanyao5");
            }
            string[] optionalTraits = { "dash", "block", "dodge", "backstep", "deflect_projectile" };
            string selectedTrait = null;
            // 检查是否所有可选特质都已被拥有
            bool allTraitsOwned = optionalTraits.All(trait => a.hasTrait(trait));
            if (!allTraitsOwned)
            {
                var availableTraits = optionalTraits.Where(t => !a.hasTrait(t)).ToList();
                selectedTrait = availableTraits[UnityEngine.Random.Range(0, availableTraits.Count)];
            }


            upTrait(
                "XianTu5",
                "XianTu6",
                a,
                new string[]
                {
                    "tumorInfection",
                    "cursed",
                    "infected",
                    "mushSpores"
                },
                (selectedTrait != null) ?
                new string[] { "freeze_proof", "fire_proof", selectedTrait } :
                new string[] { "freeze_proof", "fire_proof" }
            );
            UpdateNameSuffix(a, "XianTu6");
            ApplyXianTuTitle(a, "XianTu6");
            
            // 根据配置自动收藏合体境角色
            if (XianTuConfig.ShouldAutoCollectForRealm("XianTu6", XianTuConfig.AutoCollectHeTi))
            {
                a.data.favorite = true;
            }
            
            // 合体成功后，移除所有化神期道基特质，并只获得FaXiang1特质
            // 检查角色是否拥有DaoJi94~97特质
            if (a.hasTrait("DaoJi94"))
            {
                a.removeTrait("DaoJi94");
            }
            if (a.hasTrait("DaoJi95"))
            {
                a.removeTrait("DaoJi95");
            }
            if (a.hasTrait("DaoJi96"))
            {
                a.removeTrait("DaoJi96");
            }
            if (a.hasTrait("DaoJi97"))
            {
                a.removeTrait("DaoJi97");
            }
            // 只添加FaXiang1特质
            a.addTrait("FaXiang1");
            // 合体期增加1~1000点悟性，只增加一次
            if (!a.hasTrait("XianTu6_WuXingIncreased"))
            {
                int wuXingIncrease = UnityEngine.Random.Range(500, 1001); // 1-1000
                a.ChangeWuXing(wuXingIncrease);
                a.addTrait("XianTu6_WuXingIncreased");
            }

            // 突破成功后消除指定负面特质
            string[] negativeTraitsToRemove = { "Stupid", "Short Sighted", "Soft Skin", "Fragile Trait", "Ugly", "Skin Burns", "One Eyed", "Madness", "TaiyiLg9" };
            foreach (string trait in negativeTraitsToRemove)
            {
                if (a.hasTrait(trait))
                {
                    a.removeTrait(trait);
                }
            }

            // 突破成功后添加指定正面特质
            string[] positiveTraitsToAdd = { "genius", "eagle eyed", "strong minded", "lucky", "immune", "sunblessed", "attractive", "acid proof", "fire proof", "freeze proof", "poison immunity" };
            foreach (string trait in positiveTraitsToAdd)
            {
                if (!a.hasTrait(trait))
                {
                    a.addTrait(trait);
                }
            }

            return true;
        }

        public static void PromoteHeTiFaXiang(Actor a)
        {
            // 400000真元时，将FaXiang1晋升为FaXiang2
            if (a.GetXianTu() >= 400000 && a.GetXianTu() < 500000 && a.hasTrait("FaXiang1") && !a.hasTrait("FaXiang2") && !a.hasTrait("FaXiang3"))
            {
                // 先消耗部分真元
                a.ChangeXianTu(-40000);
                
                double promotionRate = 0.0;
                
                // 根据灵根设置不同概率
                if (a.hasTrait("TaiyiLg2")) // 五灵根
                {
                    promotionRate = 0.0125;
                }
                else if (a.hasTrait("TaiyiLg3")) // 四灵根
                {
                    promotionRate = 0.025;
                }
                else if (a.hasTrait("TaiyiLg4")) // 三灵根
                {
                    promotionRate = 0.0375;
                }
                else if (a.hasTrait("TaiyiLg5")) // 二灵根
                {
                    promotionRate = 0.0625;
                }
                else if (a.hasTrait("TaiyiLg6")) // 单灵根
                {
                    promotionRate = 0.0875;
                }
                else if (a.hasTrait("TaiyiLg7")) // 天灵根
                {
                    promotionRate = 0.125;
                }
                else
                {
                    promotionRate = 0.025; // 默认概率
                }
                
                // 检查是否有天命印记，必定成功
                if (a.hasTrait("TianDaoyinji"))
                {
                    promotionRate = 1.0;
                }
                
                // 进行晋升判定
                if (UnityEngine.Random.Range(0.0f, 1.0f) <= promotionRate)
                {
                    a.removeTrait("FaXiang1");
                    a.addTrait("FaXiang2");
                    UpdateNameSuffix(a, "FaXiang2");
                }
                // 失败不返还真元
            }
            // 500000真元时，将FaXiang2晋升为FaXiang3
            else if (a.GetXianTu() >= 500000 && a.hasTrait("FaXiang2") && !a.hasTrait("FaXiang3"))
            {
                // 先消耗部分真元
                a.ChangeXianTu(-50000);
                
                double promotionRate = 0.0;
                
                // 根据灵根设置不同概率
                if (a.hasTrait("TaiyiLg2")) // 五灵根
                {
                    promotionRate = 0.00625;
                }
                else if (a.hasTrait("TaiyiLg3")) // 四灵根
                {
                    promotionRate = 0.0125;
                }
                else if (a.hasTrait("TaiyiLg4")) // 三灵根
                {
                    promotionRate = 0.01875;
                }
                else if (a.hasTrait("TaiyiLg5")) // 二灵根
                {
                    promotionRate = 0.03125;
                }
                else if (a.hasTrait("TaiyiLg6")) // 单灵根
                {
                    promotionRate = 0.04375;
                }
                else if (a.hasTrait("TaiyiLg7")) // 天灵根
                {
                    promotionRate = 0.0625;
                }
                else
                {
                    promotionRate = 0.0125; // 默认概率
                }
                
                // 检查是否有天命印记，必定成功
                if (a.hasTrait("TianDaoyinji"))
                {
                    promotionRate = 1.0;
                }
                
                // 进行晋升判定
                if (UnityEngine.Random.Range(0.0f, 1.0f) <= promotionRate)
                {
                    a.removeTrait("FaXiang2");
                    a.addTrait("FaXiang3");
                    UpdateNameSuffix(a, "FaXiang3");
                }
                // 失败不返还真元
            }
        }

        public static bool XianTu7_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            
            // 在大乘晋升判定前先进行合体期法相晋升判定
            PromoteHeTiFaXiang(a);

            if (a.GetXianTu() <= 599999.99)
            {
                return false;
            }

            // 检查大乘境突破限制
            if (XianTuConfig.LimitDaCheng)
            {
                return false;
            }

            // 突破大乘期需要至少道果以上功法
            string gongFaLevelName = a.GetGongFaLevelName();
            if (gongFaLevelName != "羽化阶" && gongFaLevelName != "大道阶")
            {
                return false;
            }

            a.ChangeXianTu(-599999);
            double successRate = 0.05;
            if (a.hasTrait("FaXiang3"))
            {
                successRate = 0.05;
            }
            else if (a.hasTrait("FaXiang4"))
            {
                successRate = 0.1;
            }
            
            // 检查是否拥有大乘丹，增加10%成功率
            if (a.hasTrait("TupoDanyao6"))
            {
                successRate += 0.1;
            }
            
            // 不同阶别功法对突破大乘期概率的加成
            if (gongFaLevelName == "羽化阶")
            {
                successRate += 0.1;
            }
            else if (gongFaLevelName == "大道阶")
            {
                successRate += 0.2;
            }

            // 添加灵根对突破概率的影响
            if (a.hasTrait("TaiyiLg2")) // 杂灵根
            {
                successRate -= 0.3;
            }
            else if (a.hasTrait("TaiyiLg3")) // 四灵根
            {
                successRate -= 0.2;
            }
            else if (a.hasTrait("TaiyiLg4")) // 三灵根
            {
                successRate -= 0.1;
            }
            else if (a.hasTrait("TaiyiLg5")) // 二灵根
            {
                successRate += 0.01;
            }
            else if (a.hasTrait("TaiyiLg6")) // 单灵根
            {
                successRate += 0.05;
            }
            else if (a.hasTrait("TaiyiLg7")) // 天灵根
            {
                successRate += 0.1;
            }

            // 确保成功率在合理范围内
            successRate = Math.Max(0.0, Math.Min(1.0, successRate));

            // 如果有天人五衰特质，概率减半
            if (a.hasTrait("TaiyiLg9"))
            {
                successRate *= 0.5;
            }
            
            // 如果有天命印记特质，必定成功
            if (a.hasTrait("TianDaoyinji"))
            {
                successRate = 1.0;
            }

            if (UnityEngine.Random.Range(0.0f, 1.0f) > successRate)
            {
                // 无论成功与否，都删除大乘丹
                if (a.hasTrait("TupoDanyao6"))
                {
                    a.removeTrait("TupoDanyao6");
                }
                // 晋升失败，触发雷劫（如果没有不朽道体）
                if (!a.hasTrait("TaiyiLg1"))
                {
                    LeiJieMechanism.TriggerHeTiToDaChengLeiJie(a);
                }
                if (a.hasTrait("FaXiang3"))
                {
                    a.removeTrait("FaXiang3");
                    a.addTrait("FaXiang4");
                }
                // 突破失败，更新后缀为渡劫
                string currentName = a.getName();
                int lastDashIndex = currentName.LastIndexOf('-');
                string basePart = lastDashIndex >= 0 ? currentName.Substring(0, lastDashIndex).Trim() : currentName;
                a.setName($"{basePart}-大乘");
                return false;
            }
            
            // 无论成功与否，都删除大乘丹
            if (a.hasTrait("TupoDanyao6"))
            {
                a.removeTrait("TupoDanyao6");
            }

            upTrait(
                "XianTu6",
                "XianTu7",
                a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores" },
                new string[] { "tough" }
            );
            UpdateNameSuffix(a, "XianTu7");
            ApplyXianTuTitle(a, "XianTu7");

            // 根据配置自动收藏大乘境角色
            if (XianTuConfig.ShouldAutoCollectForRealm("XianTu7", XianTuConfig.AutoCollectDaCheng))
            {
                a.data.favorite = true;
            }

            // 大乘成功后，移除所有合体期法相特质，并只获得FaXiang5特质
            // 检查角色是否拥有FaXiang1~4特质
            if (a.hasTrait("FaXiang1"))
            {
                a.removeTrait("FaXiang1");
            }
            if (a.hasTrait("FaXiang2"))
            {
                a.removeTrait("FaXiang2");
            }
            if (a.hasTrait("FaXiang3"))
            {
                a.removeTrait("FaXiang3");
            }
            if (a.hasTrait("FaXiang4"))
            {
                a.removeTrait("FaXiang4");
            }
            // 只添加FaXiang5特质
            a.addTrait("FaXiang5");

            // 大乘期增加1~10000点悟性，只增加一次
            if (!a.hasTrait("XianTu7_WuXingIncreased"))
            {
                int wuXingIncrease = UnityEngine.Random.Range(1000, 10001); // 1-10000
                a.ChangeWuXing(wuXingIncrease);
                a.addTrait("XianTu7_WuXingIncreased");
            }

            // 突破成功后消除指定负面特质
            string[] negativeTraitsToRemove = { "Stupid", "Short Sighted", "Soft Skin", "Fragile Trait", "Ugly", "Skin Burns", "One Eyed", "Madness", "TaiyiLg9" };
            foreach (string trait in negativeTraitsToRemove)
            {
                if (a.hasTrait(trait))
                {
                    a.removeTrait(trait);
                }
            }

            // 突破成功后添加指定正面特质
            string[] positiveTraitsToAdd = {"genius", "eagle eyed", "strong minded", "lucky", "immune", "sunblessed", "attractive", "acid proof", "fire proof", "freeze proof", "poison immunity"};
            foreach (string trait in positiveTraitsToAdd)
            {
                if (!a.hasTrait(trait))
                {
                    a.addTrait(trait);
                }
            }

            return true;
        }

        public static void PromoteDaChengFaXiang(Actor a)
        {
            // 700000真元时，将FaXiang5晋升为FaXiang6
            if (a.GetXianTu() >= 700000 && a.GetXianTu() < 800000 && a.hasTrait("FaXiang5") && !a.hasTrait("FaXiang6") && !a.hasTrait("FaXiang7"))
            {
                // 先消耗部分真元
                a.ChangeXianTu(-70000);
                
                double promotionRate = 0.0;
                
                // 根据灵根设置不同概率
                if (a.hasTrait("TaiyiLg2")) // 五灵根
                {
                    promotionRate = 0.00625;
                }
                else if (a.hasTrait("TaiyiLg3")) // 四灵根
                {
                    promotionRate = 0.0125;
                }
                else if (a.hasTrait("TaiyiLg4")) // 三灵根
                {
                    promotionRate = 0.01875;
                }
                else if (a.hasTrait("TaiyiLg5")) // 二灵根
                {
                    promotionRate = 0.03125;
                }
                else if (a.hasTrait("TaiyiLg6")) // 单灵根
                {
                    promotionRate = 0.04375;
                }
                else if (a.hasTrait("TaiyiLg7")) // 天灵根
                {
                    promotionRate = 0.0625;
                }
                else
                {
                    promotionRate = 0.0125; // 默认概率
                }
                
                // 检查是否有天命印记，必定成功
                if (a.hasTrait("TianDaoyinji"))
                {
                    promotionRate = 1.0;
                }
                
                // 进行晋升判定
                if (UnityEngine.Random.Range(0.0f, 1.0f) <= promotionRate)
                {
                    a.removeTrait("FaXiang5");
                    a.addTrait("FaXiang6");
                    UpdateNameSuffix(a, "FaXiang6");
                }
                // 失败不返还真元
            }
            // 800000真元时，将FaXiang6晋升为FaXiang7
            else if (a.GetXianTu() >= 800000 && a.hasTrait("FaXiang6") && !a.hasTrait("FaXiang7"))
            {
                // 先消耗部分真元
                a.ChangeXianTu(-80000);
                
                double promotionRate = 0.0;
                
                // 根据灵根设置不同概率
                if (a.hasTrait("TaiyiLg2")) // 五灵根
                {
                    promotionRate = 0.003125;
                }
                else if (a.hasTrait("TaiyiLg3")) // 四灵根
                {
                    promotionRate = 0.00625;
                }
                else if (a.hasTrait("TaiyiLg4")) // 三灵根
                {
                    promotionRate = 0.009375;
                }
                else if (a.hasTrait("TaiyiLg5")) // 二灵根
                {
                    promotionRate = 0.015625;
                }
                else if (a.hasTrait("TaiyiLg6")) // 单灵根
                {
                    promotionRate = 0.021875;
                }
                else if (a.hasTrait("TaiyiLg7")) // 天灵根
                {
                    promotionRate = 0.03125;
                }
                else
                {
                    promotionRate = 0.00625; // 默认概率
                }
                
                // 检查是否有天命印记，必定成功
                if (a.hasTrait("TianDaoyinji"))
                {
                    promotionRate = 1.0;
                }
                
                // 进行晋升判定
                if (UnityEngine.Random.Range(0.0f, 1.0f) <= promotionRate)
                {
                    a.removeTrait("FaXiang6");
                    a.addTrait("FaXiang7");
                    UpdateNameSuffix(a, "FaXiang7");
                }
                // 失败不返还真元
            }
        }

        public static bool XianTu8_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            
            // 在真仙晋升判定前先进行大乘期法相晋升判定
            PromoteDaChengFaXiang(a);

            if (a.GetXianTu() <= 999999.99)
            {
                return false;
            }

            // 检查半仙境突破限制
            if (XianTuConfig.LimitBanXian)
            {
                return false;
            }

            // 突破半仙期需要至少大道功法
            string gongFaLevelName = a.GetGongFaLevelName();
            if (gongFaLevelName != "大道阶")
            {
                return false;
            }

            a.ChangeXianTu(-999999);
            double successRate = 0.01;
            if (a.hasTrait("FaXiang7"))
            {
                successRate = 0.05;
            }
            else if (a.hasTrait("FaXiang8"))
            {
                successRate = 0.1;
            }
            
            // 检查是否拥有地仙丹，增加5%成功率
            if (a.hasTrait("TupoDanyao7"))
            {
                successRate += 0.05;
            }
            
            // 大道阶功法增加10%突破半仙期概率
            if (gongFaLevelName == "大道阶")
            {
                successRate += 0.1;
            }

            // 添加灵根对突破概率的影响
            if (a.hasTrait("TaiyiLg2")) // 杂灵根
            {
                successRate -= 0.9;
            }
            else if (a.hasTrait("TaiyiLg3")) // 四灵根
            {
                successRate -= 0.5;
            }
            else if (a.hasTrait("TaiyiLg4")) // 三灵根
            {
                successRate -= 0.3;
            }
            else if (a.hasTrait("TaiyiLg5")) // 二灵根
            {
                successRate += 0.01;
            }
            else if (a.hasTrait("TaiyiLg6")) // 单灵根
            {
                successRate += 0.05;
            }
            else if (a.hasTrait("TaiyiLg7")) // 天灵根
            {
                successRate += 0.1;
            }

            // 确保成功率在合理范围内
            successRate = Math.Max(0.0, Math.Min(1.0, successRate));

            // 如果有天人五衰特质，概率减半
            if (a.hasTrait("TaiyiLg9"))
            {
                successRate *= 0.5;
            }
            
            // 如果有天命印记特质，必定成功
            if (a.hasTrait("TianDaoyinji"))
            {
                successRate = 1.0;
            }

            if (UnityEngine.Random.Range(0.0f, 1.0f) > successRate)
            {
                // 无论成功与否，都删除地仙丹
                if (a.hasTrait("TupoDanyao7"))
                {
                    a.removeTrait("TupoDanyao7");
                }
                // 晋升失败，触发雷劫（如果没有不朽道体）
                if (!a.hasTrait("TaiyiLg1"))
                {
                    LeiJieMechanism.TriggerDaChengToDiXianLeiJie(a);
                }
                if (a.hasTrait("FaXiang7"))
                {
                    a.removeTrait("FaXiang7");
                    a.addTrait("FaXiang8");
                }
                // 更新名称后缀为半仙
                string currentName = a.data.name;
                string baseName = currentName;
                if (currentName.Contains("-"))
                {
                    baseName = currentName.Split('-')[0];
                }
                a.data.name = baseName + "-半仙";
                return false;
            }
            
            // 无论成功与否，都删除地仙丹
            if (a.hasTrait("TupoDanyao7"))
            {
                a.removeTrait("TupoDanyao7");
            }

            upTrait(
                "XianTu7",
                "XianTu8",
                a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "XianTu07" },
                new string[] { "XianTu08", "strong_minded", "immune" }
            );
            UpdateNameSuffix(a, "XianTu8");
            ApplyXianTuTitle(a, "XianTu8");
            // 根据配置自动收藏半仙境角色
            if (XianTuConfig.ShouldAutoCollectForRealm("XianTu8", XianTuConfig.AutoCollectBanXian))
            {
                a.data.favorite = true;
            }

            // 突破成功后消除指定负面特质
            string[] negativeTraitsToRemove = { "stupid", "short sighted", "soft skin", "fragile trait", "ugly", "skin burns", "one eyed", "madness", "TaiyiLg9" };
            foreach (string trait in negativeTraitsToRemove)
            {
                if (a.hasTrait(trait))
                {
                    a.removeTrait(trait);
                }
            }

            // 突破成功后添加指定正面特质
            string[] positiveTraitsToAdd = { "genius", "eagle eyed", "strong minded", "lucky", "immune", "sunblessed", "attractive", "acid proof", "fire proof", "freeze proof", "poison immunity", "immortal" };
            foreach (string trait in positiveTraitsToAdd)
            {
                if (!a.hasTrait(trait))
                {
                    a.addTrait(trait);
                }
            }

            return true;
        }
        
        // 用于生成道果尊号的修饰名词池
        private static readonly string[] daoGuoAdjectives = new string[]
        {
            "混元", "阴阳", "无极", "太虚", "鸿蒙", "混沌", "造化", "三才", "四象", 
            "五行", "六合", "七星", "八卦", "九宫", "十方", "玄黄", "紫霄", "太初", 
            "太始", "太易", "太素", "天罡", "地煞", "玄元", "紫府", "玉虚", "碧游",
        "混元","太初","太玄","太始","太易","太清","玉清","上清","虚无","混沌","金庭","玉虚","紫霄","青冥","玄黄","鸿蒙",
        "乾元","坤元","离火","坎水","震雷","巽风","艮山","兑泽","太极","两仪","四象","八卦","九宫","十方","无量","无极",
        "元始","道德","灵宝","长生","不死","永恒","不朽","不灭","天罡","地煞","紫微","勾陈","玄武","朱雀","青龙","白虎",
        "麒麟","凤凰","神龟","蛟龙","鲲鹏","饕餮","混沌","穷奇","梼杌","赑屃","狴犴","睚眦","椒图","蒲牢","螭吻","狻猊",
        "趴蝮","负屃","霸下","貔貅","朝天","黄泉","九泉","幽都","冥府","地府","阴曹","冥司","幽府","幽宫","冥宫","幽殿",
        "冥殿","幽狱","冥狱","幽牢","冥牢","幽界","冥界","幽壤","冥壤","幽泉","冥泉","幽渊","冥渊","幽壑","冥壑","幽海",
        "冥海","幽湖","冥湖","幽溪","冥溪","幽涧","冥涧","幽山","冥山","幽岩","冥岩","幽石","冥石","幽林","冥林","幽草",
        "冥草","幽花","冥花","幽鸟","冥鸟","幽兽","冥兽","幽魂","冥魂","幽灵","冥灵","幽鬼","冥鬼","幽神","冥神","幽吏",
        "冥吏","幽差","冥差","阎罗","阎王","判官","奈何","忘川","彼岸","三途","鬼门","黄泉","望乡","森罗","枉死","地狱",
        "寒狱","沸狱","剥皮","拔舌","铁树","盘狱","蒸狱","柱狱","刀狱","碓狱","裂狱","凌迟","斩狱","首狱","道藏","金简",
        "玉牒","金策","玉书","天书","堪舆","符玄","玄机","奥秘","灵泽","玄泽","天浆","甘泽","嘉澍","甘霖","膏雨","瑞雨",
        "祥雨","喜雨","廉纤","轻丝","跳珠","银竹","白雨","陵雨","暴雨","雷雨","风雨","霜雨","朝雨","暮雨","夜雨","东风",
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

        // 应用道果尊号的方法：生成"2个不重复名词+特定后缀"格式的尊号
        private static void ApplyDaoGuoTitle(Actor actor, string daoGuoTrait)
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
            
            // 3. 确定特定后缀
            string titleSuffix = "仙王"; // 默认后缀
            switch (daoGuoTrait)
            {
                case "DaoGuo1": // 天仙道果
                case "DaoGuo2": // 地仙道果
                case "DaoGuo3": // 神仙道果
                case "DaoGuo4": // 人仙道果
                case "DaoGuo5": // 鬼仙道果
                    titleSuffix = "仙王"; 
                    break;
                case "DaoGuo6": // 真魔道果
                    titleSuffix = "魔君"; 
                    break;
                case "DaoGuo7": // 丹仙道果
                    titleSuffix = "丹君"; 
                    break;
                case "DaoGuo8": // 器仙道果
                    titleSuffix = "器君"; 
                    break;
                case "DaoGuo9": // 御仙道果
                    titleSuffix = "兽君"; 
                    break;
                case "DaoGuo91": // 灵仙道果
                    titleSuffix = "灵君"; 
                    break;
                case "DaoGuo92": // 阵仙道果
                    titleSuffix = "阵君"; 
                    break;
                case "DaoGuo93": // 符仙道果
                    titleSuffix = "符君"; 
                    break;
                case "DaoGuo94": // 剑仙道果
                    titleSuffix = "剑君"; 
                    break;
                case "DaoGuo95": // 儒仙道果
                    titleSuffix = "亚圣"; 
                    break;
            }
            
            // 4. 随机选择2个不重复的修饰名词
            int firstIndex = UnityEngine.Random.Range(0, daoGuoAdjectives.Length);
            int secondIndex;
            do
            {
                secondIndex = UnityEngine.Random.Range(0, daoGuoAdjectives.Length);
            } while (secondIndex == firstIndex);
            
            string title = $"{daoGuoAdjectives[firstIndex]}{daoGuoAdjectives[secondIndex]}{titleSuffix}";
            
            // 5. 设置新名称：新尊号 + · + 基础名称 + 境界后缀
            actor.setName($"{title}·{basePart}{suffixPart}");
        }

        // 道果授予的统一机制：包含职业道果和天地神人鬼五大道果
        public static bool GrantDaoGuoIfEligible(Actor a)
        {
            if (a == null)
            {
                return false;
            }
            
            // 检查是否已经拥有任何道果特质 (检查所有的道果系列特质)
            bool hasAnyDaoGuo = HasAnyDaoGuo(a);
                
            // 如果没有道果特质，且拥有200万以上真元
            if (!hasAnyDaoGuo && a.hasTrait("XianTu8") && a.GetXianTu() >= 2000000.0)
            {
                // 先检查是否满足职业道果条件
                string careerDaoGuoTrait = null;
                string careerSuffix = null;
                
                // 检查各职业道果条件
                if (a.hasTrait("XiuxianLiuyi1") && a.GetCareerLevel() == 7 && !a.hasTrait("DaoGuo7"))
                {
                    careerDaoGuoTrait = "DaoGuo7";
                    careerSuffix = "丹仙";
                }
                else if (a.hasTrait("XiuxianLiuyi2") && a.GetCareerLevel() == 7 && !a.hasTrait("DaoGuo8"))
                {
                    careerDaoGuoTrait = "DaoGuo8";
                    careerSuffix = "器仙";
                }
                else if (a.hasTrait("XiuxianLiuyi3") && a.GetCareerLevel() == 7 && !a.hasTrait("DaoGuo9"))
                {
                    careerDaoGuoTrait = "DaoGuo9";
                    careerSuffix = "御仙";
                }
                else if (a.hasTrait("XiuxianLiuyi4") && a.GetCareerLevel() == 7 && !a.hasTrait("DaoGuo91"))
                {
                    careerDaoGuoTrait = "DaoGuo91";
                    careerSuffix = "灵仙";
                }
                else if (a.hasTrait("XiuxianLiuyi5") && a.GetCareerLevel() == 7 && !a.hasTrait("DaoGuo92"))
                {
                    careerDaoGuoTrait = "DaoGuo92";
                    careerSuffix = "阵仙";
                }
                else if (a.hasTrait("XiuxianLiuyi6") && a.GetCareerLevel() == 7 && !a.hasTrait("DaoGuo93"))
                {
                    careerDaoGuoTrait = "DaoGuo93";
                    careerSuffix = "符仙";
                }
                else if (a.hasTrait("XiuxianLiuyi7") && a.GetCareerLevel() == 7 && !a.hasTrait("DaoGuo6"))
                {
                    careerDaoGuoTrait = "DaoGuo6";
                    careerSuffix = "真魔";
                }
                else if (a.hasTrait("XiuxianLiuyi8") && a.GetCareerLevel() == 7 && !a.hasTrait("DaoGuo94"))
                {
                    careerDaoGuoTrait = "DaoGuo94";
                    careerSuffix = "剑仙";
                }
                else if (a.hasTrait("XiuxianLiuyi9") && a.GetCareerLevel() == 7 && !a.hasTrait("DaoGuo95"))
                {
                    careerDaoGuoTrait = "DaoGuo95";
                    careerSuffix = "儒仙";
                }
                
                // 50%概率获得职业道果（如果满足条件），50%概率获得五仙道果之一
                float randomChance = UnityEngine.Random.value;
                if (randomChance < 0.5f && careerDaoGuoTrait != null)
                {
                    // 50%概率获得职业道果
                    a.addTrait(careerDaoGuoTrait, false);
                    a.UpdateNameSuffix(careerSuffix);
                    ApplyDaoGuoTitle(a, careerDaoGuoTrait);
                    return true;
                }
                else
                {
                    // 50%概率或不满足职业道果条件时，获得五仙道果之一
                    int randomIndex = UnityEngine.Random.Range(0, 5);
                    string daoGuoTrait = "DaoGuo" + (randomIndex + 1); // DaoGuo1到DaoGuo5对应天地神人鬼
                    a.addTrait(daoGuoTrait, false);
                    
                    // 根据选择的道果更新后缀
                    string suffix = "登仙"; // 默认后缀
                    switch (randomIndex)
                    {
                        case 0: suffix = "天仙"; break;
                        case 1: suffix = "地仙"; break;
                        case 2: suffix = "神仙"; break;
                        case 3: suffix = "人仙"; break;
                        case 4: suffix = "鬼仙"; break;
                    }
                    a.UpdateNameSuffix(suffix);
                    ApplyDaoGuoTitle(a, daoGuoTrait);
                    return true;
                }
            }
            return false;
        }
        

        // 检查角色是否拥有任何道果特质（包括所有道果系列特质，含DaoGuo96）
        public static bool HasAnyDaoGuo(Actor a)
        {
            if (a == null)
            {
                return false;
            }
            
            // 检查DaoGuo1到DaoGuo96的所有道果
            for (int i = 1; i <= 96; i++)
            {
                if (a.hasTrait("DaoGuo" + i))
                {
                    return true;
                }
            }
            return false;
        }
        
        //仙途的恢复生命值效果
        public static bool XianTu1_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor actor = pTarget.a;
            if (actor.data.health < actor.getMaxHealth())
            {
                // 获取当前序列值
                float xianTuValue = actor.GetXianTu();
                // 计算回血量：序列值的10%（可根据平衡性调整比例）
                int healAmount = Mathf.RoundToInt(xianTuValue * 0.001f);
                // 至少回复1点生命
                if (healAmount < 1) healAmount = 1; 
        
                actor.restoreHealth(healAmount);
                actor.spawnParticle(Toolbox.color_heal);
            }
            return true;
        }
        public static bool XianTu2_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor actor = pTarget.a;
            if (actor.data.health < actor.getMaxHealth())
            {
                // 获取当前序列值
                float xianTuValue = actor.GetXianTu();
                // 计算回血量：序列值的20%（可根据平衡性调整比例）
                int healAmount = Mathf.RoundToInt(xianTuValue * 0.001f);
                // 至少回复1点生命
                if (healAmount < 1) healAmount = 1; 
        
                actor.restoreHealth(healAmount);
                actor.spawnParticle(Toolbox.color_heal);
            }
            return true;
        }
        public static bool XianTu3_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor actor = pTarget.a;
            if (actor.data.health < actor.getMaxHealth())
            {
                // 获取当前序列值
                float xianTuValue = actor.GetXianTu();
                // 计算回血量：序列值的30%（可根据平衡性调整比例）
                int healAmount = Mathf.RoundToInt(xianTuValue * 0.001f);
                // 至少回复1点生命
                if (healAmount < 1) healAmount = 1; 
        
                actor.restoreHealth(healAmount);
                actor.spawnParticle(Toolbox.color_heal);
            }
            return true;
        }
        public static bool XianTu4_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor actor = pTarget.a;
            if (actor.data.health < actor.getMaxHealth())
            {
                // 获取当前序列值
                float xianTuValue = actor.GetXianTu();
                // 计算回血量：序列值的40%（可根据平衡性调整比例）
                int healAmount = Mathf.RoundToInt(xianTuValue * 0.001f);
                // 至少回复1点生命
                if (healAmount < 1) healAmount = 1; 
        
                actor.restoreHealth(healAmount);
                actor.spawnParticle(Toolbox.color_heal);
            }
            return true;
        }
        public static bool XianTu5_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor actor = pTarget.a;
            if (actor.data.health < actor.getMaxHealth())
            {
                // 获取当前序列值
                float xianTuValue = actor.GetXianTu();
                // 计算回血量：序列值的50%（可根据平衡性调整比例）
                int healAmount = Mathf.RoundToInt(xianTuValue * 0.001f);
                // 至少回复1点生命
                if (healAmount < 1) healAmount = 1; 
        
                actor.restoreHealth(healAmount);
                actor.spawnParticle(Toolbox.color_heal);
            }
            return true;
        }
        public static bool XianTu6_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor actor = pTarget.a;
            if (actor.data.health < actor.getMaxHealth())
            {
                // 获取当前序列值
                float xianTuValue = actor.GetXianTu();
                // 计算回血量：序列值的60%（可根据平衡性调整比例）
                int healAmount = Mathf.RoundToInt(xianTuValue * 0.001f);
                // 至少回复1点生命
                if (healAmount < 1) healAmount = 1; 
        
                actor.restoreHealth(healAmount);
                actor.spawnParticle(Toolbox.color_heal);
            }
            return true;
        }
        public static bool XianTu7_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor actor = pTarget.a;
            if (actor.data.health < actor.getMaxHealth())
            {
                // 获取当前序列值
                float xianTuValue = actor.GetXianTu();
                // 计算回血量：序列值的70%（可根据平衡性调整比例）
                int healAmount = Mathf.RoundToInt(xianTuValue * 0.001f);
                // 至少回复1点生命
                if (healAmount < 1) healAmount = 1; 
        
                actor.restoreHealth(healAmount);
                actor.spawnParticle(Toolbox.color_heal);
            }
            return true;
        }
        public static bool XianTu8_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor actor = pTarget.a;
            if (actor.data.health < actor.getMaxHealth())
            {
                // 获取当前序列值
                float xianTuValue = actor.GetXianTu();
                // 计算回血量：序列值的80%（可根据平衡性调整比例）
                int healAmount = Mathf.RoundToInt(xianTuValue * 0.0001f);
                // 至少回复1点生命
                if (healAmount < 1) healAmount = 1; 
        
                actor.restoreHealth(healAmount);
                actor.spawnParticle(Toolbox.color_heal);
            }
            return true;
        }
        
        public static bool XianTu9_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;

            if (a.GetXianTu() <= 2999999.99)
            {
                return false;
            }

            // 检查仙境突破限制
            if (XianTuConfig.LimitXianJing)
            {
                return false;
            }

            // 突破仙境需要至少大道阶以上功法
            string gongFaLevelName = a.GetGongFaLevelName();
            if (gongFaLevelName != "大道阶")
            {
                return false;
            }

            // 扣除300万真元
            a.ChangeXianTu(-3000000);
            
            // 基础成功率较低
            double successRate = 0.01;
            if (a.hasTrait("FaXiang5"))
            {
                successRate = 0.01;
            }
            else if (a.hasTrait("FaXiang6"))
            {
                successRate = 0.1;
            }
            else if (a.hasTrait("FaXiang7"))
            {
                successRate = 0.3;
            }
            else if (a.hasTrait("FaXiang8"))
            {
                successRate = 0.5;
            }
            

            // 添加灵根对突破概率的影响
            if (a.hasTrait("TaiyiLg2")) // 杂灵根
            {
                successRate -= 0.9;
            }
            else if (a.hasTrait("TaiyiLg3")) // 四灵根
            {
                successRate -= 0.5;
            }
            else if (a.hasTrait("TaiyiLg4")) // 三灵根
            {
                successRate -= 0.1;
            }
            else if (a.hasTrait("TaiyiLg5")) // 二灵根
            {
                successRate += 0.1;
            }
            else if (a.hasTrait("TaiyiLg6")) // 单灵根
            {
                successRate += 0.3;
            }
            else if (a.hasTrait("TaiyiLg7")) // 天灵根
            {
                successRate += 0.5;
            }

            // 确保成功率在合理范围内
            successRate = Math.Max(0.0, Math.Min(1.0, successRate));

            // 如果有天人五衰特质，概率减半
            if (a.hasTrait("TaiyiLg9"))
            {
                successRate *= 0.5;
            }

            // 如果有天命印记特质，必定成功
            if (a.hasTrait("TianDaoyinji"))
            {
                successRate = 1.0;
            }
            if (UnityEngine.Random.Range(0.0f, 1.0f) > successRate)
            {
                // 更新名称后缀为玄仙
                string currentName = a.data.name;
                string baseName = currentName;
                if (currentName.Contains("-"))
                {
                    baseName = currentName.Split('-')[0];
                }
                a.data.name = baseName + "-玄仙";
                return false;
            }
            

            upTrait(
                "XianTu8",
                "XianTu9",
                a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "XianTu08" },
                new string[] { "XianTu09", "strong_minded", "immune" }
            );
            // 突破仙王成功后授予小世界FaXiang9特质
            if (!a.hasTrait("FaXiang9"))
            {
                a.addTrait("FaXiang9");
            }
            UpdateNameSuffix(a, "XianTu9");
            ApplyXianTuTitle(a, "XianTu9");
            // 根据配置自动收藏仙境角色
            if (XianTuConfig.ShouldAutoCollectForRealm("XianTu9", XianTuConfig.AutoCollectXianJing))
            {
                a.data.favorite = true;
            }

            // 突破成功后消除指定负面特质
            string[] negativeTraitsToRemove = { "stupid", "short sighted", "soft skin", "fragile trait", "ugly", "skin burns", "one eyed", "madness", "TaiyiLg9" };
            foreach (string trait in negativeTraitsToRemove)
            {
                if (a.hasTrait(trait))
                {
                    a.removeTrait(trait);
                }
            }

            // 突破成功后添加指定正面特质
            string[] positiveTraitsToAdd = { "genius", "eagle eyed", "strong minded", "lucky", "immune", "sunblessed", "attractive", "acid proof", "fire proof", "freeze proof", "poison immunity", "immortal" };
            foreach (string trait in positiveTraitsToAdd)
            {
                if (!a.hasTrait(trait))
                {
                    a.addTrait(trait);
                }
            }

            return true;
        }
        
        public static bool XianTu91_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;

            // 拥有太乙印记的角色无法再次晋升
            if (a.hasTrait("TaiYiyinji"))
            {
                return false;
            }

            // 基础要求：600万真元
            if (a.GetXianTu() <= 5999999.99)
            {
                return false;
            }

            // 必须拥有混沌世界(FaXiang93)
            if (!a.hasTrait("FaXiang92"))
            {
                return false;
            }

            // 扣除600万真元
            a.ChangeXianTu(-1000000);
            
            // 基础成功率较低
            double successRate = 0.01;
            
            // 根据法相计算概率
            if (a.hasTrait("FaXiang5")) // 小世界
            {
                successRate = 0.01;
            }
            else if (a.hasTrait("FaXiang6")) // 小千世界
            {
                successRate = 0.05;
            }
            else if (a.hasTrait("FaXiang7")) // 大千世界
            {
                successRate = 0.1;
            }
            else if (a.hasTrait("FaXiang8")) // 混沌世界
            {
                successRate = 0.2;
            }
            
            // 添加灵根对突破概率的影响
            if (a.hasTrait("TaiyiLg2")) // 杂灵根
            {
                successRate -= 0.9;
            }
            else if (a.hasTrait("TaiyiLg3")) // 四灵根
            {
                successRate -= 0.5;
            }
            else if (a.hasTrait("TaiyiLg4")) // 三灵根
            {
                successRate -= 0.1;
            }
            else if (a.hasTrait("TaiyiLg5")) // 二灵根
            {
                successRate += 0.1;
            }
            else if (a.hasTrait("TaiyiLg6")) // 单灵根
            {
                successRate += 0.3;
            }
            else if (a.hasTrait("TaiyiLg7")) // 天灵根
            {
                successRate += 0.5;
            }

            // 确保成功率在合理范围内
            successRate = Math.Max(0.0, Math.Min(1.0, successRate));

            // 如果有天人五衰特质，概率减半
            if (a.hasTrait("TaiyiLg9"))
            {
                successRate *= 0.5;
            }

            // 如果有天命印记特质，必定成功
            if (a.hasTrait("TianDaoyinji"))
            {
                successRate = 1.0;
            }
            if (UnityEngine.Random.Range(0.0f, 1.0f) > successRate)
            {
                // 晋升失败，添加太乙印记
                if (!a.hasTrait("TaiYiyinji"))
                {
                    a.addTrait("TaiYiyinji");
                    // 更新后缀为"太乙"
                    UpdateNameSuffix(a, "TaiYiyinji");
                }
                return false;
            }
            
            // 晋升道祖
            upTrait(
                "XianTu9",
                "XianTu91",
                a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "XianTu09" },
                new string[] { "strong_minded", "immune" }
            );
            
            UpdateNameSuffix(a, "XianTu91");
            ApplyXianTuTitle(a, "XianTu91");
            
            // 根据配置自动收藏道祖角色
            if (XianTuConfig.ShouldAutoCollectForRealm("XianTu91", XianTuConfig.AutoCollectDaoZu))
            {
                a.data.favorite = true;
            }

            // 突破成功后消除指定负面特质
            string[] negativeTraitsToRemove = { "stupid", "short sighted", "soft skin", "fragile trait", "ugly", "skin burns", "one eyed", "madness", "TaiyiLg9" };
            foreach (string trait in negativeTraitsToRemove)
            {
                if (a.hasTrait(trait))
                {
                    a.removeTrait(trait);
                }
            }

            // 突破成功后添加指定正面特质
            string[] positiveTraitsToAdd = { "genius", "eagle eyed", "strong minded", "lucky", "immune", "sunblessed", "attractive", "acid proof", "fire proof", "freeze proof", "poison immunity", "immortal" };
            foreach (string trait in positiveTraitsToAdd)
            {
                if (!a.hasTrait(trait))
                {
                    a.addTrait(trait);
                }
            }
            
            // 添加道祖印记
            if (!a.hasTrait("DaoZuyinji"))
            {
                a.addTrait("DaoZuyinji");
            }

            return true;
        }
        
        public static bool XianTu9_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor actor = pTarget.a;
            if (actor.data.health < actor.getMaxHealth())
            {
                // 获取当前序列值
                float xianTuValue = actor.GetXianTu();
                // 计算回血量：序列值的90%（可根据平衡性调整比例）
                int healAmount = Mathf.RoundToInt(xianTuValue * 0.0001f);
                // 至少回复1点生命
                if (healAmount < 1) healAmount = 1; 
        
                actor.restoreHealth(healAmount);
                actor.spawnParticle(Toolbox.color_heal);
            }
            return true;
        }
        
        // 独立的世界强者晋升方法，可以被单独调用
        public static void TriggerWorldStrongPromotion(Actor actor)
        {
            // 检查角色是否有效
            if (actor == null || !actor.isAlive())
                return;
            
            // 只有拥有XianTu9或XianTu91特质的角色才能触发世界强者晋升
            if (actor.hasTrait("XianTu9") || actor.hasTrait("XianTu91"))
            {
                // 调用世界强者晋升机制
                patch.UpdateWorldStrongPopulation(actor);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="old_trait">升级前的特质</param>
        /// <param name="new_trait">升级到的特质</param>
        /// <param name="actor">单位传入</param>
        /// <param name="other_Oldtraits">升级要删掉的特质(不包括升级前的主特质)</param>
        /// <param name="other_newTrait">升级后要伴随添加的特质(不包含主特质)</param>
        /// <returns></returns>
        public static bool upTrait(
            string old_trait,
            string new_trait,
            Actor actor,
            string[] other_Oldtraits = null,
            string[] other_newTrait = null
        )
        {
            if (actor == null)
            {
                return false;
            }

            if (other_newTrait != null)
            {
                foreach (string trait in other_newTrait)
                {
                    actor.addTrait(trait);
                }
            }

            if (other_Oldtraits != null)
            {
                foreach (var trait in other_Oldtraits)
                {
                    actor.removeTrait(trait);
                }
            }

            actor.addTrait(new_trait);
            actor.removeTrait(old_trait);

            return true;
        }

        // 武道资质系列特质 - 每年增加气血
        public static bool TyGengu1_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            
            Actor actor = pTarget.a;
            if (actor.hasTrait("TyGengu1"))
            {
                // 凡人之资：每年增加1点气血
                actor.ChangeQiXue(1.0f);
            }
            return true;
        }
        
        public static bool TyGengu2_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            
            Actor actor = pTarget.a;
            if (actor.hasTrait("TyGengu2"))
            {
                // 小有天资：每年增加2点气血
                actor.ChangeQiXue(2.0f);
            }
            return true;
        }
        
        public static bool TyGengu3_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            
            Actor actor = pTarget.a;
            if (actor.hasTrait("TyGengu3"))
            {
                // 武道奇才：每年增加4点气血
                actor.ChangeQiXue(4.0f);
            }
            return true;
        }
        
        public static bool TyGengu4_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            
            Actor actor = pTarget.a;
            if (actor.hasTrait("TyGengu4"))
            {
                // 天生武骨：每年增加8点气血
                actor.ChangeQiXue(8.0f);
            }
            return true;
        }

        // 修为丹药系列 - 凝气丹效果：增加5点真元
        public static bool XiulianDanyao1_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            
            Actor actor = pTarget.a;
            if (actor.hasTrait("XiulianDanyao1"))
            {
                // 增加5点真元
                actor.ChangeXianTu(1f);
                // 移除丹药特质
                actor.removeTrait("XiulianDanyao1");
            }
            return true;
        }

        // 修为丹药系列 - 青元丹效果：增加50点真元
        public static bool XiulianDanyao2_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            
            Actor actor = pTarget.a;
            if (actor.hasTrait("XiulianDanyao2"))
            {
                // 增加50点真元
                actor.ChangeXianTu(9f);
                // 移除丹药特质
                actor.removeTrait("XiulianDanyao2");
            }
            return true;
        }

        // 修为丹药系列 - 地煞丹效果：增加200点真元
        public static bool XiulianDanyao3_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            
            Actor actor = pTarget.a;
            if (actor.hasTrait("XiulianDanyao3"))
            {
                // 增加200点真元
                actor.ChangeXianTu(90f);
                // 移除丹药特质
                actor.removeTrait("XiulianDanyao3");
            }
            return true;
        }

        // 修为丹药系列 - 天罡丹效果：增加250点真元
        public static bool XiulianDanyao4_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            
            Actor actor = pTarget.a;
            if (actor.hasTrait("XiulianDanyao4"))
            {
                // 增加250点真元
                actor.ChangeXianTu(900f);
                // 移除丹药特质
                actor.removeTrait("XiulianDanyao4");
            }
            return true;
        }

        // 修为丹药系列 - 日月丹效果：增加2000点真元
        public static bool XiulianDanyao5_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            
            Actor actor = pTarget.a;
            if (actor.hasTrait("XiulianDanyao5"))
            {
                // 增加2000点真元
                actor.ChangeXianTu(2000f);
                // 移除丹药特质
                actor.removeTrait("XiulianDanyao5");
            }
            return true;
        }

        // 修为丹药系列 - 法则丹效果：增加2500点真元
        public static bool XiulianDanyao6_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            
            Actor actor = pTarget.a;
            if (actor.hasTrait("XiulianDanyao6"))
            {
                // 增加2500点真元
                actor.ChangeXianTu(3000f);
                // 移除丹药特质
                actor.removeTrait("XiulianDanyao6");
            }
            return true;
        }

        // 修为丹药系列 - 窃天丹效果：增加4500点真元
        public static bool XiulianDanyao7_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            
            Actor actor = pTarget.a;
            if (actor.hasTrait("XiulianDanyao7"))
            {
                // 增加4500点真元
                actor.ChangeXianTu(5000f);
                // 移除丹药特质
                actor.removeTrait("XiulianDanyao7");
            }
            return true;
        }

        // 修为丹药系列 - 界天丹效果：增加10000点真元
        public static bool XiulianDanyao8_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            
            Actor actor = pTarget.a;
            if (actor.hasTrait("XiulianDanyao8"))
            {
                // 增加10000点真元
                actor.ChangeXianTu(10000f);
                // 移除丹药特质
                actor.removeTrait("XiulianDanyao8");
            }
            return true;
        }

        // 修为丹药系列 - 千界丹效果：增加30000点真元
        public static bool XiulianDanyao9_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            
            Actor actor = pTarget.a;
            if (actor.hasTrait("XiulianDanyao9"))
            {
                // 增加30000点真元
                actor.ChangeXianTu(30000f);
                // 移除丹药特质
                actor.removeTrait("XiulianDanyao9");
            }
            return true;
        }

        // 修为丹药系列 - 玉魄丹效果：增加1000点悟性
        public static bool YupoDanyao8_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            
            Actor actor = pTarget.a;
            if (actor.hasTrait("YupoDanyao8"))
            {
                // 增加1000点悟性
                actor.ChangeWuXing(10000f);
                // 移除丹药特质
                actor.removeTrait("YupoDanyao8");
            }
            return true;
        }

        // 道基体系特质 - 每年增加真元（整合版）
        public static bool DaoJi_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            
            Actor actor = pTarget.a;
            float addXianTu = 0f;
            
            // 检查是否有道基特质并添加相应的真元
            if (actor.hasTrait("DaoJi1")) addXianTu += 1f;    // 人道筑基：修炼增幅+3
            else if (actor.hasTrait("DaoJi2")) addXianTu += 2f;    // 地道筑基：修炼增幅+4
            else if (actor.hasTrait("DaoJi3")) addXianTu += 3f;    // 天道筑基：修炼增幅+5
            else if (actor.hasTrait("DaoJi4")) addXianTu += 5f;    // 无上道基：修炼增幅+6
            else if (actor.hasTrait("DaoJi5")) addXianTu += 10f;    // 下品金丹：修炼增幅+7
            else if (actor.hasTrait("DaoJi6")) addXianTu += 20f;    // 中品金丹：修炼增幅+8
            else if (actor.hasTrait("DaoJi7")) addXianTu += 40f;    // 上品金丹：修炼增幅+9
            else if (actor.hasTrait("DaoJi8")) addXianTu += 70f;   // 大道金丹：修炼增幅+10
            else if (actor.hasTrait("DaoJi9")) addXianTu += 110f;   // 三寸元婴：修炼增幅+12
            else if (actor.hasTrait("DaoJi91")) addXianTu += 130f;  // 六寸元婴：修炼增幅+14
            else if (actor.hasTrait("DaoJi92")) addXianTu += 160f;  // 九寸元婴：修炼增幅+16
            else if (actor.hasTrait("DaoJi93")) addXianTu += 200f;  // 不灭元婴：修炼增幅+18
            else if (actor.hasTrait("DaoJi94")) addXianTu += 210f;  // 三丈元神：修炼增幅+20
            else if (actor.hasTrait("DaoJi95")) addXianTu += 230f;  // 六丈元神：修炼增幅+22
            else if (actor.hasTrait("DaoJi96")) addXianTu += 260f;  // 九丈元神：修炼增幅+24
            else if (actor.hasTrait("DaoJi97")) addXianTu += 300f;  // 天地元神：修炼增幅+26
            else if (actor.hasTrait("FaXiang1")) addXianTu += 410f; // 百丈法身：修炼增幅+30
            else if (actor.hasTrait("FaXiang2")) addXianTu += 430f; // 千丈法身：修炼增幅+35
            else if (actor.hasTrait("FaXiang3")) addXianTu += 460f; // 万丈法身：修炼增幅+40
            else if (actor.hasTrait("FaXiang4")) addXianTu += 500f; // 盘古法身：修炼增幅+45
            else if (actor.hasTrait("FaXiang5")) addXianTu += 610f; // 混沌法相：修炼增幅+50
            else if (actor.hasTrait("FaXiang6")) addXianTu += 630f; // 太初法相：修炼增幅+55
            else if (actor.hasTrait("FaXiang7")) addXianTu += 660f; // 鸿蒙法相：修炼增幅+60
            else if (actor.hasTrait("FaXiang8")) addXianTu += 700f; // 大道法相：修炼增幅+65
            else if (actor.hasTrait("FaXiang9")) addXianTu += 1000f; // 小世界：修炼增幅+70
            else if (actor.hasTrait("FaXiang91")) addXianTu += 2000f; // 小千世界：修炼增幅+75
            else if (actor.hasTrait("FaXiang92")) addXianTu += 3000f; // 大千世界：修炼增幅+80
            else if (actor.hasTrait("FaXiang93")) addXianTu += 4000f; // 混沌世界：修炼增幅+85
            
            // 如果有真元需要增加，则执行增加操作
            if (addXianTu > 0f)
            {
                actor.ChangeXianTu(addXianTu);
            }
            
            return true;
        }

        // 法器购买机制 - 武器价格定义

    }
}