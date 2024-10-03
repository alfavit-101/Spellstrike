using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    public event EventHandler OnPlayerAttack;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Combat.Attack.started += PlayerAttack_started;
    }

    private void PlayerAttack_started(InputAction.CallbackContext obj) 
    {
            OnPlayerAttack?.Invoke(this, EventArgs.Empty); //если onPlayerAttack != null, то вызываем его      
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }


    //определяем положение курсора для поворота спрайта
    public Vector3 GetMousePosition() 
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        return mousePos;
    }

}
