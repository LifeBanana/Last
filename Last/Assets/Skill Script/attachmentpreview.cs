using UnityEngine;

//scrapped/outdatted: use for the early version fo the skill tree attachment previewing the attachment
public class attachmentpreview : MonoBehaviour
{
    public static attachmentpreview Instance;

    public Transform previewSpawn;

    GameObject currentAttachment;

    void Awake()
    {
        Instance = this;
    }

    public void PreviewAttachment(Attachment attachment)
    {
        if (currentAttachment != null)
            Destroy(currentAttachment);

        currentAttachment = Instantiate( attachment.prefab, previewSpawn);

        currentAttachment.transform.localPosition = Vector3.zero;
        currentAttachment.transform.localRotation = Quaternion.identity;
    }

    public void ClearPreview()
    {
        if (currentAttachment != null)
            Destroy(currentAttachment);
    }
}