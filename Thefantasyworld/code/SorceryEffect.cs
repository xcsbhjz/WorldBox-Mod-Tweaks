using System;
using System.Threading;
using NCMS;
using UnityEngine;
using ReflectionUtility;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace PeerlessThedayofGodswrath.code
{
    internal class SorceryEffect
    {
        public static void Init()
        {
            // 刺客 🎵（旋律大师）
            StatusAsset Assassinstate = new StatusAsset();
            Assassinstate.id = "Assassinstate";
            Assassinstate.path_icon = "Ring/Assassinstate";
            Assassinstate.locale_id = "status_title_Assassinstate";
            Assassinstate.locale_description = "status_desc_Assassinstate";
            AssetManager.status.add(pAsset:Assassinstate);

            // 吟游诗人 🎵（旋律大师）
            StatusAsset minstrelstate1 = new StatusAsset();
            minstrelstate1.id = "minstrelstate1";
            minstrelstate1.path_icon = "Ring/minstrelstate1";
            minstrelstate1.locale_id = "status_title_minstrelstate1";
            minstrelstate1.locale_description = "status_desc_minstrelstate1";
            minstrelstate1.base_stats["multiplier_damage"] = 0.1f;
            minstrelstate1.base_stats["multiplier_health"] = 0.1f;
            AssetManager.status.add(pAsset:minstrelstate1);

            StatusAsset minstrelstate2 = new StatusAsset();
            minstrelstate2.id = "minstrelstate2";
            minstrelstate2.path_icon = "Ring/minstrelstate2";
            minstrelstate2.locale_id = "status_title_minstrelstate2";
            minstrelstate2.locale_description = "status_desc_minstrelstate2";
            minstrelstate2.base_stats["multiplier_damage"] = 0.2f;
            minstrelstate2.base_stats["multiplier_health"] = 0.2f;
            AssetManager.status.add(pAsset:minstrelstate2);

            StatusAsset minstrelstate3 = new StatusAsset();
            minstrelstate3.id = "minstrelstate3";
            minstrelstate3.path_icon = "Ring/minstrelstate3";
            minstrelstate3.locale_id = "status_title_minstrelstate3";
            minstrelstate3.locale_description = "status_desc_minstrelstate3";
            minstrelstate3.base_stats["multiplier_damage"] = 0.4f;
            minstrelstate3.base_stats["multiplier_health"] = 0.4f;
            AssetManager.status.add(pAsset:minstrelstate3);

            StatusAsset minstrelstate4 = new StatusAsset();
            minstrelstate4.id = "minstrelstate4";
            minstrelstate4.path_icon = "Ring/minstrelstate4";
            minstrelstate4.locale_id = "status_title_minstrelstate4";
            minstrelstate4.locale_description = "status_desc_minstrelstate4";
            minstrelstate4.base_stats["hitthetarget"] = 20f;
            minstrelstate4.base_stats["DodgeEvade"] = 20f;
            AssetManager.status.add(pAsset:minstrelstate4);

            StatusAsset minstrelstate5 = new StatusAsset();
            minstrelstate5.id = "minstrelstate5";
            minstrelstate5.path_icon = "Ring/minstrelstate5";
            minstrelstate5.locale_id = "status_title_minstrelstate5";
            minstrelstate5.locale_description = "status_desc_minstrelstate5";
            minstrelstate5.base_stats["multiplier_damage"] = 0.8f;
            minstrelstate5.base_stats["multiplier_health"] = 0.8f;
            AssetManager.status.add(pAsset:minstrelstate5);

            // 咒术师 ☠️（诅咒大师）
            StatusAsset warlockstate1 = new StatusAsset();
            warlockstate1.id = "warlockstate1";
            warlockstate1.path_icon = "Ring/warlockstate1";
            warlockstate1.locale_id = "status_title_warlockstate1";
            warlockstate1.locale_description = "status_desc_warlockstate1";
            warlockstate1.allow_timer_reset = false;// 不允许重置计时器
            warlockstate1.action_interval = 0.5f;// 动作触发的时间间隔秒
            warlockstate1.action = new WorldAction(warlockSkill.attack_warlockstate1);
            AssetManager.status.add(pAsset:warlockstate1);

            StatusAsset warlockstate2 = new StatusAsset();
            warlockstate2.id = "warlockstate2";
            warlockstate2.path_icon = "Ring/warlockstate2";
            warlockstate2.locale_id = "status_title_warlockstate2";
            warlockstate2.locale_description = "status_desc_warlockstate2";
            warlockstate2.base_stats["multiplier_damage"] = -0.1f;
            warlockstate2.base_stats["multiplier_health"] = -0.1f;
            AssetManager.status.add(pAsset:warlockstate2);

            StatusAsset warlockstate3 = new StatusAsset();
            warlockstate3.id = "warlockstate3";
            warlockstate3.path_icon = "Ring/warlockstate3";
            warlockstate3.locale_id = "status_title_warlockstate3";
            warlockstate3.locale_description = "status_desc_warlockstate3";
            warlockstate3.base_stats["multiplier_damage"] = -0.2f;
            warlockstate3.base_stats["multiplier_health"] = -0.2f;
            AssetManager.status.add(pAsset:warlockstate3);

            StatusAsset warlockstate4 = new StatusAsset();
            warlockstate4.id = "warlockstate4";
            warlockstate4.path_icon = "Ring/warlockstate4";
            warlockstate4.locale_id = "status_title_warlockstate4";
            warlockstate4.locale_description = "status_desc_warlockstate4";
            warlockstate4.base_stats["hitthetarget"] = -20f;
            warlockstate4.base_stats["DodgeEvade"] = -20f;
            AssetManager.status.add(pAsset:warlockstate4);

            StatusAsset warlockstate5 = new StatusAsset();
            warlockstate5.id = "warlockstate5";
            warlockstate5.path_icon = "Ring/warlockstate5";
            warlockstate5.locale_id = "status_title_warlockstate5";
            warlockstate5.locale_description = "status_desc_warlockstate5";
            AssetManager.status.add(pAsset:warlockstate5);

            // 炼金师 🔬（炼金大师）
            StatusAsset alchemiststate1 = new StatusAsset();
            alchemiststate1.id = "alchemiststate1";
            alchemiststate1.path_icon = "Ring/alchemiststate1";
            alchemiststate1.locale_id = "status_title_alchemiststate1";
            alchemiststate1.locale_description = "status_desc_alchemiststate1";
            alchemiststate1.base_stats["multiplier_damage"] = 0.2f;
            AssetManager.status.add(pAsset:alchemiststate1);

            StatusAsset alchemiststate2 = new StatusAsset();
            alchemiststate2.id = "alchemiststate2";
            alchemiststate2.path_icon = "Ring/alchemiststate2";
            alchemiststate2.locale_id = "status_title_alchemiststate2";
            alchemiststate2.locale_description = "status_desc_alchemiststate2";
            alchemiststate2.base_stats["multiplier_damage"] = 0.4f;
            AssetManager.status.add(pAsset:alchemiststate2);

            StatusAsset alchemiststate3 = new StatusAsset();
            alchemiststate3.id = "alchemiststate3";
            alchemiststate3.path_icon = "Ring/alchemiststate3";
            alchemiststate3.locale_id = "status_title_alchemiststate3";
            alchemiststate3.locale_description = "status_desc_alchemiststate3";
            alchemiststate3.base_stats["multiplier_damage"] = 0.8f;
            AssetManager.status.add(pAsset:alchemiststate3);

            StatusAsset alchemiststate4 = new StatusAsset();
            alchemiststate4.id = "alchemiststate4";
            alchemiststate4.path_icon = "Ring/alchemiststate4";
            alchemiststate4.locale_id = "status_title_alchemiststate4";
            alchemiststate4.locale_description = "status_desc_alchemiststate4";
            alchemiststate4.base_stats["multiplier_damage"] = 1.6f;
            AssetManager.status.add(pAsset:alchemiststate4);

            StatusAsset alchemiststate5 = new StatusAsset();
            alchemiststate5.id = "alchemiststate5";
            alchemiststate5.path_icon = "Ring/alchemiststate5";
            alchemiststate5.locale_id = "status_title_alchemiststate5";
            alchemiststate5.locale_description = "status_desc_alchemiststate5";
            alchemiststate5.base_stats["multiplier_damage"] = 3.2f;
            AssetManager.status.add(pAsset:alchemiststate5);

            // 野蛮人 🪓（狂战士）
            StatusAsset barbarianstate1 = new StatusAsset();
            barbarianstate1.id = "barbarianstate1";
            barbarianstate1.path_icon = "Ring/barbarianstate1";
            barbarianstate1.locale_id = "status_title_barbarianstate1";
            barbarianstate1.locale_description = "status_desc_barbarianstate1";
            barbarianstate1.base_stats["multiplier_damage"] = 0.5f;
            AssetManager.status.add(pAsset:barbarianstate1);

            StatusAsset barbarianstate2 = new StatusAsset();
            barbarianstate2.id = "barbarianstate2";
            barbarianstate2.path_icon = "Ring/barbarianstate2";
            barbarianstate2.locale_id = "status_title_barbarianstate2";
            barbarianstate2.locale_description = "status_desc_barbarianstate2";
            barbarianstate2.base_stats["multiplier_damage"] = 1f;
            AssetManager.status.add(pAsset:barbarianstate2);

            StatusAsset barbarianstate3 = new StatusAsset();
            barbarianstate3.id = "barbarianstate3";
            barbarianstate3.path_icon = "Ring/barbarianstate3";
            barbarianstate3.locale_id = "status_title_barbarianstate3";
            barbarianstate3.locale_description = "status_desc_barbarianstate3";
            barbarianstate3.base_stats["multiplier_damage"] = 2f;
            AssetManager.status.add(pAsset:barbarianstate3);

            StatusAsset barbarianstate4 = new StatusAsset();
            barbarianstate4.id = "barbarianstate4";
            barbarianstate4.path_icon = "Ring/barbarianstate4";
            barbarianstate4.locale_id = "status_title_barbarianstate4";
            barbarianstate4.locale_description = "status_desc_barbarianstate4";
            barbarianstate4.base_stats["multiplier_damage"] = 3f; // 修复了这里的变量名错误
            AssetManager.status.add(pAsset:barbarianstate4);

            StatusAsset barbarianstate5 = new StatusAsset();
            barbarianstate5.id = "barbarianstate5";
            barbarianstate5.path_icon = "Ring/barbarianstate5";
            barbarianstate5.locale_id = "status_title_barbarianstate5";
            barbarianstate5.locale_description = "status_desc_barbarianstate5";
            barbarianstate5.base_stats["multiplier_damage"] = 4f;
            AssetManager.status.add(pAsset:barbarianstate5);
        }
    }
}