using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerXBOXController : InputManager
{
    public override bool FireButtonDown()
    {
        return Input.GetAxis("Left Shoulder") == 1;
    }

    public override bool FireMissileButtonDown()
    {
        return Input.GetAxis("Fire2") == 1;
    }

    public override float GetAscent()
    {
        return Input.GetAxis("Jump");
    }

    public override float GetDescent()
    {
        return Input.GetAxis("Fire3");
    }

    public override float GetYaw()
    {
        return Input.GetAxis("RStick X");
    }

    public override float GetHorizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    public override float GetVertical()
    {
        return Input.GetAxis("Vertical");
    }

    public override bool LockOnButtonDown()
    {
        return Input.GetAxis("Right Shoulder") == 1;
    }

}
