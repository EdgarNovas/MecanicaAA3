using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Este script permite alternar dinámicamente entre las dos simulaciones de olas: Gerstner y sinusoidal.
// Está pensado como un gestor central para activar/desactivar las olas y actualizar la interfaz de usuario.

public class SimulationManager : MonoBehaviour
{
    public bool isGerstnerWaveActive;        // Indica cuál simulación está activa actualmente
    public GameObject gerstnerWater;         // Objeto que contiene la simulación Gerstner
    public GameObject sinusoidalWater;       // Objeto que contiene la simulación sinusoidal
    public TextMeshProUGUI text;             // Texto UI que indica el estado actual de la simulación


    void Start()
    {
        // Inicializamos la simulación con las olas de Gerstner activadas
        isGerstnerWaveActive = true;
        SetText();  // Actualizamos el texto de la interfaz
    }

    void Update()
    {
        // Permite al usuario cambiar de simulación pulsando las teclas 1 o 2
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isGerstnerWaveActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isGerstnerWaveActive = false;
        }
        SetText(); // Aplicamos el cambio visualmente y en el texto
    }

    void SetText()
    {
        string textString;

        if (isGerstnerWaveActive)
        {
            gerstnerWater.SetActive(true);      // Activamos Gerstner
            sinusoidalWater.SetActive(false);   // Desactivamos Sinusoidal
            textString = "Simulating: Gerstner Wave";
        }
        else
        {
            sinusoidalWater.SetActive(true);    // Activamos Sinusoidal
            gerstnerWater.SetActive(false);     // Desactivamos Gerstner
            textString = "Simulating: Sinusoidal Wave";
        }

        text.text = textString;
    }
}
