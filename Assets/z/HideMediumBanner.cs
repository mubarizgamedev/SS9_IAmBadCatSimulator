
using UnityEngine;

public class HideMediumBanner : MonoBehaviour
{
    public bool hideOnEnable;
    public bool hideOnDisable;

    private void OnEnable()
    {
        if (hideOnEnable)
            Invoke(nameof(Call), 1f);
    }
    private void OnDisable()
    {
        if (hideOnDisable)
            Invoke(nameof(Call), 1f);
    }
    void Call()
    {
        if(AdmobAdsManager.Instance)
            AdmobAdsManager.Instance.hideMediumBanner();
    }
}
