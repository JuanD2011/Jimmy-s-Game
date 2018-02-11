using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{

    public AudioMixer mainMixer;

    public void SetVolumeMaster(float _Volume)
    {
        mainMixer.SetFloat("volume", _Volume);
    }

    public void SetMusicVolume(float _Volume)
    {
        mainMixer.SetFloat("volumeMusic", _Volume);
    }

    public void SetSFXVolume(float _Volume)
    {
        mainMixer.SetFloat("volumeSFX", _Volume);
    }
}
