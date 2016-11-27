using UnityEngine;
using System.Collections;

public class ButtonSound : MonoBehaviour {
    public AudioClip clickSound;

    public void playSound()
    {
        try
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().RandomizeSfx(clickSound);
        }
        catch
        { }
    }
}
