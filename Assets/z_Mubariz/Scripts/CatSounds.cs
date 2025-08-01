using UnityEngine;
using System.Collections;

public class CatSounds : MonoBehaviour
{
    public AudioClip catSound; 
    public AudioClip dogSound; 
    private AudioSource audioSource;
    public float interval = 8f;
    int selectedIndexForCat;

   

    private void Awake()
    {
       
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (IsCatSelected())
        {
            audioSource.clip = catSound;
        }
        else
        {
            audioSource.clip = dogSound;
        }
        audioSource.clip = catSound;
        audioSource.loop = false;
    }

    private void OnEnable()
    {
        selectedIndexForCat = PlayerPrefs.GetInt("SelectedCatIndex", 0);
        StartCoroutine(PlayCatSoundRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines(); 
    }

    private IEnumerator PlayCatSoundRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            if (gameObject.activeInHierarchy && catSound != null) // Check if active
            {
                if (IsCatSelected())
                {
                    audioSource.PlayOneShot(catSound);

                }
                else
                {
                    audioSource.PlayOneShot(dogSound);
                }

            }
        }
    }

    private bool IsCatSelected()
    {
        if (selectedIndexForCat == 0 || selectedIndexForCat == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
