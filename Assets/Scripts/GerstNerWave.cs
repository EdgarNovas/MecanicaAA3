using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerstNerWave : MonoBehaviour
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

            float x = v.x + dir.x * a * cos;
            float y = a * sin;
            float z = v.z + dir.y * a * cos;

            modifiedVertices[i] = new Vector3(x, y, z);
        }

        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals();
    }
}
