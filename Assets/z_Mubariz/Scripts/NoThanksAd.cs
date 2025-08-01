using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoThanksAd : MonoBehaviour
{
    [SerializeField] AdAfter40Sec ad;
    [SerializeField] GameObject loadInterstitialAdGameobject;
    [SerializeField] GameObject gameobjectToDiableifCantShowAd;

    private void Start()
    {
        
    }
    public void AdIfPossible()
    {
        if (ad.canShowAd)
        {
            loadInterstitialAdGameobject.SetActive(true);
        }
        else
        {
            gameobjectToDiableifCantShowAd.SetActive(false);
        }
    }
}
