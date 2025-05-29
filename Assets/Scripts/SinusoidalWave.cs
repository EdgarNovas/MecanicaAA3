using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implementación de una ola sinusoidal simple.
// A diferencia de las olas de Gerstner, esta simulación solo afecta la coordenada vertical (y)
// de los vértices de la malla, sin generar desplazamiento horizontal.

public class SinusoidalWave : MonoBehaviour, IBuoyantWater
{
    public Wave wave = new Wave();  // Parámetros de la ola
    private Mesh mesh;
    private Vector3[] baseVertices, modifiedVertices;

    void Start()
    {
        // Obtenemos los vértices de la malla original
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
        modifiedVertices = new Vector3[baseVertices.Length];
    }

    // Esta función permite a la boya obtener la altura del agua en una posición dada.
    // Se calcula aplicando la fórmula de una onda sinusoidal en función del tiempo y posición.

    public float GetWaterHeightAtPosition(Vector3 position)
    {
        // Convertimos la posición global a coordenadas locales de la malla
        Vector3 localPos = transform.InverseTransformPoint(position);

        float time = Time.time;
        float k = wave.Frequency;
        Vector2 dir = wave.direction.normalized;

        // Producto escalar para aplicar la dirección de propagación de la ola
        float dot = Vector2.Dot(new Vector2(localPos.x, localPos.z), dir);

        // Solo se calcula el desplazamiento vertical
        float y = wave.amplitude * Mathf.Sin(k * (dot - wave.speed * time) + wave.phase);

        // Convertimos el resultado a altura global
        return transform.position.y + y;
    }

    void Update()
    {
        float time = Time.time;
        for (int i = 0; i < baseVertices.Length; i++)
        {
            Vector3 v = baseVertices[i];

            // Proyectamos cada vértice en la dirección de la onda
            float dot = Vector2.Dot(new Vector2(v.x, v.z), wave.direction.normalized);

            // Solo modificamos la coordenada Y (altura)
            v.y = wave.amplitude * Mathf.Sin(wave.Frequency * (dot - wave.speed * time) + wave.phase);
            modifiedVertices[i] = v;
        }
        // Actualizamos la malla con las nuevas alturas
        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals(); // Necesario para que la luz se calcule correctamente
    }
}
