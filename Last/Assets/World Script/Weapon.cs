using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//where the primary main weapons happens 
public class Weapon : MonoBehaviour
{
    public Camera playerCamera;

    public Transform firePoint;

    public GameObject bulletPrefab;

    public bool isPrimary;
    public int currentAmmo;

    bool reloading;

    float nextFireTime;

    bool aiming;

    public int reserveAmmo = 120;
    public int sideAmmo = 60;
    public int magSize = 30;

    Profile profile;

    public float damage;
    public float recoil;
    public float reloadTime;
    public float fireRate;
    public float spread;
    public float adsTime;
    public float sprintToFire;
    public float adsFOV;
    public float normalFOV;
    public float adsSpeed;

    public Crosshair crosshair;
    //waits for system to load in the new changes
    IEnumerator Start()
    {
        while (string.IsNullOrEmpty(SaveManager.Instance.Data.className))
            yield return null;

        Initialize();
        RefreshProfile();
    }
    //applying new stats changes for weapons
    public void Initialize()
    {
        if (isPrimary)
        {
            SetupPrimaryAmmo();
        }
        else
        {
            magSize = 15;
            reserveAmmo = 60;
        }

        currentAmmo = magSize;

        Profile p = Statsmanager.Instance.Profile;

        damage = p.damage;
        fireRate = p.fireRate;
        spread = p.spread;
        reloadTime = p.reloadTime;
        recoil = p.recoil;

        adsFOV = p.adsFOV;
        normalFOV = p.normalFOV;
        adsSpeed = p.adsSpeed;
    }
    //where the player input happens
    void Update()
    {
        if (!gameObject.activeInHierarchy)
            return;

        HandleADS();
        HandleReload();

        if (reloading)
            return;

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }
    //switch statement to set ammo amount for specific class
    void SetupPrimaryAmmo()
    {
        string className = SaveManager.Instance.Data.className;

        Debug.Log("Current Class = " + className);

        switch (className)
        {
            case "Assault":
                magSize = 30;
                reserveAmmo = 120;
                break;

            case "Heavy":
                magSize = 100;
                reserveAmmo = 400;
                break;

            case "Recon":
                magSize = 10;
                reserveAmmo = 40;
                break;

            case "Engineer":
                magSize = 20;
                reserveAmmo = 80;
                break;

            case "Support":
                magSize = 30;
                reserveAmmo = 120;
                break;

            case "Demolitionist":
                magSize = 48;
                reserveAmmo = 192;
                break;

            case "Scout":
                magSize = 58;
                reserveAmmo = 232;
                break;

            case "Driver":
                magSize = 54;
                reserveAmmo = 212;
                break;

            default:
                magSize = 30;
                reserveAmmo = 120;
                break;
        }

        Debug.Log("Mag = " + magSize + " Reserve = " + reserveAmmo);
    }
    //player hold right mouse to zoom in on weapon
    void HandleADS()
    {
        aiming =   Input.GetMouseButton(1);

        float targetFOV =  aiming  ? adsFOV   : normalFOV;

        playerCamera.fieldOfView =  Mathf.Lerp(  playerCamera.fieldOfView,  targetFOV,   adsSpeed *   Time.deltaTime);
    }
    //player press r to reload
    void HandleReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }
    //reloading of current ammo for weapon
    IEnumerator Reload()
    {
        if (currentAmmo >= magSize)
            yield break;

        if (reserveAmmo <= 0)
            yield break;

        reloading = true;

        yield return new WaitForSeconds(reloadTime);

        int needed = magSize - currentAmmo;

        int amount = Mathf.Min(needed, reserveAmmo);

        currentAmmo += amount;

        reserveAmmo -= amount;

        reloading = false;
    }
    //firing of the bullet in the player directions
    void Shoot()
    {
        if (currentAmmo <= 0)
            return;

        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + 1f / fireRate;

        currentAmmo--;

        Vector3 targetPoint = GetCrosshairTarget();

        Vector3 direction = targetPoint - firePoint.position;

        direction.Normalize();

        direction += playerCamera.transform.right * UnityEngine.Random.Range(-spread, spread) * 0.01f;

        direction += playerCamera.transform.up * UnityEngine.Random.Range(-spread, spread) * 0.01f;

        direction.Normalize();

        GameObject bullet = Instantiate(  bulletPrefab, firePoint.position,  Quaternion.LookRotation(direction)  );

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDamage(damage);
        }

        if (crosshair != null)
        {
            crosshair.Fire();
        }

        ApplyRecoil();
    }
    //recoil to weapon
    void ApplyRecoil()
    {
        playerCamera.transform.localRotation *=    Quaternion.Euler(   -recoil,    UnityEngine.Random.Range(-0.5f, 0.5f),      0);
    }
    //loads in the new changes from refrsh
    public void RefreshProfile()
    {
        Profile p = Statsmanager.Instance.Profile;

        damage = p.damage;
        recoil = p.recoil;
        reloadTime = p.reloadTime;
        fireRate = p.fireRate;
        spread = p.spread;
        adsTime = p.adsTime;
        sprintToFire = p.sprintToFire;
        adsFOV = p.adsFOV;
        normalFOV = p.normalFOV;
        adsSpeed = p.adsSpeed;
    }
    //fires at the direction of crosshairs
    Vector3 GetCrosshairTarget()
    {
        Ray ray = playerCamera.ViewportPointToRay( new Vector3(0.5f, 0.5f, 0f)
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            return hit.point;
        }

        return ray.origin + ray.direction * 1000f;
    }
}