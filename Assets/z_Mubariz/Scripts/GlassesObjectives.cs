using UnityEngine;
using UnityEngine.Events;

public class GlassesObjectives : MonoBehaviour
{
    Main_Quest Main_Quest;
    Update_UI Update_UI;
    Items_Count Items_Count;

    public int glassBroken;
    [SerializeField] int totalGlassToBreak;
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
        Items_Count.UpdateLevelProgress(glassBroken, totalGlassToBreak);
        if (AdmobAdsManager.Instance)
        {
            if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
            {
              //  Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_9_Started");
            }
        }
    }

    private void Start()
    {
        Breakable.OnBreakGlass += Glass_OnGlassHitGranny;
        Update_UI = FindFirstObjectByType<Update_UI>();
        Main_Quest = FindFirstObjectByType<Main_Quest>();
        Items_Count = FindFirstObjectByType<Items_Count>();

        Items_Count.UpdateLevelNumber("Level 9");
        Items_Count.UpdateLevelProgress(glassBroken, totalGlassToBreak);
        Update_UI.ShowTextUpdate(SelectedText(), 10f);
        Main_Quest.UpdateMainQuest(SelectedText(), glassBroken, totalGlassToBreak);
    }

    private void OnDestroy()
    {
        Breakable.OnBreakGlass -= Glass_OnGlassHitGranny;
    }

    private void Glass_OnGlassHitGranny()
    {
        if (glassBroken < totalGlassToBreak)
        {
            glassBroken++;
            SFX_Manager.PlaySound(progressClip);
            Main_Quest.UpdateMainQuest(SelectedText(), glassBroken, totalGlassToBreak);
            Items_Count.UpdateLevelProgress(glassBroken, totalGlassToBreak);

            if (glassBroken == totalGlassToBreak)
            {

                SFX_Manager.PlaySound(objectiveComplete);


                // OBJECTIVE COMPLETE

                PlayerPrefs.SetInt("L9", 1);
                
                Items_Count.UpdateLevelProgress(glassBroken, totalGlassToBreak);
                Main_Quest.UpdateMainQuest(SelectedText(), glassBroken, totalGlassToBreak);
                Update_UI.ShowTextUpdate("Objective complete", 1f);
                gameObject.SetActive(false);
                EnemyHandler.Instance.ResetState();
                fadeGameobject.SetActive(true);

                if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
                {
                   // Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_9_Completed");
                }
            }
        }
    }

    private void Update()
    {
        Items_Count.UpdateLevelProgress(glassBroken, totalGlassToBreak);
        Main_Quest.UpdateMainQuest(objectiveText, glassBroken, totalGlassToBreak);
    }
}
