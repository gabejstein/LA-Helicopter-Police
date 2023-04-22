using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputManager : MonoBehaviour
{
    public abstract float GetVertical();

    public abstract float GetHorizontal();

    public abstract float GetYaw();

    public abstract float GetAscent();

    public abstract float GetDescent();

    public abstract bool FireButtonDown();

    public abstract bool FireMissileButtonDown();

    public abstract bool LockOnButtonDown();
    
}
