using HarmonyLib;
using PeerlessThedayofGodswrath.code;
using NeoModLoader.api;

namespace PeerlessThedayofGodswrath
{
    internal class PeerlessThedayofGodswrathClass : BasicMod<PeerlessThedayofGodswrathClass>
    {
        public static ModDeclare modDeclare;
        public static ModConfig config;

        public static string id = "shiyue.worldbox.mod.PeerlessThedayofGodswrath";
        protected override void OnModLoad()
        {
            try
            {
                stats.Init();
                traitGroup.Init();
                traits.Init();
                SwordImmortalFlyingSword.Init();
                new Harmony(id).PatchAll(typeof(patch));
                new Harmony(id).PatchAll(typeof(Upgradethesystem));
                modDeclare = GetDeclaration();
                config = GetConfig();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError($"Error during mod loading: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}