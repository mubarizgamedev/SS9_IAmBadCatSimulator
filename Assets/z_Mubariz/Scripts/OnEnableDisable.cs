using UnityEngine;
using UnityEngine.Events;

public class OnEnableDisable : MonoBehaviour
{
    public UnityEvent OnEnableEvents;
    public UnityEvent OnDisableEvents;
    private void OnEnable()
    {
        OnEnableEvents?.Invoke();
    }

    private void OnDisable()
    {
        OnDisableEvents?.Invoke();
    }
}
