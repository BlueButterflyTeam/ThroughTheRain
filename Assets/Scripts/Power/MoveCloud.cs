using UnityEngine;
using System.Collections;

public class MoveCloud : MonoBehaviour {

    private bool movingLeft = false;
    private bool moving = false;

    void Update()
    {
        if (moving)
        {
            int sign = movingLeft ? 1 : -1;
            transform.Translate(new Vector3(sign * .1f, 0));
        }
    }

    public void moveCloud(bool moveLeft)
    {
        moving = true;
        movingLeft = moveLeft;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player")
        {
            other.gameObject.transform.SetParent(this.transform);
        }
        else
        {
            moving = false;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
