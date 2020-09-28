﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }

    public int GetAmmoAmount(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public void ReduceAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--;
    }

    AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlots) 
        {
            if(slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }

    public void IncreaseCurrentAmmo(int ammoAmount, AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount += ammoAmount;
    }
}
