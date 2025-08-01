using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
{

    public static GameStateManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Event declarations
    public event Action OnMissionFailed;
    public event Action OnMissionCompleted;
    public event Action OnGamePaused;
    public event Action OnGameResumed;
    public event Action OnGameRestarted;

    // Methods to invoke events
    public void MissionFailed()
    {
        OnMissionFailed?.Invoke();
        Debug.Log("Mission Failed");
    }

    public void MissionCompleted()
    {
        OnMissionCompleted?.Invoke();
        Debug.Log("Mission Completed");
    }

    public void PauseGame()
    {
        OnGamePaused?.Invoke();
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        OnGameResumed?.Invoke();
        Debug.Log("Game Resumed");
    }

    public void RestartGame()
    {
        OnGameRestarted?.Invoke();
        Debug.Log("Game Restarted");
    }
}
