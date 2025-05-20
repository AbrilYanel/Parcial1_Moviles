using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrbeTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextScene < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(nextScene);
            else
                Debug.Log("¡Juego completado!");
        }
    }
}
