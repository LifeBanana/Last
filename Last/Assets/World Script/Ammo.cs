using TMPro;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public weaponmanager weaponManager;

    public TMP_Text ammoText;

    void Update()
    {
        if (weaponManager == null)
            return;

        if (weaponManager.primaryWeapon != null && weaponManager.primaryWeapon.gameObject.activeSelf)
        {
            ammoText.text = "Ammo: " + weaponManager.primaryWeapon.currentAmmo + " / " +  weaponManager.primaryWeapon.reserveAmmo;
        }
        else if (weaponManager.secondaryWeapon != null &&   weaponManager.secondaryWeapon.gameObject.activeSelf)
        {
            ammoText.text = "Ammo: " + weaponManager.secondaryWeapon.currentAmmo + " / " +  weaponManager.secondaryWeapon.reserveAmmo;
        }
        else
        {
            ammoText.text = "";
        }
    }
}
