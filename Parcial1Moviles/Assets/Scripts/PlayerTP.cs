using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTP : MonoBehaviour
{
    public LayerMask teleporterLayer;
    public float detectionRadius = 3f;
    public KeyCode teleportKey = KeyCode.T; // O llamar desde botón UI
    public Transform playerBody; // Referencia al cuerpo del jugador si es distinto del objeto raíz

    private Transform currentTeleporter;
    
    private bool isInsideTeleporter = false;
    private Rigidbody rb;
    public Teletransportador radiotp;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        if (playerBody == null) playerBody = transform;
    }

    void Update()
    {
        // Solo usar en PC. Para móviles, este método se llama desde un botón.
        if (Input.GetKeyDown(teleportKey))
        {
            ToggleTeleport();
        }

        if (isInsideTeleporter && currentTeleporter != null)
        {
            // Actualizar la posición del jugador cada frame para que siga al teletransportador
            transform.position = currentTeleporter.position;
        }
    }

    public void ToggleTeleport()
    {
        if (!isInsideTeleporter)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, teleporterLayer);
            if (colliders.Length > 0)
            {
                Transform closest = colliders[0].transform;
                float minDist = Vector3.Distance(transform.position, closest.position);

                foreach (var col in colliders)
                {
                    float dist = Vector3.Distance(transform.position, col.transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        closest = col.transform;
                    }
                }

                currentTeleporter = closest;
                EnterTeleporter();
            }
        }
        else
        {
            ExitTeleporter(); // Salida manual
        }
    }

    void EnterTeleporter()
    {
        
        isInsideTeleporter = true;
        
        rb.velocity = Vector3.zero;
        rb.useGravity = false;


        transform.position = currentTeleporter.position;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
    }

    void ExitTeleporter()
    {
        isInsideTeleporter = false;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;


    }

    public void TryJumpToExit(float jumpForce = 7f)
    {
        if (isInsideTeleporter)
        {
            ExitTeleporter();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
