using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//scrapped/outdated: images on the inventory can be dragged across screen to weapon socket
public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;

    Canvas canvas;

    CanvasGroup canvasGroup;

    RectTransform rect;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        rect = GetComponent<RectTransform>();

        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;

        transform.SetParent(canvas.transform);

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (eventData.pointerEnter == null)
        {
            ReturnHome();
            return;
        }

        Slot slot = eventData.pointerEnter.GetComponentInParent<Slot>();

        if (slot == null)
        {
            ReturnHome();
            return;
        }

        transform.SetParent(slot.transform);

        rect.anchoredPosition = Vector2.zero;
    }

    void ReturnHome()
    {
        transform.SetParent(originalParent);

        rect.anchoredPosition = Vector2.zero;
    }
}