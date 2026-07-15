using UnityEngine;
using System.Collections;

public class weaponmanager : MonoBehaviour
{
    public Weapon primaryWeapon;

    public Weapon secondaryWeapon;

    public KeyCode primaryKey = KeyCode.Alpha1;

    public KeyCode secondaryKey = KeyCode.Alpha2;

    public KeyCode quickSwapKey = KeyCode.Q;

    Weapon currentWeapon;

    bool usingPrimary = true;

    public float switchTime = 0.4f;

    bool switching;

    void Start()
    {
        StartCoroutine(SwitchRoutine(true));
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(primaryKey))
        {
            StartCoroutine(SwitchRoutine(true));
        }

        if (Input.GetKeyDown(secondaryKey))
        {
            StartCoroutine(SwitchRoutine(false));
        }

        if (Input.GetKeyDown(quickSwapKey))
        {
            ToggleWeapon();
        }

        float wheel = Input.GetAxis("Mouse ScrollWheel");

        if (wheel > 0)
        {
            ToggleWeapon();
        }

        if (wheel < 0)
        {
            ToggleWeapon();
        }
    }

    public void EquipPrimary()
    {
        usingPrimary = true;

        primaryWeapon.gameObject.SetActive(true);

        secondaryWeapon.gameObject.SetActive(false);

        currentWeapon = primaryWeapon;
    }

    public void EquipSecondary()
    {
        usingPrimary = false;

        primaryWeapon.gameObject.SetActive(false);

        secondaryWeapon.gameObject.SetActive(true);

        currentWeapon = secondaryWeapon;
    }

    public void ToggleWeapon()
    {
        if (usingPrimary)
        {
            StartCoroutine(SwitchRoutine(false));
        }
        else
        {
            StartCoroutine(SwitchRoutine(true));
        }
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    IEnumerator SwitchRoutine(bool primary)
    {
        if (switching)
            yield break;

        switching = true;

        primaryWeapon.gameObject.SetActive(false);
        secondaryWeapon.gameObject.SetActive(false);

        yield return new WaitForSeconds(switchTime);

        if (primary)
            EquipPrimary();
        else
            EquipSecondary();

        switching = false;
    }
}