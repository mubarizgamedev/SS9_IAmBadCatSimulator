using UnityEngine;
using System;


public class Glass : MonoBehaviour
{
    string GrannyTag = "Enemy";
    public static event Action OnGlassHitGranny;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GrannyTag))
        {
            Debug.Log("Gift hit granny");
            OnGlassHitGranny?.Invoke();
        }
    }
}
