using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPlatform : MonoBehaviour
{
    public float delayBeforeBreak = 1f; // Tiempo antes de romperse
    public string playerTag = "Player"; // Asegurate que tu jugador tenga este tag

    private bool isBreaking = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isBreaking && collision.gameObject.CompareTag(playerTag))
        {
            StartCoroutine(BreakPlataform());
        }
    }

    IEnumerator BreakPlataform()
    {
        isBreaking = true;
        yield return new WaitForSeconds(delayBeforeBreak);
        Destroy(gameObject);
    }
}
