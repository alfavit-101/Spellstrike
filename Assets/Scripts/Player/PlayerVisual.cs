using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private const string IS_RUNNING = "IsRunning";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    //узнали бежит герой или нет
    private void Update()
    {
        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
        AdjustPlayerFacingDirection();
    }

    //поворачиваем еблище героя сука
    private void AdjustPlayerFacingDirection() 
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition(); //смотрит координаты курсора
        Vector3 playerPosition =Player.Instance.GetPlayerScreenPosition(); //смотрит координаты героя

        if (mousePos.x < playerPosition.x) //если координата курсора по оси х меньше оси х героя, то разворачивает его
        {
            spriteRenderer.flipX = true;
        }
        else 
        {
            spriteRenderer.flipX = false;
        }

    }

}
