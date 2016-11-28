using UnityEngine;
using System.Collections;
using System;

public class VolcanoPlayerController : BasePlayerController {

    public GameObject windPrefab;

    private bool airPowerToRight = true;

    // Use this for initialization
    public override void Start () {
        base.Start();

        startForm = forms.Fire;
        changeForm(forms.Fire);
    }
	
	// Update is called once per frame
	public override void Update () {
        base.Update();

        if (keysEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
            {
                rigidBody.AddForce(new Vector2(0, jumpHeight));
                jumpsRemaining--;

                try
                {
                    GameObject.Find("AudioManager").GetComponent<AudioManager>().RandomizeSfx(jumpSound);
                }
                catch
                { }
            }
        }
    }

    protected override void environmentalPower()
    {
        ArrayList objects;

        switch (currentForm)
        {
            case forms.Air:
                // Sound effect
                try
                {
                    GameObject.Find("AudioManager").GetComponent<AudioManager>().RandomizeSfx(windSound);
                }
                catch
                { }

                // Visual effect
                GameObject windFX = (GameObject)UnityEngine.Object.Instantiate(windPrefab, transform.position, Quaternion.identity);

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
                // Sound effect
                try
                {
                    GameObject.Find("AudioManager").GetComponent<AudioManager>().RandomizeSfx(earthquakeSound);
                }
                catch
                { }

                // Visual effect
                GameObject.Find("Main Camera").GetComponent<CameraScript>().shake(1);

                // Objects that are affected
                objects = getVisbleObjectWithTag("Destructible");

                foreach (GameObject obj in objects)
                {
                    obj.GetComponent<BreakBarrier>().breakBarrier();
                }

                break;
            default:
                break;
        }
    }

    protected override void changeForm(forms form)
    {
        currentForm = form;

        try
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().RandomizeSfx(popSound1, popSound2, popSound3, popSound4);
        }
        catch
        { }

        try
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Fire").GetComponent<Collider2D>(), false);
        }
        catch
        { }

        if (currentForm == forms.Water)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprite;

            life = 0;
            updateHearts();
            die();
        }
        if (currentForm == forms.Air)
        {
            moveSpeed = 5;
            jumpHeight = 400;

            GetComponent<SpriteRenderer>().sprite = airSprite;
            rightBullet = Resources.Load("RightAirBullet") as GameObject;
            leftBullet = Resources.Load("LeftAirBullet") as GameObject;
            shootSound = waterPulseSound;
            maxNbJumps = 2;
        }
        if (currentForm == forms.Fire)
        {
            moveSpeed = 4;
            jumpHeight = 400;

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
            shootSound = fireballSound;
            maxNbJumps = 1;
        }
        if (currentForm == forms.Earth)
        {
            moveSpeed = 3;
            jumpHeight = 200;

            GetComponent<SpriteRenderer>().sprite = earthSprite;
            maxNbJumps = 1;
        }
        if (isGrounded)
        {
            jumpsRemaining = maxNbJumps;
        }
    }

}
