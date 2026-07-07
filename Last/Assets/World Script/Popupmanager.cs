using UnityEngine;

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
