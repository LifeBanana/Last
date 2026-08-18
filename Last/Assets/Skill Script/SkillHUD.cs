using TMPro;
using UnityEngine;

public class SkillHUD : MonoBehaviour
{
    public PlayerTree tree;

    public SkillCombo[] combos;

    public TMP_Text pointsText;
    public TMP_Text comboText;
    public TMP_Text unlockedSkillsText;

    private void Start()
    {
        pointsText.text = "Skill Points : " + tree.availablePoints;
    }

    void Update()
    {
        pointsText.text = "Skill Points : " + tree.availablePoints;

        RefreshUnlockedSkills();

        RefreshCombos();
    }

    void RefreshUnlockedSkills()
    {
        unlockedSkillsText.text = "";

        foreach (SkillData skill in tree.unlockedSkills)
        {
            unlockedSkillsText.text += "# " + skill.skillName + "\n";
        }

        if (tree.unlockedSkills.Count == 0)
            unlockedSkillsText.text = "None";
    }

    void RefreshCombos()
    {
        comboText.text = "";

        foreach (SkillCombo combo in combos)
        {
            int unlocked = 0;

            foreach (SkillData skill in combo.requiredSkills)
            {
                if (tree.unlockedSkills.Contains(skill))
                    unlocked++;
            }

            comboText.text += combo.name +   " : " +  unlocked +   "/" +   combo.requiredSkills.Count;

            if (Unlock.HasCombo(tree, combo))
                comboText.text += " X";

            comboText.text += "\n";
        }
    }
}