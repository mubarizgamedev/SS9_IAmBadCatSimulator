
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameplay : MonoBehaviour
{
    public string sceneName;
    public void OnEnable()
    {
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.LoadInterstitial();
        }
        Invoke(nameof(ShowAdAndGameplay), 5.8f);
        Invoke(nameof(LoadScene), 6f);
    }

    void ShowAdAndGameplay()
    {
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.ShowInterstitial();
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
