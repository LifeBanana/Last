using UnityEngine;

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
