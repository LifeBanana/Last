using UnityEngine;
//Scrapped/Outdated: early versions for testing if player can be damaged
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
