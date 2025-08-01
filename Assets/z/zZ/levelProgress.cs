using UnityEngine;

public class levelProgress : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("mainProgress") == 0)
        {
            if (AdmobAdsManager.Instance != null)
            {
                if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
                {
                    Firebase.Analytics.FirebaseAnalytics.LogEvent("EnteredMainmenu");
                    PlayerPrefs.SetInt("mainProgress", 1);
                }
            }
            
        }
    }
}
