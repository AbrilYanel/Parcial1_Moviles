using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pinchos : MonoBehaviour
{
    public string playerTag = "Player";
    public float angleThreshold = 45f; // Grados máximos para considerar que vino desde arriba

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag(playerTag)) return;

        foreach (ContactPoint contact in collision.contacts)
        {
            // Obtenemos la normal del contacto (dirección en que colisionó)
            Vector3 normal = contact.normal;

            // Si el contacto fue desde arriba (normal apuntando hacia abajo)
            float angle = Vector3.Angle(normal, Vector3.down);
            if (angle <= angleThreshold)
            {
                // Reinicia la escena actual
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            }
        }
    }
}
