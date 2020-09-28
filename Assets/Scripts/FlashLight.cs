using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashLight : MonoBehaviour
{
    [SerializeField] float lightDecay = .1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minimumAngle = 20f;
    [SerializeField] TextMeshProUGUI batteryText;
    private float batteryLvl = 1;

    Light myLight;
    float maxIntensity = 5f;
    float maxSpotAngle = 50f;

    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Light>().enabled)
        {
            DecreaseBatteryLvl();
            SetLightAngle();
            SetLightIntensity();
        }
        ProcessTrigger();
        DisplayBattery();
    }

    private void DecreaseBatteryLvl()
    {
        if(batteryLvl > 0) batteryLvl -= 0.01f * Time.deltaTime;
    }

    private void DisplayBattery()
    {
        if(batteryLvl > 0)
        batteryText.text = "Battery: " + Math.Floor(batteryLvl * 100).ToString();
    }

    private void ProcessTrigger()
    {
        if (Input.GetButtonDown("FlashLight"))
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        }
    }

    private void SetLightIntensity()
    {
        myLight.intensity = maxIntensity * batteryLvl;
        if (myLight.intensity < 0)
        {
            myLight.intensity = 0;
        }
    }

    private void SetLightAngle()
    {
        myLight.spotAngle = maxSpotAngle * batteryLvl;
        if (myLight.spotAngle < minimumAngle)
        {
            myLight.spotAngle = minimumAngle;
        }
    }

    public void RestoreBattery(float addBattery)
    {
        batteryLvl += addBattery;
        if (batteryLvl > 1f) batteryLvl = 1f;
        print(batteryLvl + " " + maxIntensity * batteryLvl + " " + maxSpotAngle * batteryLvl);
    }
    /*public void RestoreLightAngle(float restoreAngle)
    {
        myLight.spotAngle += restoreAngle;
        if (myLight.spotAngle > 50f) myLight.spotAngle = 50f;
    }

    public void RestoreLightIntensity(float intensityAmount)
    {
        myLight.intensity += intensityAmount;
        if (myLight.intensity > 3f) myLight.intensity = 5f;
    }*/
}
