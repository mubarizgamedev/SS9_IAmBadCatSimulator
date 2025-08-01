using UnityEngine;

public class AdAfter40Sec : MonoBehaviour
{
    public bool canShowAd = false;
    public float adDelay = 45f;
    private const float secondaryFunctionDelay = 40f;
    [SerializeField] GameObject adBreakGameObject;
    [SerializeField] GameObject[] InApps;

    int currentInAppIndex = 0;
    [SerializeField] private float timeRemaining;

    bool start;

    private void OnEnable()
    {
        start = true;
        StartAdTimer();
    }


    /// <summary>
    /// UPDATE AUR MYFUNCTION ME LINES COMMENT KI HAIN
    /// </summary>

    void StartAdTimer()
    {
        CancelInvoke(nameof(MyFunction));
        CancelInvoke(nameof(AdBreak));

        timeRemaining = adDelay;
        canShowAd = false; // Reset ad availability

        Invoke(nameof(AdBreak), secondaryFunctionDelay);
        Invoke(nameof(MyFunction), adDelay);
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if (!canShowAd)
        {
              //                             canShowAd = true; // Set canShowAd to true when 45 seconds have passed
        }
    }

    void MyFunction()
    {
        if (!gameObject.activeInHierarchy) return; // 🛡️ Guard

        if (start)
        {
            ShowNextInApp();
            canShowAd = true;
        }

        ShowNextInApp();


        ////
        //ONLY UNCOMMENT BELOW LINE
        ///


        //canShowAd = true;
    }


    void ShowNextInApp()
    {
        foreach (GameObject inApp in InApps)
        {
            inApp.SetActive(false);
        }

        InApps[currentInAppIndex].SetActive(true);
        //Time.timeScale = 0.5f;

        currentInAppIndex = (currentInAppIndex + 1) % InApps.Length;
    }

    void AdBreak()
    {
        //Debug.Log("_________________Ad Break");
        //adBreakGameObject.SetActive(true);
        //Invoke(nameof(DisableAdBreakGO), 4f);
    }

    void DisableAdBreakGO()
    {
        //adBreakGameObject.SetActive(false);
    }

    public void ResetAdTimer()
    {
        foreach (GameObject inApp in InApps)
        {
            inApp.SetActive(false);
        }
        StartAdTimer();
    }

    public void TimeScaleOne()
    {
        Time.timeScale = 1f;
    }

    public void DisableEachPanel()
    {
        foreach (GameObject inApp in InApps)
        {
            inApp.SetActive(false);
        }
    }

    public void ShowAd()
    {
        if (CanShowAd())
        {
            if (AdmobAdsManager.Instance)
            {
                AdmobAdsManager.Instance.ShowInterstitial();
                ResetAdTimer();
                TimeScaleOne();
            }
        }
    }

    public bool CanShowAd()
    {
        return canShowAd;
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(MyFunction));
        CancelInvoke(nameof(AdBreak));
    }

}