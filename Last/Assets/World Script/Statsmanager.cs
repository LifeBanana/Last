using UnityEngine;

public class Statsmanager : MonoBehaviour
{
    public static Statsmanager Instance;

    public Profile Profile = new Profile();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        BuildProfileFromSave();
    }

    public void BuildProfile(Stats stats)
    {
        Profile = WeaponBuild.Build(stats);
    }

    void BuildProfileFromSave()
    {
        Stats stats = new Stats();
        Debug.Log(SaveManager.Instance);
        SaveData save = SaveManager.Instance.Data;

        stats.damage = save.damage;
        stats.recoil = save.recoil;
        stats.reloadTime = save.reload;
        stats.damageFalloff = save.damageFalloff;
        stats.rateOfFire = save.fireRate;
        stats.spread = save.spread;
        stats.adsTime = save.ads;
        stats.sprintToFire = save.sprintFire;

        stats.health = save.health;
        stats.shields = save.shields;
        stats.walkSpeed = save.walkSpeed;
        stats.sprintSpeed = save.sprintSpeed;

        Profile = WeaponBuild.Build(stats);
    }
}
