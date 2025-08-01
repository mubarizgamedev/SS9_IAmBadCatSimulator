using UnityEngine;
using System;

public class FireBullet : MonoBehaviour
{
    public static event Action OnFireBulletHitGranny;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            OnFireBulletHitGranny?.Invoke();
        }

        Destroy(gameObject);
    }
}
