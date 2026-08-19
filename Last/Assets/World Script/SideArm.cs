using UnityEngine;
using System.Collections;

public class SideArm : MonoBehaviour
{
    public Camera playerCamera;

    public Transform firePoint;

    public GameObject bulletPrefab;

    public int currentAmmo;
    public int reserveAmmo;
    public int magSize;

    bool reloading;
    bool aiming;

    float nextFireTime;

    public float damage;
    public float recoil;
    public float reloadTime;
    public float fireRate;
    public float spread;
    public float adsFOV;
    public float normalFOV;
    public float adsSpeed;

    public Crosshair crosshair;

    IEnumerator Start()
    {
        while (string.IsNullOrEmpty(SaveManager.Instance.Data.className))
            yield return null;

        Initialize();
        RefreshProfile();
    }

    public void Initialize()
    {
        SetupSideArmAmmo();

        currentAmmo = magSize;

        Profile p = Statsmanager.Instance.Profile;

        damage = p.damage * 0.60f;

        recoil = p.recoil * 0.75f;

        reloadTime = p.reloadTime * 0.85f;

        fireRate = p.fireRate * 0.80f;

        spread = p.spread * 1.30f;

        adsFOV = p.adsFOV;

        normalFOV = p.normalFOV;

        adsSpeed = p.adsSpeed * 1.10f;
    }

    void Update()
    {
        if (!gameObject.activeInHierarchy)
            return;

        HandleADS();
        HandleReload();

        if (reloading)
            return;

        if (Input.GetButton("Fire1"))
            Shoot();
    }

    void SetupSideArmAmmo()
    {
        switch (SaveManager.Instance.Data.className)
        {
            case "Assault":
                magSize = 19;
                reserveAmmo = 76;
                break;

            case "Heavy":
                magSize = 8;
                reserveAmmo = 32;
                break;

            case "Recon":
                magSize = 12;
                reserveAmmo = 48;
                break;

            case "Engineer":
                magSize = 20;
                reserveAmmo = 80;
                break;

            case "Support":
                magSize = 17;
                reserveAmmo = 64;
                break;

            case "Demolitionist":
                magSize = 9;
                reserveAmmo = 36;
                break;

            case "Scout":
                magSize = 17;
                reserveAmmo = 64;
                break;

            case "Driver":
                magSize = 10;
                reserveAmmo = 40;
                break;

            default:
                magSize = 19;
                reserveAmmo = 76;
                break;
        }

        Debug.Log($"Sidearm: {SaveManager.Instance.Data.className} | Mag: {magSize} | Reserve: {reserveAmmo}");
    }

    void HandleADS()
    {
        aiming = Input.GetMouseButton(1);

        float targetFOV = aiming ? adsFOV : normalFOV;

        playerCamera.fieldOfView =  Mathf.Lerp(playerCamera.fieldOfView,  targetFOV,  adsSpeed * Time.deltaTime);
    }

    void HandleReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(Reload());
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

        nextFireTime = Time.time + (1f / fireRate);

        currentAmmo--;

        Vector3 direction = playerCamera.transform.forward;

        direction += playerCamera.transform.right *  Random.Range(-spread, spread) * 0.01f;

        direction += playerCamera.transform.up *    Random.Range(-spread, spread) * 0.01f;

        GameObject bullet =  Instantiate( bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));

        bullet.GetComponent<Bullet>().SetDamage(damage);

        if (crosshair != null)
        {
            crosshair.Fire();
        }

        ApplyRecoil();
    }

    void ApplyRecoil()
    {
        playerCamera.transform.localRotation *=  Quaternion.Euler( -recoil,  Random.Range(-0.25f, 0.25f),  0);
    }

    public void RefreshProfile()
    {
        Profile p = Statsmanager.Instance.Profile;

        damage = p.damage * 0.6f;
        recoil = p.recoil * 0.75f;
        reloadTime = p.reloadTime * 0.85f;
        fireRate = p.fireRate * 0.8f;
        spread = p.spread * 1.3f;

        adsFOV = p.adsFOV;
        normalFOV = p.normalFOV;
        adsSpeed = p.adsSpeed * 1.1f;
    }
}
