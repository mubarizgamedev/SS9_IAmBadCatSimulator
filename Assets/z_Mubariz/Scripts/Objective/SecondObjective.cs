using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SecondObjective : MonoBehaviour
{
    [SerializeField]Main_Quest Main_Quest;
    [SerializeField]Update_UI Update_UI;
    [SerializeField] Items_Count Items_Count;

    public Vector3 targetPos;

    [Space(10)]
    public List<Vector3> grannyWanderWaypoints = new();

    public int objectsToCollect;
    [SerializeField] int totalObjectsToCollect;
    [SerializeField] string objectiveText;
    [SerializeField] string objectiveText2;
    [SerializeField] float delayAfterObjectiveComp = 2f;
    [SerializeField] AudioClip progressClip;
    [SerializeField] AudioClip objectiveComplete;
    [SerializeField] GameObject fadeGameobject;
    [SerializeField] GameObject guideGranny;
    [SerializeField] Animator grannyAnimator;
    [SerializeField] RuntimeAnimatorController oldController;
    [SerializeField] RuntimeAnimatorController newController;
    [SerializeField] GameObject wanderingGameobjectGrann;
    [SerializeField] GameObject batOfGranny;
    [SerializeField] GameObject batOfGranny2;
    public Transform GrannyTransform;



    public UnityEvent ThingsToActivateOnEnable;
    public UnityEvent ThingsToDeActivateOnDisable;



    private void OnEnable()
    {
        //UpdateGrannyWayPoints();
        Invoke(nameof(SetPos), 2f);
        Invoke(nameof(Enable), 0.3f);
        Invoke(nameof(GrannyWanderingFalse), 0f);
        Invoke(nameof(UpdateGrannyWayPoints), 1f);
    }
    void GrannyWanderingFalse()
    {
        wanderingGameobjectGrann.SetActive(false);
    }
    [ContextMenu("remove all")]
    void UpdateGrannyWayPoints()
    {
        EnemyWandering enemyWandering = wanderingGameobjectGrann.GetComponent<EnemyWandering>();
        enemyWandering.UpdateWayPoints(grannyWanderWaypoints);
        Debug.Log("Granny Waypoints Updated");
    }
    void Enable()
    {
        batOfGranny.SetActive(false);
        batOfGranny2.SetActive(false);
        Items_Count.UpdateLevelProgress(objectsToCollect, totalObjectsToCollect);
        Update_UI.ShowTextUpdate(SelectedText(), 10f);
        Main_Quest.UpdateMainQuest(SelectedText(), objectsToCollect, totalObjectsToCollect);

        ChangeAnimatorToKitchen();
        ThingsToActivateOnEnable?.Invoke();
        Items_Count.UpdateLevelProgress(objectsToCollect, totalObjectsToCollect);
        if (AdmobAdsManager.Instance)
        {
            if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
            {
              //  Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_3_Started");
            }
        }
    }
    string SelectedText()
    {
        if (SelectedEnemyCheck.Instance.IsGrannySelected())
        {
            return objectiveText;
        }
        else
        {
            return objectiveText2;
        }
    }

    private void Start()
    {
        EnemyHandler.canAttackCat = true;
        
        PickableObject.KitcenObjectBroken += PickableObject_KitcenObjectBroken;

        Items_Count.UpdateLevelNumber("Level 3");
        
    }

    

    private void OnDestroy()
    {
        PickableObject.KitcenObjectBroken -= PickableObject_KitcenObjectBroken;
    }
    private void Update()
    {
        Items_Count.UpdateLevelProgress(objectsToCollect, totalObjectsToCollect);
        Main_Quest.UpdateMainQuest(SelectedText(), objectsToCollect, totalObjectsToCollect);
    }

    
    private void PickableObject_KitcenObjectBroken()
    {
        if (objectsToCollect < totalObjectsToCollect)
        {
            objectsToCollect++;
            SFX_Manager.PlaySound(progressClip);
            Main_Quest.UpdateMainQuest(SelectedText(), objectsToCollect, totalObjectsToCollect);
            Items_Count.UpdateLevelProgress(objectsToCollect, totalObjectsToCollect);

            EnemyHandler.Instance.TriggerAngerGranny();

            if (objectsToCollect == totalObjectsToCollect)
            {
                Invoke(nameof(ObjectiveComplete), delayAfterObjectiveComp);
            }
        }
    }

    void ObjectiveComplete()
    {
        SFX_Manager.PlaySound(objectiveComplete);

        // OBJECTIVE COMPLETE

        PlayerPrefs.SetInt("L2", 1);

        Items_Count.UpdateLevelProgress(objectsToCollect, totalObjectsToCollect);
        Main_Quest.UpdateMainQuest(SelectedText(), objectsToCollect, totalObjectsToCollect);
        Update_UI.ShowTextUpdate("Objective complete", 1f);

        gameObject.SetActive(false);

        EnemyHandler.Instance.ResetState();
        fadeGameobject.SetActive(true);


        if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
        {
          //  Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_3_Completed");
        }
    }


    private void OnDisable()
    {
        ThingsToDeActivateOnDisable?.Invoke();
        wanderingGameobjectGrann.SetActive(false);
    }

    public void ChangeAnimatorToKitchen()
    {
        grannyAnimator.runtimeAnimatorController = newController;
    }

    void SetPos()
    {
        GrannyTransform.localPosition = targetPos;
        GrannyTransform.localRotation = Quaternion.Euler(0, -83.841f, 0);
        wanderingGameobjectGrann.SetActive(false);
    }
}
