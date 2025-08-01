using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuToGamePlay : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    public void EnterGameplayScene()
    {
        loadingScreen.SetActive(false);
        SceneManager.LoadScene("GamePlay");
    }
}
