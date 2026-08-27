using System.Collections.Generic;
using UnityEngine;

public class PlayerTree : MonoBehaviour
{
    public int availablePoints = 30;

    public List<SkillData> unlockedSkills = new List<SkillData>();

    public SkillData[] allSkills;

    public SkillData selectedSkill;

    void Start()
    {
        LoadSkills();
    }

    public void SelectSkill(SkillData skill)
    {
        if (skill == null)
            return;

        selectedSkill = skill;

        Debug.Log( "Selected Skill: " + skill.skillName );
    }


    public void BuySelectedSkill()
    {
        if (selectedSkill == null)
        {
            Debug.LogWarning(
                "No skill has been selected."
            );

            return;
        }

        UnlockSkill(selectedSkill);
    }

    public bool UnlockSkill(SkillData skill)
    {
        if (unlockedSkills.Contains(skill))
            return false;

        foreach (var req in skill.prerequisites)
        {
            if (!unlockedSkills.Contains(req))
                return false;
        }

        if (availablePoints < skill.pointCost)
            return false;

        availablePoints -= skill.pointCost;

        unlockedSkills.Add(skill);
        SaveSkills();
        Statsmanager.Instance.RefreshProfile();
        return true;
    }

    public void SaveSkills()
    {
        SaveData save = SaveManager.Instance.Data;

        save.skillPoints = availablePoints;

        save.unlockedSkills.Clear();

        foreach (var skill in unlockedSkills)
        {
            save.unlockedSkills.Add(skill.skillID);
        }

        SaveManager.Instance.SaveGame();
    }

    public void LoadSkills()
    {
        SaveData save = SaveManager.Instance.Data;

        availablePoints = save.skillPoints;

        unlockedSkills.Clear();

        foreach (string id in save.unlockedSkills)
        {
            foreach (var skill in allSkills)
            {
                if (skill.skillID == id)
                {
                    unlockedSkills.Add(skill);
                    break;
                }
            }
        }
    }
}
