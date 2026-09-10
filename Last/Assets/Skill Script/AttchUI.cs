using UnityEngine;
//Scrapped/Outdated: early versions for attachment ui for skill tree
public class AttchUI : MonoBehaviour
{
    public GameObject attachmentPanel;

    Attachmentmanager manager;
    public static AttchUI instance;

    private void Awake()
    {
        instance = this;
    }

    public void Open(Attachmentmanager weapon)
    {
        manager = weapon;

        attachmentPanel.SetActive(true);

        Highlight.Instance.ShowHighlights( manager.sockets);

        Attchcamera.Instance.ResetView();
    }

    public void Close()
    {
        attachmentPanel.SetActive(false);

        Highlight.Instance.HideHighlights(manager.sockets);

        Attchcamera.Instance.ResetView();
    }
}
