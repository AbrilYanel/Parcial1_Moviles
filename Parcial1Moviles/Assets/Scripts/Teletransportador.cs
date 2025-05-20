using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletransportador : MonoBehaviour
{
    public float radius = 3f; // radio de detección

    // Visualiza el área de detección en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
