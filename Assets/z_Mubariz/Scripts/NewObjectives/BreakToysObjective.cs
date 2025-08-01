public class BreakToysObjective : ObjectiveBase
{
    protected override void ProgressCompleted()
    {
        StartCoroutine(ObjectiveCompleteCoroutine());
    }

    private void Start()
    {
        Breakable.OnToyBreak += Breakable_OnToyBreak;
    }

    private void Breakable_OnToyBreak()
    {
        if (gameObject.activeSelf)
        {
            UpdateProgressCount();
        }
    }

    private void OnDestroy()
    {
        Breakable.OnToyBreak -= Breakable_OnToyBreak;
    }

    protected override void ObjectiveOwnLogic()
    {
        base.ObjectiveOwnLogic();
        NewObjectiveManager.Instance.ChangeAnimatorToWandering();
        NewObjectiveManager.Instance.GrannyWanderingState(true);
    }

}
