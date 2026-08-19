using UnityEngine;

public class Overlaycanvas : MonoBehaviour
{
    public enum CanvasType
    {
        Match,
        Point,
        Skill,
        Combine
    }

    [Header("Canvas")]
    public CanvasType canvasType;

    public string canvasName;

    private void Awake()
    {
        if (string.IsNullOrEmpty(canvasName))
        {
            canvasName = canvasType.ToString();
        }

        Overlaycontroller.RegisterCanvas(this);
    }

    private void OnDestroy()
    {
        Overlaycontroller.UnregisterCanvas(this);
    }

    public void Open()
    {
        Overlaycontroller.OpenCanvas(this);
    }

    public void Close()
    {
        if (canvasType == CanvasType.Match)
            return;

        if (Overlaycontroller.GetCurrentCanvas() == this)
        {
            Overlaycontroller.ReturnToMatch();
        }
        else
        {
            SetCanvasActive(false);
        }
    }

    public void SetCanvasActive(bool active)
    {
        if (gameObject.activeSelf == active)
            return;

        gameObject.SetActive(active);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}