using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public static float timer;
    public UnityEngine.UI.Text text;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
    }

    void OnGUI()
    {
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = Mathf.Floor(timer % 60).ToString("00");

        text.text = minutes + ":" + seconds;
    }

}
