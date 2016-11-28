using UnityEngine;
using System.Collections;

public class SpikeScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            // Knock up the player
            Rigidbody2D rigidbody = other.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(new Vector2(0, 500));

            other.GetComponent<BasePlayerController>().getHit();
        }
    }
}
