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
            if (collider.GetComponent<ForestPlayerController>().getForm() != "Fire")
            {
                collider.GetComponent<ForestPlayerController>().getHit();
            }         
        }
    }
}
