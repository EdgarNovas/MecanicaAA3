using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta interfaz permite obtener la altura del agua en una posición determinada del mundo.

public interface IBuoyantWater
{
    // Devuelve la altura del agua (eje Y) en una posición dada del espacio global.
    // Implementaciones concretas: GerstnerWave.cs y SinusoidalWave.cs.

    float GetWaterHeightAtPosition(Vector3 position);
}