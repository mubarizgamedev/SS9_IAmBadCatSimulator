using UnityEngine;
using UnityEngine.Events;

public class LoadInterstitail : MonoBehaviour
{
    public UnityEvent workToAfterAd;
    [SerializeField] float adShowTimer;
    [SerializeField] float gameobjectDisableTime;
    [SerializeField] float workTime;
    void OnEnable()
    {
        LoadInterstitialCheck();
    }
    void Work()
    {
        workToAfterAd?.Invoke();
    }

    void DisableGameobject()
    {
        gameObject.SetActive(false);
    }


    // Int
    public void LoadInt()
    {
        //if(AdmobAdsManager.Instance)
        //    AdmobAdsManager.Instance.LoadInterstitial();

    }
    public void ShowInt()
    {
        //if (AdmobAdsManager.Instance)
        //    AdmobAdsManager.Instance.ShowInterstitial();
        if (MaxAdsManager.Instance)
        {
            MaxAdsManager.Instance.Btn_LS_Int();
        }
    }

    void LoadInterstitialCheck()
    {
        if (AdmobAdsManager.Instance.Ads_Googel_Max == true)
        {
            //MaxAdsManager.Instance.Btn_LS_Int();
            Work();
            gameObject.SetActive(false);
        }
        else
        {
            LoadInt();
            Invoke(nameof(ShowInt), adShowTimer);
            Invoke(nameof(DisableGameobject), gameobjectDisableTime);
            Invoke(nameof(Work), workTime);
        }
    }
}