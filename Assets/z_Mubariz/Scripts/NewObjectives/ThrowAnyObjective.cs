using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAnyObjective : ObjectiveBase
{
    protected override void ProgressCompleted()
    {
        StartCoroutine(ObjectiveCompleteCoroutine());
    }

    private void Start()
    {
        PickableObject.OnObjectHitGranny += PickableObject_OnObjectHitGranny;
       
    }

    private void PickableObject_OnObjectHitGranny()
    {
        if (gameObject.activeSelf)
        {
            UpdateProgressCount();
        }
    }

    

    private void OnDestroy()
    {
        PickableObject.OnObjectHitGranny -= PickableObject_OnObjectHitGranny;
    }

    protected override void ObjectiveOwnLogic()
    {
        base.ObjectiveOwnLogic();
        NewObjectiveManager.Instance.ChangeAnimatorToWandering();
        NewObjectiveManager.Instance.GrannyWanderingState(true);
    }
}
