public class ObjThrowObjective : ObjectiveBase
{
    private void Start()
    {
        EnemyHandler.canAttackCat = true;
        PickableObject.OnGuideObjectHitGranny += PickableObject_OnGuideObjectHitGranny;
    }

    private void PickableObject_OnGuideObjectHitGranny()
    {
        if (gameObject.activeSelf)
        {
            UpdateProgressCount();
            NewObjectiveManager.Instance.GrannyWanderingState(true);
        }
       
    }

    private void OnDestroy()
    {
        PickableObject.OnGuideObjectHitGranny -= PickableObject_OnGuideObjectHitGranny;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        NewObjectiveManager.Instance.ChangeAnimatorToWandering();
    }
    protected override void ObjectiveOwnLogic()
    {
        base.ObjectiveOwnLogic();
        NewObjectiveManager.Instance.ChangeAnimatorToWandering();
        NewObjectiveManager.Instance.GrannyWanderingState(true);
    }



    protected override void ProgressCompleted()
    {
        StartCoroutine(ObjectiveCompleteCoroutine());
    }
}
