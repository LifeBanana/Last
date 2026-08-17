using UnityEngine;
using UnityEngine.InputSystem;

public class Hotkeys : MonoBehaviour
{
    public Inputmanager inputManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameRefresh.RefreshEverything();
            //Scenemanager.Instance.OpenOverlay("Point");
            Overlaycontroller.OpenOverlayScene("Point");
            inputManager.EnableGameplay(false);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            GameRefresh.RefreshEverything();
            //Scenemanager.Instance.OpenOverlay("Skill");
            Overlaycontroller.OpenOverlayScene("Skill");
            inputManager.EnableGameplay(false);
        }


        if (Input.GetKeyDown(KeyCode.J))
        {
            GameRefresh.RefreshEverything();
            //Scenemanager.Instance.OpenOverlay("Combine");
            Overlaycontroller.OpenOverlayScene("Combine");
            inputManager.EnableGameplay(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Scenemanager.Instance.CloseOverlay("Point");
            Scenemanager.Instance.CloseOverlay("Skill");
            Scenemanager.Instance.CloseOverlay("Combine");

            Overlaycontroller.ReturnToMatch();

            inputManager.EnableGameplay(true);

            Statsmanager.Instance.RefreshProfile();

            if (Previewmanager.Instance != null)
            {
                Previewmanager.Instance.RefreshPreview();
            }

            Loadoutmanager manager = FindFirstObjectByType<Loadoutmanager>();

            if (manager != null)
                manager.Initialize();

            GameRefresh.RefreshEverything();
        }
    }
}
