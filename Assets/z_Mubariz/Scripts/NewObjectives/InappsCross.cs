using UnityEngine;
using UnityEngine.UI;

public class InappsCross : MonoBehaviour
{
    public AdAfter40Sec adAfter40Sec;

    [Header("Panel 1")]
    public Button btnInappCross;
    public GameObject inappPanel;
    [Header("Panel 2")]
    public Button btnInappCross2;
    public GameObject inappPanel2;

    public SpecialAttack_PopUp SpecialAttack_PopUp;
    public GameObject[] specialAttacksPanels;


    private void Start()
    {
        btnInappCross.onClick.AddListener(OnInappCrossButtonClicked);
        btnInappCross2.onClick.AddListener(OnInappCrossButtonClicked);
    }

    void OnInappCrossButtonClicked()
    {
        InterstitialAdCall.Instance.StartLoading(Work);
    }

    void Work()
    {
        adAfter40Sec.ResetAdTimer();
        inappPanel.SetActive(false);
        inappPanel2.SetActive(false);
        DisableAllPanels();
    }

    void DisableAllPanels()
    {
        foreach (GameObject panel in specialAttacksPanels)
        {
            panel.SetActive(false);
            SpecialAttack_PopUp.ResetSpeacialTimer();
        }
    }
}
