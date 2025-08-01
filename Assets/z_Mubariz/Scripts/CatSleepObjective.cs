using UnityEngine;
using UnityEngine.Events;

public class CatSleepObjective : MonoBehaviour
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
    [SerializeField] GameObject catSleeGameobject;


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
        ThingsToActivateOnEnable?.Invoke();
        Invoke(nameof(Enable), 0.3f);
    }

    void Enable()
    {
        Items_Count.UpdateLevelProgress(objectsBreak, totalObjectsBreak);
        if (AdmobAdsManager.Instance)
        {
            if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
            {
              //  Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_6_Started");
            }
        }
    }

    private void Start()
    {
        PlayerCollisionEvents.OnCatHitBed += PlayerCollisionEvents_OnCatHitBed;
        Update_UI = FindFirstObjectByType<Update_UI>();
        Main_Quest = FindFirstObjectByType<Main_Quest>();
        Items_Count = FindFirstObjectByType<Items_Count>();

        Items_Count.UpdateLevelNumber("Level 6");
        Items_Count.UpdateLevelProgress(objectsBreak, totalObjectsBreak);
        Update_UI.ShowTextUpdate(SelectedText(), 10f);
        Main_Quest.UpdateMainQuest(SelectedText(), objectsBreak, totalObjectsBreak);
    }

    private void OnDestroy()
    {
        PlayerCollisionEvents.OnCatHitBed -= PlayerCollisionEvents_OnCatHitBed;
    }

    private void Update()
    {
        Items_Count.UpdateLevelProgress(objectsBreak, totalObjectsBreak);
        Main_Quest.UpdateMainQuest(SelectedText(), objectsBreak, totalObjectsBreak);
    }

    private void PlayerCollisionEvents_OnCatHitBed()
    {
        SFX_Manager.PlaySound(objectiveComplete);


        // OBJECTIVE COMPLETE

        PlayerPrefs.SetInt("L6", 1);
       
        Items_Count.UpdateLevelProgress(objectsBreak, totalObjectsBreak);
        Main_Quest.UpdateMainQuest(SelectedText(), objectsBreak, totalObjectsBreak);
        Update_UI.ShowTextUpdate("Objective complete", 1f);
        gameObject.SetActive(false);
        EnemyHandler.Instance.ResetState();
        catSleeGameobject.SetActive(true);
        fadeGameobject.SetActive(true);
        if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
        {
           // Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_6_Completed");
        }
    }


    private void OnDisable()
    {
        ThingsToDeActivateOnDisEnable?.Invoke();
    }

}
