using UnityEngine;

//scrapped/outdated:  Early Ideas for attachment that didn't pan out left here as a reminder but this was used for camera and slots
public class Attachmentinput : MonoBehaviour
{
    public Camera clickCamera;

    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = clickCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Hit : " + hit.collider.name);

            AttchSelect select = hit.collider.GetComponent<AttchSelect>();

            if (select != null)
            {
                Debug.Log("Socket clicked!");
                select.Select();
            }

            AttchSelect selects = hit.collider.GetComponentInChildren<AttchSelect>();

            if (selects != null)
            {
                Debug.Log("Socket detected!");
                selects.Select();
            }
        }
        else
        {
            Debug.Log("Nothing hit.");
        }
    }
}