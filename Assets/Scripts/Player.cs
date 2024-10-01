using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;

    private PlayerInputActions playerInputActions;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = GameInput.instance.GetMovementVector();
        inputVector = inputVector.normalized;
        rb.MovePosition(rb.position + (inputVector * movementSpeed * Time.fixedDeltaTime));
    }

}
