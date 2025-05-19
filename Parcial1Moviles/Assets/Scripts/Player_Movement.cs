using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private float moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Aplicar movimiento solo mientras se mantiene el botón presionado
        Vector3 velocity = rb.velocity;
        velocity.x = moveInput * moveSpeed;
        rb.velocity = velocity;

        // Mantener al jugador en el plano Z = 0
        Vector3 position = transform.position;
        position.z = 0;
        transform.position = position;
    }

    // Estos métodos son llamados desde los botones UI
    public void MoveLeftPressed() => moveInput = -1;
    public void MoveRightPressed() => moveInput = 1;
    public void StopMoving() => moveInput = 0;

    public void Jump()
    {
        if (CheckGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
        }
    }

    private bool CheckGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }
}
