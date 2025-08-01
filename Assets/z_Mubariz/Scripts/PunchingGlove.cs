using System;
using UnityEngine;

public class PunchingGlove : MonoBehaviour
{
    public static event Action OnPunchGranny;
    [SerializeField] Animator animator;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Punching glove hit granny");
            OnPunchGranny?.Invoke();
        }
    }

    public void Punch()
    {
        animator.SetBool("Punch", true);
    }
}
