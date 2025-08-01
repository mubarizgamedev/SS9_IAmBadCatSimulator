using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelCompletePanel : MonoBehaviour
{
    [SerializeField] Objective_Manager objective_Manager;

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void NextLevel()
    {
        gameObject.SetActive(false);
        objective_Manager.NextObjective();
    }
}
