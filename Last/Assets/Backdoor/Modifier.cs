using UnityEngine;

public static class Modifier
{
    public static void Apply(Profile profile, StatModifier modifier)
    {
        switch (modifier.effectType)
        {
            case SkillEffect.Damage:

                profile.damage += modifier.amount;
                break;

            case SkillEffect.FireRate:

                profile.fireRate *= 1f + modifier.amount / 10f;
                break;

            case SkillEffect.Recoil:

                profile.recoil *= 1f - modifier.amount / 10f;
                break;

            case SkillEffect.ReloadSpeed:

                profile.reloadTime *= 1f - modifier.amount / 10f;
                break;

            case SkillEffect.Range:

                profile.range += modifier.amount;
                break;

            case SkillEffect.Spread:

                profile.spread *= 1f - modifier.amount / 10f;
                break;

            case SkillEffect.ADS:

                profile.adsTime *= 1f - modifier.amount / 10f;
                break;

            case SkillEffect.SprintToFire:

                profile.sprintToFire *= 1f - modifier.amount / 10f;
                break;

            case SkillEffect.Health:

                profile.health += modifier.amount;
                break;

            case SkillEffect.Shield:

                profile.shields += modifier.amount;
                break;

            case SkillEffect.WalkSpeed:

                profile.walkSpeed += modifier.amount;
                break;

            case SkillEffect.SprintSpeed:

                profile.sprintSpeed += modifier.amount;
                break;

            case SkillEffect.CrouchSpeed:

                profile.crouchSpeed += modifier.amount;
                break;

            case SkillEffect.JumpHeight:

                profile.jumpHeight += modifier.amount;
                break;
        }
    }
}