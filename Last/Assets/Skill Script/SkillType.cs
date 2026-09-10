using UnityEngine;
//the stats/value amount for perk and //Scrapped/abandoned: early versions of what type of perk it would be
public enum SkillType
{
    Passive,
    Active,
    EquipmentUnlock
}

public enum SkillEffect
{
    Health,
    Shield,
    WalkSpeed,
    SprintSpeed,
    ReloadSpeed,
    FireRate,
    Damage,
    Recoil,
    Spread,
    ADS,
    Range,
    SprintToFire,
    CrouchSpeed,
    JumpHeight
}

[System.Serializable]
public class StatModifier
{
    public SkillEffect effectType;

    public float amount;
}
