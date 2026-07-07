using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Popupmanager.Instance.ShowPopup( transform.position + Vector3.up,  25);
        }
    }
}
