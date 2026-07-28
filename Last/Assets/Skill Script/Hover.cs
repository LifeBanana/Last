using UnityEngine;
using UnityEngine.EventSystems;

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