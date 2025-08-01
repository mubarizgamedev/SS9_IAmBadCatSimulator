public class SleepObjective : ObjectiveBase
{
    protected override void ProgressCompleted()
    {
        StartCoroutine(ObjectiveCompleteCoroutine());
    }

    private void Start()
    {
        SleepTrigger.OnCatSleep += SleepTrigger_OnCatSleep;
        NewObjectiveManager.Instance.ChangeAnimatorToWandering();
        NewObjectiveManager.Instance.GrannyWanderingState(true);
    }

    private void SleepTrigger_OnCatSleep()
    {
        UpdateProgressCount();
    }

    private void OnDestroy()
    {
        SleepTrigger.OnCatSleep -= SleepTrigger_OnCatSleep;
    }

}
