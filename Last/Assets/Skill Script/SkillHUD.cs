using TMPro;
using UnityEngine;

public class SkillHUD : MonoBehaviour
{
    public PlayerTree tree;

    public TMP_Text pointsText;

    public TMP_Text comboText;

    public TMP_Text unlockedSkillsText;

    void Update()
    {
        pointsText.text = "Skill Points : " + tree.availablePoints;

        RefreshUnlockedSkills();
    }

    void RefreshUnlockedSkills()
    {
        unlockedSkillsText.text = "";

        foreach (SkillData skill in tree.unlockedSkills)
        {
            unlockedSkillsText.text +=   "• " + skill.skillName + "\n";
        }
    }

    public void UpdateCombo(SkillCombo combo, PlayerTree tree)
    {
        int unlocked = 0;

        foreach (SkillData skill in combo.requiredSkills)
        {
            if (tree.unlockedSkills.Contains(skill))
                unlocked++;
        }

        comboText.text = unlocked + "/" +   combo.requiredSkills.Count +  " Complete";
    }
}