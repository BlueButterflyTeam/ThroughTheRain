using UnityEngine;
using System.Collections;

public class CheckpointScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "player")
        {
            GameObject.Find("RespawnPoint").transform.position = GameObject.Find("player").transform.position;
        }
    }
}
