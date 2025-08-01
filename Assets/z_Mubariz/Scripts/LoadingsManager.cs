using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingsManager : MonoBehaviour
{
    [SerializeField] GameObject[] allLoading;

    private void Update()
    {
        if (AnyLoadingOn())
        {
            CutsceneManager.PauseAllCutscenes();
        }
        else
        {
            CutsceneManager.ResumeAllCutscenes();
        }
    }

    bool AnyLoadingOn()
    {
        foreach (GameObject go in allLoading)
        {
            if (go.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

}
