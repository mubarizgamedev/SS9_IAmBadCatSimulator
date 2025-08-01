using System;
using UnityEngine;

public class ChristmasGifts : MonoBehaviour
{
    [SerializeField] string GrannyTag;
    public static event Action OnGifthitGranny;
    bool canDamageGranny = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GrannyTag))
        {
            if (canDamageGranny)
            {
                Debug.Log("Gift hit granny");
                OnGifthitGranny?.Invoke();
                canDamageGranny =false;
            }            
        }
    }
}
