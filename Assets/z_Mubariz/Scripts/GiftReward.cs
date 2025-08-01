
using UnityEngine;

public class GiftReward : MonoBehaviour
{
    public ModesAd modesAd;

    private void OnEnable()
    {
        load_rew();
    }
    private void OnDisable()
    {
        show_rew();
    }

    void AddCoins()
    {
        modesAd.AddCoins(50);
        AdmobAdsManager.Instance.Btn_Reward_Done("");
    }

    // Rew
    void load_rew()
    {
        if (AdmobAdsManager.Instance.Ads_Googel_Max == true)
        {
            // ADsMax
        }
        else
        {
            AdmobAdsManager.Instance.LoadRewardedVideo();
        }
    }
    void show_rew()
    {
        if (AdmobAdsManager.Instance.Ads_Googel_Max == true)
        {
            //MaxAdsManager.Instance.Btn_LS_Rew(AddCoins);
        }
        else
        {
            AdmobAdsManager.Instance.ShowRewardedVideo(AddCoins);
        }
    }
}