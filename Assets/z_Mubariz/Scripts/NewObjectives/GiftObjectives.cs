public class GiftObjectives : ObjectiveBase
{
    protected override void ProgressCompleted()
    {
        StartCoroutine(ObjectiveCompleteCoroutine());
    }

    private void Start()
    {
        ChristmasGifts.OnGifthitGranny += ChristmasGifts_OnGifthitGranny;
    }

    private void ChristmasGifts_OnGifthitGranny()
    {
        if (gameObject.activeSelf)
        {
            UpdateProgressCount();
            NewObjectiveManager.Instance.ChangeAnimatorToWandering();
            NewObjectiveManager.Instance.GrannyWanderingState(true);
        }
    }

    protected override void ObjectiveOwnLogic()
    {
        base.ObjectiveOwnLogic();
        NewObjectiveManager.Instance.ChangeAnimatorToWandering();
        NewObjectiveManager.Instance.GrannyWanderingState(true);
    }

    private void OnDestroy()
    {
        ChristmasGifts.OnGifthitGranny -= ChristmasGifts_OnGifthitGranny;
    }
}
