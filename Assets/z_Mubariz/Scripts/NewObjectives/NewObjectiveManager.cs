using UnityEngine;

public class NewObjectiveManager : MonoBehaviour
{
    public static NewObjectiveManager Instance;

    [Header("Shared Components")]
    public Main_Quest mainQuest;
    public Update_UI updateUI;
    public Items_Count itemsCount;

    [Header("UI & Audio")]
    public GameObject fadeObject;
    public AudioClip completeClip;
    public AudioClip progressClip;

    [Space(5)]
    [Header("Player")]
    public Transform petTransfrom;
    public Transform petEyesCamera;
    public GameObject rayCaster;

    [Header("Granny")]
    public Transform granny;
    public Animator grannyAnimator;
    public RuntimeAnimatorController grannyWanderingAnimator;
    public RuntimeAnimatorController garnnyWatchingTvAnimator;
    public RuntimeAnimatorController grannyCookingAnimator;
    public GameObject[] allGranniesBats;
    public GameObject grannyWanderingControllerGo;
    public GameObject tutorialGranny;
    
    [Space(5)]
    [Header("Gameobjects Refrence")]
    public GameObject[] uiObjToActivate;

    [Header("Level Objectives")]
    public GameObject[] levelObjectives;
    [HideInInspector]
    public ObjectiveBase currentObjective;

    public EnemyHandler enemyHandler;

    private void Start()
    {
        ObjectiveBase.OnLevelComplete += UpdateLevelProgress;
        // Load and activate correct level
        LoadLatestLevel();
    }

    

    private void OnDestroy()
    {
        ObjectiveBase.OnLevelComplete -= UpdateLevelProgress;
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void Update_MainQuest(string text , int currentProgress, int totalProgress)
    {
        mainQuest.UpdateMainQuest(text, currentProgress, totalProgress);
    }
    public void UpdateObjectiveText(string text)
    {
        updateUI.ShowTextUpdate(text,0);
    }
    public void Update_ItemCount(string text)
    {
        itemsCount.UpdateLevelNumber(text);
    }

    public void Update_LevelProgress(int current , int total)
    {
        itemsCount.UpdateLevelProgress(current, total);
    }

    public void Activate_UI_Objects()
    {
        foreach (var item in uiObjToActivate)
        {
            item.SetActive(true);
        }
    }
    public void Deactivate_UI_Objects()
    {
        foreach (var item in uiObjToActivate)
        {
            item.SetActive(false);
        }
    }

    public void GrannyWanderingState(bool condition)
    {
        ChangeAnimatorToWandering();
        grannyWanderingControllerGo.SetActive(condition);
    }

    public void ChangeAnimatorToSit()
    {
        grannyAnimator.runtimeAnimatorController = garnnyWatchingTvAnimator;
    }

    public void ChangeAnimatorToStandWorking()
    {
        grannyAnimator.runtimeAnimatorController = grannyCookingAnimator;
    }
    public void ChangeAnimatorToWandering()
    {
        grannyAnimator.runtimeAnimatorController = grannyWanderingAnimator;
    }
    public void EnableRayCaster(bool condition)
    {
        rayCaster.SetActive(condition);
    }


    public static void LoadLatestLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // Level 1 by default

        // Safety check
        if (Instance == null)
        {
            Debug.LogError("NewObjectiveManager.Instance is null!");
            return;
        }

        Instance.ActivateLevelObjective(currentLevel);
    }

    public void ActivateLevelObjective(int levelIndex)
    {

        // Deactivate all level objectives
        foreach (GameObject obj in levelObjectives)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        // Indexing is zero-based, while level numbers start from 1
        int index = levelIndex - 1;
        if (levelObjectives[index] != null)
        {
            levelObjectives[index].SetActive(true);

            // Assign the current objective
            currentObjective = levelObjectives[index].GetComponent<ObjectiveBase>();
        }

        if (index >= 0 && index < levelObjectives.Length)
        {
            if (levelObjectives[index] != null)
            {
                levelObjectives[index].SetActive(true);
            }
            else
            {
                Debug.LogWarning($"Level objective at index {index} is null.");
            }
        }
        else
        {
            Debug.LogWarning($"Level index {index} out of bounds for levelObjectives array.");
        }
    }

    public void ResetGrannyState()
    {
        enemyHandler.isChasingCat = false;
        enemyHandler.ResetState();
    }

    void UpdateLevelProgress()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // Default to level 1
        PlayerPrefs.SetInt("CurrentLevel", currentLevel + 1);
        PlayerPrefs.Save();
    }

    public void RestartLevel()
    {
        if (currentObjective != null)
        {
            currentObjective.OnRestart();
        }
        else
        {
            Debug.LogWarning("No currentObjective found to restart.");
        }
    }
    public void GranniesBats(bool condition)
    {
        foreach (var item in allGranniesBats)
        {
            item.SetActive(condition);
        }
    }

    public void AdCoinsOnLevelComplete()
    {
        itemsCount.Ad50Coins();
    }
    int GetActiveChildIndex(GameObject parent)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);

            if (child.gameObject.activeInHierarchy)
            {
                return i; // Return index of the active child
            }
        }

        return -1; // No active child found
    }

    public int CurrentLevel()
    {
        int currentlevel = GetActiveChildIndex(gameObject) + 1;
        return currentlevel;
    }
}
