using UnityEngine;
using System.Collections;

public class WaterDetector : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D Hit)
    {
        if (Hit.GetComponent<Rigidbody2D>() != null)
        {
          transform.parent.GetComponent<Water>().Splash(transform.position.x, Hit.GetComponent<Rigidbody2D>().velocity.y*Hit.GetComponent<Rigidbody2D>().mass / 40f);
        }
        if(Hit.name == "player")
        {
                Hit.GetComponent<PlayerController>().setInWater(true);
            
        }
    }

    void OnTriggerExit2D(Collider2D Hit)
    {
        if (Hit.name == "player")
        {
            Hit.GetComponent<PlayerController>().setInWater(false);
        }
    }
}
