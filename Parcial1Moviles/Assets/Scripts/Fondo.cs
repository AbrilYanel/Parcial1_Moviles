using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fondo : MonoBehaviour
{
    public Transform jugador;
    public float factorParallax = 0.5f; // Cuánto se mueve en comparación al jugador

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(posicionInicial.x + jugador.position.x * factorParallax,
                                         transform.position.y,
                                         transform.position.z);
    }
}
