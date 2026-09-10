using System.Collections.Generic;
using UnityEngine;

public class Attachmentmanager : MonoBehaviour
{
    //fields for weapons prefab
    public Transform muzzleSocket;

    public Transform sightSocket;

    public Transform magazineSocket;

    public Transform underbarrelSocket;

    public Transform stockSocket;

    public Transform sideRailSocket;

    public Socket[] sockets;
    ////Scrapped/Outdated: early versions for the socket system
    Dictionary<AttachmentType, Transform> Socket;

    //void Awake()
    //{
    //    Socket = new Dictionary<AttachmentType, Transform>()
    //{
    //    {AttachmentType.Muzzle,muzzleSocket},
    //    {AttachmentType.Sights,sightSocket},
    //    {AttachmentType.Magazine,magazineSocket},
    //    {AttachmentType.Underbarrel,underbarrelSocket},
    //    {AttachmentType.Stock,stockSocket},
    //    {AttachmentType.SideRail,sideRailSocket}
    //};
    //}
    //equip the attachment to socket
    public void EquipAttachment(Attachment attachment)
    {
        Transform socket = GetSocketTransform(attachment.attachmentType);

        Debug.Log(socket);

        if (socket == null)
        {
            Debug.LogError("Socket missing for " + attachment.attachmentType);

            return;
        }

        foreach (Transform child in socket)
            Destroy(child.gameObject);

        GameObject obj = Instantiate   (   attachment.prefab,socket  );

        GameObject test = Instantiate ( attachment.prefab );

        Debug.Log(test.name);

        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        Debug.Log("Spawned " + attachment.attachmentName);
    }

    //get the specific socket 
    public Socket GetSocket(AttachmentType type)
    {
        foreach (Socket socket in sockets)
        {
            if (socket.socketType == type)
                return socket;
        }

        return null;
    }
    //remove the attachments from socket
    public void RemoveAttachment(AttachmentType type)
    {
        Transform socket = GetSocket(type).transform;

        foreach (Transform child in socket)
            Destroy(child.gameObject);
    }
    //gets the transform for prefab weapon
    public Transform GetSocketTransform(AttachmentType type)
    {
        switch (type)
        {
            case AttachmentType.Muzzle:
                return muzzleSocket;

            case AttachmentType.Sights:
                return sightSocket;

            case AttachmentType.Magazine:
                return magazineSocket;

            case AttachmentType.Underbarrel:
                return underbarrelSocket;

            case AttachmentType.Stock:
                return stockSocket;

            case AttachmentType.SideRail:
                return sideRailSocket;
        }

        return null;
    }
}
