using UnityEngine;
using UnityEngine.UI;
using System;

public class Items_Count : MonoBehaviour
{
    [SerializeField] Text keyCountText;
    [SerializeField] Text speacialsCountText;
    [SerializeField] Text healthText;
    [SerializeField] Text levelNumberText;
    [SerializeField] Image levelProgress;
    public ModesAd modeAd;
    public static event Action<int> OnCoinsUpdated;

    int keyCount;
    int diamondsAmount;

    private void Start()
    {
        LoadPlayerPrefs();
        ModesAd.OnCoinsUpdated += ModesAd_OnCoinsUpdated;
    }
    private void OnDestroy()
    {
        ModesAd.OnCoinsUpdated -= ModesAd_OnCoinsUpdated;
    }

    private void ModesAd_OnCoinsUpdated(int obj)
    {
        diamondsAmount = obj;
        PlayerPrefs.SetInt("MyCoins", diamondsAmount);
        PlayerPrefs.Save();
        speacialsCountText.text = diamondsAmount.ToString();
    }

    private void LoadPlayerPrefs()
    {
        keyCount = PlayerPrefs.GetInt("KeyCount", 0);
        diamondsAmount = PlayerPrefs.GetInt("MyCoins", 0);

        keyCountText.text = keyCount.ToString();
        speacialsCountText.text = diamondsAmount.ToString();
    }

    public void UpdateLevelNumber(string text)
    {
        levelNumberText.text = text;
    }

    public void IncrementKeyCount()
    {
        keyCount++;
        keyCountText.text = keyCount.ToString();
        PlayerPrefs.SetInt("KeyCount", keyCount);
        PlayerPrefs.Save();
    }

    public void IncrementDiamondCount()
    {
        diamondsAmount++;
        speacialsCountText.text = diamondsAmount.ToString();
        PlayerPrefs.SetInt("MyCoins", diamondsAmount);
        PlayerPrefs.Save();
    }

    public void UpdateLevelProgress(int currrentValue, int totalValue)
    {
        float progress = (float)currrentValue / totalValue;
        levelProgress.fillAmount = progress;
    }
    
    public void Ad50Coins()
    {
        modeAd.AddCoins(50);
    }

}
