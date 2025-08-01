using UnityEngine;

public class CheckPrivacyPolicy : MonoBehaviour
{
    public bool appOpen;
    [SerializeField] GameObject sceneHandler;
    public string sceneName;
    [SerializeField] GameObject privacyPolicyPanl;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] string privacyPolicyURL;

    void OnEnable()
    {
        //InAppload();
        if (PlayerPrefs.GetInt("PP") == 1)
        {
            sceneHandler.SetActive(true);
            loadingScreen.SetActive(true);
            //call();
        }
        else
        {
            privacyPolicyPanl.SetActive(true);
        }
    }

    public void CheckPrivacyPolicyAgreement()
    {
        PlayerPrefs.SetInt("PP", 1);
        privacyPolicyPanl.SetActive(false);
        sceneHandler.SetActive(true);
        call();
    }
    void call()
    {
        loadingScreen.SetActive(true);
        Invoke("InAppShow", 5f);
    }

    void InAppload()
    {
        //AdmobAdsManager.Instance.Btn_App_Load();
    }
    void InAppShow()
    {
        //AdmobAdsManager.Instance.Btn_App_Show();
    }
    public void OpenPrivacyPolicy()
    {
        Application.OpenURL(privacyPolicyURL);
    }
}
