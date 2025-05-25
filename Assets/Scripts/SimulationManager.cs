using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public bool isGerstnerWaveActive;
    public GameObject gerstnerWater;
    public GameObject sinusoidalWater;
    public TextMeshProUGUI text;

    void Start()
    {
        isGerstnerWaveActive = true;
        SetText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isGerstnerWaveActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isGerstnerWaveActive = false;
        }
        SetText();
    }

    void SetText()
    {
        string textString;

        if (isGerstnerWaveActive)
        {
            gerstnerWater.SetActive(true);
            sinusoidalWater.SetActive(false);

            textString = "Simulating: Gerstner Wave";
        }
        else
        {
            sinusoidalWater.SetActive(true);
            gerstnerWater.SetActive(false);
            textString = "Simulating: Sinusoidal Wave";
        }

        text.text = textString;

    }
}
