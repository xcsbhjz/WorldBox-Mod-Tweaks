using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Distributions;
using NeoModLoader.api.attributes;
using Newtonsoft.Json;
using strings;
using UnityEngine;

namespace CustomModT001;

public struct ShieldData
{
    public float value;
    public float left_time;
}

public static class ActorExtendTools
{
    private const string key_prefix = ModClass.asset_id_prefix;

    private const string mana_key              = $"{key_prefix}.mana";
    private const string continue_attack_times = $"{key_prefix}.continue_attack_times";
    private const string last_attack_time_key      = $"{key_prefix}.last_attack_time";
    private const string demon_embrace_count   = $"{key_prefix}.demon_embrace_count";
    private const string steel_heart_count     = $"{key_prefix}.steel_heart_count";
    private const string summoned_obj_owner    = $"{key_prefix}.summoned_obj_owner";
    private const string shield_key            = $"{key_prefix}.shields";

    private const string lake_shield_buff_key = $"{key_prefix}.lake_shield";

    private const string squally_showers_key         = $"{key_prefix}.squally_showers";
    private const string overwrite_stats_key         = $"{key_prefix}.overwrite_stats";
    private const string cultisys_level_key          = $"{key_prefix}.wudao_level";
    private const string exp_key                     = $"{key_prefix}.wudao_exp";
    private const string talent_key                  = $"{key_prefix}.talent";
    private const float  mean                        = 50;
    private const float  stddev                      = 15;
    private const string soldier_badge_key           = $"{key_prefix}.soldier_badge";
    private const string emperor_badge_key           = $"{key_prefix}.emperor_badge";
    private const string unbreakable_grip_key        = $"{key_prefix}.unbreakable_grip";
    private const string both_attack_armor_key       = $"{key_prefix}.both_attack_armor";
    private const string unmovable_like_mountain_key = $"{key_prefix}.unmovable_like_mountain";

    private const string survivor_contract_count_key = $"{key_prefix}.survivor_contract_count";    
    private const string health_boost_count_key = $"{key_prefix}.health_boost_count";
    private const string survivor_contract_timer_key = $"{key_prefix}.survivor_contract_timer";
    private const string undead_war_god_key          = $"{key_prefix}.undead_war_god";
    private const string summon_source_key           = $"{key_prefix}.summon_source";
    private const string war_ashes_key = $"{key_prefix}.war_ashes";
    private const string grass_can_talk_key          = $"{key_prefix}.grass_can_talk";
    private const string countless_trials_key = $"{key_prefix}.countless_trials";
    private const string soul_return_key = $"{key_prefix}.soul_return";
    private const string final_skill_key = $"{key_prefix}.final_skill";
    private const           float  max_talent = 10000;
    private static readonly Normal normal_rng = new(mean, stddev);

    public static bool HasFinalSkill(this Actor actor)
    {
        actor.data.get(final_skill_key, out string final_skill, string.Empty);
        if (actor.hasTrait(final_skill))
        {
            return true;
        }

        return false;
    }

    public static string GetFinalSkill(this Actor actor)
    {
        actor.data.get(final_skill_key, out string final_skill, string.Empty);
        return final_skill;
    }

    public static void SetFinalSkill(this Actor actor, string final_skill)
    {
        actor.data.set(final_skill_key, final_skill);
    }
    public static void UpdateSoulReturn(this Actor actor)
    {
        actor.data.get(soul_return_key, out int count);
        actor.data.set(soul_return_key, count + 1);
    }

    public static int GetSoulReturnCount(this Actor actor)
    {
        actor.data.get(soul_return_key, out int count);
        return count;
    }
    public static void UpdateCountlessTrials(this Actor actor)
    {
        actor.data.get(countless_trials_key, out int count);
        actor.data.get(countless_trials_key, out float last_gethit_time);
        var time = (float)World.world.getCurWorldTime();
        if (time - last_gethit_time > 20)
        {
            count = 0;
        }
        actor.data.set(countless_trials_key, count + 1);
        actor.data.set(countless_trials_key, time);
    }

