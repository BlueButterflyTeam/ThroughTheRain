using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Transform target;

    private bool isShaking = false;
    private bool growStronger = true;
    private float scale = 0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(target.position.x, target.position.y + 1, transform.position.z);

        if(isShaking)
        {
            if (growStronger)
                scale += .005f;
            else
                scale -= .005f;

            if (scale <= 0f)
                isShaking = false;

            float rnd1 = Random.Range(0f, 10f) * scale, rnd2 = Random.Range(0f, 10f) * scale;
            float noise1 = Mathf.PerlinNoise(rnd1, rnd1), noise2 = Mathf.PerlinNoise(rnd2, rnd2);

            newPosition = new Vector3(target.position.x + noise1, target.position.y + noise2, transform.position.z);
        }

        transform.position = newPosition;
    }

    public void shake(float time = 1)
    {
        scale = 0f;
        isShaking = true;
        growStronger = true;
        StartCoroutine(stopShaking(time));
    }

    private IEnumerator stopShaking(float time)
    {
        yield return new WaitForSeconds(time);
        growStronger = false;
    }
}
