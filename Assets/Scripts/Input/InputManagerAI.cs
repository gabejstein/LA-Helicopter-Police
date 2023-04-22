using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerAI : InputManager
{
    float vertical;
    float horizontal;
    float descent;
    float ascent;
    float yaw;

    public override bool FireButtonDown()
    {
        throw new System.NotImplementedException();
    }

    public override bool FireMissileButtonDown()
    {
        throw new System.NotImplementedException();
    }

    public override float GetAscent()
    {
        return ascent;
    }

    public override float GetDescent()
    {
        return descent;
    }

    public override float GetHorizontal()
    {
        return horizontal;
    }

    public override float GetYaw()
    {
        return yaw;
    }

    public override float GetVertical()
    {
        return vertical;
    }

    public override bool LockOnButtonDown()
    {
        throw new System.NotImplementedException();
    }

    //Values to be set by the AI.
    public void SetVertical(float value)
    {
        vertical = value;
    }

    public void SetHorizontal(float value)
    {
        horizontal = value;
    }

    public void SetYaw(float value)
    {
        yaw = value;
    }

    public void SetDescent(float value) 
    {
        descent = value;
    }

    public void SetAscent(float value)
    {
        ascent = value;
    }
}
