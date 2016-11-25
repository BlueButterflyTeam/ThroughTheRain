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

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}
