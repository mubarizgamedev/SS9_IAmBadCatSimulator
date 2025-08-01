using UnityEngine;
using UnityEngine.Events;

public class FruitsObjective : MonoBehaviour
{
    Main_Quest Main_Quest;
    Update_UI Update_UI;
    Items_Count Items_Count;

    public int eatablesThrown;
    [SerializeField] int totalEatablesThrown;
    [SerializeField] string objectiveText;
    [SerializeField] string objectiveText2;
    [SerializeField] AudioClip progressClip;
    [SerializeField] AudioClip objectiveComplete;
    [SerializeField] GameObject fadeGameobject;

    public UnityEvent ThingsToActivateOnEnable;
    public UnityEvent thingsToDoWhenComplete;
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
        Items_Count.UpdateLevelProgress(eatablesThrown, totalEatablesThrown);
        if (AdmobAdsManager.Instance)
        {
            if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
            {
                //Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_10_Started");
            }
        }
    }

    private void Start()
    {
        Food.OnFoodHitGranny += PickableObject_OnObjectHitGranny;
        Update_UI = FindFirstObjectByType<Update_UI>();
        Main_Quest = FindFirstObjectByType<Main_Quest>();
        Items_Count = FindFirstObjectByType<Items_Count>();

        Items_Count.UpdateLevelNumber("Level 10");
        Items_Count.UpdateLevelProgress(eatablesThrown, totalEatablesThrown);
        Update_UI.ShowTextUpdate(SelectedText(), 10f);
        Main_Quest.UpdateMainQuest(SelectedText(), eatablesThrown, totalEatablesThrown);
    }
    private void OnDestroy()
    {
        Food.OnFoodHitGranny -= PickableObject_OnObjectHitGranny;
    }
    private void Update()
    {
        Items_Count.UpdateLevelProgress(eatablesThrown, totalEatablesThrown);
        Main_Quest.UpdateMainQuest(SelectedText(), eatablesThrown, totalEatablesThrown);
    }



    private void PickableObject_OnObjectHitGranny()
    {
        if (eatablesThrown < totalEatablesThrown)
        {
            eatablesThrown++;
            SFX_Manager.PlaySound(progressClip);
            Main_Quest.UpdateMainQuest(SelectedText(), eatablesThrown, totalEatablesThrown);
            Items_Count.UpdateLevelProgress(eatablesThrown, totalEatablesThrown);

            if (eatablesThrown == totalEatablesThrown)
            {

                SFX_Manager.PlaySound(objectiveComplete);
          
                // OBJECTIVE COMPLETE

                PlayerPrefs.SetInt("L10", 1);
                
                Items_Count.UpdateLevelProgress(eatablesThrown, totalEatablesThrown);
                Main_Quest.UpdateMainQuest(SelectedText(), eatablesThrown, totalEatablesThrown);
                Update_UI.ShowTextUpdate("Objective complete", 1f);
                gameObject.SetActive(false);
                EnemyHandler.Instance.ResetState();
                fadeGameobject.SetActive(true);

                if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
                {
                   // Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_10_Completed");
                }
            }
        }
    }
}
