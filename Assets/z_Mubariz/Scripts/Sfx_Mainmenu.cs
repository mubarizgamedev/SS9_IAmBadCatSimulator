using UnityEngine;

public class Sfx_Mainmenu : MonoBehaviour
{
    public static Sfx_Mainmenu Instance;

    public AudioClip swordEffect;
    public AudioClip sellPurchase;
    public AudioClip newSoundButton;
    public AudioClip petSelect;
    public AudioClip granSelect;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Assign the Singleton
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }
    }

    private void Start()
    {
        Debug.Log("SFX Manager Initialized: " + gameObject.name);
    }

    public static void PlaySound(AudioClip clip, float volume = 1f)
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

    public static void PlayRandomSound(AudioClip[] clips, float volume = 1f)
    {
        if (clips == null || clips.Length == 0)
        {
            Debug.LogWarning("PlayRandomSound: No audio clips provided.");
            return;
        }

        AudioClip randomClip = clips[Random.Range(0, clips.Length)];
        PlaySound(randomClip, volume);
    }
}
