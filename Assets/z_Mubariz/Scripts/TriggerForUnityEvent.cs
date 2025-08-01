using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerForUnityEvent : MonoBehaviour
{
    public UnityEvent actionTodoAfterTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            actionTodoAfterTriggerEnter?.Invoke();
            Destroy(gameObject);
        }
    }
}
