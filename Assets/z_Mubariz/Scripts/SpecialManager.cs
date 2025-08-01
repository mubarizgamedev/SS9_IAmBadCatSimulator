using UnityEngine;
using System;

public class SpecialManager : MonoBehaviour
{
    [Header("Boxing")]
    [SerializeField] GameObject fireRewardPanel;
    [SerializeField] GameObject beeRewardPanel;
    [SerializeField] GameObject currentRewardPanel;
    [SerializeField] GameObject boxingRewardPanel;
    [Space(10)]
    [SerializeField] GameObject playerBoxingGlove;
    [SerializeField] GameObject playerShockGun;
    [Header("UI Button")]
    [SerializeField] GameObject boxingUI;
    [SerializeField] GameObject ShockGunUI;
    [SerializeField] GameObject fireButtonUI;
    [SerializeField] GameObject beeButtonUI;
    [SerializeField] AdAfter40Sec AdAfter40Sec;
    [SerializeField] GameObject rewardLoadingPanel;



    private void Start()
    {
        PlayerCollisionEvents.OnPuchBoxTrigger += OnPunchButtonPressed;
        PlayerCollisionEvents.OnShockGunTrigger += OnShockButtonPressed;
    }
    private void OnDestroy()
    {
        PlayerCollisionEvents.OnPuchBoxTrigger -= OnPunchButtonPressed;
        PlayerCollisionEvents.OnShockGunTrigger -= OnShockButtonPressed;
    }



    public void OnShockButtonPressed()
    {
        currentRewardPanel.SetActive(true);
    }

    public void OnPunchButtonPressed()
    {
        boxingRewardPanel.SetActive(true);
    }

    public void OnFireButtonPressed()
    {
        fireRewardPanel.SetActive(true);
    }
    public void OnBeeButtonPressed()
    {
        beeRewardPanel.SetActive(true);
    }




    public void RewardButtonBoxing()
    {
        REWARDADFORPunch();
    }
    public void RewardButtonShockGun()
    {
        REWARDADFORShock();
    }
    public void RewardButtonBeeAttack()
    {
        REWARDADFORBee();
    }
    public void RewardButtonFireAttack()
    {
        REWARDADFORFire();
    }


    int val;
    void REWARDADFORBee()
    {
        //rewardLoadingPanel.SetActive(true);
        load_rew();
        Invoke(nameof(ShowRewardForBee), Timer_xXx);

    }
    void REWARDADFORFire()
    {
        //rewardLoadingPanel.SetActive(true);
        load_rew();
        Invoke(nameof(ShowRewardForFire), Timer_xXx);
    }
    void REWARDADFORPunch()
    {
        //rewardLoadingPanel.SetActive(true);
        load_rew();
        Invoke(nameof(ShowRewardForPunch), Timer_xXx);
    }
    void REWARDADFORShock()
    {
        //rewardLoadingPanel.SetActive(true);
        load_rew();
        Invoke(nameof(ShowRewardForShock), Timer_xXx);
    }

    void _call()
    {
        if (val == 0)
        {
            Invoke(nameof(ActionAfterBoxingReward), .369f);
        }
        if (val == 1)
        {
            Invoke(nameof(ActionAfterShockReward), .369f);
        }
        if (val == 2)
        {
            Invoke(nameof(ActionAfterFireReward), .369f);
        }
        if (val == 3)
        {
            Invoke(nameof(ActionAfterBeeReward), .369f);
        }
    }

    //punch 1 shock 2 fire 3 bee 4

    void ShowRewardForPunch()
    {
        rewardLoadingPanel.SetActive(false);
        val = 0;

        Rew_xXx = _call;
        show_rew();
    }

    void ActionAfterBoxingReward()
    {
        boxingRewardPanel.SetActive(false);
        playerBoxingGlove.SetActive(true);
        SpecialItemInHand.Instance.SetItemState(playerBoxingGlove, true);
        boxingUI.SetActive(true);
        AdAfter40Sec.ResetAdTimer();
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.Btn_Reward_Done("REWARD GRANTED");
        }
    }

    /// <summary>
    /// ////////////////////////////////
    /// </summary>
    void ShowRewardForShock()
    {
        rewardLoadingPanel.SetActive(false);
        val = 1;

        Rew_xXx = _call;
        show_rew();
    }
    void ActionAfterShockReward()
    {
        currentRewardPanel.SetActive(false);
        ShockGunUI.SetActive(true);
        SpecialItemInHand.Instance.SetItemState(playerShockGun, true);
        playerShockGun.SetActive(true);
        AdAfter40Sec.ResetAdTimer();
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.Btn_Reward_Done("REWARD GRANTED");
            Debug.Log("...........acknownledgment");
        }
    }

    /// <summary>
    /// ////////////////////////
    /// </summary>
    void ShowRewardForFire()
    {
        rewardLoadingPanel.SetActive(false);
        val = 2;

        Rew_xXx = _call;
        show_rew();
    }
    void ActionAfterFireReward()
    {
        fireRewardPanel.SetActive(false);
        fireButtonUI.SetActive(true);
        playerShockGun.SetActive(true);
        SpecialItemInHand.Instance.SetItemState(playerShockGun, true);
        AdAfter40Sec.ResetAdTimer();
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.Btn_Reward_Done("REWARD GRANTED");
        }
    }

    /// <summary>
    /// ////////////////
    /// </summary>
    /// 

    void ShowRewardForBee()
    {
        rewardLoadingPanel.SetActive(false);
        val = 3;

        Rew_xXx = _call;
        show_rew();
    }
    void ActionAfterBeeReward()
    {
        beeRewardPanel.SetActive(false);
        beeButtonUI.SetActive(true);
        playerShockGun.SetActive(true);
        SpecialItemInHand.Instance.SetItemState(playerShockGun, true);
        AdAfter40Sec.ResetAdTimer();
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.Btn_Reward_Done("REWARD GRANTED");
        }
    }

    public void DisableAllPanels()
    {
        boxingRewardPanel.SetActive(false);
        currentRewardPanel.SetActive(false);
        fireRewardPanel.SetActive(false);
        beeRewardPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BoxUIButtonPressed()
    {
        SpecialItemInHand.Instance.SetItemState(playerBoxingGlove, false);
    }

    public void GuncUIButtonPressed()
    {
        SpecialItemInHand.Instance.SetItemState(playerShockGun, false);
    }

    float Timer_xXx;
    Action Rew_xXx;
    void Chk_Chk()
    {
        Rew_xXx.Invoke();
    }
    // Rew
    void load_rew()
    {
        if (AdmobAdsManager.Instance.Ads_Googel_Max == true)
        {
            Timer_xXx = 0.1f;
            // ADsMax
        }
        else
        {
            Timer_xXx = 6f;
            //AdmobAdsManager.Instance.LoadRewardedVideo();
        }
    }
    void show_rew()
    {
       
            MaxAdsManager.Instance.Btn_LS_Rew(Chk_Chk);
            //AdmobAdsManager.Instance.ShowRewardedVideo(Chk_Chk);
    }
}
