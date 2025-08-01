using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public UnityEvent OnPause;
    public UnityEvent OnResume;
    public UnityEvent OnReset;

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        EnemyHandler.canAttackCat = false;
        OnPause?.Invoke();
    }
    public void Resume()
    {
        EnemyHandler.canAttackCat = true;
        Time.timeScale = 1; 
        OnResume?.Invoke(); 
    }
    public void Reset()
    {
        Time.timeScale = 1;
        OnResume?.Invoke();
    }

    public void TimeScale()
    {
        Time.timeScale = 1;
    }
}
