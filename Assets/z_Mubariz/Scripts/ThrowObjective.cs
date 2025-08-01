using UnityEngine;
using UnityEngine.Events;

public class ThrowObjective : MonoBehaviour
{
    [SerializeField] string text;
    Update_UI update_UI;
    public UnityEvent OnComplete;
    [SerializeField] float timeToShowText;


    private void Start()
    {
        update_UI = FindFirstObjectByType<Update_UI>();
        update_UI.ShowTextUpdate(text, timeToShowText);
        PickableObject.OnObjectHitGranny += PickableObject_OnObjectHitGranny;
    }

    private void PickableObject_OnObjectHitGranny()
    {
       OnComplete?.Invoke();
    }

   
    
}
