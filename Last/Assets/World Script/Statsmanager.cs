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
        stats.damageFalloff = save.damageFalloff;
        stats.health = save.health;
        stats.shields = save.shields;
        stats.walkSpeed = save.walkSpeed;
        stats.sprintSpeed = save.sprintSpeed;
        Profile = WeaponBuild.Build(stats);
        Profile = ProfileBuild.Build();
    }

    public void RefreshProfile()
    {
        BuildProfileFromSave();

        PlayerTree tree = FindFirstObjectByType<PlayerTree>();

        if (tree != null)
            SkillCal.ApplySkills(Profile);

        ProfileBuild.ApplyAttachments(Profile);

        Weapon weapon = FindFirstObjectByType<Weapon>();

        if (weapon != null)
            weapon.Initialize();

        SideArm sidearm = FindFirstObjectByType<SideArm>();

        if (sidearm != null)
            sidearm.Initialize();

        Controller controller = FindFirstObjectByType<Controller>();

        if (controller != null)
            controller.Initialize();

        Health health = FindFirstObjectByType<Health>();

        if (health != null)
            health.RefreshHealth();
    }
}
