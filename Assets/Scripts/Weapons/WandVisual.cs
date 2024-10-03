using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandVisual : MonoBehaviour
{
    [SerializeField] private Wand wand;

    private Animator animator;
    private const string ATTACK = "Attack";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        wand.OnWandSwing += Wand_OnWandSwing;
    }

    private void Wand_OnWandSwing(object sender, System.EventArgs e)
    {
        //обращается к Attack в аниматоре через константу
        animator.SetTrigger(ATTACK);
    }
}
