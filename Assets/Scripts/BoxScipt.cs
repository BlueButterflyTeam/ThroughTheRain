using UnityEngine;
using System.Collections;

public class BoxScipt : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.position.x > transform.position.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
        }
    }
}
