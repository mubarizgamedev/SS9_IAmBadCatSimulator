using System;
using UnityEngine;

public class BeeBullet : MonoBehaviour
{
    public static event Action OnBeeBulletHitGranny;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            OnBeeBulletHitGranny?.Invoke();
        }

        Destroy(gameObject);
    }
}
