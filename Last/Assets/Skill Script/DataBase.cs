using UnityEngine;

public class DataBase : MonoBehaviour
{
    public static DataBase Instance;

    public Attachment[] attachments;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Attachment GetAttachment(string id)
    {
        foreach (Attachment attachment in attachments)
        {
            if (attachment.attachmentName == id)
            {
                return attachment;
            }
        }

        Debug.LogWarning("Attachment not found: " + id);
        return null;
    }
}
