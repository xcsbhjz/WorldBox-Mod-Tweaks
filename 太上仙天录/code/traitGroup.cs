using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XianTu
{
    internal class traitGroup
    {
        public static void Init()
        {
            ActorTraitGroupAsset XianTu = new ActorTraitGroupAsset();
            XianTu.id = "XianTu";
            XianTu.name = "trait_group_XianTu";
            XianTu.color = "#FFFF00";
            AssetManager.trait_groups.add(XianTu);

            ActorTraitGroupAsset TaiyiLg = new ActorTraitGroupAsset();
            TaiyiLg.id = "TaiyiLg";
            TaiyiLg.name = "trait_group_TaiyiLg";
            TaiyiLg.color = "#00FF00";
            AssetManager.trait_groups.add(TaiyiLg);


            ActorTraitGroupAsset TyXuemai = new ActorTraitGroupAsset();
            TyXuemai.id = "TyXuemai";
            TyXuemai.name = "trait_group_TyXuemai";
            TyXuemai.color = "#FF4500"; // 橙红色
            AssetManager.trait_groups.add(TyXuemai);

            // 添加武道体系分组
            ActorTraitGroupAsset TyWudao = new ActorTraitGroupAsset
            {
                id = "TyWudao",
                name = "trait_group_TyWudao",
                color = "#8B4513" // 棕色
            };
            AssetManager.trait_groups.add(TyWudao);

            // 添加武道资质分组
            ActorTraitGroupAsset TyGengu = new ActorTraitGroupAsset
            {
                id = "TyGengu",
                name = "trait_group_TyGengu",
                color = "#CD853F" // 秘鲁色
            };
            AssetManager.trait_groups.add(TyGengu);

            // 添加道基体系分组
            ActorTraitGroupAsset daoJiGroup = new ActorTraitGroupAsset
            {
                id = "DaoJi",
                name = "trait_group_DaoJi",
                color = "#00BFFF" // 深蓝色
            };
            AssetManager.trait_groups.add(daoJiGroup);

            // 添加突破丹药系列分组
            ActorTraitGroupAsset tupoDanyaoGroup = new ActorTraitGroupAsset
            {
                id = "TupoDanyao",
                name = "trait_group_TupoDanyao",
                color = "#FF69B4" // 粉红色
            };
            AssetManager.trait_groups.add(tupoDanyaoGroup);

            // 添加修为丹药系列分组
            ActorTraitGroupAsset xiulianDanyaoGroup = new ActorTraitGroupAsset
            {
                id = "XiulianDanyao",
                name = "trait_group_XiulianDanyao",
                color = "#9370DB" // 紫色
            };
            AssetManager.trait_groups.add(xiulianDanyaoGroup);

            // 添加三千大道系列分组
            ActorTraitGroupAsset xiuxianLiuyiGroup = new ActorTraitGroupAsset
            {
                id = "XiuxianLiuyi",
                name = "trait_group_XiuxianLiuyi",
                color = "#FFD700" // 金色
            };
            AssetManager.trait_groups.add(xiuxianLiuyiGroup);

            // 添加大道印记系列分组
            ActorTraitGroupAsset daDaoYinJiGroup = new ActorTraitGroupAsset
            {
                id = "DaDaoYinJi",
                name = "trait_group_DaDaoYinJi",
                color = "#FF00FF" // 紫红色
            };
            AssetManager.trait_groups.add(daDaoYinJiGroup);

            // 添加道果系列分组
            ActorTraitGroupAsset daoGuoGroup = new ActorTraitGroupAsset
            {
                id = "DaoGuo",
                name = "trait_group_DaoGuo",
                color = "#9932CC" // 深紫色
            };
            AssetManager.trait_groups.add(daoGuoGroup);
        }
    }
}