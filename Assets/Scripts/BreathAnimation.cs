using UnityEngine;
using System.Collections;

public class BreathAnimation : MonoBehaviour {

    private bool appearing = true;

    private SpriteRenderer spriteRenderer;
    private Color color;

    private float speed = 0.02f;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;

    }

	// Update is called once per frame
	void Update () {
        if (appearing)
        {
            color.a += speed;

            if (color.a >= 1)
            {
                appearing = false;
            }
        }
        else
        {
            color.a -= speed;

            if(color.a <= 0)
            {
                appearing = true;
            }
        }

        spriteRenderer.color = color;
    }
}