    public static int GetCountlessTrials(this Actor actor)
    {
        actor.data.get(countless_trials_key, out float last_gethit_time);
        
        actor.data.get(countless_trials_key, out int count);
        var time = (float)World.world.getCurWorldTime();
        if (time - last_gethit_time > 20)
        {
            count = 0;
        }

        return count;
    }
    public static void SetGrassCanTalkState(this Actor actor, bool state)
    {
        actor.data.set(grass_can_talk_key, state);
    }

    public static bool GetGrassCanTalkState(this Actor actor)
    {
        actor.data.get(grass_can_talk_key, out bool state);
        return state;
    }
    public static void IncWarAshesCount(this Actor actor, int count)
    {
        actor.data.get(war_ashes_key, out int curr_count);
        actor.data.set(war_ashes_key, curr_count + count);
    }

    public static int GetWarAshesCount(this Actor actor)
    {
        actor.data.get(war_ashes_key, out int curr_count);
        return curr_count;
    }
    public static void DecOwnerSummonSource(this Actor actor)
    {
        actor.data.get(summon_source_key, out var source, "");
        if (string.IsNullOrEmpty(source)) return;
        Actor owner = actor.GetOwner();
        if (owner == null) return;
        owner.data.get(source, out int count);
        owner.data.set(source, count - 1);
    }

    public static void IncOwnerSummonSource(this Actor actor)
    {
        actor.data.get(summon_source_key, out var source, "");
        if (string.IsNullOrEmpty(source)) return;
        Actor owner = actor.GetOwner();
        if (owner == null) return;
        owner.data.get(source, out int count);
        owner.data.set(source, count + 1);
    }

    public static int GetSummonCount(this Actor actor, string source)
    {
        actor.data.get(source, out int count);
        return count;
    }

    public static void SetSummonSource(this Actor actor, string source)
    {
        actor.data.set(summon_source_key, source);
    }

    public static bool HasUndeadWarGodTriggerd(this Actor actor)
    {
        return actor.data.hasFlag(undead_war_god_key);
    }

    public static void SetUndeadWarGodTriggerd(this Actor actor)
    {
        actor.data.addFlag(undead_war_god_key);
    }

    public static void RemoveUndeadWarGodTrigger(this Actor actor)
    {
        actor.data.removeFlag(undead_war_god_key);
        actor.data.removeInt(undead_war_god_key);
    }

    public static int IncreaseUndeadWarGodCount(this Actor actor)
    {
        actor.data.get(undead_war_god_key, out int count);
        actor.data.set(undead_war_god_key, count + 1);
        return count + 1;
    }

    public static void CheckSurvivorContractTimer(this Actor actor)
    {
        var curr_time = (float)World.world.getCurWorldTime();
        if (actor.data.health / (float)actor.getMaxHealth() > 0.2f)
        {
            actor.data.set(survivor_contract_timer_key, curr_time);
        }
        else
        {
            actor.data.get(survivor_contract_timer_key, out float last_time);
            if (curr_time - last_time > 240)
            {
                actor.data.set(survivor_contract_count_key, actor.GetSurvivorContractCount() + 1);
                actor.data.set(survivor_contract_timer_key, curr_time);
            }
        }
    }
    public static void IncSurvivorContractCount(this Actor actor)
    {
        actor.data.set(survivor_contract_count_key, actor.GetSurvivorContractCount() + 1);
    }


    public static int GetSurvivorContractCount(this Actor actor)
    {
        actor.data.get(survivor_contract_count_key, out int count);
        return count;
    }

    public static int GetHealthBoostCount(this Actor actor)
    {
        actor.data.get(health_boost_count_key, out int count);
        return count;
    }

    public static void SetHealthBoostCount(this Actor actor, int count)
    {
        actor.data.set(health_boost_count_key, count);
    }

