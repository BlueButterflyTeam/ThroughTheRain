using UnityEngine;
using System.Collections;
using System;

public class VolcanoPlayerController : BasePlayerController {
    
    // Use this for initialization
    public override void Start () {
        base.Start();

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
            }
        }
    }

    protected override void environmentalPower()
    {
        //throw new NotImplementedException();
    }

}
