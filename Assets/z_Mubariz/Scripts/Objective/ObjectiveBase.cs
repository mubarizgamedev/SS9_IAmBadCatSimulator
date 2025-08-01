using UnityEngine;
using System;
using System.Collections;

public abstract class ObjectiveBase : MonoBehaviour
{
    public int levelNumber;
    public bool containCutsceneAtStart;
    [Header("Objective Settings")]
    [SerializeField] protected string objectiveText;
    [SerializeField]
    protected int currentProgress;
    [SerializeField]
    protected int targetProgress;
    public Vector3 petPos;
    public Vector3 grannyPos;
    public float petRotaion;
    public float grannyRotation;
    Transform pet;
    Transform granny;
    Transform petEyesCamera;
    public static event Action OnLevelComplete;

    protected virtual void OnEnable()
    {
        Invoke(nameof(InitObjective), 0.15f);
        pet = NewObjectiveManager.Instance.petTransfrom;
        granny = NewObjectiveManager.Instance.granny;
        petEyesCamera = NewObjectiveManager.Instance.petEyesCamera;
    }

    protected virtual void ObjectiveOwnLogic()
    {

    }

    protected virtual void InitObjective()
    {
        EnemyHandler.Instance.ResetState();
        EnemyWandering.Instance.isWaiting = false;
        NewObjectiveManager.Instance.UpdateObjectiveText(objectiveText);
        NewObjectiveManager.Instance.Update_MainQuest(objectiveText, currentProgress, targetProgress);
        NewObjectiveManager.Instance.Update_LevelProgress( currentProgress, targetProgress);
        NewObjectiveManager.Instance.Update_ItemCount(LevelNumber());

        NewObjectiveManager.Instance.GrannyWanderingState(true);

        NewObjectiveManager.Instance.Activate_UI_Objects();
        NewObjectiveManager.Instance.EnableRayCaster(true);
        NewObjectiveManager.Instance.GranniesBats(false);

        ChangePlayerPositionTo(petPos, petRotaion);
        ChangeGrannyPositionTo(grannyPos, grannyRotation);
        ObjectiveOwnLogic();

        if (AdmobAdsManager.Instance && AdmobAdsManager.Instance.Check_Firebase &&
            Application.internetReachability != NetworkReachability.NotReachable)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent($"{LevelNumber()} Start");
            Debug.Log($"{LevelNumber()} Start");
        }
    }

    protected void UpdateProgressCount()
    {
        SFX_Manager.PlaySound(NewObjectiveManager.Instance.progressClip);

        currentProgress++;

        NewObjectiveManager.Instance.Update_MainQuest(objectiveText, currentProgress, targetProgress);

        NewObjectiveManager.Instance.Update_LevelProgress(currentProgress, targetProgress);

        if (currentProgress >= targetProgress)
        {
            ProgressCompleted();
        }
    }


    protected virtual void OnDisable()
    {
       
    }

    protected  virtual string LevelNumber()
    {
        return "Level_" + levelNumber;
    }
    protected abstract void ProgressCompleted();


    protected IEnumerator ObjectiveCompleteCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        NewObjectiveManager.Instance.AdCoinsOnLevelComplete();

        NewObjectiveManager.Instance.ResetGrannyState();

        SFX_Manager.PlaySound(NewObjectiveManager.Instance.completeClip);

        OnLevelComplete?.Invoke();

        NewObjectiveManager.Instance.fadeObject.SetActive(true);

        if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent($"{LevelNumber()} Completed");
            Debug.Log($"{LevelNumber()} Completed");
        }
    }


    void ChangePlayerPositionTo(Vector3 targerPos, float bodyRot)
    {
        pet.gameObject.SetActive(false);
        pet.transform.localPosition = targerPos;
        pet.localRotation = Quaternion.Euler(0f, bodyRot, 0f);
        petEyesCamera.localRotation = Quaternion.Euler(0f, 0f, 0f);
        pet.gameObject.SetActive(true);
    }

    void ChangeGrannyPositionTo(Vector3 pos, float RotY)
    {
        granny.gameObject.SetActive(false);
        granny.localPosition = pos;
        granny.localRotation = Quaternion.Euler(0, RotY, 0);
        granny.gameObject.SetActive(true);
    }

    public static int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("CurrentLevel", 1);
    }
    

    public void OnRestart()
    {
        currentProgress = 0;
        NewObjectiveManager.Instance.ResetGrannyState();

        InitObjective();
    }
}
