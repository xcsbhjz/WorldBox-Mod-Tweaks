namespace CustomModT001;

public static class ModConfigHelper
{
    public static bool WarRuleCitySurrender;
    public static float TalentMultiplier;
    public static float KillExpK;
    public static bool WeaponNoDurabilityLoss = true; // 默认设置为true
    // 添加新的配置项，用于控制是否限制自动获得特质
    public static bool WarRuleLimitTraitGain;

    public static float GetKillExpK()
    {
        return KillExpK;
    }

    public static void SetKillExpK(float value)
    {
        KillExpK = value;
    }
    public static bool AllowWarRuleCitySurrender()
    {
        return WarRuleCitySurrender;
    }

    public static float GetTalentMultiplier()
    {
        return TalentMultiplier;
    }

    public static void SetTalentMultiplier(float value)
    {
        TalentMultiplier = value;
    }
    public static void SwitchWarRuleCitySurrender(bool value)
    {
        WarRuleCitySurrender = value;
    }
    
    // 添加这个方法
    public static void SwitchWeaponNoDurabilityLoss(bool value)
    {
        WeaponNoDurabilityLoss = value;
    }
    
    // 添加对应的方法
    public static bool AllowWarRuleLimitTraitGain()
    {
        return WarRuleLimitTraitGain;
    }

    public static void SwitchWarRuleLimitTraitGain(bool value)
    {
        WarRuleLimitTraitGain = value;
    }
}