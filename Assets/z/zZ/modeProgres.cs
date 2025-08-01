using UnityEngine;

public class modeProgres : MonoBehaviour
{
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("modeSe") == 0)
        {
            if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
            {
                //Firebase.Analytics.FirebaseAnalytics.LogEvent("EnteredModeSelection");
                PlayerPrefs.SetInt("modeSe", 1);
            }
        }
    }
}
