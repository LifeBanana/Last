using UnityEngine;
using System.Collections.Generic;
//scriptable object to create the perks to be used in the tool
[CreateAssetMenu(menuName = "Skill Tree/Skill")]
public class SkillData : ScriptableObject
{
    public string skillID;

    public string skillName;

    [TextArea]
    public string description;
    public string buildHint;

    public SkillType skillType;

    public int pointCost = 1;

    public List<SkillData> prerequisites;

    public List<StatModifier> modifiers;
}

