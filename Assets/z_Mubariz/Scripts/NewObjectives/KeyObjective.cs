public class KeyObjective : ObjectiveBase
{
    protected override void ProgressCompleted()
    {
        StartCoroutine(ObjectiveCompleteCoroutine());
    }

    private void Start()
    {
        PlayerCollisionEvents.OnKeyHit += PlayerCollisionEvents_OnKeyHit;
        NewObjectiveManager.Instance.ChangeAnimatorToWandering();
        NewObjectiveManager.Instance.GrannyWanderingState(true);
    }

    private void PlayerCollisionEvents_OnKeyHit()
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
        PlayerCollisionEvents.OnKeyHit -= PlayerCollisionEvents_OnKeyHit;
    }
}
