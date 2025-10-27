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
            
            // 牧师特质
            {"pastor1", "信徒"},
            {"pastor2", "牧师"},
            {"pastor3", "圣职者"},
            {"pastor4", "神圣祭司"},
            {"pastor5", "传奇牧师"},
            
            // 骑士特质
            {"knight1", "侍从"},
            {"knight2", "骑士"},
            {"knight3", "圣骑士"},
            {"knight4", "圣殿骑士"},
            {"knight5", "传奇骑士"},
            
            // 战士特质
            {"valiantgeneral1", "初级战士"},
            {"valiantgeneral2", "高级战士"},
            {"valiantgeneral3", "勇士"},
            {"valiantgeneral4", "狂战士"},
            {"valiantgeneral5", "传奇战士"},
            
            // 射手特质
            {"shooter1", "新手"},
            {"shooter2", "射手"},
            {"shooter3", "狙击大师"},
            {"shooter4", "神射手"},
            {"shooter5", "传奇射手"},
            
            // 刺客特质
            {"assassin1", "杀手"},
            {"assassin2", "刺客"},
            {"assassin3", "影刺客"},
            {"assassin4", "暗夜刺客"},
            {"assassin5", "传奇刺客"}
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
            "万法之主", "织法者", "奥秘编织者", "秘法亲王", "真理探寻者",
            "咒缚者", "迷锁之主", "位面旅者", "奥术帝皇", "灵魂编织者",
            "符文亲王", "千法之智", "星辰观测者", "以太统御者", "混沌塑形师",
            "时光漫步者", "秘艺至高者", "虚空咏者", "万象归一会首", "传奇大魔导师"
            }},
            
            // 牧师职业尊号
            {"pastor5", new string[] {
            "圣者", "神选者", "神圣化身", "天堂之拳", "神恩具现",
            "信仰壁垒", "圣言使徒", "黎明先驱", "福音传播者", "神圣裁断官",
            "神之左手", "不朽丰碑", "最终祷言", "神国基石", "灵魂牧者",
            "万神谕使", "圣光本源", "教义化身", "受难圣徒", "传奇大主教"
            }},
            
            // 骑士职业尊号
            {"knight5", new string[] {
            "圣辉冠军", "龙骑统帅", "荣耀壁垒", "誓言捍卫者", "天启骑士",
            "王权之剑", "终末防线", "不落要塞", "银辉大团长", "秩序之柱",
            "传奇督军", "美德化身", "无畏之心", "希望旌旗", "黎明骑士",
            "龙血统帅", "人民之盾", "决斗之王", "不朽守护", "骑士王"
            }},
            
            // 战士职业尊号
            {"valiantgeneral5", new string[] {
            "战神", "战争化身", "无双战神", "征服者", "军团主宰",
            "武器宗师", "战场之王", "钢铁风暴", "千军破", "浴血战神",
            "不败传说", "传奇角斗士", "战争领主", "破城者", "军魂",
            "终极兵器", "狂战神", "武之极境", "凡人之神", "帝国元帅"
            }},
            
            // 射手职业尊号
            {"shooter5", new string[] {
            "苍穹之眼", "穿心者", "风语者", "寂静死神", "鹰眼领主",
            "自然之怒", "弦月舞者", "千步追魂", "弹幕诗人", "因果律箭",
            "天际线主宰", "游击大师", "万矢", "破魔箭圣", "林间圣箭",
            "狙击帝王", "无弦射手", "必中之矢", "传奇游侠", "猎星者"
            }},
            
            // 刺客职业尊号
            {"assassin5", new string[] {
            "影舞者", "无面者", "终末低语", "死亡信使", "暗影宗师",
            "剧毒艺术大师", "命运暗面", "弑君者", "虚无行者", "阴影编织者",
            "传奇割喉者", "恐惧本源", "契约执行人", "夜之主宰", "一步一杀",
            "存在抹除者", "完美谋杀", "众影之父", "暗影大帝", "刺客宗师"
            }},
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
                "occupation" // 检查是否已有职业特质
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
            // 基于occupation特质分配不同的额外特质
            string[] additionalTraits = new string[] { }; // 初始为空数组
            string[] traitsToRemove = new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness" };

            // 随机选择一个职业特质
            int randomClass = UnityEngine.Random.Range(1, 7); // 随机生成1-6的数字，对应6种职业

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
                    additionalTraits = defaultTraits.Concat(new string[] { "shooter1" }).ToArray();
                    UpdateXianSuffix(a, "shooter1");
                    break;
                case 6: // 刺客职业
                    additionalTraits = defaultTraits.Concat(new string[] { "assassin1" }).ToArray();
                    UpdateXianSuffix(a, "assassin1");
                    break;
            }

            upTrait("", "occupation", a, traitsToRemove, additionalTraits);
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
            // 基于occupation特质分配不同的额外特质
            string[] additionalTraits = new string[] { }; // 初始为空数组
            string[] traitsToRemove = new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "enchanter1", "pastor1", "knight1", "valiantgeneral1", "shooter1", "assassin1" };

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
            else if (a.hasTrait("shooter1"))
            {
                // 射手职业 - 增加远程攻击相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "shooter2" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "shooter2");
            }
            else if (a.hasTrait("assassin1"))
            {
                // 刺客职业 - 增加敏捷相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "assassin2" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "assassin2");
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
            // 基于occupation特质分配不同的额外特质
            string[] additionalTraits = new string[] { }; // 初始为空数组
            string[] traitsToRemove = new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "enchanter2", "pastor2", "knight2", "valiantgeneral2", "shooter2", "assassin2" };

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
            else if (a.hasTrait("shooter2"))
            {
                // 射手职业 - 增加远程攻击相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "shooter3" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "shooter3");
            }
            else if (a.hasTrait("assassin2"))
            {
                // 刺客职业 - 增加敏捷相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "assassin3" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "assassin3");
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
            // 基于occupation特质分配不同的额外特质
            string[] additionalTraits = new string[] { }; // 初始为空数组
            string[] traitsToRemove = new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "enchanter3", "pastor3", "knight3", "valiantgeneral3", "shooter3", "assassin3" };

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
            else if (a.hasTrait("shooter3"))
            {
                // 射手职业 - 增加远程攻击相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "shooter4" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "shooter4");
            }
            else if (a.hasTrait("assassin3"))
            {
                // 刺客职业 - 增加敏捷相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "assassin4" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "assassin4");
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
            string[] defaultTraits = new string[] { "fire_proof", "freeze_proof" };
            // 基于occupation特质分配不同的额外特质
            string[] additionalTraits = new string[] { }; // 初始为空数组
            string[] traitsToRemove = new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "enchanter4", "pastor4", "knight4", "valiantgeneral4", "shooter4", "assassin4" };

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
            else if (a.hasTrait("shooter4"))
            {
                // 射手职业 - 增加远程攻击相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "shooter5" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "shooter5");
                ApplyXianTitle(a, "shooter5");
            }
            else if (a.hasTrait("assassin4"))
            {
                // 刺客职业 - 增加敏捷相关特质
                additionalTraits = defaultTraits.Concat(new string[] { "assassin5" }).ToArray();
                upTrait("", "", a, traitsToRemove, additionalTraits);
                UpdateXianSuffix(a, "assassin5");
                ApplyXianTitle(a, "assassin5");
            }

            return true;
        }

        // 法师职业 🔮（大魔导师）
        public static bool enchanter1_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(4);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(7);
            }
            return true;
        }

        public static bool enchanter2_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(8);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(14);
            }
            return true;
        }

        public static bool enchanter3_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(16);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(28);
            }
            return true;
        }


        public static bool enchanter4_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(32);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(56);
            }
            return true;
        }

        public static bool enchanter5_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(64);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(112);
            }
            return true;
        }

        // 牧师职业 ✨（神圣祭司）
        public static bool pastor1_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(20);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(4);
            }
            return true;
        }

        public static bool pastor2_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(40);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(8);
            }
            return true;
        }

        public static bool pastor3_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(80);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(16);
            }
            return true;
        }


        public static bool pastor4_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(160);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(32);
            }
            return true;
        }

        public static bool pastor5_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(320);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(64);
            }
            return true;
        }

        // 骑士职业 🛡️（圣殿骑士）
        public static bool knight1_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(12);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(3);
            }
            return true;
        }

        public static bool knight2_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(24);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(6);
            }
            return true;
        }

        public static bool knight3_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(48);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(12);
            }
            return true;
        }


        public static bool knight4_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(96);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(24);
            }
            return true;
        }

        public static bool knight5_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(192);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(48);
            }
            return true;
        }

        // 战士职业 ⚔️（狂战士）
        public static bool valiantgeneral1_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(8);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(2);
            }
            return true;
        }

        public static bool valiantgeneral2_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(16);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(4);
            }
            return true;
        }

        public static bool valiantgeneral3_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(32);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(8);
            }
            return true;
        }


        public static bool valiantgeneral4_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(64);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(16);
            }
            return true;
        }

        public static bool valiantgeneral5_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(128);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(32);
            }
            return true;
        }

        // 射手职业 🏹（神射手）
        public static bool shooter1_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(5);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(3);
            }
            return true;
        }

        public static bool shooter2_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(10);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(6);
            }
            return true;
        }

        public static bool shooter3_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(20);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(12);
            }
            return true;
        }

        public static bool shooter4_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(40);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(24);
            }
            return true;
        }

        public static bool shooter5_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(80);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(48);
            }
            return true;
        }

        // 刺客职业 🗡️（影舞者）
        public static bool assassin1_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(2);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(2);
            }
            return true;
        }

        public static bool assassin2_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(4);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(4);
            }
            return true;
        }

        public static bool assassin3_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(8);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(8);
            }
            return true;
        }

        public static bool assassin4_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(16);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(16);
            }
            return true;
        }

        public static bool assassin5_effect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health < pTarget.a.getMaxHealth())
            {
                pTarget.a.restoreHealth(32);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            if (pTarget.a.data.mana < pTarget.a.getMaxMana())
            {
                pTarget.a.addMana(32);
            }
            return true;
        }

        private static Dictionary<Actor, float> _enchanter3Cooldowns = new Dictionary<Actor, float>();
        private const float ENCHANTER3_COOLDOWN = 0.2f; // 冷却时间

        public static bool attack_enchanter3(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    // 检查冷却时间
                    if (_enchanter3Cooldowns.TryGetValue(attacker, out float lastUsedTime))
                    {
                        if (UnityEngine.Time.time - lastUsedTime < ENCHANTER3_COOLDOWN)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _enchanter3Cooldowns[attacker] = UnityEngine.Time.time;
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 1; // 投射物数量
                    float spreadAngle = 1f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "freeze_orb",                  // 投射物資產 ID
                        pLaunchPosition: newPoint,      // 發射位置
                        pTargetPosition: spreadTarget,      // 目標位置
                        pTargetZ: 0.0f                      // 目標 Z 軸（通常為 0）
                    );
                    }

                    return true;
                }
            }
            return false;
        }

        public static bool attack_enchanter4(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pSelf == null || pTarget == null)
            {
                return false;
            }

            // 2. 隨機分配攻擊
            // 隨機選擇要觸發的攻擊效果
            float randomValue = UnityEngine.Random.value;
            if (randomValue <= 0.50f)
            {
                // 50% 機率觸發 attack1_enchanter4
                return attack1_enchanter4(pSelf, pTarget, pTile);
            }
            else
            {
                // 另外 50% 機率觸發 attack2_enchanter4
                return attack2_enchanter4(pSelf, pTarget, pTile);
            }
        }

        private static Dictionary<Actor, float> _enchanter4Cooldowns1 = new Dictionary<Actor, float>();
        private const float ENCHANTER4_COOLDOWN1 = 0.2f; // 冷却时间

        public static bool attack1_enchanter4(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    // 检查冷却时间
                    if (_enchanter4Cooldowns1.TryGetValue(attacker, out float lastUsedTime))
                    {
                        if (UnityEngine.Time.time - lastUsedTime < ENCHANTER4_COOLDOWN1)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _enchanter4Cooldowns1[attacker] = UnityEngine.Time.time;
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 2; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "freeze_orb",                  // 投射物資產 ID
                        pLaunchPosition: newPoint,      // 發射位置
                        pTargetPosition: spreadTarget,      // 目標位置
                        pTargetZ: 0.0f                      // 目標 Z 軸（通常為 0）
                    );
                    }

                    return true;
                }
            }
            return false;
        }

        private static Dictionary<Actor, float> _enchanter4Cooldowns2 = new Dictionary<Actor, float>();
        private const float ENCHANTER4_COOLDOWN2 = 0.2f; // 冷却时间

        public static bool attack2_enchanter4(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    // 检查冷却时间
                    if (_enchanter4Cooldowns2.TryGetValue(attacker, out float lastUsedTime))
                    {
                        if (UnityEngine.Time.time - lastUsedTime < ENCHANTER4_COOLDOWN2)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _enchanter4Cooldowns2[attacker] = UnityEngine.Time.time;
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 2; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "fx_wind_trail_t",                  // 投射物資產 ID
                        pLaunchPosition: newPoint,      // 發射位置
                        pTargetPosition: spreadTarget,      // 目標位置
                        pTargetZ: 0.0f                      // 目標 Z 軸（通常為 0）
                    );
                    }

                    return true;
                }
            }
            return false;
        }

        public static bool attack_enchanter5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pSelf == null || pTarget == null)
            {
                return false;
            }

            float randomValue = UnityEngine.Random.value;
            if (randomValue <= 0.33f)
            {
                // 約33% 機率觸發 attack1_enchanter5
                return attack1_enchanter5(pSelf, pTarget, pTile);
            }
            else if (randomValue <= 0.66f)
            {
                // 約33% 機率觸發 attack2_enchanter5
                return attack2_enchanter5(pSelf, pTarget, pTile);
            }
            else
            {
                // 約34% 機率觸發 attack3_enchanter5
                return attack3_enchanter5(pSelf, pTarget, pTile);
            }
        }

        private static Dictionary<Actor, float> _enchanter5Cooldowns1 = new Dictionary<Actor, float>();
        private const float ENCHANTER5_COOLDOWN1 = 0.2f; // 冷却时间

        public static bool attack1_enchanter5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    // 检查冷却时间
                    if (_enchanter5Cooldowns1.TryGetValue(attacker, out float lastUsedTime))
                    {
                        if (UnityEngine.Time.time - lastUsedTime < ENCHANTER5_COOLDOWN1)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _enchanter5Cooldowns1[attacker] = UnityEngine.Time.time;
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 4; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "freeze_orb",                  // 投射物資產 ID
                        pLaunchPosition: newPoint,      // 發射位置
                        pTargetPosition: spreadTarget,      // 目標位置
                        pTargetZ: 0.0f                      // 目標 Z 軸（通常為 0）
                    );
                    }

                    return true;
                }
            }
            return false;
        }

        private static Dictionary<Actor, float> _enchanter5Cooldowns2 = new Dictionary<Actor, float>();
        private const float ENCHANTER5_COOLDOWN2 = 0.2f; // 冷却时间

        public static bool attack2_enchanter5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    // 检查冷却时间
                    if (_enchanter5Cooldowns2.TryGetValue(attacker, out float lastUsedTime))
                    {
                        if (UnityEngine.Time.time - lastUsedTime < ENCHANTER5_COOLDOWN2)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _enchanter5Cooldowns2[attacker] = UnityEngine.Time.time;
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 4; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "fx_wind_trail_t",                  // 投射物資產 ID
                        pLaunchPosition: newPoint,      // 發射位置
                        pTargetPosition: spreadTarget,      // 目標位置
                        pTargetZ: 0.0f                      // 目標 Z 軸（通常為 0）
                    );
                    }

                    return true;
                }
            }
            return false;
        }

        private static Dictionary<Actor, float> _enchanter5Cooldowns3 = new Dictionary<Actor, float>();
        private const float ENCHANTER5_COOLDOWN3 = 0.2f; // 冷却时间

        public static bool attack3_enchanter5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    // 检查冷却时间
                    if (_enchanter5Cooldowns3.TryGetValue(attacker, out float lastUsedTime))
                    {
                        if (UnityEngine.Time.time - lastUsedTime < ENCHANTER5_COOLDOWN3)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _enchanter5Cooldowns3[attacker] = UnityEngine.Time.time;
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 4; // 投射物数量
                    float spreadAngle = 3f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "fireball",                  // 投射物資產 ID
                        pLaunchPosition: newPoint,      // 發射位置
                        pTargetPosition: spreadTarget,      // 目標位置
                        pTargetZ: 0.0f                      // 目標 Z 軸（通常為 0）
                    );
                    }

                    return true;
                }
            }
            return false;
        }

        public static bool effect_pastor3(BaseSimObject pTarget, WorldTile pTile = null)
        {
            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 2, 8f, false))
            {
                if (tActor.kingdom == pTarget.kingdom)
                {
                    if (!tActor.hasMaxHealth())
                    {
                        tActor.restoreHealth(16);
                        tActor.spawnParticle(Toolbox.color_heal);
                    }
                }
            }
            return true;
        }

        public static bool effect_pastor4(BaseSimObject pTarget, WorldTile pTile = null)
        {

            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 4, 16f, false))
            {
                if (tActor.kingdom == pTarget.kingdom)
                {
                    if (!tActor.hasMaxHealth())
                    {
                        tActor.restoreHealth(32);
                        tActor.spawnParticle(Toolbox.color_heal);
                    }
                }
            }

            return true;
        }

        public static bool effect_pastor5(BaseSimObject pTarget, WorldTile pTile = null)
        {

            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 8, 32f, false))
            {
                if (tActor.kingdom == pTarget.kingdom)
                {
                    if (!tActor.hasMaxHealth())
                    {
                        tActor.restoreHealth(64);
                        tActor.spawnParticle(Toolbox.color_heal);
                    }
                }
            }

            return true;
        }

        public static bool effect_knight3(BaseSimObject pTarget, WorldTile pTile = null)
        {
            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 2, 8f, false))
            {
                if (tActor.kingdom == pTarget.kingdom)
                {
                    tActor.addStatusEffect("shield", 30f);
                }
            }
            return true;
        }

        public static bool effect_knight4(BaseSimObject pTarget, WorldTile pTile = null)
        {
            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 4, 16f, false))
            {
                if (tActor.kingdom == pTarget.kingdom)
                {
                    tActor.addStatusEffect("shield", 30f);
                }
            }
            return true;
        }

        public static bool effect_knight5(BaseSimObject pTarget, WorldTile pTile = null)
        {
            foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 8, 32f, false))
            {
                if (tActor.kingdom == pTarget.kingdom)
                {
                    tActor.addStatusEffect("shield", 30f);
                }
            }
            return true;
        }

        public static bool attack_valiantgeneral3(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                int fixedDamage = 2;
                
                if (fixedDamage > 0 && targetActor.data.health > 0)
                {
                    targetActor.changeHealth(-fixedDamage);
                    if (!attacker.hasMaxHealth())
                    {
                        attacker.restoreHealth(2);
                    }
                    if (!targetActor.hasHealth())
                    {
                        targetActor.batch.c_check_deaths.Add(targetActor);
                        targetActor.checkCallbacksOnDeath();
                    }
                }
            }
            return false;
        }

        public static bool attack_valiantgeneral4(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                int fixedDamage = 4;
                
                if (fixedDamage > 0 && targetActor.data.health > 0)
                {
                    targetActor.changeHealth(-fixedDamage);
                    if (!attacker.hasMaxHealth())
                    {
                        attacker.restoreHealth(4);
                    }
                    if (!targetActor.hasHealth())
                    {
                        targetActor.batch.c_check_deaths.Add(targetActor);
                        targetActor.checkCallbacksOnDeath();
                    }
                }
            }
            return false;
        }

        public static bool attack_valiantgeneral5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                int fixedDamage = 8;
                
                if (fixedDamage > 0 && targetActor.data.health > 0)
                {
                    targetActor.changeHealth(-fixedDamage);
                    if (!attacker.hasMaxHealth())
                    {
                        attacker.restoreHealth(8);
                    }
                    if (!targetActor.hasHealth())
                    {
                        targetActor.batch.c_check_deaths.Add(targetActor);
                        targetActor.checkCallbacksOnDeath();
                    }
                }
            }
            return false;
        }

        // 存储每个攻击者的最后使用时间，实现冷却效果
        private static Dictionary<Actor, float> _shooter3Cooldowns = new Dictionary<Actor, float>();
        private const float SHOOTER3_COOLDOWN = 1f; // 冷却时间

        public static bool attack_shooter3(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    // 检查冷却时间
                    if (_shooter3Cooldowns.TryGetValue(attacker, out float lastUsedTime))
                    {
                        if (UnityEngine.Time.time - lastUsedTime < SHOOTER3_COOLDOWN)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _shooter3Cooldowns[attacker] = UnityEngine.Time.time;
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 3; // 投射物数量
                    float spreadAngle = 3.0f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "MagicArrow",                  // 投射物資產 ID
                        pLaunchPosition: newPoint,      // 發射位置
                        pTargetPosition: spreadTarget,      // 目標位置
                        pTargetZ: 0.0f                      // 目標 Z 軸（通常為 0）
                    );
                    }

                    return true;
                }
            }
            return false;
        }

        private static Dictionary<Actor, float> _shooter4Cooldowns = new Dictionary<Actor, float>();
        private const float SHOOTER4_COOLDOWN = 1f; // 冷却时间

        public static bool attack_shooter4(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    // 检查冷却时间
                    if (_shooter4Cooldowns.TryGetValue(attacker, out float lastUsedTime))
                    {
                        if (UnityEngine.Time.time - lastUsedTime < SHOOTER4_COOLDOWN)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _shooter4Cooldowns[attacker] = UnityEngine.Time.time;
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 6; // 投射物数量
                    float spreadAngle = 3.0f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "MagicArrow",                  // 投射物資產 ID
                        pLaunchPosition: newPoint,      // 發射位置
                        pTargetPosition: spreadTarget,      // 目標位置
                        pTargetZ: 0.0f                      // 目標 Z 軸（通常為 0）
                    );
                    }

                    return true;
                }
            }
            return false;
        }

        private static Dictionary<Actor, float> _shooter5Cooldowns = new Dictionary<Actor, float>();
        private const float SHOOTER5_COOLDOWN = 1f; // 冷却时间

        public static bool attack_shooter5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pSelf != null)
            {
                // 确保pSelf是Actor类型
                if (pSelf.isActor() && pTarget.a != null)
                {
                    Actor attacker = pSelf.a;
                    // 检查冷却时间
                    if (_shooter5Cooldowns.TryGetValue(attacker, out float lastUsedTime))
                    {
                        if (UnityEngine.Time.time - lastUsedTime < SHOOTER5_COOLDOWN)
                        {
                            return false; // 冷却中，无法使用
                        }
                    }
                    // 更新最后使用时间
                    _shooter5Cooldowns[attacker] = UnityEngine.Time.time;
                    Vector2Int pos = pTile.pos;
                    float pDist = Vector2.Distance(pTarget.current_position, pos);
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.current_position.x, pSelf.current_position.y, (float)pos.x, (float)pos.y, pDist, true);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.current_position.x, pTarget.current_position.y, (float)pos.x, (float)pos.y, pTarget.a.stats["size"], true);

                    // 添加多个投射物
                    int numberOfProjectiles = 12; // 投射物数量
                    float spreadAngle = 1.5f; // 投射物散布角度

                    for (int i = 0; i < numberOfProjectiles; i++)
                    {
                        // 计算每个投射物的旋转角度，以实现扇形散布
                        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(0f, 0f, spreadAngle * (i - (numberOfProjectiles - 1f) / 2f));
                        // 计算每个投射物的最终目标点
                        Vector3 spreadTarget = rotation * (newPoint2 - newPoint) + newPoint;

                        World.world.projectiles.spawn(
                        pInitiator: attacker,           // 發射者
                        pTargetObject: pTarget,             // 目標對象
                        pAssetID: "MagicArrow",                  // 投射物資產 ID
                        pLaunchPosition: newPoint,      // 發射位置
                        pTargetPosition: spreadTarget,      // 目標位置
                        pTargetZ: 0.0f                      // 目標 Z 軸（通常為 0）
                    );
                    }

                    return true;
                }
            }
            return false;
        }

        public static bool attack_assassin3(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                // 10%概率触发特殊效果
                if (Randy.randomChance(0.1f))
                {
                    float attackDamage = attacker.stats["damage"];

                    int trueDamage = (int)(attackDamage * 3f);
                    if (trueDamage > 0 && targetActor.data.health > 0)
                    {
                        int finalDmg = Mathf.Max(1, trueDamage);
                        targetActor.changeHealth(-finalDmg);
                        if (!targetActor.hasHealth())
                        {
                            targetActor.batch.c_check_deaths.Add(targetActor);
                            targetActor.checkCallbacksOnDeath();
                        }
                    }
                }
            }
            return false;
        }

        public static bool attack_assassin4(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                // 10%概率触发特殊效果
                if (Randy.randomChance(0.1f))
                {
                    float attackDamage = attacker.stats["damage"];

                    int trueDamage = (int)(attackDamage * 6f);
                    if (trueDamage > 0 && targetActor.data.health > 0)
                    {
                        int finalDmg = Mathf.Max(1, trueDamage);
                        targetActor.changeHealth(-finalDmg);
                        if (!targetActor.hasHealth())
                        {
                            targetActor.batch.c_check_deaths.Add(targetActor);
                            targetActor.checkCallbacksOnDeath();
                        }
                    }
                }
            }
            return false;
        }

        public static bool attack_assassin5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 检查目标和自身是否有效且都是Actor单位
            if (pTarget != null && pTarget.isActor() && pSelf.isActor())
            {
                Actor attacker = pSelf.a;      // 攻击者
                Actor targetActor = pTarget.a; // 目标单位
                // 10%概率触发特殊效果
                if (Randy.randomChance(0.1f))
                {
                    float attackDamage = attacker.stats["damage"];

                    int trueDamage = (int)(attackDamage * 12f);
                    if (trueDamage > 0 && targetActor.data.health > 0)
                    {
                        int finalDmg = Mathf.Max(1, trueDamage);
                        targetActor.changeHealth(-finalDmg);
                        if (!targetActor.hasHealth())
                        {
                            targetActor.batch.c_check_deaths.Add(targetActor);
                            targetActor.checkCallbacksOnDeath();
                        }
                    }
                }
            }
            return false;
        }
    }
}