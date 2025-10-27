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


            ActorTrait occupation = new ActorTrait
            {
                id = "occupation",
                path_icon = "trait/occupation",
                needs_to_be_explored = false,
                group_id = "RankTalentst",
                base_stats = new BaseStats
                {
                    
                }
            };
            AssetManager.traits.add(occupation);

            // 法师 🔮（大魔导师）
            ActorTrait enchanter1 = new ActorTrait
            {
                id = "enchanter1",
                path_icon = "trait/enchanter1",
                needs_to_be_explored = false,
                group_id = "enchanter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 30f,         // 寿命
                    ["damage"] = 35f,           // 伤害
                    ["health"] = 120f,          // 生命值
                    ["armor"] = 10f,            // 防御
                    ["attack_speed"] = 1.0f,    // 攻击速度
                    ["hitthetarget"] = 45f,     // 命中
                    ["DodgeEvade"] = 55f,       // 闪避
                    ["Magicapplication"] = 6f,  // 魔力应用
                    ["MagicShield"] = 6f,       // 护盾
                    ["mana"] = 140f,            // 魔力值
                    ["targets"] = 4f,           // 攻击目标
                    ["area_of_effect"] = 3f,    // 影响范围
                    ["range"] = 3f,             // 攻击范围
                }
            };
            enchanter1.action_special_effect += traitAction.grade2_effectAction;
            enchanter1.action_special_effect += (WorldAction)Delegate.Combine(enchanter1.action_special_effect,
                    new WorldAction(traitAction.enchanter1_effect));
            AssetManager.traits.add(enchanter1);

            ActorTrait enchanter2 = new ActorTrait
            {
                id = "enchanter2",
                path_icon = "trait/enchanter2",
                needs_to_be_explored = false,
                group_id = "enchanter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 60f,
                    ["damage"] = 70f,
                    ["health"] = 240f,
                    ["armor"] = 20f,
                    ["attack_speed"] = 2.0f,
                    ["hitthetarget"] = 75f,
                    ["DodgeEvade"] = 85f,
                    ["Magicapplication"] = 6f,
                    ["MagicShield"] = 12f,
                    ["mana"] = 280f,
                    ["targets"] = 8f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                }
            };
            enchanter2.action_special_effect += traitAction.grade3_effectAction;
            enchanter2.action_special_effect += (WorldAction)Delegate.Combine(enchanter2.action_special_effect,
                    new WorldAction(traitAction.enchanter2_effect));
            AssetManager.traits.add(enchanter2);

            ActorTrait enchanter3 = new ActorTrait
            {
                id = "enchanter3",
                path_icon = "trait/enchanter3",
                needs_to_be_explored = false,
                group_id = "enchanter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 120f,
                    ["damage"] = 140f,
                    ["health"] = 480f,
                    ["armor"] = 40f,
                    ["attack_speed"] = 4.0f,
                    ["hitthetarget"] = 105f,
                    ["DodgeEvade"] = 115f,
                    ["Magicapplication"] = 6f,
                    ["MagicShield"] = 24f,
                    ["mana"] = 560f,
                    ["targets"] = 16f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                }
            };
            enchanter3.action_attack_target += new AttackAction((traitAction.attack_enchanter3));
            enchanter3.action_special_effect += traitAction.grade4_effectAction;
            enchanter3.action_special_effect += (WorldAction)Delegate.Combine(enchanter3.action_special_effect,
                    new WorldAction(traitAction.enchanter3_effect));
            AssetManager.traits.add(enchanter3);

            ActorTrait enchanter4 = new ActorTrait
            {
                id = "enchanter4",
                path_icon = "trait/enchanter4",
                needs_to_be_explored = false,
                group_id = "enchanter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 240f,
                    ["damage"] = 280f,
                    ["health"] = 960f,
                    ["armor"] = 80f,
                    ["attack_speed"] = 8.0f,
                    ["hitthetarget"] = 135f,
                    ["DodgeEvade"] = 145f,
                    ["Magicapplication"] = 6f,
                    ["MagicShield"] = 48f,
                    ["mana"] = 1120f,
                    ["targets"] = 32f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                }
            };
            enchanter4.action_attack_target += new AttackAction((traitAction.attack_enchanter4));
            enchanter4.action_special_effect += traitAction.grade5_effectAction;
            enchanter4.action_special_effect += (WorldAction)Delegate.Combine(enchanter4.action_special_effect,
                    new WorldAction(traitAction.enchanter4_effect));
            AssetManager.traits.add(enchanter4);

            ActorTrait enchanter5 = new ActorTrait
            {
                id = "enchanter5",
                path_icon = "trait/enchanter5",
                needs_to_be_explored = false,
                group_id = "enchanter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 480f,
                    ["damage"] = 560f,
                    ["health"] = 1920f,
                    ["armor"] = 160f,
                    ["attack_speed"] = 16.0f,
                    ["hitthetarget"] = 165f,
                    ["DodgeEvade"] = 175f,
                    ["Magicapplication"] = 6f,
                    ["MagicShield"] = 96f,
                    ["mana"] = 2240f,
                    ["targets"] = 64f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                }
            };
            enchanter5.action_attack_target += new AttackAction((traitAction.attack_enchanter5));
            enchanter5.action_special_effect += (WorldAction)Delegate.Combine(enchanter5.action_special_effect,
                    new WorldAction(traitAction.enchanter5_effect));
            AssetManager.traits.add(enchanter5);

            // 牧师 ✨（神圣祭司）
            ActorTrait pastor1 = new ActorTrait
            {
                id = "pastor1",
                path_icon = "trait/pastor1",
                needs_to_be_explored = false,
                group_id = "pastor",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 28f,
                    ["damage"] = 18f,
                    ["health"] = 180f,
                    ["armor"] = 17f,
                    ["attack_speed"] = 1.2f,
                    ["hitthetarget"] = 30f,
                    ["DodgeEvade"] = 60f,
                    ["Magicapplication"] = 4f,
                    ["MagicShield"] = 5f,
                    ["mana"] = 80f,
                    ["targets"] = 3f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                }
            };
            pastor1.action_special_effect += traitAction.grade2_effectAction;
            pastor1.action_special_effect += (WorldAction)Delegate.Combine(pastor1.action_special_effect,
                    new WorldAction(traitAction.pastor1_effect));
            AssetManager.traits.add(pastor1);

            ActorTrait pastor2 = new ActorTrait
            {
                id = "pastor2",
                path_icon = "trait/pastor2",
                needs_to_be_explored = false,
                group_id = "pastor",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 56f,
                    ["damage"] = 36f,
                    ["health"] = 360f,
                    ["armor"] = 34f,
                    ["attack_speed"] = 2.4f,
                    ["hitthetarget"] = 60f,
                    ["DodgeEvade"] = 90f,
                    ["Magicapplication"] = 4f,
                    ["MagicShield"] = 10f,
                    ["mana"] = 160f,
                    ["targets"] = 6f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                }
            };
            pastor2.action_special_effect += traitAction.grade3_effectAction;
            pastor2.action_special_effect += (WorldAction)Delegate.Combine(pastor2.action_special_effect,
                    new WorldAction(traitAction.pastor2_effect));
            AssetManager.traits.add(pastor2);

            ActorTrait pastor3 = new ActorTrait
            {
                id = "pastor3",
                path_icon = "trait/pastor3",
                needs_to_be_explored = false,
                group_id = "pastor",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 112f,
                    ["damage"] = 72f,
                    ["health"] = 720f,
                    ["armor"] = 68f,
                    ["attack_speed"] = 4.8f,
                    ["hitthetarget"] = 90f,
                    ["DodgeEvade"] = 120f,
                    ["Magicapplication"] = 4f,
                    ["MagicShield"] = 20f,
                    ["mana"] = 320f,
                    ["targets"] = 12f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                }
            };
            pastor3.action_special_effect += traitAction.grade4_effectAction;
            pastor3.action_special_effect += (WorldAction)Delegate.Combine(pastor3.action_special_effect,
                new WorldAction(traitAction.effect_pastor3));  
            pastor3.action_special_effect += (WorldAction)Delegate.Combine(pastor3.action_special_effect,
                    new WorldAction(traitAction.pastor3_effect));        
            AssetManager.traits.add(pastor3);

            ActorTrait pastor4 = new ActorTrait
            {
                id = "pastor4",
                path_icon = "trait/pastor4",
                needs_to_be_explored = false,
                group_id = "pastor",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 224f,
                    ["damage"] = 144f,
                    ["health"] = 1440f,
                    ["armor"] = 136f,
                    ["attack_speed"] = 9.6f,
                    ["hitthetarget"] = 120f,
                    ["DodgeEvade"] = 150f,
                    ["Magicapplication"] = 4f,
                    ["MagicShield"] = 40f,
                    ["mana"] = 640f,
                    ["targets"] = 24f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                }
            };
            pastor4.action_special_effect += traitAction.grade5_effectAction;
            pastor4.action_special_effect += (WorldAction)Delegate.Combine(pastor4.action_special_effect,
                new WorldAction(traitAction.effect_pastor4));        
            pastor4.action_special_effect += (WorldAction)Delegate.Combine(pastor4.action_special_effect,
                    new WorldAction(traitAction.pastor4_effect));
            AssetManager.traits.add(pastor4);

            ActorTrait pastor5 = new ActorTrait
            {
                id = "pastor5",
                path_icon = "trait/pastor5",
                needs_to_be_explored = false,
                group_id = "pastor",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 448f,
                    ["damage"] = 288f,
                    ["health"] = 2880f,
                    ["armor"] = 272f,
                    ["attack_speed"] = 19.2f,
                    ["hitthetarget"] = 150f,
                    ["DodgeEvade"] = 180f,
                    ["Magicapplication"] = 4f,
                    ["MagicShield"] = 80f,
                    ["mana"] = 1280f,
                    ["targets"] = 48f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                }
            };
            pastor5.action_special_effect += (WorldAction)Delegate.Combine(pastor5.action_special_effect,
                new WorldAction(traitAction.effect_pastor5));        
            pastor5.action_special_effect += (WorldAction)Delegate.Combine(pastor5.action_special_effect,
                    new WorldAction(traitAction.pastor5_effect));
            AssetManager.traits.add(pastor5);

            // 骑士 🛡️（圣殿骑士）
            ActorTrait knight1 = new ActorTrait
            {
                id = "knight1",
                path_icon = "trait/knight1",
                needs_to_be_explored = false,
                group_id = "knight",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 25f,
                    ["damage"] = 22f,
                    ["health"] = 320f,
                    ["armor"] = 18f,
                    ["attack_speed"] = 1.3f,
                    ["hitthetarget"] = 35f,
                    ["DodgeEvade"] = 45f,
                    ["Magicapplication"] = 4f,
                    ["MagicShield"] = 12f,
                    ["mana"] = 70f,
                    ["targets"] = 2f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                }
            };
            knight1.action_special_effect += traitAction.grade2_effectAction;
            knight1.action_special_effect += (WorldAction)Delegate.Combine(knight1.action_special_effect,
                    new WorldAction(traitAction.knight1_effect));
            AssetManager.traits.add(knight1);

            ActorTrait knight2 = new ActorTrait
            {
                id = "knight2",
                path_icon = "trait/knight2",
                needs_to_be_explored = false,
                group_id = "knight",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 50f,
                    ["damage"] = 44f,
                    ["health"] = 640f,
                    ["armor"] = 36f,
                    ["attack_speed"] = 2.6f,
                    ["hitthetarget"] = 65f,
                    ["DodgeEvade"] = 75f,
                    ["Magicapplication"] = 4f,
                    ["MagicShield"] = 24f,
                    ["mana"] = 140f,
                    ["targets"] = 4f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                }
            };
            knight2.action_special_effect += traitAction.grade3_effectAction;
            knight2.action_special_effect += (WorldAction)Delegate.Combine(knight2.action_special_effect,
                    new WorldAction(traitAction.knight2_effect));
            AssetManager.traits.add(knight2);

            ActorTrait knight3 = new ActorTrait
            {
                id = "knight3",
                path_icon = "trait/knight3",
                needs_to_be_explored = false,
                group_id = "knight",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 100f,
                    ["damage"] = 88f,
                    ["health"] = 1280f,
                    ["armor"] = 72f,
                    ["attack_speed"] = 5.2f,
                    ["hitthetarget"] = 95f,
                    ["DodgeEvade"] = 105f,
                    ["Magicapplication"] = 4f,
                    ["MagicShield"] = 48f,
                    ["mana"] = 280f,
                    ["targets"] = 8f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                }
            };
            knight3.action_special_effect += traitAction.grade4_effectAction;
            knight3.action_special_effect += (WorldAction)Delegate.Combine(knight3.action_special_effect,
                    new WorldAction(traitAction.effect_knight3));
            knight3.action_special_effect += (WorldAction)Delegate.Combine(knight3.action_special_effect,
                    new WorldAction(traitAction.knight3_effect));
            AssetManager.traits.add(knight3);

            ActorTrait knight4 = new ActorTrait
            {
                id = "knight4",
                path_icon = "trait/knight4",
                needs_to_be_explored = false,
                group_id = "knight",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 200f,
                    ["damage"] = 176f,
                    ["health"] = 2560f,
                    ["armor"] = 144f,
                    ["attack_speed"] = 10.4f,
                    ["hitthetarget"] = 125f,
                    ["DodgeEvade"] = 135f,
                    ["Magicapplication"] = 4f,
                    ["MagicShield"] = 96f,
                    ["mana"] = 560f,
                    ["targets"] = 16f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                }
            };
            knight4.action_special_effect += traitAction.grade5_effectAction;
            knight4.action_special_effect += (WorldAction)Delegate.Combine(knight4.action_special_effect,
                    new WorldAction(traitAction.effect_knight4));
            knight4.action_special_effect += (WorldAction)Delegate.Combine(knight4.action_special_effect,
                    new WorldAction(traitAction.knight4_effect));
            AssetManager.traits.add(knight4);

            ActorTrait knight5 = new ActorTrait
            {
                id = "knight5",
                path_icon = "trait/knight5",
                needs_to_be_explored = false,
                group_id = "knight",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 400f,
                    ["damage"] = 352f,
                    ["health"] = 5120f,
                    ["armor"] = 288f,
                    ["attack_speed"] = 20.8f,
                    ["hitthetarget"] = 155f,
                    ["DodgeEvade"] = 165f,
                    ["Magicapplication"] = 4f,
                    ["MagicShield"] = 192f,
                    ["mana"] = 1120f,
                    ["targets"] = 32f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                }
            };
            knight5.action_special_effect += (WorldAction)Delegate.Combine(knight5.action_special_effect,
                    new WorldAction(traitAction.effect_knight5));
            knight5.action_special_effect += (WorldAction)Delegate.Combine(knight5.action_special_effect,
                    new WorldAction(traitAction.knight5_effect));
            AssetManager.traits.add(knight5);

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
                    ["damage"] = 32f,
                    ["health"] = 200f,
                    ["armor"] = 16f,
                    ["attack_speed"] = 1.4f,
                    ["hitthetarget"] = 45f,
                    ["DodgeEvade"] = 50f,
                    ["Magicapplication"] = 2f,
                    ["MagicShield"] = 8f,
                    ["mana"] = 40f,
                    ["targets"] = 2f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                }
            };
            valiantgeneral1.action_special_effect += traitAction.grade2_effectAction;
            valiantgeneral1.action_special_effect += (WorldAction)Delegate.Combine(valiantgeneral1.action_special_effect,
                    new WorldAction(traitAction.valiantgeneral1_effect));
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
                    ["damage"] = 64f,
                    ["health"] = 400f,
                    ["armor"] = 32f,
                    ["attack_speed"] = 2.8f,
                    ["hitthetarget"] = 75f,
                    ["DodgeEvade"] = 80f,
                    ["Magicapplication"] = 2f,
                    ["MagicShield"] = 16f,
                    ["mana"] = 80f,
                    ["targets"] = 4f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                }
            };
            valiantgeneral2.action_special_effect += traitAction.grade3_effectAction;
            valiantgeneral2.action_special_effect += (WorldAction)Delegate.Combine(valiantgeneral2.action_special_effect,
                    new WorldAction(traitAction.valiantgeneral2_effect));
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
                    ["damage"] = 128f,
                    ["health"] = 800f,
                    ["armor"] = 64f,
                    ["attack_speed"] = 5.6f,
                    ["hitthetarget"] = 105f,
                    ["DodgeEvade"] = 110f,
                    ["Magicapplication"] = 2f,
                    ["MagicShield"] = 32f,
                    ["mana"] = 160f,
                    ["targets"] = 8f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                }
            };
            valiantgeneral3.action_attack_target += new AttackAction((traitAction.attack_valiantgeneral3));
            valiantgeneral3.action_special_effect += traitAction.grade4_effectAction;
            valiantgeneral3.action_special_effect += (WorldAction)Delegate.Combine(valiantgeneral3.action_special_effect,
                    new WorldAction(traitAction.valiantgeneral3_effect));
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
                    ["damage"] = 256f,
                    ["health"] = 1600f,
                    ["armor"] = 128f,
                    ["attack_speed"] = 11.2f,
                    ["hitthetarget"] = 135f,
                    ["DodgeEvade"] = 140f,
                    ["Magicapplication"] = 2f,
                    ["MagicShield"] = 64f,
                    ["mana"] = 320f,
                    ["targets"] = 16f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                }
            };
            valiantgeneral4.action_attack_target += new AttackAction((traitAction.attack_valiantgeneral4));
            valiantgeneral4.action_special_effect += traitAction.grade5_effectAction;
            valiantgeneral4.action_special_effect += (WorldAction)Delegate.Combine(valiantgeneral4.action_special_effect,
                    new WorldAction(traitAction.valiantgeneral4_effect));
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
                    ["damage"] = 512f,
                    ["health"] = 3200f,
                    ["armor"] = 256f,
                    ["attack_speed"] = 22.4f,
                    ["hitthetarget"] = 165f,
                    ["DodgeEvade"] = 170f,
                    ["Magicapplication"] = 2f,
                    ["MagicShield"] = 128f,
                    ["mana"] = 640f,
                    ["targets"] = 32f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                }
            };
            valiantgeneral5.action_attack_target += new AttackAction((traitAction.attack_valiantgeneral5));
            valiantgeneral5.action_special_effect += (WorldAction)Delegate.Combine(valiantgeneral5.action_special_effect,
                    new WorldAction(traitAction.valiantgeneral5_effect));
            AssetManager.traits.add(valiantgeneral5);

            // 射手 🏹（神射手）
            ActorTrait shooter1 = new ActorTrait
            {
                id = "shooter1",
                path_icon = "trait/shooter1",
                needs_to_be_explored = false,
                group_id = "shooter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 20f,
                    ["damage"] = 26f,
                    ["health"] = 120f,
                    ["armor"] = 15f,
                    ["attack_speed"] = 1.5f,
                    ["hitthetarget"] = 70f,
                    ["DodgeEvade"] = 65f,
                    ["Magicapplication"] = 3f,
                    ["MagicShield"] = 5f,
                    ["mana"] = 35f,
                    ["targets"] = 3f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                }
            };
            shooter1.action_special_effect += traitAction.grade2_effectAction;
            shooter1.action_special_effect += (WorldAction)Delegate.Combine(shooter1.action_special_effect,
                    new WorldAction(traitAction.shooter1_effect));
            AssetManager.traits.add(shooter1);

            ActorTrait shooter2 = new ActorTrait
            {
                id = "shooter2",
                path_icon = "trait/shooter2",
                needs_to_be_explored = false,
                group_id = "shooter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 40f,
                    ["damage"] = 52f,
                    ["health"] = 240f,
                    ["armor"] = 30f,
                    ["attack_speed"] = 3.0f,
                    ["hitthetarget"] = 100f,
                    ["DodgeEvade"] = 95f,
                    ["Magicapplication"] = 3f,
                    ["MagicShield"] = 10f,
                    ["mana"] = 70f,
                    ["targets"] = 6f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                }
            };
            shooter2.action_special_effect += traitAction.grade3_effectAction;
            shooter2.action_special_effect += (WorldAction)Delegate.Combine(shooter2.action_special_effect,
                    new WorldAction(traitAction.shooter2_effect));
            AssetManager.traits.add(shooter2);

            ActorTrait shooter3 = new ActorTrait
            {
                id = "shooter3",
                path_icon = "trait/shooter3",
                needs_to_be_explored = false,
                group_id = "shooter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 80f,
                    ["damage"] = 104f,
                    ["health"] = 480f,
                    ["armor"] = 60f,
                    ["attack_speed"] = 6.0f,
                    ["hitthetarget"] = 130f,
                    ["DodgeEvade"] = 125f,
                    ["Magicapplication"] = 3f,
                    ["MagicShield"] = 20f,
                    ["mana"] = 140f,
                    ["targets"] = 12f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                }
            };
            shooter3.action_attack_target += new AttackAction((traitAction.attack_shooter3));
            shooter3.action_special_effect += traitAction.grade4_effectAction;
            shooter3.action_special_effect += (WorldAction)Delegate.Combine(shooter3.action_special_effect,
                    new WorldAction(traitAction.shooter3_effect));
            AssetManager.traits.add(shooter3);

            ActorTrait shooter4 = new ActorTrait
            {
                id = "shooter4",
                path_icon = "trait/shooter4",
                needs_to_be_explored = false,
                group_id = "shooter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 160f,
                    ["damage"] = 208f,
                    ["health"] = 960f,
                    ["armor"] = 120f,
                    ["attack_speed"] = 12.0f,
                    ["hitthetarget"] = 160f,
                    ["DodgeEvade"] = 155f,
                    ["Magicapplication"] = 3f,
                    ["MagicShield"] = 40f,
                    ["mana"] = 280f,
                    ["targets"] = 24f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                }
            };
            shooter4.action_attack_target += new AttackAction((traitAction.attack_shooter4));
            shooter4.action_special_effect += traitAction.grade5_effectAction;
            shooter4.action_special_effect += (WorldAction)Delegate.Combine(shooter4.action_special_effect,
                    new WorldAction(traitAction.shooter4_effect));
            AssetManager.traits.add(shooter4);

            ActorTrait shooter5 = new ActorTrait
            {
                id = "shooter5",
                path_icon = "trait/shooter5",
                needs_to_be_explored = false,
                group_id = "shooter",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 320f,
                    ["damage"] = 416f,
                    ["health"] = 1920f,
                    ["armor"] = 240f,
                    ["attack_speed"] = 24.0f,
                    ["hitthetarget"] = 190f,
                    ["DodgeEvade"] = 185f,
                    ["Magicapplication"] = 3f,
                    ["MagicShield"] = 80f,
                    ["mana"] = 560f,
                    ["targets"] = 48f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                }
            };
            shooter5.action_attack_target += new AttackAction((traitAction.attack_shooter5));
            shooter5.action_special_effect += (WorldAction)Delegate.Combine(shooter5.action_special_effect,
                    new WorldAction(traitAction.shooter5_effect));
            AssetManager.traits.add(shooter5);

            // 刺客 🗡️（影舞者）
            ActorTrait assassin1 = new ActorTrait
            {
                id = "assassin1",
                path_icon = "trait/assassin1",
                needs_to_be_explored = false,
                group_id = "assassin",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 18f,
                    ["damage"] = 40f,
                    ["health"] = 100f,
                    ["armor"] = 10f,
                    ["attack_speed"] = 1.4f,
                    ["hitthetarget"] = 55f,
                    ["DodgeEvade"] = 100f,
                    ["Magicapplication"] = 3f,
                    ["MagicShield"] = 3f,
                    ["mana"] = 25f,
                    ["targets"] = 1f,
                    ["area_of_effect"] = 3f,
                    ["range"] = 3f,
                }
            };
            assassin1.action_special_effect += traitAction.grade2_effectAction;
            assassin1.action_special_effect += (WorldAction)Delegate.Combine(assassin1.action_special_effect,
                    new WorldAction(traitAction.assassin1_effect));
            AssetManager.traits.add(assassin1);

            ActorTrait assassin2 = new ActorTrait
            {
                id = "assassin2",
                path_icon = "trait/assassin2",
                needs_to_be_explored = false,
                group_id = "assassin",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 36f,
                    ["damage"] = 80f,
                    ["health"] = 200f,
                    ["armor"] = 20f,
                    ["attack_speed"] = 2.8f,
                    ["hitthetarget"] = 85f,
                    ["DodgeEvade"] = 130f,
                    ["Magicapplication"] = 3f,
                    ["MagicShield"] = 6f,
                    ["mana"] = 50f,
                    ["targets"] = 2f,
                    ["area_of_effect"] = 6f,
                    ["range"] = 6f,
                }
            };
            assassin2.action_special_effect += traitAction.grade3_effectAction;
            assassin2.action_special_effect += (WorldAction)Delegate.Combine(assassin2.action_special_effect,
                    new WorldAction(traitAction.assassin2_effect));
            AssetManager.traits.add(assassin2);

            ActorTrait assassin3 = new ActorTrait
            {
                id = "assassin3",
                path_icon = "trait/assassin3",
                needs_to_be_explored = false,
                group_id = "assassin",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 72f,
                    ["damage"] = 160f,
                    ["health"] = 400f,
                    ["armor"] = 40f,
                    ["attack_speed"] = 5.6f,
                    ["hitthetarget"] = 115f,
                    ["DodgeEvade"] = 160f,
                    ["Magicapplication"] = 3f,
                    ["MagicShield"] = 12f,
                    ["mana"] = 100f,
                    ["targets"] = 4f,
                    ["area_of_effect"] = 12f,
                    ["range"] = 12f,
                }
            };
            assassin3.action_attack_target += new AttackAction((traitAction.attack_assassin3));
            assassin3.action_special_effect += traitAction.grade4_effectAction;
            assassin3.action_special_effect += (WorldAction)Delegate.Combine(assassin3.action_special_effect,
                    new WorldAction(traitAction.assassin3_effect));
            AssetManager.traits.add(assassin3);

            ActorTrait assassin4 = new ActorTrait
            {
                id = "assassin4",
                path_icon = "trait/assassin4",
                needs_to_be_explored = false,
                group_id = "assassin",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 144f,
                    ["damage"] = 320f,
                    ["health"] = 800f,
                    ["armor"] = 80f,
                    ["attack_speed"] = 11.2f,
                    ["hitthetarget"] = 145f,
                    ["DodgeEvade"] = 190f,
                    ["Magicapplication"] = 3f,
                    ["MagicShield"] = 24f,
                    ["mana"] = 200f,
                    ["targets"] = 8f,
                    ["area_of_effect"] = 24f,
                    ["range"] = 24f,
                }
            };
            assassin4.action_attack_target += new AttackAction((traitAction.attack_assassin4));
            assassin4.action_special_effect += traitAction.grade5_effectAction;
            assassin4.action_special_effect += (WorldAction)Delegate.Combine(assassin4.action_special_effect,
                    new WorldAction(traitAction.assassin4_effect));
            AssetManager.traits.add(assassin4);

            ActorTrait assassin5 = new ActorTrait
            {
                id = "assassin5",
                path_icon = "trait/assassin5",
                needs_to_be_explored = false,
                group_id = "assassin",
                base_stats = new BaseStats
                {
                    ["lifespan"] = 288f,
                    ["damage"] = 640f,
                    ["health"] = 1600f,
                    ["armor"] = 160f,
                    ["attack_speed"] = 22.4f,
                    ["hitthetarget"] = 175f,
                    ["DodgeEvade"] = 220f,
                    ["Magicapplication"] = 3f,
                    ["MagicShield"] = 48f,
                    ["mana"] = 400f,
                    ["targets"] = 16f,
                    ["area_of_effect"] = 48f,
                    ["range"] = 48f,
                }
            };
            assassin5.action_attack_target += new AttackAction((traitAction.attack_assassin5));
            assassin5.action_special_effect += (WorldAction)Delegate.Combine(assassin5.action_special_effect,
                    new WorldAction(traitAction.assassin5_effect));
            AssetManager.traits.add(assassin5);
        }
    }
}