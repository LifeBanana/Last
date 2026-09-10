using UnityEngine;
//controls the pop up in the world scene and the damage from weapon at object
public class Popupmanager : MonoBehaviour
{
    public static Popupmanager Instance;

    public PopUp popupPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowPopup(Vector3 position, float damage)
    {
        PopUp popup = Instantiate(popupPrefab, position + Vector3.up, Quaternion.identity);
        popup.Setup(damage);
    }
}
