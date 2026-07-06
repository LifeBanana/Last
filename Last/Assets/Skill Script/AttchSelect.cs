using UnityEngine;

public class AttchSelect : MonoBehaviour
{
    public Socket socket;

    public Attachmentmanager weapon;

    public void Select()
    {
        Attchcamera.Instance.FocusSocket(socket);

        AttchUI.instance.Open(weapon);
    }
}
