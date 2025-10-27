using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerlessThedayofGodswrath.code
{
    internal class traitGroup
    {
        public static void Init()
        {
            ActorTraitGroupAsset RankTalentst = new ActorTraitGroupAsset
            {
                id = "RankTalentst",
                name = "trait_group_RankTalentst",
                color = "#FFD700" // 金色
            };
            AssetManager.trait_groups.add(RankTalentst);

            ActorTraitGroupAsset enchanter = new ActorTraitGroupAsset
            {
                id = "enchanter",
                name = "trait_group_enchanter",
                color = "#800080" // 紫色 - 法师特质组
            };
            AssetManager.trait_groups.add(enchanter);

            ActorTraitGroupAsset pastor = new ActorTraitGroupAsset
            {
                id = "pastor",
                name = "trait_group_pastor",
                color = "#00BFFF" // 深天蓝色 - 牧师特质组
            };
            AssetManager.trait_groups.add(pastor);

            ActorTraitGroupAsset knight = new ActorTraitGroupAsset
            {
                id = "knight",
                name = "trait_group_knight",
                color = "#0000FF" // 蓝色 - 骑士特质组
            };
            AssetManager.trait_groups.add(knight);

            ActorTraitGroupAsset valiantgeneral = new ActorTraitGroupAsset
            {
                id = "valiantgeneral",
                name = "trait_group_valiantgeneral",
                color = "#8B0000" // 深红色 - 战士特质组
            };
            AssetManager.trait_groups.add(valiantgeneral);

            ActorTraitGroupAsset shooter = new ActorTraitGroupAsset
            {
                id = "shooter",
                name = "trait_group_shooter",
                color = "#008000" // 深绿色 - 射手特质组
            };
            AssetManager.trait_groups.add(shooter);

            ActorTraitGroupAsset assassin = new ActorTraitGroupAsset
            {
                id = "assassin",
                name = "trait_group_assassin",
                color = "#808080" // 灰色 - 刺客特质组
            };
            AssetManager.trait_groups.add(assassin);
        }
    }
}