using System;
using UnityEngine;

public class Pots : MonoBehaviour
{
    string GrannyTag = "Enemy";
    public static event Action OnPotHitGranny;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GrannyTag))
        {
            OnPotHitGranny?.Invoke();
        }
    }
}