    public static int GetLoongSoulCount(this Actor actor)
    {
        return actor.traits.Count(x => x.id.EndsWith("loong_soul") || x == Traits.undead_war_god);
    }

    public static bool HasSoldierBadge(this Actor actor)
    {
        if (actor.data.hasFlag(soldier_badge_key)) return true;
        if (actor.isProfession(UnitProfession.Warrior))
        {
            actor.data.addFlag(soldier_badge_key);
            return true;
        }

        return false;
    }

    public static bool HasEmperorBadge(this Actor actor)
    {
        if (actor.data.hasFlag(emperor_badge_key)) return true;
        if (actor.isProfession(UnitProfession.King))
        {
            actor.data.addFlag(emperor_badge_key);
            return true;
        }

        return false;
    }

    public static int GetUnbreakableGripCount(this Actor actor)
    {
        actor.data.get(unbreakable_grip_key, out int count);
        return count;
    }

    public static void IncUnbreakableGripCount(this Actor actor)
    {
        actor.data.set(unbreakable_grip_key, actor.GetUnbreakableGripCount() + 1);
    }

    public static float GetBothAttackArmor(this Actor actor)
    {
        actor.data.get(both_attack_armor_key, out float value);
        return value;
    }

    public static void UpdateBothAttackArmor(this Actor actor)
    {
        actor.data.set(both_attack_armor_key, actor.stats[S.damage]);
    }

    public static int GetUnmovableLikeMountainCount(this Actor actor)
    {
        actor.data.get(unmovable_like_mountain_key, out int count);
        return count;
    }

    public static void IncUnmovableLikeMountainCount(this Actor actor)
    {
        actor.data.set(unmovable_like_mountain_key, actor.GetUnmovableLikeMountainCount() + 1);
    }

    public static int GetMana(this Actor actor)
    {
        return (int)actor.GetRawMana();
    }

    private static float GetRawMana(this Actor actor)
    {
        return actor.getMana();
        actor.data.get(mana_key, out float mana);
        return mana;
    }

    public static void SetMana(this Actor actor, float value)
    {
        actor.setMana((int)value);
        //actor.data.set(mana_key, value);
    }

    public static void RestoreMana(this Actor actor, float value)
    {
        actor.addMana((int)value);
        //actor.data.set(mana_key, Mathf.Min(actor.GetRawMana() + value, actor.stats[Stats.mana.id]));
    }
    [Hotfixable]
    public static bool TryTakeMana(this Actor actor, int value)
    {
        if (actor.GetMana() < value) return false;

        actor.spendMana(value);
        return true;
    }

    public static void AddInvincible(this Actor actor, float duration)
    {
        const string status_id = "invincible";
        if (actor.hasStatus(status_id))
        {
            var status = actor._active_status_dict[status_id];
            status._end_time += duration;
        }
        else
        {
            actor.addStatusEffect(status_id, duration);
        }
    }

    public static int GetContinueAttackTimes(this Actor actor)
    {
        var last_time = actor.GetLastAttackTime();
        if (last_time + 10 < MapBox.instance.getCurWorldTime()) return 0;

        actor.data.get(continue_attack_times, out int times);
        return times;
    }

    public static float GetLastAttackTime(this Actor actor)
    {
        actor.data.get(last_attack_time_key, out float last_time);
        return last_time;
    }

    public static void SetLastAttackTime(this Actor actor, float time)
    {
        actor.data.set(last_attack_time_key, time);
    }

    public static int GetDemonEmbraceCount(this Actor actor)
    {
        actor.data.get(demon_embrace_count, out int count);
        return count;
    }

    public static void ResetDemonEmbraceCount(this Actor actor)
    {
        actor.data.set(demon_embrace_count, 0);
    }

    public static void IncDemonEmbraceCount(this Actor actor, int count)
    {
        actor.data.set(demon_embrace_count, actor.GetDemonEmbraceCount() + count);
    }

