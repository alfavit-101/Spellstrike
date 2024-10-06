using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }


    [SerializeField] private float movementSpeed = 10f;
    Vector2 inputVector;

    private Rigidbody2D rb;

    private float minMovementSpeed = 0.1f;
    private bool isRunning = false;

    private void Awake()
    {
        Instance = this;   
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack; //������� ����� ����������� ��� ������� ����� ������ ����
    }

    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack(); //���������� � �������� ������, ����� ���(���) � �������� ������� �����     
    }

    private void Update()
    {
        inputVector = GameInput.Instance.GetMovementVector();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {    
        rb.MovePosition(rb.position + (inputVector * movementSpeed * Time.fixedDeltaTime));
       //��������� ����� ����� ��� ���
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

    //���������� ��������� ����� �� ������
    public Vector3 GetPlayerScreenPosition() 
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

}