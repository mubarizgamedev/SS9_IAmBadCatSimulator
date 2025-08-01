using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterActivation : MonoBehaviour
{
    [SerializeField] float timeToDisable;

    private void OnEnable()
    {
        Invoke(nameof(DisableGameObject), timeToDisable);
    }

    void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
}
