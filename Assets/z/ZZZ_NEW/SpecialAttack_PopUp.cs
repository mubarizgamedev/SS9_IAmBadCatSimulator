using UnityEngine;

public class SpecialAttack_PopUp : MonoBehaviour
{
    public float adDelay = 45f;
    private const float secondaryFunctionDelay = 40f;
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

        timeRemaining = adDelay;

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
        if (!gameObject.activeInHierarchy) return;
        if (start)
        {
            ShowNextInApp();
           
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
        //Time.timeScale = 0.5f;

        currentInAppIndex = (currentInAppIndex + 1) % InApps.Length;

    }
    public void ResetSpeacialTimer()
    {
        foreach (GameObject inApp in InApps)
        {
            inApp.SetActive(false);
        }
        StartAdTimer();
    }

    public void DisableAllPanels()
    {
        foreach (GameObject inApp in InApps)
        {
            inApp.SetActive(false);
            Time.timeScale = 1f;
            StartAdTimer();
        }
    }

}
