using System;
using UnityEngine;

public class Food : MonoBehaviour
{
    string GrannyTag = "Enemy";
    public static event Action OnFoodHitGranny;
    bool canDamaged = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (canDamaged)
        {
            if (collision.gameObject.CompareTag(GrannyTag))
            {
                OnFoodHitGranny?.Invoke();
                canDamaged = false;
            }
        }
        
    }
}
