using UnityEngine;
using System.Collections;

public class EnnemyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.name.Contains("AirBullet"))
        {
            if(collider.transform.position.x > transform.position.x)
                {
                transform.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
            }
            else
            {
                transform.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
            }
            
        }
        else if(collider.name.Contains("Bullet"))
        {
            Destroy(gameObject);
        }
       
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
         if ((collider.collider.name.Contains("player")))
        {
            if (collider.collider.GetComponent<PlayerController>().isPlayerCharging())
            {
                collider.collider.GetComponent<PlayerController>().stopCharge();
                StartCoroutine(collider.collider.GetComponent<PlayerController>().KnockBack());
                Destroy(gameObject);
            }
            else
            {
                collider.collider.GetComponent<PlayerController>().getHit();
            }
        }
    }
}
