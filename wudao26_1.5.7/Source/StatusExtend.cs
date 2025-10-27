namespace CustomModT001;

public delegate void StatusFinishedDelegate(BaseSimObject obj, StatusAsset status_effect);

public class StatusExtend
{
    public StatusFinishedDelegate       action_finished;
    public ConditionalBaseStatsDelegate conditional_basestats;
    public BaseStats                    final_basestats = new();
}