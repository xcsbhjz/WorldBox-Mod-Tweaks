using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ReflectionUtility;
using ai;
using System.Numerics;

namespace PeerlessThedayofGodswrath.code
{
    internal class traits
    {
        public static ActorTrait RankTalentst_AddActorTrait(string id, string pathIcon)
        {
            ActorTrait RankTalentst = new ActorTrait
            {
                id = id,
                path_icon = pathIcon,
                group_id = "RankTalentst",
                needs_to_be_explored = false,
                base_stats = new BaseStats()
            };
            RankTalentst.action_special_effect += traitAction.grade1_effectAction;//升级条件
            AssetManager.traits.add(RankTalentst);
            return RankTalentst;
        }

        public static void Init()
        {
            _ = RankTalentst_AddActorTrait("RankTalentst1", "trait/RankTalentst1");
            _ = RankTalentst_AddActorTrait("RankTalentst2", "trait/RankTalentst2");
            _ = RankTalentst_AddActorTrait("RankTalentst3", "trait/RankTalentst3");
            _ = RankTalentst_AddActorTrait("RankTalentst4", "trait/RankTalentst4");
            _ = RankTalentst_AddActorTrait("RankTalentst5", "trait/RankTalentst5");

            ActorTrait RankTalentst92 = new ActorTrait();
            RankTalentst92.id = "RankTalentst92";
            RankTalentst92.path_icon = "trait/RankTalentst92";
            RankTalentst92.needs_to_be_explored = false;
            RankTalentst92.group_id = "RankTalentst";
            RankTalentst92.rate_inherit = 100;
            RankTalentst92.base_stats = new BaseStats();
            AssetManager.traits.add(RankTalentst92);


            ActorTrait OrderofBeing1 = new ActorTrait
            {
                id = "OrderofBeing1",
                path_icon = "trait/OrderofBeing1",
                needs_to_be_explored = false,
                group_id = "OrderofBeing",
                base_stats = new BaseStats()
            };
            AssetManager.traits.add(OrderofBeing1);

            ActorTrait OrderofBeing2 = new ActorTrait
            {
                id = "OrderofBeing2",
                path_icon = "trait/OrderofBeing2",
                needs_to_be_explored = false,
                group_id = "OrderofBeing",
                base_stats = new BaseStats()
            };
            AssetManager.traits.add(OrderofBeing2);

            ActorTrait OrderofBeing3 = new ActorTrait
            {
                id = "OrderofBeing3",
                path_icon = "trait/OrderofBeing3",
                needs_to_be_explored = false,
                group_id = "OrderofBeing",
                base_stats = new BaseStats()
            };
            AssetManager.traits.add(OrderofBeing3);

            ActorTrait OrderofBeing4 = new ActorTrait
            {
                id = "OrderofBeing4",
                path_icon = "trait/OrderofBeing4",
                needs_to_be_explored = false,
                group_id = "OrderofBeing",
                base_stats = new BaseStats()
            };
            AssetManager.traits.add(OrderofBeing4);


            // 法师 🔮（大魔导师）
            ActorTrait enchanter1 = new ActorTrait
            {
                id = "enchanter1",
                path_icon = "trait/enchanter1",
                needs_to_be_explored = false,
                group_id = "enchanter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 36f,             // 寿命
                    ["damage"] = 50f,               // 伤害
                    ["health"] = 120f,              // 生命值
                    ["armor"] = 10f,                // 防御
                    ["attack_speed"] = 1.0f,        // 攻击速度
                    ["hitthetarget"] = 55f,         // 命中
                    ["DodgeEvade"] = 55f,           // 闪避
                    ["MagicApplication"] = 12.0f,   // 魔力应用
                    ["MagicShield"] = 20f,          // 护盾
                    ["Fixedwound"] = 35f,           // 固定法伤
                    ["Restorehealth"] = 3f,         // 回血
                    ["MagicReply"] = 12f,           // 魔力回复
                    ["mana"] = 220f,                // 魔力值
                    ["targets"] = 6f,               // 攻击目标
                    ["area_of_effect"] = 3f,        // 影响范围
                    ["range"] = 3f,                 // 攻击范围
                    ["speed"] = 20f,                 // 移动速度
                    ["multiplier_speed"] = 1f,
                }
            };
            enchanter1.action_special_effect += traitAction.grade2_effectAction;
            AssetManager.traits.add(enchanter1);

