using System.Linq;
using HarmonyLib;
using NeoModLoader.api.attributes;
using System;
using System.Collections.Generic;
using ai;
using NeoModLoader.General;
using UnityEngine;
using UnityEngine.UI;
using ai.behaviours;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using AttributeExpansion.code.utils;
using System.Text;
using System.Reflection;
using ReflectionUtility;
using System.IO;
using AttributeExpansion.code;

namespace PeerlessThedayofGodswrath.code
{
    internal class Upgradethesystem
    {
        public static bool window_creature_info_initialized;
        [HarmonyPostfix]
        [HarmonyPatch(typeof(UnitWindow), nameof(UnitWindow.OnEnable))]
        private static void WindowCreatureInfo_OnEnable_postfix(UnitWindow __instance)
        {
            if (__instance.actor == null || !__instance.actor.isAlive()) return;
            if (!window_creature_info_initialized)
            {
                window_creature_info_initialized = true;
                UnitWindowStatsIcon.Initialize(__instance);
            }
        }
        [HarmonyPrefix, HarmonyPatch(typeof(UnitStatsElement), nameof(UnitStatsElement.showContent))]
        private static void UnitStatsElement_showContent_prefix(UnitStatsElement __instance)
        {
            var actor = __instance.actor;
            if (actor == null || !actor.isAlive()) return;
            __instance.setIconValue("careerexperience", actor.Getcareerexperience());
        }
    }
}