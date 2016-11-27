using UnityEngine;
using System.Collections;
using System;

public class WaterPlayerController : BasePlayerController {

    private bool verticalBoost = false;
    private bool horizontalBoost = false;

    // Use this for initialization
    public override void Start () {
        base.Start();

        startForm = forms.Water;
        changeForm(forms.Water);
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();

        if (keysEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
            {
                isGrounded = false;
                jumpsRemaining = 0;

                switch (currentForm)
                {
                    case forms.Water:
                        horizontalBoost = true;
                        break;
                    case forms.Air:
                        verticalBoost = true;
                        break;
                }
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        // TODO play sound
        if (horizontalBoost)
        {
            rigidBody.AddForce(new Vector2(400, 0));
            StartCoroutine(WaitAndStopHorizontalBoost(.3f));
        }

        if (verticalBoost)
        {
            rigidBody.AddForce(new Vector2(0, 30));
            StartCoroutine(WaitAndStopVerticalBoost(.3f));
        }
            
    }

    protected override void environmentalPower()
    {
        ArrayList objects;

        if(currentForm == forms.Earth)
        {
            // Visual effect
            GameObject.Find("Main Camera").GetComponent<CameraScript>().shake(1);

            // Objects that are affected
            objects = getVisbleObjectWithTag("Destructible");

            foreach (GameObject obj in objects)
            {
                obj.GetComponent<BreakBarrier>().breakBarrier();
            }
        }
    }

    private IEnumerator WaitAndStopHorizontalBoost(float time)
    {
        yield return new WaitForSeconds(time);

        horizontalBoost = false;
    }

    private IEnumerator WaitAndStopVerticalBoost(float time)
    {
        yield return new WaitForSeconds(time);

        verticalBoost = false;
    }
}
