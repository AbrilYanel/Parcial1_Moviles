using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Recolectables : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Asigná esto en el Inspector
    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Sumar punto
            score++;
            UpdateUI();

            // Destruir el recolectable
            Destroy(gameObject);
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text =  score.ToString();
        }
    }
}
