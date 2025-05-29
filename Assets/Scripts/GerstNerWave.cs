using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implementación de las olas de Gerstner, que deforman la malla en 3D (x, y, z).
// A diferencia de la ola sinusoidal, esta técnica también modifica las coordenadas horizontales.

public class GerstNerWave : MonoBehaviour, IBuoyantWater
{
    public Wave wave = new Wave();  // Parámetros de la ola
    private Mesh mesh;
    private Vector3[] baseVertices, modifiedVertices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
        modifiedVertices = new Vector3[baseVertices.Length];
    }

    // Esta función devuelve la altura del agua en una posición determinada del mundo.
    // Se utiliza para que la boya pueda adaptarse a la superficie del agua.

    public float GetWaterHeightAtPosition(Vector3 position)
    {
        // Convertimos la posición global a coordenadas locales de la malla
        Vector3 localPos = transform.InverseTransformPoint(position);

        float time = Time.time;
        float k = wave.Frequency;
        float a = wave.amplitude;
        Vector2 dir = wave.direction.normalized;
        float omega = wave.speed * k;

        // Calculamos el producto escalar entre la posición y la dirección de propagación de la ola
        float dot = Vector2.Dot(new Vector2(localPos.x, localPos.z), dir);

        // En Gerstner, la altura se basa en una función seno que depende de la dirección de la ola
        float sin = Mathf.Sin(k * dot - omega * time + wave.phase);

        // Devolvemos la altura en coordenadas globales
        return transform.position.y + a * sin;
    }


    void Update()
    {
        float time = Time.time;
        float k = wave.Frequency;
        float a = wave.amplitude;
        Vector2 dir = wave.direction.normalized;
        float omega = wave.speed * k;

        for (int i = 0; i < baseVertices.Length; i++)
        {
            Vector3 v = baseVertices[i];
            float dot = Vector2.Dot(new Vector2(v.x, v.z), dir);
            float cos = Mathf.Cos(k * dot - omega * time + wave.phase);
            float sin = Mathf.Sin(k * dot - omega * time + wave.phase);


            // Diferencia clave respecto a la onda sinusoidal:
            // se modifican también las coordenadas x y z para simular el desplazamiento horizontal.
            float x = v.x + dir.x * a * cos;
            float y = a * sin;
            float z = v.z + dir.y * a * cos;

            modifiedVertices[i] = new Vector3(x, y, z);
        }

        // Aplicamos los cambios a la malla
        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals();  // Necesario para que la iluminación se actualice 
    }
}
