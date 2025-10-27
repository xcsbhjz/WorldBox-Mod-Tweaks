using CustomModT001.Abstract;
using strings;
using UnityEngine;

namespace CustomModT001;

public class Actors : ExtendLibrary<ActorAsset, Actors>
{
    [CloneSource(nameof(SA.skeleton))] public static ActorAsset skeleton_summoned { get; private set; }
    [CloneSource(nameof(SA.cold_one))]public static ActorAsset ice_demon_summoned { get; private set; }
    [CloneSource(nameof(SA.demon))] public static ActorAsset demon_summoned { get; private set; }
    [CloneSource(nameof(SA.demon))] public static ActorAsset demon_king_1   { get; private set; }
    [CloneSource(nameof(SA.demon))] public static ActorAsset demon_king_2   { get; private set; }
    [CloneSource(nameof(SA.demon))] public static ActorAsset demon_king_3   { get; private set; }
    [CloneSource(nameof(SA.demon))] public static ActorAsset demon_king_4   { get; private set; }

    protected override void OnInit()
    {
        RegisterAssets(ModClass.asset_id_prefix);

        // 为所有自定义角色资产配置属性
        // 注意：不要设置不存在的shadow_size属性
        ConfigureSummonedActor(skeleton_summoned, 5000, 50);
        ConfigureSummonedActor(ice_demon_summoned, 4500, 400, 80);
        ConfigureSummonedActor(demon_summoned, 4500, 10, 80);

        SetupDemonKing(demon_king_1, Traits.undead_war_god);
        SetupDemonKing(demon_king_2, Traits.cyan_fury);
        SetupDemonKing(demon_king_3, Traits.end_of_time);
        SetupDemonKing(demon_king_4, Traits.mercy_lighthouse);
    }

    // 辅助方法：配置召唤角色的基本属性
    private void ConfigureSummonedActor(ActorAsset asset, int health, int damage, int armor = 0)
    {
        asset.traits.Add(Traits.summoned.id);
        asset.base_stats[S.health] = health;
        asset.base_stats[S.damage] = damage;
        if (armor > 0) asset.base_stats[S.armor] = armor;
        asset.traits.Remove("immortal");
        asset.base_stats[S.lifespan] = 25;
        asset.job = [ActorJobs.summoned_job.id];
        
        // 确保使用有效的阴影纹理而不是设置不存在的属性
        asset.shadow = true;
        asset.shadow_texture = "unitShadow_5";  // 使用默认的阴影纹理
        asset.shadow_texture_egg = "unitShadow_2";  // 设置蛋形态的阴影纹理
        asset.shadow_texture_baby = "unitShadow_4";  // 设置婴儿形态的阴影纹理
    }

    private void SetupDemonKing(ActorAsset asset, ActorTrait given_trait)
    {
        asset.traits.Remove("burning_feet");
        asset.traits.Add(Traits.summoned.id);
        asset.traits.Add(given_trait.id);
        asset.base_stats[S.health] = 1000000;
        asset.base_stats[S.damage] = 4000;
        asset.base_stats[S.attack_speed] = 50;
        asset.name_locale = asset.id;
        asset.traits.Remove("immortal");
        asset.base_stats[S.lifespan] = 25;
        // 移除不存在的shadow_size设置
        // asset.shadow_size = new Vector2(2f, 1f);
    }

    protected override ActorAsset Add(ActorAsset asset)
    {
        base.Add(asset);
        // 正确的阴影加载方式：直接调用asset.texture_asset.loadShadow()
        if (asset.shadow && asset.texture_asset != null)
        {
            asset.texture_asset.loadShadow();
        }
    
        return asset;
    }
    
    protected override ActorAsset Clone(string new_id, string from_id)
    {
        ActorAsset asset = base.Clone(new_id, from_id);
        // 同样使用正确的方式加载阴影
        if (asset.shadow && asset.texture_asset != null)
        {
            asset.texture_asset.loadShadow();
        }
    
        return asset;
    }
}