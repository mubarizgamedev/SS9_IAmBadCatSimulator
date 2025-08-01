using System;
using UnityEngine;
using UnityEngine.UI;

public class PetSelectionMainMenu : MonoBehaviour
{
    public GameObject unlockAllinapp;
    public static PetSelectionMainMenu Instance { get; private set; }
    public GameObject[] catPrefabs;
    private GameObject currentCatInstance;
    public int currentIndex = 0;
    public GameObject unlockButton;
    public GameObject selectButton;
    public Transform spawnPoint;
    public ModesAd modesAdHandler;
    public GameObject notEnoughCoinsPanel;
    public GameObject unlockAllGrans;
    public GameObject rewardCoinsButton;
    public Text reqCoinsText;
    public GameObject reqCoinsGameobject;
    public GameObject showAdAndSelectFree;
    [SerializeField] GameObject[] allGreenUI;
    [SerializeField] GameObject[] allinner;
    [SerializeField] GameObject rewardLoadingPanel;
    [SerializeField] GameObject PetselectionRoom;
    [SerializeField] GameObject grannySelection;
    [SerializeField] GameObject grannySelectionRoom;
    [SerializeField] GameObject BannerRight;

    [Space(5)]
    public GameObject pet1Button, pet2Button;


    [Space(5)]
    public GameObject fan;

    private int[] petPrices = { 0, 1, 2, 800, 1200, 2000 };
    void Start()
    {
        currentIndex = PlayerPrefs.GetInt("SelectedCatIndex", 0); // Load saved index
        int noAds = PlayerPrefs.GetInt("noADS");
        if (noAds == 1)
        {
            unlockAllinapp.SetActive(false);
        }
    }
    private void Awake()
    {
        CheckForInApp();
    }

    private void OnEnable()
    {
        PlayerPrefs.SetInt("SelectedCatIndex", 0);
        currentIndex = PlayerPrefs.GetInt("SelectedCatIndex", 0);
        SpawnPet();
        currentIndex = 0;
        CurrentBox();
    }

    public void MoveToPetAtIndex(int petIndex)
    {
        currentIndex = petIndex;
        SpawnPet();
    }

