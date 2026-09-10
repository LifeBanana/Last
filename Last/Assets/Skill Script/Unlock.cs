using System.Linq;
using UnityEngine;
//process to unlcok attachment when the right perks are bought
public static class Unlock
{
    public static bool HasCombo(  PlayerTree tree, SkillCombo combo)
    {
        return combo.requiredSkills.All(skill => tree.unlockedSkills.Contains(skill));
    }
}