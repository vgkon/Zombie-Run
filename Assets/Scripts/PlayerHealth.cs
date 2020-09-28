using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] TextMeshProUGUI healthText;
    private void Update()
    {
        DisplayHealth();
    }

    private void DisplayHealth()
    {
        healthText.text = "Health:" + hitPoints.ToString();
    }

    public void TakeDamage(float dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
            print("you dead, my glip glop");
        }
    }

    public void RestoreHealth(float addHealth)
    {
        hitPoints += addHealth;
        if (hitPoints > 100) hitPoints = 100;
    }
}
