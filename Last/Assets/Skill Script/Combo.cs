using System.Collections.Generic;
using UnityEngine;
//scriptable objects for creating flexible combo for attachments using perks
[CreateAssetMenu(menuName = "Skill Tree/Combo")]
public class SkillCombo : ScriptableObject
{
    public List<SkillData> requiredSkills;

    public Attachment reward;
}
