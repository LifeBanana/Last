using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Tree/Combo")]
public class SkillCombo : ScriptableObject
{
    public List<SkillData> requiredSkills;

    public Attachment reward;
}
