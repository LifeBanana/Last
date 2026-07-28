using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public SkillData skill;

    public Button button;

    public TMP_Text title;

    public PlayerTree tree;
    public TMP_Text costText;

    private void Start()
    {
        title.text = skill.skillName;

        costText.text = $"\nCost: {skill.pointCost}";

        button.onClick.AddListener(BuySkill);
    }

    public void BuySkill()
    {
        tree.UnlockSkill(skill);
    }
}
