using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, target.position.z - distance);
    }
}
