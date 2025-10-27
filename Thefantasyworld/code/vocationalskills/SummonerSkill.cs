using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using NeoModLoader.api.attributes;
using NeoModLoader.General;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ReflectionUtility;
using ai;
using System.Numerics;
using System.IO;
using ai.behaviours;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace PeerlessThedayofGodswrath.code
{
    public static class SummonerSkill
    {
        // 声明用于存储召唤物和主人关系的静态字典
        private static Dictionary<Actor, Actor> summonedCreatures = new Dictionary<Actor, Actor>();

        public static bool effect_summoner3(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 确保目标有效
            if (pTarget == null || !pTarget.isActor() || !pTarget.a.isAlive()) return false;
            if (pTile == null) return false;

            // 获取或初始化召唤数量计数
            int count = 0;
            if (pTarget.a.data.custom_data_int == null || !pTarget.a.data.custom_data_int.TryGetValue("wolfCount", out count))
            {
                pTarget.a.data.set("wolfCount", 0);
                count = 0;
            }

            // 限制最多召唤1只狼
            if (count < 1)
            {
                // 创建新的狼单位
                var act = World.world.units.createNewUnit("wolf", pTile);
                act.setKingdom(pTarget.kingdom);
                string[] randomTraits = new string[] { "RankTalentst2", "RankTalentst3", "RankTalentst4" };
                string selectedTrait = randomTraits[Randy.randomInt(0, randomTraits.Length)];
                act.addTrait(selectedTrait);
                act.addTrait("Summonedcreature1");
                act.addTrait("OrderofBeing1");
                act.data.age_overgrowth = 18;
                act.data.set("master_id", pTarget.a.data.id);
                act.data.name = $"Wolf of {pTarget.a.getName()}";
                act.goTo(pTarget.current_tile);

                // 记录召唤物和主人的关系
                if (!summonedCreatures.ContainsKey(act))
                    summonedCreatures.Add(act, pTarget.a);

                // 更新召唤计数
                count++;
                pTarget.a.data.set("wolfCount", count);
                return true;
            }
            return true;
        }

        public static bool effect_summoner4(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 确保目标有效
            if (pTarget == null || !pTarget.isActor() || !pTarget.a.isAlive()) return false;
            if (pTile == null) return false;

            // 获取或初始化召唤数量计数
            int count = 0;
            if (pTarget.a.data.custom_data_int == null || !pTarget.a.data.custom_data_int.TryGetValue("bearCount", out count))
            {
                pTarget.a.data.set("bearCount", 0);
                count = 0;
            }

            if (count < 1)
            {
                var act = World.world.units.createNewUnit("bear", pTile);
                act.setKingdom(pTarget.kingdom);
                string[] randomTraits = new string[] { "RankTalentst3", "RankTalentst4", "RankTalentst5" };
                string selectedTrait = randomTraits[Randy.randomInt(0, randomTraits.Length)];
                act.addTrait(selectedTrait);
                act.addTrait("Summonedcreature2");
                act.addTrait("OrderofBeing1");
                act.data.age_overgrowth = 18;
                act.data.set("master_id", pTarget.a.data.id);
                act.data.name = $"bear of {pTarget.a.getName()}";
                act.goTo(pTarget.current_tile);

                // 记录召唤物和主人的关系
                if (!summonedCreatures.ContainsKey(act))
                    summonedCreatures.Add(act, pTarget.a);

                // 更新召唤计数
                count++;
                pTarget.a.data.set("bearCount", count);
                return true;
            }
            return true;
        }

        public static bool effect_summoner5(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 确保目标有效
            if (pTarget == null || !pTarget.isActor() || !pTarget.a.isAlive()) return false;
            if (pTile == null) return false;

            // 获取或初始化召唤数量计数
            int count = 0;
            if (pTarget.a.data.custom_data_int == null || !pTarget.a.data.custom_data_int.TryGetValue("rhinoCount", out count))
            {
                pTarget.a.data.set("rhinoCount", 0);
                count = 0;
            }

            if (count < 1)
            {
                var act = World.world.units.createNewUnit("rhino", pTile);
                act.setKingdom(pTarget.kingdom);
                act.addTrait("RankTalentst5");
                act.addTrait("Summonedcreature3");
                act.addTrait("OrderofBeing2");
                act.data.age_overgrowth = 18;
                act.data.set("master_id", pTarget.a.data.id);
                act.data.name = $"rhino of {pTarget.a.getName()}";
                act.goTo(pTarget.current_tile);

                // 记录召唤物和主人的关系
                if (!summonedCreatures.ContainsKey(act))
                    summonedCreatures.Add(act, pTarget.a);

                // 更新召唤计数
                count++;
                pTarget.a.data.set("rhinoCount", count);
                return true;
            }
            return true;
        }

        public static bool effect_summoner6(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 确保目标有效
            if (pTarget == null || !pTarget.isActor() || !pTarget.a.isAlive()) return false;
            if (pTile == null) return false;

            // 获取或初始化召唤数量计数
            int count = 0;
            if (pTarget.a.data.custom_data_int == null || !pTarget.a.data.custom_data_int.TryGetValue("unicornCount", out count))
            {
                pTarget.a.data.set("unicornCount", 0);
                count = 0;
            }

            if (count < 1)
            {
                var act = World.world.units.createNewUnit("unicorn", pTile);
                act.setKingdom(pTarget.kingdom);
                act.addTrait("RankTalentst5");
                act.addTrait("Summonedcreature4");
                act.addTrait("OrderofBeing3");
                act.data.age_overgrowth = 18;
                act.data.set("master_id", pTarget.a.data.id);
                act.data.name = $"unicorn of {pTarget.a.getName()}";
                act.goTo(pTarget.current_tile);

                // 记录召唤物和主人的关系
                if (!summonedCreatures.ContainsKey(act))
                    summonedCreatures.Add(act, pTarget.a);

                // 更新召唤计数
                count++;
                pTarget.a.data.set("unicornCount", count);
                return true;
            }
            return true;
        }

        public static bool effect_summoner7(BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 确保目标有效
            if (pTarget == null || !pTarget.isActor() || !pTarget.a.isAlive()) return false;
            if (pTile == null) return false;

            int count = 0;
            if (pTarget.a.data.custom_data_int == null || !pTarget.a.data.custom_data_int.TryGetValue("dragonCount", out count))
            {
                pTarget.a.data.set("dragonCount", 0);
                count = 0;
            }

            if (count < 1)
            {
                var act = World.world.units.createNewUnit("dragon", pTile);
                act.setKingdom(pTarget.kingdom);
                act.addTrait("RankTalentst5");
                act.addTrait("Summonedcreature5");
                act.addTrait("OrderofBeing4");
                act.data.age_overgrowth = 18;
                act.data.set("master_id", pTarget.a.data.id);
                act.data.name = $"dragon of {pTarget.a.getName()}";
                act.goTo(pTarget.current_tile);

                // 记录召唤物和主人的关系
                if (!summonedCreatures.ContainsKey(act))
                    summonedCreatures.Add(act, pTarget.a);

                // 更新召唤计数
                count++;
                pTarget.a.data.set("dragonCount", count);
                return true;
            }
            return true;
        }

        public static bool tamedBeastSpecialEffect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || pTarget.a == null || !pTarget.a.isAlive())
                return false;

            Actor beast = pTarget.a;
            if (summonedCreatures.ContainsKey(beast))
            {
                Actor master = summonedCreatures[beast];
                if (master != null && master.isAlive())
                {
                    if (Randy.randomChance(0.1f))
                    {
                        beast.goTo(master.current_tile);
                        return true;
                    }
                }
            }
            else
            {
                if (beast.data?.custom_data_long != null &&
                    beast.data.custom_data_long.TryGetValue("master_id", out long masterId))
                {
                    Actor master = World.world.units.get(masterId);
                    if (master != null)
                    {
                        if (summonedCreatures == null)
                        {
                            return false;
                        }
                        summonedCreatures[beast] = master;
                        if (master.kingdom != null)
                            beast.kingdom = master.kingdom;

                        return true;
                    }
                }
            }

            return false;
        }
    }
}