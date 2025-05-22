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
        float time = Time.time;
        float dot = Vector2.Dot(new Vector2(position.x, position.z), wave.direction.normalized);
        return wave.amplitude * Mathf.Sin(wave.Frequency * (dot - wave.speed * time) + wave.phase);
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
