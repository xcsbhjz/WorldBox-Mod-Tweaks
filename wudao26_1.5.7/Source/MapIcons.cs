using System.Linq;
using CustomModT001.Abstract;
using NeoModLoader.api.attributes;
using UnityEngine;

namespace CustomModT001;

public class MapIcons : ExtendLibrary<QuantumSpriteAsset, MapIcons>
{
    [CloneSource("status_effects")] public static QuantumSpriteAsset shield { get; private set; }
    [CloneSource("status_effects")] public static QuantumSpriteAsset demon_king { get; private set; }
    [CloneSource("status_effects")] public static QuantumSpriteAsset werewolf_appearance { get; private set; }

    protected override void OnInit()
    {
        RegisterAssets(ModClass.asset_id_prefix);

        shield.draw_call = [Hotfixable](asset) =>
        {
            var time = AnimationHelper.getTime() * 5;
            var shield_sprites = SpriteTextureLoader.getSpriteList("effects/fx_status_shield_t");
            foreach (Actor actor in World.world.units)
            {
                if (actor.GetShields().Sum(x => x.value) <= 0) continue;

                Sprite sprite = AnimationHelper.getSpriteFromList(time, actor.GetHashCode(), shield_sprites);
                var scale = actor.actor_scale;
                var mark = QuantumSpriteLibrary.drawQuantumSprite(asset, actor.cur_transform_position);
                mark.setSprite(sprite);
                mark.setScale(scale);
                mark.setFlipX(actor.flip);
            }
        };
        demon_king.draw_call = [Hotfixable](asset) =>
        {
            var sprite = SpriteTextureLoader.getSprite("inmny/custommodt001/emperor_heart");
            float tCameraMod = QuantumSpriteLibrary.getCameraScaleZoomMultiplier(asset) * 1.6f;
            foreach (Actor actor in World.world.units)
            {
                if ((actor.hasTrait(Traits.king_armor.id) || actor.hasTrait(Traits.king_crown.id) || actor.hasTrait(Traits.king_sword.id))
                    &&(actor.hasTrait(Traits.demon_armor.id) || actor.hasTrait(Traits.demon_crown.id) || actor.hasTrait(Traits.demon_sword.id)))
                {
                    float tActorScale = 0.1f;
                    float tWidthXScale = 8f * tActorScale * tCameraMod;
                    Vector3 tPos = default(Vector3);
                    tPos.x = actor.cur_transform_position.x - tWidthXScale / 2f;
                    tPos.y = actor.cur_transform_position.y + 13f * tActorScale;

                    var scale = actor.actor_scale*0.3f;
                    var mark = QuantumSpriteLibrary.drawQuantumSprite(asset, tPos);
                    mark.setSprite(sprite);
                    mark.setScale(scale);
                    mark.setFlipX(actor.flip);
                }
            }
        };
        werewolf_appearance.draw_call = [Hotfixable](asset) =>
        {
            var sprite = SpriteTextureLoader.getSprite("inmny/custommodt001/werewolf_appearance");
            if (!Traits.werewolf_appearance_eras.Contains(World.world_era ?? null)) return;
            float tCameraMod = QuantumSpriteLibrary.getCameraScaleZoomMultiplier(asset) * 1.6f;
            foreach (Actor actor in World.world.units)
            {
                if (actor.hasTrait(Traits.werewolf_appearance.id))
                {
                    float tActorScale = 0.1f;
                    float tWidthXScale = 8f * tActorScale * tCameraMod;
                    Vector3 tPos = default(Vector3);
                    tPos.x = actor.cur_transform_position.x - tWidthXScale / 2f;
                    tPos.y = actor.cur_transform_position.y + 13f * tActorScale;

                    var scale = actor.actor_scale*0.3f;
                    var mark = QuantumSpriteLibrary.drawQuantumSprite(asset, tPos);
                    mark.setSprite(sprite);
                    mark.setScale(scale);
                    mark.setFlipX(actor.flip);
                }
            }
        };
        
    }

    public override void PostInit(QuantumSpriteAsset asset)
    {
        var system = new GameObject().AddComponent<QuantumSpriteGroupSystem>();
        system.create(asset);
        asset.group_system = system;
        if (asset.default_amount != 0)
        {
            for (int i = 0; i < asset.default_amount; i++)
            {
                system.getNext();
            }
            system.clearFull();
        }
    }
}