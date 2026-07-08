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
                loadout.stats.damage = value;
                break;

            case "Recoil":
                loadout.stats.recoil = value;
                break;

            case "Reload":
                loadout.stats.reloadTime = value;
                break;

            case "DamageFalloff":
                loadout.stats.damageFalloff = value;
                break;

            case "RateOfFire":
                loadout.stats.rateOfFire = value;
                break;

            case "Spread":
                loadout.stats.spread = value;
                break;

            case "ADS":
                loadout.stats.adsTime = value;
                break;

            case "SprintFire":
                loadout.stats.sprintToFire = value;
                break;

            case "Health":
                loadout.stats.health = value;
                break;

            case "Shields":
                loadout.stats.shields = value;
                break;

            case "WalkSpeed":
                loadout.stats.walkSpeed = value;
                break;

            case "SprintSpeed":
                loadout.stats.sprintSpeed = value;
                break;
        }

        Refresh();
    }

    void Refresh()
    {
        int cost =  Calculator.CalculateCost(loadout.stats);

        int remaining =  PlayerLoadout.MAX_POINTS - cost;

        pointsText.text =   $"Points Remaining: {remaining}";

        string generatedClass =   ClassGenerator.GetClass(loadout.stats);

        classText.text =  $"Class: {generatedClass}";

        weaponText.text =  $"Weapon: {DetermineWeapon()}";

        confirmButton.interactable = remaining >= 0;

        SummaryText.text = GenerateSummary();
    }

    void ConfirmBuild()
    {
        loadout.BuildLoadout();
        loadout.SaveLoadout();
    }

    string DetermineWeapon()
    {
        Stats s = loadout.stats;

        if (s.damage >= 7 && s.damageFalloff >= 5)
            return "Battle Rifle";

        if (s.rateOfFire >= 7 && s.walkSpeed >= 4)
            return "SMG";

        if (s.health >= 6 && s.shields >= 6)
            return "LMG";

        if (s.spread >= 7 && s.reloadTime >= 5)
            return "Shotgun";

        if (s.damageFalloff >= 8 &&  s.adsTime >= 6)
            return "DMR";

        if (s.damageFalloff >= 9 && s.adsTime >= 8)
            return "Sniper Rifle";

        return "Assault Rifle";
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
