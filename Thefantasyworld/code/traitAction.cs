using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ai;
using UnityEngine;
using AttributeExpansion.code.utils;
using ReflectionUtility;
using System.IO;
using HarmonyLib;
using System.Reflection;

namespace PeerlessThedayofGodswrath.code
{
    internal class traitAction
    {
        // ======== 后缀系统 ========
        private static readonly Dictionary<string, string> _xianSuffixMap = new Dictionary<string, string>
        {       
            // 法师特质
            {"enchanter1", "学徒"},
            {"enchanter2", "法师"},
            {"enchanter3", "资深法师"},
            {"enchanter4", "大法师"},
            {"enchanter5", "传奇法师"},
            {"enchanter6", "织法者"},
            {"enchanter7", "真理之主"},
            
            // 牧师特质
            {"pastor1", "信徒"},
            {"pastor2", "牧师"},
            {"pastor3", "圣职者"},
            {"pastor4", "神圣祭司"},
            {"pastor5", "传奇牧师"},
            {"pastor6", "神之化身"},
            {"pastor7", "光辉之主"},
            
            // 骑士特质
            {"knight1", "侍从"},
            {"knight2", "骑士"},
            {"knight3", "圣骑士"},
            {"knight4", "圣殿骑士"},
            {"knight5", "传奇骑士"},
            {"knight6", "天启骑士"},
            {"knight7", "审判之神"},
            
            // 战士特质
            {"valiantgeneral1", "初级战士"},
            {"valiantgeneral2", "高级战士"},
            {"valiantgeneral3", "勇士"},
            {"valiantgeneral4", "狂战士"},
            {"valiantgeneral5", "传奇战士"},
            {"valiantgeneral6", "征服者"},
            {"valiantgeneral7", "战争之主"},
            
            // 游侠特质
            {"Ranger1", "聆风者"},
            {"Ranger2", "游侠"},
            {"Ranger3", "巡林客"},
            {"Ranger4", "荒野之魂"},
            {"Ranger5", "传奇游侠"},
            {"Ranger6", "自然之怒"},
            {"Ranger7", "狩猎之神"},
            
            // 刺客特质
            {"Assassin1", "杀手"},
            {"Assassin2", "刺客"},
            {"Assassin3", "影刺客"},
            {"Assassin4", "暗夜刺客"},
            {"Assassin5", "传奇刺客"},
            {"Assassin6", "终焉使者"},
            {"Assassin7", "终末之环"},
            
            // 召唤师特质
            {"Summoner1", "灵语者"},
            {"Summoner2", "召唤师"},
            {"Summoner3", "契印者"},
            {"Summoner4", "唤魔师"},
            {"Summoner5", "传奇召唤师"},
            {"Summoner6", "唤星者"},
            {"Summoner7", "万灵之主"},
            
            // 吟游诗人特质
            {"minstrel1", "流浪诗人"},
            {"minstrel2", "吟游诗人"},
            {"minstrel3", "妙喉歌者"},
            {"minstrel4", "传颂者"},
            {"minstrel5", "传奇吟游诗人"},
            {"minstrel6", "咏唱者"},
            {"minstrel7", "世末歌者"},
            
            // 咒术师特质
            {"warlock1", "蚀魂学徒"},
            {"warlock2", "咒术师"},
            {"warlock3", "恶咒师"},
            {"warlock4", "痛苦编织者"},
            {"warlock5", "传奇咒术师"},
            {"warlock6", "天灾"},
            {"warlock7", "永厄之主"},
            
            // 炼金术士特质
            {"alchemist1", "燃素学者"},
            {"alchemist2", "炼金术士"},
            {"alchemist3", "炼金师"},
            {"alchemist4", "创生者"},
            {"alchemist5", "传奇炼金术士"},
            {"alchemist6", "贤者之石"},
            {"alchemist7", "等价交换"},
            
            // 野蛮人特质
            {"barbarian1", "蛮战士"},
            {"barbarian2", "野蛮人"},
            {"barbarian3", "碎颅者"},
            {"barbarian4", "荒野游荡者"},
            {"barbarian5", "传奇野蛮人"},
            {"barbarian6", "纷争之手"},
            {"barbarian7", "纷争之主"},
        };

        private static void UpdateXianSuffix(Actor actor, string newTrait)
        {
            if (_xianSuffixMap.TryGetValue(newTrait, out string suffix))
            {
                string currentName = actor.getName();

                // 1. 分离基础名称（包括尊号）和旧境界后缀
                int lastDashIndex = currentName.LastIndexOf('-');
                string basePart = lastDashIndex >= 0
                    ? currentName.Substring(0, lastDashIndex).Trim()
                    : currentName;

                // 3. 设置新名称：基础名称 + 新境界后缀
                actor.setName($"{basePart}-{suffix}");
            }
        }

        private static void ApplyXianTitle(Actor actor, string newTrait)
        {
            if (_xianTitleMap.TryGetValue(newTrait, out string[] titles))
            {
                string currentName = actor.getName();

                // 1. 分离境界后缀（最后一个连字符后的内容）
                int lastDashIndex = currentName.LastIndexOf('-');
                string suffixPart = lastDashIndex > 0
                    ? currentName.Substring(lastDashIndex) // 包含连字符和后缀
                    : "";

                string basePart = lastDashIndex > 0
                    ? currentName.Substring(0, lastDashIndex).Trim()
                    : currentName;

                // 2. 移除旧尊号（如果有）
                int firstDashIndex = basePart.IndexOf('-');
                if (firstDashIndex > 0)
                {
                    basePart = basePart.Substring(firstDashIndex + 1).Trim();
                }

                // 4. 随机选择新尊号
                string title = titles[UnityEngine.Random.Range(0, titles.Length)];

                // 5. 设置新名称：新尊号 + 基础名称 + 境界后缀
                actor.setName($"{title}-{basePart}{suffixPart}");
            }
        }

