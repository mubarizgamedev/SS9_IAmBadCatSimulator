using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InappButtons : MonoBehaviour
{
    [Space(5)]
    [Header("Inapp Panels")]
    public GameObject allCatInappPanel;
    public GameObject allGranInappPanel;
    public GameObject allPetGranInappPanel;

    [Space(5)]
    [Header("Mainmenu")]
    public Button allCatInapp;
    public Button allGranInapp;
    public Button allCatAndGranInapp;

    [Space(5)]
    [Header("Selection")]
    public Button pet1InappButton;
    public Button pet2InappButton;
    public Button granny1InappButton;
    public Button granny2InappButton;

    [Space(5)]
    [Header("INAPP CALLS")]
    public Button getAllPet;
    public Button getAllGranny;
    public Button getAllPetGranny;

    private void Start()
    {
        allCatInapp.onClick.AddListener(() => InterstitialAdCall.Instance.StartLoading(()=> allCatInappPanel.SetActive(true)));
        allGranInapp.onClick.AddListener(() => InterstitialAdCall.Instance.StartLoading(()=> allGranInappPanel.SetActive(true)));
        allCatAndGranInapp.onClick.AddListener(() => InterstitialAdCall.Instance.StartLoading(()=> allPetGranInappPanel.SetActive(true)));

        pet1InappButton.onClick.AddListener(() => InterstitialAdCall.Instance.StartLoading(() => allCatInappPanel.SetActive(true)));
        pet2InappButton.onClick.AddListener(() => InterstitialAdCall.Instance.StartLoading(() => allCatInappPanel.SetActive(true)));
        granny1InappButton.onClick.AddListener(() => InterstitialAdCall.Instance.StartLoading(() => allGranInappPanel.SetActive(true)));
        granny2InappButton.onClick.AddListener(() => InterstitialAdCall.Instance.StartLoading(() => allGranInappPanel.SetActive(true)));

        getAllPet.onClick.AddListener(() => GameAppManager.instance.Unlock_All_Pets());
        getAllGranny.onClick.AddListener(() => GameAppManager.instance.Unlock_All_Grans());
        getAllPetGranny.onClick.AddListener(() => GameAppManager.instance.Btn_Buy_Everything());

    }
}
