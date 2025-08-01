using UnityEngine;
using UnityEngine.UI;
using System;
public class ModesAd : MonoBehaviour
{
    [SerializeField] GameObject modeSelectionGameobject;
    [SerializeField] GameObject loadingPanelGameobject;
    [SerializeField] Text coinsText;
    [SerializeField] GameObject notEnoughCoinsPanelPet;
    [SerializeField] GameObject notEnoughCoinsPanelGran;
    public int totalCoins;
    [SerializeField] GameObject rewardLoadingPanel;
    int startCoins = 20;

    public static event Action<int> OnCoinsUpdated;

    private void OnEnable()
    {
        totalCoins = PlayerPrefs.GetInt("MyCoins",0);
        UpdateCoins(totalCoins);
        if (PlayerPrefs.GetInt("NewGame") != 1)
        {
            AddCoins(startCoins);
            PlayerPrefs.SetInt("NewGame",1);
        }

    }
    public void func(int a)
    {
        if (a == 0)
        {
            if(AdmobAdsManager.Instance)
           AdmobAdsManager.Instance.ShowInterstitial();

            Invoke("Call", .1f);
        }

        if (a == 1)
        {
            if(AdmobAdsManager.Instance)
           AdmobAdsManager.Instance.ShowRewardedVideo(Call);

            Call();
        } 
       
    }

    void UpdateCoins(int coins)
    {
        coinsText.text = coins.ToString();
    }
    void Call()
    {
        modeSelectionGameobject.SetActive(false);
        loadingPanelGameobject.SetActive(true);

    }

    public void OnWatchRewardAd()
    {
        REWARDAndSELECT();
    }

    void Ad100Coins()
    {

        Sfx_Mainmenu.PlaySound(Sfx_Mainmenu.Instance.sellPurchase);
        totalCoins += 100;
        PlayerPrefs.SetInt("MyCoins", totalCoins);
        Debug.Log("Coins: " + totalCoins);
        coinsText.text = totalCoins.ToString();
        notEnoughCoinsPanelPet.SetActive(false);
        notEnoughCoinsPanelGran.SetActive(false);
    }

    public void DeductCoins(int coins)
    {
        totalCoins -= coins;
        PlayerPrefs.SetInt("MyCoins", totalCoins);
        coinsText.text = totalCoins.ToString();
    }

    public int CheckCoins()
    {
        return totalCoins;
    }

    void REWARDAndSELECT()
    {
        rewardLoadingPanel.SetActive(true);

        load_rew();
        Invoke(nameof(show_rew), Timer_xXx);

        DisGameobject();
    }
    private void DisGameobject()
    {
        rewardLoadingPanel.SetActive(false);
    }


    public void AddCoins(int amount)
    {
        //Sfx_Mainmenu.PlaySound(Sfx_Mainmenu.Instance.sellPurchase);
        totalCoins += amount;
        PlayerPrefs.SetInt("MyCoins", totalCoins);
        Debug.Log("Coins: " + totalCoins);
        coinsText.text = totalCoins.ToString();
        OnCoinsUpdated?.Invoke(totalCoins);
    }

    public void SaveCoins()
    {

    }

    float Timer_xXx;
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
            AdmobAdsManager.Instance.LoadRewardedVideo();
        }
    }
    void show_rew()
    {
        if (AdmobAdsManager.Instance.Ads_Googel_Max == true)
        {
            //MaxAdsManager.Instance.Btn_LS_Rew(Ad100Coins);
        }
        else
        {
            AdmobAdsManager.Instance.ShowRewardedVideo(Ad100Coins);
        }
    }

    
}