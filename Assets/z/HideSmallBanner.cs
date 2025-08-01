using UnityEngine;

public class HideSmallBanner : MonoBehaviour
{
    private void OnEnable()
    {
        if (AdmobAdsManager.Instance)
            AdmobAdsManager.Instance.Btn_Hide_Bottom();
    }
    
    void Start()
    {
        if(AdmobAdsManager.Instance)
        AdmobAdsManager.Instance.Btn_Hide_Bottom();
    }
}
