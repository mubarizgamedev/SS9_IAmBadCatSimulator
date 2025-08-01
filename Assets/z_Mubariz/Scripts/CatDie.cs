using UnityEngine;
using System.Collections;

public class CatDie : MonoBehaviour
{
    [SerializeField] Animator catAnimator;
    [SerializeField] GameObject parentCat;
    public float disableAfterDie;
    public void ChangeState()
    {
        SFX_Manager.PlaySound(SFX_Manager.Instance.catCrySound);
        SFX_Manager.PlaySound(SFX_Manager.Instance.catHitSound);
        catAnimator.SetBool("Die", true);
    }

    public void SetActiveFalse()
    {
        StartCoroutine(DisableAfterDelay());
    }

    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(9f);
        parentCat.SetActive(false);
    }

}
