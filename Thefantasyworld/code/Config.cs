using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoModLoader;
using NeoModLoader.api;
using NeoModLoader.api.attributes;

namespace PeerlessThedayofGodswrath.code
{
    internal static class ThefantasyworldConfig
    {
        public static bool AutoCollectLegend = true;  // 开关
        public static bool AutoCollectDemigod = true;  // 开关
        public static bool AutoCollectgod = true;  // 开关

        public static void AutoCollectLegendCallBack(bool newValue)
        {
            AutoCollectLegend = newValue;
        }

        public static void AutoCollectDemigodCallBack(bool newValue)
        {
            AutoCollectDemigod = newValue;
        }
        public static void AutoCollectgodCallBack(bool newValue)
        {
            AutoCollectgod = newValue;
        }
    }
}