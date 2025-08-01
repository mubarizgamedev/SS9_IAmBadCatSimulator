using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ObjectivesCutscene : MonoBehaviour
{
    private PlayableDirector m_PlayableDirector;
    public bool updateUIenable;
    public UnityEvent OnCutSceneStart;
    public UnityEvent OnCutSceneEnd;
    public GameObject rewardLoadingPanel;
    public GameObject updateUI;

    public Button skipButton;

    private void OnEnable()
    {
        m_PlayableDirector = GetComponent<PlayableDirector>();

        if (m_PlayableDirector)
        {
            m_PlayableDirector.played += M_PlayableDirector_played;
            m_PlayableDirector.stopped += M_PlayableDirector_stopped;
            m_PlayableDirector.Play();
        }
        else
        {
            Debug.LogWarning("PlayableDirector component not found.");
        }
        skipButton.onClick.AddListener(SkipCutScene);
    }

    private void OnDisable()
    {
        if (m_PlayableDirector)
        {
            m_PlayableDirector.played -= M_PlayableDirector_played;
            m_PlayableDirector.stopped -= M_PlayableDirector_stopped;
        }
    }

    private void M_PlayableDirector_played(PlayableDirector obj)
    {
        Debug.Log("Cutscene Started");
        OnCutSceneStart?.Invoke();
    }

    public void M_PlayableDirector_stopped(PlayableDirector obj)
    {
        Debug.Log("Cutscene Ended");
        OnCutSceneEnd?.Invoke();
        gameObject.SetActive(false);
    }

    



    void SkipCutScene()
    {
        if (AdmobAdsManager.Instance.Ads_Googel_Max == true)
        {
            //MaxAdsManager.Instance.Btn_LS_Rew(WorkToDo);
        }
        else
        {
            rewardLoadingPanel.SetActive(true);
            m_PlayableDirector.Pause();
            load_rew();
            Invoke(nameof(Action), 6f);
        }


        
    }

    void Action()
    {
        rewardLoadingPanel.SetActive(false);
        show_rew();
    }

    void WorkToDo()
    {
        Invoke(nameof(RealWork), 0.369f);
    }

    void RealWork()
    {
        m_PlayableDirector.Resume();
        m_PlayableDirector.Stop();
        skipButton.gameObject.SetActive(false);
        Debug.Log("Cutscene Ended");
        OnCutSceneEnd?.Invoke();
        gameObject.SetActive(false);
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.Btn_Reward_Done("REWARD GRANTED");
        }
        if (updateUIenable)
        {
            Invoke(nameof(UpdateUIEnable), 1f);
        }
           
    }

    void UpdateUIEnable()
    {
        updateUI?.SetActive(true);
    }


    float Timer_xXx;
    // Rew
    void load_rew()
    {
            AdmobAdsManager.Instance.LoadRewardedVideo();
    }
    void show_rew()
    {    
           AdmobAdsManager.Instance.ShowRewardedVideo(WorkToDo);
    }
}
