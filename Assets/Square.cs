using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

    public float moveSpeed;
    public float jumpHeight;

    public Transform groundPoint;
    public float radius;
    public LayerMask groundMask;

    bool isGrounded;

    Rigidbody2D rigidBody;

    int maxExtraJumps;
    int nbExtraJumps;

    enum forms {Water, Air, Fire, Earth};
    forms currentForm;
    
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        maxExtraJumps = 1;
        nbExtraJumps = maxExtraJumps;
        currentForm = forms.Water;
        GetComponent<Renderer>().material.color = Color.blue;
    }
	
	void Update ()
    {
        Vector2 moveDirection = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigidBody.velocity.y);
        rigidBody.velocity = moveDirection;

        isGrounded = Physics2D.OverlapCircle(groundPoint.position, radius, groundMask);

        if(Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && nbExtraJumps != 0)
        {
            rigidBody.velocity = new Vector3(0, jumpHeight, 0);
            nbExtraJumps--;
        }

        if(isGrounded)
        {
            nbExtraJumps = maxExtraJumps;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            changeForm();
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
            maxExtraJumps = 1;
        }
        if (currentForm == forms.Air)
        {
            GetComponent<Renderer>().material.color = Color.white;
            maxExtraJumps = 2;
        }
        if (currentForm == forms.Fire)
        {
            GetComponent<Renderer>().material.color = Color.red;
            maxExtraJumps = 1;
        }
        if (currentForm == forms.Earth)
        {
            GetComponent<Renderer>().material.color = Color.gray;
            maxExtraJumps = 1;
        }
    }

}
