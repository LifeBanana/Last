using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttachSlot : MonoBehaviour
{
    public Image icon;

    public TMP_Text attachmentName;

    Attachment attachment;

    public void Setup(Attachment data)
    {
        attachment = data;

        attachmentName.text = data.attachmentName;

        icon.sprite = data.prefab.GetComponentInChildren<SpriteRenderer>()?.sprite;
    }

    public void Click()
    {
        EquipPrimary();
    }

    public Attachment GetAttachment()
    {
        if (attachment == null)
            Debug.LogError("Attachment has not been assigned to this slot.");

        return attachment;
    }

    public void EquipPrimary()
    {
        if (Previewmanager.Instance == null)
        {
            Debug.LogError("PreviewManager missing.");
            return;
        }

        if (Previewmanager.Instance.CurrentPrimary == null)
        {
            Debug.LogError("No primary weapon has been spawned.");
            return;
        }

        Attachmentmanager manager = Previewmanager.Instance.CurrentPrimary.GetComponent<Attachmentmanager>();

        if (manager == null)
        {
            Debug.LogError("Primary weapon has no Attachmentmanager.");
            return;
        }

        Debug.Log("Attachment Manager: " + manager);

        Debug.Log("Attachment: " + attachment);

        manager.EquipAttachment(GetAttachment());
    }
}
