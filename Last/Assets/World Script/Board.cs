using UnityEngine;
//Scrapped/Outdated: early versions the other scrapped/abandoned/outdated scripts and system for camera
public class Board : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        transform.forward = cam.transform.forward;
    }
}
