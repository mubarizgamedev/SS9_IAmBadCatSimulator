using UnityEngine.UI;
using System;
using UnityEngine;
using System.Collections;

public class RewardAdCall : MonoBehaviour
{
    public GameObject loadingPanel;
    public Image fillerImage;
    public Text loadingText;
    public bool dontAllowLoadingPanel;
    [Space(5)]
    [Header("Prefs")]
    public string textToShow;
    public float loadingTime = 6f;


    public static RewardAdCall Instance { get; private set; }

    private float currentTime = 0f;
    private bool isLoading = false;
    private Action workAction;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        loadingText.text = textToShow;
    }

    public void StartLoading(Action workToDo)
    {
        //if (AdmobAdsManager.Instance)
        //{
        //    AdmobAdsManager.Instance.LoadRewardedVideo();
        //}

        workAction = null;
        workAction = workToDo;
        if (dontAllowLoadingPanel)
        {
            loadingPanel.SetActive(false);
        }
        else
        {
            loadingPanel.SetActive(true);
        }
;
        currentTime = 0f;
        isLoading = true;
        fillerImage.fillAmount = 0f;
    }

    private void Update()
    {
        if (!isLoading) return;

        currentTime += Time.deltaTime;
        float progress = Mathf.Clamp01(currentTime / loadingTime);
        fillerImage.fillAmount = progress;

        if (progress >= 1f)
        {
            isLoading = false;
            OnLoadingComplete();
        }
    }

    private void OnLoadingComplete()
    {
        if (MaxAdsManager.Instance)
        {
            //AdmobAdsManager.Instance.ShowRewardedVideo(DoAction);
            MaxAdsManager.Instance.Btn_LS_Rew(DoAction);
            loadingPanel.SetActive(false);
        }
        else
        {
            loadingPanel.SetActive(false);
            DoAction();
        }
       
    }
    void DoAction()
    {
        StartCoroutine(Work());
    }

    IEnumerator Work()
    {
        yield return new WaitForSeconds(0.5f);
        workAction?.Invoke();
        if(AdmobAdsManager.Instance)
        AdmobAdsManager.Instance.Btn_Reward_Done("");
    }

}
