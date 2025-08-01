using System;
using UnityEngine;

public class CatBed : MonoBehaviour
{

    public static event Action OnCatBedHit;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnCatBedHit?.Invoke();
        }
    }
}
