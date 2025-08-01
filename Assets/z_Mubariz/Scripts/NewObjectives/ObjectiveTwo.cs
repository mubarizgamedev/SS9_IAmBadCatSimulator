public class PlateObjective : ObjectiveBase
{

    protected override void ProgressCompleted()
    {
        StartCoroutine(ObjectiveCompleteCoroutine());
    }

    private void Start()
    {
        Breakable.OnPlateBreak += Breakable_OnPlateBreak;
        NewObjectiveManager.Instance.ChangeAnimatorToStandWorking();
    }

    private void OnDestroy()
    {
        Breakable.OnPlateBreak -= Breakable_OnPlateBreak;
    }

    private void Breakable_OnPlateBreak()
    {
        UpdateProgressCount();
        NewObjectiveManager.Instance.GrannyWanderingState(true);
    }
}
