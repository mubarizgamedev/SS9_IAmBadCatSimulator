using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelFailPanel : MonoBehaviour
{
    [SerializeField] string currentSceneName;
    [SerializeField] string mainMenuSceneName;

    public UnityEvent OnRevive;

    public void Retry()
    {
        Load_Scene(currentSceneName);
    }
    public void Home()
    {
        Load_Scene(mainMenuSceneName);
    }

    public void Revive()
    {
        OnRevive?.Invoke();
    }


    void Load_Scene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
