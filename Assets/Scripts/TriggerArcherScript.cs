using UnityEngine;
using System.Collections;

public class TriggerArcherScript : MonoBehaviour
{
    public bool fromRight;

    bool wait = false;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {

	}

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.name.Contains("Player") && !wait)
        {
            StartCoroutine(Fire(1f));
        }
    }

    private IEnumerator Fire(float time)
    {
        wait = true;

        GetComponentInParent<ArcherScript>().shoot(fromRight);

        yield return new WaitForSeconds(time);

        wait = false;
    }
}
