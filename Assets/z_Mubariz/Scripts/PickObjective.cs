using UnityEngine;
using UnityEngine.Events;

public class PickObjective : MonoBehaviour
{
    [SerializeField] string text;
    public UnityEvent OnObjectPicked;
    [SerializeField] Update_UI updateUI;
    [SerializeField] float timeToShowText;


    private void Start()
    {
        updateUI.ShowTextUpdate(text, timeToShowText);
        ObjectPicker.ObjectPicked += ObjectPicker_ObjectPicked;
    }

    private void ObjectPicker_ObjectPicked()
    {
        OnObjectPicked?.Invoke();
    }
}
