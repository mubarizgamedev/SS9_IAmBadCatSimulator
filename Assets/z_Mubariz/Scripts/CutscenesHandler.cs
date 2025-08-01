using UnityEngine;
using UnityEngine.Playables;

public class CutscenesHandler : MonoBehaviour
{
    [SerializeField] PlayableDirector[] allPlayableDirectors;
    private void OnEnable()
    {
        PauseEveryOne();
    }

    void PauseEveryOne()
    {
        foreach (var t in allPlayableDirectors)
        {
            t.Pause();
        }
    }

    void ResumeEveryOne()
    {
        foreach (var t in allPlayableDirectors)
        {
            t.Resume();
        }
    }

    private void OnDisable()
    {
        ResumeEveryOne();
    }
}
