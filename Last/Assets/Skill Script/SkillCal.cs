using UnityEngine;

public static class SkillCal
{
    public static void ApplySkills(  PlayerTree tree, Profile player)
    {
        foreach (var skill in tree.unlockedSkills)
        {
            foreach (var mod in skill.modifiers)
            {
                switch (mod.effectType)
                {
                    case SkillEffect.Health:
                        player.health += mod.amount;
                        break;

                    case SkillEffect.Shield:
                        player.shields += mod.amount;
                        break;

                    case SkillEffect.ReloadSpeed:
                        player.reloadTime *=    1f - (mod.amount / 100f);
                        break;

                    case SkillEffect.FireRate:
                        player.fireRate *=    1f + (mod.amount / 100f);
                        break;

                    case SkillEffect.Recoil:
                        player.recoil *=   1f - (mod.amount / 100f);
                        break;

                    case SkillEffect.SprintSpeed:
                        player.sprintSpeed *= 1f + (mod.amount / 100f);
                        break;
                }
            }
        }
    }
}