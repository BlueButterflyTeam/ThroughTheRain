using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    private bool running;

    public float timer;
    public UnityEngine.UI.Text text;

    // Use this for initialization
    void Start () {
        running = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(running)
            timer += Time.deltaTime;
    }

    void OnGUI()
    {
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = Mathf.Floor(timer % 60).ToString("00");

        text.text = minutes + ":" + seconds;
    }

    public void stopTimer()
    {
        running = false;
    }

    public void continueTimer()
    {
        running = true;
    }

}
