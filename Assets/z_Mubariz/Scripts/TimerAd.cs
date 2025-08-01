using UnityEngine;

public class TimerAd : MonoBehaviour
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

    }


    void ShowNextInApp()
    {
        foreach (GameObject inApp in InApps)
        {
            inApp.SetActive(false);
        }

        InApps[currentInAppIndex].SetActive(true);

        currentInAppIndex = (currentInAppIndex + 1) % InApps.Length;
    }

    void AdBreak()
    {
        adBreakGameObject.SetActive(true);
        Invoke(nameof(DisableAdBreakGO), 4f);
    }

    void DisableAdBreakGO()
    {
        adBreakGameObject.SetActive(false);
    }

    public void ResetAdTimer()
    {
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
            Time.timeScale = 1f;
        }
    }
    private void OnDisable()
    {
        CancelInvoke(nameof(MyFunction));
        CancelInvoke(nameof(AdBreak));
    }

}