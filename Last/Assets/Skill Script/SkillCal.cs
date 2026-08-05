using UnityEngine;

public static class SkillCal
{
    public static void ApplySkills(Profile player)
    {
        PlayerTree tree = Object.FindFirstObjectByType<PlayerTree>();

        if (tree == null)
            return;

        foreach (SkillData skill in tree.unlockedSkills)
        {
            foreach (StatModifier mod in skill.modifiers)
            {
                Modifier.Apply(player, mod);
            }
        }
    }
}