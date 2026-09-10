using UnityEngine;

public class Inputmanager : MonoBehaviour
{
    //Locks and makes sure that mouse/cursor state are correct
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
