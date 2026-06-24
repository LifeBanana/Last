using System.Linq;
using UnityEngine;

public static class Unlock
{
    public static bool HasCombo(  PlayerTree tree, SkillCombo combo)
    {
        return combo.requiredSkills.All(skill => tree.unlockedSkills.Contains(skill));
    }
}