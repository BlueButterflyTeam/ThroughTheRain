using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

    public float moveSpeed;
    public float jumpHeight;

    bool isGrounded;

    Vector3 scale;

    int maxNbJumps;
    int jumpsRemaining;

    Rigidbody2D rigidBody;

    enum forms {Water, Air, Fire, Earth};
    forms currentForm;
    
	void Start () {
        maxNbJumps = 1;
        isGrounded = true;
        scale = transform.localScale;
        rigidBody = GetComponent<Rigidbody2D>();
        currentForm = forms.Water;
        GetComponent<Renderer>().material.color = Color.blue;
        jumpsRemaining = maxNbJumps;
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigidBody.velocity.y);
        rigidBody.velocity = moveDirection;

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.localScale = new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.localScale = new Vector3(-1 * Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));
        }
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            rigidBody.AddForce(new Vector2(0, jumpHeight));
            jumpsRemaining--;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            changeForm();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            jumpsRemaining = maxNbJumps;
        }
    }

    void changeForm()
    {
        if(currentForm == forms.Earth)
        {
            currentForm = forms.Water;
        }
        else
        {
            currentForm++;
        }
        if(currentForm == forms.Water)
        {
            GetComponent<Renderer>().material.color = Color.blue;
            maxNbJumps = 1;
        }
        if (currentForm == forms.Air)
        {
            GetComponent<Renderer>().material.color = Color.white;
            maxNbJumps = 2;
        }
        if (currentForm == forms.Fire)
        {
            GetComponent<Renderer>().material.color = Color.red;
            maxNbJumps = 1;
        }
        if (currentForm == forms.Earth)
        {
            GetComponent<Renderer>().material.color = Color.gray;
            maxNbJumps = 1;
        }
        jumpsRemaining = maxNbJumps;
    }

}
