using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using GoogleMobileAds.Ump.Api;
using GoogleMobileAds.Common;
using System.Xml.Linq;

public class MaxAdsManager : MonoBehaviour
{
    public static MaxAdsManager Instance;
    public bool Test_Ads;
    public bool UAD;
    // ID
    public String AppID;
    public String SdkKey;
    // Int
    public bool Skip_Int;
    public bool Ads_Int_Allow;
    public String InterID;
    // Rew
    public bool Skip_Rew;
    public bool Ads_Rew_Allow;
    public String RewardedID;
    // AppOpen
    public bool Skip_App;
    public bool Ads_Appopen_Allow;
    public String AppOpenID;
    public bool ForeGroundedAD;

    public string[] TestDevices;

    public Action RewardHandle;
    public Action NotRewardHandle;
    [HideInInspector]
    private bool InitSucceded;
    private bool IsInterstitialAdReady = false;
    private bool isRewarded = false;

    void Awake()
    {
        Instance = this;
    }
    void OnEnable()
    {
        Invoke(nameof(OnInitializeMax), 2f);
    }
    void Start()
    {

    }
    public void Update()
    {
        if (isRewarded)
        {
            isRewarded = false;
            if (RewardHandle != null)
            {
                RewardHandle.Invoke();
                NotRewardHandle = null;
            }
        }
    }

