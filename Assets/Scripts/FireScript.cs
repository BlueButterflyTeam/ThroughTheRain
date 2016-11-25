using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.name.Contains("Water"))
        {
            Destroy(gameObject);
        }
        if(col.collider.name.Contains("player"))
        {
            if (col.collider.GetComponent<PlayerController>().getForm() != "Fire")
            {
                col.collider.GetComponent<PlayerController>().getHit();
            }            
        }
    }
}