    public static int GetSteelHeartCount(this Actor actor)
    {
        actor.data.get(steel_heart_count, out int count);
        return count;
    }

    public static void IncSteelHeartCount(this Actor actor)
    {
        actor.data.set(steel_heart_count, actor.GetSteelHeartCount() + 1);
    }

    public static bool TraitUnderCooldown(this Actor actor, string trait_id)
    {
        actor.data.get($"{key_prefix}.trait_{trait_id}_cooldown", out float cooldown);
        return cooldown > 0;
    }

    public static float GetCooldown(this Actor actor, string trait_id)
    {
        actor.data.get($"{key_prefix}.trait_{trait_id}_cooldown", out float cooldown);
        return cooldown;
    }

    public static void StartCooldown(this Actor actor, ActorTrait trait, float override_time = -1)
    {
        actor.data.set($"{key_prefix}.trait_{trait.id}_cooldown",
            override_time < 0 ? trait.GetExtend().cooldown : override_time);
    }

    public static void FinishCooldown(this Actor actor, string trait_id)
    {
        actor.data.set($"{key_prefix}.trait_{trait_id}_cooldown", 0);
    }

    public static void DecreaseCooldown(this Actor actor, string trait_id, float value)
    {
        var key = $"{key_prefix}.trait_{trait_id}_cooldown";
        actor.data.get(key, out float cooldown);
        actor.data.set(key, Math.Max(0, cooldown - value));
    }

    public static void ApplyStatusEffectTo(this Actor caster,              BaseSimObject target, string status_id,
                                           float      overwrite_time = -1, bool          apply_mod_to_overwrite = false)
    {
        var status = AssetManager.status.get(status_id);
        if (status == null) return;
        if (overwrite_time < 0)
        {
            if (status.base_stats.hasTag(S_Tag.immovable))
                overwrite_time = status.duration * (1 + caster.stats[Stats.control_give.id]);
        }
        else if (apply_mod_to_overwrite && status.base_stats.hasTag(S_Tag.immovable))
        {
            overwrite_time *= 1 + caster.stats[Stats.control_give.id];
        }

        target.addStatusEffect(status_id, overwrite_time);
    }

    public static void SetOwner(this Actor actor, Actor owner)
    {
        actor.DecOwnerSummonSource();
        actor.data.set(summoned_obj_owner, owner.data.id);
        actor.IncOwnerSummonSource();
        actor.setKingdom(owner.kingdom);
    }

    public static Actor GetOwner(this Actor actor)
    {
        actor.data.get(summoned_obj_owner, out long owner_id);
        return World.world.units.get(owner_id);
    }

    public static List<ShieldData> GetShields(this Actor actor)
    {
        actor.data.get(shield_key, out string raw_shields);
        if (string.IsNullOrEmpty(raw_shields)) return new List<ShieldData>();

        var shields = JsonConvert.DeserializeObject<List<ShieldData>>(raw_shields);
        return shields;
    }

    public static bool HasShield(this Actor actor)
    {
        actor.data.get(shield_key, out string raw_shields);
        return !string.IsNullOrEmpty(raw_shields);
    }

    public static void UpdateShields(this Actor actor, List<ShieldData> shields)
    {
        if (shields.Count == 0)
        {
            actor.data.removeString(shield_key);
            return;
        }

        actor.data.set(shield_key, JsonConvert.SerializeObject(shields));
    }
    [Hotfixable]
    public static void AddShield(this Actor actor, ShieldData shield)
    {
        var shields = actor.GetShields();

        foreach (var trait in actor.traits)
        {
            trait.GetExtend().action_get_shield?.Invoke(actor, ref shield);
        }

        shields.Add(shield);
        actor.UpdateShields(shields);
    }

    public static void AddLakeShield(this Actor actor, float value)
    {
        actor.data.set(lake_shield_buff_key, actor.GetLakeShield() + value);
    }

    public static float GetLakeShield(this Actor actor)
    {
        actor.data.get(lake_shield_buff_key, out float shield);
        return shield;
    }

