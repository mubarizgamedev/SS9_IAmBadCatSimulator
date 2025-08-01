using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] float timeAfterEnable;
    [SerializeField] float timeToDisableLoading;
    [SerializeField] GameObject loadingScreen;
    private void OnEnable()
    {
        Invoke(nameof(Load_Scene), timeAfterEnable);
        Invoke(nameof(DisableLoad), timeToDisableLoading);
    }

    void Load_Scene()
    {
        SceneManager.LoadScene(sceneName);
    }
    void DisableLoad()
    {
        loadingScreen.SetActive(false);
    }
}
