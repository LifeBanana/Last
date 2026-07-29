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

    private void Start()
    {
        loadout.LoadLoadout();

        foreach (var row in statRows)
        {
            row.OnValueChanged += UpdateStat;
        }

        confirmButton.onClick.AddListener(ConfirmBuild);

        Refresh();
    }

    void UpdateStat(string stat, int value)
    {
        switch (stat)
        {
            case "Damage":
                int previous = loadout.stats.damage;

                loadout.stats.damage = value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.damage = previous;
                }
                break;

            case "Recoil":
                int p = loadout.stats.recoil;

                loadout.stats.recoil = value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.recoil = p;
                }
                break;

            case "Reload":
                int pr = loadout.stats.reloadTime;

                loadout.stats.reloadTime = value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.reloadTime = pr;
                }
                break;

            case "DamageFalloff":
                int pre = loadout.stats.damageFalloff;

                loadout.stats.damageFalloff = value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.damageFalloff = pre;
                }
                break;

            case "RateOfFire":
                int prev = loadout.stats.rateOfFire;

                loadout.stats.rateOfFire = value;

                if (Calculator.CalculateCost(loadout.stats) >  PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.damage = prev;
                }
                break;

            case "Spread":
                int previ = loadout.stats.spread;

                loadout.stats.spread = value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.spread = previ;
                }
                break;

            case "ADS":
                int previo = loadout.stats.adsTime;

                loadout.stats.adsTime = value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.adsTime = previo;
                }
                break;

            case "SprintFire":
                int previou = loadout.stats.sprintToFire;

                loadout.stats.sprintToFire = value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.sprintToFire = previou;
                }
                break;

            case "Health":
                int revious = loadout.stats.health;

                loadout.stats.health = value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.health = revious;
                }
                break;

            case "Shields":
                int evious = loadout.stats.shields;

                loadout.stats.shields = value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.shields = evious;
                }
                break;

            case "WalkSpeed":
                int vious = loadout.stats.walkSpeed;

                loadout.stats.walkSpeed = value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.walkSpeed = vious;
                }
                break;

            case "SprintSpeed":
                int ious = loadout.stats.sprintSpeed;

                loadout.stats.sprintSpeed = value;

                if (Calculator.CalculateCost(loadout.stats) > PlayerLoadout.MAX_POINTS)
                {
                    loadout.stats.sprintSpeed = ious;
                }
                break;
        }

        Refresh();
    }

    void Refresh()
    {
        int cost =  Calculator.CalculateCost(loadout.stats);

        int remaining =  PlayerLoadout.MAX_POINTS - cost;

        pointsText.text =   $"Points Remaining: {remaining}";

        classText.text = "Class: " + loadout.className;

        weaponText.text =  $"Weapon: {DetermineWeapon()}";

        confirmButton.interactable = remaining >= 0;

        SummaryText.text = GenerateSummary();
    }

    void ConfirmBuild()
    {
        loadout.BuildLoadout();
        loadout.SaveLoadout();

        Refresh();

        Statsmanager.Instance.BuildProfile(loadout.stats);
    }

    string DetermineWeapon()
    {
        Stats s = loadout.stats;

        if (loadout.className == "Assault")
            return "1) Assault Rifle \n 2) Glock";

        if (loadout.className == "Support")
            return "1) SMG \n 2) P226";

        if (loadout.className == "Heavy")
            return "1) LMG \n 2) Revolver";

        if (loadout.className == "Scout")
            return "1) Shotgun \n 2) Magnum";

        if (loadout.className == "Engineer")
            return "1) DMR \n 2) Machine Pistol";

        if (loadout.className == "Recon")
            return "1) Sniper Rifle \n 2) USP45";

        return "1) Assault Rifle \n 2) Glock";
    }

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
}
