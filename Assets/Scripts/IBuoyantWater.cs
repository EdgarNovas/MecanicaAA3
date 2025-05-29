using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta interfaz permite obtener la altura del agua en una posici�n determinada del mundo.

public interface IBuoyantWater
{
    // Devuelve la altura del agua (eje Y) en una posici�n dada del espacio global.
    // Implementaciones concretas: GerstnerWave.cs y SinusoidalWave.cs.

    float GetWaterHeightAtPosition(Vector3 position);
}