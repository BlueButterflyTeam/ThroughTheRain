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
    public GameObject windPrefab;

    public UnityEngine.UI.Image heart1, heart2, heart3;
    public float moveSpeed;
    public float jumpHeight;

    public GameWorldState gameWorld;

    bool isCharging = false;
    bool smashing = false;
    float timer;

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
    private bool airPowerToRight = true;

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
        if(isCharging)
        {
            if (facingRight)
            {
                rigidBody.AddForce(new Vector3(25, 0, 0));
            }
            else
            {
                rigidBody.AddForce(new Vector3(-25, 0, 0));
            }
            StartCoroutine(stopCharge());
        }
        else if(smashing)
        {           
            rigidBody.AddForce(new Vector2(0, -50));
            if (jumpsRemaining == maxNbJumps)
            {
                GetComponent<Renderer>().material.color = Color.white;
                smashing = false;
                keysEnabled = true;
            }
        }
        else if(keysEnabled)
        {
            if (Input.GetKeyDown(KeyCode.S) && (currentForm == forms.Earth) && (jumpsRemaining < maxNbJumps))
            {
                GetComponent<Renderer>().material.color = Color.red;
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                smashing = true;
                keysEnabled = false;
            }
            Vector3 moveDirection = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigidBody.velocity.y);
            rigidBody.velocity = moveDirection;

            if (isInWater)
            {
                if (currentForm == forms.Fire)
                {
                    life = 0;
                    updateHearts();
                    die();
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
                if(currentForm != forms.Earth)
                {
                    fire();
                }
                else
                {
                    startCharge();                 
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                environmentalPower();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "MovableCloud")
        {
            jumpsRemaining = maxNbJumps;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.name.Contains("Arrow"))
        {
            getHit();
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
            rightBullet = Resources.Load("RightWaterBullet") as GameObject;
            leftBullet = Resources.Load("LeftWaterBullet") as GameObject;
            maxNbJumps = 1;
        }
        if (currentForm == forms.Air)
        {
            moveSpeed = 12;
            jumpHeight = 350;

            GetComponent<SpriteRenderer>().sprite = airSprite;
            rightBullet = Resources.Load("RightAirBullet") as GameObject;
            leftBullet = Resources.Load("LeftAirBullet") as GameObject;
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
            rightBullet = Resources.Load("RightFireBullet") as GameObject;
            leftBullet = Resources.Load("LeftFireBullet") as GameObject;
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
        if (facingRight)
        {
            Instantiate(rightBullet, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z), Quaternion.identity);
        }
        else
        {
            Instantiate(leftBullet, new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }

    void startCharge()
    {
        isCharging = true;
        GetComponent<Renderer>().material.color = Color.red;
        keysEnabled = false;
    }

    public IEnumerator stopCharge()
    {
        yield return new WaitForSeconds(1);

        isCharging = false;
        GetComponent<Renderer>().material.color = Color.white;
        keysEnabled = true;
    }

    int getLife()
    {
        return life;
    }

    public void getHit()
    {
        rigidBody.AddForce(new Vector2(-Mathf.Sign(rigidBody.velocity.x) * 500, 0));        

        if (life > 0)
        {            
            life--;
            updateHearts();
            
        }

        if(life == 0)
        {
            die();
        }
        else
            StartCoroutine(KnockBack());
    }

    public string getForm()
    {
        return currentForm.ToString();
    }

    public IEnumerator KnockBack()
    {
        keysEnabled = false;

        yield return new WaitForSeconds(1);

        keysEnabled = true;        
    }

    public void setInWater(bool Boolean)
    {
        isInWater = Boolean;
    }

    public bool isPlayerInWater()
    {
        return isInWater;
    }

    public bool isPlayerCharging()
    {
        return isCharging;
    }

    public bool isPlayerSmashing()
    {
        return smashing;
    }

    private void environmentalPower()
    {
        ArrayList objects;

        switch (currentForm)
        {
            case forms.Air:
                // Visual effect
                GameObject windFX = (GameObject)Object.Instantiate(windPrefab, transform.position, Quaternion.identity);

                Vector3 position = transform.position;

                if (airPowerToRight)
                {
                    position.x += windPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                }
                else
                {
                    windFX.GetComponent<SpriteRenderer>().flipX = true;
                    position.x -= windPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                }

                windFX.transform.position = position;

                Destroy(windFX, 1.7f);

                // Objects that are affected
                objects = getVisbleObjectWithTag("MovableCloud");

                foreach (GameObject obj in objects)
                {
                    obj.GetComponent<MoveCloud>().moveCloud(airPowerToRight);
                }

                airPowerToRight = !airPowerToRight;

                break;
            case forms.Earth:
                // Visual effect
                GameObject.Find("Main Camera").GetComponent<CameraScript>().shake(1);

                // Objects that are affected
                objects = getVisbleObjectWithTag("Destructible");

                foreach (GameObject obj in objects)
                {
                    obj.GetComponent<BreakBarrier>().breakBarrier();
                }

                break;
            case forms.Fire:

                break;
            case forms.Water:
                // Visual effect
                GameObject rainFX = (GameObject)Object.Instantiate(rainPrefab, transform.position, Quaternion.identity);
                Destroy(rainFX, 3);

                // Objects that are affected
                objects = getVisbleObjectWithTag("Rain");
                
                foreach(GameObject obj in objects)
                {
                    obj.GetComponent<MoveUp>().moveUp();
                }

                break;
        }
    }

    private ArrayList getVisbleObjectWithTag(string tag)
    {
        int size;
        GameObject[] objects;
        ArrayList results = new ArrayList();

        objects = GameObject.FindGameObjectsWithTag(tag);

        size = objects.Length;

        for (int i = 0; i < size; i++)
        {
            GameObject obj = objects[i];
            
            if (obj.transform.GetChild(0).GetComponent<Renderer>().isVisible)
            {
                results.Add(obj);
            }
        }

        return results;
    }

    private void die()
    {
        gameOverText.gameObject.SetActive(true);
        keysEnabled = false;
        
        StartCoroutine(Respawn(3));
    }

    private IEnumerator Respawn(float time)
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

        changeForm(forms.Water);
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    void updateHearts()
    {
        if (life == 0)
        {
            heart1.GetComponent<UnityEngine.UI.Image>().sprite = blackHeartSprite;
            heart2.GetComponent<UnityEngine.UI.Image>().sprite = blackHeartSprite;
            heart3.GetComponent<UnityEngine.UI.Image>().sprite = blackHeartSprite;
        }
        else if (life == 1)
        {
            heart2.GetComponent<UnityEngine.UI.Image>().sprite = blackHeartSprite;
            heart3.GetComponent<UnityEngine.UI.Image>().sprite = blackHeartSprite;
        }
        else if (life >= 2)
        {
            heart3.GetComponent<UnityEngine.UI.Image>().sprite = blackHeartSprite;
        }
    }
}

