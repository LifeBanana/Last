using UnityEngine;

public class Attachmentmanager : MonoBehaviour
{
    public Transform muzzleSocket;

    public Transform sightSocket;

    public Transform magazineSocket;

    public Transform underbarrelSocket;

    public Transform stockSocket;

    public Transform sideRailSocket;

    public Socket[] sockets;

    public void EquipAttachment(Attachment attachment)
    {
        Transform socket = null;

        switch (attachment.attachmentType)
        {
            case AttachmentType.Muzzle:
                socket = muzzleSocket;
                break;

            case AttachmentType.Sights:
                socket = sightSocket;
                break;

            case AttachmentType.Magazine:
                socket = magazineSocket;
                break;

            case AttachmentType.Underbarrel:
                socket = underbarrelSocket;
                break;

            case AttachmentType.Stock:
                socket = stockSocket;
                break;

            case AttachmentType.SideRail:
                socket = sideRailSocket;
                break;
        }

        if (socket == null)
            return;

        foreach (Transform child in socket)
            Destroy(child.gameObject);

        Instantiate( attachment.prefab, socket, false);
    }


    public Socket GetSocket(AttachmentType type)
    {
        foreach (Socket socket in sockets)
        {
            if (socket.socketType == type)
                return socket;
        }

        return null;
    }
}
