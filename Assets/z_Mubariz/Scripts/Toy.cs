using System;
using UnityEngine;

public class Toy : MonoBehaviour
{
    string GrannyTag = "Enemy";
    public static event Action OnToyHitGranny;
    bool canDamage = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (canDamage)
        {
            if (collision.gameObject.CompareTag(GrannyTag))
            {
                OnToyHitGranny?.Invoke();
                canDamage = false;
            }
        }        
    }
}
