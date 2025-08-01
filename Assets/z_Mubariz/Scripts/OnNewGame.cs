using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class OnNewGame : MonoBehaviour
{
    public bool skipCutscene;
    public bool testingForGameplay;
    public Transform catTransform;
    public GameObject firsCutscene;
    public PlayableDirector firstSceneDirector;
    public GameObject Pet;
    public UnityEvent OnNewGameEvents;
    public UnityEvent OnPrevGameEvents;



    
    private void Start()
    {
        firstSceneDirector.stopped += FirstSceneDirector_stopped;
    }

    private void OnDisable()
    {
        firstSceneDirector.stopped -= FirstSceneDirector_stopped;
    }

    private void FirstSceneDirector_stopped(PlayableDirector obj)
    {
        firstSceneDirector.gameObject.SetActive(false);
        Pet.SetActive(true);
        OnNewGameEvents?.Invoke();        
    }

    private void OnEnable()
    {        
        Invoke("Enable", 0.2f);
    }

    void Enable()
    {
        if (testingForGameplay)
        {
            firstSceneDirector.gameObject.SetActive(false);
            Pet.SetActive(true);
            OnPrevGameEvents?.Invoke();
            return;
        }
        if (skipCutscene)
        {
            if (PlayerPrefs.GetInt("Tutorial") == 1)
            {
                firstSceneDirector.gameObject.SetActive(false);
                Pet.SetActive(true);
                OnPrevGameEvents?.Invoke();
            }
            else
            {
                firstSceneDirector.gameObject.SetActive(false);
                Pet.SetActive(true);
                OnNewGameEvents?.Invoke();
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("Tutorial") == 1)
            {
                firstSceneDirector.gameObject.SetActive(false);
                Pet.SetActive(true);
                OnPrevGameEvents?.Invoke();
            }
            else
            {
                Pet.SetActive(false);
                firstSceneDirector.gameObject.SetActive(true);
                firstSceneDirector.Play();
            }

        }
    }
}