    // SDK
    public void OnInitializeMax()
    {
        MaxSdk.SetHasUserConsent(true);
        MaxSdk.SetSdkKey(SdkKey);
        MaxSdk.SetTestDeviceAdvertisingIdentifiers(TestDevices);
        MaxSdk.InitializeSdk();

        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            InitSucceded = true;
            call();

            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += MaxOnInterstitialHiddenEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += MaxOnInterstitialLoadedHiddenEvent;
            MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnAdInterRevenuePaidEvent;

            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnAdDisplayed;
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnAdRewardRevenuePaidEvent;

            MaxSdkCallbacks.AppOpen.OnAdRevenuePaidEvent += OnAdAppopenRevenuePaidEvent;
        };
    }

    // First Call
    void call()
    {
        LoadInterstitial();
        LoadRewarded_Ad();
    }

    // Interstitial
    static int xyz_int;
    public void Btn_LS_Int()
    {
        if (Skip_Int == true)
        {
            // No ADs
        }
        else
        {
            if (xyz_int == 0)
            {
                StartCoroutine(ShowInterstetialDelay());
            }
            else
            {
                if (AdmobAdsManager.Instance._Name == true)
                {
                    StartCoroutine(ShowInterstetialDelay());
                }
                else
                {
                    Unity_Ads_Manager.Instance.Btn_Int_Show();
                }
            }

            //
            xyz_int = xyz_int + 1;
            if (xyz_int == 2)
            {
                xyz_int = 0;
            }
        }
    }
    void LoadInterstitial()
    {
        if (!MaxSdk.IsInterstitialReady(InterID))
            MaxSdk.LoadInterstitial(InterID);
    }
    WaitForSecondsRealtime secondsRealtime = new WaitForSecondsRealtime(.5f);
    bool OnTimeCallInterCall = false;
    IEnumerator ShowInterstetialDelay()
    {
        if (!OnTimeCallInterCall)
        {
            OnTimeCallInterCall = true;
            LoadInterstitial();
            yield return secondsRealtime;
            ShowInterstitial();
            yield return secondsRealtime;
            OnTimeCallInterCall = false;
        }
    }
    void ShowInterstitial()
    {
        if (!InitSucceded) return;
        if (MaxSdk.IsInterstitialReady(InterID))
        {
            AdmobAdsManager.ForeGroundedAD = true;
            MaxSdk.ShowInterstitial(InterID);
        }
        else
        {
            MaxSdk.LoadInterstitial(InterID);
        }
    }

    private void MaxOnInterstitialLoadedHiddenEvent(string arg1, MaxSdkBase.AdInfo arg2)
    {
        IsInterstitialAdReady = true;
    }
    private void MaxOnInterstitialHiddenEvent(string arg1, MaxSdkBase.AdInfo arg2)
    {
        IsInterstitialAdReady = false;
        MaxSdk.LoadInterstitial(InterID);
    }
    private void OnAdInterRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo impressionData)
    {

    }

    // Reward
    Action xXx;
    static int xyz_rew;
    public void Btn_LS_Rew(Action _Reward)
    {
        xXx = _Reward;
        if (Skip_Rew == true)
        {
            // No ADs
            xXx.Invoke();
        }
        else
        {
            if (xyz_rew == 0)
            {
                if (!OneTimeReward)
                {
                    OneTimeReward = true;
                    StartCoroutine(ShowRewardedDelay(xXx));
                }
            }
            else
            {
                if (AdmobAdsManager.Instance._Name == true)
                {
                    if (!OneTimeReward)
                    {
                        OneTimeReward = true;
                        StartCoroutine(ShowRewardedDelay(xXx));
                    }
                }
                else
                {
                    xXx.Invoke();
                    Unity_Ads_Manager.Instance.Btn_Rew_Show();
                }
            }

            //
            xyz_rew = xyz_rew + 1;
            if (xyz_rew == 2)
            {
                xyz_rew = 0;
            }
        }


        Invoke(nameof(_show), 0.8f);
    }
    void _show()
    {

        AdmobAdsManager.Instance.Btn_Reward_Done("");
    }
    void LoadRewarded_Ad()
    {
        MaxSdk.LoadRewardedAd(RewardedID);
    }
    bool CanShowReward()
    {
        return MaxSdk.IsRewardedAdReady(RewardedID);
    }
    public void _ShowRewardVideoSample()
    {
        Btn_LS_Rew(tempsucc);
    }
    public void _ShowRewardVideoifFailedRewardSample()
    {
        ShowRewardedVideo(tempsucc, tempFail);
    }
    void tempsucc()
    {
        Debug.Log("Ad Successfully reward Earn");
    }
    void tempFail()
    {
        Debug.Log("Ad Close/Fail reward Earn");
    }


    public void ShowRewardedVideo(Action _Reward, Action _NotReward)
    {
        if (!InitSucceded) return;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
        if (MaxSdk.IsRewardedAdReady(RewardedID))
        {
            RewardHandle = _Reward;
            NotRewardHandle = _NotReward;
            AdmobAdsManager.ForeGroundedAD = true;
            MaxSdk.ShowRewardedAd(RewardedID);
        }
        else
        {
            MaxSdk.LoadRewardedAd(RewardedID);
        }
    }
    bool IsRewardedVideo_Available()
    {
        if (!InitSucceded)
            return false;

        return MaxSdk.IsRewardedAdReady(RewardedID);
    }

    bool OneTimeReward = false;
    IEnumerator ShowRewardedDelay(Action _Reward)
    {
        if (!InitSucceded) yield return null;

        MaxSdk.LoadRewardedAd(RewardedID);
        yield return secondsRealtime;

        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
        if (MaxSdk.IsRewardedAdReady(RewardedID))
        {
            RewardHandle = _Reward;
            AdmobAdsManager.ForeGroundedAD = true;
            MaxSdk.ShowRewardedAd(RewardedID);
        }
        else
        {
            MaxSdk.LoadRewardedAd(RewardedID);
        }
        yield return secondsRealtime;
        OneTimeReward = false;
    }
    private void OnAdRewardRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo impressionData)
    {

    }
    private void OnRewardedAdReceivedRewardEvent(string arg1, MaxSdkBase.Reward arg2, MaxSdkBase.AdInfo arg3)
    {
        isRewarded = true;
    }
    private void OnAdDisplayed(string arg1, MaxSdkBase.AdInfo arg2)
    {
        isRewarded = false;
    }
    private void OnRewardedAdHiddenEvent(string arg1, MaxSdkBase.AdInfo arg2)
    {
        if (!isRewarded)
        {
            if (NotRewardHandle != null)
                NotRewardHandle.Invoke();
        }
        MaxSdk.LoadRewardedAd(RewardedID);
    }

    // App Open
    private void OnAdAppopenRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo impressionData)
    {

    }
    void Btn_AppOpen_AD()
    {
        if (Skip_App == true)
        {
            // No ADs
        }
        else
        {
            // Call
        }
    }
}