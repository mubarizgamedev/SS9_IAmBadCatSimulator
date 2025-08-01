public class TvObjective : ObjectiveBase
{
    protected override void ProgressCompleted()
    {
        StartCoroutine(ObjectiveCompleteCoroutine());
    }

    private void Start()
    {
        Remote.OnInteract += Remote_OnInteract;
    }

    private void Remote_OnInteract()
    {
        if (gameObject.activeSelf)
        {
            UpdateProgressCount();
            NewObjectiveManager.Instance.ChangeAnimatorToWandering();
            NewObjectiveManager.Instance.GrannyWanderingState(true);
        }
    }

    private void OnDestroy()
    {
        Remote.OnInteract += Remote_OnInteract;
    }
    protected override void ObjectiveOwnLogic()
    {
        base.ObjectiveOwnLogic();
    }
}