        // ======== 尊号系统 ========
        private static readonly Dictionary<string, string[]> _xianTitleMap = new Dictionary<string, string[]>
        {
            // 法师职业尊号
            {"enchanter5", new string[] {
            "星界传奇法皇", "元素掌控", "时空观察者", "天灾化身", "绚烂光谱",
            "电磁皇帝", "风暴主宰", "元素支配者", "秘典行者", "秘仪执钥",
            "奥秘之主", "咒法王", "三岔路执钥", "渊潮执卷", "星盘执卷",
            "砂卷执钥", "银镜执钥", "霜符执卷", "古树文执卷"
            }},
            {"enchanter6", new string[] {
            "元素半神仲裁者", "定律框架", "魔法选民", "星界宗主", "星辰冕主",
            "星界行者", "法则编织者", "月书御史", "海雾冕主", "天球冕主",
            "日盘奥书官", "蚀月奥典君", "月桂秘仪冕主", "九界符籍冕主", "石环秘仪冕主"
            }},
            {"enchanter7", new string[] {
            "永恒·秩序维护者", "命运星轮", "真理谐律", "寰宇奥秘", "以太星主",
            "万变之主", "万法之神·奥术主宰", "苍穹之主", "万法之神", "法域神宰",
            "真理之神", "万法之源", "万法全书", "魔网之主", "三重密言之神",
            "万法原典", "全知全能"
            }}, 
            
            // 牧师职业尊号
            {"pastor5", new string[] {
            "圣域传奇牧首", "恸哭者", "启蒙者", "天堂之拳", "圣徽执灯",
            "医杖守灯", "潮灯司祭", "时辰守灯", "泉盏守祷", "朔灯守祷",
            "橄榄灯司祭", "灰树守祷", "橡环守祷"
            }},
            {"pastor6", new string[] {
            "神谕半神代行者", "敲钟天使", "天堂使者", "灵应之神", "神之左手",
            "藏杖人", "终明", "光辉教宗", "圣域冠冕", "羽衡冠冕",
            "盐冠守祷", "日晷冠冕", "金羽衡司", "幽辉冠冕", "双蛇冠祷",
            "枝环冠司", "槲枝冠司"
            }},
            {"pastor7", new string[] {
            "信仰之神·圣辉主宰", "天主", "高举神座", "救赎之主", "无上辉光",
            "天启之神", "日盘启明之神", "圣徽至冕"
            }},
            
            // 骑士职业尊号
            {"knight5", new string[] {
            "圣骑传奇守护者", "千军破碎者", "不败之王", "山神", "铁血之神",
            "金身尊者", "堂吉诃德", "遨空骑士", "猋腾波流", "秩序之柱",
            "美德之剑", "苍蓝残响", "靛蓝老者", "漆黑缄默", "堇紫泪滴",
            "殷红迷雾", "绀碧少女", "银红视线", "猎龙者", "完美之人",
            "苍白骑士", "苍银誓骑", "秘印誓骑", "圆桌誓骑", "黑帆誓骑",
            "铜齿誓骑", "狮纹誓骑", "夜纱誓骑", "青铜誓骑", "盾墙誓骑",
            "结纹誓骑"
            }},
            {"knight6", new string[] {
            "圣光半神守护者", "战火之神", "光明骑士", "叹息之墙", "长城",
            "神之右手", "王国之柱", "凿穿军阵", "奔星震霆", "上帝之鞭",
            "罪恶霸业", "君临天下", "背负天穹者", "圣殿摄政", "光域冕卫",
            "圣杯冕卫", "珊瑚冕卫", "擒纵冕卫", "方尖冕卫", "幽月冕卫",
            "方阵冕卫", "冰海冕卫", "石阵冕卫"
            }},
            {"knight7", new string[] {
            "秩序之神·圣剑主宰", "摧折王庭", "光明之盾", "荣耀之神", "秩序之神",
            "毁灭骄阳", "起源之墙", "万灵天主", "审判之神", "圣裁之神",
            "天契与圣裁之神", "誓约冠印"
            }},
            
            // 战士职业尊号
            {"valiantgeneral5", new string[] {
            "战域传奇狂战士", "摇山撼海", "千军首", "征服者", "玄钢破军",
            "狮皮破军", "鲸骨破军", "铸时破军", "赤沙破军", "黯钢破军",
            "斧盾破军", "蓝纹破军"
            }},
            {"valiantgeneral6", new string[] {
            "勇武半神征服者", "残骸裹身", "永恒战士", "战争主宰", "征服之王",
            "纷争化身", "战域摄政", "十二劳誉之嗣", "铁舷摄政", "钟域摄政",
            "焚风之嗣", "血月之嗣", "熊狂之嗣", "风丘之嗣"
            }},
            {"valiantgeneral7", new string[] {
            "战争之神·雷霆主宰", "黄铜王座", "破碎穹庐", "战争之主", "神锋裂地",
            "血刃主宰", "天谴之矛", "战境之神", "战矛与火幔之神", "战域元极"
            }},
            
            // 游侠职业尊号
            {"Ranger5", new string[] {
            "自然传奇猎王", "千枫幻灵", "森誓逐影", "银弓逐月", "潮汐逐影",
            "黄道逐影", "蜃影逐踪", "朔野逐影", "霜野逐踪", "霭林逐踪"
            }},
            {"Ranger6", new string[] {
            "荒野半神引路人", "神枭", "荒界宗主", "霜岭猎君", "迷航领踪",
            "子夜领迹", "沙海领迹", "夜籁领迹", "金角领迹", "北境领迹",
            "鹿痕领迹"
            }},
            {"Ranger7", new string[] {
            "狩猎之神·万兽主宰", "永恒垢父", "千风化身", "荒野之神", "荒角之神",
            "荒野终猎"
            }},
            
            // 刺客职业尊号
            {"Assassin5", new string[] {
            "暗影领主·幽影行者", "暗影传奇夜王", "灰黯之手", "静谧", "百貌",
            "无面夜行", "静影誓刃", "鸦相·第三影", "海雾无面", "无刻之刃",
            "蝎印潜刃", "弦月潜刃", "冥渡潜影", "渡鸦潜影", "薄雾潜影"
            }},
            {"Assassin6", new string[] {
            "暗影亲王·隐秘之侯", "幽影半神猎杀者", "伏影君王", "送葬人", "告死天使",
            "影域君主", "影域冕主", "冥路引行者", "沉沙潜刃", "秒影潜行",
            "夜砂行者", "无声夜行", "石瞳夜行", "极夜潜行", "黑犬夜行"
            }},
            {"Assassin7", new string[] {
            "暗影帝王·无光主宰", "永夜主宰·恐惧源尊", "暗杀之神·永夜主宰", "万孽之源", "死神",
            "猩红之主", "暗影君王", "暗影之神", "无相之神", "影冥之神",
            "暗影无相", "虚无之主", "终末之影", "死亡主宰", "暗影本源"
            }},
            
            // 召唤师职业尊号
            {"Summoner5", new string[] {
            "灵界传奇契主", "守门人", "阴阳使臣", "星界天使", "契印行者",
            "契纹执铃", "封玺行者", "螺号契者", "星矩契者", "象形契者",
            "银环契者", "石印契者", "符印契者", "仙丘契者"
            }},
            {"Summoner6", new string[] {
            "幻兽半神统御者", "地狱之门", "泰山府君", "万契摄政", "灵廷冕宰",
            "高天字母呼名者", "海廷冕宰", "宿曜冕宰", "陵印冕宰", "影廷冕宰",
            "谶辞呼名者", "冰雾呼名者", "薄暮呼名者"
            }},
            {"Summoner7", new string[] {
            "召唤之神·万灵主宰", "门扉", "牧猎神", "郡神", "万灵之神",
            "万契神座", "万契之神", "万灵契印"
            }},
            
            // 吟游诗人职业尊号
            {"minstrel5", new string[] {
            "传奇韵域歌王", "倒吊人", "悲悯尊者", "狂想协奏曲", "地狱颂者",
            "暗巷之影", "塞西莉亚之花", "四风的叙述者", "追风的异乡人", "故事讲述者",
            "星辉歌者", "冥原七弦", "珊瑚七弦", "黄铜夜莺", "沙笛歌者",
            "月弦歌者", "冥歌七弦", "角号歌者", "绿金竖琴"
            }},
            {"minstrel6", new string[] {
            "神话韵律歌者", "世末歌者", "旋律之神", "世末旅人", "咏叹之神",
            "神喉", "高歌魔王", "秘言君主", "九艺冠冕者", "潮吟冕者",
            "发条歌者", "金盘颂官", "静谣冕者", "月桂颂冕", "诗蜜冕者",
            "三螺冕咏"
            }},
            {"minstrel7", new string[] {
            "艺术之神·旋律主宰", "初音未来", "真实歌颂者", "哲人王", "音律主",
            "黄金黎明", "铸日者", "秘言之神", "光弦之神", "七弦太音"
            }},
            
            // 咒术师职业尊号
            {"warlock5", new string[] {
            "禁域传奇咒主", "言灾老人", "始作俑者", "嫉妒之神", "卑劣恶咒",
            "黎咒执印", "暗纶执线", "渊语执咒", "黑曜刻符", "朱砂执咒",
            "蚀印执咒", "纺命执线", "冬纺执线", "三结执咒"
            }},
            {"warlock6", new string[] {
            "暗黑咒法宗师", "厄运帝君", "恶意神主", "厄典宗主", "三相月誓者",
            "潮兆宗主", "厄宿宗主", "陵咒宗主", "朔望宗主", "冥河誓约师",
            "命纱誓者"
            }},
            {"warlock7", new string[] {
            "诅咒之神·灾厄主宰", "命运", "厄咒之神", "外神之面", "厄咒真名"
            }},
            
            // 炼金术士职业尊号
            {"alchemist5", new string[] {
            "传奇炼域匠神", "权杖隐者", "秘金贤者", "黑日执坩", "盐汞执坩",
            "汞银执坩", "砂金执坩", "月汞执坩", "青铜执坩", "炉心执坩",
            "银釜执坩"
            }},
            {"alchemist6", new string[] {
            "造物半神转化者", "悖论结晶之契", "贤石宗主", "绿狮冕持", "贝冠宗主",
            "机匣冕持", "玄金贤者", "银辉贤者", "金石冕持", "符铁贤者",
            "草冠贤者"
            }},
            {"alchemist7", new string[] {
            "物质之神·元素主宰", "非欧立方", "原质之神", "贤石之神", "哲石之神",
            "贤石至型"
            }},
            
            // 野蛮人职业尊号
            {"barbarian5", new string[] {
            "狂域传奇蛮王", "暴怒城主", "独裁者", "无尽战壕", "先祖之拳",
            "不灭狂魂", "风暴行者", "撼地者", "纷争王", "不朽丰碑",
            "至高咆哮", "巨人屠夫", "屠龙王者", "狂野之灵", "征服王",
            "部落共主", "深渊屠戮者", "北地之怒", "北境之心", "兽灵行者",
            "蛮族之王", "荒血怒魂", "熊狂怒魂", "海狼怒魂", "钢心怒魂",
            "沙暴怒魂", "夜狼怒魂", "常春藤怒魂", "霜原怒魂", "鹿骨怒魂"
            }},
            {"barbarian6", new string[] {
            "暴怒半神毁灭者", "践踏之神", "群山霸主", "荒野之王", "蛮荒王者",
            "灰烬领主", "不灭狂雷", "山隐之焰", "狂域宗主", "霜巨之嗣",
            "风暴之嗣", "铁脉之嗣", "热砂之嗣", "月痕之嗣", "藤冠狂行",
            "狼披之嗣", "荒丘之嗣"
            }},
            {"barbarian7", new string[] {
            "力之神·混沌主宰", "风暴之主", "狂野之神", "蛮荒主宰", "灰烬君主无序暴君",
            "原怒之神", "雷锤之神", "原怒荒极"
            }}
        };

