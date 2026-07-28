using TMPro;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public weaponmanager weaponManager;

     Profile profile;

    public TMP_Text ammoText;

    private void Awake()
    {
        profile = Statsmanager.Instance.Profile;
    }

    void Update()
    {
        Weapon weapon = weaponManager.GetCurrentWeapon();

        if (weapon == null)
            return;

        ammoText.text = weapon.currentAmmo + " / " +  weapon.reserveAmmo;
    }
}
