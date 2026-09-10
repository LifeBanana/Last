using UnityEngine;

public class WeaponDatabase : MonoBehaviour
{
    //stores all weapons from primary to secondary to be used in other scripts
    public static WeaponDatabase Instance;

    public WeaponData[] weapons;

    void Awake()
    {
        Instance = this;
    }
    //gets weapon from ID
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
    //ID for primary weapons for specific class to get called
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

            case "Demolitionist":
                return "Grenade Launcher";

            case "Scout":
                return "Shotgun";

            case "Driver":
                return "Carbine";

            default:
                return "Assault Rifle";
        }
    }
}