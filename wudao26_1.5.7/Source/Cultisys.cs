using System;
using System.Collections.ObjectModel;
using NeoModLoader.General;
using strings;

namespace CustomModT001;

public static class Cultisys
{
    public const            int         MaxLevel = 26;
    private static readonly BaseStats[] _level_stats;
    private static readonly float[]     _level_exp_required;

    private static readonly string[] _traits_blacklist =
    {
        "eyepatch", "crippled", "cursed", "tumorInfection", "mushSpores", "plague", "skin_burns", "deathbound"
    };

    private static readonly string[] _statuses_blacklist =
    {
        "cough", "ash_fever"
    };

    static Cultisys()
    {
        _level_stats = new BaseStats[MaxLevel + 1];
        _level_exp_required = new float[MaxLevel];
        for (var i = 0; i <= MaxLevel; i++) _level_stats[i] = new BaseStats();
        
        // 设置每个等级升级所需的经验值
        float[] levelUpExp = new float[MaxLevel];
        levelUpExp[0] = 1000;    // 凡夫升级到开悟所需
        levelUpExp[1] = 3000;    // 开悟升级到入品所需
        levelUpExp[2] = 4500;    // 入品升级到入品-二阶所需
        levelUpExp[3] = 7200;    // 入品-二阶升级到入品-圆满所需
        levelUpExp[4] = 16000;   // 入品-圆满升级到后天所需
        levelUpExp[5] = 24000;   // 后天升级到后天-二阶所需
        levelUpExp[6] = 38000;   // 后天-二阶升级到后天-圆满所需
        levelUpExp[7] = 85000;   // 后天-圆满升级到先天所需
        levelUpExp[8] = 130000;  // 先天升级到先天-二阶所需
        levelUpExp[9] = 200000;  // 先天-二阶升级到先天-圆满所需
        levelUpExp[10] = 500000; // 先天-圆满升级到至臻所需
        levelUpExp[11] = 720000; // 至臻升级到至臻-二阶所需
        levelUpExp[12] = 1100000;// 至臻-二阶升级到至臻-圆满所需
        levelUpExp[13] = 2600000;// 至臻-圆满升级到超凡所需
        levelUpExp[14] = 4000000;// 超凡升级到超凡-二阶所需
        levelUpExp[15] = 6400000;// 超凡-二阶升级到超凡-圆满所需
        levelUpExp[16] = 15000000;// 超凡-圆满升级到入圣所需
        levelUpExp[17] = 24000000;// 入圣升级到入圣-二阶所需
        levelUpExp[18] = 35000000;// 入圣-二阶升级到入圣-圆满所需
        levelUpExp[19] = 80000000;// 入圣-圆满升级到不朽所需
        levelUpExp[20] = 120000000;// 不朽升级到不朽-二阶所需
        levelUpExp[21] = 200000000;// 不朽-二阶升级到不朽-圆满所需
        levelUpExp[22] = 400000000;// 不朽-圆满升级到破界所需
        levelUpExp[23] = 650000000;// 破界升级到破界-二阶所需
        levelUpExp[24] = 1000000000;// 破界-二阶升级到破界-圆满所需
        levelUpExp[25] = 2000000000;// 破界-圆满升级到镇神所需
        
        // 直接将升级所需经验值赋值给_level_exp_required数组
        for (int i = 0; i < MaxLevel; i++)
        {
            _level_exp_required[i] = levelUpExp[i]; // 升级到等级i+1所需的经验值
        }
        
        LevelExpRequired = new ReadOnlyCollection<float>(_level_exp_required);
        LevelStats = new ReadOnlyCollection<BaseStats>(_level_stats);

  // 等级0：凡夫    
BaseStats stats = _level_stats[0];
stats[S.mana] = 40;
stats[Stats.mana_regen.id] = 0.5f;


// 等级1：开悟
stats = _level_stats[1];
stats[S.mana] = 50;
stats[Stats.mana_regen.id] = 1;
stats[S.health] = 100;
stats[S.damage] = 10;


// 等级2：入品
stats = _level_stats[2];
stats[S.mana] = 60;
stats[Stats.mana_regen.id] = 1;
stats[S.health] = 260;
stats[S.damage] = 33;
stats[S.lifespan] = 25;


// 等级3：入品-二阶
stats = _level_stats[3];
stats[S.mana] = 100;
stats[Stats.mana_regen.id] = 2;
stats[S.health] = 360;
stats[S.damage] = 46;
stats[S.speed] = 20;
stats[S.lifespan] = 100;


// 等级4：入品-圆满
stats = _level_stats[4];
stats[S.mana] = 400;
stats[Stats.mana_regen.id] = 4;
stats[S.health] = 550;
stats[S.damage] = 68;
stats[S.speed] = 20;
stats[S.lifespan] = 200;


// 等级5：后天
stats = _level_stats[5];
stats[S.mana] = 1000;
stats[Stats.mana_regen.id] = 20;
stats[S.health] = 1100;
stats[S.damage] = 140;
stats[S.speed] = 20;
stats[S.lifespan] = 300;
stats[S.targets] = 5f;
stats[S.range] = 3f;
stats[S.area_of_effect] = 2f;


// 等级6：后天-二阶
stats = _level_stats[6];
stats[S.mana] = 3000;
stats[Stats.mana_regen.id] = 500;
stats[S.health] = 1500;
stats[S.damage] = 180;
stats[S.speed] = 20;
stats[S.lifespan] = 300;
stats[S.knockback_reduction] = 100;
stats[S.targets] = 5f;
stats[S.area_of_effect] = 2f;


// 等级7：后天-圆满
stats = _level_stats[7];
stats[S.mana] = 40000;
stats[Stats.mana_regen.id] = 500;
stats[S.health] = 2100;
stats[S.damage] = 270;
stats[S.speed] = 20;
stats[S.lifespan] = 300;
stats[S.knockback_reduction] = 500;
stats[S.targets] = 5f;
stats[S.area_of_effect] = 2f;


// 等级8：先天
stats = _level_stats[8];
stats[S.mana] = 40000;
stats[Stats.mana_regen.id] = 500;
stats[S.health] = 4700;
stats[S.damage] = 590;
stats[S.speed] = 20;
stats[S.lifespan] = 500;
stats[S.knockback_reduction] = 1000;
stats[S.scale] = 0.1f;
stats[S.targets] = 5f;
stats[S.range] = 3f;
stats[S.area_of_effect] = 5f;


// 等级9：先天-二阶
stats = _level_stats[9];
stats[S.mana] = 40000;
stats[Stats.mana_regen.id] = 500;
stats[S.health] = 6100;
stats[S.damage] = 760;
stats[S.speed] = 20;
stats[S.lifespan] = 500;
stats[S.knockback_reduction] = 1000;
stats[S.scale] = 0.1f;
stats[S.targets] = 5f;
stats[S.range] = 3f;
stats[S.area_of_effect] = 5f;


// 等级10：先天-圆满
stats = _level_stats[10];
stats[S.mana] = 40000;
stats[Stats.mana_regen.id] = 500;
stats[S.health] = 8600;
stats[S.damage] = 1100;
stats[S.speed] = 20;
stats[S.lifespan] = 500;
stats[S.knockback_reduction] = 1000;
stats[S.scale] = 0.1f;
stats[S.targets] = 5f;
stats[S.range] = 3f;
stats[S.area_of_effect] = 5f;


// 等级11：至臻
stats = _level_stats[11];
stats[Stats.addition_armor.id] = 15;
stats[Stats.control_get.id] = -0.3f;
stats[S.mana] = 40000;
stats[Stats.mana_regen.id] = 500;
stats[S.health] = 20000;
stats[S.damage] = 1750; // 降低30%
stats[S.speed] = 40;
stats[S.lifespan] = 700;
stats[S.knockback_reduction] = 1000;
stats[S.scale] = 0.15f;
stats[S.targets] = 10f;
stats[S.range] = 5f;
stats[S.area_of_effect] = 5f;


// 等级12：至臻-二阶
stats = _level_stats[12];
stats[Stats.addition_armor.id] = 15;
stats[Stats.control_get.id] = -0.3f;
stats[S.mana] = 40000;
stats[Stats.mana_regen.id] = 500;
stats[S.health] = 27000;
stats[S.damage] = 2310; // 降低30%
stats[S.speed] = 40;
stats[S.lifespan] = 700;
stats[S.knockback_reduction] = 10000;
stats[S.scale] = 0.15f;
stats[S.targets] = 10f;
stats[S.range] = 5f;
stats[S.area_of_effect] = 5f;


// 等级13：至臻-圆满
stats = _level_stats[13];
stats[Stats.addition_armor.id] = 15;
stats[Stats.control_get.id] = -0.3f;
stats[S.mana] = 40000;
stats[Stats.mana_regen.id] = 500;
stats[S.health] = 38000;
stats[S.damage] = 3360; // 降低30%
stats[S.speed] = 40;
stats[S.lifespan] = 800;
stats[S.knockback_reduction] = 10000;
stats[S.scale] = 0.15f;
stats[S.targets] = 10f;
stats[S.range] = 5f;
stats[S.area_of_effect] = 5f;


// 等级14：超凡
stats = _level_stats[14];
stats[Stats.addition_armor.id] = 30;
stats[Stats.control_get.id] = -0.6f;
stats[S.mana] = 40000;
stats[Stats.mana_regen.id] = 500;
stats[S.health] = 100000;
stats[S.damage] = 9100; // 降低30%
stats[S.speed] = 80;
stats[S.lifespan] = 1000;
stats[S.knockback_reduction] = 10000;
stats[S.scale] = 0.3f;
stats[S.targets] = 10f;
stats[S.range] = 5f;
stats[S.area_of_effect] = 10f;


// 等级15：超凡-二阶
stats = _level_stats[15];
stats[Stats.addition_armor.id] = 30;
stats[Stats.control_get.id] = -0.6f;
stats[S.mana] = 40000;
stats[Stats.mana_regen.id] = 500;
stats[S.health] = 130000;
stats[S.damage] = 11200; // 降低30%
stats[S.speed] = 80;
stats[S.lifespan] = 1500;
stats[S.knockback_reduction] = 10000;
stats[S.scale] = 0.3f;
stats[S.targets] = 10f;
stats[S.range] = 5f;
stats[S.area_of_effect] = 10f;


// 等级16：超凡-圆满
stats = _level_stats[16];
stats[Stats.addition_armor.id] = 30;
stats[Stats.control_get.id] = -0.6f;
stats[S.mana] = 40000;
stats[Stats.mana_regen.id] = 500;
stats[S.health] = 180000;
stats[S.damage] = 16100; // 降低30%
stats[S.speed] = 80;
stats[S.lifespan] = 2000;
stats[S.knockback_reduction] = 10000;
stats[S.scale] = 0.3f;
stats[S.targets] = 10f;
stats[S.range] = 5f;
stats[S.area_of_effect] = 20f;


// 等级17：入圣
stats = _level_stats[17];
stats[Stats.addition_armor.id] = 50;
stats[Stats.control_get.id] = -0.8f;
stats[S.mana] = 40000;
stats[Stats.mana_regen.id] = 500;
stats[S.health] = 460000;
stats[S.damage] = 39900; // 降低30%
stats[S.speed] = 100;
stats[S.lifespan] = 3000;
stats[S.knockback_reduction] = 10000;
stats[S.scale] = 0.5f;
stats[S.targets] = 20f;
stats[S.range] = 8f;
stats[S.area_of_effect] = 20f;


// 等级18：入圣-二阶
stats = _level_stats[18];
stats[Stats.addition_armor.id] = 50;
stats[Stats.control_get.id] = -0.8f;
stats[S.mana] = 40000;
stats[Stats.mana_regen.id] = 500;
stats[S.health] = 620000;
stats[S.damage] = 53900; // 降低30%
stats[S.speed] = 100;
stats[S.lifespan] = 4000;
stats[S.knockback_reduction] = 10000;
stats[S.scale] = 0.5f;
stats[S.targets] = 20f;
stats[S.range] = 8f;
stats[S.area_of_effect] = 20f;


// 等级19：入圣-圆满
stats = _level_stats[19];
stats[Stats.addition_armor.id] = 50;
stats[Stats.control_get.id] = -0.8f;
stats[S.mana] = 40000;
stats[Stats.mana_regen.id] = 500;
stats[S.health] = 890000;
stats[S.damage] = 77000; // 降低30%
stats[S.speed] = 100;
stats[S.lifespan] = 6000;
stats[S.knockback_reduction] = 10000;
stats[S.scale] = 0.5f;
stats[S.targets] = 20f;
stats[S.range] = 8f;
stats[S.area_of_effect] = 20f;


// 等级20：不朽
stats = _level_stats[20];
stats[Stats.addition_armor.id] = 60;
stats[Stats.control_get.id] = -0.9f;
stats[S.mana] = 40000;
stats[Stats.mana_regen.id] = 600;
stats[S.health] = 2300000;
stats[S.damage] = 203000; // 降低30%
stats[S.speed] = 120;
stats[S.lifespan] = 10000;
stats[S.knockback_reduction] = 15000;
stats[S.scale] = 0.6f;
stats[S.targets] = 40f;
stats[S.range] = 10f;
stats[S.area_of_effect] = 30f;

// 等级21：不朽-二阶
stats = _level_stats[21];
stats[Stats.addition_armor.id] = 60;
stats[Stats.control_get.id] = -1.0f;
stats[S.mana] = 50000;
stats[Stats.mana_regen.id] = 800;
stats[S.health] = 3300000;
stats[S.damage] = 287000; // 降低30%
stats[S.speed] = 150;
stats[S.lifespan] = 15000;
stats[S.knockback_reduction] = 20000;
stats[S.scale] = 0.7f;
stats[S.targets] = 20f;
stats[S.range] = 10f;
stats[S.area_of_effect] = 30f;

// 等级22：不朽-圆满
stats = _level_stats[22];
stats[Stats.addition_armor.id] = 60;
stats[Stats.control_get.id] = -1.0f;
stats[S.mana] = 50000;
stats[Stats.mana_regen.id] = 800;
stats[S.health] = 4900000;
stats[S.damage] = 427000; // 降低30%
stats[S.speed] = 150;
stats[S.lifespan] = 20000;
stats[S.knockback_reduction] = 20000;
stats[S.scale] = 0.7f;
stats[S.targets] = 20f;
stats[S.range] = 10f;
stats[S.area_of_effect] = 30f;

// 等级23：破界
stats = _level_stats[23];
stats[Stats.addition_armor.id] = 75;
stats[Stats.control_get.id] = -1.2f;
stats[S.mana] = 60000;
stats[Stats.mana_regen.id] = 1000;
stats[S.health] = 14000000;
stats[S.damage] = 1400000; // 降低30%
stats[S.speed] = 180;
stats[S.lifespan] = 30000;
stats[S.knockback_reduction] = 30000;
stats[S.scale] = 0.8f;
stats[S.targets] = 50f;
stats[S.range] = 15f;
stats[S.area_of_effect] = 50f;

// 等级24：破界-二阶
stats = _level_stats[24];
stats[Stats.addition_armor.id] = 75;
stats[Stats.control_get.id] = -1.5f;
stats[S.mana] = 100000;
stats[Stats.mana_regen.id] = 1800;
stats[S.health] = 20000000;
stats[S.damage] = 2100000; // 降低30%
stats[S.speed] = 250;
stats[S.lifespan] = 40000;
stats[S.knockback_reduction] = 50000;
stats[S.scale] = 1.0f;
stats[S.targets] = 50f;
stats[S.range] = 15f;
stats[S.area_of_effect] = 50f;


// 等级25：破界-圆满
stats = _level_stats[25];
stats[Stats.addition_armor.id] = 75;
stats[Stats.control_get.id] = -1.6f;
stats[S.mana] = 120000;
stats[Stats.mana_regen.id] = 2200;
stats[S.health] = 30000000;
stats[S.damage] = 3500000; // 降低30%
stats[S.speed] = 280;
stats[S.lifespan] = 50000;
stats[S.knockback_reduction] = 60000;
stats[S.scale] = 1.1f;
stats[S.targets] = 50f;
stats[S.range] = 15f;
stats[S.area_of_effect] = 50f;


// 等级26：镇神
stats = _level_stats[26];
stats[Stats.addition_armor.id] = 75;
stats[Stats.control_get.id] = -1f;
stats[S.mana] = 40000;
stats[Stats.mana_regen.id] = 500;
stats[S.health] = 60000000;
stats[S.damage] = 7000000; // 降低30%
stats[S.speed] = 500;
stats[S.lifespan] = 100000;
stats[S.knockback_reduction] = 10000;
stats[S.scale] = 2f;
stats[S.targets] = 100f;
stats[S.range] = 20f;
stats[S.area_of_effect] = 100f;    
    }


    public static ReadOnlyCollection<BaseStats> LevelStats       { get; }
    public static ReadOnlyCollection<float>     LevelExpRequired { get; }

    public static ReadOnlyCollection<string> TraitsBlacklist { get; } = new(_traits_blacklist);

    public static ReadOnlyCollection<string> StatusesBlacklist { get; } = new(_statuses_blacklist);

    public static string GetName(int level)
    {
        level = Math.Min(MaxLevel, Math.Max(level, 0));
        return LM.Get($"{ModClass.asset_id_prefix}.wudao.{level}");
    }
}