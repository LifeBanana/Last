using UnityEngine;

[System.Serializable]
public class WeaponPreview
{
    public string weaponID;

    public WeaponSlot slot;

    [Header("Primary")]
    public GameObject primaryWeaponPrefab;

    [Header("Secondary")]
    public GameObject secondaryWeaponPrefab;
}

