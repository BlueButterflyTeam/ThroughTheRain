using UnityEngine;
using System.Collections;

public class GameWorldState : MonoBehaviour {

    public int numberOfFormsInLevel;
    public AudioClip music;

    void Start () {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayMusic(music);
	}
	
	void Update () {
	
	}
}
