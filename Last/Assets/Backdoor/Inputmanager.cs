using UnityEngine;

public class Inputmanager : MonoBehaviour
{
    public Controller movement;

    public Mouse mouseLook;

    public Weapon weapon;

    public void EnableGameplay(bool enabled)
    {
        movement.enabled = enabled;
        mouseLook.enabled = enabled;
        weapon.enabled = enabled;

        Cursor.visible = !enabled;

        Cursor.lockState = enabled ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
