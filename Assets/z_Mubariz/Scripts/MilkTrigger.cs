using System;
using UnityEngine;

public class MilkTrigger : MonoBehaviour
{
    public static event Action OnCatDrink;
    public GameObject playerCamera;
    public GameObject milkCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerCamera.SetActive(false);
            milkCamera.SetActive(true);
            Invoke(nameof(Return), 5f);
        }
    }

    void Return()
    {
        milkCamera.SetActive(false);
        playerCamera.SetActive(true);
        OnCatDrink?.Invoke();
    }

}
