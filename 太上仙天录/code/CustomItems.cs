using System.Collections.Generic;
using NCMS.Utils;
using NeoModLoader.api.attributes;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XianTu.code
{
    internal class CustomItems
    {
        private const string PathIcon = "ui/Icons/items";
        private const string PathSlash = "ui/effects/slashes";

        [Hotfixable]
        public static void Init()
        {
            loadCustomItems();
        }

        private static void loadCustomItems()
        {
            #region 基础修仙武器
            // 基础木剑 - 适合练气期使用
            EquipmentAsset basicWoodenSword = AssetManager.items.clone("magic_wooden_sword", "$weapon") as EquipmentAsset;
            basicWoodenSword.id = "magic_wooden_sword";
            basicWoodenSword.material = "wood";
            basicWoodenSword.translation_key = "magic_wooden_sword";
            basicWoodenSword.equipment_subtype = "magic_wooden_sword";
            basicWoodenSword.group_id = "sword";
            basicWoodenSword.animated = false;
            basicWoodenSword.is_pool_weapon = false;
            basicWoodenSword.unlock(true);
            basicWoodenSword.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            basicWoodenSword.base_stats = new();
            basicWoodenSword.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            basicWoodenSword.base_stats.set(CustomBaseStatsConstant.Damage, 1f);
            basicWoodenSword.base_stats.set(CustomBaseStatsConstant.Accuracy, 2f);
            basicWoodenSword.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.1f);
            basicWoodenSword.equipment_value = 50;
            basicWoodenSword.quality = Rarity.R1_Rare;
            basicWoodenSword.equipment_type = EquipmentType.Weapon;
            basicWoodenSword.name_class = "item_class_weapon";
            basicWoodenSword.path_slash_animation = "effects/slashes/slash_sword";
            basicWoodenSword.path_icon = $"{PathIcon}/icon_magic_wooden_sword";
            basicWoodenSword.path_gameplay_sprite = $"weapons/magic_wooden_sword";
            basicWoodenSword.gameplay_sprites = getWeaponSprites("magic_wooden_sword");
            AssetManager.items.add(basicWoodenSword);
            addToLocale(basicWoodenSword.id, basicWoodenSword.translation_key, "一把普通的木剑，蕴含着微弱的灵气，适合修仙初学者使用。");

            // 精铁剑 - 适合筑基期使用
            EquipmentAsset fineIronSword = AssetManager.items.clone("magic_fine_iron_sword", "$weapon") as EquipmentAsset;
            fineIronSword.id = "magic_fine_iron_sword";
            fineIronSword.material = "steel";
            fineIronSword.translation_key = "magic_fine_iron_sword";
            fineIronSword.equipment_subtype = "magic_fine_iron_sword";
            fineIronSword.group_id = "sword";
            fineIronSword.animated = false;
            fineIronSword.is_pool_weapon = false;
            fineIronSword.unlock(true);
            fineIronSword.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            fineIronSword.base_stats = new();
            fineIronSword.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            fineIronSword.base_stats.set(CustomBaseStatsConstant.Damage, 10f);
            fineIronSword.base_stats.set(CustomBaseStatsConstant.Accuracy, 4f);
            fineIronSword.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.2f);
            fineIronSword.equipment_value = 200;
            fineIronSword.quality = Rarity.R1_Rare;
            fineIronSword.equipment_type = EquipmentType.Weapon;
            fineIronSword.name_class = "item_class_weapon";
            fineIronSword.path_slash_animation = "effects/slashes/slash_sword";
            fineIronSword.path_icon = $"{PathIcon}/icon_magic_fine_iron_sword";
            fineIronSword.path_gameplay_sprite = $"weapons/magic_fine_iron_sword";
            fineIronSword.gameplay_sprites = getWeaponSprites("magic_fine_iron_sword");
            AssetManager.items.add(fineIronSword);
            addToLocale(fineIronSword.id, fineIronSword.translation_key, "由精铁打造的剑，比普通武器更锋利，蕴含着些许灵气。");

            // 青钢剑 - 适合金丹期使用
            EquipmentAsset greenSteelSword = AssetManager.items.clone("magic_green_steel_sword", "$weapon") as EquipmentAsset;
            greenSteelSword.id = "magic_green_steel_sword";
            greenSteelSword.material = "steel";
            greenSteelSword.translation_key = "magic_green_steel_sword";
            greenSteelSword.equipment_subtype = "magic_green_steel_sword";
            greenSteelSword.group_id = "sword";
            greenSteelSword.animated = false;
            greenSteelSword.is_pool_weapon = false;
            greenSteelSword.unlock(true);
            greenSteelSword.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            greenSteelSword.base_stats = new();
            greenSteelSword.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            greenSteelSword.base_stats.set(CustomBaseStatsConstant.Damage, 30f);
            greenSteelSword.base_stats.set(CustomBaseStatsConstant.Accuracy, 8f);
            greenSteelSword.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.3f);
            greenSteelSword.equipment_value = 1000;
            greenSteelSword.quality = Rarity.R1_Rare;
            greenSteelSword.equipment_type = EquipmentType.Weapon;
            greenSteelSword.name_class = "item_class_weapon";
            greenSteelSword.path_slash_animation = "effects/slashes/slash_sword";
            greenSteelSword.path_icon = $"{PathIcon}/icon_magic_green_steel_sword";
            greenSteelSword.path_gameplay_sprite = $"weapons/magic_green_steel_sword";
            greenSteelSword.gameplay_sprites = getWeaponSprites("magic_green_steel_sword");
            AssetManager.items.add(greenSteelSword);
            addToLocale(greenSteelSword.id, greenSteelSword.translation_key, "由青钢打造的剑，锋利无比，蕴含着丰富的灵气。");
            #endregion

            #region 元素修仙武器
            // 火灵剑 - 适合元婴期使用
            EquipmentAsset fireSpiritSword = AssetManager.items.clone("magic_fire_spirit_sword", "$weapon") as EquipmentAsset;
            fireSpiritSword.id = "magic_fire_spirit_sword";
            fireSpiritSword.material = "steel";
            fireSpiritSword.translation_key = "magic_fire_spirit_sword";
            fireSpiritSword.equipment_subtype = "magic_fire_spirit_sword";
            fireSpiritSword.group_id = "sword";
            fireSpiritSword.animated = false;
            fireSpiritSword.is_pool_weapon = false;
            fireSpiritSword.unlock(true);
            fireSpiritSword.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            fireSpiritSword.base_stats = new();
            fireSpiritSword.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            fireSpiritSword.base_stats.set(CustomBaseStatsConstant.Damage, 50f);
            fireSpiritSword.base_stats.set(CustomBaseStatsConstant.Accuracy, 15f);
            fireSpiritSword.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.5f);
            fireSpiritSword.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.2f);
            fireSpiritSword.equipment_value = 5000;
            fireSpiritSword.quality = Rarity.R2_Epic;
            fireSpiritSword.equipment_type = EquipmentType.Weapon;
            fireSpiritSword.name_class = "item_class_weapon";
            fireSpiritSword.path_slash_animation = "effects/slashes/slash_sword";
            fireSpiritSword.path_icon = $"{PathIcon}/icon_magic_fire_spirit_sword";
            fireSpiritSword.path_gameplay_sprite = $"weapons/magic_fire_spirit_sword";
            fireSpiritSword.gameplay_sprites = getWeaponSprites("magic_fire_spirit_sword");
            AssetManager.items.add(fireSpiritSword);
            addToLocale(fireSpiritSword.id, fireSpiritSword.translation_key, "蕴含着南明离火之力的宝剑，攻击时能释放出炽热的火焰。");

            // 水灵剑 - 适合化神期使用
            EquipmentAsset waterSpiritSword = AssetManager.items.clone("magic_water_spirit_sword", "$weapon") as EquipmentAsset;
            waterSpiritSword.id = "magic_water_spirit_sword";
            waterSpiritSword.material = "steel";
            waterSpiritSword.translation_key = "magic_water_spirit_sword";
            waterSpiritSword.equipment_subtype = "magic_water_spirit_sword";
            waterSpiritSword.group_id = "sword";
            waterSpiritSword.animated = false;
            waterSpiritSword.is_pool_weapon = false;
            waterSpiritSword.unlock(true);
            waterSpiritSword.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            waterSpiritSword.base_stats = new();
            waterSpiritSword.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            waterSpiritSword.base_stats.set(CustomBaseStatsConstant.Damage, 100f);
            waterSpiritSword.base_stats.set(CustomBaseStatsConstant.Accuracy, 25f);
            waterSpiritSword.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.7f);
            waterSpiritSword.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.3f);
            waterSpiritSword.equipment_value = 10000;
            waterSpiritSword.quality = Rarity.R2_Epic;
            waterSpiritSword.equipment_type = EquipmentType.Weapon;
            waterSpiritSword.name_class = "item_class_weapon";
            waterSpiritSword.path_slash_animation = "effects/slashes/slash_sword";
            waterSpiritSword.path_icon = $"{PathIcon}/icon_magic_water_spirit_sword";
            waterSpiritSword.path_gameplay_sprite = $"weapons/magic_water_spirit_sword";
            waterSpiritSword.gameplay_sprites = getWeaponSprites("magic_water_spirit_sword");
            AssetManager.items.add(waterSpiritSword);
            addToLocale(waterSpiritSword.id, waterSpiritSword.translation_key, "蕴含着北方玄水之力的宝剑，攻击时能释放出寒冷的冰霜。");
            #endregion

            #region 传说修仙武器
            // 紫霄剑 - 适合合体期使用
            EquipmentAsset purpleCloudSword = AssetManager.items.clone("magic_purple_cloud_sword", "$weapon") as EquipmentAsset;
            purpleCloudSword.id = "magic_purple_cloud_sword";
            purpleCloudSword.material = "adamantine";
            purpleCloudSword.translation_key = "magic_purple_cloud_sword";
            purpleCloudSword.equipment_subtype = "magic_purple_cloud_sword";
            purpleCloudSword.group_id = "sword";
            purpleCloudSword.animated = false;
            purpleCloudSword.is_pool_weapon = false;
            purpleCloudSword.unlock(true);
            purpleCloudSword.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            purpleCloudSword.base_stats = new();
            purpleCloudSword.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            purpleCloudSword.base_stats.set(CustomBaseStatsConstant.Damage, 300f);
            purpleCloudSword.base_stats.set(CustomBaseStatsConstant.Accuracy, 50f);
            purpleCloudSword.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 1.0f);
            purpleCloudSword.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.5f);
            purpleCloudSword.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 1.5f);
            purpleCloudSword.equipment_value = 50000;
            purpleCloudSword.quality = Rarity.R3_Legendary;
            purpleCloudSword.equipment_type = EquipmentType.Weapon;
            purpleCloudSword.name_class = "item_class_weapon";
            purpleCloudSword.path_slash_animation = "effects/slashes/slash_sword";
            purpleCloudSword.path_icon = $"{PathIcon}/icon_magic_purple_cloud_sword";
            purpleCloudSword.path_gameplay_sprite = $"weapons/magic_purple_cloud_sword";
            purpleCloudSword.gameplay_sprites = getWeaponSprites("magic_purple_cloud_sword");
            AssetManager.items.add(purpleCloudSword);
            addToLocale(purpleCloudSword.id, purpleCloudSword.translation_key, "传说中由紫霄宫遗留下来的宝剑，蕴含着天地法则之力。");

            // 太初剑 - 适合大乘期使用
            EquipmentAsset primalSword = AssetManager.items.clone("magic_primal_sword", "$weapon") as EquipmentAsset;
            primalSword.id = "magic_primal_sword";
            primalSword.material = "adamantine";
            primalSword.translation_key = "magic_primal_sword";
            primalSword.equipment_subtype = "magic_primal_sword";
            primalSword.group_id = "sword";
            primalSword.animated = false;
            primalSword.is_pool_weapon = false;
            primalSword.unlock(true);
            primalSword.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            primalSword.base_stats = new();
            primalSword.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            primalSword.base_stats.set(CustomBaseStatsConstant.Damage, 500f);
            primalSword.base_stats.set(CustomBaseStatsConstant.Accuracy, 100f);
            primalSword.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 1.5f);
            primalSword.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.7f);
            primalSword.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 2.0f);
            primalSword.equipment_value = 100000;
            primalSword.quality = Rarity.R3_Legendary;
            primalSword.equipment_type = EquipmentType.Weapon;
            primalSword.name_class = "item_class_weapon";
            primalSword.path_slash_animation = "effects/slashes/slash_sword";
            primalSword.path_icon = $"{PathIcon}/icon_magic_primal_sword";
            primalSword.path_gameplay_sprite = $"weapons/magic_primal_sword";
            primalSword.gameplay_sprites = getWeaponSprites("magic_primal_sword");
            AssetManager.items.add(primalSword);
            addToLocale(primalSword.id, primalSword.translation_key, "传说中由太初之气所化的宝剑，拥有毁天灭地的力量。");
            #endregion

            #region 鼎系列修仙武器
            // 青铜鼎 - 适合修仙者使用的神秘宝鼎
            EquipmentAsset bronzeCauldron = AssetManager.items.clone("magic_sanzuding1", "$weapon") as EquipmentAsset;
            bronzeCauldron.id = "magic_sanzuding1";
            bronzeCauldron.material = "bronze";
            bronzeCauldron.translation_key = "magic_sanzuding1";
            bronzeCauldron.equipment_subtype = "magic_sanzuding1";
            bronzeCauldron.group_id = "sword";
            bronzeCauldron.animated = false;
            bronzeCauldron.is_pool_weapon = false;
            bronzeCauldron.unlock(true);
            bronzeCauldron.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            bronzeCauldron.base_stats = new();
            bronzeCauldron.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            bronzeCauldron.base_stats.set(CustomBaseStatsConstant.Damage, 10f);
            bronzeCauldron.base_stats.set(CustomBaseStatsConstant.Accuracy, 8f);
            bronzeCauldron.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.3f);
            bronzeCauldron.equipment_value = 1000;
            bronzeCauldron.quality = Rarity.R1_Rare;
            bronzeCauldron.equipment_type = EquipmentType.Weapon;
            bronzeCauldron.name_class = "item_class_weapon";
            bronzeCauldron.path_slash_animation = "effects/slashes/slash_sword";
            bronzeCauldron.path_icon = $"{PathIcon}/icon_magic_sanzuding1";
            bronzeCauldron.path_gameplay_sprite = $"weapons/magic_sanzuding1";
            bronzeCauldron.gameplay_sprites = getWeaponSprites("magic_sanzuding1");
            AssetManager.items.add(bronzeCauldron);
            addToLocale(bronzeCauldron.id, bronzeCauldron.translation_key, "古老的青铜鼎，蕴含着神秘的力量，是修仙者的重宝。");

            // 三足鼎 - 拥有强大灵力的宝鼎
            EquipmentAsset threeLeggedCauldron = AssetManager.items.clone("magic_sanzuding2", "$weapon") as EquipmentAsset;
            threeLeggedCauldron.id = "magic_sanzuding2";
            threeLeggedCauldron.material = "steel";
            threeLeggedCauldron.translation_key = "magic_sanzuding2";
            threeLeggedCauldron.equipment_subtype = "magic_sanzuding2";
            threeLeggedCauldron.group_id = "sword";
            threeLeggedCauldron.animated = false;
            threeLeggedCauldron.is_pool_weapon = false;
            threeLeggedCauldron.unlock(true);
            threeLeggedCauldron.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            threeLeggedCauldron.base_stats = new();
            threeLeggedCauldron.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            threeLeggedCauldron.base_stats.set(CustomBaseStatsConstant.Damage, 100f);
            threeLeggedCauldron.base_stats.set(CustomBaseStatsConstant.Accuracy, 15f);
            threeLeggedCauldron.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.5f);
            threeLeggedCauldron.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.2f);
            threeLeggedCauldron.equipment_value = 5000;
            threeLeggedCauldron.quality = Rarity.R2_Epic;
            threeLeggedCauldron.equipment_type = EquipmentType.Weapon;
            threeLeggedCauldron.name_class = "item_class_weapon";
            threeLeggedCauldron.path_slash_animation = "effects/slashes/slash_sword";
            threeLeggedCauldron.path_icon = $"{PathIcon}/icon_magic_sanzuding2";
            threeLeggedCauldron.path_gameplay_sprite = $"weapons/magic_sanzuding2";
            threeLeggedCauldron.gameplay_sprites = getWeaponSprites("magic_sanzuding2");
            AssetManager.items.add(threeLeggedCauldron);
            addToLocale(threeLeggedCauldron.id, threeLeggedCauldron.translation_key, "传说中的三足鼎，拥有强大的灵力，能增幅修仙者的力量。");

            // 九州鼎 - 顶级修仙宝鼎
            EquipmentAsset nineProvinceCauldron = AssetManager.items.clone("magic_sanzuding3", "$weapon") as EquipmentAsset;
            nineProvinceCauldron.id = "magic_sanzuding3";
            nineProvinceCauldron.material = "adamantine";
            nineProvinceCauldron.translation_key = "magic_sanzuding3";
            nineProvinceCauldron.equipment_subtype = "magic_sanzuding3";
            nineProvinceCauldron.group_id = "sword";
            nineProvinceCauldron.animated = false;
            nineProvinceCauldron.is_pool_weapon = false;
            nineProvinceCauldron.unlock(true);
            nineProvinceCauldron.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            nineProvinceCauldron.base_stats = new();
            nineProvinceCauldron.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            nineProvinceCauldron.base_stats.set(CustomBaseStatsConstant.Damage, 1000f);
            nineProvinceCauldron.base_stats.set(CustomBaseStatsConstant.Accuracy, 50f);
            nineProvinceCauldron.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 1.0f);
            nineProvinceCauldron.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.5f);
            nineProvinceCauldron.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 1.5f);
            nineProvinceCauldron.equipment_value = 50000;
            nineProvinceCauldron.quality = Rarity.R3_Legendary;
            nineProvinceCauldron.equipment_type = EquipmentType.Weapon;
            nineProvinceCauldron.name_class = "item_class_weapon";
            nineProvinceCauldron.path_slash_animation = "effects/slashes/slash_sword";
            nineProvinceCauldron.path_icon = $"{PathIcon}/icon_magic_sanzuding3";
            nineProvinceCauldron.path_gameplay_sprite = $"weapons/magic_sanzuding3";
            nineProvinceCauldron.gameplay_sprites = getWeaponSprites("magic_sanzuding3");
            AssetManager.items.add(nineProvinceCauldron);
            addToLocale(nineProvinceCauldron.id, nineProvinceCauldron.translation_key, "传说中的九州鼎，象征着天下归一，拥有无穷的神力。");
            #endregion

            #region 魂幡系列修仙武器
            // 魂幡 - 适合鬼道修士使用的基础魂幡
            EquipmentAsset hunFan1 = AssetManager.items.clone("magic_hunfan1", "$weapon") as EquipmentAsset;
            hunFan1.id = "magic_hunfan1";
            hunFan1.material = "cloth";
            hunFan1.translation_key = "magic_hunfan1";
            hunFan1.equipment_subtype = "magic_hunfan1";
            hunFan1.group_id = "sword";
            hunFan1.animated = false;
            hunFan1.is_pool_weapon = false;
            hunFan1.unlock(true);
            hunFan1.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            hunFan1.base_stats = new();
            hunFan1.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            hunFan1.base_stats.set(CustomBaseStatsConstant.Damage, 10f);
            hunFan1.base_stats.set(CustomBaseStatsConstant.Accuracy, 10f);
            hunFan1.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.4f);
            hunFan1.equipment_value = 3000;
            hunFan1.quality = Rarity.R1_Rare;
            hunFan1.equipment_type = EquipmentType.Weapon;
            hunFan1.name_class = "item_class_weapon";
            hunFan1.path_slash_animation = "effects/slashes/slash_sword";
            hunFan1.path_icon = $"{PathIcon}/icon_magic_hunfan1";
            hunFan1.path_gameplay_sprite = $"weapons/magic_hunfan1";
            hunFan1.gameplay_sprites = getWeaponSprites("magic_hunfan1");
            AssetManager.items.add(hunFan1);
            addToLocale(hunFan1.id, hunFan1.translation_key, "蕴含着微弱灵魂之力的幡旗，能操控少量魂魄为己所用。");

            // 十魂幡 - 能操控十个魂魄的幡旗
            EquipmentAsset hunFan2 = AssetManager.items.clone("magic_hunfan2", "$weapon") as EquipmentAsset;
            hunFan2.id = "magic_hunfan2";
            hunFan2.material = "cloth";
            hunFan2.translation_key = "magic_hunfan2";
            hunFan2.equipment_subtype = "magic_hunfan2";
            hunFan2.group_id = "sword";
            hunFan2.animated = false;
            hunFan2.is_pool_weapon = false;
            hunFan2.unlock(true);
            hunFan2.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            hunFan2.base_stats = new();
            hunFan2.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            hunFan2.base_stats.set(CustomBaseStatsConstant.Damage, 10f);
            hunFan2.base_stats.set(CustomBaseStatsConstant.Accuracy, 20f);
            hunFan2.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.6f);
            hunFan2.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.25f);
            hunFan2.equipment_value = 8000;
            hunFan2.quality = Rarity.R2_Epic;
            hunFan2.equipment_type = EquipmentType.Weapon;
            hunFan2.name_class = "item_class_weapon";
            hunFan2.path_slash_animation = "effects/slashes/slash_sword";
            hunFan2.path_icon = $"{PathIcon}/icon_magic_hunfan2";
            hunFan2.path_gameplay_sprite = $"weapons/magic_hunfan2";
            hunFan2.gameplay_sprites = getWeaponSprites("magic_hunfan2");
            AssetManager.items.add(hunFan2);
            addToLocale(hunFan2.id, hunFan2.translation_key, "能操控十个强大魂魄的幡旗，蕴含着强大的灵魂之力。");

            // 百魂幡 - 能操控百个魂魄的幡旗
            EquipmentAsset hunFan3 = AssetManager.items.clone("magic_hunfan3", "$weapon") as EquipmentAsset;
            hunFan3.id = "magic_hunfan3";
            hunFan3.material = "cloth";
            hunFan3.translation_key = "magic_hunfan3";
            hunFan3.equipment_subtype = "magic_hunfan3";
            hunFan3.group_id = "sword";
            hunFan3.animated = false;
            hunFan3.is_pool_weapon = false;
            hunFan3.unlock(true);
            hunFan3.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            hunFan3.base_stats = new();
            hunFan3.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            hunFan3.base_stats.set(CustomBaseStatsConstant.Damage, 30f);
            hunFan3.base_stats.set(CustomBaseStatsConstant.Accuracy, 35f);
            hunFan3.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.8f);
            hunFan3.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.35f);
            hunFan3.equipment_value = 30000;
            hunFan3.quality = Rarity.R2_Epic;
            hunFan3.equipment_type = EquipmentType.Weapon;
            hunFan3.name_class = "item_class_weapon";
            hunFan3.path_slash_animation = "effects/slashes/slash_sword";
            hunFan3.path_icon = $"{PathIcon}/icon_magic_hunfan3";
            hunFan3.path_gameplay_sprite = $"weapons/magic_hunfan3";
            hunFan3.gameplay_sprites = getWeaponSprites("magic_hunfan3");
            AssetManager.items.add(hunFan3);
            addToLocale(hunFan3.id, hunFan3.translation_key, "能操控百个强大魂魄的幡旗，灵魂之力凝聚如实质。");

            // 千魂幡 - 能操控千个魂魄的幡旗
            EquipmentAsset hunFan4 = AssetManager.items.clone("magic_hunfan4", "$weapon") as EquipmentAsset;
            hunFan4.id = "magic_hunfan4";
            hunFan4.material = "cloth";
            hunFan4.translation_key = "magic_hunfan4";
            hunFan4.equipment_subtype = "magic_hunfan4";
            hunFan4.group_id = "sword";
            hunFan4.animated = false;
            hunFan4.is_pool_weapon = false;
            hunFan4.unlock(true);
            hunFan4.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            hunFan4.base_stats = new();
            hunFan4.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            hunFan4.base_stats.set(CustomBaseStatsConstant.Damage, 50f);
            hunFan4.base_stats.set(CustomBaseStatsConstant.Accuracy, 60f);
            hunFan4.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 1.2f);
            hunFan4.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.55f);
            hunFan4.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 1.6f);
            hunFan4.equipment_value = 150000;
            hunFan4.quality = Rarity.R3_Legendary;
            hunFan4.equipment_type = EquipmentType.Weapon;
            hunFan4.name_class = "item_class_weapon";
            hunFan4.path_slash_animation = "effects/slashes/slash_sword";
            hunFan4.path_icon = $"{PathIcon}/icon_magic_hunfan4";
            hunFan4.path_gameplay_sprite = $"weapons/magic_hunfan4";
            hunFan4.gameplay_sprites = getWeaponSprites("magic_hunfan4");
            AssetManager.items.add(hunFan4);
            addToLocale(hunFan4.id, hunFan4.translation_key, "能操控千个强大魂魄的幡旗，挥动间鬼哭神嚎，天地变色。");

            // 万魂幡 - 能操控万个魂魄的幡旗
            EquipmentAsset hunFan5 = AssetManager.items.clone("magic_hunfan5", "$weapon") as EquipmentAsset;
            hunFan5.id = "magic_hunfan5";
            hunFan5.material = "cloth";
            hunFan5.translation_key = "magic_hunfan5";
            hunFan5.equipment_subtype = "magic_hunfan5";
            hunFan5.group_id = "sword";
            hunFan5.animated = false;
            hunFan5.is_pool_weapon = false;
            hunFan5.unlock(true);
            hunFan5.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            hunFan5.base_stats = new();
            hunFan5.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            hunFan5.base_stats.set(CustomBaseStatsConstant.Damage, 100f);
            hunFan5.base_stats.set(CustomBaseStatsConstant.Accuracy, 80f);
            hunFan5.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 1.6f);
            hunFan5.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.7f);
            hunFan5.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 1.8f);
            hunFan5.equipment_value = 500000;
            hunFan5.quality = Rarity.R3_Legendary;
            hunFan5.equipment_type = EquipmentType.Weapon;
            hunFan5.name_class = "item_class_weapon";
            hunFan5.path_slash_animation = "effects/slashes/slash_sword";
            hunFan5.path_icon = $"{PathIcon}/icon_magic_hunfan5";
            hunFan5.path_gameplay_sprite = $"weapons/magic_hunfan5";
            hunFan5.gameplay_sprites = getWeaponSprites("magic_hunfan5");
            AssetManager.items.add(hunFan5);
            addToLocale(hunFan5.id, hunFan5.translation_key, "能操控万个强大魂魄的幡旗，拥有毁天灭地的灵魂之力。");

            // 万亿魂幡 - 能操控万亿个魂魄的幡旗
            EquipmentAsset hunFan6 = AssetManager.items.clone("magic_hunfan6", "$weapon") as EquipmentAsset;
            hunFan6.id = "magic_hunfan6";
            hunFan6.material = "cloth";
            hunFan6.translation_key = "magic_hunfan6";
            hunFan6.equipment_subtype = "magic_hunfan6";
            hunFan6.group_id = "sword";
            hunFan6.animated = false;
            hunFan6.is_pool_weapon = false;
            hunFan6.unlock(true);
            hunFan6.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            hunFan6.base_stats = new();
            hunFan6.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            hunFan6.base_stats.set(CustomBaseStatsConstant.Damage, 300f);
            hunFan6.base_stats.set(CustomBaseStatsConstant.Accuracy, 90f);
            hunFan6.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 2.0f);
            hunFan6.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.85f);
            hunFan6.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 2.2f);
            hunFan6.equipment_value = 2000000;
            hunFan6.quality = Rarity.R3_Legendary;
            hunFan6.equipment_type = EquipmentType.Weapon;
            hunFan6.name_class = "item_class_weapon";
            hunFan6.path_slash_animation = "effects/slashes/slash_sword";
            hunFan6.path_icon = $"{PathIcon}/icon_magic_hunfan6";
            hunFan6.path_gameplay_sprite = $"weapons/magic_hunfan6";
            hunFan6.gameplay_sprites = getWeaponSprites("magic_hunfan6");
            AssetManager.items.add(hunFan6);
            addToLocale(hunFan6.id, hunFan6.translation_key, "能操控万亿个强大魂魄的幡旗，是鬼道修士梦寐以求的顶级宝物。");

            // 万仙魂幡 - 能操控万仙魂魄的终极幡旗
            EquipmentAsset hunFan7 = AssetManager.items.clone("magic_hunfan7", "$weapon") as EquipmentAsset;
            hunFan7.id = "magic_hunfan7";
            hunFan7.material = "cloth";
            hunFan7.translation_key = "magic_hunfan7";
            hunFan7.equipment_subtype = "magic_hunfan7";
            hunFan7.group_id = "sword";
            hunFan7.animated = false;
            hunFan7.is_pool_weapon = false;
            hunFan7.unlock(true);
            hunFan7.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            hunFan7.base_stats = new();
            hunFan7.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            hunFan7.base_stats.set(CustomBaseStatsConstant.Damage, 500f);
            hunFan7.base_stats.set(CustomBaseStatsConstant.Accuracy, 100f);
            hunFan7.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 3.0f);
            hunFan7.base_stats.set(CustomBaseStatsConstant.CriticalChance, 1.0f);
            hunFan7.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 3.0f);
            hunFan7.equipment_value = 10000000;
            hunFan7.quality = Rarity.R3_Legendary;
            hunFan7.equipment_type = EquipmentType.Weapon;
            hunFan7.name_class = "item_class_weapon";
            hunFan7.path_slash_animation = "effects/slashes/slash_sword";
            hunFan7.path_icon = $"{PathIcon}/icon_magic_hunfan7";
            hunFan7.path_gameplay_sprite = $"weapons/magic_hunfan7";
            hunFan7.gameplay_sprites = getWeaponSprites("magic_hunfan7");
            AssetManager.items.add(hunFan7);
            addToLocale(hunFan7.id, hunFan7.translation_key, "传说中能操控万仙魂魄的终极幡旗，拥有超越想象的力量，是鬼道的极致象征。");
            #endregion

            #region 葫芦系列修仙武器
            // 酒葫芦 - 适合修仙者使用的基础酒葫芦
            EquipmentAsset huLu1 = AssetManager.items.clone("magic_hulu1", "$weapon") as EquipmentAsset;
            huLu1.id = "magic_hulu1";
            huLu1.material = "wood";
            huLu1.translation_key = "magic_hulu1";
            huLu1.equipment_subtype = "magic_hulu1";
            huLu1.group_id = "sword";
            huLu1.animated = false;
            huLu1.is_pool_weapon = false;
            huLu1.unlock(true);
            huLu1.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            huLu1.base_stats = new();
            huLu1.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            huLu1.base_stats.set(CustomBaseStatsConstant.Damage, 10f);
            huLu1.base_stats.set(CustomBaseStatsConstant.Accuracy, 9f);
            huLu1.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.35f);
            huLu1.equipment_value = 1500;
            huLu1.quality = Rarity.R1_Rare;
            huLu1.equipment_type = EquipmentType.Weapon;
            huLu1.name_class = "item_class_weapon";
            huLu1.path_slash_animation = "effects/slashes/slash_sword";
            huLu1.path_icon = $"{PathIcon}/icon_magic_hulu1";
            huLu1.path_gameplay_sprite = $"weapons/magic_hulu1";
            huLu1.gameplay_sprites = getWeaponSprites("magic_hulu1");
            AssetManager.items.add(huLu1);
            addToLocale(huLu1.id, huLu1.translation_key, "普通的酒葫芦，蕴含着少量灵力，饮酒时能增强修为。");

            // 青云葫芦 - 拥有祥云之力的葫芦
            EquipmentAsset huLu2 = AssetManager.items.clone("magic_hulu2", "$weapon") as EquipmentAsset;
            huLu2.id = "magic_hulu2";
            huLu2.material = "bronze";
            huLu2.translation_key = "magic_hulu2";
            huLu2.equipment_subtype = "magic_hulu2";
            huLu2.group_id = "sword";
            huLu2.animated = false;
            huLu2.is_pool_weapon = false;
            huLu2.unlock(true);
            huLu2.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            huLu2.base_stats = new();
            huLu2.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            huLu2.base_stats.set(CustomBaseStatsConstant.Damage, 100f);
            huLu2.base_stats.set(CustomBaseStatsConstant.Accuracy, 20f);
            huLu2.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.6f);
            huLu2.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.25f);
            huLu2.equipment_value = 8000;
            huLu2.quality = Rarity.R2_Epic;
            huLu2.equipment_type = EquipmentType.Weapon;
            huLu2.name_class = "item_class_weapon";
            huLu2.path_slash_animation = "effects/slashes/slash_sword";
            huLu2.path_icon = $"{PathIcon}/icon_magic_hulu2";
            huLu2.path_gameplay_sprite = $"weapons/magic_hulu2";
            huLu2.gameplay_sprites = getWeaponSprites("magic_hulu2");
            AssetManager.items.add(huLu2);
            addToLocale(huLu2.id, huLu2.translation_key, "能释放青云之力的葫芦，拥有飞天之能，蕴含着强大的灵力。");

            // 吞天葫芦 - 能吞噬万物的神奇葫芦
            EquipmentAsset huLu3 = AssetManager.items.clone("magic_hulu3", "$weapon") as EquipmentAsset;
            huLu3.id = "magic_hulu3";
            huLu3.material = "steel";
            huLu3.translation_key = "magic_hulu3";
            huLu3.equipment_subtype = "magic_hulu3";
            huLu3.group_id = "sword";
            huLu3.animated = false;
            huLu3.is_pool_weapon = false;
            huLu3.unlock(true);
            huLu3.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            huLu3.base_stats = new();
            huLu3.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            huLu3.base_stats.set(CustomBaseStatsConstant.Damage, 200f);
            huLu3.base_stats.set(CustomBaseStatsConstant.Accuracy, 45f);
            huLu3.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.9f);
            huLu3.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.45f);
            huLu3.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 1.4f);
            huLu3.equipment_value = 40000;
            huLu3.quality = Rarity.R3_Legendary;
            huLu3.equipment_type = EquipmentType.Weapon;
            huLu3.name_class = "item_class_weapon";
            huLu3.path_slash_animation = "effects/slashes/slash_sword";
            huLu3.path_icon = $"{PathIcon}/icon_magic_hulu3";
            huLu3.path_gameplay_sprite = $"weapons/magic_hulu3";
            huLu3.gameplay_sprites = getWeaponSprites("magic_hulu3");
            AssetManager.items.add(huLu3);
            addToLocale(huLu3.id, huLu3.translation_key, "传说中能吞噬万物的神奇葫芦，拥有无尽的空间和强大的吞噬之力。");

            // 长生仙葫 - 蕴含着长生之力的仙葫
            EquipmentAsset huLu4 = AssetManager.items.clone("magic_hulu4", "$weapon") as EquipmentAsset;
            huLu4.id = "magic_hulu4";
            huLu4.material = "adamantine";
            huLu4.translation_key = "magic_hulu4";
            huLu4.equipment_subtype = "magic_hulu4";
            huLu4.group_id = "sword";
            huLu4.animated = false;
            huLu4.is_pool_weapon = false;
            huLu4.unlock(true);
            huLu4.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            huLu4.base_stats = new();
            huLu4.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            huLu4.base_stats.set(CustomBaseStatsConstant.Damage, 500f);
            huLu4.base_stats.set(CustomBaseStatsConstant.Accuracy, 90f);
            huLu4.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 1.8f);
            huLu4.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.8f);
            huLu4.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 2.5f);
            huLu4.equipment_value = 500000;
            huLu4.quality = Rarity.R3_Legendary;
            huLu4.equipment_type = EquipmentType.Weapon;
            huLu4.name_class = "item_class_weapon";
            huLu4.path_slash_animation = "effects/slashes/slash_sword";
            huLu4.path_icon = $"{PathIcon}/icon_magic_hulu4";
            huLu4.path_gameplay_sprite = $"weapons/magic_hulu4";
            huLu4.gameplay_sprites = getWeaponSprites("magic_hulu4");
            AssetManager.items.add(huLu4);
            addToLocale(huLu4.id, huLu4.translation_key, "传说中由仙人所炼的仙葫，蕴含着长生之力，能令人寿元无尽。");
            #endregion

            #region 仙器系列修仙武器
            // 地府幽冥录 - 记录着地府秘密的神秘书卷
            EquipmentAsset xianQi1 = AssetManager.items.clone("magic_xianqi1", "$weapon") as EquipmentAsset;
            xianQi1.id = "magic_xianqi1";
            xianQi1.material = "paper";
            xianQi1.translation_key = "magic_xianqi1";
            xianQi1.equipment_subtype = "magic_xianqi1";
            xianQi1.group_id = "sword";
            xianQi1.animated = false;
            xianQi1.is_pool_weapon = false;
            xianQi1.unlock(true);
            xianQi1.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            xianQi1.base_stats = new();
            xianQi1.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            xianQi1.base_stats.set(CustomBaseStatsConstant.Damage, 1000f);
            xianQi1.base_stats.set(CustomBaseStatsConstant.Accuracy, 18f);
            xianQi1.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.55f);
            xianQi1.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.22f);
            xianQi1.equipment_value = 6000;
            xianQi1.quality = Rarity.R2_Epic;
            xianQi1.equipment_type = EquipmentType.Weapon;
            xianQi1.name_class = "item_class_weapon";
            xianQi1.path_slash_animation = "effects/slashes/slash_sword";
            xianQi1.path_icon = $"{PathIcon}/icon_magic_xianqi1";
            xianQi1.path_gameplay_sprite = $"weapons/magic_xianqi1";
            xianQi1.gameplay_sprites = getWeaponSprites("magic_xianqi1");
            AssetManager.items.add(xianQi1);
            addToLocale(xianQi1.id, xianQi1.translation_key, "记录着地府秘密的神秘书卷，能沟通阴曹地府，驱使鬼差阴兵。");

            // 人皇赦天录 - 蕴含着皇权之力的宝录
            EquipmentAsset xianQi2 = AssetManager.items.clone("magic_xianqi2", "$weapon") as EquipmentAsset;
            xianQi2.id = "magic_xianqi2";
            xianQi2.material = "paper";
            xianQi2.translation_key = "magic_xianqi2";
            xianQi2.equipment_subtype = "magic_xianqi2";
            xianQi2.group_id = "sword";
            xianQi2.animated = false;
            xianQi2.is_pool_weapon = false;
            xianQi2.unlock(true);
            xianQi2.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            xianQi2.base_stats = new();
            xianQi2.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            xianQi2.base_stats.set(CustomBaseStatsConstant.Damage, 1000f);
            xianQi2.base_stats.set(CustomBaseStatsConstant.Accuracy, 35f);
            xianQi2.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 0.8f);
            xianQi2.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.35f);
            xianQi2.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 1.2f);
            xianQi2.equipment_value = 30000;
            xianQi2.quality = Rarity.R3_Legendary;
            xianQi2.equipment_type = EquipmentType.Weapon;
            xianQi2.name_class = "item_class_weapon";
            xianQi2.path_slash_animation = "effects/slashes/slash_sword";
            xianQi2.path_icon = $"{PathIcon}/icon_magic_xianqi2";
            xianQi2.path_gameplay_sprite = $"weapons/magic_xianqi2";
            xianQi2.gameplay_sprites = getWeaponSprites("magic_xianqi2");
            AssetManager.items.add(xianQi2);
            addToLocale(xianQi2.id, xianQi2.translation_key, "传说中由人皇所留的宝录，蕴含着皇权之力，能赦天下之罪。");

            // 天庭长生录 - 记录着长生之法的仙录
            EquipmentAsset xianQi3 = AssetManager.items.clone("magic_xianqi3", "$weapon") as EquipmentAsset;
            xianQi3.id = "magic_xianqi3";
            xianQi3.material = "paper";
            xianQi3.translation_key = "magic_xianqi3";
            xianQi3.equipment_subtype = "magic_xianqi3";
            xianQi3.group_id = "sword";
            xianQi3.animated = false;
            xianQi3.is_pool_weapon = false;
            xianQi3.unlock(true);
            xianQi3.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            xianQi3.base_stats = new();
            xianQi3.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            xianQi3.base_stats.set(CustomBaseStatsConstant.Damage, 1000f);
            xianQi3.base_stats.set(CustomBaseStatsConstant.Accuracy, 60f);
            xianQi3.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 1.2f);
            xianQi3.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.55f);
            xianQi3.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 1.6f);
            xianQi3.equipment_value = 150000;
            xianQi3.quality = Rarity.R3_Legendary;
            xianQi3.equipment_type = EquipmentType.Weapon;
            xianQi3.name_class = "item_class_weapon";
            xianQi3.path_slash_animation = "effects/slashes/slash_sword";
            xianQi3.path_icon = $"{PathIcon}/icon_magic_xianqi3";
            xianQi3.path_gameplay_sprite = $"weapons/magic_xianqi3";
            xianQi3.gameplay_sprites = getWeaponSprites("magic_xianqi3");
            AssetManager.items.add(xianQi3);
            addToLocale(xianQi3.id, xianQi3.translation_key, "记录着长生之法的仙录，是天庭的镇庭之宝，蕴含着无穷的仙道之力。");

            // 归墟万鬼录 - 收录着万鬼之名的邪录
            EquipmentAsset xianQi4 = AssetManager.items.clone("magic_xianqi4", "$weapon") as EquipmentAsset;
            xianQi4.id = "magic_xianqi4";
            xianQi4.material = "paper";
            xianQi4.translation_key = "magic_xianqi4";
            xianQi4.equipment_subtype = "magic_xianqi4";
            xianQi4.group_id = "sword";
            xianQi4.animated = false;
            xianQi4.is_pool_weapon = false;
            xianQi4.unlock(true);
            xianQi4.name_templates = AssetLibrary<EquipmentAsset>.l<string>("flame_sword_name");
            xianQi4.base_stats = new();
            xianQi4.base_stats[strings.S.damage_range] = 0.6f;//伤害范围
            xianQi4.base_stats.set(CustomBaseStatsConstant.Damage, 1000f);
            xianQi4.base_stats.set(CustomBaseStatsConstant.Accuracy, 80f);
            xianQi4.base_stats.set(CustomBaseStatsConstant.MultiplierDamage, 1.6f);
            xianQi4.base_stats.set(CustomBaseStatsConstant.CriticalChance, 0.7f);
            xianQi4.base_stats.set(CustomBaseStatsConstant.AttackSpeed, 1.8f);
            xianQi4.equipment_value = 500000;
            xianQi4.quality = Rarity.R3_Legendary;
            xianQi4.equipment_type = EquipmentType.Weapon;
            xianQi4.name_class = "item_class_weapon";
            xianQi4.path_slash_animation = "effects/slashes/slash_sword";
            xianQi4.path_icon = $"{PathIcon}/icon_magic_xianqi4";
            xianQi4.path_gameplay_sprite = $"weapons/magic_xianqi4";
            xianQi4.gameplay_sprites = getWeaponSprites("magic_xianqi4");
            AssetManager.items.add(xianQi4);
            addToLocale(xianQi4.id, xianQi4.translation_key, "收录着万鬼之名的邪录，能召唤归墟中的无尽鬼物，是万鬼之主的象征。");
            #endregion
        }

        // 复用现有图片资源的辅助方法
        private static Sprite[] getWeaponSprites(string weaponName)
        {
            // 尝试从weapons文件夹加载图片
            var sprite = Resources.Load<Sprite>("weapons/" + weaponName);
            if (sprite != null)
            {
                return new Sprite[] { sprite };
            }
            
            // 如果weapons文件夹中找不到，尝试从trait文件夹加载相应的XianTu图片
            // 匹配武器名称和对应的XianTu图片编号
            string xianTuPath = null;
            if (weaponName.Contains("wooden")) xianTuPath = "trait/XianTu1";
            else if (weaponName.Contains("fine_iron")) xianTuPath = "trait/XianTu2";
            else if (weaponName.Contains("green_steel")) xianTuPath = "trait/XianTu3";
            else if (weaponName.Contains("fire_spirit")) xianTuPath = "trait/XianTu4";
            else if (weaponName.Contains("water_spirit")) xianTuPath = "trait/XianTu5";
            else if (weaponName.Contains("purple_cloud")) xianTuPath = "trait/XianTu6";
            else if (weaponName.Contains("primal")) xianTuPath = "trait/XianTu7";
            else if (weaponName.Contains("sanzuding1")) xianTuPath = "trait/XianTu8";
            else if (weaponName.Contains("sanzuding2")) xianTuPath = "trait/XianTu9";
            else if (weaponName.Contains("sanzuding3")) xianTuPath = "trait/XianTu10";
            else if (weaponName.Contains("hunfan1")) xianTuPath = "trait/XianTu11";
            else if (weaponName.Contains("hunfan2")) xianTuPath = "trait/XianTu12";
            else if (weaponName.Contains("hunfan3")) xianTuPath = "trait/XianTu13";
            else if (weaponName.Contains("hunfan4")) xianTuPath = "trait/XianTu14";
            else if (weaponName.Contains("hunfan5")) xianTuPath = "trait/XianTu15";
            else if (weaponName.Contains("hunfan6")) xianTuPath = "trait/XianTu16";
            else if (weaponName.Contains("hunfan7")) xianTuPath = "trait/XianTu17";
            else if (weaponName.Contains("hulu1")) xianTuPath = "trait/XianTu18";
            else if (weaponName.Contains("hulu2")) xianTuPath = "trait/XianTu19";
            else if (weaponName.Contains("hulu3")) xianTuPath = "trait/XianTu20";
            else if (weaponName.Contains("hulu4")) xianTuPath = "trait/XianTu21";
            else if (weaponName.Contains("xianqi1")) xianTuPath = "trait/XianTu22";
            else if (weaponName.Contains("xianqi2")) xianTuPath = "trait/XianTu23";
            else if (weaponName.Contains("xianqi3")) xianTuPath = "trait/XianTu24";
            else if (weaponName.Contains("xianqi4")) xianTuPath = "trait/XianTu25";
            
            if (xianTuPath != null)
            {
                sprite = Resources.Load<Sprite>(xianTuPath);
                if (sprite != null)
                {
                    return new Sprite[] { sprite };
                }
            }
            
            // 如果都找不到，记录错误并返回空数组
            UnityEngine.Debug.LogError("Can not find weapon sprite for weapon: " + weaponName);
            return new Sprite[0];
        }

        // 添加本地化的辅助方法 - 使用正确的本地化方法
        private static void addToLocale(string id, string translation_key, string description)
        {
            // 此方法简化实现，因为武器名称已通过translation_key设置
            // 本地化文本已在Locales/cz.json文件中定义
            // 注意：不要使用LM类，因为它在当前版本中不存在或已更改
        }
    }
}