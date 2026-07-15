using UnityEngine;

public class PlayerLoadout : MonoBehaviour
{
    public const int MAX_POINTS = 30;

    public Stats stats = new Stats();

    public WeaponProfile weapon;
    public Profile character;

    public string className;

    [SerializeField]
    private Gun equippedGun;

    [SerializeField]
    private PlayerMove movement;

    private void Start()
    {
        BuildLoadout();
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

        weapon = WeaponBuild.Build(stats);
        equippedGun.ApplyProfile(weapon);

        character = Build.build(stats);
        movement.ApplyProfile(character);

        className = ClassGenerator.GetClass(stats);
        string secondary = secondweapon.GetSecondary(className);
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

        SaveManager.Instance.SaveGame();
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
