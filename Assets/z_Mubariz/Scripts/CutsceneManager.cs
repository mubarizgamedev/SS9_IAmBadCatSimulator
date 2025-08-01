using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance { get; private set; }

    public PlayableDirector[] cutscenes;

    private void Awake()
    {
        // Ensure singleton instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Static functions to be called globally
    public static void PauseAllCutscenes()
    {
        foreach (var cutscene in Instance.cutscenes)
        {
            cutscene.Pause();
        }
    }

    public static void ResumeAllCutscenes()
    {
        foreach (var cutscene in Instance.cutscenes)
        {
            cutscene.Resume();
        }
    }
}
