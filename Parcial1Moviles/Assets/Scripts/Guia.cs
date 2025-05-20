using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Guia : MonoBehaviour
{
    public string mensaje = "¡Has colisionado con el objeto!";


    public float duracion = 3f;

   
    public float velocidadTipeo = 30f;

 
    public TextMeshProUGUI textoMensaje;

    private Coroutine mostrarMensajeCoroutine;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MostrarMensaje();
        }
    }

    void MostrarMensaje()
    {
        if (textoMensaje != null)
        {
            if (mostrarMensajeCoroutine != null)
                StopCoroutine(mostrarMensajeCoroutine);

            mostrarMensajeCoroutine = StartCoroutine(EscribirMensaje());
        }
    }

    IEnumerator EscribirMensaje()
    {
        textoMensaje.gameObject.SetActive(true);
        textoMensaje.text = "";

        float delay = 1f / velocidadTipeo;

        foreach (char letra in mensaje)
        {
            textoMensaje.text += letra;
            yield return new WaitForSeconds(delay);
        }

        // Espera el tiempo restante después de escribir
        yield return new WaitForSeconds(duracion);
        textoMensaje.gameObject.SetActive(false);
    }
}

