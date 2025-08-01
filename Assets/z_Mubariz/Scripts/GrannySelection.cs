using UnityEngine;
using UnityEngine.UI;
using System;

public class GrannySelection : MonoBehaviour
{
    public GameObject unlockAllinapp;
    public static GrannySelection Instance { get; private set; }
    public GameObject[] grannyPrefabs;
    private GameObject currentGrannyInstance;
    public int currentIndex = 0;
    public GameObject unlockButton;
    public GameObject selectButton;
    public GameObject unlockAllGrans;
    public GameObject rewardCoinsButton;
    public Transform spawnPoint;
    public GameObject notEnoughCoinsPanel;
    public Text reqCoinsText;
    public GameObject reqCoinsGameobject;
    public ModesAd modeAdsHandler;
    public GameObject showAdAndSelectFree;
    [SerializeField] GameObject[] greenUI;
    [SerializeField] GameObject[] allinner;
    [SerializeField] GameObject rewardLoadingPanel;
    [SerializeField] GameObject loadnigScreen;
    [SerializeField] GameObject grannySelectionRoom;
    [SerializeField] GameObject grannySelection;
    [SerializeField] GameObject BannerRight;
    [SerializeField] GameObject coinspanel;

    public GameObject gran1Button, gran2Button;


    private int[] grannyPrices = { 0, 1 , 2 , 1000 };

    public void DisableGreenUI()
    {
        foreach (var item in greenUI)
        {
            item.SetActive(false);
        }
    }

    private void Awake()
    {
        CheckForInApp();
    }

    void Start()
    {
        currentIndex = PlayerPrefs.GetInt("SelectedGrannyIndex", 0);
        int noAds = PlayerPrefs.GetInt("noADS");
        if (noAds == 1)
        {
            unlockAllinapp.SetActive(false);
        }
    }
    private void OnEnable()
    {
        PlayerPrefs.SetInt("SelectedGrannyIndex", 0);
        currentIndex = PlayerPrefs.GetInt("SelectedGrannyIndex", 0);
        SpawnGranny();
        currentIndex = 0;
        CurrentBox();
    }

