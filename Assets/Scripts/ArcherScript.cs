using UnityEngine;
using System.Collections;

public class ArcherScript : MonoBehaviour
{
    Rigidbody2D rigidBody;

    public GameObject rightBullet;
    public GameObject leftBullet;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
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

    void OnCollisionEnter2D(Collision2D collider)
    {
        if ((collider.collider.name.Contains("Player")))
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

    public void shoot(bool fromRight)
    {
        print(fromRight);
        if (fromRight)
        {
            Instantiate(rightBullet, new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z), Quaternion.identity);
        }
        else
        {
            Instantiate(leftBullet, new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }
}
