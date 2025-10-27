using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoModLoader;
using NeoModLoader.api;
using NeoModLoader.api.attributes;

namespace InterestingTrait.code.Config
{
    internal static class XianTuConfig
    {
        // 由于删除了收藏人数限制功能，恢复ShouldAutoCollectForRealm方法以避免编译错误
        // 简化版本，直接返回autoCollectSetting参数
        public static bool ShouldAutoCollectForRealm(string realm, bool autoCollectSetting)
        {
            return autoCollectSetting;
        }
        
        // 自动收藏相关配置 - 修仙系统
        public static bool AutoCollectHuaShen = true;    // 化神境自动收藏
        public static bool AutoCollectHeTi = true;       // 合体境自动收藏
        public static bool AutoCollectDaCheng = true;    // 大乘境自动收藏
        public static bool AutoCollectBanXian = true;    // 半仙境自动收藏
        public static bool AutoCollectXianJing = true;   // 仙境自动收藏
        public static bool AutoCollectDaoZu = true;      // 道祖境自动收藏
        
        // 自动收藏相关配置 - 武道系统
        public static bool AutoCollectWuDaoShanHai = true;    // 融天境自动收藏
        public static bool AutoCollectWuDaoShengQu = true;    // 山海境自动收藏
        public static bool AutoCollectWuDaoZhengDi = true;    // 圣躯境自动收藏
        public static bool AutoCollectWuDaoZunZhi = true;    // 至尊境自动收藏
        public static bool AutoCollectWuDaoDiJing = true;     // 帝境自动收藏
        
        // 境界突破限制相关配置 - 修仙系统
        public static bool LimitBanXian = false;         // 半仙境限制
        public static bool LimitDaCheng = false;         // 大乘境限制
        public static bool LimitHeTi = false;            // 合体境限制
        public static bool LimitHuaShen = false;         // 化神境限制
        public static bool LimitYingYuan = false;        // 元婴境限制
        public static bool LimitJinDan = false;          // 金丹境限制
        public static bool LimitZhuJi = false;           // 筑基境限制
        public static bool LimitXianJing = false;        // 仙境限制
        public static bool LimitLianQi = false;          // 练气境限制
        public static bool LimitDaoZu = false;           // 道祖境限制
        
        // 境界突破限制相关配置 - 武道系统
        public static bool LimitWuDaoLianPi = false;    // 炼皮境限制
        public static bool LimitWuDaoDuanGu = false;    // 锻骨境限制
        public static bool LimitWuDaoHuanXue = false;    // 换血境限制
        public static bool LimitWuDaoLianZang = false;    // 炼脏境限制
        public static bool LimitWuDaoTianRen = false;    // 先天境限制
        public static bool LimitWuDaoShenYou = false;    // 抱丹境限制
        public static bool LimitWuDaoTaXu = false;       // 神游境限制
        public static bool LimitWuDaoMingWo = false;     // 明意境限制
        public static bool LimitWuDaoShanHai = false;    // 融天境限制
        public static bool LimitWuDaoShengQu = false;    // 山海境限制
        public static bool LimitWuDaoZhengDi = false;    // 圣躯境限制
        public static bool LimitWuDaoZunZhi = false;     // 至尊境限制
        public static bool LimitWuDaoDiJing = false;     // 帝境限制

        // 自动收藏相关回调 - 修仙系统
        public static void AutoCollectHuaShenCallBack(bool newValue)
        {
            AutoCollectHuaShen = newValue;
        }

        public static void AutoCollectHeTiCallBack(bool newValue)
        {
            AutoCollectHeTi = newValue;
        }

        public static void AutoCollectDaChengCallBack(bool newValue)
        {
            AutoCollectDaCheng = newValue;
        }

        public static void AutoCollectBanXianCallBack(bool newValue)
        {
            AutoCollectBanXian = newValue;
        }
        
        public static void AutoCollectXianJingCallBack(bool newValue)
        {
            AutoCollectXianJing = newValue;
        }
        
