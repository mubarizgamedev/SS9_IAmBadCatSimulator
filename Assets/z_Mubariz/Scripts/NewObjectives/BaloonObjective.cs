using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonObjective : ObjectiveBase
{
    protected override void ProgressCompleted()
    {
        StartCoroutine(ObjectiveCompleteCoroutine());
    }


    private void Start()
    {
        BallonsBehaviour.OnBalloonPopped += BallonsBehaviour_OnBalloonPopped;
        NewObjectiveManager.Instance.ChangeAnimatorToWandering();
        NewObjectiveManager.Instance.GrannyWanderingState(true);
    }

    private void BallonsBehaviour_OnBalloonPopped()
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
        BallonsBehaviour.OnBalloonPopped -= BallonsBehaviour_OnBalloonPopped;
    }

    protected override void ObjectiveOwnLogic()
    {
        base.ObjectiveOwnLogic();
        NewObjectiveManager.Instance.GrannyWanderingState(true);
    }
}
