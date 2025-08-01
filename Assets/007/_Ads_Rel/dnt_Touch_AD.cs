using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class dnt_Touch_AD : MonoBehaviour
{
    public float Time_Mini = 0f;
    public float Time_Current;
    public float Time_Maxi = 60f;

    [Header("Coin")]
    public int coins;
    public GameObject GO_Coin;

    int xXx;

    void OnEnable()
    {
        gc_Off();
        cc();
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void cc()
    {
        //Debug.Log("Counter Coin__" + coins);
        coins = PlayerPrefs.GetInt("TotalCoins");
        Invoke("cc", 1f);
    }

    void gc_On()
    {
        GO_Coin.SetActive(true);
    }

    void gc_Off()
    {
        load_rv();
        GO_Coin.SetActive(false);
    }

    void Start()
    {
        task();
    }

    void task()
    {
        Time_Current = 0f;
        //Debug.Log("Counter = Start");
        InvokeRepeating("Counter", 1f, 1f);
    }

    void Counter()
    {
        Time_Current = Time_Current + 1f;
        //Debug.Log("Counter = " + Time_Current + " seconds");
        if (Time_Current >= Time_Maxi)
        {
            Debug.Log("Counter = Done");
            CancelInvoke("Counter");
            Get_Coins();
        }
    }

    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            re_set();
        }
    }

    void re_set()
    {
        CancelInvoke("Counter");
        Debug.Log("Counter = Reset");
        task();
    }

    public void Get_Coins()
    {
        Debug.Log("Counter = AD Call");
        Invoke(nameof(waitAD_now), 0.1f);
    }

    void waitAD_now()
    {
        show_rv();
    }
    void show_rv()
    {
        if (AdmobAdsManager.Instance._Name == true)
        {
            //MaxAdsManager.Instance.Btn_LS_Rew(Chk_Coins);
        }
        else
        {
            if (xXx == 0)
            {
                Unity_Ads_Manager.Instance.Btn_Rew_Show();
            }
            else
            {
                Unity_Ads_Manager.Instance.Btn_Int_Show();
            }
            Chk_Coins();
        }
    }
    void load_rv()
    {
        if(AdmobAdsManager.Instance)
        if (AdmobAdsManager.Instance._Name == true)
        {
            //ADsMax
        }
        else
        {
            if (xXx == 0)
            {
                Unity_Ads_Manager.Instance.Btn_Rew_Load();
            }
            else
            {
                Unity_Ads_Manager.Instance.Btn_Int_Load();
            }
        }
    }

    void Chk_Coins()
    {
        xXx = xXx + 1;
        if (xXx == 2)
        {
            xXx = 0;
        }
        Debug.Log("Counter = Reward AD");

        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + 1000);
        
        //Data.SaveData();
        re_set();
        gc_On();
    }
}