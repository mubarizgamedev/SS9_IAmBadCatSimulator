using System;
using UnityEngine;

public class TotalCoins : MonoBehaviour
{
    [SerializeField] int totalCoins;
    private void OnEnable()
    {
        
    }

    public void Btn_Reward()
    {
        Debug.Log("Total Coins are :" + totalCoins);
        CallRewarded();
    }
    
    void CallRewarded()
    {
       AdmobAdsManager.Instance.ShowRewardedVideo(DoubleCoins);
    }

    void DoubleCoins()
    {
        totalCoins *= 2;
        Debug.Log("Coins Doubled! New total: " + totalCoins);
    }
}
