using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutMenu : MonoBehaviour
{
    public PlayerLoadout loadout;

    public TMP_Text pointsText;
    public TMP_Text classText;
    public TMP_Text weaponText;
    public TMP_Text SummaryText;

    public Button confirmButton;

    public StatsRow[] statRows;

    //where the stats modifications process happens
    private void Start()
    {
        loadout.LoadLoadout();

        foreach (StatsRow row in statRows)
        {
            row.OnValueChanged += UpdateStat;
            row.CanIncrease = CanIncreaseStat;
        }

        SyncRows();

        confirmButton.onClick.AddListener(ConfirmBuild);

        Refresh();
    }

    void UpdateStat(StatsRow row)
    {
        switch (row.statName)
        {
            case "Damage":
                int previous = loadout.stats.damage;

                loadout.stats.damage = row.value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.damage = previous;
                }
                break;

            case "Recoil":
                int p = loadout.stats.recoil;

                loadout.stats.recoil = row.value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.recoil = p;
                }
                break;

            case "Reload":
                int pr = loadout.stats.reloadTime;

                loadout.stats.reloadTime = row.value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.reloadTime = pr;
                }
                break;

            case "DamageFalloff":
                int pre = loadout.stats.damageFalloff;

                loadout.stats.damageFalloff = row.value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.damageFalloff = pre;
                }
                break;

            case "RateOfFire":
                int prev = loadout.stats.rateOfFire;

                loadout.stats.rateOfFire = row.value;

                if (Calculator.CalculateCost(loadout.stats) >  PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.rateOfFire = prev;
                }
                break;

            case "Spread":
                int previ = loadout.stats.spread;

                loadout.stats.spread = row.value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.spread = previ;
                }
                break;

            case "ADS":
                int previo = loadout.stats.adsTime;

                loadout.stats.adsTime = row.value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.adsTime = previo;
                }
                break;

            case "SprintFire":
                int previou = loadout.stats.sprintToFire;

                loadout.stats.sprintToFire = row.value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.sprintToFire = previou;
                }
                break;

            case "Health":
                int revious = loadout.stats.health;

                loadout.stats.health = row.value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.health = revious;
                }
                break;

            case "Shields":
                int evious = loadout.stats.shields;

                loadout.stats.shields = row.value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.shields = evious;
                }
                break;

            case "WalkSpeed":
                int vious = loadout.stats.walkSpeed;

                loadout.stats.walkSpeed = row.value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.walkSpeed = vious;
                }
                break;

            case "SprintSpeed":
                int ious = loadout.stats.sprintSpeed;

                loadout.stats.sprintSpeed = row.value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.sprintSpeed = ious;
                }
                break;
        }

        Refresh();
    }
    //refresh the scene containing the tests for player notifications
    void Refresh()
    {
        int cost =  Calculator.CalculateCost(loadout.stats);

        int remaining =  PlayerLoadout.MAX_POINTS - cost;

        pointsText.text =   $"Points Remaining: {remaining}";

        classText.text = "Class: " + ClassGenerator.GetClass(loadout.stats);

        weaponText.text =  $"Weapon: {DetermineWeapon()}";

        confirmButton.interactable = loadout.IsValidBuild();

        //confirmButton.interactable = Calculator.CalculateCost(loadout.stats) <= PlayerLoadout.MAX_POINTS;

        SummaryText.text = GenerateSummary();
    }
    //calls the function to make the weapons and save the data
    void ConfirmBuild()
    {
        loadout.BuildLoadout();
        loadout.SaveLoadout();

        Refresh();

        Statsmanager.Instance.BuildProfile(loadout.stats);
    }
    //detetimes the class and weapons for player
    string DetermineWeapon()
    {
        Stats s = loadout.stats;

        if (loadout.className == "Assault")
            return "1) Assault Rifle  2) Glock";

        if (loadout.className == "Support")
            return "\n 1) SMG  2) P226";

        if (loadout.className == "Heavy")
            return "\n 1) LMG  2) Revolver";

        if (loadout.className == "Scout")
            return "\n 1) Shotgun  2) Magnum";

        if (loadout.className == "Engineer")
            return "\n 1) DMR  2) Machine Pistol";

        if (loadout.className == "Recon")
            return "\n 1) Sniper Rifle  2) USP45";

        if (loadout.className == "Demolitionist")
            return "\n 1) Grenade Launcher 2) Hand Cannon";

        if (loadout.className == "Scout")
            return "\n 1) Shotgun 2) Magnum";

        if (loadout.className == "Driver")
            return "\n 1) Carbine 2) Pocket Rifle";

        return "\n 1) Assault Rifle  2) Glock";
    }
    //gives descriptions when stat threshold reached
    string GenerateSummary()
    {
        var s = loadout.stats;

        string summary = "";

        if (s.damage > 5)
            summary += "High weapon damage.\n";

        if (s.rateOfFire > 5)
            summary += "Excellent DPS output.\n";

        if (s.health > 5)
            summary += "Heavy survivability.\n";

        if (s.sprintSpeed > 5)
            summary += "Very mobile.\n";

        if (s.adsTime > 5)
            summary += "Fast target acquisition.\n";

        if (s.damageFalloff > 5)
            summary += "Strong long-range performance.\n";

        if (s.health < 0)
            summary += "Reduced durability.\n";

        if (s.recoil < 0)
            summary += "Harder to control weapon.\n";

        if (s.reloadTime > 5)
            summary += "Fast reload speed.\n";

        if (s.spread > 5)
            summary += "Excellent hip-fire accuracy.\n";

        if (s.shields > 5)
            summary += "High shield capacity.\n";

        if (s.walkSpeed > 5)
            summary += "Fast tactical movement.\n";

        if (s.sprintToFire > 5)
            summary += "Very quick sprint-out time.\n";

        if (s.reloadTime < 0)
            summary += "Slow reload speed.\n";

        if (s.damageFalloff < 0)
            summary += "Reduced effective range.\n";

        if (s.adsTime < 0)
            summary += "Slower aiming speed.\n";

        if (s.shields < 0)
            summary += "Lower shield capacity.\n";

        if (s.walkSpeed < 0)
            summary += "Reduced movement speed.\n";

        if (s.sprintToFire < 0)
            summary += "Slower sprint-to-fire transition.\n";

        return summary;
    }
    //checks to see the stat can increase
    bool CanIncreaseStat(StatsRow row)
    {
        Stats copy = new Stats();

        copy.damage = loadout.stats.damage;
        copy.recoil = loadout.stats.recoil;
        copy.reloadTime = loadout.stats.reloadTime;
        copy.damageFalloff = loadout.stats.damageFalloff;
        copy.rateOfFire = loadout.stats.rateOfFire;
        copy.spread = loadout.stats.spread;
        copy.adsTime = loadout.stats.adsTime;
        copy.sprintToFire = loadout.stats.sprintToFire;
        copy.health = loadout.stats.health;
        copy.shields = loadout.stats.shields;
        copy.walkSpeed = loadout.stats.walkSpeed;
        copy.sprintSpeed = loadout.stats.sprintSpeed;

        switch (row.statName)
        {
            case "Damage": copy.damage++; break;
            case "Recoil": copy.recoil++; break;
            case "Reload": copy.reloadTime++; break;
            case "DamageFalloff": copy.damageFalloff++; break;
            case "RateOfFire": copy.rateOfFire++; break;
            case "Spread": copy.spread++; break;
            case "ADS": copy.adsTime++; break;
            case "SprintFire": copy.sprintToFire++; break;
            case "Health": copy.health++; break;
            case "Shields": copy.shields++; break;
            case "WalkSpeed": copy.walkSpeed++; break;
            case "SprintSpeed": copy.sprintSpeed++; break;
        }

        return Calculator.CalculateCost(copy) <= PlayerLoadout.MAX_POINTS;
    }
    //sets the value for each stat
    void SyncRows()
    {
        foreach (StatsRow row in statRows)
        {
            switch (row.statName)
            {
                case "Damage":
                    row.SetValue(loadout.stats.damage);
                    break;

                case "Recoil":
                    row.SetValue(loadout.stats.recoil);
                    break;

                case "Reload":
                    row.SetValue(loadout.stats.reloadTime);
                    break;

                case "DamageFalloff":
                    row.SetValue(loadout.stats.damageFalloff);
                    break;

                case "RateOfFire":
                    row.SetValue(loadout.stats.rateOfFire);
                    break;

                case "Spread":
                    row.SetValue(loadout.stats.spread);
                    break;

                case "ADS":
                    row.SetValue(loadout.stats.adsTime);
                    break;

                case "SprintFire":
                    row.SetValue(loadout.stats.sprintToFire);
                    break;

                case "Health":
                    row.SetValue(loadout.stats.health);
                    break;

                case "Shields":
                    row.SetValue(loadout.stats.shields);
                    break;

                case "WalkSpeed":
                    row.SetValue(loadout.stats.walkSpeed);
                    break;

                case "SprintSpeed":
                    row.SetValue(loadout.stats.sprintSpeed);
                    break;
            }
        }
    }
}
