using CustomModT001.Abstract;

namespace CustomModT001;

public class ActorJobs : ExtendLibrary<ActorJob, ActorJobs>
{
    public static ActorJob summoned_job { get; private set; }

    protected override void OnInit()
    {
        RegisterAssets(ModClass.asset_id_prefix);

        summoned_job.addTask(ActorTasks.follow_owner.id);
    }
}