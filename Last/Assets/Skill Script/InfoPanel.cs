using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text descText;
    public TMP_Text costText;
    public TMP_Text hintText;

    public void Show(SkillData skill)
    {
        nameText.text = skill.skillName;

        descText.text = skill.description;

        costText.text =  $"Cost: {skill.pointCost}";

        hintText.text =  skill.buildHint;
    }
}
