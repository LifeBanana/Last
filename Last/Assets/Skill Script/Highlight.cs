using System.Net.Sockets;
using UnityEngine;
//Scrapped/Outdated: early versions where sockets are highlighed
public class Highlight : MonoBehaviour
{
    public static Highlight Instance;

    public GameObject[] highlights;

    void Awake()
    {
        Instance = this;
    }

    public void ShowHighlights(Socket[] sockets)
    {
        foreach (Socket socket in sockets)
        {
            socket.highlight.SetActive(true);
        }
    }

    public void HideHighlights(Socket[] sockets)
    {
        foreach (Socket socket in sockets)
        {
            socket.highlight.SetActive(false);
        }
    }
}