using UnityEngine;
using UnityEngine.UI;

public class attachmentbutton : MonoBehaviour
{
    public Attachment attachment;

    public Button previewButton;
    public Button clearButton;

    private void Start()
    {
        if (previewButton != null)
            previewButton.onClick.AddListener(Preview);

        if (clearButton != null)
            clearButton.onClick.AddListener(Clear);
    }

    public void Preview()
    {
        if (attachment == null)
        {
            Debug.LogWarning("No Attachment assigned.");
            return;
        }

        if (attachment.prefab == null)
        {
            Debug.LogWarning("Attachment prefab missing.");
            return;
        }

        attachmentpreview.Instance.PreviewAttachment(attachment);
    }

    public void Clear()
    {
        attachmentpreview.Instance.ClearPreview();
    }
}