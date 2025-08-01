using UnityEngine;
using System;

public class TouchCheck : MonoBehaviour
{
    public static event Action OnTouch;

    bool touched;
    [SerializeField] GameObject uiUpdate;

    private void Update()
    {
        if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            OnTouch?.Invoke();
            touched = true;
            HasTouched();
        }
    }

    void HasTouched()
    {
        if (touched)
        {
            Invoke(nameof(After2Sec), 3.5f);
        }

    }

    void After2Sec()
    {
        uiUpdate.SetActive(false);
        touched = false;
    }
}
