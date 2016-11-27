using UnityEngine;
using System.Collections;

public class AudioSlider : MonoBehaviour {

    private AudioManager manager;

    void Start()
    {
        try
        {
            manager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

            if (gameObject.tag == "MusicSlider")
                gameObject.GetComponent<UnityEngine.UI.Slider>().value = manager.getMusicVolume();
            else
                gameObject.GetComponent<UnityEngine.UI.Slider>().value = manager.getSoundVolume();
        }
        catch
        { }
    }


    public void MusicVolume(float vol)
    {
        manager.MusicVolume(vol);
    }

    public void SoundVolume(float vol)
    {
        manager.SoundVolume(vol);
    }
}
