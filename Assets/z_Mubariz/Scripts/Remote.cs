using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Remote : MonoBehaviour, IInteractable
{
    [SerializeField] RawImage m_RawImage;
    [SerializeField] VideoPlayer m_VideoPlayer;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float timeAfterChannelChanged = 1f;

    bool playerInZone;

    public static event Action OnInteract;

    [Serializable]
    public struct RemoteData
    {
        public string name;
        public VideoClip videoClip;
        public AudioClip audioClip;
    }

    public RemoteData[] remoteData;
    private int currentIndex = 0;

    private void Start()
    {
        if (remoteData != null && remoteData.Length > 0)
        {
            m_VideoPlayer.clip = remoteData[currentIndex].videoClip;
            m_VideoPlayer.Prepare(); // Prepare the video first
            m_VideoPlayer.prepareCompleted += OnVideoPrepared; // Subscribe to the event

            audioSource.clip = remoteData[currentIndex].audioClip;
            audioSource.Play();

            // Cycle to the next index
            currentIndex = (currentIndex + 1) % remoteData.Length;
        }
        else
        {
            Debug.LogWarning("No Remote Data assigned to " + gameObject.name);
        }
    }

    public void Interact()
    {
        OnInteract?.Invoke();
        Debug.Log("Interacted with: " + gameObject.name);

        Invoke(nameof(ChangeChannel), timeAfterChannelChanged);    
    }

    void ChangeChannel()
    {
        Debug.Log("Interacted with: " + gameObject.name);

        if (remoteData != null && remoteData.Length > 0)
        {
            m_VideoPlayer.clip = remoteData[currentIndex].videoClip;
            m_VideoPlayer.Prepare(); // Prepare the video first
            m_VideoPlayer.prepareCompleted += OnVideoPrepared; // Subscribe to the event

            audioSource.clip = remoteData[currentIndex].audioClip;
            audioSource.Play();

            // Cycle to the next index
            currentIndex = (currentIndex + 1) % remoteData.Length;
        }
        else
        {
            Debug.LogWarning("No Remote Data assigned to " + gameObject.name);
        }
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        m_RawImage.texture = m_VideoPlayer.texture;
        m_VideoPlayer.Play();
        m_VideoPlayer.prepareCompleted -= OnVideoPrepared; // Unsubscribe to prevent multiple triggers
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInZone = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }
}