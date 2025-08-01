using UnityEngine;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    public bool allowAppOpen;
    [SerializeField] string sceneName;
    [SerializeField] float timeToLoadAppOpen = 3f;
    [SerializeField] float timeToShowAppOpen = 6f;
    [SerializeField] float timeToLoadNextScene = 9f;
    private void OnEnable()
    {
        Invoke(nameof(Load_Scene), timeToLoadNextScene);
        if (allowAppOpen)
        {
            Invoke(nameof(LoadAppOpen), timeToLoadAppOpen);
            Invoke(nameof(ShowAppOpen), timeToShowAppOpen);
        }
    }

    void Load_Scene()
    {
        SceneManager.LoadScene(sceneName);
    }

    void LoadAppOpen()
    {
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.Btn_App_Load();
        }
    }
    void ShowAppOpen()
    {
        if (AdmobAdsManager.Instance)
        {
            AdmobAdsManager.Instance.Btn_App_Show();
        }
    }


    
}
