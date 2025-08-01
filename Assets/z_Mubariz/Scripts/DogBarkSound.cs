using UnityEngine;

public class DogBarkSound : MonoBehaviour
{
    public AudioClip dogBarkClip;
    public void BarkSound()
    {
        PlaySound(dogBarkClip);
    }

    public void PlaySound(AudioClip clip, float volume = 0.5f)
    {
        if (clip == null)
        {
            Debug.LogWarning("PlaySound: No AudioClip provided.");
            return;
        }

        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(soundGameObject, clip.length);
    }
}

