using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour
{
    [SerializeField] GameObject loadingPanel;
    public void EnterGamePlayScene()
    {
        loadingPanel.SetActive(false);
        SceneManager.LoadScene("GamePlay");
    }
}
