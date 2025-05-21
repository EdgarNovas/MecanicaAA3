using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalWave : MonoBehaviour
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
