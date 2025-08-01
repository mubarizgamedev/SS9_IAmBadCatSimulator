using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunAwayGrannyGuide : MonoBehaviour
{
    [SerializeField] string text;
    Update_UI update_UI;
    public UnityEvent OnComplete;
    [SerializeField] float timeToShowText;
    [SerializeField] float timeAfterCongratsToResume;
    public UnityEvent OnAfterGuideCongratsDone;

    private void Start()
    {

        update_UI = FindFirstObjectByType<Update_UI>();
        update_UI.ShowTextUpdate(text, timeToShowText);
        Invoke(nameof(Completed), 5f);
    }

    

    private void Completed()
    {
        OnComplete?.Invoke();
        
        Invoke(nameof(CallEvent), timeAfterCongratsToResume);
    }

    void CallEvent()
    {
        OnAfterGuideCongratsDone?.Invoke();
    }

}
