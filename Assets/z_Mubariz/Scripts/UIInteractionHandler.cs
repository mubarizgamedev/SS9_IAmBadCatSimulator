using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIInteractionHandler : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenu;
    public GameObject missionCompletePanel;
    public GameObject missionFailPanel;
    [Space(5)]
    public GameObject fadePanel;
    public GameObject congratesLevelComplete;
    public GameObject bloodOverLayUI;
    public GameObject gameplayControls;

    [Space(5)]
    [Header("Buttons")]
    public Button pauseButton;
    public Button restartButton_Fail;
    public Button restartButton_Pause;
    public Button resumeButton;
    public Button homeButton;
    public Button nextButton;

    [Space(5)]
    [Header("Gameobjects")]
    public GameObject granny;

    [Space(5)]
    [Header("Req Components")]
    public AdAfter40Sec adAfter40Sec;
    public SpecialAttack_PopUp specialAttack_Pop;

    public bool adsNotAllowed;
    private void Start()
    {
        if(AdmobAdsManager.Instance)
        if (AdmobAdsManager.Instance.Skip_Int)
        {
            adsNotAllowed = true;
        }

        //mubariz

        AssigningButtons();
        GameStateManager.Instance.OnMissionFailed += ShowMissionFailPanel;
        GameStateManager.Instance.OnMissionCompleted += ShowMissionCompletePanel;
        GameStateManager.Instance.OnGamePaused += ShowPauseMenu;
        GameStateManager.Instance.OnGameResumed += HidePauseMenu;
        GameStateManager.Instance.OnGameRestarted += ResetGame;
    }
    

    private void OnDestroy()
    {
        if (GameStateManager.Instance == null) return;

        GameStateManager.Instance.OnMissionFailed -= ShowMissionFailPanel;
        GameStateManager.Instance.OnMissionCompleted -= ShowMissionCompletePanel;
        GameStateManager.Instance.OnGamePaused -= ShowPauseMenu;
        GameStateManager.Instance.OnGameResumed -= HidePauseMenu;
        GameStateManager.Instance.OnGameRestarted -= ResetGame;
    }
    
    void AssigningButtons()
    {
        pauseButton.onClick.AddListener(OnPauseButtonPressed);
        restartButton_Fail.onClick.AddListener(OnRestartButtonPressed);
        restartButton_Pause.onClick.AddListener(OnRestartButtonPressed);
        resumeButton.onClick.AddListener(OnResumeButtonPressed);
        homeButton.onClick.AddListener(OnHomeButtonPressed);
        nextButton.onClick.AddListener(OnNextMissionButtonPressed);
    }


    // === BUTTON FUNCTIONS ===

    public void OnPauseButtonPressed()
    {
        GameStateManager.Instance.PauseGame();
    }

    public void OnResumeButtonPressed()
    {
        GameStateManager.Instance.ResumeGame();
    }

    public void OnRestartButtonPressed()
    {
        GameStateManager.Instance.RestartGame();
    }


    #region HOME
    public void OnHomeButtonPressed()
    {
        Home();
    }

    void Home()
    {
        StartCoroutine(HomeCoroutine());
    }

    IEnumerator HomeCoroutine()
    {
        fadePanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }

    #endregion

    #region NEXT MISSION

    //NEXT MISSION

    public void OnNextMissionButtonPressed()
    {
        NextMission();
    }

    void NextMission()
    {
        StartCoroutine(NextMissionCoroutine());
    }

    IEnumerator NextMissionCoroutine()
    {
        //NewObjectiveManager.Instance.DisableHintObjects();
        fadePanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #endregion

    #region PAUSE

    //PAUSE
    private void ShowPauseMenu()
    {
        if (adsNotAllowed)
        {
            PauseWork();
        }
        else
        {
            InterstitialAdCall.Instance.StartLoading(PauseWork);
        }
        
    }

    void PauseWork()
    {
        adAfter40Sec.ResetAdTimer();
        specialAttack_Pop.ResetSpeacialTimer();
        granny.SetActive(false);
        pauseMenu.SetActive(true);

    }

    #endregion

    #region RESUME

    //RESUME
    private void HidePauseMenu()
    {
        Resume();
    }
    void Resume()
    {
        StartCoroutine(ResumeCoroutine());
    }

    IEnumerator ResumeCoroutine()
    {
        Debug.Log("Called");
        fadePanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        pauseMenu.SetActive(false);
        granny.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fadePanel.SetActive(false);
    }

    #endregion

    #region RESET
    //RESET
    private void ResetGame()
    {
        OnReset();
    }
    void OnReset()
    {
        StartCoroutine(ResetCoroutine());
    }


    IEnumerator ResetCoroutine()
    {
        
        EnemyHandler.Instance.ResetState();
        fadePanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #endregion

    #region MISSION COMPLETE




    private void ShowMissionCompletePanel()
    {
        adAfter40Sec.ResetAdTimer();
        if (adsNotAllowed)
        {
            CompleteWork();
        }
        else
        {
            InterstitialAdCall.Instance.StartLoading(CompleteWork);
        }
    }

    void CompleteWork()
    {
        StartCoroutine(MissionComplete());
    }

    IEnumerator MissionComplete()
    {
        adAfter40Sec.ResetAdTimer();
        specialAttack_Pop.ResetSpeacialTimer();
        fadePanel.SetActive(true);
        EnemyHandler.Instance.ResetState();
        //NewObjectiveManager.Instance.AdCoinsOnLevelComplete();
        //SFX_Manager.PlaySound(NewObjectiveManager.Instance.completeClip);

        yield return new WaitForSeconds(1.5f);

        fadePanel.SetActive(false);
        granny.SetActive(false);
        congratesLevelComplete.SetActive(true);

        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   

    #endregion

    #region SHOW MISSION FAIL
    private void ShowMissionFailPanel()
    {
        adAfter40Sec.ResetAdTimer();
        if (adsNotAllowed)
        {
            Fail();
        }
        else
        {
            InterstitialAdCall.Instance.StartLoading(Fail);
        }
    }

    void Fail()
    {
        StartCoroutine(FailCoroutine());
    }

    IEnumerator FailCoroutine()
    {
        adAfter40Sec.ResetAdTimer();
        specialAttack_Pop.ResetSpeacialTimer();
        //fadePanel.SetActive(true);
        yield return new WaitForSeconds(0f);
        missionFailPanel.SetActive(true);
        fadePanel.SetActive(false);
    }

    #endregion
}
