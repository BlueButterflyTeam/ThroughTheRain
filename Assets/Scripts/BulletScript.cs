using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public Vector2 speed;
    Rigidbody2D rigidBody;

    public float delay;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, delay);
        rigidBody.velocity = speed;
    }

    void Update()
    {
        rigidBody.velocity = speed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(!collider.name.Contains("Trigger") && !collider.name.Contains("Archer"))
        {
            if(collider.name.Contains("Player"))
            {
                if(!collider.GetComponent<BasePlayerController>().isPlayerCharging())
                {
                    // Knock back the player
                    Rigidbody2D rigidbody = collider.GetComponent<Rigidbody2D>();
                    rigidbody.AddForce(new Vector2(Mathf.Sign(speed.x) * 500, 0));

                    collider.GetComponent<BasePlayerController>().getHit();
                }
            }

            Destroy(gameObject);
        }
    }
}
