using TMPro;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public weaponmanager weaponManager;

    public TMP_Text ammoText;

    void Update()
    {
        Weapon weapon = weaponManager.GetCurrentWeapon();

        if (weapon == null)
            return;

        ammoText.text = weapon.currentAmmo + " / " +  weapon.reserveAmmo;
    }
}
