using UnityEngine;
//scrapped/outdated: early versions for selecting socket and attachment
public class AttchSelect : MonoBehaviour
{
    public Socket socket;

    public Attachmentmanager weapon;

    public void Select()
    {
        Attchcamera.Instance.FocusSocket(socket);

        Attachmentmenu.Instance.Open(weapon, socket.socketType);
    }
}