            ActorTrait enchanter2 = new ActorTrait
            {
                id = "enchanter2",
                path_icon = "trait/enchanter2",
                needs_to_be_explored = false,
                group_id = "enchanter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 72f,
                    ["damage"] = 100f,
                    ["health"] = 240f,
                    ["armor"] = 20f,
                    ["attack_speed"] = 2.0f,
                    ["hitthetarget"] = 85f,
                    ["DodgeEvade"] = 85f,
                    ["MagicApplication"] = 12.0f,
                    ["MagicShield"] = 40f,
                    ["Fixedwound"] = 70f,
                    ["Restorehealth"] = 6f,         // 回血
                    ["MagicReply"] = 24f,           // 魔力回复
                    ["mana"] = 440f,
                    ["targets"] = 12f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            enchanter2.action_special_effect += traitAction.grade3_effectAction;
            AssetManager.traits.add(enchanter2);

            ActorTrait enchanter3 = new ActorTrait
            {
                id = "enchanter3",
                path_icon = "trait/enchanter3",
                needs_to_be_explored = false,
                group_id = "enchanter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 144f,
                    ["damage"] = 200f,
                    ["health"] = 480f,
                    ["armor"] = 40f,
                    ["attack_speed"] = 4.0f,
                    ["hitthetarget"] = 115f,
                    ["DodgeEvade"] = 115f,
                    ["MagicApplication"] = 12.0f,
                    ["MagicShield"] = 80f,
                    ["Fixedwound"] = 140f,
                    ["Restorehealth"] = 12f,        // 回血
                    ["MagicReply"] = 48f,           // 魔力回复
                    ["mana"] = 880f,
                    ["targets"] = 24f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            enchanter3.action_attack_target += new AttackAction((enchanterSkill.attack_enchanter3));
            enchanter3.action_special_effect += traitAction.grade4_effectAction;
            AssetManager.traits.add(enchanter3);

            ActorTrait enchanter4 = new ActorTrait
            {
                id = "enchanter4",
                path_icon = "trait/enchanter4",
                needs_to_be_explored = false,
                group_id = "enchanter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 288f,
                    ["damage"] = 400f,
                    ["health"] = 960f,
                    ["armor"] = 80f,
                    ["attack_speed"] = 8.0f,
                    ["hitthetarget"] = 145f,
                    ["DodgeEvade"] = 145f,
                    ["MagicApplication"] = 12.0f,
                    ["MagicShield"] = 160f,
                    ["Fixedwound"] = 280f,
                    ["Restorehealth"] = 24f,        // 回血
                    ["MagicReply"] = 96f,           // 魔力回复
                    ["mana"] = 1760f,
                    ["targets"] = 48f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            enchanter4.action_attack_target += new AttackAction((enchanterSkill.attack_enchanter4));
            enchanter4.action_special_effect += traitAction.grade5_effectAction;
            AssetManager.traits.add(enchanter4);

            ActorTrait enchanter5 = new ActorTrait
            {
                id = "enchanter5",
                path_icon = "trait/enchanter5",
                needs_to_be_explored = false,
                group_id = "enchanter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 576f,
                    ["damage"] = 800f,
                    ["health"] = 1920f,
                    ["armor"] = 160f,
                    ["attack_speed"] = 16.0f,
                    ["hitthetarget"] = 175f,
                    ["DodgeEvade"] = 175f,
                    ["MagicApplication"] = 12.0f,
                    ["MagicShield"] = 320f,
                    ["Fixedwound"] = 560f,
                    ["Restorehealth"] = 48f,        // 回血
                    ["MagicReply"] = 192f,          // 魔力回复
                    ["mana"] = 3520f,
                    ["targets"] = 96f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };

            enchanter5.action_attack_target += new AttackAction((enchanterSkill.attack_enchanter5));
            enchanter5.action_special_effect += traitAction.grade6_effectAction;
            AssetManager.traits.add(enchanter5);

            ActorTrait enchanter6 = new ActorTrait
            {
                id = "enchanter6",
                path_icon = "trait/enchanter6",
                needs_to_be_explored = false,
                group_id = "enchanter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 1152f,
                    ["damage"] = 1600f,
                    ["health"] = 3840f,
                    ["armor"] = 320f,
                    ["attack_speed"] = 32.0f,
                    ["hitthetarget"] = 205f,
                    ["DodgeEvade"] = 205f,
                    ["MagicApplication"] = 12.0f,
                    ["MagicShield"] = 640f,
                    ["Fixedwound"] = 1120f,
                    ["Restorehealth"] = 96f,        // 回血
                    ["MagicReply"] = 384f,          // 魔力回复
                    ["mana"] = 7040f,
                    ["targets"] = 192f,
                    ["area_of_effect"] = 96f,
                    ["range"] = 96f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            enchanter6.action_attack_target += new AttackAction((enchanterSkill.attack_enchanter6));
            enchanter6.action_special_effect += traitAction.grade7_effectAction;
            AssetManager.traits.add(enchanter6);

            ActorTrait enchanter7 = new ActorTrait
            {
                id = "enchanter7",
                path_icon = "trait/enchanter7",
                needs_to_be_explored = false,
                group_id = "enchanter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 2304f,
                    ["damage"] = 3200f,
                    ["health"] = 7680f,
                    ["armor"] = 640f,
                    ["attack_speed"] = 64.0f,
                    ["hitthetarget"] = 235f,
                    ["DodgeEvade"] = 235f,
                    ["MagicApplication"] = 24.0f,
                    ["MagicShield"] = 1280f,
                    ["Fixedwound"] = 2240f,
                    ["Restorehealth"] = 192f,       // 回血
                    ["MagicReply"] = 768f,          // 魔力回复
                    ["mana"] = 14080f,
                    ["targets"] = 384f,
                    ["area_of_effect"] = 192f,
                    ["range"] = 192f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            enchanter7.action_attack_target += new AttackAction((enchanterSkill.attack_enchanter7));
            AssetManager.traits.add(enchanter7);

            // 牧师 ✨（神圣祭司）
            ActorTrait pastor1 = new ActorTrait
            {
                id = "pastor1",
                path_icon = "trait/pastor1",
                needs_to_be_explored = false,
                group_id = "pastor",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 32f,
                    ["damage"] = 20f,
                    ["health"] = 240f,
                    ["armor"] = 18f,
                    ["attack_speed"] = 1.2f,
                    ["hitthetarget"] = 40f,
                    ["DodgeEvade"] = 60f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 10f,
                    ["Fixedwound"] = 10f,
                    ["Restorehealth"] = 40f,       // 回血
                    ["MagicReply"] = 8f,            // 魔力回复
                    ["mana"] = 120f,
                    ["targets"] = 4f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            pastor1.action_special_effect += traitAction.grade2_effectAction;
            AssetManager.traits.add(pastor1);

            ActorTrait pastor2 = new ActorTrait
            {
                id = "pastor2",
                path_icon = "trait/pastor2",
                needs_to_be_explored = false,
                group_id = "pastor",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 64f,
                    ["damage"] = 40f,
                    ["health"] = 480f,
                    ["armor"] = 36f,
                    ["attack_speed"] = 2.4f,
                    ["hitthetarget"] = 70f,
                    ["DodgeEvade"] = 90f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 20f,
                    ["Fixedwound"] = 20f,
                    ["Restorehealth"] = 80f,       // 回血
                    ["MagicReply"] = 16f,           // 魔力回复
                    ["mana"] = 240f,
                    ["targets"] = 8f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            pastor2.action_special_effect += traitAction.grade3_effectAction;
            AssetManager.traits.add(pastor2);

            ActorTrait pastor3 = new ActorTrait
            {
                id = "pastor3",
                path_icon = "trait/pastor3",
                needs_to_be_explored = false,
                group_id = "pastor",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 128f,
                    ["damage"] = 80f,
                    ["health"] = 960f,
                    ["armor"] = 72f,
                    ["attack_speed"] = 4.8f,
                    ["hitthetarget"] = 100f,
                    ["DodgeEvade"] = 120f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 40f,
                    ["Fixedwound"] = 40f,
                    ["Restorehealth"] = 160f,      // 回血
                    ["MagicReply"] = 32f,           // 魔力回复
                    ["mana"] = 480f,
                    ["targets"] = 16f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            pastor3.action_special_effect += traitAction.grade4_effectAction;
            pastor3.action_special_effect += (WorldAction)Delegate.Combine(pastor3.action_special_effect,
                new WorldAction(pastorSkill.effect_pastor3));
            AssetManager.traits.add(pastor3);

            ActorTrait pastor4 = new ActorTrait
            {
                id = "pastor4",
                path_icon = "trait/pastor4",
                needs_to_be_explored = false,
                group_id = "pastor",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 256f,
                    ["damage"] = 160f,
                    ["health"] = 1920f,
                    ["armor"] = 144f,
                    ["attack_speed"] = 9.6f,
                    ["hitthetarget"] = 130f,
                    ["DodgeEvade"] = 150f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 80f,
                    ["Fixedwound"] = 80f,
                    ["Restorehealth"] = 320f,      // 回血
                    ["MagicReply"] = 64f,           // 魔力回复
                    ["mana"] = 960f,
                    ["targets"] = 32f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            pastor4.action_special_effect += traitAction.grade5_effectAction;
            pastor4.action_special_effect += (WorldAction)Delegate.Combine(pastor4.action_special_effect,
                new WorldAction(pastorSkill.effect_pastor4));
            AssetManager.traits.add(pastor4);

            ActorTrait pastor5 = new ActorTrait
            {
                id = "pastor5",
                path_icon = "trait/pastor5",
                needs_to_be_explored = false,
                group_id = "pastor",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 512f,
                    ["damage"] = 320f,
                    ["health"] = 3840f,
                    ["armor"] = 288f,
                    ["attack_speed"] = 19.2f,
                    ["hitthetarget"] = 160f,
                    ["DodgeEvade"] = 180f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 160f,
                    ["Fixedwound"] = 160f,
                    ["Restorehealth"] = 640f,      // 回血
                    ["MagicReply"] = 128f,          // 魔力回复
                    ["mana"] = 1920f,
                    ["targets"] = 64f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            pastor5.action_special_effect += traitAction.grade6_effectAction;
            pastor5.action_special_effect += (WorldAction)Delegate.Combine(pastor5.action_special_effect,
                new WorldAction(pastorSkill.effect_pastor5));
            AssetManager.traits.add(pastor5);

            ActorTrait pastor6 = new ActorTrait
            {
                id = "pastor6",
                path_icon = "trait/pastor6",
                needs_to_be_explored = false,
                group_id = "pastor",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 1024f,
                    ["damage"] = 640f,
                    ["health"] = 7680f,
                    ["armor"] = 576f,
                    ["attack_speed"] = 38.4f,
                    ["hitthetarget"] = 190f,
                    ["DodgeEvade"] = 210f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 320f,
                    ["Fixedwound"] = 320f,
                    ["Restorehealth"] = 1280f,     // 回血
                    ["MagicReply"] = 256f,          // 魔力回复
                    ["mana"] = 3840f,
                    ["targets"] = 128f,
                    ["area_of_effect"] = 96f,
                    ["range"] = 96f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            pastor6.action_special_effect += traitAction.grade7_effectAction;
            pastor6.action_special_effect += (WorldAction)Delegate.Combine(pastor6.action_special_effect,
                new WorldAction(pastorSkill.effect_pastor6));
            AssetManager.traits.add(pastor6);

            ActorTrait pastor7 = new ActorTrait
            {
                id = "pastor7",
                path_icon = "trait/pastor7",
                needs_to_be_explored = false,
                group_id = "pastor",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 2048f,
                    ["damage"] = 1280f,
                    ["health"] = 15360f,
                    ["armor"] = 1152f,
                    ["attack_speed"] = 76.8f,
                    ["hitthetarget"] = 220f,
                    ["DodgeEvade"] = 240f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 640f,
                    ["Fixedwound"] = 640f,
                    ["Restorehealth"] = 2560f,     // 回血
                    ["MagicReply"] = 512f,          // 魔力回复
                    ["mana"] = 7680f,
                    ["targets"] = 256f,
                    ["area_of_effect"] = 192f,
                    ["range"] = 192f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            pastor7.action_special_effect += (WorldAction)Delegate.Combine(pastor7.action_special_effect,
                new WorldAction(pastorSkill.effect_pastor7));
            AssetManager.traits.add(pastor7);

            // 骑士 🛡️（圣殿骑士）
            ActorTrait knight1 = new ActorTrait
            {
                id = "knight1",
                path_icon = "trait/knight1",
                needs_to_be_explored = false,
                group_id = "knight",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 26f,
                    ["damage"] = 38f,
                    ["health"] = 400f,
                    ["armor"] = 25f,
                    ["attack_speed"] = 1.3f,
                    ["hitthetarget"] = 45f,
                    ["DodgeEvade"] = 45f,
                    ["MagicApplication"] = 4.0f,
                    ["MagicShield"] = 18f,
                    ["Fixedwound"] = 15f,
                    ["Restorehealth"] = 18f,       // 回血
                    ["MagicReply"] = 4f,            // 魔力回复
                    ["mana"] = 80f,
                    ["targets"] = 3f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            knight1.action_special_effect += traitAction.grade2_effectAction;
            AssetManager.traits.add(knight1);

            ActorTrait knight2 = new ActorTrait
            {
                id = "knight2",
                path_icon = "trait/knight2",
                needs_to_be_explored = false,
                group_id = "knight",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 52f,
                    ["damage"] = 76f,
                    ["health"] = 800f,
                    ["armor"] = 50f,
                    ["attack_speed"] = 2.6f,
                    ["hitthetarget"] = 75f,
                    ["DodgeEvade"] = 75f,
                    ["MagicApplication"] = 4.0f,
                    ["MagicShield"] = 36f,
                    ["Fixedwound"] = 30f,
                    ["Restorehealth"] = 36f,       // 回血
                    ["MagicReply"] = 8f,            // 魔力回复
                    ["mana"] = 160f,
                    ["targets"] = 6f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            knight2.action_special_effect += traitAction.grade3_effectAction;
            AssetManager.traits.add(knight2);

            ActorTrait knight3 = new ActorTrait
            {
                id = "knight3",
                path_icon = "trait/knight3",
                needs_to_be_explored = false,
                group_id = "knight",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 104f,
                    ["damage"] = 152f,
                    ["health"] = 1600f,
                    ["armor"] = 100f,
                    ["attack_speed"] = 5.2f,
                    ["hitthetarget"] = 105f,
                    ["DodgeEvade"] = 105f,
                    ["MagicApplication"] = 4.0f,
                    ["MagicShield"] = 72f,
                    ["Fixedwound"] = 60f,
                    ["Restorehealth"] = 72f,       // 回血
                    ["MagicReply"] = 16f,           // 魔力回复
                    ["mana"] = 320f,
                    ["targets"] = 12f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            knight3.action_special_effect += traitAction.grade4_effectAction;
            knight3.action_special_effect += (WorldAction)Delegate.Combine(knight3.action_special_effect,
                    new WorldAction(knightSkill.effect_knight3));
            AssetManager.traits.add(knight3);

            ActorTrait knight4 = new ActorTrait
            {
                id = "knight4",
                path_icon = "trait/knight4",
                needs_to_be_explored = false,
                group_id = "knight",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 208f,
                    ["damage"] = 304f,
                    ["health"] = 3200f,
                    ["armor"] = 200f,
                    ["attack_speed"] = 10.4f,
                    ["hitthetarget"] = 135f,
                    ["DodgeEvade"] = 135f,
                    ["MagicApplication"] = 4.0f,
                    ["MagicShield"] = 144f,
                    ["Fixedwound"] = 120f,
                    ["Restorehealth"] = 144f,      // 回血
                    ["MagicReply"] = 32f,           // 魔力回复
                    ["mana"] = 640f,
                    ["targets"] = 24f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            knight4.action_special_effect += traitAction.grade5_effectAction;
            knight4.action_special_effect += (WorldAction)Delegate.Combine(knight4.action_special_effect,
                    new WorldAction(knightSkill.effect_knight4));
            AssetManager.traits.add(knight4);

            ActorTrait knight5 = new ActorTrait
            {
                id = "knight5",
                path_icon = "trait/knight5",
                needs_to_be_explored = false,
                group_id = "knight",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 416f,
                    ["damage"] = 608f,
                    ["health"] = 6400f,
                    ["armor"] = 400f,
                    ["attack_speed"] = 20.8f,
                    ["hitthetarget"] = 165f,
                    ["DodgeEvade"] = 165f,
                    ["MagicApplication"] = 4.0f,
                    ["MagicShield"] = 288f,
                    ["Fixedwound"] = 240f,
                    ["Restorehealth"] = 288f,      // 回血
                    ["MagicReply"] = 64f,           // 魔力回复
                    ["mana"] = 1280f,
                    ["targets"] = 48f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            knight5.action_get_hit += new GetHitAction(knightSkill.knight5_Attack);
            knight5.action_special_effect += traitAction.grade6_effectAction;
            knight5.action_special_effect += (WorldAction)Delegate.Combine(knight5.action_special_effect,
                    new WorldAction(knightSkill.effect_knight5));
            AssetManager.traits.add(knight5);

            ActorTrait knight6 = new ActorTrait
            {
                id = "knight6",
                path_icon = "trait/knight6",
                needs_to_be_explored = false,
                group_id = "knight",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 832f,
                    ["damage"] = 1216f,
                    ["health"] = 12800f,
                    ["armor"] = 800f,
                    ["attack_speed"] = 41.6f,
                    ["hitthetarget"] = 195f,
                    ["DodgeEvade"] = 195f,
                    ["MagicApplication"] = 4.0f,
                    ["MagicShield"] = 576f,
                    ["Fixedwound"] = 480f,
                    ["Restorehealth"] = 576f,      // 回血
                    ["MagicReply"] = 128f,          // 魔力回复
                    ["mana"] = 2560f,
                    ["targets"] = 96f,
                    ["area_of_effect"] = 96f,
                    ["range"] = 96f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            knight6.action_get_hit += new GetHitAction(knightSkill.knight6_Attack);
            knight6.action_special_effect += traitAction.grade7_effectAction;
            knight6.action_special_effect += (WorldAction)Delegate.Combine(knight6.action_special_effect,
                    new WorldAction(knightSkill.effect_knight6));
            AssetManager.traits.add(knight6);

            ActorTrait knight7 = new ActorTrait
            {
                id = "knight7",
                path_icon = "trait/knight7",
                needs_to_be_explored = false,
                group_id = "knight",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 1664f,
                    ["damage"] = 2432f,
                    ["health"] = 25600f,
                    ["armor"] = 1600f,
                    ["attack_speed"] = 83.2f,
                    ["hitthetarget"] = 225f,
                    ["DodgeEvade"] = 225f,
                    ["MagicApplication"] = 8f,
                    ["MagicShield"] = 1152f,
                    ["Fixedwound"] = 960f,
                    ["Restorehealth"] = 1152f,     // 回血
                    ["MagicReply"] = 256f,          // 魔力回复
                    ["mana"] = 5120f,
                    ["targets"] = 192f,
                    ["area_of_effect"] = 192f,
                    ["range"] = 192f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            knight7.action_get_hit += new GetHitAction(knightSkill.knight7_Attack);
            knight7.action_special_effect += (WorldAction)Delegate.Combine(knight7.action_special_effect,
                    new WorldAction(knightSkill.effect_knight7));
            knight7.action_special_effect += (WorldAction)Delegate.Combine(knight7.action_special_effect,
                    new WorldAction(knightSkill.Trial_knight7));
            AssetManager.traits.add(knight7);

            // 战士 ⚔️（狂战士）
            ActorTrait valiantgeneral1 = new ActorTrait
            {
                id = "valiantgeneral1",
                path_icon = "trait/valiantgeneral1",
                needs_to_be_explored = false,
                group_id = "valiantgeneral",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 22f,
                    ["damage"] = 42f,
                    ["health"] = 280f,
                    ["armor"] = 20f,
                    ["attack_speed"] = 1.4f,
                    ["hitthetarget"] = 55f,
                    ["DodgeEvade"] = 50f,
                    ["MagicApplication"] = 2.5f,
                    ["MagicShield"] = 12f,
                    ["Fixedwound"] = 12f,
                    ["Restorehealth"] = 16f,       // 回血
                    ["MagicReply"] = 2f,            // 魔力回复
                    ["mana"] = 60f,
                    ["targets"] = 2f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            valiantgeneral1.action_special_effect += traitAction.grade2_effectAction;
            AssetManager.traits.add(valiantgeneral1);

            ActorTrait valiantgeneral2 = new ActorTrait
            {
                id = "valiantgeneral2",
                path_icon = "trait/valiantgeneral2",
                needs_to_be_explored = false,
                group_id = "valiantgeneral",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 44f,
                    ["damage"] = 84f,
                    ["health"] = 560f,
                    ["armor"] = 40f,
                    ["attack_speed"] = 2.8f,
                    ["hitthetarget"] = 85f,
                    ["DodgeEvade"] = 80f,
                    ["MagicApplication"] = 2.5f,
                    ["MagicShield"] = 24f,
                    ["Fixedwound"] = 24f,
                    ["Restorehealth"] = 32f,       // 回血
                    ["MagicReply"] = 4f,            // 魔力回复
                    ["mana"] = 120f,
                    ["targets"] = 4f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            valiantgeneral2.action_special_effect += traitAction.grade3_effectAction;
            AssetManager.traits.add(valiantgeneral2);

            ActorTrait valiantgeneral3 = new ActorTrait
            {
                id = "valiantgeneral3",
                path_icon = "trait/valiantgeneral3",
                needs_to_be_explored = false,
                group_id = "valiantgeneral",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 88f,
                    ["damage"] = 168f,
                    ["health"] = 1120f,
                    ["armor"] = 80f,
                    ["attack_speed"] = 5.6f,
                    ["hitthetarget"] = 115f,
                    ["DodgeEvade"] = 110f,
                    ["MagicApplication"] = 2.5f,
                    ["MagicShield"] = 48f,
                    ["Fixedwound"] = 48f,
                    ["Restorehealth"] = 64f,       // 回血
                    ["MagicReply"] = 8f,            // 魔力回复
                    ["mana"] = 240f,
                    ["targets"] = 8f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            valiantgeneral3.action_attack_target += new AttackAction((ValiantGeneralSkill.attack_valiantgeneral3));
            valiantgeneral3.action_special_effect += traitAction.grade4_effectAction;
            AssetManager.traits.add(valiantgeneral3);

            ActorTrait valiantgeneral4 = new ActorTrait
            {
                id = "valiantgeneral4",
                path_icon = "trait/valiantgeneral4",
                needs_to_be_explored = false,
                group_id = "valiantgeneral",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 176f,
                    ["damage"] = 336f,
                    ["health"] = 2240f,
                    ["armor"] = 160f,
                    ["attack_speed"] = 11.2f,
                    ["hitthetarget"] = 145f,
                    ["DodgeEvade"] = 140f,
                    ["MagicApplication"] = 2.5f,
                    ["MagicShield"] = 96f,
                    ["Fixedwound"] = 96f,
                    ["Restorehealth"] = 128f,      // 回血
                    ["MagicReply"] = 16f,           // 魔力回复
                    ["mana"] = 480f,
                    ["targets"] = 16f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            valiantgeneral4.action_attack_target += new AttackAction((ValiantGeneralSkill.attack_valiantgeneral4));
            valiantgeneral4.action_special_effect += traitAction.grade5_effectAction;
            AssetManager.traits.add(valiantgeneral4);

            ActorTrait valiantgeneral5 = new ActorTrait
            {
                id = "valiantgeneral5",
                path_icon = "trait/valiantgeneral5",
                needs_to_be_explored = false,
                group_id = "valiantgeneral",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 352f,
                    ["damage"] = 672f,
                    ["health"] = 4480f,
                    ["armor"] = 320f,
                    ["attack_speed"] = 22.4f,
                    ["hitthetarget"] = 175f,
                    ["DodgeEvade"] = 170f,
                    ["MagicApplication"] = 2.5f,
                    ["MagicShield"] = 192f,
                    ["Fixedwound"] = 192f,
                    ["Restorehealth"] = 256f,      // 回血
                    ["MagicReply"] = 32f,           // 魔力回复
                    ["mana"] = 960f,
                    ["targets"] = 32f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            valiantgeneral5.action_attack_target += new AttackAction((ValiantGeneralSkill.attack_valiantgeneral5));
            valiantgeneral5.action_special_effect += traitAction.grade6_effectAction;
            AssetManager.traits.add(valiantgeneral5);

            ActorTrait valiantgeneral6 = new ActorTrait
            {
                id = "valiantgeneral6",
                path_icon = "trait/valiantgeneral6",
                needs_to_be_explored = false,
                group_id = "valiantgeneral",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 704f,
                    ["damage"] = 1344f,
                    ["health"] = 8960f,
                    ["armor"] = 640f,
                    ["attack_speed"] = 44.8f,
                    ["hitthetarget"] = 205f,
                    ["DodgeEvade"] = 200f,
                    ["MagicApplication"] = 2.5f,
                    ["MagicShield"] = 384f,
                    ["Fixedwound"] = 384f,
                    ["Restorehealth"] = 512f,      // 回血
                    ["MagicReply"] = 64f,           // 魔力回复
                    ["mana"] = 1920f,
                    ["targets"] = 64f,
                    ["area_of_effect"] = 96f,
                    ["range"] = 96f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            valiantgeneral6.action_attack_target += new AttackAction((ValiantGeneralSkill.attack_valiantgeneral6));
            valiantgeneral6.action_special_effect += traitAction.grade7_effectAction;
            AssetManager.traits.add(valiantgeneral6);

            ActorTrait valiantgeneral7 = new ActorTrait
            {
                id = "valiantgeneral7",
                path_icon = "trait/valiantgeneral7",
                needs_to_be_explored = false,
                group_id = "valiantgeneral",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 1408f,
                    ["damage"] = 2688f,
                    ["health"] = 17920f,
                    ["armor"] = 1280f,
                    ["attack_speed"] = 89.6f,
                    ["hitthetarget"] = 235f,
                    ["DodgeEvade"] = 230f,
                    ["MagicApplication"] = 5f,
                    ["MagicShield"] = 768f,
                    ["Fixedwound"] = 768f,
                    ["Restorehealth"] = 1024f,     // 回血
                    ["MagicReply"] = 128f,          // 魔力回复
                    ["mana"] = 3840f,
                    ["targets"] = 128f,
                    ["area_of_effect"] = 192f,
                    ["range"] = 192f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            valiantgeneral7.action_attack_target += new AttackAction((ValiantGeneralSkill.attack_valiantgeneral7));
            AssetManager.traits.add(valiantgeneral7);

            // 游侠 🏹（神射手）
            ActorTrait Ranger1 = new ActorTrait
            {
                id = "Ranger1",
                path_icon = "trait/Ranger1",
                needs_to_be_explored = false,
                group_id = "Ranger",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 28f,
                    ["damage"] = 40f,
                    ["health"] = 140f,
                    ["armor"] = 12f,
                    ["attack_speed"] = 1.6f,
                    ["hitthetarget"] = 75f,
                    ["DodgeEvade"] = 85f,
                    ["MagicApplication"] = 4.0f,
                    ["MagicShield"] = 6f,
                    ["Fixedwound"] = 18f,
                    ["Restorehealth"] = 6f,        // 回血
                    ["MagicReply"] = 6f,            // 魔力回复
                    ["mana"] = 70f,
                    ["targets"] = 3f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Ranger1.action_special_effect += traitAction.grade2_effectAction;
            AssetManager.traits.add(Ranger1);

            ActorTrait Ranger2 = new ActorTrait
            {
                id = "Ranger2",
                path_icon = "trait/Ranger2",
                needs_to_be_explored = false,
                group_id = "Ranger",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 56f,
                    ["damage"] = 80f,
                    ["health"] = 280f,
                    ["armor"] = 24f,
                    ["attack_speed"] = 3.2f,
                    ["hitthetarget"] = 105f,
                    ["DodgeEvade"] = 115f,
                    ["MagicApplication"] = 4.0f,
                    ["MagicShield"] = 12f,
                    ["Fixedwound"] = 36f,
                    ["Restorehealth"] = 12f,       // 回血
                    ["MagicReply"] = 12f,           // 魔力回复
                    ["mana"] = 140f,
                    ["targets"] = 6f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Ranger2.action_special_effect += traitAction.grade3_effectAction;
            AssetManager.traits.add(Ranger2);

            ActorTrait Ranger3 = new ActorTrait
            {
                id = "Ranger3",
                path_icon = "trait/Ranger3",
                needs_to_be_explored = false,
                group_id = "Ranger",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 112f,
                    ["damage"] = 160f,
                    ["health"] = 560f,
                    ["armor"] = 48f,
                    ["attack_speed"] = 6.4f,
                    ["hitthetarget"] = 135f,
                    ["DodgeEvade"] = 145f,
                    ["MagicApplication"] = 4.0f,
                    ["MagicShield"] = 24f,
                    ["Fixedwound"] = 72f,
                    ["Restorehealth"] = 24f,       // 回血
                    ["MagicReply"] = 24f,           // 魔力回复
                    ["mana"] = 280f,
                    ["targets"] = 12f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Ranger3.action_attack_target += new AttackAction((RangerSkill.attack_Ranger3));
            Ranger3.action_special_effect += traitAction.grade4_effectAction;
            AssetManager.traits.add(Ranger3);

            ActorTrait Ranger4 = new ActorTrait
            {
                id = "Ranger4",
                path_icon = "trait/Ranger4",
                needs_to_be_explored = false,
                group_id = "Ranger",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 224f,
                    ["damage"] = 320f,
                    ["health"] = 1120f,
                    ["armor"] = 96f,
                    ["attack_speed"] = 12.8f,
                    ["hitthetarget"] = 165f,
                    ["DodgeEvade"] = 175f,
                    ["MagicApplication"] = 4.0f,
                    ["MagicShield"] = 48f,
                    ["Fixedwound"] = 144f,
                    ["Restorehealth"] = 48f,       // 回血
                    ["MagicReply"] = 48f,           // 魔力回复
                    ["mana"] = 560f,
                    ["targets"] = 24f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Ranger4.action_attack_target += new AttackAction((RangerSkill.attack_Ranger4));
            Ranger4.action_special_effect += traitAction.grade5_effectAction;
            AssetManager.traits.add(Ranger4);

            ActorTrait Ranger5 = new ActorTrait
            {
                id = "Ranger5",
                path_icon = "trait/Ranger5",
                needs_to_be_explored = false,
                group_id = "Ranger",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 448f,
                    ["damage"] = 640f,
                    ["health"] = 2240f,
                    ["armor"] = 192f,
                    ["attack_speed"] = 25.6f,
                    ["hitthetarget"] = 195f,
                    ["DodgeEvade"] = 205f,
                    ["MagicApplication"] = 4.0f,
                    ["MagicShield"] = 96f,
                    ["Fixedwound"] = 288f,
                    ["Restorehealth"] = 96f,       // 回血
                    ["MagicReply"] = 96f,           // 魔力回复
                    ["mana"] = 1120f,
                    ["targets"] = 48f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Ranger5.action_attack_target += new AttackAction((RangerSkill.attack_Ranger5));
            Ranger5.action_special_effect += traitAction.grade6_effectAction;
            AssetManager.traits.add(Ranger5);

            ActorTrait Ranger6 = new ActorTrait
            {
                id = "Ranger6",
                path_icon = "trait/Ranger6",
                needs_to_be_explored = false,
                group_id = "Ranger",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 896f,
                    ["damage"] = 1280f,
                    ["health"] = 4480f,
                    ["armor"] = 384f,
                    ["attack_speed"] = 51.2f,
                    ["hitthetarget"] = 225f,
                    ["DodgeEvade"] = 235f,
                    ["MagicApplication"] = 4.0f,
                    ["MagicShield"] = 192f,
                    ["Fixedwound"] = 576f,
                    ["Restorehealth"] = 192f,      // 回血
                    ["MagicReply"] = 192f,          // 魔力回复
                    ["mana"] = 2240f,
                    ["targets"] = 96f,
                    ["area_of_effect"] = 96f,
                    ["range"] = 96f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Ranger6.action_attack_target += new AttackAction((RangerSkill.attack_Ranger6));
            Ranger6.action_special_effect += traitAction.grade7_effectAction;
            AssetManager.traits.add(Ranger6);

            ActorTrait Ranger7 = new ActorTrait
            {
                id = "Ranger7",
                path_icon = "trait/Ranger7",
                needs_to_be_explored = false,
                group_id = "Ranger",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 1792f,
                    ["damage"] = 2560f,
                    ["health"] = 8960f,
                    ["armor"] = 768f,
                    ["attack_speed"] = 102.4f,
                    ["hitthetarget"] = 255f,
                    ["DodgeEvade"] = 265f,
                    ["MagicApplication"] = 8f,
                    ["MagicShield"] = 384f,
                    ["Fixedwound"] = 1152f,
                    ["Restorehealth"] = 384f,      // 回血
                    ["MagicReply"] = 384f,          // 魔力回复
                    ["mana"] = 4480f,
                    ["targets"] = 192f,
                    ["area_of_effect"] = 192f,
                    ["range"] = 192f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Ranger7.action_attack_target += new AttackAction((RangerSkill.attack_Ranger7));
            AssetManager.traits.add(Ranger7);

            // 刺客 🗡️（影舞者）
            ActorTrait Assassin1 = new ActorTrait
            {
                id = "Assassin1",
                path_icon = "trait/Assassin1",
                needs_to_be_explored = false,
                group_id = "Assassin",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 16f,
                    ["damage"] = 65f,
                    ["health"] = 100f,
                    ["armor"] = 8f,
                    ["attack_speed"] = 1.5f,
                    ["hitthetarget"] = 65f,
                    ["DodgeEvade"] = 130f,
                    ["MagicApplication"] = 3.5f,
                    ["MagicShield"] = 4f,
                    ["Fixedwound"] = 25f,
                    ["Restorehealth"] = 2f,        // 回血
                    ["MagicReply"] = 4f,            // 魔力回复
                    ["mana"] = 40f,
                    ["targets"] = 1f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Assassin1.action_special_effect += traitAction.grade2_effectAction;
            AssetManager.traits.add(Assassin1);

            ActorTrait Assassin2 = new ActorTrait
            {
                id = "Assassin2",
                path_icon = "trait/Assassin2",
                needs_to_be_explored = false,
                group_id = "Assassin",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 32f,
                    ["damage"] = 130f,
                    ["health"] = 200f,
                    ["armor"] = 16f,
                    ["attack_speed"] = 3.0f,
                    ["hitthetarget"] = 95f,
                    ["DodgeEvade"] = 160f,
                    ["MagicApplication"] = 3.5f,
                    ["MagicShield"] = 8f,
                    ["Fixedwound"] = 50f,
                    ["Restorehealth"] = 4f,        // 回血
                    ["MagicReply"] = 8f,            // 魔力回复
                    ["mana"] = 80f,
                    ["targets"] = 2f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Assassin2.action_special_effect += traitAction.grade3_effectAction;
            AssetManager.traits.add(Assassin2);

            ActorTrait Assassin3 = new ActorTrait
            {
                id = "Assassin3",
                path_icon = "trait/Assassin3",
                needs_to_be_explored = false,
                group_id = "Assassin",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 64f,
                    ["damage"] = 260f,
                    ["health"] = 400f,
                    ["armor"] = 32f,
                    ["attack_speed"] = 6.0f,
                    ["hitthetarget"] = 125f,
                    ["DodgeEvade"] = 190f,
                    ["MagicApplication"] = 3.5f,
                    ["MagicShield"] = 16f,
                    ["Fixedwound"] = 100f,
                    ["Restorehealth"] = 8f,        // 回血
                    ["MagicReply"] = 16f,           // 魔力回复
                    ["mana"] = 160f,
                    ["targets"] = 4f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Assassin3.action_attack_target += new AttackAction((AssassinSkill.attack_Assassin3));
            Assassin3.action_special_effect += traitAction.grade4_effectAction;
            AssetManager.traits.add(Assassin3);

            ActorTrait Assassin4 = new ActorTrait
            {
                id = "Assassin4",
                path_icon = "trait/Assassin4",
                needs_to_be_explored = false,
                group_id = "Assassin",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 128f,
                    ["damage"] = 520f,
                    ["health"] = 800f,
                    ["armor"] = 64f,
                    ["attack_speed"] = 12.0f,
                    ["hitthetarget"] = 155f,
                    ["DodgeEvade"] = 220f,
                    ["MagicApplication"] = 3.5f,
                    ["MagicShield"] = 32f,
                    ["Fixedwound"] = 200f,
                    ["Restorehealth"] = 16f,      // 回血
                    ["MagicReply"] = 32f,           // 魔力回复
                    ["mana"] = 320f,
                    ["targets"] = 8f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Assassin4.action_attack_target += new AttackAction((AssassinSkill.attack_Assassin4));
            Assassin4.action_special_effect += traitAction.grade5_effectAction;
            AssetManager.traits.add(Assassin4);

            ActorTrait Assassin5 = new ActorTrait
            {
                id = "Assassin5",
                path_icon = "trait/Assassin5",
                needs_to_be_explored = false,
                group_id = "Assassin",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 256f,
                    ["damage"] = 1040f,
                    ["health"] = 1600f,
                    ["armor"] = 128f,
                    ["attack_speed"] = 24.0f,
                    ["hitthetarget"] = 185f,
                    ["DodgeEvade"] = 250f,
                    ["MagicApplication"] = 3.5f,
                    ["MagicShield"] = 64f,
                    ["Fixedwound"] = 400f,
                    ["Restorehealth"] = 32f,      // 回血
                    ["MagicReply"] = 64f,           // 魔力回复
                    ["mana"] = 640f,
                    ["targets"] = 16f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Assassin5.action_attack_target += new AttackAction((AssassinSkill.attack_Assassin5));
            Assassin5.action_special_effect += traitAction.grade6_effectAction;
            AssetManager.traits.add(Assassin5);

            ActorTrait Assassin6 = new ActorTrait
            {
                id = "Assassin6",
                path_icon = "trait/Assassin6",
                needs_to_be_explored = false,
                group_id = "Assassin",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 512f,
                    ["damage"] = 2080f,
                    ["health"] = 3200f,
                    ["armor"] = 256f,
                    ["attack_speed"] = 48.0f,
                    ["hitthetarget"] = 215f,
                    ["DodgeEvade"] = 280f,
                    ["MagicApplication"] = 3.5f,
                    ["MagicShield"] = 128f,
                    ["Fixedwound"] = 800f,
                    ["Restorehealth"] = 64f,      // 回血
                    ["MagicReply"] = 128f,          // 魔力回复
                    ["mana"] = 1280f,
                    ["targets"] = 32f,
                    ["area_of_effect"] = 96f,
                    ["range"] = 96f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Assassin6.action_attack_target += new AttackAction((AssassinSkill.attack_Assassin6));
            Assassin6.action_special_effect += traitAction.grade7_effectAction;
            AssetManager.traits.add(Assassin6);

            ActorTrait Assassin7 = new ActorTrait
            {
                id = "Assassin7",
                path_icon = "trait/Assassin7",
                needs_to_be_explored = false,
                group_id = "Assassin",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 1024f,
                    ["damage"] = 4160f,
                    ["health"] = 6400f,
                    ["armor"] = 512f,
                    ["attack_speed"] = 96.0f,
                    ["hitthetarget"] = 245f,
                    ["DodgeEvade"] = 310f,
                    ["MagicApplication"] = 7f,
                    ["MagicShield"] = 256f,
                    ["Fixedwound"] = 1600f,
                    ["Restorehealth"] = 128f,     // 回血
                    ["MagicReply"] = 256f,          // 魔力回复
                    ["mana"] = 2560f,
                    ["targets"] = 64f,
                    ["area_of_effect"] = 192f,
                    ["range"] = 192f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Assassin7.action_attack_target += new AttackAction((AssassinSkill.attack_Assassin7));
            Assassin7.action_special_effect += (WorldAction)Delegate.Combine(Assassin7.action_special_effect,
                    new WorldAction(AssassinSkill.Assassin7_Regen));
            AssetManager.traits.add(Assassin7);

            // 召唤师 🌟（契约使者）
            ActorTrait Summoner1 = new ActorTrait
            {
                id = "Summoner1",
                path_icon = "trait/Summoner1",
                needs_to_be_explored = false,
                group_id = "Summoner",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 20f,
                    ["damage"] = 15f,
                    ["health"] = 150f,
                    ["armor"] = 12f,
                    ["attack_speed"] = 1.0f,
                    ["hitthetarget"] = 45f,
                    ["DodgeEvade"] = 60f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 7f,
                    ["Fixedwound"] = 12f,
                    ["Restorehealth"] = 8f,        // 回血
                    ["MagicReply"] = 9f,            // 魔力回复
                    ["mana"] = 180f,
                    ["targets"] = 5f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Summoner1.action_special_effect += traitAction.grade2_effectAction;
            AssetManager.traits.add(Summoner1);

            ActorTrait Summoner2 = new ActorTrait
            {
                id = "Summoner2",
                path_icon = "trait/Summoner2",
                needs_to_be_explored = false,
                group_id = "Summoner",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 40f,
                    ["damage"] = 30f,
                    ["health"] = 300f,
                    ["armor"] = 24f,
                    ["attack_speed"] = 2.0f,
                    ["hitthetarget"] = 75f,
                    ["DodgeEvade"] = 90f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 14f,
                    ["Fixedwound"] = 24f,
                    ["Restorehealth"] = 16f,      // 回血
                    ["MagicReply"] = 18f,           // 魔力回复
                    ["mana"] = 360f,
                    ["targets"] = 10f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Summoner2.action_special_effect += traitAction.grade3_effectAction;
            AssetManager.traits.add(Summoner2);

            ActorTrait Summoner3 = new ActorTrait
            {
                id = "Summoner3",
                path_icon = "trait/Summoner3",
                needs_to_be_explored = false,
                group_id = "Summoner",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 80f,
                    ["damage"] = 60f,
                    ["health"] = 600f,
                    ["armor"] = 48f,
                    ["attack_speed"] = 4.0f,
                    ["hitthetarget"] = 105f,
                    ["DodgeEvade"] = 120f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 28f,
                    ["Fixedwound"] = 48f,
                    ["Restorehealth"] = 32f,      // 回血
                    ["MagicReply"] = 36f,           // 魔力回复
                    ["mana"] = 720f,
                    ["targets"] = 20f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Summoner3.action_special_effect += traitAction.grade4_effectAction;
            Summoner3.action_special_effect += (WorldAction)Delegate.Combine(Summoner3.action_special_effect,
                new WorldAction(SummonerSkill.effect_summoner3));
            AssetManager.traits.add(Summoner3);

            ActorTrait Summoner4 = new ActorTrait
            {
                id = "Summoner4",
                path_icon = "trait/Summoner4",
                needs_to_be_explored = false,
                group_id = "Summoner",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 160f,
                    ["damage"] = 120f,
                    ["health"] = 1200f,
                    ["armor"] = 96f,
                    ["attack_speed"] = 8.0f,
                    ["hitthetarget"] = 135f,
                    ["DodgeEvade"] = 150f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 56f,
                    ["Fixedwound"] = 96f,
                    ["Restorehealth"] = 64f,      // 回血
                    ["MagicReply"] = 72f,           // 魔力回复
                    ["mana"] = 1440f,
                    ["targets"] = 40f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Summoner4.action_special_effect += traitAction.grade5_effectAction;
            Summoner4.action_special_effect += (WorldAction)Delegate.Combine(Summoner4.action_special_effect,
                new WorldAction(SummonerSkill.effect_summoner4));
            AssetManager.traits.add(Summoner4);

            ActorTrait Summoner5 = new ActorTrait
            {
                id = "Summoner5",
                path_icon = "trait/Summoner5",
                needs_to_be_explored = false,
                group_id = "Summoner",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 320f,
                    ["damage"] = 240f,
                    ["health"] = 2400f,
                    ["armor"] = 192f,
                    ["attack_speed"] = 16.0f,
                    ["hitthetarget"] = 165f,
                    ["DodgeEvade"] = 180f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 112f,
                    ["Fixedwound"] = 192f,
                    ["Restorehealth"] = 128f,     // 回血
                    ["MagicReply"] = 144f,          // 魔力回复
                    ["mana"] = 2880f,
                    ["targets"] = 80f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Summoner5.action_special_effect += traitAction.grade6_effectAction;
            Summoner5.action_special_effect += (WorldAction)Delegate.Combine(Summoner5.action_special_effect,
                new WorldAction(SummonerSkill.effect_summoner5));
            AssetManager.traits.add(Summoner5);

            ActorTrait Summoner6 = new ActorTrait
            {
                id = "Summoner6",
                path_icon = "trait/Summoner6",
                needs_to_be_explored = false,
                group_id = "Summoner",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 640f,
                    ["damage"] = 480f,
                    ["health"] = 4800f,
                    ["armor"] = 384f,
                    ["attack_speed"] = 32.0f,
                    ["hitthetarget"] = 195f,
                    ["DodgeEvade"] = 210f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 224f,
                    ["Fixedwound"] = 384f,
                    ["Restorehealth"] = 256f,     // 回血
                    ["MagicReply"] = 288f,          // 魔力回复
                    ["mana"] = 5760f,
                    ["targets"] = 160f,
                    ["area_of_effect"] = 96f,
                    ["range"] = 96f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Summoner6.action_special_effect += traitAction.grade7_effectAction;
            Summoner6.action_special_effect += (WorldAction)Delegate.Combine(Summoner6.action_special_effect,
                new WorldAction(SummonerSkill.effect_summoner6));
            AssetManager.traits.add(Summoner6);

            ActorTrait Summoner7 = new ActorTrait
            {
                id = "Summoner7",
                path_icon = "trait/Summoner7",
                needs_to_be_explored = false,
                group_id = "Summoner",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 1280f,
                    ["damage"] = 960f,
                    ["health"] = 9600f,
                    ["armor"] = 768f,
                    ["attack_speed"] = 64.0f,
                    ["hitthetarget"] = 225f,
                    ["DodgeEvade"] = 240f,
                    ["MagicApplication"] = 10f,
                    ["MagicShield"] = 448f,
                    ["Fixedwound"] = 768f,
                    ["Restorehealth"] = 512f,     // 回血
                    ["MagicReply"] = 576f,          // 魔力回复
                    ["mana"] = 11520f,
                    ["targets"] = 320f,
                    ["area_of_effect"] = 192f,
                    ["range"] = 192f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Summoner7.action_special_effect += (WorldAction)Delegate.Combine(Summoner7.action_special_effect,
                new WorldAction(SummonerSkill.effect_summoner7));
            AssetManager.traits.add(Summoner7);

            // 吟游诗人 🎵（旋律大师）
            ActorTrait minstrel1 = new ActorTrait
            {
                id = "minstrel1",
                path_icon = "trait/minstrel1",
                needs_to_be_explored = false,
                group_id = "minstrel",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 30f,
                    ["damage"] = 22f,
                    ["health"] = 160f,
                    ["armor"] = 14f,
                    ["attack_speed"] = 1.1f,
                    ["hitthetarget"] = 55f,
                    ["DodgeEvade"] = 70f,
                    ["MagicApplication"] = 5.5f,
                    ["MagicShield"] = 12f,
                    ["Fixedwound"] = 18f,
                    ["Restorehealth"] = 12f,       // 回血
                    ["MagicReply"] = 7f,            // 魔力回复
                    ["mana"] = 130f,
                    ["targets"] = 4f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            minstrel1.action_special_effect += traitAction.grade2_effectAction;
            AssetManager.traits.add(minstrel1);

            ActorTrait minstrel2 = new ActorTrait
            {
                id = "minstrel2",
                path_icon = "trait/minstrel2",
                needs_to_be_explored = false,
                group_id = "minstrel",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 60f,
                    ["damage"] = 44f,
                    ["health"] = 320f,
                    ["armor"] = 28f,
                    ["attack_speed"] = 2.2f,
                    ["hitthetarget"] = 85f,
                    ["DodgeEvade"] = 100f,
                    ["MagicApplication"] = 5.5f,
                    ["MagicShield"] = 24f,
                    ["Fixedwound"] = 36f,
                    ["Restorehealth"] = 24f,       // 回血
                    ["MagicReply"] = 14f,           // 魔力回复
                    ["mana"] = 260f,
                    ["targets"] = 8f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            minstrel2.action_special_effect += traitAction.grade3_effectAction;
            AssetManager.traits.add(minstrel2);

            ActorTrait minstrel3 = new ActorTrait
            {
                id = "minstrel3",
                path_icon = "trait/minstrel3",
                needs_to_be_explored = false,
                group_id = "minstrel",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 120f,
                    ["damage"] = 88f,
                    ["health"] = 640f,
                    ["armor"] = 56f,
                    ["attack_speed"] = 4.4f,
                    ["hitthetarget"] = 115f,
                    ["DodgeEvade"] = 130f,
                    ["MagicApplication"] = 5.5f,
                    ["MagicShield"] = 48f,
                    ["Fixedwound"] = 72f,
                    ["Restorehealth"] = 48f,       // 回血
                    ["MagicReply"] = 28f,           // 魔力回复
                    ["mana"] = 520f,
                    ["targets"] = 16f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            minstrel3.action_special_effect += traitAction.grade4_effectAction;
            minstrel3.action_special_effect += (WorldAction)Delegate.Combine(minstrel3.action_special_effect,
                    new WorldAction(minstrelSkill.effect_minstrel3));
            AssetManager.traits.add(minstrel3);

            ActorTrait minstrel4 = new ActorTrait
            {
                id = "minstrel4",
                path_icon = "trait/minstrel4",
                needs_to_be_explored = false,
                group_id = "minstrel",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 240f,
                    ["damage"] = 176f,
                    ["health"] = 1280f,
                    ["armor"] = 112f,
                    ["attack_speed"] = 8.8f,
                    ["hitthetarget"] = 145f,
                    ["DodgeEvade"] = 160f,
                    ["MagicApplication"] = 5.5f,
                    ["MagicShield"] = 96f,
                    ["Fixedwound"] = 144f,
                    ["Restorehealth"] = 96f,       // 回血
                    ["MagicReply"] = 56f,           // 魔力回复
                    ["mana"] = 1040f,
                    ["targets"] = 32f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            minstrel4.action_special_effect += traitAction.grade5_effectAction;
            minstrel4.action_special_effect += (WorldAction)Delegate.Combine(minstrel4.action_special_effect,
                    new WorldAction(minstrelSkill.effect_minstrel4));
            AssetManager.traits.add(minstrel4);

            ActorTrait minstrel5 = new ActorTrait
            {
                id = "minstrel5",
                path_icon = "trait/minstrel5",
                needs_to_be_explored = false,
                group_id = "minstrel",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 480f,
                    ["damage"] = 352f,
                    ["health"] = 2560f,
                    ["armor"] = 224f,
                    ["attack_speed"] = 17.6f,
                    ["hitthetarget"] = 175f,
                    ["DodgeEvade"] = 190f,
                    ["MagicApplication"] = 5.5f,
                    ["MagicShield"] = 192f,
                    ["Fixedwound"] = 288f,
                    ["Restorehealth"] = 192f,      // 回血
                    ["MagicReply"] = 112f,          // 魔力回复
                    ["mana"] = 2080f,
                    ["targets"] = 64f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            minstrel5.action_special_effect += traitAction.grade6_effectAction;
            minstrel5.action_special_effect += (WorldAction)Delegate.Combine(minstrel5.action_special_effect,
                    new WorldAction(minstrelSkill.effect_minstrel5));
            AssetManager.traits.add(minstrel5);

            ActorTrait minstrel6 = new ActorTrait
            {
                id = "minstrel6",
                path_icon = "trait/minstrel6",
                needs_to_be_explored = false,
                group_id = "minstrel",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 960f,
                    ["damage"] = 704f,
                    ["health"] = 5120f,
                    ["armor"] = 448f,
                    ["attack_speed"] = 35.2f,
                    ["hitthetarget"] = 205f,
                    ["DodgeEvade"] = 220f,
                    ["MagicApplication"] = 5.5f,
                    ["MagicShield"] = 384f,
                    ["Fixedwound"] = 576f,
                    ["Restorehealth"] = 384f,      // 回血
                    ["MagicReply"] = 224f,          // 魔力回复
                    ["mana"] = 4160f,
                    ["targets"] = 128f,
                    ["area_of_effect"] = 96f,
                    ["range"] = 96f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            minstrel6.action_special_effect += traitAction.grade7_effectAction;
            minstrel6.action_special_effect += (WorldAction)Delegate.Combine(minstrel6.action_special_effect,
                    new WorldAction(minstrelSkill.effect_minstrel6));
            AssetManager.traits.add(minstrel6);

            ActorTrait minstrel7 = new ActorTrait
            {
                id = "minstrel7",
                path_icon = "trait/minstrel7",
                needs_to_be_explored = false,
                group_id = "minstrel",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 1920f,
                    ["damage"] = 1408f,
                    ["health"] = 10240f,
                    ["armor"] = 896f,
                    ["attack_speed"] = 70.4f,
                    ["hitthetarget"] = 235f,
                    ["DodgeEvade"] = 250f,
                    ["MagicApplication"] = 11.0f,
                    ["MagicShield"] = 768f,
                    ["Fixedwound"] = 1152f,
                    ["Restorehealth"] = 768f,      // 回血
                    ["MagicReply"] = 448f,          // 魔力回复
                    ["mana"] = 8320f,
                    ["targets"] = 256f,
                    ["area_of_effect"] = 192f,
                    ["range"] = 192f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            minstrel7.action_attack_target += new AttackAction((minstrelSkill.attack_minstrel7));
            minstrel7.action_special_effect += (WorldAction)Delegate.Combine(minstrel7.action_special_effect,
                    new WorldAction(minstrelSkill.effect_minstrel7));
            AssetManager.traits.add(minstrel7);

            // 咒术师 ☠️（诅咒大师）
            ActorTrait warlock1 = new ActorTrait
            {
                id = "warlock1",
                path_icon = "trait/warlock1",
                needs_to_be_explored = false,
                group_id = "warlock",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 18f,
                    ["damage"] = 32f,
                    ["health"] = 110f,
                    ["armor"] = 9f,
                    ["attack_speed"] = 1.2f,
                    ["hitthetarget"] = 70f,
                    ["DodgeEvade"] = 40f,
                    ["MagicApplication"] = 7.0f,
                    ["MagicShield"] = 15f,
                    ["Fixedwound"] = 35f,
                    ["Restorehealth"] = 2f,       // 回血
                    ["MagicReply"] = 5f,            // 魔力回复
                    ["mana"] = 210f,
                    ["targets"] = 2f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            warlock1.action_special_effect += traitAction.grade2_effectAction;
            AssetManager.traits.add(warlock1);

            ActorTrait warlock2 = new ActorTrait
            {
                id = "warlock2",
                path_icon = "trait/warlock2",
                needs_to_be_explored = false,
                group_id = "warlock",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 36f,
                    ["damage"] = 64f,
                    ["health"] = 220f,
                    ["armor"] = 18f,
                    ["attack_speed"] = 2.4f,
                    ["hitthetarget"] = 100f,
                    ["DodgeEvade"] = 70f,
                    ["MagicApplication"] = 7.0f,
                    ["MagicShield"] = 30f,
                    ["Fixedwound"] = 70f,
                    ["Restorehealth"] = 4f,       // 回血
                    ["MagicReply"] = 10f,           // 魔力回复
                    ["mana"] = 420f,
                    ["targets"] = 4f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            warlock2.action_special_effect += traitAction.grade3_effectAction;
            AssetManager.traits.add(warlock2);

            ActorTrait warlock3 = new ActorTrait
            {
                id = "warlock3",
                path_icon = "trait/warlock3",
                needs_to_be_explored = false,
                group_id = "warlock",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 72f,
                    ["damage"] = 128f,
                    ["health"] = 440f,
                    ["armor"] = 36f,
                    ["attack_speed"] = 4.8f,
                    ["hitthetarget"] = 130f,
                    ["DodgeEvade"] = 100f,
                    ["MagicApplication"] = 7.0f,
                    ["MagicShield"] = 60f,
                    ["Fixedwound"] = 140f,
                    ["Restorehealth"] = 8f,       // 回血
                    ["MagicReply"] = 20f,           // 魔力回复
                    ["mana"] = 840f,
                    ["targets"] = 8f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            warlock3.action_special_effect += traitAction.grade4_effectAction;
            warlock3.action_special_effect += (WorldAction)Delegate.Combine(warlock3.action_special_effect,
                    new WorldAction(warlockSkill.effect_warlock3));
            AssetManager.traits.add(warlock3);

            ActorTrait warlock4 = new ActorTrait
            {
                id = "warlock4",
                path_icon = "trait/warlock4",
                needs_to_be_explored = false,
                group_id = "warlock",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 144f,
                    ["damage"] = 256f,
                    ["health"] = 880f,
                    ["armor"] = 72f,
                    ["attack_speed"] = 9.6f,
                    ["hitthetarget"] = 160f,
                    ["DodgeEvade"] = 130f,
                    ["MagicApplication"] = 7.0f,
                    ["MagicShield"] = 120f,
                    ["Fixedwound"] = 280f,
                    ["Restorehealth"] = 16f,      // 回血
                    ["MagicReply"] = 40f,           // 魔力回复
                    ["mana"] = 1680f,
                    ["targets"] = 16f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            warlock4.action_special_effect += traitAction.grade5_effectAction;
            warlock4.action_special_effect += (WorldAction)Delegate.Combine(warlock4.action_special_effect,
                    new WorldAction(warlockSkill.effect_warlock4));
            AssetManager.traits.add(warlock4);

            ActorTrait warlock5 = new ActorTrait
            {
                id = "warlock5",
                path_icon = "trait/warlock5",
                needs_to_be_explored = false,
                group_id = "warlock",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 288f,
                    ["damage"] = 512f,
                    ["health"] = 1760f,
                    ["armor"] = 144f,
                    ["attack_speed"] = 19.2f,
                    ["hitthetarget"] = 190f,
                    ["DodgeEvade"] = 160f,
                    ["MagicApplication"] = 7.0f,
                    ["MagicShield"] = 240f,
                    ["Fixedwound"] = 560f,
                    ["Restorehealth"] = 32f,      // 回血
                    ["MagicReply"] = 80f,           // 魔力回复
                    ["mana"] = 3360f,
                    ["targets"] = 32f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            warlock5.action_special_effect += traitAction.grade6_effectAction;
            warlock5.action_special_effect += (WorldAction)Delegate.Combine(warlock5.action_special_effect,
                    new WorldAction(warlockSkill.effect_warlock5));
            AssetManager.traits.add(warlock5);

            ActorTrait warlock6 = new ActorTrait
            {
                id = "warlock6",
                path_icon = "trait/warlock6",
                needs_to_be_explored = false,
                group_id = "warlock",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 576f,
                    ["damage"] = 1024f,
                    ["health"] = 3520f,
                    ["armor"] = 288f,
                    ["attack_speed"] = 38.4f,
                    ["hitthetarget"] = 220f,
                    ["DodgeEvade"] = 190f,
                    ["MagicApplication"] = 7.0f,
                    ["MagicShield"] = 480f,
                    ["Fixedwound"] = 1120f,
                    ["Restorehealth"] = 64f,      // 回血
                    ["MagicReply"] = 160f,          // 魔力回复
                    ["mana"] = 6720f,
                    ["targets"] = 64f,
                    ["area_of_effect"] = 96f,
                    ["range"] = 96f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            warlock6.action_special_effect += traitAction.grade7_effectAction;
            warlock6.action_special_effect += (WorldAction)Delegate.Combine(warlock6.action_special_effect,
                    new WorldAction(warlockSkill.effect_warlock6));
            AssetManager.traits.add(warlock6);

            ActorTrait warlock7 = new ActorTrait
            {
                id = "warlock7",
                path_icon = "trait/warlock7",
                needs_to_be_explored = false,
                group_id = "warlock",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 1152f,
                    ["damage"] = 2048f,
                    ["health"] = 7040f,
                    ["armor"] = 576f,
                    ["attack_speed"] = 76.8f,
                    ["hitthetarget"] = 250f,
                    ["DodgeEvade"] = 220f,
                    ["MagicApplication"] = 14.0f,
                    ["MagicShield"] = 960f,
                    ["Fixedwound"] = 2240f,
                    ["Restorehealth"] = 128f,     // 回血
                    ["MagicReply"] = 320f,          // 魔力回复
                    ["mana"] = 13440f,
                    ["targets"] = 128f,
                    ["area_of_effect"] = 192f,
                    ["range"] = 192f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            warlock7.action_special_effect += (WorldAction)Delegate.Combine(warlock7.action_special_effect,
                    new WorldAction(warlockSkill.effect_warlock7));
            AssetManager.traits.add(warlock7);

            // 炼金术士 🧪（魔法药剂师）
            ActorTrait alchemist1 = new ActorTrait
            {
                id = "alchemist1",
                path_icon = "trait/alchemist1",
                needs_to_be_explored = false,
                group_id = "alchemist",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 34f,
                    ["damage"] = 28f,
                    ["health"] = 150f,
                    ["armor"] = 15f,
                    ["attack_speed"] = 1.3f,
                    ["hitthetarget"] = 55f,
                    ["DodgeEvade"] = 55f,
                    ["MagicApplication"] = 6.5f,
                    ["MagicShield"] = 15f,
                    ["Fixedwound"] = 25f,
                    ["Restorehealth"] = 14f,       // 回血
                    ["MagicReply"] = 7f,            // 魔力回复
                    ["mana"] = 170f,
                    ["targets"] = 3f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            alchemist1.action_special_effect += traitAction.grade2_effectAction;
            AssetManager.traits.add(alchemist1);

            ActorTrait alchemist2 = new ActorTrait
            {
                id = "alchemist2",
                path_icon = "trait/alchemist2",
                needs_to_be_explored = false,
                group_id = "alchemist",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 68f,
                    ["damage"] = 56f,
                    ["health"] = 300f,
                    ["armor"] = 30f,
                    ["attack_speed"] = 2.6f,
                    ["hitthetarget"] = 85f,
                    ["DodgeEvade"] = 85f,
                    ["MagicApplication"] = 6.5f,
                    ["MagicShield"] = 30f,
                    ["Fixedwound"] = 50f,
                    ["Restorehealth"] = 28f,       // 回血
                    ["MagicReply"] = 14f,           // 魔力回复
                    ["mana"] = 340f,
                    ["targets"] = 6f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            alchemist2.action_special_effect += traitAction.grade3_effectAction;
            AssetManager.traits.add(alchemist2);

            ActorTrait alchemist3 = new ActorTrait
            {
                id = "alchemist3",
                path_icon = "trait/alchemist3",
                needs_to_be_explored = false,
                group_id = "alchemist",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 136f,
                    ["damage"] = 112f,
                    ["health"] = 600f,
                    ["armor"] = 60f,
                    ["attack_speed"] = 5.2f,
                    ["hitthetarget"] = 115f,
                    ["DodgeEvade"] = 115f,
                    ["MagicApplication"] = 6.5f,
                    ["MagicShield"] = 60f,
                    ["Fixedwound"] = 100f,
                    ["Restorehealth"] = 56f,       // 回血
                    ["MagicReply"] = 28f,           // 魔力回复
                    ["mana"] = 680f,
                    ["targets"] = 12f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            alchemist3.action_special_effect += traitAction.grade4_effectAction;
            alchemist3.action_attack_target = new AttackAction(alchemistSkill.attack_alchemist3);
            AssetManager.traits.add(alchemist3);

            ActorTrait alchemist4 = new ActorTrait
            {
                id = "alchemist4",
                path_icon = "trait/alchemist4",
                needs_to_be_explored = false,
                group_id = "alchemist",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 272f,
                    ["damage"] = 224f,
                    ["health"] = 1200f,
                    ["armor"] = 120f,
                    ["attack_speed"] = 10.4f,
                    ["hitthetarget"] = 145f,
                    ["DodgeEvade"] = 145f,
                    ["MagicApplication"] = 6.5f,
                    ["MagicShield"] = 120f,
                    ["Fixedwound"] = 200f,
                    ["Restorehealth"] = 48f,      // 回血
                    ["MagicReply"] = 48f,           // 魔力回复
                    ["mana"] = 1360f,
                    ["targets"] = 24f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            alchemist4.action_attack_target = new AttackAction(alchemistSkill.attack_alchemist4);
            alchemist4.action_special_effect += traitAction.grade5_effectAction;
            AssetManager.traits.add(alchemist4);

            ActorTrait alchemist5 = new ActorTrait
            {
                id = "alchemist5",
                path_icon = "trait/alchemist5",
                needs_to_be_explored = false,
                group_id = "alchemist",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 544f,
                    ["damage"] = 448f,
                    ["health"] = 2400f,
                    ["armor"] = 240f,
                    ["attack_speed"] = 20.8f,
                    ["hitthetarget"] = 175f,
                    ["DodgeEvade"] = 175f,
                    ["MagicApplication"] = 6.5f,
                    ["MagicShield"] = 240f,
                    ["Fixedwound"] = 400f,
                    ["Restorehealth"] = 96f,      // 回血
                    ["MagicReply"] = 96f,           // 魔力回复
                    ["mana"] = 2720f,
                    ["targets"] = 48f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            alchemist5.action_attack_target = new AttackAction(alchemistSkill.attack_alchemist5);
            alchemist5.action_special_effect += traitAction.grade6_effectAction;
            AssetManager.traits.add(alchemist5);

            ActorTrait alchemist6 = new ActorTrait
            {
                id = "alchemist6",
                path_icon = "trait/alchemist6",
                needs_to_be_explored = false,
                group_id = "alchemist",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 1088f,
                    ["damage"] = 896f,
                    ["health"] = 4800f,
                    ["armor"] = 480f,
                    ["attack_speed"] = 41.6f,
                    ["hitthetarget"] = 205f,
                    ["DodgeEvade"] = 205f,
                    ["MagicApplication"] = 6.5f,
                    ["MagicShield"] = 480f,
                    ["Fixedwound"] = 800f,
                    ["Restorehealth"] = 192f,      // 回血
                    ["MagicReply"] = 192f,          // 魔力回复
                    ["mana"] = 5440f,
                    ["targets"] = 96f,
                    ["area_of_effect"] = 96f,
                    ["range"] = 96f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            alchemist6.action_attack_target = new AttackAction(alchemistSkill.attack_alchemist6);
            alchemist6.action_special_effect += traitAction.grade7_effectAction;
            AssetManager.traits.add(alchemist6);

            ActorTrait alchemist7 = new ActorTrait
            {
                id = "alchemist7",
                path_icon = "trait/alchemist7",
                needs_to_be_explored = false,
                group_id = "alchemist",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 2176f,
                    ["damage"] = 1792f,
                    ["health"] = 9600f,
                    ["armor"] = 960f,
                    ["attack_speed"] = 83.2f,
                    ["hitthetarget"] = 235f,
                    ["DodgeEvade"] = 235f,
                    ["MagicApplication"] = 13.0f,
                    ["MagicShield"] = 960f,
                    ["Fixedwound"] = 1600f,
                    ["Restorehealth"] = 384f,      // 回血
                    ["MagicReply"] = 384f,          // 魔力回复
                    ["mana"] = 10880f,
                    ["targets"] = 192f,
                    ["area_of_effect"] = 192f,
                    ["range"] = 192f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            alchemist7.action_attack_target = new AttackAction(alchemistSkill.attack_alchemist7);
            AssetManager.traits.add(alchemist7);

            // 野蛮人 🪓（狂战士）
            ActorTrait barbarian1 = new ActorTrait
            {
                id = "barbarian1",
                path_icon = "trait/barbarian1",
                needs_to_be_explored = false,
                group_id = "barbarian",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 14f,
                    ["damage"] = 45f,
                    ["health"] = 320f,
                    ["armor"] = 16f,
                    ["attack_speed"] = 1.7f,
                    ["hitthetarget"] = 35f,
                    ["DodgeEvade"] = 35f,
                    ["MagicApplication"] = 3.0f,
                    ["MagicShield"] = 12f,
                    ["Fixedwound"] = 10f,
                    ["Restorehealth"] = 12f,       // 回血
                    ["MagicReply"] = 3f,            // 魔力回复
                    ["mana"] = 60f,
                    ["targets"] = 2f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            barbarian1.action_special_effect += traitAction.grade2_effectAction;
            AssetManager.traits.add(barbarian1);

            ActorTrait barbarian2 = new ActorTrait
            {
                id = "barbarian2",
                path_icon = "trait/barbarian2",
                needs_to_be_explored = false,
                group_id = "barbarian",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 28f,
                    ["damage"] = 90f,
                    ["health"] = 640f,
                    ["armor"] = 32f,
                    ["attack_speed"] = 3.4f,
                    ["hitthetarget"] = 65f,
                    ["DodgeEvade"] = 65f,
                    ["MagicApplication"] = 3.0f,
                    ["MagicShield"] = 24f,
                    ["Fixedwound"] = 20f,
                    ["Restorehealth"] = 24f,       // 回血
                    ["MagicReply"] = 6f,            // 魔力回复
                    ["mana"] = 120f,
                    ["targets"] = 4f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            barbarian2.action_special_effect += traitAction.grade3_effectAction;
            AssetManager.traits.add(barbarian2);

            ActorTrait barbarian3 = new ActorTrait
            {
                id = "barbarian3",
                path_icon = "trait/barbarian3",
                needs_to_be_explored = false,
                group_id = "barbarian",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 56f,
                    ["damage"] = 180f,
                    ["health"] = 1280f,
                    ["armor"] = 64f,
                    ["attack_speed"] = 6.8f,
                    ["hitthetarget"] = 95f,
                    ["DodgeEvade"] = 95f,
                    ["MagicApplication"] = 3.0f,
                    ["MagicShield"] = 48f,
                    ["Fixedwound"] = 40f,
                    ["Restorehealth"] = 48f,       // 回血
                    ["MagicReply"] = 12f,           // 魔力回复
                    ["mana"] = 240f,
                    ["targets"] = 8f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            barbarian3.action_special_effect += traitAction.grade4_effectAction;
            barbarian3.action_special_effect += (WorldAction)Delegate.Combine(barbarian3.action_special_effect,
                    new WorldAction(barbarianSkill.barbarian3_Regen));
            AssetManager.traits.add(barbarian3);

            ActorTrait barbarian4 = new ActorTrait
            {
                id = "barbarian4",
                path_icon = "trait/barbarian4",
                needs_to_be_explored = false,
                group_id = "barbarian",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 112f,
                    ["damage"] = 360f,
                    ["health"] = 2560f,
                    ["armor"] = 128f,
                    ["attack_speed"] = 13.6f,
                    ["hitthetarget"] = 125f,
                    ["DodgeEvade"] = 125f,
                    ["MagicApplication"] = 3.0f,
                    ["MagicShield"] = 96f,
                    ["Fixedwound"] = 80f,
                    ["Restorehealth"] = 96f,       // 回血
                    ["MagicReply"] = 24f,           // 魔力回复
                    ["mana"] = 480f,
                    ["targets"] = 16f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            barbarian4.action_special_effect += traitAction.grade5_effectAction;
            barbarian4.action_special_effect += (WorldAction)Delegate.Combine(barbarian4.action_special_effect,
                    new WorldAction(barbarianSkill.barbarian4_Regen));
            AssetManager.traits.add(barbarian4);

            ActorTrait barbarian5 = new ActorTrait
            {
                id = "barbarian5",
                path_icon = "trait/barbarian5",
                needs_to_be_explored = false,
                group_id = "barbarian",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 224f,
                    ["damage"] = 720f,
                    ["health"] = 5120f,
                    ["armor"] = 256f,
                    ["attack_speed"] = 27.2f,
                    ["hitthetarget"] = 155f,
                    ["DodgeEvade"] = 155f,
                    ["MagicApplication"] = 3.0f,
                    ["MagicShield"] = 192f,
                    ["Fixedwound"] = 160f,
                    ["Restorehealth"] = 192f,      // 回血
                    ["MagicReply"] = 48f,           // 魔力回复
                    ["mana"] = 960f,
                    ["targets"] = 32f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            barbarian5.action_special_effect += traitAction.grade6_effectAction;
            barbarian5.action_special_effect += (WorldAction)Delegate.Combine(barbarian5.action_special_effect,
                    new WorldAction(barbarianSkill.barbarian5_Regen));
            AssetManager.traits.add(barbarian5);

            ActorTrait barbarian6 = new ActorTrait
            {
                id = "barbarian6",
                path_icon = "trait/barbarian6",
                needs_to_be_explored = false,
                group_id = "barbarian",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 448f,
                    ["damage"] = 1440f,
                    ["health"] = 10240f,
                    ["armor"] = 512f,
                    ["attack_speed"] = 54.4f,
                    ["hitthetarget"] = 185f,
                    ["DodgeEvade"] = 185f,
                    ["MagicApplication"] = 3.0f,
                    ["MagicShield"] = 384f,
                    ["Fixedwound"] = 320f,
                    ["Restorehealth"] = 384f,      // 回血
                    ["MagicReply"] = 96f,           // 魔力回复
                    ["mana"] = 1920f,
                    ["targets"] = 64f,
                    ["area_of_effect"] = 96f,
                    ["range"] = 96f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            barbarian6.action_special_effect += traitAction.grade7_effectAction;
            barbarian6.action_special_effect += (WorldAction)Delegate.Combine(barbarian6.action_special_effect,
                    new WorldAction(barbarianSkill.barbarian6_Regen));
            AssetManager.traits.add(barbarian6);

            ActorTrait barbarian7 = new ActorTrait
            {
                id = "barbarian7",
                path_icon = "trait/barbarian7",
                needs_to_be_explored = false,
                group_id = "barbarian",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 896f,
                    ["damage"] = 2880f,
                    ["health"] = 20480f,
                    ["armor"] = 1024f,
                    ["attack_speed"] = 108.8f,
                    ["hitthetarget"] = 215f,
                    ["DodgeEvade"] = 215f,
                    ["MagicApplication"] = 6.0f,
                    ["MagicShield"] = 768f,
                    ["Fixedwound"] = 640f,
                    ["Restorehealth"] = 768f,      // 回血
                    ["MagicReply"] = 192f,          // 魔力回复
                    ["mana"] = 3840f,
                    ["targets"] = 128f,
                    ["area_of_effect"] = 192f,
                    ["range"] = 192f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            barbarian7.action_special_effect += (WorldAction)Delegate.Combine(barbarian7.action_special_effect,
                    new WorldAction(barbarianSkill.barbarian7_Regen));
            AssetManager.traits.add(barbarian7);

            // 召唤兽职业
            ActorTrait Summonedcreature1 = new ActorTrait
            {
                id = "Summonedcreature1",
                path_icon = "trait/Summonedcreature1",
                needs_to_be_explored = false,
                group_id = "Summonedcreature",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 80f,
                    ["damage"] = 60f,
                    ["health"] = 600f,
                    ["armor"] = 48f,
                    ["attack_speed"] = 4.0f,
                    ["hitthetarget"] = 105f,
                    ["DodgeEvade"] = 120f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 28f,
                    ["Fixedwound"] = 48f,
                    ["Restorehealth"] = 32f,      // 回血
                    ["MagicReply"] = 36f,           // 魔力回复
                    ["mana"] = 720f,
                    ["targets"] = 20f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Summonedcreature1.action_special_effect += traitAction.grade4_effectAction;
            Summonedcreature1.action_special_effect += (WorldAction)Delegate.Combine(Summonedcreature1.action_special_effect,
                    new WorldAction(SummonerSkill.tamedBeastSpecialEffect));
            AssetManager.traits.add(Summonedcreature1);

            ActorTrait Summonedcreature2 = new ActorTrait
            {
                id = "Summonedcreature2",
                path_icon = "trait/Summonedcreature2",
                needs_to_be_explored = false,
                group_id = "Summonedcreature",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 160f,
                    ["damage"] = 120f,
                    ["health"] = 1200f,
                    ["armor"] = 96f,
                    ["attack_speed"] = 8.0f,
                    ["hitthetarget"] = 135f,
                    ["DodgeEvade"] = 150f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 56f,
                    ["Fixedwound"] = 96f,
                    ["Restorehealth"] = 64f,      // 回血
                    ["MagicReply"] = 72f,           // 魔力回复
                    ["mana"] = 1440f,
                    ["targets"] = 40f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Summonedcreature2.action_special_effect += traitAction.grade5_effectAction;
            Summonedcreature2.action_special_effect += (WorldAction)Delegate.Combine(Summonedcreature2.action_special_effect,
                    new WorldAction(SummonerSkill.tamedBeastSpecialEffect));
            AssetManager.traits.add(Summonedcreature2);

            ActorTrait Summonedcreature3 = new ActorTrait
            {
                id = "Summonedcreature3",
                path_icon = "trait/Summonedcreature3",
                needs_to_be_explored = false,
                group_id = "Summonedcreature",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 320f,
                    ["damage"] = 240f,
                    ["health"] = 2400f,
                    ["armor"] = 192f,
                    ["attack_speed"] = 16.0f,
                    ["hitthetarget"] = 165f,
                    ["DodgeEvade"] = 180f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 112f,
                    ["Fixedwound"] = 192f,
                    ["Restorehealth"] = 128f,     // 回血
                    ["MagicReply"] = 144f,          // 魔力回复
                    ["mana"] = 2880f,
                    ["targets"] = 80f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Summonedcreature3.action_special_effect += traitAction.grade6_effectAction;
            Summonedcreature3.action_special_effect += (WorldAction)Delegate.Combine(Summonedcreature3.action_special_effect,
                    new WorldAction(SummonerSkill.tamedBeastSpecialEffect));
            AssetManager.traits.add(Summonedcreature3);

            ActorTrait Summonedcreature4 = new ActorTrait
            {
                id = "Summonedcreature4",
                path_icon = "trait/Summonedcreature4",
                needs_to_be_explored = false,
                group_id = "Summonedcreature",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 640f,
                    ["damage"] = 480f,
                    ["health"] = 4800f,
                    ["armor"] = 384f,
                    ["attack_speed"] = 32.0f,
                    ["hitthetarget"] = 195f,
                    ["DodgeEvade"] = 210f,
                    ["MagicApplication"] = 5.0f,
                    ["MagicShield"] = 224f,
                    ["Fixedwound"] = 384f,
                    ["Restorehealth"] = 256f,     // 回血
                    ["MagicReply"] = 288f,          // 魔力回复
                    ["mana"] = 5760f,
                    ["targets"] = 160f,
                    ["area_of_effect"] = 96f,
                    ["range"] = 96f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Summonedcreature4.action_special_effect += (WorldAction)Delegate.Combine(Summonedcreature4.action_special_effect,
                    new WorldAction(SummonerSkill.tamedBeastSpecialEffect));
            AssetManager.traits.add(Summonedcreature4);

            ActorTrait Summonedcreature5 = new ActorTrait
            {
                id = "Summonedcreature5",
                path_icon = "trait/Summonedcreature5",
                needs_to_be_explored = false,
                group_id = "Summonedcreature",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 1280f,
                    ["damage"] = 960f,
                    ["health"] = 9600f,
                    ["armor"] = 768f,
                    ["attack_speed"] = 64.0f,
                    ["hitthetarget"] = 225f,
                    ["DodgeEvade"] = 240f,
                    ["MagicApplication"] = 10f,
                    ["MagicShield"] = 448f,
                    ["Fixedwound"] = 768f,
                    ["Restorehealth"] = 512f,     // 回血
                    ["MagicReply"] = 576f,          // 魔力回复
                    ["mana"] = 11520f,
                    ["targets"] = 320f,
                    ["area_of_effect"] = 192f,
                    ["range"] = 192f,
                    ["speed"] = 20f,
                    ["multiplier_speed"] = 1f,
                }
            };
            Summonedcreature5.action_special_effect += (WorldAction)Delegate.Combine(Summonedcreature5.action_special_effect,
                    new WorldAction(SummonerSkill.tamedBeastSpecialEffect));
            AssetManager.traits.add(Summonedcreature5);

            StatusAsset cursed = AssetManager.status.get("cursed");
            cursed.opposite_traits = AssetLibrary<StatusAsset>.a<string>(new string[]
            {
                "evil",
                // 法师特质
                "enchanter1","enchanter2","enchanter3","enchanter4","enchanter5","enchanter6","enchanter7",
                // 牧师特质
                "pastor1","pastor2","pastor3","pastor4","pastor5","pastor6","pastor7",
                // 骑士特质
                "knight1","knight2","knight3","knight4","knight5","knight6","knight7",
                // 战士特质
                "valiantgeneral1","valiantgeneral2","valiantgeneral3","valiantgeneral4","valiantgeneral5","valiantgeneral6","valiantgeneral7",
                // 射手特质
                "Ranger1","Ranger2","Ranger3","Ranger4","Ranger5","Ranger6","Ranger7",
                // 刺客特质
                "Assassin1","Assassin2","Assassin3","Assassin4","Assassin5","Assassin6","Assassin7",
                // 召唤师特质
                "Summoner1","Summoner2","Summoner3","Summoner4","Summoner5","Summoner6","Summoner7",
                // 吟游诗人特质
                "minstrel1","minstrel2","minstrel3","minstrel4","minstrel5","minstrel6","minstrel7",
                // 咒术师特质
                "warlock1","warlock2","warlock3","warlock4","warlock5","warlock6","warlock7",
                // 炼金术士特质
                "alchemist1","alchemist2","alchemist3","alchemist4","alchemist5","alchemist6","alchemist7",      
                // 野蛮人特质
                "barbarian1","barbarian2","barbarian3","barbarian4","barbarian5","barbarian6","barbarian7",
                // 受保护的召唤兽特质
                "Summonedcreature1","Summonedcreature2","Summonedcreature3","Summonedcreature4","Summonedcreature5"
            });
        }
    }
}