using TMPro;
using UnityEngine;

public class SkillTreeHUD : MonoBehaviour
{
    public PlayerTree tree;

    public TMP_Text PointsText;

    public TMP_Text comboText;

    void Update()
    {
        PointsText.text = $"Skill Points: {tree.availablePoints}";
    }


    public void UpdateCombo( SkillCombo combo, PlayerTree tree)
    {
        int unlocked = 0;

        foreach (var skill in combo.requiredSkills)
        {
            if (tree.unlockedSkills.Contains(skill))
                unlocked++;
        }

        comboText.text = $"{unlocked}/{combo.requiredSkills.Count} Complete";
    }
}