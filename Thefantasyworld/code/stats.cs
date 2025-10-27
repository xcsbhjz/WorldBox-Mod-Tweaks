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
            careerexperience.used_only_for_civs = false;
            AssetManager.base_stats_library.add(careerexperience);

            BaseStatAsset DodgeEvade = new BaseStatAsset();
            DodgeEvade.id = "DodgeEvade";
            DodgeEvade.normalize = true;
            DodgeEvade.normalize_min = -99999;
            DodgeEvade.normalize_max = 2100000000;
            DodgeEvade.used_only_for_civs = false;
            AssetManager.base_stats_library.add(DodgeEvade);

            BaseStatAsset hitthetarget = new BaseStatAsset();
            hitthetarget.id = "hitthetarget";
            hitthetarget.normalize = true;
            hitthetarget.normalize_min = 0;
            hitthetarget.normalize_max = 2100000000;
            hitthetarget.used_only_for_civs = false;
            AssetManager.base_stats_library.add(hitthetarget);

            BaseStatAsset MagicApplication = new BaseStatAsset();
            MagicApplication.id = "MagicApplication";
            MagicApplication.normalize = true;
            MagicApplication.normalize_min = 0;
            MagicApplication.normalize_max = 2100000000;
            MagicApplication.used_only_for_civs = false;
            AssetManager.base_stats_library.add(MagicApplication);

            BaseStatAsset MagicShield = new BaseStatAsset();
            MagicShield.id = "MagicShield";
            MagicShield.normalize = true;
            MagicShield.normalize_min = 0;
            MagicShield.normalize_max = 2100000000;
            MagicShield.used_only_for_civs = false;
            AssetManager.base_stats_library.add(MagicShield);

            BaseStatAsset Fixedwound = new BaseStatAsset();
            Fixedwound.id = "Fixedwound";
            Fixedwound.normalize = true;
            Fixedwound.normalize_min = 0;
            Fixedwound.normalize_max = 2100000000;
            Fixedwound.used_only_for_civs = false;
            AssetManager.base_stats_library.add(Fixedwound);

            BaseStatAsset Restorehealth = new BaseStatAsset();
            Restorehealth.id = "Restorehealth";
            Restorehealth.normalize = true;
            Restorehealth.normalize_min = 0;
            Restorehealth.normalize_max = 2100000000;
            Restorehealth.used_only_for_civs = false;
            AssetManager.base_stats_library.add(Restorehealth);

            BaseStatAsset MagicReply = new BaseStatAsset();
            MagicReply.id = "MagicReply";
            MagicReply.normalize = true;
            MagicReply.normalize_min = 0;
            MagicReply.normalize_max = 2100000000;
            MagicReply.used_only_for_civs = false;
            AssetManager.base_stats_library.add(MagicReply);
        }
    }
}