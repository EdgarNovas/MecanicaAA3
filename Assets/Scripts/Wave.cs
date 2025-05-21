using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public float amplitude = 1f;
    public float wavelength = 2f;
    public float speed = 1f;
    public float phase = 0f;
    public Vector2 direction = Vector2.right;

    public float Frequency => 2 * Mathf.PI / wavelength;
}
