using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataformas : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            // Si el jugador está cayendo (velocidad hacia abajo)
            if (rb != null && rb.velocity.y <= 0f)
            {
                // Activar colisión momentáneamente
                Collider platformCollider = GetComponent<Collider>();
                platformCollider.isTrigger = false;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Desactivar colisión nuevamente para que se pueda subir desde abajo
            Collider platformCollider = GetComponent<Collider>();
            platformCollider.isTrigger = true;
        }
    }
}
