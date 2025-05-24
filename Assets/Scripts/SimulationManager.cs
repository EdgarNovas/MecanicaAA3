using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public bool isGerstnerWaveActive;
    public GameObject water;
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
            water.GetComponent<GerstNerWave>().enabled = true;
            water.GetComponent<SinusoidalWave>().enabled = false;
            textString = "Simulating: Gerstner Wave";
        }
        else
        {
            water.GetComponent<SinusoidalWave>().enabled = true;
            water.GetComponent<GerstNerWave>().enabled = false;
            textString = "Simulating: Sinusoidal Wave";
        }

        text.text = textString;

    }
}
