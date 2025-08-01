using UnityEngine;

public class ObjectiveCongrats : MonoBehaviour
{
    [Header("This script will diable this gameobject after 3 seconds and enable level complete panel")]

    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] AudioClip clapSound;
    [SerializeField] float disableAfterseconds;

    private void OnEnable()
    {
        Invoke("Func", 3f);
        SFX_Manager.PlaySound(clapSound);
    }

    void Func()
    {
        levelCompletePanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