        public static void AutoCollectDaoZuCallBack(bool newValue)
        {
            AutoCollectDaoZu = newValue;
        }
        
        // 自动收藏相关回调 - 武道系统
        public static void AutoCollectWuDaoShanHaiCallBack(bool newValue)
        {
            AutoCollectWuDaoShanHai = newValue;
        }
        
        public static void AutoCollectWuDaoShengQuCallBack(bool newValue)
        {
            AutoCollectWuDaoShengQu = newValue;
        }
        
        public static void AutoCollectWuDaoZhengDiCallBack(bool newValue)
        {
            AutoCollectWuDaoZhengDi = newValue;
        }
        
        public static void AutoCollectWuDaoZunZhiCallBack(bool newValue)
        {
            AutoCollectWuDaoZunZhi = newValue;
        }
        
        public static void AutoCollectWuDaoDiJingCallBack(bool newValue)
        {
            AutoCollectWuDaoDiJing = newValue;
        }
        

        // 境界限制相关回调 - 修仙系统
        public static void LimitBanXianCallBack(bool newValue)
        {
            LimitBanXian = newValue;
        }
        
        public static void LimitXianJingCallBack(bool newValue)
        {
            LimitXianJing = newValue;
        }

        public static void LimitDaChengCallBack(bool newValue)
        {
            LimitDaCheng = newValue;
        }

        public static void LimitHeTiCallBack(bool newValue)
        {
            LimitHeTi = newValue;
        }

        public static void LimitHuaShenCallBack(bool newValue)
        {
            LimitHuaShen = newValue;
        }

        public static void LimitYingYuanCallBack(bool newValue)
        {
            LimitYingYuan = newValue;
        }

        public static void LimitJinDanCallBack(bool newValue)
        {
            LimitJinDan = newValue;
        }

        public static void LimitZhuJiCallBack(bool newValue)
        {
            LimitZhuJi = newValue;
        }

        public static void LimitLianQiCallBack(bool newValue)
        {
            LimitLianQi = newValue;
        }
        
        public static void LimitDaoZuCallBack(bool newValue)
        {
            LimitDaoZu = newValue;
        }
        
        // 境界限制相关回调 - 武道系统
        public static void LimitWuDaoLianPiCallBack(bool newValue)
        {
            LimitWuDaoLianPi = newValue;
        }
        
        public static void LimitWuDaoDuanGuCallBack(bool newValue)
        {
            LimitWuDaoDuanGu = newValue;
        }
        
        public static void LimitWuDaoHuanXueCallBack(bool newValue)
        {
            LimitWuDaoHuanXue = newValue;
        }
        
        public static void LimitWuDaoLianZangCallBack(bool newValue)
        {
            LimitWuDaoLianZang = newValue;
        }
      
        public static void LimitWuDaoTianRenCallBack(bool newValue)
        {
            LimitWuDaoTianRen = newValue;
        }
        
        public static void LimitWuDaoShenYouCallBack(bool newValue)
        {
            LimitWuDaoShenYou = newValue;
        }
        
        public static void LimitWuDaoTaXuCallBack(bool newValue)
        {
            LimitWuDaoTaXu = newValue;
        }
        
        public static void LimitWuDaoMingWoCallBack(bool newValue)
        {
            LimitWuDaoMingWo = newValue;
        }
        
        public static void LimitWuDaoShanHaiCallBack(bool newValue)
        {
            LimitWuDaoShanHai = newValue;
        }
        
        public static void LimitWuDaoShengQuCallBack(bool newValue)
        {
            LimitWuDaoShengQu = newValue;
        }
        
        public static void LimitWuDaoZhengDiCallBack(bool newValue)
        {
            LimitWuDaoZhengDi = newValue;
        }
        
        public static void LimitWuDaoZunZhiCallBack(bool newValue)
        {
            LimitWuDaoZunZhi = newValue;
        }
        
        public static void LimitWuDaoDiJingCallBack(bool newValue)
        {
            LimitWuDaoDiJing = newValue;
        }
    }
}