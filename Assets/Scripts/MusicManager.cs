using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    bool soundOn = true;
    AudioSource m_AudioSource;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    public void ToggleMusic()
    {
        if (soundOn)
        {
            m_AudioSource.Pause();
            soundOn = false;
        }
        else
        {
            m_AudioSource.Play();
            soundOn = true;
        }
    }
}
