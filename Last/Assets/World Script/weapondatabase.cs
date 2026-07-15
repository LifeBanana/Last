using UnityEngine;

public class WeaponDatabase : MonoBehaviour
{
    public static WeaponDatabase Instance;

    public WeaponData[] weapons;

    void Awake()
    {
        Instance = this;
    }

    public GameObject GetWeapon(string id)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.weaponID == id)
                return weapon.prefab;
        }

        return null;
    }
}

[System.Serializable]
public class WeaponData
{
    public string weaponID;

    public GameObject prefab;
}
