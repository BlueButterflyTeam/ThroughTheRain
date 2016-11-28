using UnityEngine;
using System.Collections;

public class CheckpointScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            GameObject.Find("RespawnPoint").transform.position = other.gameObject.transform.position;
            Destroy(gameObject);
        }
    }
}
