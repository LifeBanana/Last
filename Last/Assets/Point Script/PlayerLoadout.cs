using UnityEngine;

public class PlayerLoadout : MonoBehaviour
{
    public const int MAX_POINTS = 30;

    public Stats stats = new Stats();

    public Profile character;

    public string className;

    [SerializeField]
    private Gun equippedGun;

    [SerializeField]
    private PlayerMove movement;

    private void Start()
    {
        LoadLoadout();
    }

    public bool IsValidBuild()
    {
        return Calculator.CalculateCost(stats)
            <= MAX_POINTS;
    }

    public void BuildLoadout()
    {
        if (!IsValidBuild())
        {
            Debug.LogError("Build exceeds point limit.");
            return;
        }

        Statsmanager.Instance.BuildProfile(stats);

        character = Statsmanager.Instance.Profile;
        equippedGun.ApplyProfile(character);
        movement.ApplyProfile(character);

        className = ClassGenerator.GetClass(stats);
        string secondary = secondweapon.GetSecondary(className);

        SaveManager.Instance.Data.className = className;
        SaveManager.Instance.Data.primaryWeaponID = className;
        SaveManager.Instance.Data.secondaryWeaponID = secondweapon.GetSecondary(className);
        Previewmanager.Instance.ShowWeapon(className);

        Debug.Log("Generated Class: " + className);
    }

    public void SaveLoadout()
    {
        var save = SaveManager.Instance.Data;

        save.damage = stats.damage;
        save.recoil = stats.recoil;
        save.reload = stats.reloadTime;
        save.damageFalloff = stats.damageFalloff;
        save.fireRate = stats.rateOfFire;
        save.spread = stats.spread;
        save.ads = stats.adsTime;
        save.sprintFire = stats.sprintToFire;

        save.health = stats.health;
        save.shields = stats.shields;
        save.walkSpeed = stats.walkSpeed;
        save.sprintSpeed = stats.sprintSpeed;

        save.className = className;

        save.primaryWeaponID = className;

        save.secondaryWeaponID = secondweapon.GetSecondary(className);

        SaveManager.Instance.SaveGame();

        Statsmanager.Instance.BuildProfile(stats);

        Loadoutmanager manager = FindFirstObjectByType<Loadoutmanager>();

        if (manager != null)
            manager.Initialize();

        if (Previewmanager.Instance != null)
        {
            Previewmanager.Instance.RefreshPreview();
        }

        Health health = FindFirstObjectByType<Health>();

        if (health != null)
        {
            health.RefreshHealth();
        }
    }

    public void LoadLoadout()
    {
        var save = SaveManager.Instance.Data;

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

        BuildLoadout();
    }
}
