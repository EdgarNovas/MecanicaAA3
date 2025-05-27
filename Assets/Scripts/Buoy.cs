using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Buoy : MonoBehaviour
{
    public Transform gerstnerWaterSurface;   // Objeto con el componente Gerstner
    public Transform sinusoidalWaterSurface;   // Objeto con el componente Sinusoidal


    private IBuoyantWater gerstnerWater;
    private IBuoyantWater sinusoidalWater;

    public float buoyVolume = 1f;        // m3
    public float buoyHeight = 1f;        // Altura del cuerpo (m)
    public float fluidDensity = 1000f;   // Agua dulce = 1000 kg/m3
    public float gravity = 9.81f;

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

        // Calcular cuánto está sumergida la boya
        float submergedDepth = waterHeight - (pos.y - buoyHeight / 2f); // Parte inferior de la boya

        if (submergedDepth > 0f)
        {
            // Limitamos a la altura de la boya (no puede sumergirse más de su tamaño)
            submergedDepth = Mathf.Min(submergedDepth, buoyHeight);

            // Volumen desplazado proporcional a profundidad
            float displacedVolume = (submergedDepth / buoyHeight) * buoyVolume;

            // Flotabilidad: F = p g V
            float buoyantForce = fluidDensity * gravity * displacedVolume;

            // Simular aceleración: a = F / m, asumimos masa = volumen (densidad 1), así que a = F
            float acceleration = buoyantForce;

            // Integrar velocidad y aplicar amortiguación
            velocityY += acceleration * Time.deltaTime;
            velocityY *= (1f - damping);

            pos.y += velocityY * Time.deltaTime;
        }
        else
        {
            // En el aire: gravedad simple
            velocityY -= gravity * Time.deltaTime;
            pos.y += velocityY * Time.deltaTime;
        }

        transform.position = pos;
    }
}
