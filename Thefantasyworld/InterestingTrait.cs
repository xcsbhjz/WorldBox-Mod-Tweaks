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
            {
                stats.Init();
                traitGroup.Init();
                traits.Init();
                SorceryEffect.Init();
                ThefantasyworldWorldLog.Init();
                SwordImmortalFlyingSword.Init();
                new Harmony(id).PatchAll(typeof(patch));
                new Harmony(id).PatchAll(typeof(Battlecalculation));
                new Harmony(id).PatchAll(typeof(ThefantasyworldConfig));
                new Harmony(id).PatchAll(typeof(Upgradethesystem));
                modDeclare = GetDeclaration();
                config = GetConfig();
            }
        }
    }
}