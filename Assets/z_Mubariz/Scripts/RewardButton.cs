using UnityEngine;

public class RewardButton : MonoBehaviour
{

    public GameObject currentRewardedGameobject;
    [SerializeField] PlayerRaycaster playerRaycaster;
    [SerializeField] AdAfter40Sec AdAfter40Sec;
    [SerializeField] GameObject rewardLoading;



    private void Start()
    {
        playerRaycaster.OnInteractedWithRewarded += PlayerRaycaster_OnInteractedWithRewarded;
    }

    private void PlayerRaycaster_OnInteractedWithRewarded(object sender, PlayerRaycaster.OnInteractedWithRewardedClass e)
    {
        currentRewardedGameobject = e.rewardedGameObject;
    }

    public void Btn_Reward()
    {     
        rewardLoading.SetActive(true);

        load_rew(); 
        Invoke(nameof(ShowRewardVideo), Timer_xXx);

    }
    void ShowRewardVideo()
    {
        Debug.Log("ShowRewardVideo");
        rewardLoading.SetActive(false);
        show_rew();
    }
    void ActionReward()
    {
        currentRewardedGameobject.layer = 8;
        AdAfter40Sec.ResetAdTimer();
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.Btn_Reward_Done("REWARD GRANTED");
        }
    }

    float Timer_xXx;
    // Rew
    void load_rew()
    {
       
            Timer_xXx = 6f;
            //AdmobAdsManager.Instance.LoadRewardedVideo();
    }
    void show_rew()
    {
        //if (AdmobAdsManager.Instance.Ads_Googel_Max == true)
        //{
        //    //MaxAdsManager.Instance.Btn_LS_Rew(ActionReward);
        //}
        //else
        //{
            AdmobAdsManager.Instance.ShowRewardedVideo(ActionReward);
            MaxAdsManager.Instance.Btn_LS_Rew(ActionReward);
        //}
    }
}