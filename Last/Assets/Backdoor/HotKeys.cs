using UnityEngine;
using UnityEngine.InputSystem;

public class Hotkeys : MonoBehaviour
{
    public Inputmanager inputManager;

    //Where the player/participants switches scenes
    void Update()
    {
        //switches to the points stat weapon scene
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameRefresh.RefreshEverything();
            //Scenemanager.Instance.OpenOverlay("Point");
            Overlaycontroller.OpenOverlayScene("Point");
            inputManager.EnableGameplay(false);
        }
        //switches to the perk skill tree attachment scene
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameRefresh.RefreshEverything();
            //Scenemanager.Instance.OpenOverlay("Skill");
            Overlaycontroller.OpenOverlayScene("Skill");
            inputManager.EnableGameplay(false);
        }

        //switches to the attachment on weapons scene 
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameRefresh.RefreshEverything();
            //Scenemanager.Instance.OpenOverlay("Combine");
            Overlaycontroller.OpenOverlayScene("Combine");
            inputManager.EnableGameplay(false);
        }
        //returns to the FPS scene or testing area and updates the major scripts
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

            Health health = UnityEngine.Object.FindAnyObjectByType<Health>();

            if (health != null)
                health.InitializeHealth();

            GameRefresh.RefreshEverything();
        }
    }
}
