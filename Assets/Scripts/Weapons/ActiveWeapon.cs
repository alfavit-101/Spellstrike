using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance { get; private set; }

    [SerializeField] private Wand wand;

    private void Awake()
    {
        Instance = this;
    }

    public Wand GetActiveWeapon() 
    {
        return wand;
    }
}
