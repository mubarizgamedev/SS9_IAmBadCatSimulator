using UnityEngine;
using UnityEngine.Events;

public class BreakObjective : MonoBehaviour
{
    Main_Quest Main_Quest;
    Update_UI Update_UI;
    Items_Count Items_Count;

    public int objectsBreak;
    [SerializeField] int totalObjectsBreak;
    [SerializeField] string objectiveText;
    [SerializeField] string objectiveText2;
    [SerializeField] AudioClip progressClip;
    [SerializeField] AudioClip objectiveComplete;
    [SerializeField] GameObject fadeGameobject;


    public UnityEvent ThingsToActivateOnEnable;


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
        Items_Count.UpdateLevelProgress(objectsBreak, totalObjectsBreak);
        if (AdmobAdsManager.Instance)
        {
            if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
            {
              //  Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_3_Started");
            }
        }
    }

    private void Start()
    {
        Breakable.OnBreakObject += PickableObject_OnObjectHitGranny;
        Update_UI = FindFirstObjectByType<Update_UI>();
        Main_Quest = FindFirstObjectByType<Main_Quest>();
        Items_Count = FindFirstObjectByType<Items_Count>();

        Items_Count.UpdateLevelNumber("Level 3");
        Items_Count.UpdateLevelProgress(objectsBreak, totalObjectsBreak);
        Update_UI.ShowTextUpdate(SelectedText(), 10f);
        Main_Quest.UpdateMainQuest(SelectedText(), objectsBreak, totalObjectsBreak);
    }
    private void OnDestroy()
    {
        Breakable.OnBreakObject -= PickableObject_OnObjectHitGranny;
    }
    private void Update()
    {
        Items_Count.UpdateLevelProgress(objectsBreak, totalObjectsBreak);
        Main_Quest.UpdateMainQuest(SelectedText(), objectsBreak, totalObjectsBreak);
    }



    private void PickableObject_OnObjectHitGranny()
    {
        if (objectsBreak < totalObjectsBreak)
        {
            objectsBreak++;
            SFX_Manager.PlaySound(progressClip);
            Main_Quest.UpdateMainQuest(SelectedText(), objectsBreak, totalObjectsBreak);
            Items_Count.UpdateLevelProgress(objectsBreak, totalObjectsBreak);

            if (objectsBreak == totalObjectsBreak)
            {

                SFX_Manager.PlaySound(objectiveComplete);
                /////////////////////////////////// 
                /// 
                ///       CAN CALL AD HERE
                /// 
                ///////////////////////////////////




                // OBJECTIVE COMPLETE

                PlayerPrefs.SetInt("L3", 1);
                
                Items_Count.UpdateLevelProgress(objectsBreak, totalObjectsBreak);
                Main_Quest.UpdateMainQuest(SelectedText(), objectsBreak, totalObjectsBreak);
                Update_UI.ShowTextUpdate("Objective complete", 1f);
                gameObject.SetActive(false);
                EnemyHandler.Instance.ResetState();
                fadeGameobject.SetActive(true);

                if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
                {
                   // Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_3_maxCompleted");
                }
            }
        }
    }

    
}
