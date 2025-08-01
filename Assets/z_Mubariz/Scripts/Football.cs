using System;
using UnityEngine;

public class Football : MonoBehaviour
{
    string GrannyTag = "Enemy";
    public static event Action OnBallHitGranny;
    bool canDamage = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (canDamage)
        {
            if (collision.gameObject.CompareTag(GrannyTag))
            {
                OnBallHitGranny?.Invoke();
                canDamage = false;
            }
        }        
    }
}