    public void MoveLeft()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = catPrefabs.Length - 1; // Loop to last cat
        }
        SpawnPet();
        CurrentBox();
    }

    public void MoveRight()
    {
        currentIndex++;
        if (currentIndex >= catPrefabs.Length)
        {
            currentIndex = 0; // Loop to first cat
        }
        SpawnPet();
        CurrentBox();
    }

    public void SelectPet()
    {
        if (IsPetUnlocked(currentIndex))
        {
            PlayerPrefs.SetInt("SelectedCatIndex", currentIndex);
            PlayerPrefs.Save();
            Debug.Log("Cat " + currentIndex + " selected!");
            Destroy(currentCatInstance);
            //NextPanel();
            //AdmobAdsManager.Instance.Btn_Reward_Done("REWARD GRANTED");
        }
        else
        {
            Debug.Log("This cat is locked! Unlock it first.");
        }
    }

    public void ShowAdAndSelectPet()
    {
        //AdmobAdsManager.Instance.ShowRewardedVideo(SelectPet);

        REWARDAndSELECT();
    }

    public void UnlockPet()
    {
        //AdmobAdsManager.Instance.ShowRewardedVideo(UnlockAfterAd);
        REWARDandUnlockAfterAD();
    }

    void UnlockAfterAd()
    {
        fan.SetActive(true);
        Sfx_Mainmenu.PlaySound(Sfx_Mainmenu.Instance.sellPurchase);
        PlayerPrefs.SetInt("CatUnlocked_" + currentIndex, 1);
        PlayerPrefs.Save();
        Debug.Log("Cat " + currentIndex + " unlocked!");
        UpdateButtons();
        NextPanel();
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.Btn_Reward_Done("REWARD GRANTED");
        }
    }

    public void SpawnPet()
    {
        if (currentCatInstance != null)
        {
            Destroy(currentCatInstance);
        }
        if (Sfx_Mainmenu.Instance)
            Sfx_Mainmenu.PlaySound(Sfx_Mainmenu.Instance.petSelect, 1);
        currentCatInstance = Instantiate(catPrefabs[currentIndex], spawnPoint.position, Quaternion.identity);
        currentCatInstance.transform.rotation = Quaternion.Euler(0, 225, 0);
        UpdateButtons();
    }

    void UpdateButtons()
    {
        if (IsPetUnlocked(currentIndex))
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
        UpdateReqCoinsText(petPrices[currentIndex]);
        ShowButtonsIfPetLocked();
    }

    bool IsPetUnlocked(int index)
    {
        if (index == 0) return true;
        return PlayerPrefs.GetInt("CatUnlocked_" + index, 0) == 1;
    }
    public void DestroyCurrentPet()
    {
        Destroy(currentCatInstance);
    }

    public void UnlockAllPets()
    {
        for (int i = 0; i < catPrefabs.Length; i++)
        {
            PlayerPrefs.SetInt("CatUnlocked_" + i, 1);
        }
        PlayerPrefs.Save();
        Debug.Log("All pets unlocked!");
        UpdateButtons();
    }

    public void BuyNow()
    {
        int playerCoins = PlayerPrefs.GetInt("MyCoins", 0);
        int petPrice = petPrices[currentIndex];

        if (IsPetUnlocked(currentIndex))
        {
            Debug.Log("Pet already unlocked!");
            return;
        }

        if (playerCoins >= petPrice)
        {
            playerCoins -= petPrice;
            PlayerPrefs.SetInt("MyCoins", playerCoins);
            PlayerPrefs.SetInt("CatUnlocked_" + currentIndex, 1);
            PlayerPrefs.Save();
            UpdateButtons();
            Sfx_Mainmenu.PlaySound(Sfx_Mainmenu.Instance.sellPurchase);
            modesAdHandler.DeductCoins(petPrices[currentIndex]);
        }
        else
        {

            notEnoughCoinsPanel.SetActive(true);
            //DestroyCurrentPet();
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
            UnlockAllPets();
        }
    }

    void REWARDAndSELECT()
    {
        fan.SetActive(false);
        DestroyCurrentPet();
        BannerRight.SetActive(false);
        rewardLoadingPanel.SetActive(true);

        load_rew();
        Rew_xXx = SelectPet;
        Invoke(nameof(show_rew), Timer_xXx);

        Invoke(nameof(DisGameobject), Timer_xXx + 0.3f);
    }
    private void DisGameobject()
    {
        rewardLoadingPanel.SetActive(false);
        BannerRight.SetActive(true);
    }

    void REWARDandUnlockAfterAD()
    {
        BannerRight.SetActive(false);
        rewardLoadingPanel.SetActive(true);

        load_rew();
        Rew_xXx = UnlockAfterAd;
        Invoke(nameof(show_rew), Timer_xXx);

        Invoke(nameof(DisGameobject), Timer_xXx + 0.3f);
    }

    void NextPanel()
    {
        gameObject.SetActive(false);
        PetselectionRoom.SetActive(false);
        DestroyCurrentPet();
        grannySelection.SetActive(true);
        grannySelectionRoom.SetActive(true);
    }

    void CurrentBox()
    {
        if (currentIndex == 0)
        {
            allGreenUI[0].SetActive(true);
            allGreenUI[1].SetActive(false);
            allGreenUI[2].SetActive(false);

            allinner[0].SetActive(false);
            allinner[1].SetActive(true);
            allinner[2].SetActive(true);
            MoveToPetAtIndex(0);
        }
        else if (currentIndex == 1)
        {
            allGreenUI[0].SetActive(false);
            allGreenUI[1].SetActive(true);
            allGreenUI[2].SetActive(false);


            allinner[0].SetActive(true);
            allinner[1].SetActive(false);
            allinner[2].SetActive(true);

            MoveToPetAtIndex(1);
        }
        else if (currentIndex == 2)
        {
            allGreenUI[0].SetActive(false);
            allGreenUI[1].SetActive(false);
            allGreenUI[2].SetActive(true);


            allinner[0].SetActive(true);
            allinner[1].SetActive(true);
            allinner[2].SetActive(false);

            MoveToPetAtIndex(2);
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

    public void UnlockPetAtIndex(int index)
    {
        if (index < 0 || index >= catPrefabs.Length)
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
            if (!IsPetUnlocked(1))
            {
                pet1Button.SetActive(true);
                pet2Button.SetActive(false);
            }
            else
            {
                pet1Button.SetActive(false);
            }

        }
        else if (currentIndex == 2)
        {
            // Check if pet at index 2 is locked
            if (!IsPetUnlocked(2))
            {
                pet2Button.SetActive(true);
                pet1Button.SetActive(false);
            }
            else
            {
                pet2Button.SetActive(false);
            }
        }
        else
        {
            pet1Button.SetActive(false);
            pet2Button.SetActive(false);
        }
    }

}