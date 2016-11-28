using UnityEngine;
using System.Collections;

public class UpwardWind : MonoBehaviour {

    private bool appearing = true;

    private SpriteRenderer[] spriteRenderer = new SpriteRenderer[8];
    private Color color;

    public float speed = 0.02f;

    void Start()
    {
        for(int i = 0; i < spriteRenderer.Length; i++)
        {
            spriteRenderer[i] = gameObject.transform.GetChild(0).transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
        
        color = spriteRenderer[0].color;
    }

    // Update is called once per frame
    void Update()
    {
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

            if (color.a <= 0)
            {
                appearing = true;
            }
        }
        
        for(int i = 0; i < spriteRenderer.Length; i++)
        {
            spriteRenderer[i].color = color;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<BasePlayerController>().getForm() != "Earth")
            {
                float scale = Mathf.Pow((gameObject.transform.position.y / other.gameObject.transform.position.y), 2);
                print(scale);
                Rigidbody2D rigidbody = other.gameObject.GetComponent<Rigidbody2D>();
                rigidbody.AddForce(new Vector2(0, 10 * scale));
            }
        }
    }
}
