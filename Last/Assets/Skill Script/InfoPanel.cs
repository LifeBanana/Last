using TMPro;
using UnityEngine;
//where the information on perks are shown to the player
public class InfoPanel : MonoBehaviour
{
    public static InfoPanel Instance;

    public TMP_Text nameText;
    public TMP_Text descText;
    public TMP_Text costText;
    public TMP_Text hintText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Hide();
    }

    public void Show(SkillData skill)
    {
        gameObject.SetActive(true);

        nameText.text = skill.skillName;
        descText.text = skill.description;
        costText.text = "Cost : " + skill.pointCost;
        hintText.text = skill.buildHint;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}