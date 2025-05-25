using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Buoy : MonoBehaviour
{
    public Transform gerstnerWaterSurface;   // Objeto con el componente Gerstner o Sinusoidal
    public Transform sinusoidalWaterSurface;   // Objeto con el componente Gerstner o Sinusoidal


    private IBuoyantWater gerstnerWater;
    private IBuoyantWater sinusoidalWater;

    public float buoyancyStrength = 5f;
    public float damping = 0.1f;
    private float velocityY = 0f;

    void Start()
    {
        gerstnerWater = gerstnerWaterSurface.GetComponent<IBuoyantWater>();
        if (gerstnerWater == null)
        {
            Debug.LogError("El objeto de agua no implementa IBuoyantWater");
        }

        sinusoidalWater = sinusoidalWaterSurface.GetComponent<IBuoyantWater>();
        if (sinusoidalWater == null)
        {
            Debug.LogError("El objeto de agua no implementa IBuoyantWater");
        }
    }

    void Update()
    {
        Vector3 pos = transform.position;

        float waterHeight = 0f;


        if (gerstnerWaterSurface.gameObject.activeSelf)
        {
            if (gerstnerWater == null) return;

            waterHeight = gerstnerWater.GetWaterHeightAtPosition(pos);
        }
        else if (sinusoidalWaterSurface.gameObject.activeSelf)
        {
            if (sinusoidalWater == null) return;

            waterHeight = sinusoidalWater.GetWaterHeightAtPosition(pos);
        }


        // Obtener altura del agua en esa posición

        // Desfase vertical entre boya y superficie
        float displacement = waterHeight - pos.y;

        // Fuerza de flotación simulada (sin Rigidbody)
        float acceleration = displacement * buoyancyStrength;

        // Aplicar velocidad y amortiguación
        velocityY += acceleration * Time.deltaTime;
        velocityY *= (1f - damping);

        // Actualizar posición
        pos.y += velocityY * Time.deltaTime;
        transform.position = pos;
    }
}
