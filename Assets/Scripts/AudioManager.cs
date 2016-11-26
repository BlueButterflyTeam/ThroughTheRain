using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioSource musicSource;
    public AudioSource soundSource;

    public static AudioManager instance = null;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance);

        DontDestroyOnLoad(gameObject);
	}
	
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySingle(AudioClip clip)
    {
        soundSource.clip = clip;
        soundSource.Play();
    }

    public void RandomizeSfx(params AudioClip [] clips)
    {
        int randInt = Random.Range(0, clips.Length);
        float randPitch = Random.Range(lowPitchRange, highPitchRange);

        soundSource.pitch = randPitch;
        soundSource.clip = clips[randInt];
        soundSource.Play();
    }

    public void MusicVolume(float vol)
    {
        musicSource.volume = vol;
    }

    public void SoundVolume(float vol)
    {
        soundSource.volume = vol;
    }
}
