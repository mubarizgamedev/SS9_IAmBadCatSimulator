using UnityEngine;
using UnityEngine.UI;
using System;

public class InterstitialAdCall : MonoBehaviour
{

    public GameObject loadingPanel;
    public Image fillerImage;
    public Text loadingText;
    public bool dontAllowLoadingPanel;


    [Space(5)]
    [Header("Prefs")]
    public string textToShow;
    public float loadingTime = 6f;
    bool panelActivation;

    public static InterstitialAdCall Instance { get; private set; }

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

    public void StartLoading(Action workToDo, bool activationOfPanel = false)
    {
        //if (AdmobAdsManager.Instance)
        //{
        //    AdmobAdsManager.Instance.LoadInterstitial();
        //}



        if (dontAllowLoadingPanel)
        {
            loadingPanel.SetActive(false);
        }
        else
        {
            panelActivation = activationOfPanel;
        }

        workAction = workToDo;
        loadingPanel.SetActive(true);
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
        if (AdmobAdsManager.Instance)
        {
            MaxAdsManager.Instance.Btn_LS_Int();
            //AdmobAdsManager.Instance.ShowInterstitial();
        }


        //mubari
        workAction();
        loadingPanel.SetActive(panelActivation);
    }
}
