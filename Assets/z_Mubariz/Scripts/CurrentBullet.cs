using System;
using UnityEngine;

public class CurrentBullet : MonoBehaviour
{
    public static event Action OnCurrentBulletHitGranny;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            OnCurrentBulletHitGranny?.Invoke();
        }

        Destroy(gameObject);
    }
}
