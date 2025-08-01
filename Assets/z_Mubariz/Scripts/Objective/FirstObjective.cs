using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class FirstObjective : MonoBehaviour
{
    [SerializeField] Main_Quest Main_Quest;
    [SerializeField] Update_UI Update_UI;
    [SerializeField] Items_Count Items_Count;

    [Header("For Cat Positioning")]
    [Space(10)]
    public Transform cat;
    [SerializeField] Transform cameraTrasnform;
    [SerializeField] Transform catJumpCameraTransform;
    public Vector3 targetPositionAfterReach;
    public float targetRotationYAfterReach;
    [Space(10)]
    public int hitToGranny;
    [SerializeField] int totalHitsToGranny;
    [SerializeField] string objectiveText;
    [SerializeField] string objectiveText2;
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
    [SerializeField] PlayableDirector m_PlayableDirector;

    public static event Action OnChangeChannel;

    string selectedText;

    private void OnEnable()
    {
        EnemyHandler.Instance.ResetState();
        Invoke(nameof(Enable), 0.3f);
        if (AdmobAdsManager.Instance)
        {
            if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
            {
              //  Firebase.Analytics.FirebaseAnalytics.LogEvent("Level2Started");
            }
        }
    }
    void Enable()
    {
        batOfGranny.SetActive(false);
        batOfGranny2.SetActive(false);
        Items_Count.UpdateLevelNumber("Level 2");
        Items_Count.UpdateLevelProgress(hitToGranny, totalHitsToGranny);

        Update_UI.ShowTextUpdate(SelectedText(), 10f);

        Main_Quest.UpdateMainQuest(SelectedText(), hitToGranny, totalHitsToGranny);

        Invoke(nameof(ChangeToNewAnimator), 1f);
        wanderingGameobjectGrann.SetActive(false);
        Items_Count.UpdateLevelProgress(hitToGranny, totalHitsToGranny);
    }
    public void ChangeToNewAnimator()
    {
        grannyAnimator.runtimeAnimatorController = newController;
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
        Remote.OnInteract += Remote_OnInteract;

        Items_Count.UpdateLevelNumber("Level 2");
        Items_Count.UpdateLevelProgress(hitToGranny, totalHitsToGranny);

        Update_UI.ShowTextUpdate(SelectedText(), 10f);

        Main_Quest.UpdateMainQuest(SelectedText(), hitToGranny, totalHitsToGranny);
        if (guideGranny != null)
        {
            Destroy(guideGranny);
        }
    }
    private void OnDestroy()
    {
        Remote.OnInteract += Remote_OnInteract;
    }

    private void Update()
    {
        Main_Quest.UpdateMainQuest(SelectedText(), hitToGranny, totalHitsToGranny);
        Items_Count.UpdateLevelProgress(hitToGranny, totalHitsToGranny);
    }

    private void Remote_OnInteract()
    {
        Debug.Log("interacted with remote");
        if (hitToGranny < totalHitsToGranny)
        {

            hitToGranny++;
            SFX_Manager.PlaySound(progressClip);

            //null ref
            Items_Count.UpdateLevelProgress(hitToGranny, totalHitsToGranny);


            Main_Quest.UpdateMainQuest(objectiveText, hitToGranny, totalHitsToGranny);

            grannyAnimator.SetTrigger("NowAnger");

            SFX_Manager.PlaySound(SFX_Manager.Instance.GrannyAngerNewspaper);

            if (hitToGranny == totalHitsToGranny)
            {               
                m_PlayableDirector.gameObject.SetActive(true);
            }
        }
    }

    public void ObjectiveComplete()
    {
        Debug.Log("Mission 2 completed");
        SFX_Manager.PlaySound(objectiveComplete);

        PlayerPrefs.SetInt("L1", 1);

        EnemyHandler.Instance.ResetState();

        fadeGameobject.SetActive(true);

        Items_Count.UpdateLevelProgress(hitToGranny, totalHitsToGranny);
        Main_Quest.UpdateMainQuest(SelectedText(), hitToGranny, totalHitsToGranny);

        Update_UI.ShowTextUpdate("Objective complete", 1f);
        gameObject.SetActive(false);

        
        

        if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
        {
            //Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_2_Completed");
        }
    }

    public void ChangePosAfterReach()
    {
        cat.gameObject.SetActive(false);
        cat.localPosition = targetPositionAfterReach;
        cat.localRotation = Quaternion.Euler(0f, targetRotationYAfterReach, 0f);
        //catJumpCameraTransform.localRotation = Quaternion.Euler(4f, 0f, 0f);
        cameraTrasnform.localRotation = Quaternion.Euler(28f, 0f, 0f);
        cat.gameObject.SetActive(true);
    }

    void wandering()
    {
        wanderingGameobjectGrann.SetActive(false);
    }

    private void OnDisable()
    {
        Invoke(nameof(wandering), 2f);
    }

}