        private static string GetNewRandomTrait(string[] additionalTraits)
        {
            // 逻辑从 additionalTraits 中随机选择一个特质
            int randomIndex = UnityEngine.Random.Range(0, additionalTraits.Length);
            return additionalTraits[randomIndex];
        }
        /// </summary>
        /// <param name="old_trait">升级前的特质</param>
        /// <param name="new_trait">升级到的特质</param>
        /// <param name="actor">单位传入</param>
        /// <param name="other_Oldtraits">升级要删掉的特质(不包括升级前的主特质)</param>
        /// <param name="other_newTrait">升级后要伴随添加的特质(不包含主特质)</param>
        /// <returns></returns>
        public static bool upTrait(string old_trait, string new_trait, Actor actor, string[] other_Oldtraits = null,
                                   string[] other_newTrait = null)
        {
            if (actor == null)
            {
                return false;
            }

            foreach (string VARIABLE in other_newTrait)
            {
                actor.addTrait(VARIABLE);
            }

            foreach (var VARIABLE in other_Oldtraits)
            {
                actor.removeTrait(VARIABLE);
            }

            actor.addTrait(new_trait);
            actor.removeTrait(old_trait);
            return true;
        }
        public static bool grade1_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
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
            if (a.Getcareerexperience() <= 10)
            {
                return false;
            }

