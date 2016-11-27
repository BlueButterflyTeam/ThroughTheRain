using UnityEngine;
using System.Collections;

public class MoveUp : MonoBehaviour
{
    private bool movingUp = false;
    private bool moved = false;

    public AudioClip sound;

    private float startingPosition;

    // Use this for initialization
    void Start()
    {
        startingPosition = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingUp)
        {
            gameObject.transform.Translate(new Vector3(0, .1f, 0));

            if (gameObject.transform.position.y - startingPosition > 9)
            {
                movingUp = false;
            }
        }
    }

    public void moveUp()
    {
        StartCoroutine(ExecuteAfterTime(1));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        if (!moved)
        {
            try
            {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().RandomizeSfx(sound);
            }
            catch
            { }

            this.movingUp = true;
            moved = true;
        }
    }
}
