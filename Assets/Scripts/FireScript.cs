using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name.Contains("Water"))
        {
            Destroy(gameObject);
        }
        if(collider.name.Contains("Player"))
        {
            if (collider.GetComponent<BasePlayerController>().getForm() != "Fire")
            {
                // Knock back the player
                Rigidbody2D rigidbody = collider.GetComponent<Rigidbody2D>();
                rigidbody.AddForce(new Vector2(-Mathf.Sign(rigidbody.velocity.x) * 500, 0));

                collider.GetComponent<BasePlayerController>().getHit();
            }         
        }
    }
}
