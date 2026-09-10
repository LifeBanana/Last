using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    //perk button to display the game object with costs, name, perk data
    public SkillData skill;

    public Button button;

    public TMP_Text title;

    public PlayerTree tree;
    public TMP_Text costText;

    private void Start()
    {
        if (skill == null)
        {
            Debug.LogWarning( "SkillData is missing on SkillButton." );

            return;
        }

        title.text = skill.skillName;

        costText.text =  $"Cost: {skill.pointCost}";

        if (button != null)
        {
            button.onClick.AddListener(SelectSkill);
        }
    }

    public void SelectSkill()
    {
        if (tree == null)
        {
            Debug.LogWarning( "PlayerTree is missing on " + skill.skillName );

            return;
        }

        tree.SelectSkill(skill);
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(SelectSkill);
        }
    }
}
