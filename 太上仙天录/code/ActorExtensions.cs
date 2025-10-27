using UnityEngine;
using System.Collections.Generic;
using VideoCopilot.code;
using XianTu.code;

namespace VideoCopilot.code.utils
{
    public static class ActorExtensions
    {
        private const string xianTu_key = "xiantu.xianTuNum";
        private const string lifespan_key = "strings.S.lifespan";
        private const string qiXue_key = "xiantu.qiXueNum";
        private const string wuXing_key = "xiantu.wuXingNum";
        private const string gongFaDian_key = "xiantu.gongFaDianNum";
        private const string lingShi_key = "xiantu.lingShiNum";
        private const string zhongPin_key = "xiantu.zhongPinNum";
        private const string shangPin_key = "xiantu.shangPinNum";
        private const string name_suffix_key = "wudao.name_suffix";
        // 存储当前功法名称的键
        private const string current_gongfa_name_key = "xiantu.current_gongfa_name";
        // 存储当前神通名称的键
        private const string current_shentong_name_key = "xiantu.current_shentong_name";
        // 职业进度相关键
        private const string careerProgress_key = "xiantu.careerProgressNum";
        private const string careerLevel_key = "xiantu.careerLevelNum";
        // 功法层次相关键
        private const string gongFaLevel_key = "xiantu.gongFaLevelNum";
        // 推演点相关键
        private const string tuiYanDian_key = "xiantu.tuiYanDianNum";
        // 神通层次相关键
        private const string shenTongLevel_key = "xiantu.shenTongLevelNum";
        
        // 世界信息相关键
        private const string world_strong_first_key = "xiantu.world_strong_first";
        private const string world_strong_second_key = "xiantu.world_strong_second";
        private const string world_strong_third_key = "xiantu.world_strong_third";
        private const string world_strong_fourth_key = "xiantu.world_strong_fourth";
        private const string world_strong_fifth_key = "xiantu.world_strong_fifth";
        private const string world_strong_sixth_key = "xiantu.world_strong_sixth";

        public static float GetXianTu(this Actor actor)
        {
            actor.data.get(xianTu_key, out float val, 0);
            return val;
        }

        public static void SetXianTu(this Actor actor, float val)
        {
            actor.data.set(xianTu_key, val);
        }

        public static void ChangeXianTu(this Actor actor, float delta)
        {
            actor.data.get(xianTu_key, out float val, 0);
            val += delta;
            actor.data.set(xianTu_key, Mathf.Max(0, val));
        }

        // 气血相关扩展方法
        public static float GetQiXue(this Actor actor)
        {
            actor.data.get(qiXue_key, out float val, 0);
            return val;
        }

        public static void SetQiXue(this Actor actor, float val)
        {
            actor.data.set(qiXue_key, val);
        }

        public static void ChangeQiXue(this Actor actor, float delta)
        {
            actor.data.get(qiXue_key, out float val, 0);
            val += delta;
            actor.data.set(qiXue_key, Mathf.Max(0, val));
        }

        // 五行属性相关键名
        private const string metal_key = "metal_proerty";
        private const string wood_key = "wood_proerty";
        private const string water_key = "water_proerty";
        private const string fire_key = "fire_proerty";
        private const string earth_key = "earth_proerty";
        private const string has_checked_spiritual_root_key = "has_checked_spiritual_root";
        private const string spiritual_root_name_key = "spiritual_root_name";

        // 金属性相关扩展方法
        public static float GetMetal(this Actor actor)
        {
            actor.data.get(metal_key, out float val, 0);
            return val;
        }

        public static void SetMetal(this Actor actor, float val)
        {
            actor.data.set(metal_key, val);
        }

        public static void ChangeMetal(this Actor actor, float delta)
        {
            actor.data.get(metal_key, out float val, 0);
            val += delta;
            actor.data.set(metal_key, Mathf.Max(0, val));
        }

        // 木属性相关扩展方法
        public static float GetWood(this Actor actor)
        {
            actor.data.get(wood_key, out float val, 0);
            return val;
        }

        public static void SetWood(this Actor actor, float val)
        {
            actor.data.set(wood_key, val);
        }

        public static void ChangeWood(this Actor actor, float delta)
        {
            actor.data.get(wood_key, out float val, 0);
            val += delta;
            actor.data.set(wood_key, Mathf.Max(0, val));
        }

        // 水属性相关扩展方法
        public static float GetWater(this Actor actor)
        {
            actor.data.get(water_key, out float val, 0);
            return val;
        }

        public static void SetWater(this Actor actor, float val)
        {
            actor.data.set(water_key, val);
        }

        public static void ChangeWater(this Actor actor, float delta)
        {
            actor.data.get(water_key, out float val, 0);
            val += delta;
            actor.data.set(water_key, Mathf.Max(0, val));
        }

        // 火属性相关扩展方法
        public static float GetFire(this Actor actor)
        {
            actor.data.get(fire_key, out float val, 0);
            return val;
        }

        public static void SetFire(this Actor actor, float val)
        {
            actor.data.set(fire_key, val);
        }

        public static void ChangeFire(this Actor actor, float delta)
        {
            actor.data.get(fire_key, out float val, 0);
            val += delta;
            actor.data.set(fire_key, Mathf.Max(0, val));
        }

        // 土属性相关扩展方法
        public static float GetEarth(this Actor actor)
        {
            actor.data.get(earth_key, out float val, 0);
            return val;
        }

        public static void SetEarth(this Actor actor, float val)
        {
            actor.data.set(earth_key, val);
        }

        public static void ChangeEarth(this Actor actor, float delta)
        {
            actor.data.get(earth_key, out float val, 0);
            val += delta;
            actor.data.set(earth_key, Mathf.Max(0, val));
        }

        // 灵根检查相关方法
        public static bool HasCheckedSpiritualRoot(this Actor actor)
        {
            actor.data.get(has_checked_spiritual_root_key, out bool val, false);
            return val;
        }

        public static void SetHasCheckedSpiritualRoot(this Actor actor, bool val)
        {
            actor.data.set(has_checked_spiritual_root_key, val);
        }

        // 灵根名称相关方法
        public static string GetSpiritualRootName(this Actor actor)
        {
            actor.data.get(spiritual_root_name_key, out string val, "灵根未显");
            return val;
        }

        public static void SetSpiritualRootName(this Actor actor, string val)
        {
            actor.data.set(spiritual_root_name_key, val);
        }

        // 悟性相关扩展方法
        public static float GetWuXing(this Actor actor)
        {
            actor.data.get(wuXing_key, out float val, 0);
            return val;
        }

        public static void SetWuXing(this Actor actor, float val)
        {
            actor.data.set(wuXing_key, val);
        }

        public static void ChangeWuXing(this Actor actor, float delta)
        {
            actor.data.get(wuXing_key, out float val, 0);
            val += delta;
            actor.data.set(wuXing_key, Mathf.Max(0, val));
        }

        // 功法进度相关扩展方法
        public static float GetGongFaDian(this Actor actor)
        {
            actor.data.get(gongFaDian_key, out float val, 0);
            return val;
        }

        public static void SetGongFaDian(this Actor actor, float val)
        {
            actor.data.set(gongFaDian_key, val);
        }

