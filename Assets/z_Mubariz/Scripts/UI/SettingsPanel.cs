using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] AudioSource bgMusicAudioSource;
    [SerializeField] Slider settingVolumeSlider;

    private void Update()
    {
        bgMusicAudioSource.volume = settingVolumeSlider.value;
    }
}
