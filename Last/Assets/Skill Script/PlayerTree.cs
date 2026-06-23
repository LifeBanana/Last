using System.Collections.Generic;
using UnityEngine;

public class PlayerTree : MonoBehaviour
{
    public int availablePoints = 10;

    public List<SkillData> unlockedSkills = new List<SkillData>();

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

        return true;
    }
}
