using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    public event EventHandler OnWandSwing;

    public void Attack() 
    {
        OnWandSwing?.Invoke(this, EventArgs.Empty);
    }

}
