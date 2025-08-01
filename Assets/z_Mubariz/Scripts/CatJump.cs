using UnityEngine;

public class CatJump : MonoBehaviour
{
    [SerializeField] GameObject stillhands;
    [SerializeField] AudioClip jumpSound;
   public void DisableThisGameObject()
    {
        gameObject.SetActive(false);
        stillhands.SetActive(true);
    }

    private void OnEnable()
    {
        SFX_Manager.PlaySound(jumpSound);
    }
}
