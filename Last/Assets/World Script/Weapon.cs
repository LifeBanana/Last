using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    public Camera playerCamera;

    public Transform firePoint;

    public GameObject bulletPrefab;

    public int magSize = 30;
    public bool isPrimary;
    public int reserveAmmo = 120;
    public int currentAmmo;

    public float reloadTime = 2f;

    bool reloading;

    public float fireRate = 10f;
    public float sprintToFireDelay = 0.2f;

    float nextFireTime;

    public float spread = 1f;

    public float recoilAmount = 2f;

    public float normalFOV = 75f;
    public float adsFOV = 55f;
    public float adsSpeed = 10f;

    bool aiming;

    void Start()
    {
        if (isPrimary)
        {
            magSize = 30;
            reserveAmmo = 120;
        }
        else
        {
            magSize = 15;
            reserveAmmo = 60;
        }

        currentAmmo = magSize;
    }

    void Update()
    {
        HandleADS();
        HandleReload();

        if (reloading)
            return;

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    void HandleADS()
    {
        aiming =   Input.GetMouseButton(1);

        float targetFOV =  aiming  ? adsFOV   : normalFOV;

        playerCamera.fieldOfView =  Mathf.Lerp(  playerCamera.fieldOfView,  targetFOV,   adsSpeed *   Time.deltaTime);
    }

    void HandleReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

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

    void Shoot()
    {
        if (currentAmmo <= 0)
            return;

        if (Time.time < nextFireTime)
            return;

        nextFireTime =   Time.time +   1f / fireRate;

        currentAmmo--;

        Vector3 direction =   playerCamera.transform.forward;

        direction +=  playerCamera.transform.right *   UnityEngine.Random.Range(-spread, spread) *   0.01f;

        direction +=   playerCamera.transform.up *  UnityEngine.Random.Range(-spread, spread) *  0.01f;

        GameObject bullet =  Instantiate(   bulletPrefab,    firePoint.position,   Quaternion.LookRotation(direction));

        ApplyRecoil();
    }

    void ApplyRecoil()
    {
        playerCamera.transform.localRotation *=    Quaternion.Euler(   -recoilAmount,    UnityEngine.Random.Range(-0.5f, 0.5f),      0);
    }
}