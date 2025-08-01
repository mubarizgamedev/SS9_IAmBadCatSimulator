public class DiamondObjective : ObjectiveBase
{

    protected override void ProgressCompleted()
    {
        StartCoroutine(ObjectiveCompleteCoroutine());
    }

    private void Start()
    {
        PlayerCollisionEvents.OnDiamondHit += PlayerCollisionEvents_OnDiamondHit;
    }

    private void PlayerCollisionEvents_OnDiamondHit()
    {
        if (gameObject.activeSelf)
        {
            UpdateProgressCount();
            NewObjectiveManager.Instance.GrannyWanderingState(true);
        }
    }

    private void OnDestroy()
    {
        PlayerCollisionEvents.OnDiamondHit -= PlayerCollisionEvents_OnDiamondHit;
    }

    protected override void ObjectiveOwnLogic()
    {
        base.ObjectiveOwnLogic();
        NewObjectiveManager.Instance.ChangeAnimatorToWandering();
        NewObjectiveManager.Instance.GrannyWanderingState(true);
    }
}
