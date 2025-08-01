namespace Firebase.Sample.Analytics
{
    using Firebase;
    using Firebase.Analytics;
    using Firebase.Extensions;
    using System;
    using System.Threading.Tasks;
    using UnityEngine;

    public class FirebaseHandler : MonoBehaviour
    {
        DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
        protected bool firebaseInitialized = false;

        void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    Invoke("wait", 1f);
                }
                else
                {
                }
            });
        }
        void wait()
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            firebaseInitialized = true;

            // AdmobAdsManager.Instance.Test_Ads = true;
            AdmobAdsManager.Instance.Internet = true;
            AdmobAdsManager.Instance.Check_Firebase = true;
            AdmobAdsManager.Instance.Ads_Purchase = true;
            AdmobAdsManager.Instance.GDRP = true;
        }
    }
}