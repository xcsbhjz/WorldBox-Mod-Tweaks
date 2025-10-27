using ai.behaviours;
using CustomModT001.Abstract;

namespace CustomModT001;

public class ActorTasks : ExtendLibrary<BehaviourTaskActor, ActorTasks>
{
    public static BehaviourTaskActor follow_owner { get; private set; }

    protected override void OnInit()
    {
        RegisterAssets(ModClass.asset_id_prefix);

        follow_owner.addBeh(new BehSummonedLookForOwner());
        follow_owner.addBeh(new BehGoToActorTarget(GoToActorTargetType.SameRegion, true));
        follow_owner.addBeh(new BehRandomWait(3f, 6f));
    }

    private class BehSummonedLookForOwner : BehaviourActionActor
    {
        public override BehResult execute(Actor pObject)
        {
            Actor owner = pObject.GetOwner();
            if (owner == null)
            {
                return BehResult.Stop;
            }

            pObject.beh_actor_target = owner;
            return BehResult.Continue;
        }
    }
}