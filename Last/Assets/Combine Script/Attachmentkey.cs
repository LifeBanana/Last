using UnityEngine;

public class Attachmentkey : MonoBehaviour
{
    //where the panel for each type of attachments in which the player press 1 to 6
    void Update()
    {
        if (Previewmanager.Instance == null)
            return;

        Attachmentmanager manager = Previewmanager.Instance.CurrentAttachmentManager;

        if (manager == null)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Attachmentmenu.Instance.Open(manager, AttachmentType.Muzzle);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Attachmentmenu.Instance.Open(manager, AttachmentType.Sights);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Attachmentmenu.Instance.Open(manager, AttachmentType.Magazine);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Attachmentmenu.Instance.Open(manager, AttachmentType.Underbarrel);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Attachmentmenu.Instance.Open(manager, AttachmentType.Stock);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Attachmentmenu.Instance.Open(manager, AttachmentType.SideRail);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Attachmentmenu.Instance.Close();
        }
    }
}
