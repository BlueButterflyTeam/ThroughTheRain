using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioSource musicSource;
    public System.Collections.Generic.LinkedList<AudioSource> soundSources = new System.Collections.Generic.LinkedList<AudioSource>();

    public static AudioManager instance = null;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    private float soundVolume = .5f;
    private float musicVolume = .5f;

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

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void RandomizeSfx(params AudioClip [] clips)
    {
        int randInt = Random.Range(0, clips.Length);
        float randPitch = Random.Range(lowPitchRange, highPitchRange);

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clips[randInt];
        audioSource.pitch = randPitch;
        audioSource.volume = soundVolume;
        audioSource.Play();

        soundSources.AddLast(audioSource);
        
        StartCoroutine(WaitAndDestroySound(audioSource));
    }

    public void MusicVolume(float vol)
    {
        musicVolume = vol;
        musicSource.volume = vol;
    }

    public void SoundVolume(float vol)
    {
        soundVolume = vol;
    }

    public bool isSoundPlaying()
    {
        return soundSources.Count != 0;
    }

    private IEnumerator WaitAndDestroySound(AudioSource audioSource)
    {
        while(audioSource.isPlaying)
            yield return null;

        soundSources.Remove(audioSource);
        Destroy(audioSource);
    }
}
