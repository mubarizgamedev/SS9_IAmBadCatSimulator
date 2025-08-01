
public class BreakObjectives : ObjectiveBase
{
    protected override void ProgressCompleted()
    {
        StartCoroutine(ObjectiveCompleteCoroutine());
    }

    private void Start()
    {
        Breakable.OnBreakObject += Breakable_OnBreakObject;
    }
    private void OnDestroy()
    {
        Breakable.OnBreakObject -= Breakable_OnBreakObject;
    }

    protected override void ObjectiveOwnLogic()
    {
        base.ObjectiveOwnLogic();
        NewObjectiveManager.Instance.ChangeAnimatorToWandering();
        NewObjectiveManager.Instance.GrannyWanderingState(true);
    }
    private void Breakable_OnBreakObject()
    {
        UpdateProgressCount();
    }
}
