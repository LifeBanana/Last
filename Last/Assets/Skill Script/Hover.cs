using UnityEngine;
using UnityEngine.EventSystems;
//Scrapped/Outdated: early versions for displaying information to the player on perks
public class SkillHover : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler
{
    public SkillData skill;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (InfoPanel.Instance != null)
            InfoPanel.Instance.Show(skill);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (InfoPanel.Instance != null)
            InfoPanel.Instance.Hide();
    }
}