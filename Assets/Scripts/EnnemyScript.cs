using UnityEngine;
using System.Collections;

public class EnnemyScript : MonoBehaviour
{
    Rigidbody2D rigidBody;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name.Contains("AirBullet"))
        {
            if (collider.transform.position.x > transform.position.x)
            {
                rigidBody.AddForce(new Vector2(-200, 0));
            }
            else
            {
                rigidBody.AddForce(new Vector2(200, 0));
            }

        }
        else if (collider.name.Contains("Bullet"))
        {
            Destroy(gameObject);
        }
       
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.collider.name.Contains("Player")))
        {
            if (other.collider.GetComponent<BasePlayerController>().isPlayerCharging())
            {
                other.collider.GetComponent<BasePlayerController>().stopCharge();
                StartCoroutine(other.collider.GetComponent<BasePlayerController>().KnockBack());
                Destroy(gameObject);
            }
            else
            {
                // Knock back the player
                Rigidbody2D rigidbody = other.collider.GetComponent<Rigidbody2D>();
                rigidbody.AddForce(new Vector2(-Mathf.Sign(rigidbody.velocity.x) * 200, 0));

                other.collider.GetComponent<BasePlayerController>().getHit();
            }            
        }
    }
}
