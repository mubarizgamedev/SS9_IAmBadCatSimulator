using UnityEngine;
using UnityEngine.Events;

public class ToysObjective : MonoBehaviour
{
    Main_Quest Main_Quest;
    Update_UI Update_UI;
    Items_Count Items_Count;

    public int toysThrown;
    [SerializeField] int totalToysThrown;
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
        Items_Count.UpdateLevelProgress(toysThrown, totalToysThrown);
        if (AdmobAdsManager.Instance)
        {
            if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
            {
              //  Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_7_Started");
            }
        }
    }

    private void Start()
    {
        Toy.OnToyHitGranny += PickableObject_OnObjectHitGranny;
        Update_UI = FindFirstObjectByType<Update_UI>();
        Main_Quest = FindFirstObjectByType<Main_Quest>();
        Items_Count = FindFirstObjectByType<Items_Count>();

        Items_Count.UpdateLevelNumber("Level 7");
        Items_Count.UpdateLevelProgress(toysThrown, totalToysThrown);
        Update_UI.ShowTextUpdate(SelectedText(), 10f);
        Main_Quest.UpdateMainQuest(SelectedText(), toysThrown, totalToysThrown);
    }
    private void OnDestroy()
    {
        Toy.OnToyHitGranny -= PickableObject_OnObjectHitGranny;
    }
    private void Update()
    {
        Items_Count.UpdateLevelProgress(toysThrown, totalToysThrown);
        Main_Quest.UpdateMainQuest(SelectedText(), toysThrown, totalToysThrown);
    }



    private void PickableObject_OnObjectHitGranny()
    {
        if (toysThrown < totalToysThrown)
        {
            toysThrown++;
            SFX_Manager.PlaySound(progressClip);
            Main_Quest.UpdateMainQuest(SelectedText(), toysThrown, totalToysThrown);
            Items_Count.UpdateLevelProgress(toysThrown, totalToysThrown);

            if (toysThrown == totalToysThrown)
            {

                SFX_Manager.PlaySound(objectiveComplete);


                // OBJECTIVE COMPLETE

                PlayerPrefs.SetInt("L7", 1);
                
                Items_Count.UpdateLevelProgress(toysThrown, totalToysThrown);
                Main_Quest.UpdateMainQuest(SelectedText(), toysThrown, totalToysThrown);
                Update_UI.ShowTextUpdate("Objective complete", 1f);
                gameObject.SetActive(false);
                EnemyHandler.Instance.ResetState();
                fadeGameobject.SetActive(true);

                if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
                {
                  //  Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_7_Completed");
                }
            }
        }
    }
}
