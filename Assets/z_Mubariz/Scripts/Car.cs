using UnityEngine;
using System;

public class Car : MonoBehaviour
{
    string GrannyTag = "Enemy";
    public static event Action OnToyHitGranny;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GrannyTag))
        {
            OnToyHitGranny?.Invoke();
        }
    }
}
