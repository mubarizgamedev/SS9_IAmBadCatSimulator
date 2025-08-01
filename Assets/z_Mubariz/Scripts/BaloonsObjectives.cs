using UnityEngine;
using UnityEngine.Events;

public class BaloonsObjectives : MonoBehaviour
{
    Main_Quest Main_Quest;
    Update_UI Update_UI;
    Items_Count Items_Count;

    public int BaloonsPoped;
    [SerializeField] int totalBaloonsPoped;
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
        Items_Count.UpdateLevelProgress(BaloonsPoped, totalBaloonsPoped);
        if (AdmobAdsManager.Instance)
        {
            if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
            {
               // Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_4_Started");
            }
        }
    }

    private void Start()
    {
        EnemyHandler.canAttackCat = true;
        BallonsBehaviour.OnBalloonPopped += PlayerCollisionEvents_OnPlayerCollideWithKey;
        Update_UI = FindFirstObjectByType<Update_UI>();
        Main_Quest = FindFirstObjectByType<Main_Quest>();
        Items_Count = FindFirstObjectByType<Items_Count>();

        Items_Count.UpdateLevelNumber("Level 4");
        Items_Count.UpdateLevelProgress(BaloonsPoped, totalBaloonsPoped);
        Update_UI.ShowTextUpdate(SelectedText(), 10f);
        Main_Quest.UpdateMainQuest(SelectedText(), BaloonsPoped, totalBaloonsPoped);
    }
    private void OnDestroy()
    {
        BallonsBehaviour.OnBalloonPopped -= PlayerCollisionEvents_OnPlayerCollideWithKey;
    }
    private void Update()
    {
        Items_Count.UpdateLevelProgress(BaloonsPoped, totalBaloonsPoped);
        Main_Quest.UpdateMainQuest(SelectedText(), BaloonsPoped, totalBaloonsPoped);
    }


    private void PlayerCollisionEvents_OnPlayerCollideWithKey()
    {
        if (BaloonsPoped < totalBaloonsPoped)
        {
            BaloonsPoped++;
            SFX_Manager.PlaySound(progressClip);
            Main_Quest.UpdateMainQuest(SelectedText(), BaloonsPoped, totalBaloonsPoped);
            Items_Count.UpdateLevelProgress(BaloonsPoped, totalBaloonsPoped);

            if (BaloonsPoped == totalBaloonsPoped)
            {

                SFX_Manager.PlaySound(objectiveComplete);
                

                // OBJECTIVE COMPLETE

                PlayerPrefs.SetInt("L4", 1);
                
                Items_Count.UpdateLevelProgress(BaloonsPoped, totalBaloonsPoped);
                Main_Quest.UpdateMainQuest(SelectedText(), BaloonsPoped, totalBaloonsPoped);
                Update_UI.ShowTextUpdate("Objective complete", 1f);
                gameObject.SetActive(false);
                EnemyHandler.Instance.ResetState();
                fadeGameobject.SetActive(true);

                if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
                {
                   // Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_4_Completed");
                }
            }
        }
    }

   
    private void OnDisable()
    {
        ThingsToDeActivateOnDisEnable?.Invoke();
    }

 

}
