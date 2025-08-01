using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FootballObjective : MonoBehaviour
{

    Main_Quest Main_Quest;
    Update_UI Update_UI;
    Items_Count Items_Count;

    public int footballCount;
    [SerializeField] int totalFootBallCount;
    [SerializeField] string objectiveText;
    [SerializeField] string objectiveText2;
    [SerializeField] AudioClip progressClip;
    [SerializeField] AudioClip objectiveComplete;
    [SerializeField] GameObject fadeGameobject;



    public UnityEvent ThingsToActivateOnEnable;
    public UnityEvent ThingsToDeActivateOnDisEnable;

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
    private void OnEnable()
    {
        Invoke(nameof(Enable), 0.3f);
    }

    void Enable()
    {
        ThingsToActivateOnEnable?.Invoke();
        Items_Count.UpdateLevelProgress(footballCount, totalFootBallCount);
        if (AdmobAdsManager.Instance)
        {
            if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
            {
             //   Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_8_Started");
            }
        }
    }

    private void Start()
    {
        EnemyHandler.canAttackCat = true;
        Football.OnBallHitGranny += Football_OnBallHitGranny;
        Update_UI = FindFirstObjectByType<Update_UI>();
        Main_Quest = FindFirstObjectByType<Main_Quest>();
        Items_Count = FindFirstObjectByType<Items_Count>();

        Items_Count.UpdateLevelNumber("Level 8");
        Items_Count.UpdateLevelProgress(footballCount, totalFootBallCount);
        Update_UI.ShowTextUpdate(SelectedText(), 10f);
        Main_Quest.UpdateMainQuest(SelectedText(), footballCount, totalFootBallCount);
    }
    private void OnDestroy()
    {
        Football.OnBallHitGranny -= Football_OnBallHitGranny;
    }
    private void Football_OnBallHitGranny()
    {
        if (footballCount < totalFootBallCount)
        {
            footballCount++;
            SFX_Manager.PlaySound(progressClip);
            Main_Quest.UpdateMainQuest(SelectedText(), footballCount, totalFootBallCount);
            Items_Count.UpdateLevelProgress(footballCount, totalFootBallCount);

            if (footballCount == totalFootBallCount)
            {

                SFX_Manager.PlaySound(objectiveComplete);
                
                // OBJECTIVE COMPLETE

                PlayerPrefs.SetInt("L8", 1);
                
                Items_Count.UpdateLevelProgress(footballCount, totalFootBallCount);
                Main_Quest.UpdateMainQuest(SelectedText(), footballCount, totalFootBallCount);
                Update_UI.ShowTextUpdate("Objective complete", 1f);
                gameObject.SetActive(false);
                EnemyHandler.Instance.ResetState();
                fadeGameobject.SetActive(true);

                if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
                {
                  //  Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_8_Completed");
                }
            }
        }
    }

    private void Update()
    {
        Items_Count.UpdateLevelProgress(footballCount, totalFootBallCount);
        Main_Quest.UpdateMainQuest(objectiveText, footballCount, totalFootBallCount);
    }
   
    private void OnDisable()
    {
        ThingsToDeActivateOnDisEnable?.Invoke();
    }


}