            string[] forbiddenTraits =
            {
                "OrderofBeing1", "OrderofBeing2", "OrderofBeing3", "OrderofBeing4"
            };
            foreach (string trait in forbiddenTraits)
            {
                if (pTarget.a.hasTrait(trait))
                {
                    return false;
                }
            }

            // 基础默认特质 - 无论什么职业都有的特质
            string[] defaultTraits = new string[] { "fire_proof", "freeze_proof", "immune", "dash", "block", "dodge", "backstep", "deflect_projectile" };
            // 基于OrderofBeing1特质分配不同的额外特质
            string[] additionalTraits = new string[] { }; // 初始为空数组
            string[] traitsToRemove = new string[] { "tumorInfection", "cursed", "infected", "mushSpores",
            "plague", "madness" };

            // 随机选择一个职业特质
            int randomClass = UnityEngine.Random.Range(1, 12); // 随机生成1-11的数字，对应11种职业

            // 根据随机选择的职业分配相应的特质
            switch (randomClass)
            {
                case 1: // 法师职业
                    additionalTraits = defaultTraits.Concat(new string[] { "enchanter1" }).ToArray();
                    UpdateXianSuffix(a, "enchanter1");
                    break;
                case 2: // 牧师职业
                    additionalTraits = defaultTraits.Concat(new string[] { "pastor1" }).ToArray();
                    UpdateXianSuffix(a, "pastor1");
                    break;
                case 3: // 骑士职业
                    additionalTraits = defaultTraits.Concat(new string[] { "knight1" }).ToArray();
                    UpdateXianSuffix(a, "knight1");
                    break;
                case 4: // 战士职业
                    additionalTraits = defaultTraits.Concat(new string[] { "valiantgeneral1" }).ToArray();
                    UpdateXianSuffix(a, "valiantgeneral1");
                    break;
                case 5: // 射手职业
                    additionalTraits = defaultTraits.Concat(new string[] { "Ranger1" }).ToArray();
                    UpdateXianSuffix(a, "Ranger1");
                    break;
                case 6: // 刺客职业
                    additionalTraits = defaultTraits.Concat(new string[] { "Assassin1" }).ToArray();
                    UpdateXianSuffix(a, "Assassin1");
                    break;
                case 7: // 召唤师职业
                    additionalTraits = defaultTraits.Concat(new string[] { "Summoner1" }).ToArray();
                    UpdateXianSuffix(a, "Summoner1");
                    break;
                case 8: // 吟游诗人职业
                    additionalTraits = defaultTraits.Concat(new string[] { "minstrel1" }).ToArray();
                    UpdateXianSuffix(a, "minstrel1");
                    break;
                case 9: // 咒术师职业
                    additionalTraits = defaultTraits.Concat(new string[] { "warlock1" }).ToArray();
                    UpdateXianSuffix(a, "warlock1");
                    break;
                case 10: // 炼金术士职业
                    additionalTraits = defaultTraits.Concat(new string[] { "alchemist1" }).ToArray();
                    UpdateXianSuffix(a, "alchemist1");
                    break;
                case 11: // 野蛮人职业
                    additionalTraits = defaultTraits.Concat(new string[] { "barbarian1" }).ToArray();
                    UpdateXianSuffix(a, "barbarian1");
                    break;
            }

            upTrait("", "OrderofBeing1", a, traitsToRemove, additionalTraits);
            return true;
        }

        public static bool grade2_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
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
            if (a.Getcareerexperience() <= 40)
            {
                return false;
            }

            // 基础默认特质 - 无论什么职业都有的特质
            string[] defaultTraits = new string[] { "fire_proof", "freeze_proof", "battle_reflexes" };
            // 基于OrderofBeing1特质分配不同的额外特质
            string[] additionalTraits = new string[] { }; // 初始为空数组
            string[] traitsToRemove = new string[] { "tumorInfection", "cursed", "infected", "mushSpores",
            "plague", "madness", "enchanter1", "pastor1", "knight1", "valiantgeneral1", "Ranger1",
            "Assassin1", "Summoner1", "minstrel1", "warlock1", "alchemist1", "barbarian1" };

