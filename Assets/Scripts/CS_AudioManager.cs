using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CS_AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
 
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }
 
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Background", volume);
    }
 
    public void SetSoundEffectVolume(float volume)
    {
        audioMixer.SetFloat("SoundEffect", volume);
    }
}
