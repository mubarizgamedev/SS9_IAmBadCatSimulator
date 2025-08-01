
public class ToysThrowObjective : ObjectiveBase
{
    protected override void ProgressCompleted()
    {
        StartCoroutine(ObjectiveCompleteCoroutine());
    }

    private void Start()
    {
        Toy.OnToyHitGranny += Toy_OnToyHitGranny;
    }

    private void OnDestroy()
    {
        Toy.OnToyHitGranny -= Toy_OnToyHitGranny;
    }

    private void Toy_OnToyHitGranny()
    {
        if (gameObject.activeSelf)
        {
            UpdateProgressCount();
            NewObjectiveManager.Instance.ChangeAnimatorToWandering();
            NewObjectiveManager.Instance.GrannyWanderingState(true);
        }
    }
}
