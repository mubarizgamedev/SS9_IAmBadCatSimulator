using UnityEngine;

public class SFX_Manager : MonoBehaviour
{
    public static SFX_Manager Instance; // Singleton Instance

    public AudioClip[] OnDieSounds;
    public AudioClip OnDangerSounds;
    public AudioClip GrannyAngerNewspaper;
    public AudioClip angryTalkGranny;
    public AudioClip[] angryTalkGrandpa;
    public AudioClip[] OnWinSounds;
    public AudioClip catHitSound;
    public AudioClip glassBreak;
    public AudioClip grannyOhNo;
    public AudioClip dogCrySound;
    public AudioClip catCrySound;
    public AudioClip[] catRandomSounds;
    public AudioClip punchSound;
    public AudioClip ButtonClick;
    public AudioClip[] hitObjectRandom;
    public AudioClip clapSound;

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
