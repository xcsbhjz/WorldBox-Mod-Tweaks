using System.Collections.Generic;
using System.Linq;

namespace CustomModT001;

public class TraitPairs
{
    public static TraitPairs Instance { get; private set; }
    private List<TraitPair> pairs = new();
    internal TraitPairs()
    {
        Instance = this;
    }
    
    public void Init()
    {
        New([Traits.emperor_heart] , [Traits.demon_armor, Traits.demon_crown, Traits.demon_sword, Traits.king_armor, Traits.king_crown, Traits.king_sword
        ]);
        
        
        New([Traits.loong_heart], [Traits.mountain_loong_soul, Traits.ocean_loong_soul, Traits.purgatory_loong_soul, Traits.skies_loong_soul, Traits.undead_war_god], with_inversed: false);
        New([Traits.mountain_loong_soul, Traits.ocean_loong_soul, Traits.purgatory_loong_soul, Traits.skies_loong_soul, Traits.undead_war_god], [Traits.loong_heart], with_inversed: false);
        
        
        New([Traits.stacked_horned_dragon], [Traits.extraordinary_evil, Traits.unbreakable_grip, Traits.feast, Traits.unbreakable_life, Traits.longer_then_stronger, Traits.unmovable_like_mountain, Traits.bastard, Traits.survivor_contract, Traits.meat_eater]);
        
        
        New([Traits.lake_shield], [Traits.mountain_loong_soul, Traits.intentional_punching, Traits.shield_attack, Traits.shield_protect, Traits.super_shield_protect], 1);

        New([Traits.empty_feathered, Traits.servant_master, Traits.houseback, Traits.unrepentant_refactoring],
        [
            Traits.summoning_dead, Traits.explosive_dawn, Traits.summoner, Traits.puppetry_master,
            Traits.mysterious_mage, Traits.snow_struggling
        ]);
        
        New([Traits.angel_tear], [Traits.ocean_loong_soul, Traits.soar_mind, Traits.star_mind], 1);

        New([Traits.end_of_time], [Traits.doppelganger, Traits.self_destruct]);
        
        New([Traits.entropy_power], [Traits.frozen_tomb, Traits.eternal_confine, Traits.staggering_blow, Traits.explosive_dawn]);
        
        New([Traits.heart_defence, Traits.weakness_analysis], [Traits.critor, Traits.vital_crit]);
        
        New([Traits.like_star], [Traits.dance_attack, Traits.sword_in_dance, Traits.run_man, Traits.runningman]);
        
        New([Traits.steadfastness], [Traits.indestructibility, Traits.destined, Traits.countless_trials, Traits.stable, Traits.stable_like_mountain], 1, false);
        New( [Traits.indestructibility, Traits.countless_trials, Traits.stable, Traits.stable_like_mountain], [Traits.steadfastness, Traits.destined], with_inversed: false);
    }

    private void New(ActorTrait[] source, ActorTrait[] target, int random_start = 0, bool with_inversed = true)
    {
        pairs.Add(new TraitPair(source, target, random_start));
        if (with_inversed)
            pairs.Add(new TraitPair(target, source, random_start));
    }

    public void CheckAndAdd(Actor actor, out string trait)
    {
        foreach (var pair in pairs)
        {
            if (pair.Match(actor, out trait)) return;
        }

        actor.addTrait(Traits.diligent.id);
        trait = Traits.diligent.id;
    }

    class TraitPair
    {
        public string[] Source;
        public string[] Target;
        public int RandomStart;

        public bool Match(Actor actor, out string trait)
        {
            trait = null;
            if (!Source.Any(actor.hasTrait)) return false;

            for (int i = 0; i < RandomStart; i++)
            {
                if (!actor.hasTrait(Target[i]))
                {
                    trait = Target[i];
                    actor.addTrait(Target[i]);
                    return true;
                }
            }
            
            int count = RandomStart;
            while (count < Target.Length)
            {
                var rd_idx = Randy.randomInt(count, Target.Length);
                var next = Target[rd_idx];
                if (!actor.hasTrait(next))
                {
                    trait = next;
                    actor.addTrait(next);
                    return true;
                }
                Target.Swap(count, rd_idx);
                count++;
            }
            return false;


        }
        public TraitPair(ActorTrait[] source, ActorTrait[] target, int random_start = 0)
        {
            Source = source.Select(x => x.id).ToArray();
            Target = target.Select(x => x.id).ToArray();
            RandomStart = random_start;
        }
    }
}