    public void MoveLeft()
    {

        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = grannyPrefabs.Length - 1;
        }
        SpawnGranny();
        CurrentBox();
    }

    public void MoveRight()
    {
        currentIndex++;
        if (currentIndex >= grannyPrefabs.Length)
        {
            currentIndex = 0;
        }
        SpawnGranny();
        CurrentBox();
    }

    public void MoveToIndex(int indexOfGran)
    {
        currentIndex = indexOfGran;
        SpawnGranny();
    }

    public void SelectGranny()
    {
        if (IsGrannyUnlocked(currentIndex))
        {
            PlayerPrefs.SetInt("SelectedGrannyIndex", currentIndex);
            PlayerPrefs.Save();
            Debug.Log("Granny " + currentIndex + " selected!");
            Destroy(currentGrannyInstance);
            //NextPanel();

        }
        else
        {
            Debug.Log("This Granny is locked! Unlock her first.");
        }
    }

    public void ShowAdAndSelectGranny()
    {
        //AdmobAdsManager.Instance.ShowRewardedVideo(SelectGranny);

        REWARDAndSELECT();
    }

    public void UnlockGranny()
    {
        //AdmobAdsManager.Instance.ShowRewardedVideo(UnlockGrannyAfterAd);

        REWARDandUnlockAfterAD();
    }

    void UnlockGrannyAfterAd()
    {
        Sfx_Mainmenu.PlaySound(Sfx_Mainmenu.Instance.sellPurchase);
        PlayerPrefs.SetInt("GrannyUnlocked_" + currentIndex, 1);
        PlayerPrefs.Save();
        Debug.Log("Granny " + currentIndex + " unlocked!");
        UpdateButtons();
        NextPanel();
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.Btn_Reward_Done("REWARD GRANTED");
        }

    }

    public void SpawnGranny()
    {
        if (currentGrannyInstance != null)
        {
            Destroy(currentGrannyInstance);
        }
        if (Sfx_Mainmenu.Instance)
            Sfx_Mainmenu.PlaySound(Sfx_Mainmenu.Instance.granSelect, 0.25f);
        currentGrannyInstance = Instantiate(grannyPrefabs[currentIndex], spawnPoint.position, Quaternion.identity);
        currentGrannyInstance.transform.rotation = Quaternion.Euler(0, 180, 0);
        UpdateButtons();
        //CurrentBox();
    }

    void UpdateButtons()
    {
        if (IsGrannyUnlocked(currentIndex))
        {
            showAdAndSelectFree.SetActive(true);
            selectButton.SetActive(true);
            unlockButton.SetActive(false);
            unlockAllGrans.SetActive(false);
            rewardCoinsButton.SetActive(false);
            reqCoinsGameobject.SetActive(false);

        }
        else
        {
            showAdAndSelectFree.SetActive(false);
            selectButton.SetActive(false);
            unlockButton.SetActive(true);
            unlockAllGrans.SetActive(true);
            rewardCoinsButton.SetActive(true);
            reqCoinsGameobject.SetActive(true);
        }
        UpdateReqCoinsText(grannyPrices[currentIndex]);
        ShowButtonsIfPetLocked();
    }

    bool IsGrannyUnlocked(int index)
    {
        if (index == 0) return true;
        return PlayerPrefs.GetInt("GrannyUnlocked_" + index, 0) == 1;
    }

    public void DestroyCurrentGranny()
    {
        Destroy(currentGrannyInstance);
    }

    public void UnlockAllGarns()
    {
        for (int i = 0; i < grannyPrefabs.Length; i++)
        {
            PlayerPrefs.SetInt("GrannyUnlocked_" + i, 1); // Unlock every pet
        }
        PlayerPrefs.Save();
        Debug.Log("All pets unlocked!");
        UpdateButtons();
    }

    public void BuyNow()
    {
        int playerCoins = PlayerPrefs.GetInt("MyCoins", 0);
        int grannyPrice = grannyPrices[currentIndex];

        if (IsGrannyUnlocked(currentIndex))
        {
            Debug.Log("Granny already unlocked!");
            return;
        }

        if (playerCoins >= grannyPrice)
        {
            playerCoins -= grannyPrice; // Deduct coins
            PlayerPrefs.SetInt("MyCoins", playerCoins); // Fix key from "Coins" to "MyCoins"
            PlayerPrefs.SetInt("GrannyUnlocked_" + currentIndex, 1);
            PlayerPrefs.Save();
            UpdateButtons();
            Sfx_Mainmenu.PlaySound(Sfx_Mainmenu.Instance.sellPurchase);
            modeAdsHandler.DeductCoins(grannyPrices[currentIndex]);
        }
        else
        {
            //not enough coins
            notEnoughCoinsPanel.SetActive(true);
            //DestroyCurrentGranny();
        }
    }

    void UpdateReqCoinsText(int coins)
    {
        reqCoinsText.text = coins.ToString() + "$";
    }


    void CheckForInApp()
    {
        if (PlayerPrefs.GetInt("noADS") == 1)
        {
            UnlockAllGarns();
        }
    }

    void REWARDAndSELECT()
    {
        BannerRight.SetActive(false);
        rewardLoadingPanel.SetActive(true);

        load_rew();
        Rew_xXx = SelectGranny;
        Invoke(nameof(show_rew), Timer_xXx);

        Invoke(nameof(DisGameobject), Timer_xXx + 0.3f);
    }
    private void DisGameobject()
    {
        BannerRight.SetActive(true);
        rewardLoadingPanel.SetActive(false);
    }

    void REWARDandUnlockAfterAD()
    {
        BannerRight.SetActive(false);
        rewardLoadingPanel.SetActive(true);

        load_rew();
        Rew_xXx = UnlockGrannyAfterAd;
        Invoke(nameof(show_rew), Timer_xXx);

        DisGameobject();
    }

    void NextPanel()
    {
        grannySelectionRoom.SetActive(false);
        grannySelection.SetActive(false);
        coinspanel.SetActive(false);
        loadnigScreen.SetActive(true);
    }

    void CurrentBox()
    {
        
        if (currentIndex == 0)
        {
            MoveToIndex(0);
            greenUI[0].SetActive(true);
            greenUI[1].SetActive(false);
            greenUI[2].SetActive(false);
            allinner[0].SetActive(false);
            allinner[1].SetActive(true);
            allinner[2].SetActive(true);
        }
        else if (currentIndex == 1)
        {
            MoveToIndex(1);
            greenUI[0].SetActive(false);
            greenUI[1].SetActive(true);
            greenUI[2].SetActive(false);
            allinner[0].SetActive(true);
            allinner[1].SetActive(false);
            allinner[2].SetActive(true);
        }
        else if (currentIndex == 2)
        {
            MoveToIndex(2);
            greenUI[0].SetActive(false);
            greenUI[1].SetActive(false);
            greenUI[2].SetActive(true);

            allinner[0].SetActive(true);
            allinner[1].SetActive(true);
            allinner[2].SetActive(false);
        }
        else if (currentIndex == 3)
        {
            MoveToIndex(3);
            greenUI[3].SetActive(true);
        }
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
            AdmobAdsManager.Instance.LoadRewardedVideo();
        }
    }
    void show_rew()
    {
        if (AdmobAdsManager.Instance.Ads_Googel_Max == true)
        {
            //MaxAdsManager.Instance.Btn_LS_Rew(Chk_Chk);
        }
        else
        {
            AdmobAdsManager.Instance.ShowRewardedVideo(Chk_Chk);
        }
    }

    public void UnlockGranAtIndex(int index)
    {
        if (index < 0 || index >= grannyPrefabs.Length)
        {
            Debug.LogWarning("Invalid pet index: " + index);
            return;
        }

        PlayerPrefs.SetInt("CatUnlocked_" + index, 1);
        PlayerPrefs.Save();

        Debug.Log("Pet at index " + index + " unlocked!");

        // If the current index is the same as the unlocked index, update buttons
        if (currentIndex == index)
        {
            UpdateButtons();
        }
    }
    public void ShowButtonsIfPetLocked()
    {
        if (currentIndex == 1)
        {
            // Check if pet at index 1 is locked
            if (!IsGrannyUnlocked(1))
            {
                gran1Button.SetActive(true);
                gran2Button.SetActive(false);
            }
            else
            {
                gran1Button.SetActive(false);
            }

        }
        else if (currentIndex == 2)
        {
            // Check if pet at index 2 is locked
            if (!IsGrannyUnlocked(2))
            {
                gran2Button.SetActive(true);
                gran1Button.SetActive(false);
            }
            else
            {
                gran2Button.SetActive(false);
            }
        }
        else
        {
            gran1Button.SetActive(false);
            gran2Button.SetActive(false);
        }
    }
}