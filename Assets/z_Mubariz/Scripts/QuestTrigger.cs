using UnityEngine;
using System;

public class QuestTrigger : MonoBehaviour
{
    public static event Action OnCatQuest;
    public Animator questAnimator;
    public AudioClip chestOpen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            questAnimator.SetBool("Open", true);
            PlaySound(chestOpen);
            Invoke(nameof(Return), 2f);
        }
    }

    void Return()
    {
        OnCatQuest?.Invoke();
    }

    void PlaySound(AudioClip clip)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(soundGameObject, clip.length);
    }
}
