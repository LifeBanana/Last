using UnityEngine;
using UnityEngine.InputSystem;

public class Hotkeys : MonoBehaviour
{
    public Inputmanager inputManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Scenemanager.Instance.OpenOverlay("Point");
            inputManager.EnableGameplay(false);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Scenemanager.Instance.OpenOverlay("Skill");
            inputManager.EnableGameplay(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Scenemanager.Instance.CloseOverlay("Point");
            Scenemanager.Instance.CloseOverlay("Skill");

            inputManager.EnableGameplay(true);
        }
    }
}
