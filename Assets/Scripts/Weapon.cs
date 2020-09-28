using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 10f;
    [SerializeField] GameObject hitEffect;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] AmmoType ammoType;
    Ammo ammoSlot;
    ParticleSystem muzzleFlash;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] AudioSource audioSource;

    bool canShoot = true;
    private void Start()
    {
        FPCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        ammoSlot = GetComponentInParent<Ammo>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        canShoot = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (sceneLoader.IsPlaying())
        {
            DisplayAmmo();
            ProcessFire();
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetAmmoAmount(ammoType);
        ammoText.text =ammoType.ToString()+ " ammo: " + currentAmmo.ToString();
    }

    private void ProcessFire()
    {
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            StartCoroutine("Shoot");
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetAmmoAmount(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceAmmo(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        print(GetComponentInParent<ChangeWeapon>().GetActiveWeaponSound().name);
        audioSource.PlayOneShot(GetComponentInParent<ChangeWeapon>().GetActiveWeaponSound());
        
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            try
            {
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                target.TakeDamage(damage);
            }
            catch
            {
                Debug.Log("Item does not have Enemyhealth script");
            }
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }
}
