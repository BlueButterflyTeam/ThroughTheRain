using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Transform target;

    private bool isShaking = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        if(isShaking)
        {
            float rnd1 = Random.Range(0f, 10f) * .5f, rnd2 = Random.Range(0f, 10f) * .5f;
            float noise1 = Mathf.PerlinNoise(rnd1, rnd1), noise2 = Mathf.PerlinNoise(rnd2, rnd2);

            newPosition = new Vector3(target.position.x + noise1, target.position.y + noise2, transform.position.z);
        }

        transform.position = newPosition;
    }

    public void shake(float time = 1)
    {
        isShaking = true;
        StartCoroutine(stopShaking(time));
    }

    private IEnumerator stopShaking(float time)
    {
        yield return new WaitForSeconds(time);

        isShaking = false;
    }
}
