using UnityEngine;
using UnityEngine.Events;

public class KeysObjective: MonoBehaviour
{
    Main_Quest Main_Quest;
    [SerializeField] Update_UI Update_UI;
    Items_Count Items_Count;

    public int keysCount;
    [SerializeField] int toatlKeysToCollect;
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
        Items_Count.UpdateLevelProgress(keysCount, toatlKeysToCollect);
        if (AdmobAdsManager.Instance)
        {
            if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
            {
              //  Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_5_Started");
            }
        }
    }

    private void Start()
    {
        EnemyHandler.canAttackCat = true;
        PlayerCollisionEvents.OnKeyHit += PlayerCollisionEvents_OnKeyHit;
        Update_UI = FindFirstObjectByType<Update_UI>();
        Main_Quest = FindFirstObjectByType<Main_Quest>();
        Items_Count = FindFirstObjectByType<Items_Count>();

        Items_Count.UpdateLevelNumber("Level 5");
        Items_Count.UpdateLevelProgress(keysCount, toatlKeysToCollect);
        Update_UI.ShowTextUpdate(SelectedText(), 10f);
        Main_Quest.UpdateMainQuest(SelectedText(), keysCount, toatlKeysToCollect);
    }

    private void OnDestroy()
    {
        PlayerCollisionEvents.OnKeyHit -= PlayerCollisionEvents_OnKeyHit;
    }
    private void PlayerCollisionEvents_OnKeyHit()
    {
        if (keysCount < toatlKeysToCollect)
        {
            keysCount++;
            SFX_Manager.PlaySound(progressClip);
            Main_Quest.UpdateMainQuest(SelectedText(), keysCount, toatlKeysToCollect);
            Items_Count.UpdateLevelProgress(keysCount, toatlKeysToCollect);

            if (keysCount == toatlKeysToCollect)
            {

                SFX_Manager.PlaySound(objectiveComplete);
                
                // OBJECTIVE COMPLETE

                PlayerPrefs.SetInt("L5", 1);
                
                Items_Count.UpdateLevelProgress(keysCount, toatlKeysToCollect);
                Main_Quest.UpdateMainQuest(SelectedText(), keysCount, toatlKeysToCollect);
                Update_UI.ShowTextUpdate("Objective complete", 1f);
                gameObject.SetActive(false);
                EnemyHandler.Instance.ResetState();
                fadeGameobject.SetActive(true);

                if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
                {
                   // Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_5_Completed");
                }
            }
        }
    }

    private void Update()
    {
        Items_Count.UpdateLevelProgress(keysCount, toatlKeysToCollect);
        Main_Quest.UpdateMainQuest(objectiveText, keysCount, toatlKeysToCollect);
    }

    private void OnDisable()
    {
        ThingsToDeActivateOnDisEnable?.Invoke();
    }

  

}
