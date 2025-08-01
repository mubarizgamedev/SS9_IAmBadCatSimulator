using UnityEngine;

public class KeyQuestObjective : ObjectiveBase
{
    protected override void ProgressCompleted()
    {
        StartCoroutine(ObjectiveCompleteCoroutine());
    }
    public GameObject questBoxToEnable;

    private void Start()
    {
        PlayerCollisionEvents.OnCatHitObjKey += PlayerCollisionEvents_OnCatHitObjKey;
        QuestTrigger.OnCatQuest+= PlayerCollisionEvents_OnCatHitObjKey;
        NewObjectiveManager.Instance.ChangeAnimatorToWandering();
        NewObjectiveManager.Instance.GrannyWanderingState(true);
    }

    private void PlayerCollisionEvents_OnCatHitObjKey()
    {
        UpdateProgressCount();
        questBoxToEnable.SetActive(true);
    }

    private void OnDestroy()
    {
        PlayerCollisionEvents.OnCatHitObjKey -= PlayerCollisionEvents_OnCatHitObjKey;
        QuestTrigger.OnCatQuest -= PlayerCollisionEvents_OnCatHitObjKey;
    }
}
