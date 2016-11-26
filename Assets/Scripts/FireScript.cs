using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        print("salut");
        if (collider.name.Contains("ater"))
        {
            print("toto");
            Destroy(gameObject);
        }
        if(collider.name.Contains("player"))
        {
            if (collider.GetComponent<PlayerController>().getForm() != "Fire")
            {
                collider.GetComponent<PlayerController>().getHit();
            }         
        }
    }
}
