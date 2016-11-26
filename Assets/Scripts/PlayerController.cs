﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Sprite waterSprite;
    public Sprite airSprite;
    public Sprite fireSprite;
    public Sprite earthSprite;

    public Sprite blackHeartSprite;
    public Sprite HeartSprite;

    public UnityEngine.UI.Text gameOverText;
    public GameObject rainPrefab;
    public UnityEngine.UI.Image heart1, heart2, heart3;
    public float moveSpeed;
    public float jumpHeight;

    public GameWorldState gameWorld;

    int numberOfForms;

    GameObject rightBullet;
    GameObject leftBullet;


    bool isGrounded;

    Vector3 scale;

    bool facingRight = true;

    bool isInWater;

    int maxNbJumps;
    int jumpsRemaining;

    Rigidbody2D rigidBody;

    enum forms { Water, Air, Fire, Earth };
    forms currentForm;

    int life;

    private bool keysEnabled = true;

    void Start()
    {
        life = 3;
        maxNbJumps = 1;
        isGrounded = true;
        scale = transform.localScale;
        rigidBody = GetComponent<Rigidbody2D>();
        changeForm(forms.Water);
        jumpsRemaining = maxNbJumps;
        numberOfForms = gameWorld.GetComponent<GameWorldState>().numberOfFormsInLevel;
    }

    void FixedUpdate()
    {
        if(keysEnabled)
        {
            Vector3 moveDirection = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigidBody.velocity.y);
            rigidBody.velocity = moveDirection;

            if (isInWater)
            {
                if (currentForm == forms.Fire)
                {
                    life = 0;
                }
                else if(currentForm == forms.Air)
                {
                    rigidBody.AddForce(new Vector3(0, 13, 0));
                }
            }
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
    }

    void Update()
    {
        if(keysEnabled)
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
            if (Input.GetKeyDown(KeyCode.Alpha2) && numberOfForms >= 2)
            {
                changeForm(forms.Air);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && numberOfForms >= 3)
            {
                changeForm(forms.Earth);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && numberOfForms >= 4)
            {
                changeForm(forms.Fire);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                fire();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                environmentalPower();
            }
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

        try
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Fire").GetComponent<Collider2D>(), false);
        }
        catch
        {
        }

        if (currentForm == forms.Water)
        {
            moveSpeed = 9;
            jumpHeight = 350;

            GetComponent<SpriteRenderer>().sprite = waterSprite;
            rightBullet = Resources.Load("RightWater") as GameObject;
            leftBullet = Resources.Load("LeftWater") as GameObject;
            maxNbJumps = 1;
        }
        if (currentForm == forms.Air)
        {
            moveSpeed = 12;
            jumpHeight = 350;

            GetComponent<SpriteRenderer>().sprite = airSprite;
            rightBullet = Resources.Load("RightAir") as GameObject;
            leftBullet = Resources.Load("LeftAir") as GameObject;
            maxNbJumps = 2;
        }
        if (currentForm == forms.Fire)
        {
            moveSpeed = 9;
            jumpHeight = 350;

            try
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Fire").GetComponent<Collider2D>());
            }
            catch
            {
            }

            GetComponent<SpriteRenderer>().sprite = fireSprite;
            rightBullet = Resources.Load("RightFire") as GameObject;
            leftBullet = Resources.Load("LeftFire") as GameObject;
            maxNbJumps = 1;
        }
        if (currentForm == forms.Earth)
        {
            moveSpeed = 5;
            jumpHeight = 200;

            GetComponent<SpriteRenderer>().sprite = earthSprite;
            maxNbJumps = 1;
        }
        if (isGrounded)
        {
            jumpsRemaining = maxNbJumps;
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
        StartCoroutine(KnockBack());
        if (life > 0)
        {
            if(life == 1)
            {
                heart1.GetComponent<UnityEngine.UI.Image>().sprite = blackHeartSprite;
            }
            else if(life == 2)
            {
                heart2.GetComponent<UnityEngine.UI.Image>().sprite = blackHeartSprite;
            }
            else if(life >= 3)
            {
                heart3.GetComponent<UnityEngine.UI.Image>().sprite = blackHeartSprite;
            }
            life--;
        }

        if(life == 0)
        {
            die();
        }
    }

    public string getForm()
    {
        return currentForm.ToString();
    }

    IEnumerator KnockBack()
    {
        float timer = 0;

        while(timer < 0.05f)
        {
            timer += Time.deltaTime;

            rigidBody.AddForce(new Vector3(rigidBody.velocity.x * -100, rigidBody.velocity.y * -5, transform.position.z));
        }
        yield return 0;            
    }

    public void setInWater(bool Boolean)
    {
        isInWater = Boolean;
    }

    public bool isPlayerInWater()
    {
        return isInWater;
    }

    private void environmentalPower()
    {
        switch (currentForm)
        {
            case forms.Air:
                break;
            case forms.Earth:
                break;
            case forms.Fire:
                break;
            case forms.Water:
                Object rainFX = Object.Instantiate(rainPrefab, transform.position, Quaternion.identity);
                Destroy(rainFX, 3);

                GameObject[] objects = GameObject.FindGameObjectsWithTag("Rain");

                int size = objects.Length;
                
                for (int i = 0; i < size; i++)
                {
                    GameObject obj = objects[i];
                    if(obj.GetComponent<Renderer>().isVisible)
                    {
                        obj.GetComponent<MoveUp>().moveUp();
                    }
                }

                break;
        }
    }

    private void die()
    {
        gameOverText.gameObject.SetActive(true);
        keysEnabled = false;

        StartCoroutine(Wait(3));
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);

        keysEnabled = true;
        gameOverText.gameObject.SetActive(false);

        Vector3 respawn = GameObject.Find("RespawnPoint").transform.position;
        gameObject.transform.position = new Vector3(respawn.x, respawn.y, transform.position.z);

        life = 3;

        heart1.GetComponent<UnityEngine.UI.Image>().sprite = HeartSprite;
        heart2.GetComponent<UnityEngine.UI.Image>().sprite = HeartSprite;
        heart3.GetComponent<UnityEngine.UI.Image>().sprite = HeartSprite;
    }
}

