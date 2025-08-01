using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanInteract : MonoBehaviour
{
    public PlayerInteractor playerInteractor;
    bool playerInZone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

    private void Update()
    {
        if (playerInZone)
        {
            playerInteractor.CanInteract = true;
        }
        else
        {
            playerInteractor.CanInteract = false;
        }
    }
}
