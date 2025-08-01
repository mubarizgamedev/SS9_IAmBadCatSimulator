using UnityEngine;
using UnityEngine.Events;

public class CallInterstialAd : MonoBehaviour
{
    [SerializeField] float loadAdAfter;
    public UnityEvent ToDosAfterAD;
   public void CallInterstitialMax()
   {
       Debug.Log("Call Interstitial Ad");
    }

    private void OnEnable()
    {
        LoadInt();
    }
    void DisableGameobject()
    {
        gameObject.SetActive(false);
    }
    void MainWork()
    {
        ToDosAfterAD?.Invoke();
    }

    // Int
    void LoadInt()
    {
        if (AdmobAdsManager.Instance.Ads_Googel_Max == true)
        {

            MainWork();
            //MaxAdsManager.Instance.Btn_LS_Int();
            gameObject.SetActive(false);
        }
        else
        {

            LoadInterstial();
            Invoke(nameof(show_Int),5.7f );
            Invoke(nameof(DisableGameobject), 5.9f);
            Invoke(nameof(MainWork), 6f);
        }
    }
    public void show_Int()
    {
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.ShowInterstitial();
        }
    }

    public void LoadInterstial()
    {
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.LoadInterstitial();
        }
    }
}