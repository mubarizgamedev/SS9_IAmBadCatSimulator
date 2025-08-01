using System;
using UnityEngine;

public class SleepTrigger : MonoBehaviour
{
    public static event Action OnCatSleep;
    public GameObject playerCamera;
    public GameObject sleepCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerCamera.SetActive(false);
            sleepCamera.SetActive(true);
            Invoke(nameof(Return), 5f);
        }
    }

    void Return()
    {
        sleepCamera.SetActive(false);
        playerCamera.SetActive(true);
        OnCatSleep?.Invoke();
    }
}
