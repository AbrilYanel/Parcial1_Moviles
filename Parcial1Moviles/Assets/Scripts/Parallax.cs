using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxSpeed = 50f;   // Velocidad del parallax en píxeles por segundo
    public bool isLooping = true;       // Para repetir el fondo cuando sale de la pantalla

    private RectTransform rectTransform;
    private Vector2 startPosition;
    private float backgroundWidth;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;

        // Calcula el ancho del fondo basándose en el rectángulo
        backgroundWidth = rectTransform.rect.width;
    }

    void Update()
    {
        float movement = Mathf.Repeat(Time.time * parallaxSpeed, backgroundWidth);

        rectTransform.anchoredPosition = startPosition + Vector2.left * movement;

        if (isLooping && movement == 0)
        {
            rectTransform.anchoredPosition = startPosition;
        }
    }
}
