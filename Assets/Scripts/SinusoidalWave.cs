using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalWave : MonoBehaviour, IBuoyantWater
{
    public Wave wave = new Wave();
    private Mesh mesh;
    private Vector3[] baseVertices, modifiedVertices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
        modifiedVertices = new Vector3[baseVertices.Length];
    }

    public float GetWaterHeightAtPosition(Vector3 position)
    {
        Vector3 localPos = transform.InverseTransformPoint(position);

        float time = Time.time;
        float k = wave.Frequency;
        Vector2 dir = wave.direction.normalized;

        float dot = Vector2.Dot(new Vector2(localPos.x, localPos.z), dir);
        float y = wave.amplitude * Mathf.Sin(k * (dot - wave.speed * time) + wave.phase);

        // Volver a coordenadas globales (el centro del plano puede estar más arriba o más abajo)
        return transform.position.y + y;
    }

    void Update()
    {
        float time = Time.time;
        for (int i = 0; i < baseVertices.Length; i++)
        {
            Vector3 v = baseVertices[i];
            float dot = Vector2.Dot(new Vector2(v.x, v.z), wave.direction.normalized);
            v.y = wave.amplitude * Mathf.Sin(wave.Frequency * (dot - wave.speed * time) + wave.phase);
            modifiedVertices[i] = v;
        }
        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals();
    }
}