        /// <summary>
        /// 检查角色是否拥有指定物品
        /// </summary>
        public static bool hasItem(this Actor actor, string item_id)
        {
            if (actor == null || string.IsNullOrEmpty(item_id))
                return false;

            // 首先检查是否存储在actor.data字典中
            string item_count_key = $"item_count_{item_id}";
            actor.data.get(item_count_key, out int count, 0);
            if (count > 0)
                return true;

            // 然后检查是否装备在角色身上（针对武器类型物品）
            if (actor.equipment != null && actor.canEditEquipment())
            {
                // 尝试获取武器槽位
                ActorEquipmentSlot weaponSlot = actor.equipment.getSlot(EquipmentType.Weapon);
                if (weaponSlot != null && !weaponSlot.isEmpty())
                {
                    Item item = weaponSlot.getItem();
                    if (item != null && item.asset.id == item_id)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 给角色添加指定物品（兼容原版方法名）
        /// </summary>
        public static bool addItem(this Actor actor, string item_id, int count = 1)
        {
            if (actor == null || string.IsNullOrEmpty(item_id) || count <= 0)
                return false;

            // 首先尝试获取物品资源
            ItemAsset item_asset = AssetManager.items.get(item_id);
            if (item_asset == null)
                return false;

            // 检查是否是装备类型物品
            EquipmentAsset equipment_asset = item_asset as EquipmentAsset;
            if (equipment_asset != null && equipment_asset.equipment_type == EquipmentType.Weapon)
            {
                // 如果是武器，尝试装备到武器槽
                if (actor.canEditEquipment() && actor.equipment != null)
                {
                    ActorEquipmentSlot slot = actor.equipment.getSlot(equipment_asset.equipment_type);
                    if (slot != null && slot.canChangeSlot())
                    {
                        // 生成物品实例并装备（不管槽位是否为空，因为PurchaseWeaponAction已经处理了移除低级武器的逻辑）
                        Item item = World.world.items.generateItem(equipment_asset, actor.kingdom, World.world.map_stats.player_name, count, actor, 0, true);
                        actor.equipment.setItem(item, actor);
                        return true;
                    }
                }
            }

            // 对于非装备物品或无法装备的情况，使用原版添加方式
            string item_count_key = $"item_count_{item_id}";
            actor.data.get(item_count_key, out int current_count, 0);
            actor.data.set(item_count_key, current_count + count);
            return true;
        }

        public static void ChangeGongFaDian(this Actor actor, float delta)
        {
            actor.data.get(gongFaDian_key, out float val, 0);
            val += delta;
            actor.data.set(gongFaDian_key, Mathf.Max(0, val));
        }
        
        // 功法层次相关扩展方法
        public static int GetGongFaLevel(this Actor actor)
        {
            actor.data.get(gongFaLevel_key, out int val, 0);
            return val;
        }

        public static void SetGongFaLevel(this Actor actor, int val)
        {
            actor.data.set(gongFaLevel_key, Mathf.Max(0, val));
        }

        public static void ChangeGongFaLevel(this Actor actor, int delta)
        {
            actor.data.get(gongFaLevel_key, out int val, 0);
            val += delta;
            actor.data.set(gongFaLevel_key, Mathf.Max(0, val));
        }

        // 推演点相关扩展方法
        public static float GetTuiYanDian(this Actor actor)
        {
            actor.data.get(tuiYanDian_key, out float val, 0);
            return val;
        }

        public static void SetTuiYanDian(this Actor actor, float val)
        {
            int limit = actor.GetTuiYanDianLimit();
            actor.data.set(tuiYanDian_key, Mathf.Min(limit, Mathf.Max(0, val)));
        }

        public static void ChangeTuiYanDian(this Actor actor, float delta)
        {
            actor.data.get(tuiYanDian_key, out float val, 0);
            val += delta;
            // 上限为99999999
            actor.data.set(tuiYanDian_key, Mathf.Min(99999999f, Mathf.Max(0, val)));
        }

        // 获取推演点上限
        public static int GetTuiYanDianLimit(this Actor actor)
        {
            int shenTongLevel = actor.GetShenTongLevel();
            
            // 根据神通层次确定推演点上限
            switch (shenTongLevel)
            {
                case 0: // 未觉醒神通
                    return 1000;
                case 1: // 法术层次
                    return 10000;
                case 2: // 小神通层次
                    return 100000;
                case 3: // 大神通层次
                    return 1000000;
                case 4: // 天地大神通层次
                    return 10000000;
                case 5: // 无上妙法神通层次
                    return 99999999;
                default:
                    return 1000;
            }
        }

        // 神通层次相关扩展方法
        public static int GetShenTongLevel(this Actor actor)
        {
            actor.data.get(shenTongLevel_key, out int val, 0);
            return val;
        }

        public static void SetShenTongLevel(this Actor actor, int val)
        {
            actor.data.set(shenTongLevel_key, Mathf.Max(0, Mathf.Min(5, val)));
        }

        public static void ChangeShenTongLevel(this Actor actor, int delta)
        {
            actor.data.get(shenTongLevel_key, out int val, 0);
            val += delta;
            actor.data.set(shenTongLevel_key, Mathf.Max(0, Mathf.Min(5, val)));
        }
        
        // 获取当前功法点对应的阶段上限值
        public static int GetGongFaPointLimit(this Actor actor)
        {
            float currentPoints = actor.GetGongFaDian();
            
            if (currentPoints < 100) return 100;            // 凡俗阶 -> 炼炁阶
            else if (currentPoints < 1000) return 1000;      // 炼炁阶 -> 道基阶
            else if (currentPoints < 10000) return 10000;    // 道基阶 -> 紫府阶
            else if (currentPoints < 30000) return 30000;    // 紫府阶 -> 道胎阶
            else if (currentPoints < 60000) return 60000;    // 道胎阶 -> 元神阶
            else if (currentPoints < 100000) return 100000;  // 元神阶 -> 法相阶
            else if (currentPoints < 500000) return 500000;  // 法相阶 -> 羽化阶
            else if (currentPoints < 1000000) return 1000000;// 羽化阶 -> 大道阶
            else return 9999999;                           // 大道阶上限
        }
        
        // 根据当前功法点判断功法层次名称
        public static string GetGongFaLevelName(this Actor actor)
        {
            float currentPoints = actor.GetGongFaDian();
            
            if (currentPoints < 100) return "凡俗阶";
            else if (currentPoints < 1000) return "炼炁阶";
            else if (currentPoints < 10000) return "道基阶";
            else if (currentPoints < 30000) return "紫府阶";
            else if (currentPoints < 60000) return "道胎阶";
            else if (currentPoints < 100000) return "元神阶";
            else if (currentPoints < 500000) return "法相阶";
            else if (currentPoints < 1000000) return "羽化阶";
            else return "大道阶";
        }
        
        // 获取角色的功法名称
        public static string GetGongFaName(this Actor actor, string noun = null)
        {
            float currentPoints = actor.GetGongFaDian();
            
            // 凡俗阶时返回固定的"练气残篇"
            if (currentPoints < 100)
            {
                return "练气残篇";
            }
            
            // 检查是否有存储的当前功法名称
            actor.data.get(current_gongfa_name_key, out string currentName, null);
            
            // 获取当前的功法层次名称和数值
            string currentLevelName = actor.GetGongFaLevelName();
            int currentGongFaLevel = GongFaNaming.DetermineGongFaLevel(currentPoints);
            
            // 检查是否存储了对应的层次名称和数值
            string storedLevelKey = $"{current_gongfa_name_key}_level";
            actor.data.get(storedLevelKey, out string storedLevelName, null);
            string lastLevelKey = "xiantu.gongfa_last_level";
            actor.data.get(lastLevelKey, out int lastGongFaLevel, -1);
            
            // 如果没有存储的名称，或者功法层次名称或数值发生了变化，则生成新名称
            if (string.IsNullOrEmpty(currentName) || currentLevelName != storedLevelName || currentGongFaLevel != lastGongFaLevel)
            {
                // 保存旧名称的副本，用于继承逻辑
                string oldName = currentName;
                
                // 生成新的功法名称，传入旧名称以确保正确继承
                currentName = GongFaNaming.GenerateGongFaNameForActor(actor, noun, oldName);
                
                // 存储新名称和对应的层次信息
                actor.data.set(current_gongfa_name_key, currentName);
                actor.data.set(storedLevelKey, currentLevelName);
                actor.data.set(lastLevelKey, currentGongFaLevel);
            }
            
            return currentName;
        }

        // 灵石相关扩展方法
        public static float GetLingShi(this Actor actor)
        {
            actor.data.get(lingShi_key, out float val, 0);
            return val;
        }

        public static void SetLingShi(this Actor actor, float val)
        {
            actor.data.set(lingShi_key, val);
        }

        public static void ChangeLingShi(this Actor actor, float delta)
        {
            actor.data.get(lingShi_key, out float val, 0);
            val += delta;
            actor.data.set(lingShi_key, Mathf.Max(0, val));
        }
        
        // 中品灵石相关扩展方法
        public static float GetZhongPin(this Actor actor)
        {
            actor.data.get(zhongPin_key, out float val, 0);
            return val;
        }

        public static void SetZhongPin(this Actor actor, float val)
        {
            actor.data.set(zhongPin_key, val);
        }

        public static void ChangeZhongPin(this Actor actor, float delta)
        {
            actor.data.get(zhongPin_key, out float val, 0);
            val += delta;
            actor.data.set(zhongPin_key, Mathf.Max(0, val));
        }
        
        // 上品灵石相关扩展方法
        public static float GetShangPin(this Actor actor)
        {
            actor.data.get(shangPin_key, out float val, 0);
            return val;
        }

        public static void SetShangPin(this Actor actor, float val)
        {
            actor.data.set(shangPin_key, val);
        }

        public static void ChangeShangPin(this Actor actor, float delta)
        {
            actor.data.get(shangPin_key, out float val, 0);
            val += delta;
            actor.data.set(shangPin_key, Mathf.Max(0, val));
        }

        // 购买基础木剑 - 练气期可用
        public static void BuyBasicWoodenSword(this Actor actor)
        {
            // 检查是否已经拥有该武器
            if (actor.hasItem("magic_wooden_sword"))
                return;

            // 检查是否达到练气期
            if (!actor.hasTrait("XianTu1"))
                return;

            // 检查是否有足够的下品灵石
            if (actor.GetLingShi() >= 10f)
            {
                actor.ChangeLingShi(-10f);
                actor.addItem("magic_wooden_sword", 1);
            }
        }

        // 购买精铁剑 - 筑基期可用
        public static void BuyFineIronSword(this Actor actor)
        {
            if (actor.hasItem("magic_fine_iron_sword"))
                return;

            if (!actor.hasTrait("XianTu2"))
                return;

            if (actor.GetLingShi() >= 100f)
            {
                actor.ChangeLingShi(-100f);
                actor.addItem("magic_fine_iron_sword", 1);
            }
        }

        // 购买青钢剑 - 金丹期可用
        public static void BuyGreenSteelSword(this Actor actor)
        {
            if (actor.hasItem("magic_green_steel_sword"))
                return;

            if (!actor.hasTrait("XianTu3"))
                return;

            if (actor.GetLingShi() >= 1000f)
            {
                actor.ChangeLingShi(-1000f);
                actor.addItem("magic_green_steel_sword", 1);
            }
        }

        // 购买火铜剑 - 元婴期可用
        public static void BuyFireCopperSword(this Actor actor)
        {
            if (actor.hasItem("magic_fire_spirit_sword"))
                return;

            if (!actor.hasTrait("XianTu4"))
                return;

            if (actor.GetZhongPin() >= 1f)
            {
                actor.ChangeZhongPin(-1f);
                actor.addItem("magic_fire_spirit_sword", 1);
            }
        }

        // 购买玄极剑 - 化神期可用
        public static void BuyXuanJiSword(this Actor actor)
        {
            if (actor.hasItem("magic_water_spirit_sword"))
                return;

            if (!actor.hasTrait("XianTu5"))
                return;

            if (actor.GetZhongPin() >= 10f)
            {
                actor.ChangeZhongPin(-10f);
                actor.addItem("magic_water_spirit_sword", 1);
            }
        }

        // 购买紫霄剑 - 合体期可用
        public static void BuyPurpleCloudSword(this Actor actor)
        {
            if (actor.hasItem("magic_purple_cloud_sword"))
                return;

            if (!actor.hasTrait("XianTu6"))
                return;

            if (actor.GetZhongPin() >= 100f)
            {
                actor.ChangeZhongPin(-100f);
                actor.addItem("magic_purple_cloud_sword", 1);
            }
        }

        // 购买太初剑 - 大乘期可用
        public static void BuyPrimalSword(this Actor actor)
        {
            if (actor.hasItem("magic_primal_sword"))
                return;

            if (!actor.hasTrait("XianTu7"))
                return;

            if (actor.GetZhongPin() >= 1000f)
            {
                actor.ChangeZhongPin(-1000f);
                actor.addItem("magic_primal_sword", 1);
            }
        }

        // 自动购买对应境界的武器
        public static void AutoBuyRealmWeapon(this Actor actor)
        {
            // 按照境界从高到低检查，确保只购买当前境界能买的最高级武器
            if (actor.hasTrait("XianTu7")) // 大乘期
            {
                actor.BuyPrimalSword();
            }
            else if (actor.hasTrait("XianTu6")) // 合体期
            {
                actor.BuyPurpleCloudSword();
            }
            else if (actor.hasTrait("XianTu5")) // 化神期
            {
                actor.BuyXuanJiSword();
            }
            else if (actor.hasTrait("XianTu4")) // 元婴期
            {
                actor.BuyFireCopperSword();
            }
            else if (actor.hasTrait("XianTu3")) // 金丹期
            {
                actor.BuyGreenSteelSword();
            }
            else if (actor.hasTrait("XianTu2")) // 筑基期
            {
                actor.BuyFineIronSword();
            }
            else if (actor.hasTrait("XianTu1")) // 练气期
            {
                actor.BuyBasicWoodenSword();
            }
        }

        // 存储每秒伤害信息的字典
        private static Dictionary<string, Dictionary<string, float>> damageThisSecond = new Dictionary<string, Dictionary<string, float>>();
        private static Dictionary<string, Dictionary<string, float>> lastDamageTime = new Dictionary<string, Dictionary<string, float>>();

        // 获取修士的位格等级
        public static int GetRealmLevel(this Actor actor)
        {
            // 帝阶修士
            if (actor.hasTrait("TyWudao94"))
            {
                return 10; // 帝阶
            }
            
            // 道果修士：同时拥有半仙和任意一个道果，或者同时拥有涅槃和任意一个道果
            bool hasAnyDaoGuo = actor.hasTrait("DaoGuo1") || actor.hasTrait("DaoGuo2") || actor.hasTrait("DaoGuo3") || 
                               actor.hasTrait("DaoGuo4") || actor.hasTrait("DaoGuo5") || actor.hasTrait("DaoGuo6") || 
                               actor.hasTrait("DaoGuo7") || actor.hasTrait("DaoGuo8") || actor.hasTrait("DaoGuo9") || 
                               actor.hasTrait("DaoGuo91") || actor.hasTrait("DaoGuo92") || actor.hasTrait("DaoGuo93") || 
                               actor.hasTrait("DaoGuo94") || actor.hasTrait("DaoGuo95") || actor.hasTrait("DaoGuo96");
            
            if (hasAnyDaoGuo && (actor.hasTrait("XianTu8") || actor.hasTrait("TyWudao92") || actor.hasTrait("TyWudao93")))
            {
                return 9; // 道果
            }
            
            // 半仙境界/涅槃境
            if (actor.hasTrait("XianTu8") || actor.hasTrait("TyWudao92"))
            {
                return 8; // 半仙
            }
            
            // 大乘境界/圣躯境
            if (actor.hasTrait("XianTu7") || actor.hasTrait("TyWudao91"))
            {
                return 7; // 大乘
            }
            
            // 合体境界/山海境
            if (actor.hasTrait("XianTu6") || actor.hasTrait("TyWudao9"))
            {
                return 6; // 合体
            }
            
            // 化神境界/明我境
            if (actor.hasTrait("XianTu5") || actor.hasTrait("TyWudao8"))
            {
                return 5; // 化神
            }
            
            // 元婴境界/踏虚境
            if (actor.hasTrait("XianTu4") || actor.hasTrait("TyWudao7"))
            {
                return 4; // 元婴
            }
            
            // 金丹境界/神游境
            if (actor.hasTrait("XianTu3") || actor.hasTrait("TyWudao6"))
            {
                return 3; // 金丹
            }
            
            // 筑基境界/天人境
            if (actor.hasTrait("XianTu2") || actor.hasTrait("TyWudao5"))
            {
                return 2; // 筑基
            }
            
            // 凡人/练气期/真意境/玄罡境
            return 1; // 筑基以下
        }

        // 应用伤害并应用位格减伤制度
        public static void ApplyDamageWithRealmReduction(this Actor target, float damage, Actor attacker = null)
        {
            // 如果没有攻击者，直接应用伤害
            if (attacker == null)
            {
                target.restoreHealth(-Mathf.RoundToInt(damage));
                return;
            }

            int attackerLevel = attacker.GetRealmLevel();
            int targetLevel = target.GetRealmLevel();
            float finalDamage = damage;
            float maxHealth = target.getMaxHealth();

            // 获取当前时间
            float currentTime = Time.time;
            string attackerId = attacker.id.ToString();
            string targetId = target.id.ToString();

            // 初始化伤害跟踪数据
            if (!lastDamageTime.ContainsKey(attackerId))
                lastDamageTime[attackerId] = new Dictionary<string, float>();
            
            if (!lastDamageTime[attackerId].ContainsKey(targetId))
                lastDamageTime[attackerId][targetId] = currentTime;
            
            if (!damageThisSecond.ContainsKey(attackerId))
                damageThisSecond[attackerId] = new Dictionary<string, float>();
            
            if (!damageThisSecond[attackerId].ContainsKey(targetId))
                damageThisSecond[attackerId][targetId] = 0;

            // 检查是否需要重置每秒伤害计数
            if (currentTime - lastDamageTime[attackerId][targetId] >= 1.0f)
            {
                damageThisSecond[attackerId][targetId] = 0;
                lastDamageTime[attackerId][targetId] = currentTime;
            }

            // 根据位格差异确定伤害限制和反噬效果
            int levelDiff = attackerLevel - targetLevel;

            if (levelDiff <= -2) // 攻击者比目标低至少两个境界
            {
                // 筑基以下（1）对筑基（2）: 1秒内最多10%生命上限伤害
                if (attackerLevel == 1 && targetLevel == 2)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 筑基以下（1）对金丹（3）: 1秒内最多1点伤害
                else if (attackerLevel == 1 && targetLevel == 3)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 筑基以下（1）对元婴（4）及以上: 无法造成伤害
                else if (attackerLevel == 1 && targetLevel >= 4)
                {
                    finalDamage = 0;
                }
                // 筑基以下（1）对化神（5）及以上: 攻击自己死亡（位格反噬）
                else if (attackerLevel == 1 && targetLevel >= 5)
                {
                    finalDamage = 0;
                    // 位格反噬：低阶存在攻击高阶存在会受到自身生命力的反噬
                    attacker.restoreHealth(-Mathf.RoundToInt(attacker.getMaxHealth() * 1f)); // 攻击者受到生命上限50%的伤害
                }
                // 筑基（2）对金丹（3）: 1秒内最多10%生命上限伤害
                else if (attackerLevel == 2 && targetLevel == 3)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 筑基（2）对元婴（4）: 1秒内最多1点伤害
                else if (attackerLevel == 2 && targetLevel == 4)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 筑基（2）对化神（5）及以上: 无法造成伤害
                else if (attackerLevel == 2 && targetLevel >= 5)
                {
                    finalDamage = 0;
                }
                // 筑基（2）对合体（6）及以上: 攻击自己死亡（位格反噬）
                else if (attackerLevel == 2 && targetLevel >= 6)
                {
                    finalDamage = 0;
                    // 位格反噬：低阶存在攻击高阶存在会受到自身生命力的反噬
                    attacker.restoreHealth(-Mathf.RoundToInt(attacker.getMaxHealth() * 1f)); // 攻击者受到生命上限50%的伤害
                }
                // 金丹（3）对元婴（4）: 1秒内最多10%生命上限伤害
                else if (attackerLevel == 3 && targetLevel == 4)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 金丹（3）对化神（5）: 1秒内最多1点伤害
                else if (attackerLevel == 3 && targetLevel == 5)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 金丹（3）对合体（6）及以上: 无法造成伤害
                else if (attackerLevel == 3 && targetLevel >= 6)
                {
                    finalDamage = 0;
                }
                // 金丹（3）对大乘（7）及以上: 攻击自己死亡（位格反噬）
                else if (attackerLevel == 3 && targetLevel >= 7)
                {
                    finalDamage = 0;
                    // 位格反噬：低阶存在攻击高阶存在会受到自身生命力的反噬
                    attacker.restoreHealth(-Mathf.RoundToInt(attacker.getMaxHealth() * 1f)); // 攻击者受到生命上限30%的伤害
                }
                // 元婴（4）对化神（5）: 1秒内最多10%生命上限伤害
                else if (attackerLevel == 4 && targetLevel == 5)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 元婴（4）对合体（6）: 1秒内最多1点伤害
                else if (attackerLevel == 4 && targetLevel == 6)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 元婴（4）对大乘（7）及以上: 无法造成伤害
                else if (attackerLevel == 4 && targetLevel >= 7)
                {
                    finalDamage = 0;
                }
                // 元婴（4）对半仙（8）及以上: 攻击自己死亡（位格反噬）
                else if (attackerLevel == 4 && targetLevel >= 8)
                {
                    finalDamage = 0;
                    // 位格反噬：低阶存在攻击高阶存在会受到自身生命力的反噬
                    attacker.restoreHealth(-Mathf.RoundToInt(attacker.getMaxHealth() * 1f)); // 攻击者受到生命上限20%的伤害
                }
                // 化神（5）对合体（6）: 1秒内最多10%生命上限伤害
                else if (attackerLevel == 5 && targetLevel == 6)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 化神（5）对大乘（7）: 1秒内最多1点伤害
                else if (attackerLevel == 5 && targetLevel == 7)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 化神（5）对半仙（8）及以上: 无法造成伤害
                else if (attackerLevel == 5 && targetLevel >= 8)
                {
                    finalDamage = 0;
                }
                // 化神（5）对道果（9）: 攻击自己死亡（位格反噬）
                else if (attackerLevel == 5 && targetLevel == 9)
                {
                    finalDamage = 0;
                    // 位格反噬：低阶存在攻击高阶存在会受到自身生命力的反噬
                    attacker.restoreHealth(-Mathf.RoundToInt(attacker.getMaxHealth() * 1f)); // 攻击者受到生命上限10%的伤害
                }
                // 合体（6）对大乘（7）: 1秒内最多10%生命上限伤害
                else if (attackerLevel == 6 && targetLevel == 7)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 合体（6）对半仙（8）: 1秒内最多1点伤害
                else if (attackerLevel == 6 && targetLevel == 8)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 合体（6）对道果（9）: 无法造成伤害
                else if (attackerLevel == 6 && targetLevel == 9)
                {
                    finalDamage = 0;
                }
                // 大乘（7）对半仙（8）: 1秒内最多10%生命上限伤害
                else if (attackerLevel == 7 && targetLevel == 8)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 大乘（7）对道果（9）: 1秒内最多1点伤害
                else if (attackerLevel == 7 && targetLevel == 9)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 半仙（8）对道果（9）: 1秒内最多10%生命上限伤害
                else if (attackerLevel == 8 && targetLevel == 9)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 半仙（8）对帝阶（10）: 1秒内最多1点伤害
                else if (attackerLevel == 8 && targetLevel == 10)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 道果（9）对帝阶（10）: 1秒内最多10%生命上限伤害
                else if (attackerLevel == 9 && targetLevel == 10)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 其他情况：根据位格差距按比例减少伤害（诡秘之主风格）
                else if (attackerLevel < targetLevel)
                {
                    // 根据位格差距按指数级减少伤害
                    float reductionFactor = Mathf.Pow(0.1f, Mathf.Abs(levelDiff));
                    finalDamage = Mathf.Max(1, damage * reductionFactor);
                    
                    // 应用每秒伤害限制
                    float maxAllowedDamage = maxHealth * 0.05f; // 最多造成目标生命上限5%的伤害
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                else
                {
                    finalDamage = 0;
                }
            }
            // 境界相同或攻击者更高
            else
            {
                // 同境界时，限制每秒伤害为目标生命上限的20%
                if (levelDiff == 0)
                {
                    float maxAllowedDamage = maxHealth * 0.2f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 攻击者境界更高时，不做过多限制，但仍有每秒伤害上限
                else
                {
                    float maxAllowedDamage = maxHealth * 0.5f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
            }

            // 应用最终伤害
            if (finalDamage > 0)
            {
                target.restoreHealth(-Mathf.RoundToInt(finalDamage));
            }
        }
        
        // 应用真伤并应用位格减伤制度
        public static void ApplyTrueDamageWithRealmReduction(this Actor target, float damage, Actor attacker = null)
        {
            // 如果没有攻击者，直接应用真伤
            if (attacker == null)
            {
                target.restoreHealth(-Mathf.RoundToInt(damage));
                return;
            }

            int attackerLevel = attacker.GetRealmLevel();
            int targetLevel = target.GetRealmLevel();
            float finalDamage = damage;
            float maxHealth = target.getMaxHealth();

            // 获取当前时间
            float currentTime = Time.time;
            string attackerId = attacker.id.ToString();
            string targetId = target.id.ToString();

            // 初始化伤害跟踪数据
            if (!lastDamageTime.ContainsKey(attackerId))
                lastDamageTime[attackerId] = new Dictionary<string, float>();
            
            if (!lastDamageTime[attackerId].ContainsKey(targetId))
                lastDamageTime[attackerId][targetId] = currentTime;
            
            if (!damageThisSecond.ContainsKey(attackerId))
                damageThisSecond[attackerId] = new Dictionary<string, float>();
            
            if (!damageThisSecond[attackerId].ContainsKey(targetId))
                damageThisSecond[attackerId][targetId] = 0;

            // 检查是否需要重置每秒伤害计数
            if (currentTime - lastDamageTime[attackerId][targetId] >= 1.0f)
            {
                damageThisSecond[attackerId][targetId] = 0;
                lastDamageTime[attackerId][targetId] = currentTime;
            }

            // 根据位格差异确定伤害限制和反噬效果
            int levelDiff = attackerLevel - targetLevel;

            if (levelDiff <= -2) // 攻击者比目标低至少两个境界
            {
                // 筑基以下（1）对筑基（2）: 1秒内最多10%生命上限真伤
                if (attackerLevel == 1 && targetLevel == 2)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 筑基以下（1）对金丹（3）: 1秒内最多1点真伤
                else if (attackerLevel == 1 && targetLevel == 3)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 筑基以下（1）对元婴（4）及以上: 无法造成真伤
                else if (attackerLevel == 1 && targetLevel >= 4)
                {
                    finalDamage = 0;
                }
                // 筑基以下（1）对化神（5）及以上: 攻击自己死亡（位格反噬）
                else if (attackerLevel == 1 && targetLevel >= 5)
                {
                    finalDamage = 0;
                    // 位格反噬：低阶存在攻击高阶存在会受到自身生命力的反噬
                    attacker.restoreHealth(-Mathf.RoundToInt(attacker.getMaxHealth() * 1f)); // 攻击者受到生命上限50%的伤害
                }
                // 筑基（2）对金丹（3）: 1秒内最多10%生命上限真伤
                else if (attackerLevel == 2 && targetLevel == 3)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 筑基（2）对元婴（4）: 1秒内最多1点真伤
                else if (attackerLevel == 2 && targetLevel == 4)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 筑基（2）对化神（5）及以上: 无法造成真伤
                else if (attackerLevel == 2 && targetLevel >= 5)
                {
                    finalDamage = 0;
                }
                // 筑基（2）对合体（6）及以上: 攻击自己死亡（位格反噬）
                else if (attackerLevel == 2 && targetLevel >= 6)
                {
                    finalDamage = 0;
                    // 位格反噬：低阶存在攻击高阶存在会受到自身生命力的反噬
                    attacker.restoreHealth(-Mathf.RoundToInt(attacker.getMaxHealth() * 1f)); // 攻击者受到生命上限50%的伤害
                }
                // 金丹（3）对元婴（4）: 1秒内最多10%生命上限真伤
                else if (attackerLevel == 3 && targetLevel == 4)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 金丹（3）对化神（5）: 1秒内最多1点真伤
                else if (attackerLevel == 3 && targetLevel == 5)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 金丹（3）对合体（6）及以上: 无法造成真伤
                else if (attackerLevel == 3 && targetLevel >= 6)
                {
                    finalDamage = 0;
                }
                // 金丹（3）对大乘（7）及以上: 攻击自己死亡（位格反噬）
                else if (attackerLevel == 3 && targetLevel >= 7)
                {
                    finalDamage = 0;
                    // 位格反噬：低阶存在攻击高阶存在会受到自身生命力的反噬
                    attacker.restoreHealth(-Mathf.RoundToInt(attacker.getMaxHealth() * 1f)); // 攻击者受到生命上限30%的伤害
                }
                // 元婴（4）对化神（5）: 1秒内最多10%生命上限真伤
                else if (attackerLevel == 4 && targetLevel == 5)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 元婴（4）对合体（6）: 1秒内最多1点真伤
                else if (attackerLevel == 4 && targetLevel == 6)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 元婴（4）对大乘（7）及以上: 无法造成真伤
                else if (attackerLevel == 4 && targetLevel >= 7)
                {
                    finalDamage = 0;
                }
                // 元婴（4）对半仙（8）及以上: 攻击自己死亡（位格反噬）
                else if (attackerLevel == 4 && targetLevel >= 8)
                {
                    finalDamage = 0;
                    // 位格反噬：低阶存在攻击高阶存在会受到自身生命力的反噬
                    attacker.restoreHealth(-Mathf.RoundToInt(attacker.getMaxHealth() * 1f)); // 攻击者受到生命上限20%的伤害
                }
                // 化神（5）对合体（6）: 1秒内最多10%生命上限真伤
                else if (attackerLevel == 5 && targetLevel == 6)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 化神（5）对大乘（7）: 1秒内最多1点真伤
                else if (attackerLevel == 5 && targetLevel == 7)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 化神（5）对半仙（8）及以上: 无法造成真伤
                else if (attackerLevel == 5 && targetLevel >= 8)
                {
                    finalDamage = 0;
                }
                // 化神（5）对道果（9）: 攻击自己死亡（位格反噬）
                else if (attackerLevel == 5 && targetLevel == 9)
                {
                    finalDamage = 0;
                    // 位格反噬：低阶存在攻击高阶存在会受到自身生命力的反噬
                    attacker.restoreHealth(-Mathf.RoundToInt(attacker.getMaxHealth() * 1f)); // 攻击者受到生命上限10%的伤害
                }
                // 合体（6）对大乘（7）: 1秒内最多10%生命上限真伤
                else if (attackerLevel == 6 && targetLevel == 7)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 合体（6）对半仙（8）: 1秒内最多1点真伤
                else if (attackerLevel == 6 && targetLevel == 8)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 合体（6）对道果（9）: 无法造成真伤
                else if (attackerLevel == 6 && targetLevel == 9)
                {
                    finalDamage = 0;
                }
                // 大乘（7）对半仙（8）: 1秒内最多10%生命上限真伤
                else if (attackerLevel == 7 && targetLevel == 8)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 大乘（7）对道果（9）: 1秒内最多1点真伤
                else if (attackerLevel == 7 && targetLevel == 9)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 半仙（8）对道果（9）: 1秒内最多10%生命上限真伤
                else if (attackerLevel == 8 && targetLevel == 9)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 半仙（8）对帝阶（10）: 1秒内最多1点真伤
                else if (attackerLevel == 8 && targetLevel == 10)
                {
                    if (damageThisSecond[attackerId][targetId] < 1)
                    {
                        finalDamage = 1 - damageThisSecond[attackerId][targetId];
                        damageThisSecond[attackerId][targetId] = 1;
                    }
                    else
                    {
                        finalDamage = 0;
                    }
                }
                // 道果（9）对帝阶（10）: 1秒内最多10%生命上限真伤
                else if (attackerLevel == 9 && targetLevel == 10)
                {
                    float maxAllowedDamage = maxHealth * 0.1f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 其他情况：根据位格差距按比例减少真伤（诡秘之主风格）
                else if (attackerLevel < targetLevel)
                {
                    // 根据位格差距按指数级减少真伤
                    float reductionFactor = Mathf.Pow(0.1f, Mathf.Abs(levelDiff));
                    finalDamage = Mathf.Max(1, damage * reductionFactor);
                    
                    // 应用每秒伤害限制
                    float maxAllowedDamage = maxHealth * 0.05f; // 最多造成目标生命上限5%的真伤
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                else
                {
                    finalDamage = 0;
                }
            }
            // 境界相同或攻击者更高
            else
            {
                // 同境界时，限制每秒真伤为目标生命上限的20%
                if (levelDiff == 0)
                {
                    float maxAllowedDamage = maxHealth * 0.2f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
                // 攻击者境界更高时，不做过多限制，但仍有每秒真伤上限
                else
                {
                    float maxAllowedDamage = maxHealth * 0.5f;
                    if (damageThisSecond[attackerId][targetId] + finalDamage > maxAllowedDamage)
                    {
                        finalDamage = maxAllowedDamage - damageThisSecond[attackerId][targetId];
                    }
                    damageThisSecond[attackerId][targetId] += finalDamage;
                }
            }

            // 应用最终真伤
            if (finalDamage > 0)
            {
                target.restoreHealth(-Mathf.RoundToInt(finalDamage));
            }
        }
        
        // 添加或修改角色名称后缀
        public static void UpdateNameSuffix(this Actor actor, string suffix)
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
        
        // 下品灵石转换为中品灵石
        public static void ConvertLingShiToZhongPin(this Actor actor)
        {
            float lingShiAmount = actor.GetLingShi();
            if (lingShiAmount >= 10000)
            {
                int convertCount = Mathf.FloorToInt(lingShiAmount / 10000);
                actor.ChangeLingShi(-convertCount * 10000);
                actor.ChangeZhongPin(convertCount);
            }
        }
        
        // 中品灵石转换为上品灵石
        public static void ConvertZhongPinToShangPin(this Actor actor)
        {
            float zhongPinAmount = actor.GetZhongPin();
            if (zhongPinAmount >= 10000)
            {
                int convertCount = Mathf.FloorToInt(zhongPinAmount / 10000);
                actor.ChangeZhongPin(-convertCount * 10000);
                actor.ChangeShangPin(convertCount);
            }
        }
        
        // 购买筑基丹 (TupoDanyao1)
        public static void BuyTupoDanyao1(this Actor actor)
        {
            // 检查是否已经拥有筑基丹或者已经达到筑基及以上境界
            if (!actor.hasTrait("TupoDanyao1") && !actor.hasTrait("XianTu2") && !actor.hasTrait("XianTu3") && 
                !actor.hasTrait("XianTu4") && !actor.hasTrait("XianTu5") && !actor.hasTrait("XianTu6") && 
                !actor.hasTrait("XianTu7") && !actor.hasTrait("XianTu8"))
            {
                float lingShiAmount = actor.GetLingShi();
                if (lingShiAmount >= 50)
                {
                    actor.ChangeLingShi(-50);
                    actor.addTrait("TupoDanyao1");
                }
            }
        }
        
        // 购买化金丹 (TupoDanyao2)
        public static void BuyTupoDanyao2(this Actor actor)
        {
            // 检查是否已经拥有化金丹或者已经达到金丹及以上境界
            if (!actor.hasTrait("TupoDanyao2") && !actor.hasTrait("XianTu3") && !actor.hasTrait("XianTu4") && 
                !actor.hasTrait("XianTu5") && !actor.hasTrait("XianTu6") && !actor.hasTrait("XianTu7") && 
                !actor.hasTrait("XianTu8"))
            {
                float lingShiAmount = actor.GetLingShi();
                if (lingShiAmount >= 500)
                {
                    actor.ChangeLingShi(-500);
                    actor.addTrait("TupoDanyao2");
                }
            }
        }
        
        // 购买结婴丹 (TupoDanyao3)
        public static void BuyTupoDanyao3(this Actor actor)
        {
            // 检查是否已经拥有结婴丹或者已经达到元婴及以上境界
            if (!actor.hasTrait("TupoDanyao3") && !actor.hasTrait("XianTu4") && !actor.hasTrait("XianTu5") && 
                !actor.hasTrait("XianTu6") && !actor.hasTrait("XianTu7") && !actor.hasTrait("XianTu8"))
            {
                float lingShiAmount = actor.GetLingShi();
                if (lingShiAmount >= 5000)
                {
                    actor.ChangeLingShi(-5000);
                    actor.addTrait("TupoDanyao3");
                }
            }
        }
        
        // 购买凝神丹 (TupoDanyao4)
        public static void BuyTupoDanyao4(this Actor actor)
        {
            // 检查是否已经拥有凝神丹或者已经达到化神及以上境界
            if (!actor.hasTrait("TupoDanyao4") && !actor.hasTrait("XianTu5") && !actor.hasTrait("XianTu6") && 
                !actor.hasTrait("XianTu7") && !actor.hasTrait("XianTu8"))
            {
                float zhongPinAmount = actor.GetZhongPin();
                if (zhongPinAmount >= 5)
                {
                    actor.ChangeZhongPin(-5);
                    actor.addTrait("TupoDanyao4");
                }
            }
        }
        
        // 购买合体丹 (TupoDanyao5)
        public static void BuyTupoDanyao5(this Actor actor)
        {
            // 检查是否已经拥有合体丹或者已经达到合体及以上境界
            if (!actor.hasTrait("TupoDanyao5") && !actor.hasTrait("XianTu6") && !actor.hasTrait("XianTu7") && 
                !actor.hasTrait("XianTu8"))
            {
                float zhongPinAmount = actor.GetZhongPin();
                if (zhongPinAmount >= 500)
                {
                    actor.ChangeZhongPin(-500);
                    actor.addTrait("TupoDanyao5");
                }
            }
        }
        
        // 购买大乘丹 (TupoDanyao6)
        public static void BuyTupoDanyao6(this Actor actor)
        {
            // 检查是否已经拥有大乘丹或者已经达到大乘及以上境界
            if (!actor.hasTrait("TupoDanyao6") && !actor.hasTrait("XianTu7") && !actor.hasTrait("XianTu8"))
            {
                float zhongPinAmount = actor.GetZhongPin();
                if (zhongPinAmount >= 5000)
                {
                    actor.ChangeZhongPin(-5000);
                    actor.addTrait("TupoDanyao6");
                }
            }
        }
        
        // 购买地仙丹 (TupoDanyao7)
        public static void BuyTupoDanyao7(this Actor actor)
        {
            // 检查是否已经拥有地仙丹或者已经达到地仙及以上境界
            if (!actor.hasTrait("TupoDanyao7") && !actor.hasTrait("XianTu8"))
            {
                float shangPinAmount = actor.GetShangPin();
                if (shangPinAmount >= 5)
                {
                    actor.ChangeShangPin(-5);
                    actor.addTrait("TupoDanyao7");
                }
            }
        }
        
        // 购买凝气丹 (XiulianDanyao1)
        public static void BuyXiulianDanyao1(this Actor actor)
        {
            // 检查是否已经拥有凝气丹或者已经达到筑基及以上境界
            // 凡人可以直接购买，练气期(XianTu1)需要拥有筑基丹才能购买
            if (!actor.hasTrait("XiulianDanyao1") && !actor.hasTrait("XianTu2") && !actor.hasTrait("XianTu3") && 
                !actor.hasTrait("XianTu4") && !actor.hasTrait("XianTu5") && !actor.hasTrait("XianTu6") && 
                !actor.hasTrait("XianTu7") && !actor.hasTrait("XianTu8") &&
                // 凡人条件或练气期拥有筑基丹条件
                (!actor.hasTrait("XianTu1") || actor.hasTrait("TupoDanyao1")))
            {
                float lingShiAmount = actor.GetLingShi();
                if (lingShiAmount >= 10)
                {
                    actor.ChangeLingShi(-10);
                    actor.addTrait("XiulianDanyao1");
                }
            }
        }
        
        // 购买青元丹 (XiulianDanyao2)
        public static void BuyXiulianDanyao2(this Actor actor)
        {
            // 检查是否已经拥有青元丹或者已经达到金丹及以上境界，并且是否拥有化金丹（筑基期条件）
            if (!actor.hasTrait("XiulianDanyao2") && !actor.hasTrait("XianTu3") && !actor.hasTrait("XianTu4") && 
                !actor.hasTrait("XianTu5") && !actor.hasTrait("XianTu6") && !actor.hasTrait("XianTu7") && 
                !actor.hasTrait("XianTu8") && actor.hasTrait("XianTu2") && actor.hasTrait("TupoDanyao2"))
            {
                float lingShiAmount = actor.GetLingShi();
                if (lingShiAmount >= 100)
                {
                    actor.ChangeLingShi(-100);
                    actor.addTrait("XiulianDanyao2");
                }
            }
        }
        
        // 购买地煞丹 (XiulianDanyao3)
        public static void BuyXiulianDanyao3(this Actor actor)
        {
            // 检查是否已经拥有地煞丹或者已经达到元婴及以上境界，并且是否拥有结婴丹（金丹期条件）
            if (!actor.hasTrait("XiulianDanyao3") && !actor.hasTrait("XianTu4") && !actor.hasTrait("XianTu5") && 
                !actor.hasTrait("XianTu6") && !actor.hasTrait("XianTu7") && !actor.hasTrait("XianTu8") && 
                actor.hasTrait("XianTu3") && actor.hasTrait("TupoDanyao3"))
            {
                float lingShiAmount = actor.GetLingShi();
                if (lingShiAmount >= 1000)
                {
                    actor.ChangeLingShi(-1000);
                    actor.addTrait("XiulianDanyao3");
                }
            }
        }
        
        // 购买天罡丹 (XiulianDanyao4)
        public static void BuyXiulianDanyao4(this Actor actor)
        {
            // 检查是否已经拥有天罡丹或者已经达到化神及以上境界，并且是否拥有凝神丹（元婴期条件）
            if (!actor.hasTrait("XiulianDanyao4") && !actor.hasTrait("XianTu5") && !actor.hasTrait("XianTu6") && 
                !actor.hasTrait("XianTu7") && !actor.hasTrait("XianTu8") && actor.hasTrait("XianTu4") && 
                actor.hasTrait("TupoDanyao4"))
            {
                float zhongPinAmount = actor.GetZhongPin();
                if (zhongPinAmount >= 1)
                {
                    actor.ChangeZhongPin(-1);
                    actor.addTrait("XiulianDanyao4");
                }
            }
        }
        
        // 购买日月丹 (XiulianDanyao5)
        public static void BuyXiulianDanyao5(this Actor actor)
        {
            // 检查是否已经拥有日月丹或者已经达到合体及以上境界，并且是否拥有合体丹（化神期条件）
            if (!actor.hasTrait("XiulianDanyao5") && !actor.hasTrait("XianTu6") && !actor.hasTrait("XianTu7") && 
                !actor.hasTrait("XianTu8") && actor.hasTrait("XianTu5") && actor.hasTrait("TupoDanyao5"))
            {
                float zhongPinAmount = actor.GetZhongPin();
                if (zhongPinAmount >= 10)
                {
                    actor.ChangeZhongPin(-10);
                    actor.addTrait("XiulianDanyao5");
                }
            }
        }
        
        // 购买法则丹 (XiulianDanyao6)
        public static void BuyXiulianDanyao6(this Actor actor)
        {
            // 检查是否已经拥有法则丹或者已经达到大乘及以上境界，并且是否拥有大乘丹（合体期条件）
            if (!actor.hasTrait("XiulianDanyao6") && !actor.hasTrait("XianTu7") && !actor.hasTrait("XianTu8") && 
                actor.hasTrait("XianTu6") && actor.hasTrait("TupoDanyao6"))
            {
                float zhongPinAmount = actor.GetZhongPin();
                if (zhongPinAmount >= 100)
                {
                    actor.ChangeZhongPin(-100);
                    actor.addTrait("XiulianDanyao6");
                }
            }
        }
        
        // 购买窃天丹 (XiulianDanyao7)
        public static void BuyXiulianDanyao7(this Actor actor)
        {
            // 检查是否已经拥有窃天丹或者已经达到地仙及以上境界，并且是否拥有地仙丹（大乘期条件）
            if (!actor.hasTrait("XiulianDanyao7") && !actor.hasTrait("XianTu8") && actor.hasTrait("XianTu7") && 
                actor.hasTrait("TupoDanyao7"))
            {
                float zhongPinAmount = actor.GetZhongPin();
                if (zhongPinAmount >= 1000)
                {
                    actor.ChangeZhongPin(-1000);
                    actor.addTrait("XiulianDanyao7");
                }
            }
        }

        // 购买界天丹 (XiulianDanyao8)
        public static void BuyXiulianDanyao8(this Actor actor)
        {
            // 检查是否已经拥有界天丹或者已经达到仙王及以上境界，并且是否拥有地仙丹（登仙境条件）
            if (!actor.hasTrait("XiulianDanyao8") && !actor.hasTrait("XianTu9") && actor.hasTrait("XianTu8"))
            {
                float shangPinAmount = actor.GetShangPin();
                if (shangPinAmount >= 10)
                {
                    actor.ChangeShangPin(-10);
                    actor.addTrait("XiulianDanyao8");
                }
            }
        }

        // 购买千界丹 (XiulianDanyao9)
        public static void BuyXiulianDanyao9(this Actor actor)
        {
            // 检查是否已经拥有千界丹，并且是否达到仙王境
            if (!actor.hasTrait("XiulianDanyao9") && actor.hasTrait("XianTu9"))
            {
                float shangPinAmount = actor.GetShangPin();
                if (shangPinAmount >= 100)
                {
                    actor.ChangeShangPin(-100);
                    actor.addTrait("XiulianDanyao9");
                }
            }
        }

        // 购买玉魄丹 (YupoDanyao8)
        public static void BuyYupoDanyao8(this Actor actor)
        {
            /* 暂时隐藏XianTu9~93相关代码
            // 检查是否可以购买（需要达到地仙境以上，并且没有玉魄丹）
            if (!actor.hasTrait("YupoDanyao8") && (actor.hasTrait("XianTu9") || actor.hasTrait("XianTu91") || 
                actor.hasTrait("XianTu92") || actor.hasTrait("XianTu93")) && actor.GetGongFaPointLimit() >= 50000)
            {
                // 消耗1颗上品灵石
                if (actor.getResource("ShangPin") >= 1)
                {
                    actor.modResource("ShangPin", -1);
                    actor.addTrait("YupoDanyao8");
                }
            }
            */
        }
        
        // 根据被击杀者的修仙境界给予击杀者灵石奖励
        public static void GetRewardForKilling(this Actor attacker, Actor victim)
        {
            // 根据被击杀者的XianTu特质判定奖励
            if (victim.hasTrait("XianTu1")) // 练气期
            {
                attacker.ChangeLingShi(10); // 10下品灵石
            }
            else if (victim.hasTrait("XianTu2")) // 筑基期
            {
                attacker.ChangeLingShi(100); // 100下品灵石
            }
            else if (victim.hasTrait("XianTu3")) // 金丹期
            {
                attacker.ChangeLingShi(1000); // 1000下品灵石
            }
            else if (victim.hasTrait("XianTu4")) // 元婴期
            {
                attacker.ChangeLingShi(10000); // 10000下品灵石
            }
            else if (victim.hasTrait("XianTu5")) // 化神期
            {
                attacker.ChangeZhongPin(10); // 10中品灵石
            }
            else if (victim.hasTrait("XianTu6")) // 合体期
            {
                attacker.ChangeZhongPin(100); // 100中品灵石
            }
            else if (victim.hasTrait("XianTu7")) // 大乘期
            {
                attacker.ChangeZhongPin(1000); // 1000中品灵石
            }
            else if (victim.hasTrait("XianTu8")) // 地仙期
            {
                attacker.ChangeZhongPin(10000); // 10000中品灵石
            }
        }

        // 职业进度相关扩展方法
        public static float GetCareerProgress(this Actor actor)
        {
            actor.data.get(careerProgress_key, out float val, 0);
            return val;
        }

        public static void SetCareerProgress(this Actor actor, float val)
        {
            actor.data.set(careerProgress_key, val);
        }

        public static void ChangeCareerProgress(this Actor actor, float delta)
        {
            actor.data.get(careerProgress_key, out float val, 0);
            val += delta;
            // 上限为9999999
            actor.data.set(careerProgress_key, Mathf.Min(9999999f, Mathf.Max(0, val)));
        }

        public static int GetCareerLevel(this Actor actor)
        {
            actor.data.get(careerLevel_key, out int val, 0);
            return val;
        }

        public static void SetCareerLevel(this Actor actor, int val)
        {
            actor.data.set(careerLevel_key, val);
        }

        // 根据悟性更新职业进度
        public static void UpdateCareerProgressByWuXing(this Actor actor)
        {
            // 只有选择了职业的角色才会增加职业进度
            if (actor.hasTrait("XiuxianLiuyi1") || actor.hasTrait("XiuxianLiuyi2") || actor.hasTrait("XiuxianLiuyi3") ||
                actor.hasTrait("XiuxianLiuyi4") || actor.hasTrait("XiuxianLiuyi5") || actor.hasTrait("XiuxianLiuyi6") ||
                actor.hasTrait("XiuxianLiuyi7") || actor.hasTrait("XiuxianLiuyi8") || actor.hasTrait("XiuxianLiuyi9"))
            {
                float wuXing = actor.GetWuXing();
                // 基础进度增加值为悟性值的0.1，每年增加一次
                float progressToAdd = wuXing * 0.9f;
                actor.ChangeCareerProgress(progressToAdd);
                
                // 检查是否需要晋升职业等级
                CheckCareerPromotion(actor);
            }
        }

        // 检查职业晋升
        public static void CheckCareerPromotion(this Actor actor)
        {
            int currentLevel = actor.GetCareerLevel();
            float currentProgress = actor.GetCareerProgress();
            
            // 职业等级晋升条件
            int targetLevel = currentLevel;
            float targetProgress = 0;
            
            // 根据当前进度确定目标等级和目标进度
            if (currentProgress >= 1000000 && currentLevel < 7) // 仙阶
            {
                targetLevel = 7;
                targetProgress = 9999999;
            }
            else if (currentProgress >= 600000 && currentLevel < 6) // 帝阶
            {
                targetLevel = 6;
                targetProgress = 1000000;
            }
            else if (currentProgress >= 300000 && currentLevel < 5) // 圣阶
            {
                targetLevel = 5;
                targetProgress = 600000;
            }
            else if (currentProgress >= 100000 && currentLevel < 4) // 天阶
            {
                targetLevel = 4;
                targetProgress = 300000;
            }
            else if (currentProgress >= 50000 && currentLevel < 3) // 地阶
            {
                targetLevel = 3;
                targetProgress = 100000;
            }
            else if (currentProgress >= 10000 && currentLevel < 2) // 玄阶
            {
                targetLevel = 2;
                targetProgress = 50000;
            }
            else if (currentProgress >= 1000 && currentLevel < 1) // 灵阶
            {
                targetLevel = 1;
                targetProgress = 10000;
            }
            else if (currentProgress >= 100 && currentLevel < 0) // 凡阶
            {
                targetLevel = 0;
                targetProgress = 1000;
            }
            
            // 如果等级提升了，更新等级和进度
            if (targetLevel > currentLevel)
            {
                actor.SetCareerLevel(targetLevel);
                // 保留超出部分的进度
                float extraProgress = currentProgress - (targetLevel == 0 ? 100 : 
                                               targetLevel == 1 ? 1000 : 
                                               targetLevel == 2 ? 10000 : 
                                               targetLevel == 3 ? 50000 : 
                                               targetLevel == 4 ? 100000 : 
                                               targetLevel == 5 ? 300000 : 600000);
                actor.SetCareerProgress(extraProgress);
            }
        }

        // 获取职业头衔
        public static string GetCareerTitle(this Actor actor)
        {
            int careerLevel = actor.GetCareerLevel();
            string baseTitle = "";
            
            // 确定基础职业名称
            if (actor.hasTrait("XiuxianLiuyi1"))
                baseTitle = "炼丹";
            else if (actor.hasTrait("XiuxianLiuyi2"))
                baseTitle = "炼器";
            else if (actor.hasTrait("XiuxianLiuyi3"))
                baseTitle = "驭兽";
            else if (actor.hasTrait("XiuxianLiuyi4"))
                baseTitle = "灵植";
            else if (actor.hasTrait("XiuxianLiuyi5"))
                baseTitle = "阵法";
            else if (actor.hasTrait("XiuxianLiuyi6"))
                baseTitle = "制符";
            else if (actor.hasTrait("XiuxianLiuyi7"))
            {
                // 魔修特殊命名规则
                if (careerLevel < 0)
                    return "一阶-入魔";
                else if (careerLevel == 0)
                    return "二阶-魔炁";
                else if (careerLevel == 1)
                    return "三阶-魔元";
                else if (careerLevel == 2)
                    return "四阶-魔丹";
                else if (careerLevel == 3)
                    return "五阶-魔胎";
                else if (careerLevel == 4)
                    return "六阶-天魔";
                else if (careerLevel == 5)
                    return "七阶-魔相";
                else if (careerLevel == 6)
                    return "八阶-魔罗";
                else if (careerLevel == 7)
                    return "九阶-魔神";
            }
            else if (actor.hasTrait("XiuxianLiuyi8"))
            {
                // 剑修特殊命名规则
                if (careerLevel < 0)
                    return "一阶-练剑";
                else if (careerLevel == 0)
                    return "二阶-剑气";
                else if (careerLevel == 1)
                    return "三阶-剑罡";
                else if (careerLevel == 2)
                    return "四阶-剑势";
                else if (careerLevel == 3)
                    return "五阶-剑意";
                else if (careerLevel == 4)
                    return "六阶-剑心";
                else if (careerLevel == 5)
                    return "七阶-剑相";
                else if (careerLevel == 6)
                    return "八阶-剑成";
                else if (careerLevel == 7)
                    return "九阶-剑神";
            }
            else if (actor.hasTrait("XiuxianLiuyi9"))
            {
                // 儒修特殊命名规则
                if (careerLevel < 0)
                    return "一阶-修身";
                else if (careerLevel == 0)
                    return "二阶-养性";
                else if (careerLevel == 1)
                    return "三阶-浩然";
                else if (careerLevel == 2)
                    return "四阶-德行";
                else if (careerLevel == 3)
                    return "五阶-文胆";
                else if (careerLevel == 4)
                    return "六阶-才心";
                else if (careerLevel == 5)
                    return "七阶-君子";
                else if (careerLevel == 6)
                    return "八阶-立命";
                else if (careerLevel == 7)
                    return "九阶-圣贤";
            }
            
            // 确定职业等级对应的后缀
            string levelSuffix = "";
            if (careerLevel < 0) // 学徒阶段
                return baseTitle + "学徒";
            else if (careerLevel == 0)
                levelSuffix = "凡阶";
            else if (careerLevel == 1)
                levelSuffix = "灵阶";
            else if (careerLevel == 2)
                levelSuffix = "玄阶";
            else if (careerLevel == 3)
                levelSuffix = "地阶";
            else if (careerLevel == 4)
                levelSuffix = "天阶";
            else if (careerLevel == 5)
                levelSuffix = "圣阶";
            else if (careerLevel == 6)
                levelSuffix = "帝阶";
            else if (careerLevel == 7)
                levelSuffix = "仙阶";
            
            return levelSuffix + baseTitle + "师";
        }

        // 获取职业进度显示文本
        public static string GetCareerProgressText(this Actor actor)
        {
            float currentProgress = actor.GetCareerProgress();
            int careerLevel = actor.GetCareerLevel();
            float maxProgress = 100; // 默认上限为学徒晋升凡阶的100
            
            // 根据职业等级确定当前阶段的最大进度
            if (careerLevel == 0) // 凡阶
                maxProgress = 1000;
            else if (careerLevel == 1) // 灵阶
                maxProgress = 10000;
            else if (careerLevel == 2) // 玄阶
                maxProgress = 50000;
            else if (careerLevel == 3) // 地阶
                maxProgress = 100000;
            else if (careerLevel == 4) // 天阶
                maxProgress = 300000;
            else if (careerLevel == 5) // 圣阶
                maxProgress = 600000;
            else if (careerLevel == 6) // 帝阶
                maxProgress = 1000000;
            else if (careerLevel == 7) // 仙阶
                maxProgress = 9999999;
            
            // 如果是仙阶且进度达到上限，直接显示9999999/9999999
            if (careerLevel == 7 && currentProgress >= 9999999)
                return "9999999/9999999";
            
            return Mathf.FloorToInt(currentProgress) + "/" + maxProgress;
        }
        
        // 根据职业等级每年增加灵石
        public static void AddCareerResourceByLevel(this Actor actor)
        {
            // 只有选择了职业的角色才会获得职业资源
            if (actor.hasTrait("XiuxianLiuyi1") || actor.hasTrait("XiuxianLiuyi2") || actor.hasTrait("XiuxianLiuyi3") ||
                actor.hasTrait("XiuxianLiuyi4") || actor.hasTrait("XiuxianLiuyi5") || actor.hasTrait("XiuxianLiuyi6"))
            {
                int careerLevel = actor.GetCareerLevel();
                
                // 根据职业等级增加不同数量的灵石
                switch (careerLevel)
                {
                    case 0: // 凡阶职业
                        actor.ChangeLingShi(1f); // 每年增加1下品灵石
                        break;
                    case 1: // 灵阶职业
                        actor.ChangeLingShi(10f); // 每年增加10下品灵石
                        break;
                    case 2: // 玄阶职业
                        actor.ChangeLingShi(100f); // 每年增加100下品灵石
                        break;
                    case 3: // 地阶职业
                        actor.ChangeLingShi(1000f); // 每年增加1000下品灵石
                        break;
                    case 4: // 天阶职业
                        actor.ChangeZhongPin(1f); // 每年增加1中品灵石
                        break;
                    case 5: // 圣阶职业
                        actor.ChangeZhongPin(10f); // 每年增加10中品灵石
                        break;
                    case 6: // 帝阶职业
                        actor.ChangeZhongPin(100f); // 每年增加100中品灵石
                        break;
                    case 7: // 仙阶职业
                        actor.ChangeZhongPin(1000f); // 每年增加1000中品灵石
                        break;
                }
            }
        }

        // 根据悟性每年增加推演点
        public static void UpdateTuiYanDianByWuXing(this Actor actor)
        {
            float wuXing = actor.GetWuXing();
            // 基础推演点增加值为0.1倍悟性值，每年增加一次
            float tuiYanDianToAdd = wuXing * 0.1f;
            actor.ChangeTuiYanDian(tuiYanDianToAdd);
            
            // 检查是否需要晋升神通层次
            CheckShenTongPromotion(actor);
        }

        // 检查神通晋升
        public static void CheckShenTongPromotion(this Actor actor)
        {
            int currentShenTongLevel = actor.GetShenTongLevel();
            float currentTuiYanDian = actor.GetTuiYanDian();
            float currentGongFaDian = actor.GetGongFaDian();
            string gongFaLevelName = actor.GetGongFaLevelName();
            
            // 检查是否可以晋升到下一个神通层次
            if (currentShenTongLevel < 1 && currentTuiYanDian >= 1000 && gongFaLevelName != "凡俗阶")
            {
                // 推演点达到1000，并且功法层次达到"炼炁阶"以上可以晋升成"法术"层次
                actor.SetShenTongLevel(1);
                // 晋升时生成并存储新的神通名称
                actor.UpdateShenTongName();
            }
            else if (currentShenTongLevel < 2 && currentTuiYanDian >= 10000 && gongFaLevelName != "凡俗阶" && gongFaLevelName != "炼炁阶" && gongFaLevelName != "道基阶")
            {
                // 推演点达到10000，并且功法层次达到"紫府阶"以上可以晋升成"小神通"层次
                actor.SetShenTongLevel(2);
                // 晋升时生成并存储新的神通名称
                actor.UpdateShenTongName();
            }
            else if (currentShenTongLevel < 3 && currentTuiYanDian >= 100000 && (gongFaLevelName == "元神阶" || gongFaLevelName == "法相阶" || gongFaLevelName == "羽化阶" || gongFaLevelName == "大道阶"))
            {
                // 推演点达到100000，并且功法层次达到"元神阶"以上可以晋升成"大神通"层次
                actor.SetShenTongLevel(3);
                // 晋升时生成并存储新的神通名称
                actor.UpdateShenTongName();
            }
            else if (currentShenTongLevel < 4 && currentTuiYanDian >= 1000000 && (gongFaLevelName == "法相阶" || gongFaLevelName == "羽化阶" || gongFaLevelName == "大道阶"))
            {
                // 推演点达到1000000，并且功法层次达到"法相阶"以上可以晋升成"天地大神通"层次
                actor.SetShenTongLevel(4);
                // 晋升时生成并存储新的神通名称
                actor.UpdateShenTongName();
            }
            else if (currentShenTongLevel < 5 && currentTuiYanDian >= 10000000 && gongFaLevelName == "大道阶")
            {
                // 推演点达到10000000，并且功法层次达到"大道阶"可以晋升成"无上妙法神通"层次
                actor.SetShenTongLevel(5);
                // 晋升时生成并存储新的神通名称
                actor.UpdateShenTongName();
            }
        }

        // 更新神通名称
        public static void UpdateShenTongName(this Actor actor)
        {
            int shenTongLevel = actor.GetShenTongLevel();
            if (shenTongLevel > 0)
            {
                // 生成新的神通名称并存储
                string newShenTongName = ShenTongSystem.GetRandomShenTongName(shenTongLevel);
                actor.data.set(current_shentong_name_key, newShenTongName);
            }
        }

        // 获取角色的神通名称
        public static string GetShenTongName(this Actor actor)
        {
            int shenTongLevel = actor.GetShenTongLevel();
            
            // 如果神通层次为0，则返回"未觉醒"
            if (shenTongLevel == 0)
            {
                return "无";
            }
            
            // 尝试从存储中获取神通名称
            string storedName = null;
            actor.data.get(current_shentong_name_key, out storedName, null);
            
            // 如果没有存储的名称，则生成并存储一个新名称
            if (string.IsNullOrEmpty(storedName))
            {
                storedName = ShenTongSystem.GetRandomShenTongName(shenTongLevel);
                actor.data.set(current_shentong_name_key, storedName);
            }
            
            // 返回存储的神通名称
            return storedName;
        }

        // 一阶-锻体强者数量相关扩展方法
        public static float GetWorldStrongFirst(this Actor actor)
        {
            actor.data.get(world_strong_first_key, out float val, 0);
            return val;
        }

        public static void SetWorldStrongFirst(this Actor actor, float val)
        {
            actor.data.set(world_strong_first_key, Mathf.Max(0, val));
        }

        public static void ChangeWorldStrongFirst(this Actor actor, float delta)
        {
            actor.data.get(world_strong_first_key, out float val, 0);
            val += delta;
            actor.data.set(world_strong_first_key, Mathf.Max(0, val));
        }

        // 二阶-炼炁强者数量相关扩展方法
        public static float GetWorldStrongSecond(this Actor actor)
        {
            actor.data.get(world_strong_second_key, out float val, 0);
            return val;
        }

        public static void SetWorldStrongSecond(this Actor actor, float val)
        {
            actor.data.set(world_strong_second_key, Mathf.Max(0, val));
        }

        public static void ChangeWorldStrongSecond(this Actor actor, float delta)
        {
            actor.data.get(world_strong_second_key, out float val, 0);
            val += delta;
            actor.data.set(world_strong_second_key, Mathf.Max(0, val));
        }

        // 三阶-紫府强者数量相关扩展方法
        public static float GetWorldStrongThird(this Actor actor)
        {
            actor.data.get(world_strong_third_key, out float val, 0);
            return val;
        }

        public static void SetWorldStrongThird(this Actor actor, float val)
        {
            actor.data.set(world_strong_third_key, Mathf.Max(0, val));
        }

        public static void ChangeWorldStrongThird(this Actor actor, float delta)
        {
            actor.data.get(world_strong_third_key, out float val, 0);
            val += delta;
            actor.data.set(world_strong_third_key, Mathf.Max(0, val));
        }

        // 四阶-元神强者数量相关扩展方法
        public static float GetWorldStrongFourth(this Actor actor)
        {
            actor.data.get(world_strong_fourth_key, out float val, 0);
            return val;
        }

        public static void SetWorldStrongFourth(this Actor actor, float val)
        {
            actor.data.set(world_strong_fourth_key, Mathf.Max(0, val));
        }

        public static void ChangeWorldStrongFourth(this Actor actor, float delta)
        {
            actor.data.get(world_strong_fourth_key, out float val, 0);
            val += delta;
            actor.data.set(world_strong_fourth_key, Mathf.Max(0, val));
        }

        // 五阶-法相强者数量相关扩展方法
        public static float GetWorldStrongFifth(this Actor actor)
        {
            actor.data.get(world_strong_fifth_key, out float val, 0);
            return val;
        }

        public static void SetWorldStrongFifth(this Actor actor, float val)
        {
            actor.data.set(world_strong_fifth_key, Mathf.Max(0, val));
        }

        public static void ChangeWorldStrongFifth(this Actor actor, float delta)
        {
            actor.data.get(world_strong_fifth_key, out float val, 0);
            val += delta;
            actor.data.set(world_strong_fifth_key, Mathf.Max(0, val));
        }

        // 六阶-飞升强者数量相关扩展方法
        public static float GetWorldStrongSixth(this Actor actor)
        {
            actor.data.get(world_strong_sixth_key, out float val, 0);
            return val;
        }

        public static void SetWorldStrongSixth(this Actor actor, float val)
        {
            actor.data.set(world_strong_sixth_key, Mathf.Max(0, val));
        }

        public static void ChangeWorldStrongSixth(this Actor actor, float delta)
        {
            actor.data.get(world_strong_sixth_key, out float val, 0);
            val += delta;
            actor.data.set(world_strong_sixth_key, Mathf.Max(0, val));
        }

        // 获取角色的神通层次名称
        public static string GetShenTongLevelName(this Actor actor)
        {
            int shenTongLevel = actor.GetShenTongLevel();
            return ShenTongSystem.GetShenTongLevelName(shenTongLevel);
        }
        
        // 七阶-仙人强者数量相关扩展方法
        private static string world_strong_seventh_key = "worldStrongSeventh";
        public static float GetWorldStrongSeventh(this Actor actor)
        {
            actor.data.get(world_strong_seventh_key, out float val, 0);
            return val;
        }

        public static void SetWorldStrongSeventh(this Actor actor, float val)
        {
            actor.data.set(world_strong_seventh_key, Mathf.Max(0, val));
        }

        public static void ChangeWorldStrongSeventh(this Actor actor, float delta)
        {
            actor.data.get(world_strong_seventh_key, out float val, 0);
            val += delta;
            actor.data.set(world_strong_seventh_key, Mathf.Max(0, val));
        }
        
        // 八阶-准圣强者数量相关扩展方法
        private static string world_strong_eighth_key = "worldStrongEighth";
        public static float GetWorldStrongEighth(this Actor actor)
        {
            actor.data.get(world_strong_eighth_key, out float val, 0);
            return val;
        }

        public static void SetWorldStrongEighth(this Actor actor, float val)
        {
            actor.data.set(world_strong_eighth_key, Mathf.Max(0, val));
        }

        public static void ChangeWorldStrongEighth(this Actor actor, float delta)
        {
            actor.data.get(world_strong_eighth_key, out float val, 0);
            val += delta;
            actor.data.set(world_strong_eighth_key, Mathf.Max(0, val));
        }
        
        // 九阶-圣人强者数量相关扩展方法
        private static string world_strong_ninth_key = "worldStrongNinth";
        public static float GetWorldStrongNinth(this Actor actor)
        {
            actor.data.get(world_strong_ninth_key, out float val, 0);
            return val;
        }

        public static void SetWorldStrongNinth(this Actor actor, float val)
        {
            actor.data.set(world_strong_ninth_key, Mathf.Max(0, val));
        }

        public static void ChangeWorldStrongNinth(this Actor actor, float delta)
        {
            actor.data.get(world_strong_ninth_key, out float val, 0);
            val += delta;
            actor.data.set(world_strong_ninth_key, Mathf.Max(0, val));
        }
    }
}