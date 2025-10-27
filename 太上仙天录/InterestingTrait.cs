using HarmonyLib;
using XianTu.code;
using NeoModLoader.api;

namespace XianTu
{
    internal class XianTuClass : BasicMod<XianTuClass>
    {
        public static string id = "shiyue.worldbox.mod.XianTu";
        protected override void OnModLoad()
        {
            try
            {
                UnityEngine.Debug.Log("Starting stats initialization...");
                stats.Init();
                UnityEngine.Debug.Log("Stats initialization completed.");

                UnityEngine.Debug.Log("Starting traitGroup initialization...");
                traitGroup.Init();
                UnityEngine.Debug.Log("traitGroup initialization completed.");

                UnityEngine.Debug.Log("Starting traits initialization...");
                traits.Init();
                UnityEngine.Debug.Log("traits initialization completed.");

                UnityEngine.Debug.Log("Applying Harmony patches...");
                new Harmony(id).PatchAll(typeof(patch));
                UnityEngine.Debug.Log("Harmony patches applied successfully.");
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError($"Error during mod loading: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}