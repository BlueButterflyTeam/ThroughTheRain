﻿using UnityEngine;
using System.Collections;

public class HiddenObject : MonoBehaviour {

    public UnityEngine.UI.Image image;
    public AudioClip sound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            try
            {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().RandomizeSfx(sound);
            }
            catch
            { }

            Color tmp = image.color;
            tmp.a = 255;
            image.color = tmp;

            Destroy(this.gameObject);
        }
    }
}
