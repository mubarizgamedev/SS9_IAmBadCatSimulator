using UnityEngine;
using System;
public class Breakable : MonoBehaviour
{
    public static event Action OnBreakObject;
    public static event Action OnBreakGlass;
    public static event Action OnPlateBreak;
    public static event Action OnToyBreak;
    //[SerializeField] GameObject wholeGameobject;
    [SerializeField] GameObject breakGameobject;
    [SerializeField] AudioClip breakSound;
    bool canBreak = true;
    int selectedIndex;
    private void Start()
    {
        selectedIndex = PlayerPrefs.GetInt("SelectedGrannyIndex", 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (canBreak)
        {

            Debug.Log(gameObject.name + "(breakable) collided with " + collision.gameObject);
            gameObject.SetActive(false);
            GameObject breakObject = Instantiate(breakGameobject, transform.position, transform.rotation);
            Destroy(breakObject, 5f);
            PlaySound(breakSound);
            gameObject.layer = 0;
            OnBreakObject?.Invoke();

            if (gameObject.CompareTag("Glass"))
            {
                OnBreakGlass?.Invoke();
            }
            if (gameObject.CompareTag("Plate"))
            {
                OnPlateBreak?.Invoke();
            }
            if (gameObject.CompareTag("Toy"))
            {
                OnToyBreak?.Invoke();
            }

            SFX_Manager.PlaySound(SFX_Manager.Instance.glassBreak);

            
            canBreak = false;

        }
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
