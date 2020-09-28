using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    Weapon[] weapons;
    int activeWeapon = 0;
    bool next = true;
    WeaponZoom weaponZoom;
    [SerializeField] AudioClip[] fireSound;

    private void Start()
    {
        weaponZoom = GetComponent<WeaponZoom>();
        weapons = GetComponentsInChildren<Weapon>();
        for (var i = weapons.Length-1; i > 0; i--){
            weapons[i].gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    
    void Update()
    {
        ProcessChangeWeapon();
    }
    public AudioClip GetActiveWeaponSound()
    {
        print(fireSound[activeWeapon].name);
        return fireSound[activeWeapon];
    }
    private void ProcessChangeWeapon()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            GetComponentInParent<Animator>().SetTrigger("Change Weapon");
            next = true;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            GetComponentInParent<Animator>().SetTrigger("Change Weapon");
            next = false;
        }
    }

    private void AlternateWeapon()
    {
        weaponZoom.SetZoomedInToggle(false);
        weaponZoom.ToggleZoom();
        weapons[activeWeapon].gameObject.SetActive(false);
        if (next)
        {
            if(activeWeapon == weapons.Length - 1)
            {
                activeWeapon = 0;
            }
            else
            {
                activeWeapon++;
            }
        }
        else
        {
            if (activeWeapon == 0)
            {
                activeWeapon = weapons.Length - 1;
            }
            else
            {
                activeWeapon--;
            }
        }
        print(activeWeapon);
        weapons[activeWeapon].gameObject.SetActive(true);
    }
}
