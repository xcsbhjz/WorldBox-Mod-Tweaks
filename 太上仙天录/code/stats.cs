using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using strings;
using XianTu.code;

namespace XianTu.code
{
    internal class stats
    {
        public static BaseStatAsset Resist;

        public static void Init()
        {
            BaseStatAsset XianTu = new BaseStatAsset();
            XianTu.id = "XianTu";
            XianTu.normalize = true;
            XianTu.normalize_min = -999999;
            XianTu.normalize_max = 99999999;
            //XianTu.multiplier = true;
            XianTu.used_only_for_civs = false;
            AssetManager.base_stats_library.add(XianTu);

            // 定义 Resist 属性
            Resist = new BaseStatAsset();
            Resist.id = "Resist";
            Resist.normalize = true;
            Resist.normalize_min = 0;
            Resist.normalize_max = 999999;
            Resist.used_only_for_civs = false;
            AssetManager.base_stats_library.add(Resist);

            BaseStatAsset Dodge = new BaseStatAsset();
            Dodge.id = "Dodge"; // 闪避率
            Dodge.normalize = true;
            Dodge.normalize_min = 0;
            Dodge.normalize_max = 99999;
            Dodge.used_only_for_civs = false;
            AssetManager.base_stats_library.add(Dodge);

            BaseStatAsset Accuracy = new BaseStatAsset();
            Accuracy.id = "Accuracy"; // 命中率
            Accuracy.normalize = true;
            Accuracy.normalize_min = 0;
            Accuracy.normalize_max = 99999;
            Accuracy.used_only_for_civs = false;
            AssetManager.base_stats_library.add(Accuracy);

            // 添加气血属性
            BaseStatAsset QiXue = new BaseStatAsset();
            QiXue.id = "QiXue"; // 气血值
            QiXue.normalize = true;
            QiXue.normalize_min = -999999;
            QiXue.normalize_max = 999999;
            QiXue.used_only_for_civs = false;
            AssetManager.base_stats_library.add(QiXue);

            // 添加灵石属性
            BaseStatAsset LingShi = new BaseStatAsset();
            LingShi.id = "LingShi"; // 灵石值
            LingShi.normalize = true;
            LingShi.normalize_min = -999999;
            LingShi.normalize_max = 999999;
            LingShi.used_only_for_civs = false;
            AssetManager.base_stats_library.add(LingShi);
            
            // 添加中品灵石属性
            BaseStatAsset ZhongPin = new BaseStatAsset();
            ZhongPin.id = "ZhongPin"; // 中品灵石值
            ZhongPin.normalize = true;
            ZhongPin.normalize_min = -999999;
            ZhongPin.normalize_max = 999999;
            ZhongPin.used_only_for_civs = false;
            AssetManager.base_stats_library.add(ZhongPin);
            
            // 添加上品灵石属性
            BaseStatAsset ShangPin = new BaseStatAsset();
            ShangPin.id = "ShangPin"; // 上品灵石值
            ShangPin.normalize = true;
            ShangPin.normalize_min = -999999;
            ShangPin.normalize_max = 999999;
            ShangPin.used_only_for_civs = false;
            AssetManager.base_stats_library.add(ShangPin);

            // 初始化自定义法器系统
            CustomItems.Init();
        }
    }
}