            // 根据不同的特质分配不同的特质（按数字顺序排列）
            if (a.hasTrait("enchanter1"))
            {
                // 法师职业 - 增加魔法相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "enchanter2", "arcane_reflexes" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "enchanter2");
            }
            else if (a.hasTrait("pastor1"))
            {
                // 牧师职业 - 增加防御和回复相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "pastor2" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "pastor2");
            }
            else if (a.hasTrait("knight1"))
            {
                // 骑士职业 - 增加防御和战斗相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "knight2" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "knight2");
            }
            else if (a.hasTrait("valiantgeneral1"))
            {
                // 战士职业 - 增加攻击和暴击相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "valiantgeneral2" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "valiantgeneral2");
            }
            else if (a.hasTrait("Ranger1"))
            {
                // 射手职业 - 增加远程攻击相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Ranger2" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Ranger2");
            }
            else if (a.hasTrait("Assassin1"))
            {
                // 刺客职业 - 增加敏捷相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Assassin2" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Assassin2");
            }
            else if (a.hasTrait("Summoner1"))
            {
                // 召唤师职业 - 增加召唤相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Summoner2" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Summoner2");
            }
            else if (a.hasTrait("minstrel1"))
            {
                // 吟游诗人职业 - 增加音乐相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "minstrel2" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "minstrel2");
            }
            else if (a.hasTrait("warlock1"))
            {
                // 咒术师职业 - 增加诅咒相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "warlock2" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "warlock2");
            }
            else if (a.hasTrait("alchemist1"))
            {
                // 炼金术士职业 - 增加炼金相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "alchemist2" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "alchemist2");
            }
            else if (a.hasTrait("barbarian1"))
            {
                // 野蛮人职业 - 增加力量相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "barbarian2" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "barbarian2");
            }
            return true;
        }

        public static bool grade3_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
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
            if (a.Getcareerexperience() <= 160)
            {
                return false;
            }

            // 基础默认特质 - 无论什么职业都有的特质
            string[] defaultTraits = new string[] { "fire_proof", "freeze_proof" };
            // 基于OrderofBeing1特质分配不同的额外特质
            string[] additionalTraits = new string[] { }; // 初始为空数组
            string[] traitsToRemove = new string[] { "tumorInfection", "cursed", "infected", "mushSpores",
            "plague", "madness", "enchanter2", "pastor2", "knight2", "valiantgeneral2", "Ranger2",
            "Assassin2", "Summoner2", "minstrel2", "warlock2" , "alchemist2", "barbarian2"};

            // 根据不同的特质分配不同的特质（按数字顺序排列）
            if (a.hasTrait("enchanter2"))
            {
                // 法师职业 - 增加魔法相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "enchanter3" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "enchanter3");
            }
            else if (a.hasTrait("pastor2"))
            {
                // 牧师职业 - 增加防御和回复相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "pastor3" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "pastor3");
            }
            else if (a.hasTrait("knight2"))
            {
                // 骑士职业 - 增加防御和战斗相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "knight3" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "knight3");
            }
            else if (a.hasTrait("valiantgeneral2"))
            {
                // 战士职业 - 增加攻击和暴击相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "valiantgeneral3" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "valiantgeneral3");
            }
            else if (a.hasTrait("Ranger2"))
            {
                // 射手职业 - 增加远程攻击相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Ranger3" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Ranger3");
            }
            else if (a.hasTrait("Assassin2"))
            {
                // 刺客职业 - 增加敏捷相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Assassin3" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Assassin3");
            }
            else if (a.hasTrait("Summoner2"))
            {
                // 召唤师职业 - 增加召唤相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Summoner3" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Summoner3");
            }
            else if (a.hasTrait("minstrel2"))
            {
                // 吟游诗人职业 - 增加音乐相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "minstrel3" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "minstrel3");
            }
            else if (a.hasTrait("warlock2"))
            {
                // 咒术师职业 - 增加诅咒相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "warlock3" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "warlock3");
            }
            else if (a.hasTrait("alchemist2"))
            {
                // 炼金术士职业 - 增加炼金相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "alchemist3" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "alchemist3");
            }
            else if (a.hasTrait("barbarian2"))
            {
                // 野蛮人职业 - 增加力量相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "barbarian3" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "barbarian3");
            }

            return true;
        }

        public static bool grade4_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
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
            if (a.Getcareerexperience() <= 640)
            {
                return false;
            }

            // 基础默认特质 - 无论什么职业都有的特质
            string[] defaultTraits = new string[] { "fire_proof", "freeze_proof" };
            // 基于OrderofBeing1特质分配不同的额外特质
            string[] additionalTraits = new string[] { }; // 初始为空数组
            string[] traitsToRemove = new string[] { "tumorInfection", "cursed", "infected", "mushSpores",
            "plague", "madness", "enchanter3", "pastor3", "knight3", "valiantgeneral3", "Ranger3",
            "Assassin3", "Summoner3", "minstrel3", "warlock3" , "alchemist3", "barbarian3", "Summonedcreature1" };

            // 根据不同的特质分配不同的特质（按数字顺序排列）
            if (a.hasTrait("enchanter3"))
            {
                // 法师职业 - 增加魔法相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "enchanter4" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "enchanter4");
            }
            else if (a.hasTrait("pastor3"))
            {
                // 牧师职业 - 增加防御和回复相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "pastor4" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "pastor4");
            }
            else if (a.hasTrait("knight3"))
            {
                // 骑士职业 - 增加防御和战斗相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "knight4" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "knight4");
            }
            else if (a.hasTrait("valiantgeneral3"))
            {
                // 战士职业 - 增加攻击和暴击相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "valiantgeneral4" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "valiantgeneral4");
            }
            else if (a.hasTrait("Ranger3"))
            {
                // 射手职业 - 增加远程攻击相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Ranger4" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Ranger4");
            }
            else if (a.hasTrait("Assassin3"))
            {
                // 刺客职业 - 增加敏捷相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Assassin4" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Assassin4");
            }
            else if (a.hasTrait("Summoner3"))
            {
                // 召唤师职业 - 增加召唤相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Summoner4" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Summoner4");
            }
            else if (a.hasTrait("minstrel3"))
            {
                // 吟游诗人职业 - 增加音乐相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "minstrel4" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "minstrel4");
            }
            else if (a.hasTrait("warlock3"))
            {
                // 咒术师职业 - 增加诅咒相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "warlock4" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "warlock4");
            }
            else if (a.hasTrait("alchemist3"))
            {
                // 炼金术士职业 - 增加炼金相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "alchemist4" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "alchemist4");
            }
            else if (a.hasTrait("barbarian3"))
            {
                // 野蛮人职业 - 增加力量相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "barbarian4" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "barbarian4");
            }
            else if (a.hasTrait("Summonedcreature1"))
            {
                // 召唤兽职业
                additionalTraits = defaultTraits.Concat(new string[] { "Summonedcreature2" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Summonedcreature2");
            }

            return true;
        }

        public static bool grade5_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
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
            if (a.Getcareerexperience() <= 2560)
            {
                return false;
            }

            // 基础默认特质 - 无论什么职业都有的特质
            string[] defaultTraits = new string[] { "fire_proof", "freeze_proof", "OrderofBeing2" };
            // 基于OrderofBeing1特质分配不同的额外特质
            string[] additionalTraits = new string[] { }; // 初始为空数组
            string[] traitsToRemove = new string[] { "tumorInfection", "cursed", "infected", "mushSpores",
            "plague", "madness", "enchanter4", "pastor4", "knight4", "valiantgeneral4", "Ranger4",
            "Assassin4", "Summoner4", "minstrel4", "warlock4" , "alchemist4", "barbarian4", "OrderofBeing1", "Summonedcreature2" };

            // 根据不同的特质分配不同的特质（按数字顺序排列）
            if (a.hasTrait("enchanter4"))
            {
                // 法师职业 - 增加魔法相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "enchanter5" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "enchanter5");
                ApplyXianTitle(a, "enchanter5");
            }
            else if (a.hasTrait("pastor4"))
            {
                // 牧师职业 - 增加防御和回复相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "pastor5" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "pastor5");
                ApplyXianTitle(a, "pastor5");
            }
            else if (a.hasTrait("knight4"))
            {
                // 骑士职业 - 增加防御和战斗相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "knight5" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "knight5");
                ApplyXianTitle(a, "knight5");
            }
            else if (a.hasTrait("valiantgeneral4"))
            {
                // 战士职业 - 增加攻击和暴击相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "valiantgeneral5" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "valiantgeneral5");
                ApplyXianTitle(a, "valiantgeneral5");
            }
            else if (a.hasTrait("Ranger4"))
            {
                // 射手职业 - 增加远程攻击相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Ranger5" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Ranger5");
                ApplyXianTitle(a, "Ranger5");
            }
            else if (a.hasTrait("Assassin4"))
            {
                // 刺客职业 - 增加敏捷相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Assassin5" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Assassin5");
                ApplyXianTitle(a, "Assassin5");
            }
            else if (a.hasTrait("Summoner4"))
            {
                // 召唤师职业 - 增加召唤相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Summoner5" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Summoner5");
                ApplyXianTitle(a, "Summoner5");
            }
            else if (a.hasTrait("minstrel4"))
            {
                // 吟游诗人职业 - 增加音乐相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "minstrel5" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "minstrel5");
                ApplyXianTitle(a, "minstrel5");
            }
            else if (a.hasTrait("warlock4"))
            {
                // 咒术师职业 - 增加诅咒相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "warlock5" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "warlock5");
                ApplyXianTitle(a, "warlock5");
            }
            else if (a.hasTrait("alchemist4"))
            {
                // 炼金术士职业 - 增加炼金相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "alchemist5" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "alchemist5");
                ApplyXianTitle(a, "alchemist5");
            }
            else if (a.hasTrait("barbarian4"))
            {
                // 野蛮人职业 - 增加力量相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "barbarian5" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "barbarian5");
                ApplyXianTitle(a, "barbarian5");
            }
            else if (a.hasTrait("Summonedcreature2"))
            {
                // 召唤兽职业 - 增加召唤相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Summonedcreature3" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Summonedcreature3");
                ApplyXianTitle(a, "Summonedcreature3");
            }

            if (ThefantasyworldConfig.AutoCollectLegend)
            {
                a.data.favorite = true;
            }

            return true;
        }

        private static void BreakthroughDeath(Actor target)
        {
            // 直接调用死亡方法而不是扣血
            target.dieAndDestroy(AttackType.Other);

            // 添加视觉效果
            target.startColorEffect(ActorColorEffect.Red);
            target.startShake(0.3f, 0.1f, true, true);
        }

        public static bool grade6_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
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
            if (a.Getcareerexperience() <= 10000)
            {
                return false;
            }

            a.Changecareerexperience(-1000);
            double successRate = 0.3; //默认概率
            double deathRate = 0.15; //失败后死亡概率15%

            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f); //生成0到1之间的随机数
            if (randomValue > successRate)
            {
                // 突破失败，判断是否死亡
                double deathRandom = UnityEngine.Random.Range(0.0f, 1.0f);
                if (deathRandom <= deathRate)
                {
                    // 10%概率死亡
                    BreakthroughDeath(pTarget.a);
                }
                return false;
            }

            // 基础默认特质 - 无论什么职业都有的特质
            string[] defaultTraits = new string[] { "OrderofBeing3" };
            // 基于OrderofBeing1特质分配不同的额外特质
            string[] additionalTraits = new string[] { }; // 初始为空数组
            string[] traitsToRemove = new string[] { "tumorInfection", "cursed", "infected", "mushSpores",
            "plague", "madness", "enchanter5", "pastor5", "knight5", "valiantgeneral5", "Ranger5",
            "Assassin5", "Summoner5", "minstrel5", "warlock5" , "alchemist5", "barbarian5", "OrderofBeing2", "Summonedcreature3" };

            // 根据不同的特质分配不同的特质（按数字顺序排列）
            if (a.hasTrait("enchanter5"))
            {
                // 法师职业 - 增加魔法相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "enchanter6" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "enchanter6");
                ApplyXianTitle(a, "enchanter6");
            }
            else if (a.hasTrait("pastor5"))
            {
                // 牧师职业 - 增加防御和回复相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "pastor6" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "pastor6");
                ApplyXianTitle(a, "pastor6");
            }
            else if (a.hasTrait("knight5"))
            {
                // 骑士职业 - 增加防御和战斗相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "knight6" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "knight6");
                ApplyXianTitle(a, "knight6");
            }
            else if (a.hasTrait("valiantgeneral5"))
            {
                // 战士职业 - 增加攻击和暴击相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "valiantgeneral6" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "valiantgeneral6");
                ApplyXianTitle(a, "valiantgeneral6");
            }
            else if (a.hasTrait("Ranger5"))
            {
                // 射手职业 - 增加远程攻击相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Ranger6" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Ranger6");
                ApplyXianTitle(a, "Ranger6");
            }
            else if (a.hasTrait("Assassin5"))
            {
                // 刺客职业 - 增加敏捷相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Assassin6" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Assassin6");
                ApplyXianTitle(a, "Assassin6");
            }
            else if (a.hasTrait("Summoner5"))
            {
                // 召唤师职业 - 增加召唤相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Summoner6" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Summoner6");
                ApplyXianTitle(a, "Summoner6");
            }
            else if (a.hasTrait("minstrel5"))
            {
                // 吟游诗人职业 - 增加音乐相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "minstrel6" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "minstrel6");
                ApplyXianTitle(a, "minstrel6");
            }
            else if (a.hasTrait("warlock5"))
            {
                // 咒术师职业 - 增加诅咒相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "warlock6" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "warlock6");
                ApplyXianTitle(a, "warlock6");
            }
            else if (a.hasTrait("alchemist5"))
            {
                // 炼金术士职业 - 增加炼金相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "alchemist6" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "alchemist6");
                ApplyXianTitle(a, "alchemist6");
            }
            else if (a.hasTrait("barbarian5"))
            {
                // 野蛮人职业 - 增加力量相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "barbarian6" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "barbarian6");
                ApplyXianTitle(a, "barbarian6");
            }
            else if (a.hasTrait("Summonedcreature3"))
            {
                // 召唤兽职业 - 增加召唤相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "Summonedcreature4" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "Summonedcreature4");
                ApplyXianTitle(a, "Summonedcreature4");
            }

            if (ThefantasyworldConfig.AutoCollectDemigod)
            {
                a.data.favorite = true;
            }

            return true;
        }

        // 职业7级特质唯一性相关变量
        private static readonly Dictionary<string, Actor> s_grade7Owners = new Dictionary<string, Actor>();
        private static bool s_grade7OwnersScannedOnce = false;

        // 一次性扫描，找出已有的7级特质持有者
        private static void EnsureGrade7OwnersCachedOnce()
        {
            if (s_grade7OwnersScannedOnce) return;
            s_grade7OwnersScannedOnce = true;

            var list = MapBox.instance.units?.getSimpleList();
            if (list == null) return;

            string[] grade7Traits = new string[] { "enchanter7", "pastor7", "knight7", "valiantgeneral7", "Ranger7", 
                "Assassin7", "Summoner7", "minstrel7", "warlock7", "alchemist7", "barbarian7" };

            for (int i = 0; i < list.Count; i++)
            {
                var u = list[i];
                if (u == null || !u.isAlive()) continue;
                
                foreach (var trait in grade7Traits)
                {
                    if (u.hasTrait(trait))
                    {
                        s_grade7Owners[trait] = u;
                        break;
                    }
                }
            }
        }

        // 维护7级特质持有者缓存
        private static void MaintainGrade7Owners()
        {
            List<string> toRemove = new List<string>();
            
            foreach (var kvp in s_grade7Owners)
            {
                if (kvp.Value == null || !kvp.Value.isAlive() || !kvp.Value.hasTrait(kvp.Key))
                {
                    toRemove.Add(kvp.Key);
                }
            }
            
            foreach (var key in toRemove)
            {
                s_grade7Owners.Remove(key);
            }
        }

        // 检查某个7级特质是否可用
        private static bool IsGrade7TraitAvailableFor(string trait, Actor requester)
        {
            EnsureGrade7OwnersCachedOnce();
            MaintainGrade7Owners();
            
            if (s_grade7Owners.ContainsKey(trait))
            {
                // 如果请求者已经是持有者，则允许（可能是读档或其他情况）
                return s_grade7Owners[trait] == requester;
            }
            
            return true;
        }

        // 设置7级特质持有者
        private static void SetGrade7Owner(string trait, Actor owner)
        {
            s_grade7Owners[trait] = owner;
            s_grade7OwnersScannedOnce = true;
        }

        public static bool grade7_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
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
            if (a.Getcareerexperience() <= 50000)
            {
                return false;
            }

            // 基础默认特质 - 无论什么职业都有的特质
            string[] defaultTraits = new string[] { "OrderofBeing4", "immortal" };
            // 基于OrderofBeing1特质分配不同的额外特质
            string[] additionalTraits = new string[] { }; // 初始为空数组
            string[] traitsToRemove = new string[] { "tumorInfection", "cursed", "infected", "mushSpores",
            "plague", "madness", "enchanter6", "pastor6", "knight6", "valiantgeneral6", "Ranger6",
            "Assassin6", "Summoner6", "minstrel6", "warlock6" , "alchemist6", "barbarian6", "OrderofBeing3" };

            // 先检查角色的目标职业7级特质是否可用
            string targetTrait = "";
            if (a.hasTrait("enchanter6"))
            {
                targetTrait = "enchanter7";
            }
            else if (a.hasTrait("pastor6"))
            {
                targetTrait = "pastor7";
            }
            else if (a.hasTrait("knight6"))
            {
                targetTrait = "knight7";
            }
            else if (a.hasTrait("valiantgeneral6"))
            {
                targetTrait = "valiantgeneral7";
            }
            else if (a.hasTrait("Ranger6"))
            {
                targetTrait = "Ranger7";
            }
            else if (a.hasTrait("Assassin6"))
            {
                targetTrait = "Assassin7";
            }
            else if (a.hasTrait("Summoner6"))
            {
                targetTrait = "Summoner7";
            }
            else if (a.hasTrait("minstrel6"))
            {
                targetTrait = "minstrel7";
            }
            else if (a.hasTrait("warlock6"))
            {
                targetTrait = "warlock7";
            }
            else if (a.hasTrait("alchemist6"))
            {
                targetTrait = "alchemist7";
            }
            else if (a.hasTrait("barbarian6"))
            {
                targetTrait = "barbarian7";
            }
            
            // 检查是否已拥有任何职业7级特质
            string[] grade7Traits = new string[] { "enchanter7", "pastor7", "knight7", "valiantgeneral7", "Ranger7", 
                "Assassin7", "Summoner7", "minstrel7", "warlock7", "alchemist7", "barbarian7" };
            
            foreach (var trait in grade7Traits)
            {
                if (a.hasTrait(trait))
                {
                    return false; // 已拥有职业7特质，不允许突破
                }
            }
            
            // 如果有目标特质且该特质不可用，则直接返回false
            if (!string.IsNullOrEmpty(targetTrait) && !IsGrade7TraitAvailableFor(targetTrait, a))
            {
                return false;
            }

            // 进行突破相关操作
            a.Changecareerexperience(-5000);
            double successRate = 0.2; //默认概率降低
            double deathRate = 0.2; //失败后死亡概率20%

            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f); //生成0到1之间的随机数
            if (randomValue > successRate)
            {
                // 突破失败，判断是否死亡
                double deathRandom = UnityEngine.Random.Range(0.0f, 1.0f);
                if (deathRandom <= deathRate)
                {
                    BreakthroughDeath(pTarget.a);
                }
                return false;
            }

            // 根据不同的特质分配不同的特质（按数字顺序排列）
            if (a.hasTrait("enchanter6"))
            {
                additionalTraits = defaultTraits.Concat(new string[] { targetTrait }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, targetTrait);
                ApplyXianTitle(a, targetTrait);
                SetGrade7Owner(targetTrait, a);
            }
            else if (a.hasTrait("pastor6"))
            {
                additionalTraits = defaultTraits.Concat(new string[] { targetTrait }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, targetTrait);
                ApplyXianTitle(a, targetTrait);
                SetGrade7Owner(targetTrait, a);
            }
            else if (a.hasTrait("knight6"))
            {      
                additionalTraits = defaultTraits.Concat(new string[] { targetTrait }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, targetTrait);
                ApplyXianTitle(a, targetTrait);
                SetGrade7Owner(targetTrait, a);
            }
            else if (a.hasTrait("valiantgeneral6"))
            {      
                additionalTraits = defaultTraits.Concat(new string[] { targetTrait }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, targetTrait);
                ApplyXianTitle(a, targetTrait);
                SetGrade7Owner(targetTrait, a);
            }
            else if (a.hasTrait("Ranger6"))
            {          
                additionalTraits = defaultTraits.Concat(new string[] { targetTrait }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, targetTrait);
                ApplyXianTitle(a, targetTrait);
                SetGrade7Owner(targetTrait, a);
            }
            else if (a.hasTrait("Assassin6"))
            {            
                additionalTraits = defaultTraits.Concat(new string[] { targetTrait }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, targetTrait);
                ApplyXianTitle(a, targetTrait);
                SetGrade7Owner(targetTrait, a);
            }
            else if (a.hasTrait("Summoner6"))
            {           
                additionalTraits = defaultTraits.Concat(new string[] { targetTrait }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, targetTrait);
                ApplyXianTitle(a, targetTrait);
                SetGrade7Owner(targetTrait, a);
            }
            else if (a.hasTrait("minstrel6"))
            {            
                additionalTraits = defaultTraits.Concat(new string[] { targetTrait }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, targetTrait);
                ApplyXianTitle(a, targetTrait);
                SetGrade7Owner(targetTrait, a);
            }
            else if (a.hasTrait("warlock6"))
            {          
                additionalTraits = defaultTraits.Concat(new string[] { targetTrait }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, targetTrait);
                ApplyXianTitle(a, targetTrait);
                SetGrade7Owner(targetTrait, a);
            }
            else if (a.hasTrait("alchemist6"))
            {           
                additionalTraits = defaultTraits.Concat(new string[] { targetTrait }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, targetTrait);
                ApplyXianTitle(a, targetTrait);
                SetGrade7Owner(targetTrait, a);
            }
            else if (a.hasTrait("barbarian6"))
            {
                additionalTraits = defaultTraits.Concat(new string[] { targetTrait }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, targetTrait);
                ApplyXianTitle(a, targetTrait);
                SetGrade7Owner(targetTrait, a);
            }

            if (ThefantasyworldConfig.AutoCollectgod)
            {
                a.data.favorite = true;
            }

            return true;
        }
    }
}