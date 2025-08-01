using System;
using UnityEngine;

public class BallonsBehaviour : MonoBehaviour
{
    [SerializeField] GameObject effect;
    [SerializeField] AudioClip popSound;
    public static event Action OnBalloonPopped;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dart") || other.CompareTag("PlayerHand"))
        {
            Vector3 collisionPoint = other.transform.position;
            Destroy(gameObject);
            OnBalloonPopped?.Invoke();
            SFX_Manager.PlaySound(popSound);
            GameObject gb = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gb,2f);
        }
    }

}
