using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    GameObject rightBullet;
    GameObject leftBullet;

    public float moveSpeed;
    public float jumpHeight;

    bool isGrounded;

    Vector3 scale;

    bool facingRight = true;

    int maxNbJumps;
    int jumpsRemaining;

    Rigidbody2D rigidBody;

    public GameWorldState gameWorld;

    enum forms { Water, Air, Fire, Earth };
    forms currentForm;

    int life;

    void Start()
    {
        life = 3;
        maxNbJumps = 1;
        isGrounded = true;
        scale = transform.localScale;
        rigidBody = GetComponent<Rigidbody2D>();
        changeForm(forms.Water);
        GetComponent<Renderer>().material.color = Color.blue;
        jumpsRemaining = maxNbJumps;
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigidBody.velocity.y);
        rigidBody.velocity = moveDirection;

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            facingRight = true;
            transform.localScale = new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            facingRight = false;
            transform.localScale = new Vector3(-1 * Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            rigidBody.AddForce(new Vector2(0, jumpHeight));
            jumpsRemaining--;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeForm(forms.Water);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            changeForm(forms.Air);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            changeForm(forms.Earth);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            changeForm(forms.Fire);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            fire();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            jumpsRemaining = maxNbJumps;
        }
    }

    void changeForm(forms form)
    {
        currentForm = form;

        if (currentForm == forms.Water)
        {
            GetComponent<Renderer>().material.color = Color.blue;
            rightBullet = Resources.Load("RightWater") as GameObject;
            leftBullet = Resources.Load("LeftWater") as GameObject;
            maxNbJumps = 1;
        }
        if (currentForm == forms.Air)
        {
            GetComponent<Renderer>().material.color = Color.white;
            rightBullet = Resources.Load("RightAir") as GameObject;
            leftBullet = Resources.Load("LeftAir") as GameObject;
            maxNbJumps = 2;
        }
        if (currentForm == forms.Fire)
        {
            GetComponent<Renderer>().material.color = Color.red;
            rightBullet = Resources.Load("RightFire") as GameObject;
            leftBullet = Resources.Load("LeftFire") as GameObject;
            maxNbJumps = 1;
        }
        if (currentForm == forms.Earth)
        {
            GetComponent<Renderer>().material.color = Color.gray;
            maxNbJumps = 1;
        }
    }

    void fire()
    {
        if (currentForm != forms.Earth)
        {
            if (facingRight)
            {
                Instantiate(rightBullet, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z), Quaternion.identity);
            }
            else
            {
                Instantiate(leftBullet, new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z), Quaternion.identity);
            }
        }
    }

    int getLife()
    {
        return life;
    }

    public void getHit()
    {
        if (facingRight)
        {
            rigidBody.velocity = new Vector2(-50000, 0);
        }
        else
        {
            rigidBody.velocity = new Vector2(50000, 0);
        }       
        life--;
    }

    public string getForm()
    {
        return currentForm.ToString();
    }
}
