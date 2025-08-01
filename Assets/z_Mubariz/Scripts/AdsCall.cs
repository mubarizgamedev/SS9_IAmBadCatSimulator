using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsCall : MonoBehaviour
{
    public Button pauseButton;


    public GameObject pausePanel;
    public GameObject failPanel;

    public GameObject granny;
    public GameObject bloodOverlayPanel;
    public AdAfter40Sec adAfter40Sec;
    public SpecialAttack_PopUp specialAttack_Pop;
    private void Start()
    {
        EnemyHandler.OnGrannyHitCat += Fail;

        pauseButton.onClick.AddListener(() => 
        {
            MaxAdsManager.Instance.Btn_LS_Int();
            pausePanel.SetActive(true);
            granny.SetActive(false);
            EnemyHandler.canAttackCat = false;
            adAfter40Sec.ResetAdTimer();
            specialAttack_Pop.ResetSpeacialTimer();
        });
    }

    void Fail()
    {
        bloodOverlayPanel.SetActive(true);
        StartCoroutine(FailCoroutine());
    }

    IEnumerator FailCoroutine()
    {
        yield return new WaitForSeconds(4f);
        granny.SetActive(false);
        failPanel.SetActive(true);
        MaxAdsManager.Instance.Btn_LS_Int();
        EnemyHandler.canAttackCat = false;
        adAfter40Sec.ResetAdTimer();
        specialAttack_Pop.ResetSpeacialTimer();
    }
}

