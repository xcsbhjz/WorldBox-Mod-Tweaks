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

            ActorTraitGroupAsset OrderofBeing = new ActorTraitGroupAsset
            {
                id = "OrderofBeing",
                name = "trait_group_OrderofBeing",
                color = "#FF4500" // 橙红色 -  demi-god特质组
            };
            AssetManager.trait_groups.add(OrderofBeing);    

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

            ActorTraitGroupAsset Ranger = new ActorTraitGroupAsset
            {
                id = "Ranger",
                name = "trait_group_Ranger",
                color = "#008000" // 深绿色 - 射手特质组
            };
            AssetManager.trait_groups.add(Ranger);

            ActorTraitGroupAsset Assassin = new ActorTraitGroupAsset
            {
                id = "Assassin",
                name = "trait_group_Assassin",
                color = "#808080" // 灰色 - 刺客特质组
            };
            AssetManager.trait_groups.add(Assassin);

            ActorTraitGroupAsset Summoner = new ActorTraitGroupAsset
            {
                id = "Summoner",
                name = "trait_group_Summoner",
                color = "#9932CC" // 暗紫色 - 召唤师特质组（契约使者）
            };
            AssetManager.trait_groups.add(Summoner);

            ActorTraitGroupAsset minstrel = new ActorTraitGroupAsset
            {
                id = "minstrel",
                name = "trait_group_minstrel",
                color = "#FF69B4" // 粉色 - 吟游诗人特质组（旋律大师）
            };
            AssetManager.trait_groups.add(minstrel);

            ActorTraitGroupAsset warlock = new ActorTraitGroupAsset
            {
                id = "warlock",
                name = "trait_group_warlock",
                color = "#4B0082" // 靛蓝色 - 咒术师特质组（诅咒大师）
            };
            AssetManager.trait_groups.add(warlock);

            ActorTraitGroupAsset alchemist = new ActorTraitGroupAsset
            {
                id = "alchemist",
                name = "trait_group_alchemist",
                color = "#FF4500" // 橙红色 - 炼金术士特质组（元素大师）
            };
            AssetManager.trait_groups.add(alchemist);

            ActorTraitGroupAsset barbarian = new ActorTraitGroupAsset
            {
                id = "barbarian",
                name = "trait_group_barbarian",
                color = "#B22222" // 耐火砖红色 - 野蛮人特质组（野蛮icus）
            };
            AssetManager.trait_groups.add(barbarian);

            ActorTraitGroupAsset Summonedcreature = new ActorTraitGroupAsset
            {
                id = "Summonedcreature",
                name = "trait_group_Summonedcreature",
                color = "#9932CC" // 暗紫色 - 召唤物特质组（契约使者）
            };
            AssetManager.trait_groups.add(Summonedcreature);
        }
    }
}