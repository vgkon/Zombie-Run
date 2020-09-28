using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    /*[SerializeField] float restoreAngle = 30f;
    [SerializeField] float addIntensity = 1f;*/
    [SerializeField] float addBattery = 0.4f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponentInChildren<FlashLight>().RestoreBattery(addBattery);
        }
        Destroy(gameObject);
    }

}
