namespace PeerlessThedayofGodswrath.code
{
    internal class stats
    {
        public static void Init()
        {
            BaseStatAsset careerexperience = new BaseStatAsset();
            careerexperience.id = "careerexperience";
            careerexperience.normalize = true;
            careerexperience.normalize_min = -99999;
            careerexperience.normalize_max = 2100000000;
            // careerexperience.multiplier = true;
            careerexperience.used_only_for_civs = false;
            AssetManager.base_stats_library.add(careerexperience);

            BaseStatAsset DodgeEvade = new BaseStatAsset();
            DodgeEvade.id = "DodgeEvade";
            DodgeEvade.normalize = true;
            DodgeEvade.normalize_min = -99999;
            DodgeEvade.normalize_max = 2100000000;
            // DodgeEvade.multiplier = true;
            DodgeEvade.used_only_for_civs = false;
            AssetManager.base_stats_library.add(DodgeEvade);

            BaseStatAsset hitthetarget = new BaseStatAsset();
            hitthetarget.id = "hitthetarget";
            hitthetarget.normalize = true;
            hitthetarget.normalize_min = 0;
            hitthetarget.normalize_max = 2100000000;
            // hitthetarget.multiplier = true;
            hitthetarget.used_only_for_civs = false;
            AssetManager.base_stats_library.add(hitthetarget);

            BaseStatAsset MagicApplication = new BaseStatAsset();
            MagicApplication.id = "MagicApplication";
            MagicApplication.normalize = true;
            MagicApplication.normalize_min = 0;
            MagicApplication.normalize_max = 2100000000;
            // MagicApplication.multiplier = true;
            MagicApplication.used_only_for_civs = false;
            AssetManager.base_stats_library.add(MagicApplication);

            BaseStatAsset MagicShield = new BaseStatAsset();
            MagicShield.id = "MagicShield";
            MagicShield.normalize = true;
            MagicShield.normalize_min = 0;
            MagicShield.normalize_max = 2100000000;
            // MagicShield.multiplier = true;
            MagicShield.used_only_for_civs = false;
            AssetManager.base_stats_library.add(MagicShield);

            BaseStatAsset Magicapplication = new BaseStatAsset();
            Magicapplication.id = "Magicapplication";
            Magicapplication.normalize = true;
            Magicapplication.normalize_min = 0;
            Magicapplication.normalize_max = 2100000000;
            // Magicapplication.multiplier = true;
            Magicapplication.used_only_for_civs = false;
            AssetManager.base_stats_library.add(Magicapplication);

            BaseStatAsset Resistretreat = new BaseStatAsset();
            Resistretreat.id = "Resistretreat";
            Resistretreat.normalize = true;
            Resistretreat.normalize_min = 0;
            Resistretreat.normalize_max = 2100000000;
            Resistretreat.used_only_for_civs = false;
            AssetManager.base_stats_library.add(Resistretreat);

            BaseStatAsset Break = new BaseStatAsset();
            Break.id = "Break";
            Break.normalize = true;
            Break.normalize_min = 0;
            Break.normalize_max = 2100000000;
            Break.used_only_for_civs = false;
            AssetManager.base_stats_library.add(Break);          
        }
    }
}