using System.Linq;
using NeoModLoader.api.attributes;
using UnityEngine;
using UnityEngine.UI;

namespace CustomModT001;

internal static class WindowCreatureInfoPatchHelper
{/*
    private static StatBar                            _hp_bar;
    private static Image                              _hp_bg;
    private static StatBar                            _cultiprogress;
    private static StatBar                            _mp_bar;
    private static GameObject                         _culti_trait_group;
    private static ObjectPoolGenericMono<TraitButton> _cultitrait_pool;

    public static void Initialize(WindowCreatureInfo window)
    {
        UiUnitAvatarElement avatar = window.avatarElement;
        Transform mask = avatar.gameObject.transform.Find("Mask");
        var tip_button = mask.gameObject.AddComponent<TipButton>();
        tip_button.hoverAction = () => { tooltip_action_cultisys(avatar.gameObject, window.actor); };
        window.gameObject.transform.Find("Background/Scroll View").GetComponent<ScrollRect>().enabled = true;
        window.gameObject.transform.Find("Background/Scroll View/Viewport").GetComponent<Mask>().enabled = true;
        window.gameObject.transform.Find("Background/Scroll View/Viewport").GetComponent<Image>().enabled = true;
        Transform content_transform = window.gameObject.transform.Find("Background/Scroll View/Viewport/Content");
        VerticalLayoutGroup vert_layout_group = content_transform.GetComponent<VerticalLayoutGroup>() ??
                                                content_transform.gameObject.AddComponent<VerticalLayoutGroup>();
        ContentSizeFitter fitter = content_transform.GetComponent<ContentSizeFitter>() ??
                                   content_transform.gameObject.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.MinSize;
        vert_layout_group.childControlHeight = false;
        vert_layout_group.childControlWidth = false;
        vert_layout_group.childForceExpandHeight = false;
        vert_layout_group.childForceExpandWidth = false;
        vert_layout_group.childScaleHeight = false;
        vert_layout_group.childScaleWidth = false;
        vert_layout_group.spacing = 4;

        _hp_bar = window.health;
        _mp_bar = Object.Instantiate(_hp_bar,        _hp_bar.transform.parent);
        _cultiprogress = Object.Instantiate(_hp_bar, _hp_bar.transform.parent);

        _hp_bg = _hp_bar.transform.Find("Background").GetComponent<Image>();
        _hp_bg.sprite =
            SpriteTextureLoader.getSprite("ui/special/windowBar");
        _hp_bg.color = Color.white;

        _mp_bar.transform.Find("Icon").GetComponent<Image>().sprite =
            SpriteTextureLoader.getSprite("inmny/custommodt001/iconCheckWakan");
        _mp_bar.transform.Find("Mask/Bar").GetComponent<Image>().color = Color.blue;
        _mp_bar.GetComponent<TipButton>().textOnClick = $"{ModClass.asset_id_prefix}.mana";
        _cultiprogress.transform.Find("Icon").GetComponent<Image>().sprite =
            SpriteTextureLoader.getSprite("inmny/custommodt001/iconCultiSysSquare");
        _cultiprogress.transform.Find("Mask/Bar").GetComponent<Image>().color = Color.gray;
        _cultiprogress.GetComponent<TipButton>().textOnClick = $"{ModClass.asset_id_prefix}.ui.cultiprogress";

        var bars_rect = _hp_bar.transform.parent.GetComponent<RectTransform>();
        fitter = bars_rect.gameObject.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        fitter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
        var grid_layout = bars_rect.gameObject.AddComponent<GridLayoutGroup>();
        grid_layout.cellSize = new Vector2(90, 14);
        grid_layout.spacing = new Vector2(5,   4);
        //grid_layout.padding = new(0, 0, 8, 4);
        grid_layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid_layout.constraintCount = 2;

        Transform part2 = window.equipmentParent.parent.parent;
        _culti_trait_group = Object.Instantiate(window.equipmentParent.parent.gameObject, content_transform);
        _culti_trait_group.transform.SetSiblingIndex(part2.GetSiblingIndex() + 2);
        _culti_trait_group.transform.Find("Title").GetComponent<LocalizedText>()
            .setKeyAndUpdate($"{ModClass.asset_id_prefix}.ui.cultitrait");
        _culti_trait_group.GetComponent<RectTransform>().sizeDelta = new Vector2(190, 23);

        _cultitrait_pool = new ObjectPoolGenericMono<TraitButton>(window.pool_traits._prefab,
            _culti_trait_group.transform.Find("Equipment Grid"));
        var i = _cultitrait_pool._parent_transform.childCount;
        while (i-- > 0) Object.Destroy(_cultitrait_pool._parent_transform.GetChild(i).gameObject);
        _cultitrait_pool._parent_transform.gameObject.SetActive(true);
    }

    [Hotfixable]
    public static void OnEnable(WindowCreatureInfo window)
    {
        LoadCultiTraits(window);
        Actor actor = window.actor;

        var shield_value = actor.GetShields().Sum(x => x.value);
        if ((int)shield_value > 0)
        {
            _hp_bg.color = Color.white;
            _hp_bar.setBar(actor.data.health, actor.data.health + (int)shield_value,
                $"/{actor.stats[S.health]}(+{Toolbox.formatNumber((int)shield_value)})");
        }
        else
        {
            _hp_bg.color = new Color(0.12f, 0.12f, 0.1f, 1);
        }

        _mp_bar.setBar(actor.GetMana(), (int)actor.stats[Stats.mana.id], $"/{(int)actor.stats[Stats.mana.id]}");

        var level = actor.GetCultisysLevel();
        var max_exp = level < Cultisys.MaxLevel ? Cultisys.LevelExpRequired[level] : actor.GetExp();
        _cultiprogress.setBar(actor.GetExp(), max_exp, $"/{(int)max_exp}");
    }

    [Hotfixable]
    private static void LoadCultiTraits(WindowCreatureInfo window)
    {
        _cultitrait_pool.clear();
        Actor actor = window.actor;
        var traits_to_load = actor.data.traits.Where(Traits.all_traits.Contains).ToList();

        _culti_trait_group.SetActive(traits_to_load.Any());
        foreach (var id in traits_to_load)
        {
            TraitButton button = _cultitrait_pool.getNext();
            button.Awake();
            button.load(id);
        }
    }

    private static void tooltip_action_cultisys(GameObject obj, Actor actor)
    {
        Tooltip.show(obj, Tooltips.cultisys.id, new TooltipData
        {
            actor = actor
        });
    }*/
}