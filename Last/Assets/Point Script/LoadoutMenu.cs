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

            case "Health":
                loadout.stats.health = value;
                break;

            case "RateOfFire":
                loadout.stats.rateOfFire = value;
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
        var s = loadout.stats;

        if (s.damage > 7 && s.rateOfFire < 0)
            return "Battle Rifle";

        if (s.rateOfFire > 7 && s.damage < 0)
            return "SMG";

        if (s.health > 7)
            return "LMG";

        if (s.spread > 7)
            return "Shotgun";

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

        return summary;
    }
}