    public static void SetupSquallyShowers(this Actor actor)
    {
        actor.data.set(squally_showers_key, 0);
    }

    public static int GetSquallyShowersCount(this Actor actor)
    {
        actor.data.get(squally_showers_key, out var count, 20000);
        return count;
    }

    public static void IncSquallyShowersCount(this Actor actor, int count = 1)
    {
        actor.data.set(squally_showers_key, actor.GetSquallyShowersCount() + count);
    }

    public static BaseStats GetOverwriteStats(this Actor actor)
    {
        actor.data.get(overwrite_stats_key, out string raw_stats);
        if (string.IsNullOrEmpty(raw_stats)) return null;

        var stats = GeneralTools.from_json<BaseStats>(raw_stats, true);
        stats.AfterDeserialize();
        return stats;
    }
    public static bool HasOverwriteStats(this Actor actor)
    {
        actor.data.get(overwrite_stats_key, out string raw_stats);
        return !string.IsNullOrEmpty(raw_stats);
    }

    public static void OverwriteStats(this Actor actor, BaseStats stats)
    {
        actor.data.WriteObj(overwrite_stats_key, stats, true);
    }

    public static int GetCultisysLevel(this Actor actor)
    {
        actor.data.get(cultisys_level_key, out int level);
        return level;
    }

    public static float GetExp(this Actor actor)
    {
        actor.data.get(exp_key, out float exp);
        return exp;
    }

    public static void IncExp(this Actor actor, float value)
    {
        actor.data.set(exp_key, actor.GetExp() + value);
    }

    public static void ResetExp(this Actor actor)
    {
        actor.data.set(exp_key, 0);
    }

    public static void ResetTalent(this Actor actor)
    {
        actor.data.set(talent_key, -1);
    }

    public static void ResetCultisysLevel(this Actor actor)
    {
        actor.data.set(cultisys_level_key, 0);
    }

