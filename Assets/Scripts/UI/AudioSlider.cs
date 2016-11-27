using UnityEngine;
using System.Collections;

public class AudioSlider : MonoBehaviour {

    public void MusicVolume(float vol)
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().MusicVolume(vol);
    }

    public void SoundVolume(float vol)
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().SoundVolume(vol);
    }

    public void MasterVolume(float vol)
    {
        MusicVolume(vol);
        SoundVolume(vol);
    }
}
