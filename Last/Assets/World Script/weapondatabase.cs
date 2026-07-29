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
        Debug.Log("Looking for weapon: " + id);

        foreach (var weapon in weapons)
        {
            Debug.Log("Database contains: " + weapon.weaponID);

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

[System.Serializable]
public class PrimaryWeapon
{
    public string className;

    public GameObject weaponPrefab;
}

public static class Primary
{
    public static string GetPrimary(string className)
    {
        switch (className)
        {
            case "Assault":
                return "Assault Rifle";

            case "Heavy":
                return "LMG";

            case "Recon":
                return "Sniper Rifle";

            case "Engineer":
                return "DMR";

            case "Support":
                return "SMG";

            default:
                return "Assault Rifle";
        }
    }
}