using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase representa los parámetros básicos de una ola.
// Se utiliza tanto en la simulación de olas de Gerstner como en la sinusoidal.


[System.Serializable]
public class Wave
{
    // Amplitud de la ola: controla su altura máxima
    public float amplitude = 1f;

    // Longitud de onda: distancia entre dos crestas consecutivas
    public float wavelength = 2f;

    // Velocidad de propagación de la ola
    public float speed = 1f;

    // Fase inicial de la ola: permite desplazarla en el tiempo
    public float phase = 0f;

    // Dirección de propagación de la ola en el plano XZ
    public Vector2 direction = Vector2.right;

    // Frecuencia espacial angular de la ola
    public float Frequency => 2 * Mathf.PI / wavelength;
}
