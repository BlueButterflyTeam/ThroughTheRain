using UnityEngine;
using System.Collections;
using System;

public class RavinePlayerController : BasePlayerController {
    public GameObject rainPrefab;
    public GameObject windPrefab;

    private bool airPowerToRight = true;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        startForm = forms.Air;
        changeForm(forms.Air);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

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
            case forms.Fire:

                break;
            case forms.Water:
                // Visual effect
                GameObject rainFX = (GameObject)UnityEngine.Object.Instantiate(rainPrefab, transform.position, Quaternion.identity);
                Destroy(rainFX, 3);

                // Objects that are affected
                objects = getVisbleObjectWithTag("Rain");

                foreach (GameObject obj in objects)
                {
                    obj.GetComponent<MoveUp>().moveUp();
                }

                break;
        }
    }
}
