using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ReflectionUtility;
using ai;
using System.Numerics;
using strings;
using VideoCopilot.code.utils;
using InterestingTrait.code.Config;

namespace XianTu.code
{
    internal class traits
    {
        private static ActorTrait CreateTrait(string id, string path_icon, string group_id)
        {
            ActorTrait trait = new ActorTrait();
            trait.id = id;
            trait.path_icon = path_icon;
            trait.needs_to_be_explored = false;
            trait.group_id = group_id;
            trait.base_stats = new BaseStats();

            return trait;
        }
        public static ActorTrait TaiyiLg_AddActorTrait(string id, string pathIcon)
        {
            ActorTrait TaiyiLg = new ActorTrait
            {
                id = id,
                path_icon = pathIcon,
                group_id = "TaiyiLg",
                needs_to_be_explored = false
            };
            TaiyiLg.action_special_effect += traitAction.XianTu1_effectAction;
            AssetManager.traits.add(TaiyiLg);
            return TaiyiLg;
        }
        public static void Init()
        {
            ActorTrait TaiyiLg5 = TaiyiLg_AddActorTrait("TaiyiLg5", "trait/TaiyiLg5");
            TaiyiLg5.rarity = Rarity.R2_Epic;

            ActorTrait TaiyiLg6 = TaiyiLg_AddActorTrait("TaiyiLg6", "trait/TaiyiLg6");
            TaiyiLg6.rarity = Rarity.R2_Epic;
        
            ActorTrait TaiyiLg7 = TaiyiLg_AddActorTrait("TaiyiLg7", "trait/TaiyiLg7");
            TaiyiLg7.rarity = Rarity.R3_Legendary;

            _=TaiyiLg_AddActorTrait("TaiyiLg2", "trait/TaiyiLg2");
            _=TaiyiLg_AddActorTrait("TaiyiLg3", "trait/TaiyiLg3");
            _=TaiyiLg_AddActorTrait("TaiyiLg4", "trait/TaiyiLg4");

            ActorTrait TaiyiLg8 = CreateTrait("TaiyiLg8", "trait/TaiyiLg8", "TaiyiLg");
            TaiyiLg8.rate_inherit = 100;
            AssetManager.traits.add(TaiyiLg8);

            ActorTrait TaiyiLg9 = CreateTrait("TaiyiLg9", "trait/TaiyiLg9", "TaiyiLg");
            SafeSetStat(TaiyiLg9.base_stats, strings.S.lifespan, -0.5f);
            AssetManager.traits.add(TaiyiLg9);

            // 帝血诅咒 - 只有一个功能：完全无法觉醒武道资质
            ActorTrait TaiyiLg91 = CreateTrait("TaiyiLg91", "trait/TaiyiLg91", "TaiyiLg");
            TaiyiLg91.rarity = Rarity.R3_Legendary;
            AssetManager.traits.add(TaiyiLg91);

            ActorTrait XianTu1 = CreateTrait("XianTu1", "trait/XianTu1", "XianTu");
            SafeSetStat(XianTu1.base_stats, stats.Resist.id, 1.5f);
            SafeSetStat(XianTu1.base_stats , strings.S.damage, 10f);
            SafeSetStat(XianTu1.base_stats, strings.S.mass, 10f);
            SafeSetStat(XianTu1.base_stats , strings.S.health, 200f);
            SafeSetStat(XianTu1.base_stats , strings.S.accuracy, 3f);
            SafeSetStat(XianTu1.base_stats , strings.S.multiplier_speed, 0.2f);
            SafeSetStat(XianTu1.base_stats , strings.S.stamina, 30f);
            SafeSetStat(XianTu1.base_stats, "Dodge", 140f);
            SafeSetStat(XianTu1.base_stats, "Accuracy", 120f);
            XianTu1.action_special_effect += traitAction.XianTu2_effectAction;
            XianTu1.action_special_effect += traitAction.XianTu1_Regen;
            // 添加购买筑基丹功能
            XianTu1.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyTupoDanyao1(); return false; };
            // 添加购买凝气丹功能
            XianTu1.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyXiulianDanyao1(); return false; };
            // 添加灵石自动转换功能
            XianTu1.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.ConvertLingShiToZhongPin(); return false; } return false; };
            // 添加自动购买对应境界武器机制
            XianTu1.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.AutoBuyRealmWeapon(); return false; } return false; };
            // 绑定法术级别的真伤攻击机制
            XianTu1.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定基于真元的真实伤害攻击机制
            XianTu1.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            AssetManager.traits.add(XianTu1);

            ActorTrait XianTu2 = CreateTrait("XianTu2", "trait/XianTu2", "XianTu");
            SafeSetStat(XianTu2.base_stats, stats.Resist.id, 2.0f);
            SafeSetStat(XianTu2.base_stats , strings.S.damage, 100f);
            SafeSetStat(XianTu2.base_stats, strings.S.mass, 10f);
            SafeSetStat(XianTu2.base_stats , strings.S.health, 2000f);
            SafeSetStat(XianTu2.base_stats , strings.S.accuracy, 5f);
            SafeSetStat(XianTu2.base_stats , strings.S.multiplier_speed, 0.3f);
            SafeSetStat(XianTu2.base_stats , strings.S.stamina, 40f);
            SafeSetStat(XianTu2.base_stats, "Dodge", 150f);
            SafeSetStat(XianTu2.base_stats, "Accuracy", 130f);
            XianTu2.action_special_effect += traitAction.XianTu3_effectAction;
            XianTu2.action_special_effect += traitAction.XianTu2_Regen;
            // 添加购买化金丹功能
            XianTu2.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyTupoDanyao2(); return false; };
            // 添加购买青元丹功能
            XianTu2.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyXiulianDanyao2(); return false; };
            // 添加购买精铁剑功能
            XianTu2.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyFineIronSword(); return false; };
            // 添加灵石自动转换功能
            XianTu2.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.ConvertLingShiToZhongPin(); pTarget.a.ConvertZhongPinToShangPin(); return false; } return false; };
            // 添加自动购买对应境界武器机制
            XianTu2.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.AutoBuyRealmWeapon(); return false; } return false; };
            // 绑定法术级别的真伤攻击机制
            XianTu2.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定基于真元的真实伤害攻击机制
            XianTu2.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            AssetManager.traits.add(XianTu2);

            ActorTrait XianTu3 = CreateTrait("XianTu3", "trait/XianTu3", "XianTu");
            SafeSetStat(XianTu3.base_stats, stats.Resist.id, 3.0f);  
            SafeSetStat(XianTu3.base_stats , strings.S.damage, 500f);
            SafeSetStat(XianTu3.base_stats, strings.S.mass, 10f);
            SafeSetStat(XianTu3.base_stats , strings.S.health, 10000f);
            SafeSetStat(XianTu3.base_stats , strings.S.armor, 10f);
            SafeSetStat(XianTu3.base_stats , strings.S.targets, 0.2f);
            SafeSetStat(XianTu3.base_stats , strings.S.accuracy, 8f);
            SafeSetStat(XianTu3.base_stats , strings.S.multiplier_speed, 0.4f);
            SafeSetStat(XianTu3.base_stats , strings.S.attack_speed, 1f);
            SafeSetStat(XianTu3.base_stats , strings.S.stamina, 50f);
            SafeSetStat(XianTu3.base_stats, "Dodge", 200f);
            SafeSetStat(XianTu3.base_stats, "Accuracy", 150f);
            XianTu3.action_special_effect += traitAction.XianTu4_effectAction;
            XianTu3.action_special_effect += traitAction.XianTu3_Regen;
            // 添加购买结婴丹功能
            XianTu3.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyTupoDanyao3(); return false; };
            // 添加购买地煞丹功能
            XianTu3.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyXiulianDanyao3(); return false; };
            // 添加灵石自动转换功能
            XianTu3.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.ConvertLingShiToZhongPin(); pTarget.a.ConvertZhongPinToShangPin(); return false; } return false; };
            // 添加自动购买对应境界武器机制
            XianTu3.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.AutoBuyRealmWeapon(); return false; } return false; };
            // 绑定小神通级别的真伤攻击机制
            XianTu3.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定基于真元的真实伤害攻击机制
            XianTu3.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            AssetManager.traits.add(XianTu3);

            ActorTrait XianTu4 = CreateTrait("XianTu4", "trait/XianTu4", "XianTu");
            SafeSetStat(XianTu4.base_stats, stats.Resist.id, 6.0f);
            SafeSetStat(XianTu4.base_stats , strings.S.damage, 1000f);
            SafeSetStat(XianTu4.base_stats, strings.S.mass, 12f);
            SafeSetStat(XianTu4.base_stats , strings.S.health, 20000f);
            SafeSetStat(XianTu4.base_stats , strings.S.speed, 8f);
            SafeSetStat(XianTu4.base_stats , strings.S.armor, 15f);
            SafeSetStat(XianTu4.base_stats , strings.S.targets, 1.5f); 
            SafeSetStat(XianTu4.base_stats , strings.S.critical_chance, 0.3f);
            SafeSetStat(XianTu4.base_stats , strings.S.accuracy, 15f);
            SafeSetStat(XianTu4.base_stats , strings.S.multiplier_speed, 0.5f);
            SafeSetStat(XianTu4.base_stats , strings.S.attack_speed, 3f);
            SafeSetStat(XianTu4.base_stats , strings.S.stamina, 60f);
            SafeSetStat(XianTu4.base_stats, "Dodge", 400f);
            SafeSetStat(XianTu4.base_stats, "Accuracy", 350f);
            XianTu4.action_special_effect += traitAction.XianTu5_effectAction;
            XianTu4.action_special_effect += traitAction.XianTu4_Regen;
            // 添加购买凝神丹功能
            XianTu4.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyTupoDanyao4(); return false; };
            // 添加购买天罡丹功能
            XianTu4.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyXiulianDanyao4(); return false; };
            // 添加灵石自动转换功能
            XianTu4.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.ConvertLingShiToZhongPin(); pTarget.a.ConvertZhongPinToShangPin(); return false; } return false; };
            // 添加自动购买对应境界武器机制
            XianTu4.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.AutoBuyRealmWeapon(); return false; } return false; };
            // 绑定小神通级别的真伤攻击机制
            XianTu4.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定基于真元的真实伤害攻击机制
            XianTu4.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            AssetManager.traits.add(XianTu4);

            ActorTrait XianTu5 = CreateTrait("XianTu5", "trait/XianTu5", "XianTu");
            SafeSetStat(XianTu5.base_stats, stats.Resist.id, 10.0f);
            SafeSetStat(XianTu5.base_stats , strings.S.warfare, 15f);
            SafeSetStat(XianTu5.base_stats, strings.S.mass, 18f);
            SafeSetStat(XianTu5.base_stats , strings.S.damage, 5000f);
            SafeSetStat(XianTu5.base_stats , strings.S.health, 100000f);
            SafeSetStat(XianTu5.base_stats , strings.S.speed, 12f);
            SafeSetStat(XianTu5.base_stats , strings.S.armor, 20f);
            SafeSetStat(XianTu5.base_stats , strings.S.targets, 3f);
            SafeSetStat(XianTu5.base_stats , strings.S.critical_chance, 0.4f);
            SafeSetStat(XianTu5.base_stats , strings.S.accuracy, 25f);
            SafeSetStat(XianTu5.base_stats , strings.S.multiplier_speed, 0.6f);
            SafeSetStat(XianTu5.base_stats , strings.S.attack_speed, 6f);
            SafeSetStat(XianTu5.base_stats , strings.S.stamina, 80f);
            SafeSetStat(XianTu5.base_stats, "Dodge", 600f);
            SafeSetStat(XianTu5.base_stats, "Accuracy", 550f);
            XianTu5.action_special_effect += traitAction.XianTu6_effectAction;
            XianTu5.action_special_effect += traitAction.XianTu5_Regen;
            // 添加购买合体丹功能
            XianTu5.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyTupoDanyao5(); return false; };
            // 添加购买日月丹功能
            XianTu5.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyXiulianDanyao5(); return false; };
            // 添加灵石自动转换功能
            XianTu5.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.ConvertLingShiToZhongPin(); pTarget.a.ConvertZhongPinToShangPin(); return false; } return false; };
            // 添加自动购买对应境界武器机制
            XianTu5.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.AutoBuyRealmWeapon(); return false; } return false; };
            // 绑定大神通级别的真伤攻击机制
            XianTu5.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定基于真元的真实伤害攻击机制
            XianTu5.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            AssetManager.traits.add(XianTu5);

            ActorTrait XianTu6 = CreateTrait("XianTu6", "trait/XianTu6", "XianTu");
            XianTu6.rarity = Rarity.R2_Epic;
            SafeSetStat(XianTu6.base_stats, stats.Resist.id, 18.0f);
            SafeSetStat(XianTu6.base_stats , strings.S.warfare, 25f);
            SafeSetStat(XianTu6.base_stats, strings.S.mass, 22f);
            SafeSetStat(XianTu6.base_stats , strings.S.damage, 10000f);
            SafeSetStat(XianTu6.base_stats , strings.S.armor, 30f);
            SafeSetStat(XianTu6.base_stats , strings.S.health, 200000f);
            SafeSetStat(XianTu6.base_stats , strings.S.speed, 18f);
            SafeSetStat(XianTu6.base_stats , strings.S.area_of_effect, 1.5f);
            SafeSetStat(XianTu6.base_stats , strings.S.targets, 6f);
            SafeSetStat(XianTu6.base_stats , strings.S.critical_chance, 0.55f);
            SafeSetStat(XianTu6.base_stats , strings.S.accuracy, 35f);
            SafeSetStat(XianTu6.base_stats , strings.S.multiplier_speed, 0.7f);
            SafeSetStat(XianTu6.base_stats , strings.S.attack_speed, 7f);
            SafeSetStat(XianTu6.base_stats , strings.S.stamina, 100f);
            SafeSetStat(XianTu6.base_stats, "Dodge", 800f);
            SafeSetStat(XianTu6.base_stats, "Accuracy", 750f);
            XianTu6.action_special_effect += traitAction.XianTu7_effectAction;
            XianTu6.action_special_effect += traitAction.XianTu6_Regen;
            // 绑定大神通级别的真伤攻击机制
            XianTu6.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定基于真元的真实伤害攻击机制
            XianTu6.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            // 添加购买大乘丹功能
            XianTu6.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyTupoDanyao6(); return false; };
            // 添加购买法则丹功能
            XianTu6.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyXiulianDanyao6(); return false; };
            // 添加购买紫霄剑功能
            XianTu6.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyPurpleCloudSword(); return false; };
            // 添加灵石自动转换功能
            XianTu6.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.ConvertLingShiToZhongPin(); pTarget.a.ConvertZhongPinToShangPin(); return false; } return false; };
            // 添加自动购买对应境界武器机制
            XianTu6.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.AutoBuyRealmWeapon(); return false; } return false; };
            // 添加饱食度维护功能
            XianTu6.action_special_effect += traitAction.MaintainFullNutrition;
            AssetManager.traits.add(XianTu6);

            ActorTrait XianTu7 = CreateTrait("XianTu7", "trait/XianTu7", "XianTu");
            XianTu7.rarity = Rarity.R2_Epic;
            SafeSetStat(XianTu7.base_stats, stats.Resist.id, 25.0f);
            SafeSetStat(XianTu7.base_stats, strings.S.warfare, 35f);
            SafeSetStat(XianTu7.base_stats, strings.S.damage, 50000f);
            SafeSetStat(XianTu7.base_stats, strings.S.mass, 28f);
            SafeSetStat(XianTu7.base_stats, strings.S.armor, 40f);
            SafeSetStat(XianTu7.base_stats, strings.S.health, 1000000f);
            SafeSetStat(XianTu7.base_stats, strings.S.speed, 22f);
            SafeSetStat(XianTu7.base_stats, strings.S.area_of_effect, 2.5f);
            SafeSetStat(XianTu7.base_stats, strings.S.targets, 8f);
            SafeSetStat(XianTu7.base_stats, strings.S.critical_chance, 0.65f);
            SafeSetStat(XianTu7.base_stats, strings.S.accuracy, 45f);
            SafeSetStat(XianTu7.base_stats, strings.S.multiplier_speed, 0.9f);
            SafeSetStat(XianTu7.base_stats, strings.S.stamina, 160f);
            SafeSetStat(XianTu7.base_stats, strings.S.range, 3f);
            SafeSetStat(XianTu7.base_stats, strings.S.attack_speed, 8f);
            SafeSetStat(XianTu7.base_stats, strings.S.scale, 0.05f);
            SafeSetStat(XianTu7.base_stats, strings.S.multiplier_health, 0.3f);
            SafeSetStat(XianTu7.base_stats, strings.S.multiplier_damage, 0.3f);
            SafeSetStat(XianTu7.base_stats, "Dodge", 1000f);
            SafeSetStat(XianTu7.base_stats, "Accuracy", 900f);
            // 绑定天地大神通级别的真伤攻击机制
            XianTu7.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定基于真元的真实伤害攻击机制
            XianTu7.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            XianTu7.action_special_effect += traitAction.XianTu8_effectAction;
            XianTu7.action_special_effect += traitAction.XianTu7_Regen;
            // 添加购买地仙丹功能
            XianTu7.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyTupoDanyao7(); return false; };
            // 添加购买窃天丹功能
            XianTu7.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyXiulianDanyao7(); return false; };
            // 添加购买太初剑功能
            XianTu7.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyPrimalSword(); return false; };
            // 添加灵石自动转换功能
            XianTu7.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.ConvertLingShiToZhongPin(); pTarget.a.ConvertZhongPinToShangPin(); return false; } return false; };
            // 添加自动购买对应境界武器机制
            XianTu7.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.AutoBuyRealmWeapon(); return false; } return false; };
            // 添加饱食度维护功能
            XianTu7.action_special_effect += traitAction.MaintainFullNutrition;
            AssetManager.traits.add(XianTu7);

            ActorTrait XianTu8 = CreateTrait("XianTu8", "trait/XianTu8", "XianTu");
            XianTu8.rarity = Rarity.R2_Epic;
            SafeSetStat(XianTu8.base_stats, stats.Resist.id, 35.0f);
            SafeSetStat(XianTu8.base_stats, strings.S.warfare, 45f);
            SafeSetStat(XianTu8.base_stats, strings.S.damage, 100000f);
            SafeSetStat(XianTu8.base_stats, strings.S.mass, 35f);
            SafeSetStat(XianTu8.base_stats, strings.S.armor, 50f);
            SafeSetStat(XianTu8.base_stats, strings.S.health, 2000000f);
            SafeSetStat(XianTu8.base_stats, strings.S.speed, 50f);
            SafeSetStat(XianTu8.base_stats, strings.S.area_of_effect, 10.0f);
            SafeSetStat(XianTu8.base_stats, strings.S.targets, 10f);
            SafeSetStat(XianTu8.base_stats, strings.S.critical_chance, 0.75f);
            SafeSetStat(XianTu8.base_stats, strings.S.accuracy, 55f);
            SafeSetStat(XianTu8.base_stats, strings.S.multiplier_speed, 1.2f);
            SafeSetStat(XianTu8.base_stats, strings.S.stamina, 280f);
            SafeSetStat(XianTu8.base_stats, strings.S.range, 5f);
            SafeSetStat(XianTu8.base_stats, strings.S.attack_speed, 9f);
            SafeSetStat(XianTu8.base_stats, strings.S.multiplier_health, 0.5f);
            SafeSetStat(XianTu8.base_stats, strings.S.multiplier_damage, 0.5f);
            SafeSetStat(XianTu8.base_stats, strings.S.scale, 0.07f);
            SafeSetStat(XianTu8.base_stats, "Dodge", 2000f);
            SafeSetStat(XianTu8.base_stats, "Accuracy", 1900f);
            // 绑定无上妙法神通级别的真伤攻击机制
            XianTu8.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定基于真元的真实伤害攻击机制
            XianTu8.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            XianTu8.action_special_effect += traitAction.XianTu9_effectAction; // 暂时隐藏XianTu9相关代码
            XianTu8.action_special_effect += traitAction.XianTu8_Regen;
            // 添加统一的道果授予机制
            XianTu8.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) return traitAction.GrantDaoGuoIfEligible(pTarget.a); return false; };
            // 添加灵石自动转换功能
            XianTu8.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.ConvertLingShiToZhongPin(); pTarget.a.ConvertZhongPinToShangPin(); return false; } return false; };
            // 添加饱食度维护功能
            XianTu8.action_special_effect += traitAction.MaintainFullNutrition;
            // 添加购买界天丹功能
            XianTu8.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyXiulianDanyao8(); return false; };
            AssetManager.traits.add(XianTu8);

            ActorTrait XianTu9 = CreateTrait("XianTu9", "trait/XianTu9", "XianTu");
            XianTu9.rarity = Rarity.R2_Epic;
            SafeSetStat(XianTu9.base_stats, stats.Resist.id, 128.0f);
            SafeSetStat(XianTu9.base_stats, strings.S.warfare, 50f);
            SafeSetStat(XianTu9.base_stats, strings.S.damage, 300000f);
            SafeSetStat(XianTu9.base_stats, strings.S.mass, 140f);
            SafeSetStat(XianTu9.base_stats, strings.S.armor, 60f);
            SafeSetStat(XianTu9.base_stats, strings.S.health, 5000000f);
            SafeSetStat(XianTu9.base_stats, strings.S.speed, 30f);
            SafeSetStat(XianTu9.base_stats, strings.S.area_of_effect, 4f);
            SafeSetStat(XianTu9.base_stats, strings.S.targets, 12f);
            SafeSetStat(XianTu9.base_stats, strings.S.critical_chance, 0.8f);
            SafeSetStat(XianTu9.base_stats, strings.S.accuracy, 60f);
            SafeSetStat(XianTu9.base_stats, strings.S.multiplier_speed, 1.2f);
            SafeSetStat(XianTu9.base_stats, strings.S.stamina, 400f);
            SafeSetStat(XianTu9.base_stats, strings.S.range, 6f);
            SafeSetStat(XianTu9.base_stats, strings.S.attack_speed, 6f);
            SafeSetStat(XianTu9.base_stats, strings.S.scale, 0.08f); 
            SafeSetStat(XianTu9.base_stats, strings.S.multiplier_health, 0.6f);
            SafeSetStat(XianTu9.base_stats, strings.S.multiplier_damage, 0.6f);
            SafeSetStat(XianTu9.base_stats, "Dodge", 3000f);
            SafeSetStat(XianTu9.base_stats, "Accuracy", 2600f);
            // 绑定无上妙法神通级别的真伤攻击机制
            XianTu9.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定基于真元的真实伤害攻击机制
            XianTu9.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            XianTu9.action_special_effect += traitAction.XianTu91_effectAction;
            XianTu9.action_special_effect += traitAction.XianTu9_Regen;
            // 添加灵石自动转换功能
            XianTu9.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.ConvertLingShiToZhongPin(); pTarget.a.ConvertZhongPinToShangPin(); return false; } return false; };
            // 添加饱食度维护功能
            XianTu9.action_special_effect += traitAction.MaintainFullNutrition;
            // 添加世界强者晋升功能（单独绑定）
            XianTu9.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { traitAction.TriggerWorldStrongPromotion(pTarget.a); return false; } return false; };
            // 添加购买千界丹功能
            XianTu9.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyXiulianDanyao9(); return false; };
            AssetManager.traits.add(XianTu9);    
            
            // 道祖境
            ActorTrait XianTu91 = CreateTrait("XianTu91", "trait/XianTu91", "XianTu");
            XianTu91.rarity = Rarity.R3_Legendary;
            SafeSetStat(XianTu91.base_stats, stats.Resist.id, 256.0f);
            SafeSetStat(XianTu91.base_stats, strings.S.warfare, 100f);
            SafeSetStat(XianTu91.base_stats, strings.S.damage, 500000f);
            SafeSetStat(XianTu91.base_stats, strings.S.mass, 180f);
            SafeSetStat(XianTu91.base_stats, strings.S.armor, 80f);
            SafeSetStat(XianTu91.base_stats, strings.S.health, 10000000f);
            SafeSetStat(XianTu91.base_stats, strings.S.speed, 40f);
            SafeSetStat(XianTu91.base_stats, strings.S.area_of_effect, 5f);
            SafeSetStat(XianTu91.base_stats, strings.S.targets, 16f);
            SafeSetStat(XianTu91.base_stats, strings.S.critical_chance, 0.9f);
            SafeSetStat(XianTu91.base_stats, strings.S.accuracy, 80f);
            SafeSetStat(XianTu91.base_stats, strings.S.multiplier_speed, 1.5f);
            SafeSetStat(XianTu91.base_stats, strings.S.stamina, 500f);
            SafeSetStat(XianTu91.base_stats, strings.S.range, 8f);
            SafeSetStat(XianTu91.base_stats, strings.S.attack_speed, 8f);
            SafeSetStat(XianTu91.base_stats, strings.S.scale, 0.1f); 
            SafeSetStat(XianTu91.base_stats, strings.S.multiplier_health, 1.0f);
            SafeSetStat(XianTu91.base_stats, strings.S.multiplier_damage, 1.0f);
            SafeSetStat(XianTu91.base_stats, "Dodge", 4000f);
            SafeSetStat(XianTu91.base_stats, "Accuracy", 3600f);
            // 绑定无上妙法神通级别的真伤攻击机制
            XianTu91.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定基于真元的真实伤害攻击机制
            XianTu91.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            XianTu91.action_special_effect += traitAction.XianTu9_Regen;
            // 添加灵石自动转换功能
            XianTu91.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.ConvertLingShiToZhongPin(); pTarget.a.ConvertZhongPinToShangPin(); return false; } return false; };
            // 添加饱食度维护功能
            XianTu91.action_special_effect += traitAction.MaintainFullNutrition;
            // 添加世界强者晋升功能（单独绑定）
            XianTu91.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { traitAction.TriggerWorldStrongPromotion(pTarget.a); return false; } return false; };
            // 添加购买千界丹功能
            XianTu91.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) pTarget.a.BuyXiulianDanyao9(); return false; };
            AssetManager.traits.add(XianTu91);
            /* 以下是XianTu9~93的代码，暂时隐藏
            ActorTrait XianTu91 = CreateTrait("XianTu91", "trait/XianTu91", "XianTu");
            XianTu91.rarity = Rarity.R3_Legendary;
            SafeSetStat(XianTu91.base_stats, stats.Resist.id, 160.0f);
            SafeSetStat(XianTu91.base_stats, strings.S.warfare, 70f);
            SafeSetStat(XianTu91.base_stats, strings.S.damage, 400000f);
            SafeSetStat(XianTu91.base_stats, strings.S.mass, 400f);
            SafeSetStat(XianTu91.base_stats, strings.S.armor, 80f);
            SafeSetStat(XianTu91.base_stats, strings.S.health, 8000000f);
            SafeSetStat(XianTu91.base_stats, strings.S.speed, 50f);
            SafeSetStat(XianTu91.base_stats, strings.S.area_of_effect, 5f);
            SafeSetStat(XianTu91.base_stats, strings.S.targets, 15f);
            SafeSetStat(XianTu91.base_stats, strings.S.critical_chance, 0.9f);
            SafeSetStat(XianTu91.base_stats, strings.S.accuracy, 80f);
            SafeSetStat(XianTu91.base_stats, strings.S.multiplier_speed, 1.5f);
            SafeSetStat(XianTu91.base_stats, strings.S.stamina, 600f);
            SafeSetStat(XianTu91.base_stats, strings.S.range, 12f);
            SafeSetStat(XianTu91.base_stats, strings.S.attack_speed, 12f);
            SafeSetStat(XianTu91.base_stats, strings.S.scale, 0.1f);
            SafeSetStat(XianTu91.base_stats, strings.S.multiplier_health, 1.2f);
            SafeSetStat(XianTu91.base_stats, strings.S.multiplier_damage, 1.2f);
            SafeSetStat(XianTu91.base_stats, "Dodge", 5000f);
            SafeSetStat(XianTu91.base_stats, "Accuracy", 4900f);
            // 绑定无上妙法神通级别的真伤攻击机制
            XianTu91.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定基于真元的真实伤害攻击机制
            XianTu91.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            //XianTu91.action_special_effect += traitAction.XianTu92_effectAction;
            XianTu91.action_special_effect += traitAction.XianTu91_Regen;
            XianTu91.action_attack_target += traitAction.fire1_attackAction;
            XianTu91.action_special_effect += traitAction.MaintainFullNutrition;
            // 添加灵石自动转换功能
            XianTu91.action_special_effect += (pTarget, pTile) => { if (pTarget != null && pTarget.isActor()) { pTarget.a.ConvertLingShiToZhongPin(); pTarget.a.ConvertZhongPinToShangPin(); return false; } return false; };
            // 添加饱食度维护功能
            XianTu91.action_special_effect += traitAction.MaintainFullNutrition;
            AssetManager.traits.add(XianTu91);


            /* 以下是XianTu9~93的代码，暂时隐藏
            ActorTrait XianTu92 = CreateTrait("XianTu92", "trait/XianTu92", "XianTu");
            XianTu92.rarity = Rarity.R3_Legendary;
            SafeSetStat(XianTu92.base_stats, stats.Resist.id, 200.0f);
            SafeSetStat(XianTu92.base_stats, strings.S.warfare, 100f);
            SafeSetStat(XianTu92.base_stats, strings.S.damage, 600000f);
            SafeSetStat(XianTu92.base_stats, strings.S.mass, 1600f);
            SafeSetStat(XianTu92.base_stats, strings.S.armor, 100f);
            SafeSetStat(XianTu92.base_stats, strings.S.health, 6000000f);
            SafeSetStat(XianTu92.base_stats, strings.S.speed, 100f);
            SafeSetStat(XianTu92.base_stats, strings.S.area_of_effect, 7f);
            SafeSetStat(XianTu92.base_stats, strings.S.targets, 20f);
            SafeSetStat(XianTu92.base_stats, strings.S.critical_chance, 1.0f);
            SafeSetStat(XianTu92.base_stats, strings.S.accuracy, 100f);
            SafeSetStat(XianTu92.base_stats, strings.S.multiplier_speed, 2.0f);
            SafeSetStat(XianTu92.base_stats, strings.S.stamina, 1000f);
            SafeSetStat(XianTu92.base_stats, strings.S.range, 20f);
            SafeSetStat(XianTu92.base_stats, strings.S.attack_speed, 20f);
            SafeSetStat(XianTu92.base_stats, strings.S.scale, 0.15f);
            SafeSetStat(XianTu92.base_stats, strings.S.multiplier_health, 2f);
            SafeSetStat(XianTu92.base_stats, strings.S.multiplier_damage, 2f);
            SafeSetStat(XianTu92.base_stats, "Dodge", 380f);
            SafeSetStat(XianTu92.base_stats, "Accuracy", 340f);
            XianTu92.action_special_effect += traitAction.XianTu93_effectAction;
            XianTu92.action_special_effect += traitAction.XianTu92_Regen;
            XianTu92.action_attack_target += traitAction.TrueDamage6_AttackAction;
            XianTu92.action_attack_target += traitAction.fire1_attackAction;
            XianTu92.action_attack_target += traitAction.TrueDamageByXianTu2_AttackAction;
            XianTu92.action_special_effect += traitAction.MaintainFullNutrition;
            AssetManager.traits.add(XianTu92);

            ActorTrait XianTu91 = CreateTrait("XianTu93", "trait/XianTu93", "XianTu");
            XianTu93.rarity = Rarity.R3_Legendary;
            SafeSetStat(XianTu93.base_stats, stats.Resist.id, 240.0f);
            SafeSetStat(XianTu93.base_stats, strings.S.warfare, 160f);
            SafeSetStat(XianTu93.base_stats, strings.S.damage, 1000000f);
            SafeSetStat(XianTu93.base_stats, strings.S.mass, 2400f);
            SafeSetStat(XianTu93.base_stats, strings.S.armor, 160f);
            SafeSetStat(XianTu93.base_stats, strings.S.health, 10000000f);
            SafeSetStat(XianTu93.base_stats, strings.S.speed, 300f);    
            SafeSetStat(XianTu93.base_stats, strings.S.area_of_effect, 10f);
            SafeSetStat(XianTu93.base_stats, strings.S.targets, 25f);
            SafeSetStat(XianTu93.base_stats, strings.S.critical_chance, 1.5f);
            SafeSetStat(XianTu93.base_stats, strings.S.accuracy, 160f);
            SafeSetStat(XianTu93.base_stats, strings.S.multiplier_speed, 3.0f);
            SafeSetStat(XianTu93.base_stats, strings.S.stamina, 10000f);
            SafeSetStat(XianTu93.base_stats, strings.S.range, 30f);
            SafeSetStat(XianTu93.base_stats, strings.S.attack_speed, 30f);
            SafeSetStat(XianTu93.base_stats, strings.S.scale, 0.2f);
            SafeSetStat(XianTu93.base_stats, strings.S.multiplier_health, 3f);
            SafeSetStat(XianTu93.base_stats, strings.S.multiplier_damage, 3f);    
            SafeSetStat(XianTu93.base_stats, "Dodge", 500f);
            SafeSetStat(XianTu93.base_stats, "Accuracy", 480f);
            XianTu93.action_special_effect += traitAction.XianTu93_Regen;
            XianTu93.action_attack_target += traitAction.TrueDamage7_AttackAction;
            XianTu93.action_attack_target += traitAction.fire2_attackAction;
            XianTu93.action_attack_target += traitAction.TrueDamageByXianTu3_AttackAction;
            XianTu93.action_special_effect += traitAction.MaintainFullNutrition;
            AssetManager.traits.add(XianTu93);
            */

            ActorTrait TaiyiLg1 = CreateTrait("TaiyiLg1", "trait/TaiyiLg1", "TaiyiLg");
        AssetManager.traits.add(TaiyiLg1);

            // DaoGuo系列特质
            ActorTrait DaoGuo1 = CreateTrait("DaoGuo1", "trait/DaoGuo1", "DaoGuo");
            DaoGuo1.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoGuo1.base_stats, strings.S.multiplier_damage, 5.0f);
            SafeSetStat(DaoGuo1.base_stats, strings.S.multiplier_health, 5.0f);
            SafeSetStat(DaoGuo1.base_stats, strings.S.intelligence, 100f);
            SafeSetStat(DaoGuo1.base_stats, strings.S.lifespan, 180f);
            SafeSetStat(DaoGuo1.base_stats, strings.S.stamina, 300f);
            SafeSetStat(DaoGuo1.base_stats, strings.S.area_of_effect, 8f);
            SafeSetStat(DaoGuo1.base_stats, strings.S.critical_chance, 0.2f);
            SafeSetStat(DaoGuo1.base_stats, strings.S.targets, 10f);
            SafeSetStat(DaoGuo1.base_stats, strings.S.accuracy, 60f);
            SafeSetStat(DaoGuo1.base_stats, "Accuracy", 50f);
            SafeSetStat(DaoGuo1.base_stats, "Dodge", 50f);
            // 绑定10倍生命真伤攻击动作
            DaoGuo1.action_attack_target += traitAction.DaoGuoTrueDamage_AttackAction;
            AssetManager.traits.add(DaoGuo1);

            ActorTrait DaoGuo2 = CreateTrait("DaoGuo2", "trait/DaoGuo2", "DaoGuo");
            DaoGuo2.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoGuo2.base_stats, strings.S.multiplier_damage, 5.0f);
            SafeSetStat(DaoGuo2.base_stats, strings.S.multiplier_health, 5.0f);
            SafeSetStat(DaoGuo2.base_stats, strings.S.intelligence, 40f);
            SafeSetStat(DaoGuo2.base_stats, strings.S.lifespan, 120f);
            SafeSetStat(DaoGuo2.base_stats, strings.S.stamina, 200f);
            SafeSetStat(DaoGuo2.base_stats, strings.S.area_of_effect, 18f);
            SafeSetStat(DaoGuo2.base_stats, strings.S.critical_chance, 0.8f);
            SafeSetStat(DaoGuo2.base_stats, strings.S.targets, 15f);
            SafeSetStat(DaoGuo2.base_stats, strings.S.accuracy, 40f);
            SafeSetStat(DaoGuo2.base_stats, "Accuracy", 40f);
            SafeSetStat(DaoGuo2.base_stats, "Dodge", 60f);
            // 绑定10倍生命真伤攻击动作
            DaoGuo2.action_attack_target += traitAction.DaoGuoTrueDamage_AttackAction;
            AssetManager.traits.add(DaoGuo2);

            ActorTrait DaoGuo3 = CreateTrait("DaoGuo3", "trait/DaoGuo3", "DaoGuo");
            DaoGuo3.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoGuo3.base_stats, strings.S.multiplier_damage, 5.0f);
            SafeSetStat(DaoGuo3.base_stats, strings.S.multiplier_health, 5.0f);
            SafeSetStat(DaoGuo3.base_stats, strings.S.intelligence, 120f);
            SafeSetStat(DaoGuo3.base_stats, strings.S.lifespan, 150f);
            SafeSetStat(DaoGuo3.base_stats, strings.S.stamina, 240f);
            SafeSetStat(DaoGuo3.base_stats, strings.S.area_of_effect, 6f);
            SafeSetStat(DaoGuo3.base_stats, strings.S.critical_chance, 0.5f);
            SafeSetStat(DaoGuo3.base_stats, strings.S.targets, 8f);
            SafeSetStat(DaoGuo3.base_stats, strings.S.accuracy, 50f);
            SafeSetStat(DaoGuo3.base_stats, "Accuracy", 60f);
            SafeSetStat(DaoGuo3.base_stats, "Dodge", 40f);
            // 绑定10倍生命真伤攻击动作
            DaoGuo3.action_attack_target += traitAction.DaoGuoTrueDamage_AttackAction;
            AssetManager.traits.add(DaoGuo3);

            ActorTrait DaoGuo4 = CreateTrait("DaoGuo4", "trait/DaoGuo4", "DaoGuo");
            DaoGuo4.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoGuo4.base_stats, strings.S.multiplier_damage, 5.0f);
            SafeSetStat(DaoGuo4.base_stats, strings.S.multiplier_health, 5.0f);
            SafeSetStat(DaoGuo4.base_stats, strings.S.intelligence, 60f);
            SafeSetStat(DaoGuo4.base_stats, strings.S.lifespan, 140f);
            SafeSetStat(DaoGuo4.base_stats, strings.S.stamina, 240f);
            SafeSetStat(DaoGuo4.base_stats, strings.S.area_of_effect, 6f);
            SafeSetStat(DaoGuo4.base_stats, strings.S.critical_chance, 0.5f);
            SafeSetStat(DaoGuo4.base_stats, strings.S.targets, 15f);
            SafeSetStat(DaoGuo4.base_stats, strings.S.accuracy, 60f);
            SafeSetStat(DaoGuo4.base_stats, "Accuracy", 70f);
            SafeSetStat(DaoGuo4.base_stats, "Dodge", 30f);
            // 绑定10倍生命真伤攻击动作
            DaoGuo4.action_attack_target += traitAction.DaoGuoTrueDamage_AttackAction;
            AssetManager.traits.add(DaoGuo4);

            ActorTrait DaoGuo5 = CreateTrait("DaoGuo5", "trait/DaoGuo5", "DaoGuo");
            DaoGuo5.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoGuo5.base_stats, strings.S.multiplier_damage, 5.0f);
            SafeSetStat(DaoGuo5.base_stats, strings.S.multiplier_health, 5.0f);
            SafeSetStat(DaoGuo5.base_stats, strings.S.intelligence, 260f);
            SafeSetStat(DaoGuo5.base_stats, strings.S.lifespan, 140f);
            SafeSetStat(DaoGuo5.base_stats, strings.S.stamina, 40f);
            SafeSetStat(DaoGuo5.base_stats, strings.S.area_of_effect, 2f);
            SafeSetStat(DaoGuo5.base_stats, strings.S.critical_chance, 0.1f);
            SafeSetStat(DaoGuo5.base_stats, strings.S.targets, 10f);
            SafeSetStat(DaoGuo5.base_stats, strings.S.accuracy, 20f);
            SafeSetStat(DaoGuo5.base_stats, strings.S.targets, 20f);
            SafeSetStat(DaoGuo5.base_stats, strings.S.accuracy, 45f);
            SafeSetStat(DaoGuo5.base_stats, "Accuracy", 80f);
            SafeSetStat(DaoGuo5.base_stats, "Dodge", 20f);
            // 绑定10倍生命真伤攻击动作
            DaoGuo5.action_attack_target += traitAction.DaoGuoTrueDamage_AttackAction;
            AssetManager.traits.add(DaoGuo5);

            ActorTrait DaoGuo6 = CreateTrait("DaoGuo6", "trait/DaoGuo6", "DaoGuo");
            DaoGuo6.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoGuo6.base_stats, strings.S.multiplier_health, 5.0f);
            SafeSetStat(DaoGuo6.base_stats, strings.S.multiplier_damage, 5.0f);
            SafeSetStat(DaoGuo6.base_stats, strings.S.intelligence, 20f);
            SafeSetStat(DaoGuo6.base_stats, strings.S.lifespan, 200f);
            SafeSetStat(DaoGuo6.base_stats, strings.S.stamina, 240f);
            SafeSetStat(DaoGuo6.base_stats, strings.S.area_of_effect, 12f);
            SafeSetStat(DaoGuo6.base_stats, strings.S.critical_chance, 0.4f);
            SafeSetStat(DaoGuo6.base_stats, strings.S.targets, 15f);
            SafeSetStat(DaoGuo6.base_stats, strings.S.accuracy, 60f);
            SafeSetStat(DaoGuo6.base_stats, "Accuracy", 90f);
            SafeSetStat(DaoGuo6.base_stats, "Dodge", 10f);
            // 绑定10倍生命真伤攻击动作
            DaoGuo6.action_attack_target += traitAction.DaoGuoTrueDamage_AttackAction;
            AssetManager.traits.add(DaoGuo6);

            ActorTrait DaoGuo7 = CreateTrait("DaoGuo7", "trait/DaoGuo7", "DaoGuo");
            DaoGuo7.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoGuo7.base_stats, strings.S.multiplier_health, 5.0f);
            SafeSetStat(DaoGuo7.base_stats, strings.S.multiplier_damage, 5.0f);
            SafeSetStat(DaoGuo7.base_stats, strings.S.intelligence, 320f);
            SafeSetStat(DaoGuo7.base_stats, strings.S.lifespan, 160f);
            SafeSetStat(DaoGuo7.base_stats, strings.S.stamina, 500f);
            SafeSetStat(DaoGuo7.base_stats, strings.S.area_of_effect, 20f);
            SafeSetStat(DaoGuo7.base_stats, strings.S.critical_chance, 0.8f);
            SafeSetStat(DaoGuo7.base_stats, strings.S.targets, 24f);
            SafeSetStat(DaoGuo7.base_stats, strings.S.accuracy, 64f);
            SafeSetStat(DaoGuo7.base_stats, "Accuracy", 40f);
            SafeSetStat(DaoGuo7.base_stats, "Dodge", 60f);
            // 绑定10倍生命真伤攻击动作
            DaoGuo7.action_attack_target += traitAction.DaoGuoTrueDamage_AttackAction;
            AssetManager.traits.add(DaoGuo7);

            ActorTrait DaoGuo8 = CreateTrait("DaoGuo8", "trait/DaoGuo8", "DaoGuo");
            DaoGuo8.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoGuo8.base_stats, strings.S.multiplier_health, 5.0f);
            SafeSetStat(DaoGuo8.base_stats, strings.S.multiplier_damage, 5.0f);
            SafeSetStat(DaoGuo8.base_stats, strings.S.intelligence, 120f);
            SafeSetStat(DaoGuo8.base_stats, strings.S.lifespan, 200f);
            SafeSetStat(DaoGuo8.base_stats, strings.S.stamina, 300f);
            SafeSetStat(DaoGuo8.base_stats, strings.S.area_of_effect, 10f);
            SafeSetStat(DaoGuo8.base_stats, strings.S.critical_chance, 0.6f);
            SafeSetStat(DaoGuo8.base_stats, strings.S.targets, 25f);
            SafeSetStat(DaoGuo8.base_stats, strings.S.accuracy, 70f);
            SafeSetStat(DaoGuo8.base_stats, "Accuracy", 30f);
            SafeSetStat(DaoGuo8.base_stats, "Dodge", 70f);
            // 绑定10倍生命真伤攻击动作
            DaoGuo8.action_attack_target += traitAction.DaoGuoTrueDamage_AttackAction;
            AssetManager.traits.add(DaoGuo8);

            ActorTrait DaoGuo9 = CreateTrait("DaoGuo9", "trait/DaoGuo9", "DaoGuo");
            DaoGuo9.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoGuo9.base_stats, strings.S.multiplier_health, 5.0f);
            SafeSetStat(DaoGuo9.base_stats, strings.S.multiplier_damage, 5.0f);
            SafeSetStat(DaoGuo9.base_stats, strings.S.intelligence, 108f);
            SafeSetStat(DaoGuo9.base_stats, strings.S.lifespan, 125f);
            SafeSetStat(DaoGuo9.base_stats, strings.S.stamina, 225f);
            SafeSetStat(DaoGuo9.base_stats, strings.S.area_of_effect, 22f);
            SafeSetStat(DaoGuo9.base_stats, strings.S.critical_chance, 0.36f);
            SafeSetStat(DaoGuo9.base_stats, strings.S.targets, 72f);
            SafeSetStat(DaoGuo9.base_stats, strings.S.accuracy, 50f);
            SafeSetStat(DaoGuo9.base_stats, "Accuracy", 20f);
            SafeSetStat(DaoGuo9.base_stats, "Dodge", 80f);
            // 绑定10倍生命真伤攻击动作
            DaoGuo9.action_attack_target += traitAction.DaoGuoTrueDamage_AttackAction;
            AssetManager.traits.add(DaoGuo9);

            ActorTrait DaoGuo91 = CreateTrait("DaoGuo91", "trait/DaoGuo91", "DaoGuo");
            DaoGuo91.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoGuo91.base_stats, strings.S.multiplier_damage, 5.0f);
            SafeSetStat(DaoGuo91.base_stats, strings.S.multiplier_health, 5.0f);
            SafeSetStat(DaoGuo91.base_stats, strings.S.intelligence, 130f);
            SafeSetStat(DaoGuo91.base_stats, strings.S.lifespan, 160f);
            SafeSetStat(DaoGuo91.base_stats, strings.S.stamina, 500f);
            SafeSetStat(DaoGuo91.base_stats, strings.S.area_of_effect, 28f);
            SafeSetStat(DaoGuo91.base_stats, strings.S.critical_chance, 0.24f);
            SafeSetStat(DaoGuo91.base_stats, strings.S.targets, 49f);
            SafeSetStat(DaoGuo91.base_stats, strings.S.accuracy, 50f);
            SafeSetStat(DaoGuo91.base_stats, "Accuracy", 10f);
            SafeSetStat(DaoGuo91.base_stats, "Dodge", 90f);
            // 绑定10倍生命真伤攻击动作
            DaoGuo91.action_attack_target += traitAction.DaoGuoTrueDamage_AttackAction;
            AssetManager.traits.add(DaoGuo91);

            ActorTrait DaoGuo92 = CreateTrait("DaoGuo92", "trait/DaoGuo92", "DaoGuo");
            DaoGuo92.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoGuo92.base_stats, strings.S.multiplier_damage, 5.0f);
            SafeSetStat(DaoGuo92.base_stats, strings.S.multiplier_health, 5.0f);
            SafeSetStat(DaoGuo92.base_stats, strings.S.intelligence, 90f);
            SafeSetStat(DaoGuo92.base_stats, strings.S.lifespan, 180f);
            SafeSetStat(DaoGuo92.base_stats, strings.S.stamina, 160f);
            SafeSetStat(DaoGuo92.base_stats, strings.S.area_of_effect, 19f);
            SafeSetStat(DaoGuo92.base_stats, strings.S.critical_chance, 0.64f);
            SafeSetStat(DaoGuo92.base_stats, strings.S.targets, 20f);
            SafeSetStat(DaoGuo92.base_stats, strings.S.accuracy, 99f);
            SafeSetStat(DaoGuo92.base_stats, "Accuracy", 55f);
            SafeSetStat(DaoGuo92.base_stats, "Dodge", 45f);
            // 绑定10倍生命真伤攻击动作
            DaoGuo92.action_attack_target += traitAction.DaoGuoTrueDamage_AttackAction;
            AssetManager.traits.add(DaoGuo92);

            ActorTrait DaoGuo93 = CreateTrait("DaoGuo93", "trait/DaoGuo93", "DaoGuo");
            DaoGuo93.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoGuo93.base_stats, strings.S.multiplier_damage, 5.0f);
            SafeSetStat(DaoGuo93.base_stats, strings.S.multiplier_health, 5.0f);
            SafeSetStat(DaoGuo93.base_stats, strings.S.intelligence, 121f);
            SafeSetStat(DaoGuo93.base_stats, strings.S.lifespan, 199f);
            SafeSetStat(DaoGuo93.base_stats, strings.S.stamina, 250f);
            SafeSetStat(DaoGuo93.base_stats, strings.S.area_of_effect, 30f);
            SafeSetStat(DaoGuo93.base_stats, strings.S.critical_chance, 0.11f);
            SafeSetStat(DaoGuo93.base_stats, strings.S.targets, 100f);
            SafeSetStat(DaoGuo93.base_stats, strings.S.accuracy, 99f);
            SafeSetStat(DaoGuo93.base_stats, "Accuracy", 65f);
            SafeSetStat(DaoGuo93.base_stats, "Dodge", 35f);
            // 绑定10倍生命真伤攻击动作
            DaoGuo93.action_attack_target += traitAction.DaoGuoTrueDamage_AttackAction;
            AssetManager.traits.add(DaoGuo93);

            ActorTrait DaoGuo94 = CreateTrait("DaoGuo94", "trait/DaoGuo94", "DaoGuo");
            DaoGuo94.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoGuo94.base_stats, strings.S.multiplier_damage, 5.0f);
            SafeSetStat(DaoGuo94.base_stats, strings.S.multiplier_health, 5.0f);
            SafeSetStat(DaoGuo94.base_stats, strings.S.intelligence, 81f);
            SafeSetStat(DaoGuo94.base_stats, strings.S.lifespan, 125f);
            SafeSetStat(DaoGuo94.base_stats, strings.S.stamina, 144f);
            SafeSetStat(DaoGuo94.base_stats, strings.S.area_of_effect, 16f);
            SafeSetStat(DaoGuo94.base_stats, strings.S.critical_chance, 0.49f);
            SafeSetStat(DaoGuo94.base_stats, strings.S.targets, 14f);
            SafeSetStat(DaoGuo94.base_stats, strings.S.accuracy, 49f);
            SafeSetStat(DaoGuo94.base_stats, "Accuracy", 75f);
            SafeSetStat(DaoGuo94.base_stats, "Dodge", 25f);
            // 绑定10倍生命真伤攻击动作
            DaoGuo94.action_attack_target += traitAction.DaoGuoTrueDamage_AttackAction;
            AssetManager.traits.add(DaoGuo94);

            ActorTrait DaoGuo95 = CreateTrait("DaoGuo95", "trait/DaoGuo95", "DaoGuo");
            DaoGuo95.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoGuo95.base_stats, strings.S.multiplier_damage, 5.0f);
            SafeSetStat(DaoGuo95.base_stats, strings.S.multiplier_health, 5.0f);
            SafeSetStat(DaoGuo95.base_stats, strings.S.intelligence, 188f);
            SafeSetStat(DaoGuo95.base_stats, strings.S.lifespan, 188f);
            SafeSetStat(DaoGuo95.base_stats, strings.S.stamina, 188f);
            SafeSetStat(DaoGuo95.base_stats, strings.S.area_of_effect, 18f);
            SafeSetStat(DaoGuo95.base_stats, strings.S.critical_chance, 0.18f);
            SafeSetStat(DaoGuo95.base_stats, strings.S.targets, 18f);
            SafeSetStat(DaoGuo95.base_stats, strings.S.accuracy, 88f);
            SafeSetStat(DaoGuo95.base_stats, "Accuracy", 85f);
            SafeSetStat(DaoGuo95.base_stats, "Dodge", 15f);
            // 绑定10倍生命真伤攻击动作
            DaoGuo95.action_attack_target += traitAction.DaoGuoTrueDamage_AttackAction;
            AssetManager.traits.add(DaoGuo95);
            
            ActorTrait DaoGuo96 = CreateTrait("DaoGuo96", "trait/DaoGuo96", "DaoGuo");
            DaoGuo96.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoGuo96.base_stats, strings.S.multiplier_damage, 5.0f);
            SafeSetStat(DaoGuo96.base_stats, strings.S.multiplier_health, 5.0f);
            SafeSetStat(DaoGuo96.base_stats, strings.S.intelligence, 200f);
            SafeSetStat(DaoGuo96.base_stats, strings.S.lifespan, 200f);
            SafeSetStat(DaoGuo96.base_stats, strings.S.stamina, 200f);
            SafeSetStat(DaoGuo96.base_stats, strings.S.area_of_effect, 20f);
            SafeSetStat(DaoGuo96.base_stats, strings.S.critical_chance, 0.2f);
            SafeSetStat(DaoGuo96.base_stats, strings.S.targets, 20f);
            SafeSetStat(DaoGuo96.base_stats, strings.S.accuracy, 90f);
            SafeSetStat(DaoGuo96.base_stats, "Accuracy", 90f);
            SafeSetStat(DaoGuo96.base_stats, "Dodge", 10f);
            // 绑定10倍生命真伤攻击动作
            DaoGuo96.action_attack_target += traitAction.DaoGuoTrueDamage_AttackAction;
            AssetManager.traits.add(DaoGuo96);
            // 定义五个等级的血脉特质
            ActorTrait TyXuemai1 = CreateTrait("TyXuemai1", "trait/TyXuemai1", "TyXuemai");
            TyXuemai1.rate_inherit = 10;
            SafeSetStat(TyXuemai1.base_stats, strings.S.multiplier_health, 0.01f);
            SafeSetStat(TyXuemai1.base_stats, strings.S.multiplier_damage, 0.01f);
            SafeSetStat(TyXuemai1.base_stats, strings.S.lifespan, 5f);
            AssetManager.traits.add(TyXuemai1);

            ActorTrait TyXuemai2 = CreateTrait("TyXuemai2", "trait/TyXuemai2", "TyXuemai");
            TyXuemai2.rate_inherit = 10;
            SafeSetStat(TyXuemai2.base_stats, strings.S.multiplier_health, 0.05f);
            SafeSetStat(TyXuemai2.base_stats, strings.S.multiplier_damage, 0.05f);
            SafeSetStat(TyXuemai2.base_stats, strings.S.lifespan, 10f);
            AssetManager.traits.add(TyXuemai2);

            ActorTrait TyXuemai3 = CreateTrait("TyXuemai3", "trait/TyXuemai3", "TyXuemai");
            TyXuemai3.rate_inherit = 9; 
            SafeSetStat(TyXuemai3.base_stats, strings.S.multiplier_health, 0.1f);
            SafeSetStat(TyXuemai3.base_stats, strings.S.multiplier_damage, 0.1f);
            SafeSetStat(TyXuemai3.base_stats, strings.S.lifespan, 15f);
            AssetManager.traits.add(TyXuemai3);

            ActorTrait TyXuemai4 = CreateTrait("TyXuemai4", "trait/TyXuemai4", "TyXuemai");
            TyXuemai4.rarity = Rarity.R2_Epic;
            TyXuemai4.rate_inherit = 8;
            SafeSetStat(TyXuemai4.base_stats, strings.S.multiplier_health, 0.15f);
            SafeSetStat(TyXuemai4.base_stats, strings.S.multiplier_damage, 0.15f);
            SafeSetStat(TyXuemai4.base_stats, strings.S.lifespan, 20f);
            AssetManager.traits.add(TyXuemai4);

            ActorTrait TyXuemai5 = CreateTrait("TyXuemai5", "trait/TyXuemai5", "TyXuemai");
            TyXuemai5.rarity = Rarity.R2_Epic;
            TyXuemai5.rate_inherit = 7; 
            SafeSetStat(TyXuemai5.base_stats, strings.S.multiplier_health, 0.2f);
            SafeSetStat(TyXuemai5.base_stats, strings.S.multiplier_damage, 0.2f);
            SafeSetStat(TyXuemai5.base_stats, strings.S.lifespan, 30f);
            AssetManager.traits.add(TyXuemai5);

            ActorTrait TyXuemai6 = CreateTrait("TyXuemai6", "trait/TyXuemai6", "TyXuemai");
            TyXuemai6.rarity = Rarity.R3_Legendary;
            TyXuemai6.rate_inherit = 6; 
            SafeSetStat(TyXuemai6.base_stats, strings.S.multiplier_health, 0.25f);
            SafeSetStat(TyXuemai6.base_stats, strings.S.multiplier_damage, 0.25f);
            SafeSetStat(TyXuemai6.base_stats, strings.S.lifespan, 50f);
            AssetManager.traits.add(TyXuemai6);

            ActorTrait TyXuemai7 = CreateTrait("TyXuemai7", "trait/TyXuemai7", "TyXuemai");
            TyXuemai7.rarity = Rarity.R3_Legendary;
            TyXuemai7.rate_inherit = 5; 
            SafeSetStat(TyXuemai7.base_stats, strings.S.multiplier_health, 0.3f);
            SafeSetStat(TyXuemai7.base_stats, strings.S.multiplier_damage, 0.3f);
            SafeSetStat(TyXuemai7.base_stats, strings.S.lifespan, 100f);
            AssetManager.traits.add(TyXuemai7);

            // 创建TyWudao系列特质 - 武道体系
            // 炼皮
            ActorTrait TyWudao1 = CreateTrait("TyWudao1", "trait/TyWudao1", "TyWudao");
            TyWudao1.rarity = Rarity.R2_Epic;
            SafeSetStat(TyWudao1.base_stats, strings.S.multiplier_damage, 0.10f);
            SafeSetStat(TyWudao1.base_stats, strings.S.multiplier_health, 0.10f);
            SafeSetStat(TyWudao1.base_stats, strings.S.damage, 1f);
            SafeSetStat(TyWudao1.base_stats, strings.S.health, 20f);
            SafeSetStat(TyWudao1.base_stats, strings.S.lifespan, 30f);
            SafeSetStat(TyWudao1.base_stats, strings.S.stamina, 50f);
            SafeSetStat(TyWudao1.base_stats, strings.S.accuracy, 10f);
            SafeSetStat(TyWudao1.base_stats, "Accuracy", 5f);
            SafeSetStat(TyWudao1.base_stats, "Dodge", 4f);
            // 绑定回血机制
            TyWudao1.action_special_effect += WudaoPromotion.Wudao1_Regen;
            AssetManager.traits.add(TyWudao1);

            // 锻骨
            ActorTrait TyWudao2 = CreateTrait("TyWudao2", "trait/TyWudao2", "TyWudao");
            TyWudao2.rarity = Rarity.R2_Epic;
            SafeSetStat(TyWudao2.base_stats, strings.S.multiplier_damage, 0.20f);
            SafeSetStat(TyWudao2.base_stats, strings.S.multiplier_health, 0.20f);
            SafeSetStat(TyWudao2.base_stats, strings.S.damage, 5f);
            SafeSetStat(TyWudao2.base_stats, strings.S.health, 100f);
            SafeSetStat(TyWudao2.base_stats, strings.S.lifespan, 50f);
            SafeSetStat(TyWudao2.base_stats, strings.S.stamina, 80f);
            SafeSetStat(TyWudao2.base_stats, strings.S.accuracy, 15f);
            SafeSetStat(TyWudao2.base_stats, "Accuracy", 10f);
            SafeSetStat(TyWudao2.base_stats, "Dodge", 9f);
            // 绑定回血机制
            TyWudao2.action_special_effect += WudaoPromotion.Wudao2_Regen;
            AssetManager.traits.add(TyWudao2);

            // 真意
            ActorTrait TyWudao3 = CreateTrait("TyWudao3", "trait/TyWudao3", "TyWudao");
            TyWudao3.rarity = Rarity.R2_Epic;
            SafeSetStat(TyWudao3.base_stats, strings.S.multiplier_damage, 0.30f);
            SafeSetStat(TyWudao3.base_stats, strings.S.multiplier_health, 0.30f);
            SafeSetStat(TyWudao3.base_stats, strings.S.damage, 10f);
            SafeSetStat(TyWudao3.base_stats, strings.S.health, 200f);
            SafeSetStat(TyWudao3.base_stats, strings.S.intelligence, 20f);
            SafeSetStat(TyWudao3.base_stats, strings.S.lifespan, 70f);
            SafeSetStat(TyWudao3.base_stats, strings.S.stamina, 90f);
            SafeSetStat(TyWudao3.base_stats, strings.S.accuracy, 18f);
            SafeSetStat(TyWudao3.base_stats, "Accuracy", 30f);
            SafeSetStat(TyWudao3.base_stats, "Dodge", 25f);
            // 绑定基于气血的真实伤害攻击机制
            TyWudao3.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            // 绑定神通级别的真实伤害攻击机制
            TyWudao3.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定回血机制
            TyWudao3.action_special_effect += WudaoPromotion.Wudao3_Regen;
            AssetManager.traits.add(TyWudao3);

            // 玄罡
            ActorTrait TyWudao4 = CreateTrait("TyWudao4", "trait/TyWudao4", "TyWudao");
            TyWudao4.rarity = Rarity.R2_Epic;
            SafeSetStat(TyWudao4.base_stats, strings.S.lifespan, 90f);
            SafeSetStat(TyWudao4.base_stats, strings.S.multiplier_damage, 0.40f);
            SafeSetStat(TyWudao4.base_stats, strings.S.multiplier_health, 0.40f);
            SafeSetStat(TyWudao4.base_stats , strings.S.damage, 50f);
            SafeSetStat(TyWudao4.base_stats, strings.S.mass, 10f);
            SafeSetStat(TyWudao4.base_stats , strings.S.health, 1000f);
            SafeSetStat(TyWudao4.base_stats , strings.S.speed, 5f);
            SafeSetStat(TyWudao4.base_stats , strings.S.armor, 10f);
            SafeSetStat(TyWudao4.base_stats , strings.S.targets, 1f);
            SafeSetStat(TyWudao4.base_stats , strings.S.critical_chance, 0.2f);
            SafeSetStat(TyWudao4.base_stats , strings.S.accuracy, 10f);
            SafeSetStat(TyWudao4.base_stats , strings.S.multiplier_speed, 0.3f);
            SafeSetStat(TyWudao4.base_stats , strings.S.stamina, 40f);
            SafeSetStat(TyWudao4.base_stats, "Dodge", 60f);
            SafeSetStat(TyWudao4.base_stats, "Accuracy", 55f);
            // 绑定基于气血的真实伤害攻击机制
            TyWudao4.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            // 绑定神通级别的真实伤害攻击机制
            TyWudao4.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定回血机制
            TyWudao4.action_special_effect += WudaoPromotion.Wudao4_Regen;
            AssetManager.traits.add(TyWudao4);

            // 天人
            ActorTrait TyWudao5 = CreateTrait("TyWudao5", "trait/TyWudao5", "TyWudao");
            TyWudao5.rarity = Rarity.R2_Epic;
            SafeSetStat(TyWudao5.base_stats, strings.S.multiplier_damage, 0.50f);
            SafeSetStat(TyWudao5.base_stats, strings.S.multiplier_health, 0.50f);
            SafeSetStat(TyWudao5.base_stats, strings.S.lifespan, 120f);
            SafeSetStat(TyWudao5.base_stats, stats.Resist.id, 8.0f);
            SafeSetStat(TyWudao5.base_stats , strings.S.warfare, 10f);
            SafeSetStat(TyWudao5.base_stats, strings.S.mass, 15f);
            SafeSetStat(TyWudao5.base_stats , strings.S.damage, 100f);
            SafeSetStat(TyWudao5.base_stats , strings.S.health, 2000f);
            SafeSetStat(TyWudao5.base_stats , strings.S.speed, 10f);
            SafeSetStat(TyWudao5.base_stats , strings.S.armor, 15f);
            SafeSetStat(TyWudao5.base_stats , strings.S.targets, 2f);
            SafeSetStat(TyWudao5.base_stats , strings.S.critical_chance, 0.3f);
            SafeSetStat(TyWudao5.base_stats , strings.S.accuracy, 20f);
            SafeSetStat(TyWudao5.base_stats , strings.S.multiplier_speed, 0.4f);
            SafeSetStat(TyWudao5.base_stats , strings.S.stamina, 50f);
            SafeSetStat(TyWudao5.base_stats, "Dodge", 100f);
            SafeSetStat(TyWudao5.base_stats, "Accuracy", 90f);
            // 绑定基于气血的真实伤害攻击机制
            TyWudao5.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            // 绑定神通级别的真实伤害攻击机制
            TyWudao5.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定回血机制
            TyWudao5.action_special_effect += WudaoPromotion.Wudao5_Regen;
            AssetManager.traits.add(TyWudao5);

            // 神游
            ActorTrait TyWudao6 = CreateTrait("TyWudao6", "trait/TyWudao6", "TyWudao");
            TyWudao6.rarity = Rarity.R2_Epic;
            SafeSetStat(TyWudao6.base_stats, stats.Resist.id, 32.0f);
            SafeSetStat(TyWudao6.base_stats, strings.S.warfare, 30f);
            SafeSetStat(TyWudao6.base_stats, strings.S.damage, 500f);
            SafeSetStat(TyWudao6.base_stats, strings.S.mass, 25f);
            SafeSetStat(TyWudao6.base_stats, strings.S.armor, 35f);
            SafeSetStat(TyWudao6.base_stats, strings.S.health, 10000f);
            SafeSetStat(TyWudao6.base_stats, strings.S.speed, 20f);
            SafeSetStat(TyWudao6.base_stats, strings.S.area_of_effect, 20f);
            SafeSetStat(TyWudao6.base_stats, strings.S.targets, 7f);
            SafeSetStat(TyWudao6.base_stats, strings.S.critical_chance, 0.6f);
            SafeSetStat(TyWudao6.base_stats, strings.S.accuracy, 40f);
            SafeSetStat(TyWudao6.base_stats, strings.S.multiplier_speed, 0.8f);
            SafeSetStat(TyWudao6.base_stats, strings.S.stamina, 140f);
            SafeSetStat(TyWudao6.base_stats, strings.S.range, 2f);
            SafeSetStat(TyWudao6.base_stats, strings.S.attack_speed, 2f);
            SafeSetStat(TyWudao6.base_stats, strings.S.scale, 0.04f);  
            SafeSetStat(TyWudao6.base_stats, strings.S.multiplier_health, 0.6f);
            SafeSetStat(TyWudao6.base_stats, strings.S.multiplier_damage, 0.6f);
            SafeSetStat(TyWudao6.base_stats, "Dodge", 200f);
            SafeSetStat(TyWudao6.base_stats, "Accuracy", 150f);
            SafeSetStat(TyWudao6.base_stats, strings.S.lifespan, 150f);
            // 绑定基于气血的真实伤害攻击机制
            TyWudao6.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            // 绑定神通级别的真实伤害攻击机制
            TyWudao6.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定回血机制
            TyWudao6.action_special_effect += WudaoPromotion.Wudao6_Regen;
            AssetManager.traits.add(TyWudao6);

            // 踏虚
            ActorTrait TyWudao7 = CreateTrait("TyWudao7", "trait/TyWudao7", "TyWudao");
            TyWudao7.rarity = Rarity.R3_Legendary;
            SafeSetStat(TyWudao7.base_stats, stats.Resist.id, 64.0f);
            SafeSetStat(TyWudao7.base_stats, strings.S.warfare, 40f);
            SafeSetStat(TyWudao7.base_stats, strings.S.damage, 1000f);
            SafeSetStat(TyWudao7.base_stats, strings.S.mass, 60f);
            SafeSetStat(TyWudao7.base_stats, strings.S.armor, 45f);
            SafeSetStat(TyWudao7.base_stats, strings.S.health, 20000f);
            SafeSetStat(TyWudao7.base_stats, strings.S.speed, 25f);
            SafeSetStat(TyWudao7.base_stats, strings.S.area_of_effect, 30f);
            SafeSetStat(TyWudao7.base_stats, strings.S.targets, 9f);
            SafeSetStat(TyWudao7.base_stats, strings.S.critical_chance, 0.7f);
            SafeSetStat(TyWudao7.base_stats, strings.S.accuracy, 50f);
            SafeSetStat(TyWudao7.base_stats, strings.S.multiplier_speed, 1.0f);
            SafeSetStat(TyWudao7.base_stats, strings.S.stamina, 240f);
            SafeSetStat(TyWudao7.base_stats, strings.S.range, 4f);
            SafeSetStat(TyWudao7.base_stats, strings.S.attack_speed, 4f);
            SafeSetStat(TyWudao7.base_stats, strings.S.multiplier_health, 0.7f);
            SafeSetStat(TyWudao7.base_stats, strings.S.multiplier_damage, 0.7f);
            SafeSetStat(TyWudao7.base_stats, strings.S.scale, 0.06f);
            SafeSetStat(TyWudao7.base_stats, "Dodge", 400f);
            SafeSetStat(TyWudao7.base_stats, "Accuracy", 350f);
            SafeSetStat(TyWudao7.base_stats, strings.S.lifespan, 200f);
            // 绑定基于气血的真实伤害攻击机制
            TyWudao7.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            // 绑定神通级别的真实伤害攻击机制
            TyWudao7.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定回血机制
            TyWudao7.action_special_effect += WudaoPromotion.Wudao7_Regen;
            AssetManager.traits.add(TyWudao7);

            // 明我
            ActorTrait TyWudao8 = CreateTrait("TyWudao8", "trait/TyWudao8", "TyWudao");
            TyWudao8.rarity = Rarity.R3_Legendary;
            SafeSetStat(TyWudao8.base_stats, stats.Resist.id, 128.0f);
            SafeSetStat(TyWudao8.base_stats, strings.S.warfare, 50f);
            SafeSetStat(TyWudao8.base_stats, strings.S.damage, 5000f);
            SafeSetStat(TyWudao8.base_stats, strings.S.mass, 140f);
            SafeSetStat(TyWudao8.base_stats, strings.S.armor, 60f);
            SafeSetStat(TyWudao8.base_stats, strings.S.health, 100000f);
            SafeSetStat(TyWudao8.base_stats, strings.S.speed, 30f);
            SafeSetStat(TyWudao8.base_stats, strings.S.area_of_effect, 40f);
            SafeSetStat(TyWudao8.base_stats, strings.S.targets, 12f);
            SafeSetStat(TyWudao8.base_stats, strings.S.critical_chance, 0.8f);
            SafeSetStat(TyWudao8.base_stats, strings.S.accuracy, 60f);
            SafeSetStat(TyWudao8.base_stats, strings.S.multiplier_speed, 1.2f);
            SafeSetStat(TyWudao8.base_stats, strings.S.stamina, 400f);
            SafeSetStat(TyWudao8.base_stats, strings.S.range, 6f);
            SafeSetStat(TyWudao8.base_stats, strings.S.attack_speed, 6f);
            SafeSetStat(TyWudao8.base_stats, strings.S.scale, 0.08f);
            SafeSetStat(TyWudao8.base_stats, strings.S.multiplier_health, 0.8f);
            SafeSetStat(TyWudao8.base_stats, strings.S.multiplier_damage, 0.8f);
            SafeSetStat(TyWudao8.base_stats, "Dodge", 600f);
            SafeSetStat(TyWudao8.base_stats, "Accuracy", 550f);
            SafeSetStat(TyWudao8.base_stats, strings.S.lifespan, 300f);
            // 绑定基于气血的真实伤害攻击机制
            TyWudao8.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            // 绑定神通级别的真实伤害攻击机制
            TyWudao8.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定回血机制
            TyWudao8.action_special_effect += WudaoPromotion.Wudao8_Regen;
            AssetManager.traits.add(TyWudao8);

            // 山海
            ActorTrait TyWudao9 = CreateTrait("TyWudao9", "trait/TyWudao9", "TyWudao");
            TyWudao9.rarity = Rarity.R3_Legendary;
            SafeSetStat(TyWudao9.base_stats, stats.Resist.id, 160.0f);
            SafeSetStat(TyWudao9.base_stats, strings.S.warfare, 100f);
            SafeSetStat(TyWudao9.base_stats, strings.S.damage, 10000f);
            SafeSetStat(TyWudao9.base_stats, strings.S.mass, 400f);
            SafeSetStat(TyWudao9.base_stats, strings.S.armor, 80f);
            SafeSetStat(TyWudao9.base_stats, strings.S.health, 200000f);
            SafeSetStat(TyWudao9.base_stats, strings.S.speed, 100f);
            SafeSetStat(TyWudao9.base_stats, strings.S.area_of_effect, 50f);
            SafeSetStat(TyWudao9.base_stats, strings.S.targets, 30f);
            SafeSetStat(TyWudao9.base_stats, strings.S.critical_chance, 1.8f);
            SafeSetStat(TyWudao9.base_stats, strings.S.accuracy, 160f);
            SafeSetStat(TyWudao9.base_stats, strings.S.multiplier_speed, 1.5f);
            SafeSetStat(TyWudao9.base_stats, strings.S.stamina, 1200f);
            SafeSetStat(TyWudao9.base_stats, strings.S.range, 24f);
            SafeSetStat(TyWudao9.base_stats, strings.S.attack_speed, 24f);
            SafeSetStat(TyWudao9.base_stats, strings.S.scale, 0.2f);
            SafeSetStat(TyWudao9.base_stats, strings.S.multiplier_health, 1.2f);
            SafeSetStat(TyWudao9.base_stats, strings.S.multiplier_damage, 1.2f);
            SafeSetStat(TyWudao9.base_stats, "Dodge", 800f);
            SafeSetStat(TyWudao9.base_stats, "Accuracy", 750f);
            SafeSetStat(TyWudao9.base_stats, strings.S.lifespan, 500f);
            // 绑定基于气血的真实伤害攻击机制
            TyWudao9.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            // 绑定神通级别的真实伤害攻击机制
            TyWudao9.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定回血机制
            TyWudao9.action_special_effect += WudaoPromotion.Wudao9_Regen;
            AssetManager.traits.add(TyWudao9);

            // 圣躯境(TyWudao91) - 属性=大乘(XianTu7)
            ActorTrait TyWudao91 = CreateTrait("TyWudao91", "trait/TyWudao91", "TyWudao");
            TyWudao91.rarity = Rarity.R3_Legendary;
            SafeSetStat(TyWudao91.base_stats, strings.S.lifespan, 1000f);
            SafeSetStat(TyWudao91.base_stats, stats.Resist.id, 200.0f);
            SafeSetStat(TyWudao91.base_stats, strings.S.warfare, 200f);
            SafeSetStat(TyWudao91.base_stats, strings.S.damage, 50000f);
            SafeSetStat(TyWudao91.base_stats, strings.S.mass, 1600f);
            SafeSetStat(TyWudao91.base_stats, strings.S.armor, 100f);
            SafeSetStat(TyWudao91.base_stats, strings.S.health, 1000000f);
            SafeSetStat(TyWudao91.base_stats, strings.S.speed, 200f);
            SafeSetStat(TyWudao91.base_stats, strings.S.area_of_effect, 70f);
            SafeSetStat(TyWudao91.base_stats, strings.S.targets, 40f);
            SafeSetStat(TyWudao91.base_stats, strings.S.critical_chance, 2.0f);
            SafeSetStat(TyWudao91.base_stats, strings.S.accuracy, 200f);
            SafeSetStat(TyWudao91.base_stats, strings.S.multiplier_speed, 2.0f);
            SafeSetStat(TyWudao91.base_stats, strings.S.stamina, 3000f);
            SafeSetStat(TyWudao91.base_stats, strings.S.range, 40f);
            SafeSetStat(TyWudao91.base_stats, strings.S.attack_speed, 40f);
            SafeSetStat(TyWudao91.base_stats, strings.S.scale, 0.3f);
            SafeSetStat(TyWudao91.base_stats, strings.S.multiplier_health, 2f);
            SafeSetStat(TyWudao91.base_stats, strings.S.multiplier_damage, 2f);
            SafeSetStat(TyWudao91.base_stats, "Dodge", 1000f);
            SafeSetStat(TyWudao91.base_stats, "Accuracy", 900f);
            // 绑定基于气血的真实伤害攻击机制
            TyWudao91.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            // 绑定神通级别的真实伤害攻击机制
            TyWudao91.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定回血机制
            TyWudao91.action_special_effect += WudaoPromotion.Wudao91_Regen;
            // 添加饱食度维护功能
            TyWudao91.action_special_effect += traitAction.MaintainFullNutrition;
            AssetManager.traits.add(TyWudao91);

            // 涅槃
            ActorTrait TyWudao92 = CreateTrait("TyWudao92", "trait/TyWudao92", "TyWudao");
            TyWudao92.rarity = Rarity.R3_Legendary;
            SafeSetStat(TyWudao92.base_stats, strings.S.lifespan, 1500f);
            SafeSetStat(TyWudao92.base_stats, stats.Resist.id, 240.0f);
            SafeSetStat(TyWudao92.base_stats, strings.S.warfare, 400f);
            SafeSetStat(TyWudao92.base_stats, strings.S.damage, 100000f);
            SafeSetStat(TyWudao92.base_stats, strings.S.mass, 2400f);
            SafeSetStat(TyWudao92.base_stats, strings.S.armor, 160f);
            SafeSetStat(TyWudao92.base_stats, strings.S.health, 2000000f);
            SafeSetStat(TyWudao92.base_stats, strings.S.speed, 300f);
            SafeSetStat(TyWudao92.base_stats, strings.S.area_of_effect, 100f);
            SafeSetStat(TyWudao92.base_stats, strings.S.targets, 50f);
            SafeSetStat(TyWudao92.base_stats, strings.S.critical_chance, 3.0f);
            SafeSetStat(TyWudao92.base_stats, strings.S.accuracy, 360f);
            SafeSetStat(TyWudao92.base_stats, strings.S.multiplier_speed, 3.0f);
            SafeSetStat(TyWudao92.base_stats, strings.S.stamina, 10000f);
            SafeSetStat(TyWudao92.base_stats, strings.S.range, 60f);
            SafeSetStat(TyWudao92.base_stats, strings.S.attack_speed, 60f);
            SafeSetStat(TyWudao92.base_stats, strings.S.scale, 0.4f);
            SafeSetStat(TyWudao92.base_stats, strings.S.multiplier_health, 3f);
            SafeSetStat(TyWudao92.base_stats, strings.S.multiplier_damage, 3f);    
            SafeSetStat(TyWudao92.base_stats, "Dodge", 2000f);
            SafeSetStat(TyWudao92.base_stats, "Accuracy", 1900f);
            // 绑定基于气血的真实伤害攻击机制
            TyWudao92.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            // 绑定神通级别的真实伤害攻击机制
            // 绑定回血机制
            TyWudao92.action_special_effect += WudaoPromotion.Wudao92_Regen;
            TyWudao92.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 添加饱食度维护功能
            TyWudao92.action_special_effect += traitAction.MaintainFullNutrition;
            AssetManager.traits.add(TyWudao92);

            // 圣躯境
            ActorTrait TyWudao93 = CreateTrait("TyWudao93", "trait/TyWudao93", "TyWudao");
            TyWudao93.rarity = Rarity.R3_Legendary;
            SafeSetStat(TyWudao93.base_stats, strings.S.lifespan, 2400f);
            SafeSetStat(TyWudao93.base_stats, stats.Resist.id, 320.0f);
            SafeSetStat(TyWudao93.base_stats, strings.S.warfare, 600f);
            SafeSetStat(TyWudao93.base_stats, strings.S.damage, 300000f);
            SafeSetStat(TyWudao93.base_stats, strings.S.mass, 4800f);
            SafeSetStat(TyWudao93.base_stats, strings.S.armor, 240f);
            SafeSetStat(TyWudao93.base_stats, strings.S.health, 5000000f);
            SafeSetStat(TyWudao93.base_stats, strings.S.speed, 500f);
            SafeSetStat(TyWudao93.base_stats, strings.S.area_of_effect, 150f);
            SafeSetStat(TyWudao93.base_stats, strings.S.targets, 75f);
            SafeSetStat(TyWudao93.base_stats, strings.S.critical_chance, 4.0f);
            SafeSetStat(TyWudao93.base_stats, strings.S.accuracy, 480f);
            SafeSetStat(TyWudao93.base_stats, strings.S.multiplier_speed, 4.0f);
            SafeSetStat(TyWudao93.base_stats, strings.S.stamina, 15000f);
            SafeSetStat(TyWudao93.base_stats, strings.S.range, 80f);
            SafeSetStat(TyWudao93.base_stats, strings.S.attack_speed, 80f);
            SafeSetStat(TyWudao93.base_stats, strings.S.scale, 0.5f);
            SafeSetStat(TyWudao93.base_stats, strings.S.multiplier_health, 4f);
            SafeSetStat(TyWudao93.base_stats, strings.S.multiplier_damage, 4f);    
            SafeSetStat(TyWudao93.base_stats, "Dodge", 2500f);
            SafeSetStat(TyWudao93.base_stats, "Accuracy", 2400f);
            // 绑定基于气血的真实伤害攻击机制
            TyWudao93.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            // 绑定神通级别的真实伤害攻击机制
            TyWudao93.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定回血机制
            TyWudao93.action_special_effect += WudaoPromotion.Wudao93_Regen;
            // 绑定极尽升华检查机制
            TyWudao93.action_special_effect += WudaoPromotion.CheckJiJinShengHua;
            // 添加饱食度维护功能
            TyWudao93.action_special_effect += traitAction.MaintainFullNutrition;
            AssetManager.traits.add(TyWudao93);

            // 帝境
            ActorTrait TyWudao94 = CreateTrait("TyWudao94", "trait/TyWudao94", "TyWudao");
            TyWudao94.rarity = Rarity.R3_Legendary;
            SafeSetStat(TyWudao94.base_stats, strings.S.lifespan, 3000f);
            SafeSetStat(TyWudao94.base_stats, stats.Resist.id, 400.0f);
            SafeSetStat(TyWudao94.base_stats, strings.S.warfare, 1000f);
            SafeSetStat(TyWudao94.base_stats, strings.S.damage, 500000f);
            SafeSetStat(TyWudao94.base_stats, strings.S.mass, 8000f);
            SafeSetStat(TyWudao94.base_stats, strings.S.armor, 320f);
            SafeSetStat(TyWudao94.base_stats, strings.S.health, 10000000f);
            SafeSetStat(TyWudao94.base_stats, strings.S.speed, 800f);
            SafeSetStat(TyWudao94.base_stats, strings.S.area_of_effect, 200f);
            SafeSetStat(TyWudao94.base_stats, strings.S.targets, 100f);
            SafeSetStat(TyWudao94.base_stats, strings.S.critical_chance, 5.0f);
            SafeSetStat(TyWudao94.base_stats, strings.S.accuracy, 600f);
            SafeSetStat(TyWudao94.base_stats, strings.S.multiplier_speed, 5.0f);
            SafeSetStat(TyWudao94.base_stats, strings.S.stamina, 20000f);
            SafeSetStat(TyWudao94.base_stats, strings.S.range, 100f);
            SafeSetStat(TyWudao94.base_stats, strings.S.attack_speed, 100f);
            SafeSetStat(TyWudao94.base_stats, strings.S.scale, 0.6f);
            SafeSetStat(TyWudao94.base_stats, strings.S.multiplier_health, 5f);
            SafeSetStat(TyWudao94.base_stats, strings.S.multiplier_damage, 5f);    
            SafeSetStat(TyWudao94.base_stats, "Dodge", 3000f);
            SafeSetStat(TyWudao94.base_stats, "Accuracy", 2900f);
            // 绑定基于气血的真实伤害攻击机制
            TyWudao94.action_attack_target += ShenTongSystem.XianTuTrueDamage_AttackAction;
            // 绑定神通级别的真实伤害攻击机制
            TyWudao94.action_attack_target += ShenTongSystem.ShenTongTrueDamage_AttackAction;
            // 绑定回血机制
            TyWudao94.action_special_effect += WudaoPromotion.Wudao94_Regen;
            // 添加饱食度维护功能
            TyWudao94.action_special_effect += traitAction.MaintainFullNutrition;
            AssetManager.traits.add(TyWudao94);

            // 创建DaoJi系列特质 - 道基体系
            // 人道筑基
            ActorTrait daoJi1 = CreateTrait("DaoJi1", "trait/DaoJi1", "DaoJi");
            daoJi1.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi1.base_stats, strings.S.multiplier_damage, 0.1f);
            SafeSetStat(daoJi1.base_stats, strings.S.multiplier_health, 0.1f);
            SafeSetStat(daoJi1.base_stats, strings.S.intelligence, 30f);
            SafeSetStat(daoJi1.base_stats, strings.S.lifespan, 80f);
            SafeSetStat(daoJi1.base_stats, strings.S.stamina, 100f);
            SafeSetStat(daoJi1.base_stats, strings.S.area_of_effect, 2f);
            SafeSetStat(daoJi1.base_stats, strings.S.critical_chance, 0.1f);
            SafeSetStat(daoJi1.base_stats, strings.S.targets, 3f);
            SafeSetStat(daoJi1.base_stats, strings.S.accuracy, 20f);
            SafeSetStat(daoJi1.base_stats, "Accuracy", 20f);
            SafeSetStat(daoJi1.base_stats, "Dodge", 20f);
            AssetManager.traits.add(daoJi1);

            // 地道筑基
            ActorTrait daoJi2 = CreateTrait("DaoJi2", "trait/DaoJi2", "DaoJi");
            daoJi2.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi2.base_stats, strings.S.multiplier_damage, 1.0f);
            SafeSetStat(daoJi2.base_stats, strings.S.multiplier_health, 1.0f);
            SafeSetStat(daoJi2.base_stats, strings.S.intelligence, 40f);
            SafeSetStat(daoJi2.base_stats, strings.S.lifespan, 90f);
            SafeSetStat(daoJi2.base_stats, strings.S.stamina, 120f);
            SafeSetStat(daoJi2.base_stats, strings.S.area_of_effect, 3f);
            SafeSetStat(daoJi2.base_stats, strings.S.critical_chance, 0.15f);
            SafeSetStat(daoJi2.base_stats, strings.S.targets, 4f);
            SafeSetStat(daoJi2.base_stats, strings.S.accuracy, 25f);
            SafeSetStat(daoJi2.base_stats, "Accuracy", 25f);
            SafeSetStat(daoJi2.base_stats, "Dodge", 25f);
            AssetManager.traits.add(daoJi2);

            // 天道筑基
            ActorTrait daoJi3 = CreateTrait("DaoJi3", "trait/DaoJi3", "DaoJi");
            daoJi3.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi3.base_stats, strings.S.multiplier_damage, 2.0f);
            SafeSetStat(daoJi3.base_stats, strings.S.multiplier_health, 2.0f);
            SafeSetStat(daoJi3.base_stats, strings.S.intelligence, 50f);
            SafeSetStat(daoJi3.base_stats, strings.S.lifespan, 100f);
            SafeSetStat(daoJi3.base_stats, strings.S.stamina, 150f);
            SafeSetStat(daoJi3.base_stats, strings.S.area_of_effect, 4f);
            SafeSetStat(daoJi3.base_stats, strings.S.critical_chance, 0.2f);
            SafeSetStat(daoJi3.base_stats, strings.S.targets, 5f);
            SafeSetStat(daoJi3.base_stats, strings.S.accuracy, 30f);
            SafeSetStat(daoJi3.base_stats, "Accuracy", 30f);
            SafeSetStat(daoJi3.base_stats, "Dodge", 30f);
            AssetManager.traits.add(daoJi3);

            // 无上道基
            ActorTrait daoJi4 = CreateTrait("DaoJi4", "trait/DaoJi4", "DaoJi");
            daoJi4.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi4.base_stats, strings.S.multiplier_damage, 4.0f);
            SafeSetStat(daoJi4.base_stats, strings.S.multiplier_health, 4.0f);
            SafeSetStat(daoJi4.base_stats, strings.S.intelligence, 60f);
            SafeSetStat(daoJi4.base_stats, strings.S.lifespan, 110f);
            SafeSetStat(daoJi4.base_stats, strings.S.stamina, 180f);
            SafeSetStat(daoJi4.base_stats, strings.S.area_of_effect, 5f);
            SafeSetStat(daoJi4.base_stats, strings.S.critical_chance, 0.25f);
            SafeSetStat(daoJi4.base_stats, strings.S.targets, 6f);
            SafeSetStat(daoJi4.base_stats, strings.S.accuracy, 35f);
            SafeSetStat(daoJi4.base_stats, "Accuracy", 35f);
            SafeSetStat(daoJi4.base_stats, "Dodge", 35f);
            AssetManager.traits.add(daoJi4);

            // 下品金丹
            ActorTrait daoJi5 = CreateTrait("DaoJi5", "trait/DaoJi5", "DaoJi");
            daoJi5.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi5.base_stats, strings.S.multiplier_damage, 0.1f);
            SafeSetStat(daoJi5.base_stats, strings.S.multiplier_health, 0.1f);
            SafeSetStat(daoJi5.base_stats, strings.S.intelligence, 70f);
            SafeSetStat(daoJi5.base_stats, strings.S.lifespan, 150f);
            SafeSetStat(daoJi5.base_stats, strings.S.stamina, 210f);
            SafeSetStat(daoJi5.base_stats, strings.S.area_of_effect, 6f);
            SafeSetStat(daoJi5.base_stats, strings.S.critical_chance, 0.30f);
            SafeSetStat(daoJi5.base_stats, strings.S.targets, 7f);
            SafeSetStat(daoJi5.base_stats, strings.S.accuracy, 40f);
            SafeSetStat(daoJi5.base_stats, "Accuracy", 40f);
            SafeSetStat(daoJi5.base_stats, "Dodge", 40f);
            AssetManager.traits.add(daoJi5);

            // 中品金丹
            ActorTrait daoJi6 = CreateTrait("DaoJi6", "trait/DaoJi6", "DaoJi");
            daoJi6.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi6.base_stats, strings.S.multiplier_damage, 1.0f);
            SafeSetStat(daoJi6.base_stats, strings.S.multiplier_health, 1.0f);
            SafeSetStat(daoJi6.base_stats, strings.S.intelligence, 80f);
            SafeSetStat(daoJi6.base_stats, strings.S.lifespan, 200f);
            SafeSetStat(daoJi6.base_stats, strings.S.stamina, 240f);
            SafeSetStat(daoJi6.base_stats, strings.S.area_of_effect, 7f);
            SafeSetStat(daoJi6.base_stats, strings.S.critical_chance, 0.35f);
            SafeSetStat(daoJi6.base_stats, strings.S.targets, 8f);
            SafeSetStat(daoJi6.base_stats, strings.S.accuracy, 45f);
            SafeSetStat(daoJi6.base_stats, "Accuracy", 45f);
            SafeSetStat(daoJi6.base_stats, "Dodge", 45f);
            AssetManager.traits.add(daoJi6);

            // 上品金丹
            ActorTrait daoJi7 = CreateTrait("DaoJi7", "trait/DaoJi7", "DaoJi");
            daoJi7.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi7.base_stats, strings.S.multiplier_damage, 2.0f);
            SafeSetStat(daoJi7.base_stats, strings.S.multiplier_health, 2.0f);
            SafeSetStat(daoJi7.base_stats, strings.S.intelligence, 90f);
            SafeSetStat(daoJi7.base_stats, strings.S.lifespan, 250f);
            SafeSetStat(daoJi7.base_stats, strings.S.stamina, 270f);
            SafeSetStat(daoJi7.base_stats, strings.S.area_of_effect, 8f);
            SafeSetStat(daoJi7.base_stats, strings.S.critical_chance, 0.40f);
            SafeSetStat(daoJi7.base_stats, strings.S.targets, 9f);
            SafeSetStat(daoJi7.base_stats, strings.S.accuracy, 50f);
            SafeSetStat(daoJi7.base_stats, "Accuracy", 50f);
            SafeSetStat(daoJi7.base_stats, "Dodge", 50f);
            AssetManager.traits.add(daoJi7);

            // 大道金丹
            ActorTrait daoJi8 = CreateTrait("DaoJi8", "trait/DaoJi8", "DaoJi");
            daoJi8.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi8.base_stats, strings.S.multiplier_damage, 4.0f);
            SafeSetStat(daoJi8.base_stats, strings.S.multiplier_health, 4.0f);
            SafeSetStat(daoJi8.base_stats, strings.S.intelligence, 100f);
            SafeSetStat(daoJi8.base_stats, strings.S.lifespan, 300f);
            SafeSetStat(daoJi8.base_stats, strings.S.stamina, 300f);
            SafeSetStat(daoJi8.base_stats, strings.S.area_of_effect, 9f);
            SafeSetStat(daoJi8.base_stats, strings.S.critical_chance, 0.45f);
            SafeSetStat(daoJi8.base_stats, strings.S.targets, 10f);
            SafeSetStat(daoJi8.base_stats, strings.S.accuracy, 55f);
            SafeSetStat(daoJi8.base_stats, "Accuracy", 55f);
            SafeSetStat(daoJi8.base_stats, "Dodge", 55f);
            AssetManager.traits.add(daoJi8);

            // 三寸元婴
            ActorTrait daoJi9 = CreateTrait("DaoJi9", "trait/DaoJi9", "DaoJi");
            daoJi9.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi9.base_stats, strings.S.multiplier_damage, 0.1f);
            SafeSetStat(daoJi9.base_stats, strings.S.multiplier_health, 0.1f);
            SafeSetStat(daoJi9.base_stats, strings.S.intelligence, 110f);
            SafeSetStat(daoJi9.base_stats, strings.S.lifespan, 300f);
            SafeSetStat(daoJi9.base_stats, strings.S.stamina, 330f);
            SafeSetStat(daoJi9.base_stats, strings.S.area_of_effect, 10f);
            SafeSetStat(daoJi9.base_stats, strings.S.critical_chance, 0.50f);
            SafeSetStat(daoJi9.base_stats, strings.S.targets, 11f);
            SafeSetStat(daoJi9.base_stats, strings.S.accuracy, 60f);
            SafeSetStat(daoJi9.base_stats, "Accuracy", 60f);
            SafeSetStat(daoJi9.base_stats, "Dodge", 60f);
            AssetManager.traits.add(daoJi9);

            // 六寸元婴
            ActorTrait daoJi91 = CreateTrait("DaoJi91", "trait/DaoJi91", "DaoJi");
            daoJi91.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi91.base_stats, strings.S.multiplier_damage, 1.0f);
            SafeSetStat(daoJi91.base_stats, strings.S.multiplier_health, 1.0f);
            SafeSetStat(daoJi91.base_stats, strings.S.intelligence, 120f);
            SafeSetStat(daoJi91.base_stats, strings.S.lifespan, 360f);
            SafeSetStat(daoJi91.base_stats, strings.S.stamina, 360f);
            SafeSetStat(daoJi91.base_stats, strings.S.area_of_effect, 12f);
            SafeSetStat(daoJi91.base_stats, strings.S.critical_chance, 0.55f);
            SafeSetStat(daoJi91.base_stats, strings.S.targets, 13f);
            SafeSetStat(daoJi91.base_stats, strings.S.accuracy, 65f);
            SafeSetStat(daoJi91.base_stats, "Accuracy", 65f);
            SafeSetStat(daoJi91.base_stats, "Dodge", 65f);
            AssetManager.traits.add(daoJi91);

            // 九寸元婴
            ActorTrait daoJi92 = CreateTrait("DaoJi92", "trait/DaoJi92", "DaoJi");
            daoJi92.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi92.base_stats, strings.S.multiplier_damage, 2.0f);
            SafeSetStat(daoJi92.base_stats, strings.S.multiplier_health, 2.0f);
            SafeSetStat(daoJi92.base_stats, strings.S.intelligence, 140f);
            SafeSetStat(daoJi92.base_stats, strings.S.lifespan, 400f);
            SafeSetStat(daoJi92.base_stats, strings.S.stamina, 400f);
            SafeSetStat(daoJi92.base_stats, strings.S.area_of_effect, 15f);
            SafeSetStat(daoJi92.base_stats, strings.S.critical_chance, 0.60f);
            SafeSetStat(daoJi92.base_stats, strings.S.targets, 16f);
            SafeSetStat(daoJi92.base_stats, strings.S.accuracy, 70f);
            SafeSetStat(daoJi92.base_stats, "Accuracy", 70f);
            SafeSetStat(daoJi92.base_stats, "Dodge", 70f);
            AssetManager.traits.add(daoJi92);

            // 不灭元婴
            ActorTrait daoJi93 = CreateTrait("DaoJi93", "trait/DaoJi93", "DaoJi");
            daoJi93.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi93.base_stats, strings.S.multiplier_damage, 4.0f);
            SafeSetStat(daoJi93.base_stats, strings.S.multiplier_health, 4.0f);
            SafeSetStat(daoJi93.base_stats, strings.S.intelligence, 160f);
            SafeSetStat(daoJi93.base_stats, strings.S.lifespan, 500f);
            SafeSetStat(daoJi93.base_stats, strings.S.stamina, 450f);
            SafeSetStat(daoJi93.base_stats, strings.S.area_of_effect, 18f);
            SafeSetStat(daoJi93.base_stats, strings.S.critical_chance, 0.65f);
            SafeSetStat(daoJi93.base_stats, strings.S.targets, 20f);
            SafeSetStat(daoJi93.base_stats, strings.S.accuracy, 75f);
            SafeSetStat(daoJi93.base_stats, "Accuracy", 75f);
            SafeSetStat(daoJi93.base_stats, "Dodge", 75f);
            AssetManager.traits.add(daoJi93);

            // 三丈元神
            ActorTrait daoJi94 = CreateTrait("DaoJi94", "trait/DaoJi94", "DaoJi");
            daoJi94.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi94.base_stats, strings.S.multiplier_damage, 0.1f);
            SafeSetStat(daoJi94.base_stats, strings.S.multiplier_health, 0.1f);
            SafeSetStat(daoJi94.base_stats, strings.S.intelligence, 180f);
            SafeSetStat(daoJi94.base_stats, strings.S.lifespan, 500f);
            SafeSetStat(daoJi94.base_stats, strings.S.stamina, 500f);
            SafeSetStat(daoJi94.base_stats, strings.S.area_of_effect, 22f);
            SafeSetStat(daoJi94.base_stats, strings.S.critical_chance, 0.70f);
            SafeSetStat(daoJi94.base_stats, strings.S.targets, 25f);
            SafeSetStat(daoJi94.base_stats, strings.S.accuracy, 80f);
            SafeSetStat(daoJi94.base_stats, "Accuracy", 80f);
            SafeSetStat(daoJi94.base_stats, "Dodge", 80f);
            AssetManager.traits.add(daoJi94);

            // 六丈元神
            ActorTrait daoJi95 = CreateTrait("DaoJi95", "trait/DaoJi95", "DaoJi");
            daoJi95.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi95.base_stats, strings.S.multiplier_damage, 1.0f);
            SafeSetStat(daoJi95.base_stats, strings.S.multiplier_health, 1.0f);
            SafeSetStat(daoJi95.base_stats, strings.S.intelligence, 210f);
            SafeSetStat(daoJi95.base_stats, strings.S.lifespan, 600f);
            SafeSetStat(daoJi95.base_stats, strings.S.stamina, 560f);
            SafeSetStat(daoJi95.base_stats, strings.S.area_of_effect, 26f);
            SafeSetStat(daoJi95.base_stats, strings.S.critical_chance, 0.75f);
            SafeSetStat(daoJi95.base_stats, strings.S.targets, 30f);
            SafeSetStat(daoJi95.base_stats, strings.S.accuracy, 85f);
            SafeSetStat(daoJi95.base_stats, "Accuracy", 85f);
            SafeSetStat(daoJi95.base_stats, "Dodge", 85f);
            AssetManager.traits.add(daoJi95);

            // 九丈元神
            ActorTrait daoJi96 = CreateTrait("DaoJi96", "trait/DaoJi96", "DaoJi");
            daoJi96.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi96.base_stats, strings.S.multiplier_damage, 2.0f);
            SafeSetStat(daoJi96.base_stats, strings.S.multiplier_health, 2.0f);
            SafeSetStat(daoJi96.base_stats, strings.S.intelligence, 250f);
            SafeSetStat(daoJi96.base_stats, strings.S.lifespan, 700f);
            SafeSetStat(daoJi96.base_stats, strings.S.stamina, 620f);
            SafeSetStat(daoJi96.base_stats, strings.S.area_of_effect, 30f);
            SafeSetStat(daoJi96.base_stats, strings.S.critical_chance, 0.80f);
            SafeSetStat(daoJi96.base_stats, strings.S.targets, 36f);
            SafeSetStat(daoJi96.base_stats, strings.S.accuracy, 90f);
            SafeSetStat(daoJi96.base_stats, "Accuracy", 90f);
            SafeSetStat(daoJi96.base_stats, "Dodge", 90f);
            AssetManager.traits.add(daoJi96);

            // 天地元神
            ActorTrait daoJi97 = CreateTrait("DaoJi97", "trait/DaoJi97", "DaoJi");
            daoJi97.rarity = Rarity.R3_Legendary;
            SafeSetStat(daoJi97.base_stats, strings.S.multiplier_damage, 4.0f);
            SafeSetStat(daoJi97.base_stats, strings.S.multiplier_health, 4.0f);
            SafeSetStat(daoJi97.base_stats, strings.S.intelligence, 280f);
            SafeSetStat(daoJi97.base_stats, strings.S.lifespan, 800f);
            SafeSetStat(daoJi97.base_stats, strings.S.stamina, 700f);
            SafeSetStat(daoJi97.base_stats, strings.S.area_of_effect, 35f);
            SafeSetStat(daoJi97.base_stats, strings.S.critical_chance, 0.85f);
            SafeSetStat(daoJi97.base_stats, strings.S.targets, 45f);
            SafeSetStat(daoJi97.base_stats, strings.S.accuracy, 95f);
            SafeSetStat(daoJi97.base_stats, "Accuracy", 95f);
            SafeSetStat(daoJi97.base_stats, "Dodge", 95f);
            AssetManager.traits.add(daoJi97);

            // 百丈法相
            ActorTrait FaXiang1 = CreateTrait("FaXiang1", "trait/FaXiang1", "DaoJi");
            FaXiang1.rarity = Rarity.R3_Legendary;
            SafeSetStat(FaXiang1.base_stats, strings.S.multiplier_damage, 0.1f);
            SafeSetStat(FaXiang1.base_stats, strings.S.multiplier_health, 0.1f);
            SafeSetStat(FaXiang1.base_stats, strings.S.intelligence, 300f);
            SafeSetStat(FaXiang1.base_stats, strings.S.lifespan, 900f);
            SafeSetStat(FaXiang1.base_stats, strings.S.stamina, 800f);
            SafeSetStat(FaXiang1.base_stats, strings.S.area_of_effect, 40f);
            SafeSetStat(FaXiang1.base_stats, strings.S.critical_chance, 0.90f);
            SafeSetStat(FaXiang1.base_stats, strings.S.targets, 50f);
            SafeSetStat(FaXiang1.base_stats, strings.S.accuracy, 100f);
            SafeSetStat(FaXiang1.base_stats, "Accuracy", 100f);
            SafeSetStat(FaXiang1.base_stats, "Dodge", 100f);
            AssetManager.traits.add(FaXiang1);

            // 千丈法相
            ActorTrait FaXiang2 = CreateTrait("FaXiang2", "trait/FaXiang2", "DaoJi");
            FaXiang2.rarity = Rarity.R3_Legendary;
            SafeSetStat(FaXiang2.base_stats, strings.S.multiplier_damage, 1.0f);
            SafeSetStat(FaXiang2.base_stats, strings.S.multiplier_health, 1.0f);
            SafeSetStat(FaXiang2.base_stats, strings.S.intelligence, 350f);
            SafeSetStat(FaXiang2.base_stats, strings.S.lifespan, 1000f);
            SafeSetStat(FaXiang2.base_stats, strings.S.stamina, 900f);
            SafeSetStat(FaXiang2.base_stats, strings.S.area_of_effect, 45f);
            SafeSetStat(FaXiang2.base_stats, strings.S.critical_chance, 0.95f);
            SafeSetStat(FaXiang2.base_stats, strings.S.targets, 55f);
            SafeSetStat(FaXiang2.base_stats, strings.S.accuracy, 110f);
            SafeSetStat(FaXiang2.base_stats, "Accuracy", 110f);
            SafeSetStat(FaXiang2.base_stats, "Dodge", 110f);
            AssetManager.traits.add(FaXiang2);

            // 万丈法相
            ActorTrait FaXiang3 = CreateTrait("FaXiang3", "trait/FaXiang3", "DaoJi");
            FaXiang3.rarity = Rarity.R3_Legendary;
            SafeSetStat(FaXiang3.base_stats, strings.S.multiplier_damage, 2.0f);
            SafeSetStat(FaXiang3.base_stats, strings.S.multiplier_health, 2.0f);
            SafeSetStat(FaXiang3.base_stats, strings.S.intelligence, 400f);
            SafeSetStat(FaXiang3.base_stats, strings.S.lifespan, 1500f);
            SafeSetStat(FaXiang3.base_stats, strings.S.stamina, 1000f);
            SafeSetStat(FaXiang3.base_stats, strings.S.area_of_effect, 50f);
            SafeSetStat(FaXiang3.base_stats, strings.S.critical_chance, 1.0f);
            SafeSetStat(FaXiang3.base_stats, strings.S.targets, 60f);
            SafeSetStat(FaXiang3.base_stats, strings.S.accuracy, 120f);
            SafeSetStat(FaXiang3.base_stats, "Accuracy", 120f);
            SafeSetStat(FaXiang3.base_stats, "Dodge", 120f);
            AssetManager.traits.add(FaXiang3);

            // 盘古法相
            ActorTrait FaXiang4 = CreateTrait("FaXiang4", "trait/FaXiang4", "DaoJi");
            FaXiang4.rarity = Rarity.R3_Legendary;
            SafeSetStat(FaXiang4.base_stats, strings.S.multiplier_damage, 4.0f);
            SafeSetStat(FaXiang4.base_stats, strings.S.multiplier_health, 4.0f);
            SafeSetStat(FaXiang4.base_stats, strings.S.intelligence, 500f);
            SafeSetStat(FaXiang4.base_stats, strings.S.lifespan, 2000f);
            SafeSetStat(FaXiang4.base_stats, strings.S.stamina, 1200f);
            SafeSetStat(FaXiang4.base_stats, strings.S.area_of_effect, 60f);
            SafeSetStat(FaXiang4.base_stats, strings.S.critical_chance, 1.2f);
            SafeSetStat(FaXiang4.base_stats, strings.S.targets, 70f);
            SafeSetStat(FaXiang4.base_stats, strings.S.accuracy, 150f);
            SafeSetStat(FaXiang4.base_stats, "Accuracy", 150f);
            SafeSetStat(FaXiang4.base_stats, "Dodge", 150f);
            AssetManager.traits.add(FaXiang4);

            // 法相系列 - 混沌法相
            ActorTrait FaXiang5 = CreateTrait("FaXiang5", "trait/FaXiang5", "DaoJi");
            FaXiang5.rarity = Rarity.R3_Legendary;
            SafeSetStat(FaXiang5.base_stats, strings.S.multiplier_damage, 0.1f);
            SafeSetStat(FaXiang5.base_stats, strings.S.multiplier_health, 0.1f);
            SafeSetStat(FaXiang5.base_stats, strings.S.intelligence, 600f);
            SafeSetStat(FaXiang5.base_stats, strings.S.lifespan, 2500f);
            SafeSetStat(FaXiang5.base_stats, strings.S.stamina, 1400f);
            SafeSetStat(FaXiang5.base_stats, strings.S.area_of_effect, 70f);
            SafeSetStat(FaXiang5.base_stats, strings.S.critical_chance, 1.4f);
            SafeSetStat(FaXiang5.base_stats, strings.S.targets, 80f);
            SafeSetStat(FaXiang5.base_stats, strings.S.accuracy, 180f);
            SafeSetStat(FaXiang5.base_stats, "Accuracy", 180f);
            SafeSetStat(FaXiang5.base_stats, "Dodge", 180f);
            AssetManager.traits.add(FaXiang5);

            // 法相系列 - 太初法相
            ActorTrait FaXiang6 = CreateTrait("FaXiang6", "trait/FaXiang6", "DaoJi");
            FaXiang6.rarity = Rarity.R3_Legendary;
            SafeSetStat(FaXiang6.base_stats, strings.S.multiplier_damage, 1.0f);
            SafeSetStat(FaXiang6.base_stats, strings.S.multiplier_health, 1.0f);
            SafeSetStat(FaXiang6.base_stats, strings.S.intelligence, 700f);
            SafeSetStat(FaXiang6.base_stats, strings.S.lifespan, 3000f);
            SafeSetStat(FaXiang6.base_stats, strings.S.stamina, 1600f);
            SafeSetStat(FaXiang6.base_stats, strings.S.area_of_effect, 80f);
            SafeSetStat(FaXiang6.base_stats, strings.S.critical_chance, 1.6f);
            SafeSetStat(FaXiang6.base_stats, strings.S.targets, 90f);
            SafeSetStat(FaXiang6.base_stats, strings.S.accuracy, 210f);
            SafeSetStat(FaXiang6.base_stats, "Accuracy", 210f);
            SafeSetStat(FaXiang6.base_stats, "Dodge", 210f);
            AssetManager.traits.add(FaXiang6);

            // 法相系列 - 鸿蒙法相
            ActorTrait FaXiang7 = CreateTrait("FaXiang7", "trait/FaXiang7", "DaoJi");
            FaXiang7.rarity = Rarity.R3_Legendary;
            SafeSetStat(FaXiang7.base_stats, strings.S.multiplier_damage, 2.0f);
            SafeSetStat(FaXiang7.base_stats, strings.S.multiplier_health, 2.0f);
            SafeSetStat(FaXiang7.base_stats, strings.S.intelligence, 800f);
            SafeSetStat(FaXiang7.base_stats, strings.S.lifespan, 3500f);
            SafeSetStat(FaXiang7.base_stats, strings.S.stamina, 1800f);
            SafeSetStat(FaXiang7.base_stats, strings.S.area_of_effect, 90f);
            SafeSetStat(FaXiang7.base_stats, strings.S.critical_chance, 1.8f);
            SafeSetStat(FaXiang7.base_stats, strings.S.targets, 100f);
            SafeSetStat(FaXiang7.base_stats, strings.S.accuracy, 240f);
            SafeSetStat(FaXiang7.base_stats, "Accuracy", 240f);
            SafeSetStat(FaXiang7.base_stats, "Dodge", 240f);
            AssetManager.traits.add(FaXiang7);

            // 法相系列 - 大道法相
            ActorTrait FaXiang8 = CreateTrait("FaXiang8", "trait/FaXiang8", "DaoJi");
            FaXiang8.rarity = Rarity.R3_Legendary;
            SafeSetStat(FaXiang8.base_stats, strings.S.multiplier_damage, 4.0f);
            SafeSetStat(FaXiang8.base_stats, strings.S.multiplier_health, 4.0f);
            SafeSetStat(FaXiang8.base_stats, strings.S.intelligence, 900f);
            SafeSetStat(FaXiang8.base_stats, strings.S.lifespan, 4000f);
            SafeSetStat(FaXiang8.base_stats, strings.S.stamina, 2000f);
            SafeSetStat(FaXiang8.base_stats, strings.S.area_of_effect, 100f);
            SafeSetStat(FaXiang8.base_stats, strings.S.critical_chance, 2.0f);
            SafeSetStat(FaXiang8.base_stats, strings.S.targets, 120f);
            SafeSetStat(FaXiang8.base_stats, strings.S.accuracy, 270f);
            SafeSetStat(FaXiang8.base_stats, "Accuracy", 270f);
            SafeSetStat(FaXiang8.base_stats, "Dodge", 270f);
            AssetManager.traits.add(FaXiang8);

            // 法相系列 - 小世界
            ActorTrait FaXiang9 = CreateTrait("FaXiang9", "trait/FaXiang9", "DaoJi");
            FaXiang9.rarity = Rarity.R3_Legendary;
            SafeSetStat(FaXiang9.base_stats, strings.S.multiplier_damage, 0.5f);
            SafeSetStat(FaXiang9.base_stats, strings.S.multiplier_health, 0.5f);
            SafeSetStat(FaXiang9.base_stats, strings.S.intelligence, 1000f);
            SafeSetStat(FaXiang9.base_stats, strings.S.lifespan, 1000f);
            SafeSetStat(FaXiang9.base_stats, strings.S.stamina, 2200f);
            SafeSetStat(FaXiang9.base_stats, strings.S.area_of_effect, 110f);
            SafeSetStat(FaXiang9.base_stats, strings.S.critical_chance, 2.2f);
            SafeSetStat(FaXiang9.base_stats, strings.S.targets, 130f);
            SafeSetStat(FaXiang9.base_stats, strings.S.accuracy, 300f);
            SafeSetStat(FaXiang9.base_stats, "Accuracy", 300f);
            SafeSetStat(FaXiang9.base_stats, "Dodge", 300f);
            AssetManager.traits.add(FaXiang9);

            // 法相系列 - 小千世界
            ActorTrait FaXiang91 = CreateTrait("FaXiang91", "trait/FaXiang91", "DaoJi");
            FaXiang91.rarity = Rarity.R3_Legendary;
            SafeSetStat(FaXiang91.base_stats, strings.S.multiplier_damage, 1.0f);
            SafeSetStat(FaXiang91.base_stats, strings.S.multiplier_health, 1.0f);
            SafeSetStat(FaXiang91.base_stats, strings.S.intelligence, 1100f);
            SafeSetStat(FaXiang91.base_stats, strings.S.lifespan, 2000f);
            SafeSetStat(FaXiang91.base_stats, strings.S.stamina, 2400f);
            SafeSetStat(FaXiang91.base_stats, strings.S.area_of_effect, 120f);
            SafeSetStat(FaXiang91.base_stats, strings.S.critical_chance, 2.4f);
            SafeSetStat(FaXiang91.base_stats, strings.S.targets, 140f);
            SafeSetStat(FaXiang91.base_stats, strings.S.accuracy, 330f);
            SafeSetStat(FaXiang91.base_stats, "Accuracy", 330f);
            SafeSetStat(FaXiang91.base_stats, "Dodge", 330f);
            AssetManager.traits.add(FaXiang91);

            // 法相系列 - 大千世界
            ActorTrait FaXiang92 = CreateTrait("FaXiang92", "trait/FaXiang92", "DaoJi");
            FaXiang92.rarity = Rarity.R3_Legendary;
            SafeSetStat(FaXiang92.base_stats, strings.S.multiplier_damage, 2.0f);
            SafeSetStat(FaXiang92.base_stats, strings.S.multiplier_health, 2.0f);
            SafeSetStat(FaXiang92.base_stats, strings.S.intelligence, 1200f);
            SafeSetStat(FaXiang92.base_stats, strings.S.lifespan, 3000f);
            SafeSetStat(FaXiang92.base_stats, strings.S.stamina, 2600f);
            SafeSetStat(FaXiang92.base_stats, strings.S.area_of_effect, 130f);
            SafeSetStat(FaXiang92.base_stats, strings.S.critical_chance, 2.6f);
            SafeSetStat(FaXiang92.base_stats, strings.S.targets, 150f);
            SafeSetStat(FaXiang92.base_stats, strings.S.accuracy, 360f);
            SafeSetStat(FaXiang92.base_stats, "Accuracy", 360f);
            SafeSetStat(FaXiang92.base_stats, "Dodge", 360f);
            AssetManager.traits.add(FaXiang92);

            // 法相系列 - 混沌世界
            ActorTrait FaXiang93 = CreateTrait("FaXiang93", "trait/FaXiang93", "DaoJi");
            FaXiang93.rarity = Rarity.R3_Legendary;
            SafeSetStat(FaXiang93.base_stats, strings.S.multiplier_damage, 3.0f);
            SafeSetStat(FaXiang93.base_stats, strings.S.multiplier_health, 3.0f);
            SafeSetStat(FaXiang93.base_stats, strings.S.intelligence, 1300f);
            SafeSetStat(FaXiang93.base_stats, strings.S.lifespan, 4000f);
            SafeSetStat(FaXiang93.base_stats, strings.S.stamina, 2800f);
            SafeSetStat(FaXiang93.base_stats, strings.S.area_of_effect, 140f);
            SafeSetStat(FaXiang93.base_stats, strings.S.critical_chance, 2.8f);
            SafeSetStat(FaXiang93.base_stats, strings.S.targets, 160f);
            SafeSetStat(FaXiang93.base_stats, strings.S.accuracy, 390f);
            SafeSetStat(FaXiang93.base_stats, "Accuracy", 390f);
            SafeSetStat(FaXiang93.base_stats, "Dodge", 390f);
            AssetManager.traits.add(FaXiang93);

            // 武道资质系列 - 凡人之资
            ActorTrait TyGengu1 = CreateTrait("TyGengu1", "trait/TyGengu1", "TyGengu");
            TyGengu1.rarity = Rarity.R2_Epic;
            TyGengu1.action_special_effect += traitAction.TyGengu1_effectAction;
            AssetManager.traits.add(TyGengu1);

            // 武道资质系列 - 小有天资
            ActorTrait TyGengu2 = CreateTrait("TyGengu2", "trait/TyGengu2", "TyGengu");
            TyGengu2.rarity = Rarity.R2_Epic;
            TyGengu2.action_special_effect += traitAction.TyGengu2_effectAction;
            AssetManager.traits.add(TyGengu2);

            // 武道资质系列 - 武道奇才
            ActorTrait TyGengu3 = CreateTrait("TyGengu3", "trait/TyGengu3", "TyGengu");
            TyGengu3.rarity = Rarity.R3_Legendary;
            TyGengu3.action_special_effect += traitAction.TyGengu3_effectAction;
            AssetManager.traits.add(TyGengu3);

            // 武道资质系列 - 天生武骨
            ActorTrait TyGengu4 = CreateTrait("TyGengu4", "trait/TyGengu4", "TyGengu");
            TyGengu4.rarity = Rarity.R3_Legendary;
            TyGengu4.action_special_effect += traitAction.TyGengu4_effectAction;
            AssetManager.traits.add(TyGengu4);

            // 为道基系列特质添加action_special_effect绑定
            daoJi1.action_special_effect += traitAction.DaoJi_effectAction;
            daoJi2.action_special_effect += traitAction.DaoJi_effectAction;
            daoJi3.action_special_effect += traitAction.DaoJi_effectAction;
            daoJi4.action_special_effect += traitAction.DaoJi_effectAction;
            daoJi5.action_special_effect += traitAction.DaoJi_effectAction;
            daoJi6.action_special_effect += traitAction.DaoJi_effectAction;
            daoJi7.action_special_effect += traitAction.DaoJi_effectAction;
            daoJi8.action_special_effect += traitAction.DaoJi_effectAction;
            daoJi9.action_special_effect += traitAction.DaoJi_effectAction;
            daoJi91.action_special_effect += traitAction.DaoJi_effectAction;
            daoJi92.action_special_effect += traitAction.DaoJi_effectAction;
            daoJi93.action_special_effect += traitAction.DaoJi_effectAction;
            daoJi94.action_special_effect += traitAction.DaoJi_effectAction;
            daoJi95.action_special_effect += traitAction.DaoJi_effectAction;
            daoJi96.action_special_effect += traitAction.DaoJi_effectAction;
            daoJi97.action_special_effect += traitAction.DaoJi_effectAction;
            FaXiang1.action_special_effect += traitAction.DaoJi_effectAction;
            FaXiang2.action_special_effect += traitAction.DaoJi_effectAction;
            FaXiang3.action_special_effect += traitAction.DaoJi_effectAction;
            FaXiang4.action_special_effect += traitAction.DaoJi_effectAction;
            FaXiang5.action_special_effect += traitAction.DaoJi_effectAction;
            FaXiang6.action_special_effect += traitAction.DaoJi_effectAction;
            FaXiang7.action_special_effect += traitAction.DaoJi_effectAction;
            FaXiang8.action_special_effect += traitAction.DaoJi_effectAction;
            FaXiang9.action_special_effect += traitAction.DaoJi_effectAction;
            FaXiang91.action_special_effect += traitAction.DaoJi_effectAction;
            FaXiang92.action_special_effect += traitAction.DaoJi_effectAction;
            FaXiang93.action_special_effect += traitAction.DaoJi_effectAction;

            // 突破丹药系列 - 筑基丹
            ActorTrait TupoDanyao1 = CreateTrait("TupoDanyao1", "trait/TupoDanyao1", "TupoDanyao");
            TupoDanyao1.rarity = Rarity.R2_Epic;
            AssetManager.traits.add(TupoDanyao1);

            // 突破丹药系列 - 化金丹
            ActorTrait TupoDanyao2 = CreateTrait("TupoDanyao2", "trait/TupoDanyao2", "TupoDanyao");
            TupoDanyao2.rarity = Rarity.R2_Epic;
            AssetManager.traits.add(TupoDanyao2);

            // 突破丹药系列 - 结婴丹
            ActorTrait TupoDanyao3 = CreateTrait("TupoDanyao3", "trait/TupoDanyao3", "TupoDanyao");
            TupoDanyao3.rarity = Rarity.R3_Legendary;
            AssetManager.traits.add(TupoDanyao3);

            // 突破丹药系列 - 凝神丹
            ActorTrait TupoDanyao4 = CreateTrait("TupoDanyao4", "trait/TupoDanyao4", "TupoDanyao");
            TupoDanyao4.rarity = Rarity.R3_Legendary;
            AssetManager.traits.add(TupoDanyao4);

            // 突破丹药系列 - 合体丹
            ActorTrait TupoDanyao5 = CreateTrait("TupoDanyao5", "trait/TupoDanyao5", "TupoDanyao");
            TupoDanyao5.rarity = Rarity.R3_Legendary;
            AssetManager.traits.add(TupoDanyao5);

            // 突破丹药系列 - 大乘丹
            ActorTrait TupoDanyao6 = CreateTrait("TupoDanyao6", "trait/TupoDanyao6", "TupoDanyao");
            TupoDanyao6.rarity = Rarity.R3_Legendary;
            AssetManager.traits.add(TupoDanyao6);

            // 突破丹药系列 - 地仙丹
            ActorTrait TupoDanyao7 = CreateTrait("TupoDanyao7", "trait/TupoDanyao7", "TupoDanyao");
            TupoDanyao7.rarity = Rarity.R3_Legendary;
            AssetManager.traits.add(TupoDanyao7);

            // 修为丹药系列 - 凝气丹
            ActorTrait XiulianDanyao1 = CreateTrait("XiulianDanyao1", "trait/XiulianDanyao1", "XiulianDanyao");
            XiulianDanyao1.rarity = Rarity.R2_Epic;
            XiulianDanyao1.action_special_effect += traitAction.XiulianDanyao1_effectAction;
            AssetManager.traits.add(XiulianDanyao1);

            // 修为丹药系列 - 青元丹
            ActorTrait XiulianDanyao2 = CreateTrait("XiulianDanyao2", "trait/XiulianDanyao2", "XiulianDanyao");
            XiulianDanyao2.rarity = Rarity.R2_Epic;
            XiulianDanyao2.action_special_effect += traitAction.XiulianDanyao2_effectAction;
            AssetManager.traits.add(XiulianDanyao2);

            // 修为丹药系列 - 地煞丹
            ActorTrait XiulianDanyao3 = CreateTrait("XiulianDanyao3", "trait/XiulianDanyao3", "XiulianDanyao");
            XiulianDanyao3.rarity = Rarity.R2_Epic;
            XiulianDanyao3.action_special_effect += traitAction.XiulianDanyao3_effectAction;
            AssetManager.traits.add(XiulianDanyao3);

            // 修为丹药系列 - 天罡丹
            ActorTrait XiulianDanyao4 = CreateTrait("XiulianDanyao4", "trait/XiulianDanyao4", "XiulianDanyao");
            XiulianDanyao4.rarity = Rarity.R2_Epic;
            XiulianDanyao4.action_special_effect += traitAction.XiulianDanyao4_effectAction;
            AssetManager.traits.add(XiulianDanyao4);

            // 修为丹药系列 - 日月丹
            ActorTrait XiulianDanyao5 = CreateTrait("XiulianDanyao5", "trait/XiulianDanyao5", "XiulianDanyao");
            XiulianDanyao5.rarity = Rarity.R3_Legendary;
            XiulianDanyao5.action_special_effect += traitAction.XiulianDanyao5_effectAction;
            AssetManager.traits.add(XiulianDanyao5);

            // 修为丹药系列 - 法则丹
            ActorTrait XiulianDanyao6 = CreateTrait("XiulianDanyao6", "trait/XiulianDanyao6", "XiulianDanyao");
            XiulianDanyao6.rarity = Rarity.R3_Legendary;
            XiulianDanyao6.action_special_effect += traitAction.XiulianDanyao6_effectAction;
            AssetManager.traits.add(XiulianDanyao6);

            // 修为丹药系列 - 窃天丹
            ActorTrait XiulianDanyao7 = CreateTrait("XiulianDanyao7", "trait/XiulianDanyao7", "XiulianDanyao");
            XiulianDanyao7.rarity = Rarity.R3_Legendary;
            XiulianDanyao7.action_special_effect += traitAction.XiulianDanyao7_effectAction;
            AssetManager.traits.add(XiulianDanyao7);

            // 修为丹药系列 - 界天丹
            ActorTrait XiulianDanyao8 = CreateTrait("XiulianDanyao8", "trait/XiulianDanyao8", "XiulianDanyao");
            XiulianDanyao8.rarity = Rarity.R3_Legendary;
            XiulianDanyao8.action_special_effect += traitAction.XiulianDanyao8_effectAction;
            AssetManager.traits.add(XiulianDanyao8);

            // 修为丹药系列 - 千界丹
            ActorTrait XiulianDanyao9 = CreateTrait("XiulianDanyao9", "trait/XiulianDanyao9", "XiulianDanyao");
            XiulianDanyao9.rarity = Rarity.R3_Legendary;
            XiulianDanyao9.action_special_effect += traitAction.XiulianDanyao9_effectAction;
            AssetManager.traits.add(XiulianDanyao9);

            // 修为丹药系列 - 玉魄丹（增加1000悟性）
            ActorTrait YupoDanyao8 = CreateTrait("YupoDanyao8", "trait/YupoDanyao8", "XiulianDanyao");
            YupoDanyao8.rarity = Rarity.R3_Legendary;
            YupoDanyao8.action_special_effect += traitAction.YupoDanyao8_effectAction;
            AssetManager.traits.add(YupoDanyao8);

            // 三千大道系列 - 炼丹师
            ActorTrait XiuxianLiuyi1 = CreateTrait("XiuxianLiuyi1", "trait/XiuxianLiuyi1", "XiuxianLiuyi");
            AssetManager.traits.add(XiuxianLiuyi1);

            // 三千大道系列 - 炼器师
            ActorTrait XiuxianLiuyi2 = CreateTrait("XiuxianLiuyi2", "trait/XiuxianLiuyi2", "XiuxianLiuyi");
            AssetManager.traits.add(XiuxianLiuyi2);

            // 三千大道系列 - 驭兽师
            ActorTrait XiuxianLiuyi3 = CreateTrait("XiuxianLiuyi3", "trait/XiuxianLiuyi3", "XiuxianLiuyi");
            AssetManager.traits.add(XiuxianLiuyi3);

            // 三千大道系列 - 灵植师
            ActorTrait XiuxianLiuyi4 = CreateTrait("XiuxianLiuyi4", "trait/XiuxianLiuyi4", "XiuxianLiuyi");
            AssetManager.traits.add(XiuxianLiuyi4);

            // 三千大道系列 - 阵法师
            ActorTrait XiuxianLiuyi5 = CreateTrait("XiuxianLiuyi5", "trait/XiuxianLiuyi5", "XiuxianLiuyi");
            AssetManager.traits.add(XiuxianLiuyi5);

            // 三千大道系列 - 制符师
            ActorTrait XiuxianLiuyi6 = CreateTrait("XiuxianLiuyi6", "trait/XiuxianLiuyi6", "XiuxianLiuyi");
            AssetManager.traits.add(XiuxianLiuyi6);

            // 三千大道系列 - 魔修
            ActorTrait XiuxianLiuyi7 = CreateTrait("XiuxianLiuyi7", "trait/XiuxianLiuyi7", "XiuxianLiuyi");
            AssetManager.traits.add(XiuxianLiuyi7);

            // 三千大道系列 - 剑修
            ActorTrait XiuxianLiuyi8 = CreateTrait("XiuxianLiuyi8", "trait/XiuxianLiuyi8", "XiuxianLiuyi");
            AssetManager.traits.add(XiuxianLiuyi8);

            // 三千大道系列 - 儒修
            ActorTrait XiuxianLiuyi9 = CreateTrait("XiuxianLiuyi9", "trait/XiuxianLiuyi9", "XiuxianLiuyi");
            AssetManager.traits.add(XiuxianLiuyi9);

            // 大道印记系列 - 天心印记
            ActorTrait TianXinYinji = CreateTrait("TianXinYinji", "trait/TianXinYinji", "DaDaoYinJi");
            TianXinYinji.rarity = Rarity.R3_Legendary;
            SafeSetStat(TianXinYinji.base_stats, strings.S.multiplier_health, 10f);
            SafeSetStat(TianXinYinji.base_stats, strings.S.multiplier_damage, 10f);
            AssetManager.traits.add(TianXinYinji);

            // 大道印记系列 - 至尊印记
            ActorTrait ZhiZunYinJi = CreateTrait("ZhiZunYinJi", "trait/ZhiZunYinJi", "DaDaoYinJi");
            ZhiZunYinJi.rarity = Rarity.R3_Legendary;
            SafeSetStat(ZhiZunYinJi.base_stats, strings.S.multiplier_health, 5f);
            SafeSetStat(ZhiZunYinJi.base_stats, strings.S.multiplier_damage, 5f);
            SafeSetStat(ZhiZunYinJi.base_stats, strings.S.lifespan, 10000f);
            AssetManager.traits.add(ZhiZunYinJi);
            
            // 大道印记系列 - 天命印记
            ActorTrait TianDaoyinji = CreateTrait("TianDaoyinji", "trait/TianDaoyinji", "DaDaoYinJi");
            TianDaoyinji.rarity = Rarity.R3_Legendary;
            // 无属性加成
            AssetManager.traits.add(TianDaoyinji);
            
            // 大道印记系列 - 道祖印记
            ActorTrait DaoZuyinji = CreateTrait("DaoZuyinji", "trait/DaoZuyinji", "DaDaoYinJi");
            DaoZuyinji.rarity = Rarity.R3_Legendary;
            SafeSetStat(DaoZuyinji.base_stats, strings.S.multiplier_health, 10f);
            SafeSetStat(DaoZuyinji.base_stats, strings.S.multiplier_damage, 10f);
            AssetManager.traits.add(DaoZuyinji);
            
            // 大道印记系列 - 太乙印记
            ActorTrait TaiYiyinji = CreateTrait("TaiYiyinji", "trait/TaiYiyinji", "DaDaoYinJi");
            TaiYiyinji.rarity = Rarity.R3_Legendary;
            SafeSetStat(TaiYiyinji.base_stats, strings.S.multiplier_health, 5f);
            SafeSetStat(TaiYiyinji.base_stats, strings.S.multiplier_damage, 5f);
            AssetManager.traits.add(TaiYiyinji);
        }

        private static void SafeSetStat(BaseStats baseStats, string statKey, float value)
        {
            baseStats[statKey]= value;
        }
    }
}