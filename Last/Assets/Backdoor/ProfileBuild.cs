using UnityEngine;

public static class ProfileBuild
{
    //where the stats for weapons, player is applied and called to build weapon for class
    public static Profile Build()
    {
        SaveData save = SaveManager.Instance.Data;

        Stats stats = new Stats();

        stats.damage = save.damage;
        stats.recoil = save.recoil;
        stats.reloadTime = save.reload;
        stats.damageFalloff = save.damageFalloff;
        stats.rateOfFire = save.fireRate;
        stats.spread = save.spread;
        stats.adsTime = save.ads;
        stats.sprintToFire = save.sprintFire;

        stats.health = save.health;
        stats.shields = save.shields;
        stats.walkSpeed = save.walkSpeed;
        stats.sprintSpeed = save.sprintSpeed;

        Profile profile = WeaponBuild.Build(stats);

        ApplySkills(profile);

        ApplyAttachments(profile);

        return profile;
    }
    //where the perks for player and weapon are applied 
   public static void ApplySkills(Profile profile)
    {
        PlayerTree tree = Object.FindFirstObjectByType<PlayerTree>();

        if (tree == null)
            return;

        foreach (SkillData skill in tree.unlockedSkills)
        {
            foreach (StatModifier mod in skill.modifiers)
            {
                Modifier.Apply(profile, mod);
            }
        }
    }
    //where the attachments and modifications are applied to weapon
   public static void ApplyAttachments(Profile profile)
    {
        foreach (EquippedAttachment equipped in SaveManager.Instance.Data.equippedAttachments)
        {
            Attachment attachment = DataBase.Instance.GetAttachment(equipped.attachmentID);

            if (attachment == null)
                continue;

            foreach (StatModifier mod in attachment.modifiers)
            {
                Modifier.Apply(profile, mod);
            }
        }
    }
}