    public static void LevelUp(this Actor actor)
    {
        var level = actor.GetCultisysLevel();
        if (level >= Cultisys.MaxLevel) return;
        level++;
        actor.data.set(cultisys_level_key, level);

        // 自动收藏超凡及以上等级的单位
        // 14级对应'超凡'等级，15级'超凡-二阶'，16级'超凡-圆满'
        // 当单位升级到14级及以上且尚未被收藏时，自动设置为收藏状态
        if (level >= 14 && !actor.data.favorite)
        {
            actor.data.favorite = true; // 设置收藏状态为true
        }

        var trait_list = actor.traits.Select(x=>x.id).ToList();
        switch (level)
        {
        case 2:
          //  actor.addTrait(Traits.yuanhe.id);
            break;
        case 3:
            if (!ModConfigHelper.AllowWarRuleLimitTraitGain())
                actor.addTrait(NextActiveTrait(trait_list));
            break;
        case 5:
            if (!ModConfigHelper.AllowWarRuleLimitTraitGain())
                actor.addTrait(NextActiveTrait(trait_list));
            break;
        case 8:
            if (!ModConfigHelper.AllowWarRuleLimitTraitGain())
                actor.addTrait(NextActiveTrait(trait_list));
            break;
        case 11:
            if (!ModConfigHelper.AllowWarRuleLimitTraitGain())
                actor.addTrait(NextPassiveTrait(trait_list)); //被动2
            break;
        case 12:
            if (!ModConfigHelper.AllowWarRuleLimitTraitGain())
                actor.addTrait(NextActiveTrait(trait_list));
            break;
        case 14:
            if (!ModConfigHelper.AllowWarRuleLimitTraitGain())
                actor.addTrait(NextActiveTrait(trait_list));
            break;
        case 16:
            if (!ModConfigHelper.AllowWarRuleLimitTraitGain())
                actor.addTrait(NextActiveTrait(trait_list));
            break;
        case 17:
            if (!ModConfigHelper.AllowWarRuleLimitTraitGain())
            {
                actor.addTrait(NextActiveTrait(trait_list));
                actor.addTrait(NextPassiveTrait(trait_list));
            }
            break;
        case 19:
            if (!ModConfigHelper.AllowWarRuleLimitTraitGain())
                actor.addTrait(NextActiveTrait(trait_list));
            break; 
        case 20:
            if (!ModConfigHelper.AllowWarRuleLimitTraitGain())
            {
                actor.addTrait(NextActiveTrait(trait_list));
                TraitPairs.Instance.CheckAndAdd(actor, out _);
            }
            break;
        case 21:
            if (!ModConfigHelper.AllowWarRuleLimitTraitGain())
                actor.addTrait(NextActiveTrait(trait_list));
            break;
        case 23:
            if (!ModConfigHelper.AllowWarRuleLimitTraitGain())
            {
                actor.addTrait(NextActiveTrait(trait_list));
                actor.addTrait(NextPassiveTrait(trait_list));
            }
            break;
        case 24:
            if (!ModConfigHelper.AllowWarRuleLimitTraitGain())
                actor.addTrait(NextActiveTrait(trait_list));
            break;
        case 25:
            if (!ModConfigHelper.AllowWarRuleLimitTraitGain())
                actor.addTrait(NextActiveTrait(trait_list));
            break;
        case 26:
            if (!ModConfigHelper.AllowWarRuleLimitTraitGain())
            {
                actor.addTrait(NextPassiveTrait(trait_list));
                actor.addTrait(NextActiveTrait(trait_list)); 
                actor.addTrait(NextActiveTrait(trait_list)); 

                var final_trait = Traits.final_traits.GetRandom();
                actor.addTrait(final_trait);
            }
            break;
        }

        if (level >= 14)
        {
            actor.addTrait("strong_minded", true);
            foreach (var blacklist_trait_id in Cultisys.TraitsBlacklist) actor.removeTrait(blacklist_trait_id);

            foreach (var blacklist_status_id in Cultisys.StatusesBlacklist)
                actor.finishStatusEffect(blacklist_status_id);
        }

        actor.event_full_stats = true;
        actor.setStatsDirty();
    }

    private static string NextTrait(IEnumerable<string> trait_ids)
    {
        var left_traits = Traits.all_enabled_traits.Except(trait_ids);
        var list = left_traits.ToList();
        if (!list.Any()) return "";
        return list.GetRandom();
    }

    private static string NextPassiveTrait(IEnumerable<string> trait_ids)
    {
        var left_traits = Traits.passive_traits.Except(trait_ids);
        var list = left_traits.ToList();
        if (!list.Any()) return "";
        return list.GetRandom();
    }

    private static string NextActiveTrait(IEnumerable<string> trait_ids)
    {
        var left_traits = Traits.active_traits.Except(trait_ids);
        var list = left_traits.ToList();
        if (!list.Any()) return "";
        return list.GetRandom();
    }

    public static float GetTalent(this Actor actor)
    {
        actor.data.get(talent_key, out float talent, -1);
        if (talent < 0)
        {
            if (actor.asset.is_boat)
            {
                talent = 0;
            }
            else
            {
                var v = Mathf.Max(0, (float)normal_rng.Sample());
                talent = 1 /
                         Mathf.Clamp(
                             Mathf.Exp(-(v - mean) * (v - mean) / (2 * stddev * stddev)) /
                             (Mathf.Sqrt(2 * Mathf.PI) * stddev), 1 / max_talent, 1f);
                if (Mathf.Approximately(talent, max_talent))
                {
                    int power = 2;
                    for (; power < 9; power++)
                    {
                        if (Randy.randomChance(0.9f))
                        {
                            break;
                        }
                    }

                    talent = Randy.randomFloat(1, Mathf.Pow(10, power));
                }
                if (!actor.asset.civ) talent /= 10;
            }

            actor.data.set(talent_key, talent);
        }

        return talent;
    }
}