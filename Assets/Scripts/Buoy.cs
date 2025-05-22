using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoy : MonoBehaviour
{
    public Transform waterSurface; 
    public float floatStrength = 1f;
    public float smoothTime = 0.2f;

    private IBuoyantWater water;
    private float velocityY = 0f;

    void Start()
    {
        water = waterSurface.GetComponent<IBuoyantWater>();
        if (water == null)
            Debug.LogError("Error: IBuoyantWater not implemented");
    }

    void Update()
    {
        if (water == null) return;

        Vector3 pos = transform.position;
        float waterHeight = water.GetWaterHeightAtPosition(pos);
        float targetY = waterHeight;

        // Suaviza el movimiento vertical
        pos.y = Mathf.SmoothDamp(pos.y, targetY, ref velocityY, smoothTime);
        transform.position = pos;
    }
}
