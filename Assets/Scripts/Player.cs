using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }


    [SerializeField] private float movementSpeed = 10f;

    private PlayerInputActions playerInputActions;

    private Rigidbody2D rb;

    private float minMovementSpeed = 0.1f;
    private bool isRunning = false;

    private void Awake()
    {
        Instance = this;   
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        rb.MovePosition(rb.position + (inputVector * movementSpeed * Time.fixedDeltaTime));


       //проверяем бежит герой или нет
        if (Mathf.Abs(inputVector.x) > minMovementSpeed || Mathf.Abs(inputVector.y) > minMovementSpeed) 
        {
            isRunning = true;
        }
        else 
        {
            isRunning = false;
        }
    }
    
    public bool IsRunning() 
    {
        return isRunning;  
    }

    //возвращает положение героя на экране
    public Vector3 GetPlayerScreenPosition() 
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

}
