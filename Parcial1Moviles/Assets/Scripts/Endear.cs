using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Endear : MonoBehaviour
{
    public GameObject panelUI; // Panel que contiene el mensaje y el botón
    public TextMeshProUGUI mensajeTexto;
    public string mensaje = "¡Has alcanzado el final!";
    public Button botonReiniciar;

    private bool juegoPausado = false;

    void Start()
    {
        if (panelUI != null)
            panelUI.SetActive(false);

        if (botonReiniciar != null)
            botonReiniciar.onClick.AddListener(VolverAScena0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!juegoPausado && other.CompareTag("Player"))
        {
            PausarJuego();
        }
    }

    void PausarJuego()
    {
        juegoPausado = true;
        Time.timeScale = 0f; // Detiene el tiempo
        if (mensajeTexto != null)
            mensajeTexto.text = mensaje;
        if (panelUI != null)
            panelUI.SetActive(true);
    }

    public void VolverAScena0()
    {
        Time.timeScale = 1f; // Reanudar tiempo antes de cambiar de escena
        SceneManager.LoadScene(0);
    }
}
