using UnityEngine;

public class LevelProgressNew : MonoBehaviour
{
    [SerializeField] string textToShow;
    private void OnEnable()
    {
        if (AdmobAdsManager.Instance)
        {
            if (AdmobAdsManager.Instance.Check_Firebase && Application.internetReachability != NetworkReachability.NotReachable)
            {
                Firebase.Analytics.FirebaseAnalytics.LogEvent(textToShow);
            }
        }        
    }
}
