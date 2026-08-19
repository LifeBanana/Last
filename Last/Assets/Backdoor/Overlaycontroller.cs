using UnityEngine;
using UnityEngine.SceneManagement;

public class Overlaycontroller : MonoBehaviour
{
    public static Overlaycontroller Instance;
    private static Overlaycanvas currentCanvas;
    private static Overlaycanvas matchCanvas;
    private static string currentOverlayScene = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
    }

    public static void RegisterCanvas(Overlaycanvas canvas)
    {
        if (canvas == null)
            return;

        if (canvas.canvasType == Overlaycanvas.CanvasType.Match)
        {
            matchCanvas = canvas;

            if (string.IsNullOrEmpty(currentOverlayScene))
            {
                SetOnlyCanvasActive(matchCanvas);
                currentCanvas = matchCanvas;
            }
            else
            {
                canvas.SetCanvasActive(false);
            }

            return;
        }

        canvas.SetCanvasActive(false);
    }

    public static void UnregisterCanvas(Overlaycanvas canvas)
    {
        if (canvas == null)
            return;

        if (currentCanvas == canvas)
        {
            currentCanvas = null;
        }

        if (matchCanvas == canvas)
        {
            matchCanvas = null;
        }
    }

    public static void OpenOverlayScene(string sceneName)
    {
        if (Instance == null)
        {
            return;
        }

        Overlaycanvas.CanvasType requestedType;

        if (!TryGetCanvasType(sceneName, out requestedType))
        {
            Debug.LogError(  "No CanvasType exists for overlay scene: " +  sceneName  );
            return;
        }

        Scene existingScene = SceneManager.GetSceneByName(sceneName);

        if (existingScene.isLoaded)
        {
            SelectCanvasFromScene(existingScene, requestedType);
            return;
        }

        CloseCurrentOverlay();

        currentOverlayScene = sceneName;

        if (matchCanvas != null)
        {
            matchCanvas.SetCanvasActive(false);
        }

        SceneManager.LoadSceneAsync( sceneName, LoadSceneMode.Additive );
    }

    private static void OnSceneLoaded( Scene scene, LoadSceneMode mode)
    {
        if (string.IsNullOrEmpty(currentOverlayScene))
            return;

        if (scene.name != currentOverlayScene)
            return;

        Overlaycanvas.CanvasType type;

        if (!TryGetCanvasType(scene.name, out type))
            return;

        SelectCanvasFromScene(scene, type);
    }

    private static void SelectCanvasFromScene( Scene scene,  Overlaycanvas.CanvasType requestedType)
    {
        Overlaycanvas[] canvases = Object.FindObjectsByType<Overlaycanvas>( FindObjectsInactive.Include, FindObjectsSortMode.None );

        Overlaycanvas foundCanvas = null;

        foreach (Overlaycanvas canvas in canvases)
        {
            if (canvas == null)
                continue;

            if (canvas.gameObject.scene != scene)
                continue;

            if (canvas.canvasType != requestedType)
                continue;

            foundCanvas = canvas;
            break;
        }

        if (foundCanvas == null)
        {
            return;
        }

        OpenCanvas(foundCanvas);
    }

    public static void OpenCanvas(Overlaycanvas canvas)
    {
        if (canvas == null)
        {
            return;
        }

        currentCanvas = canvas;

        SetOnlyCanvasActive(canvas);
    }

    private static void SetOnlyCanvasActive( Overlaycanvas canvasToActivate)
    {
        if (canvasToActivate == null)
            return;

        Overlaycanvas[] allCanvases = Object.FindObjectsByType<Overlaycanvas>( FindObjectsInactive.Include, FindObjectsSortMode.None );

        foreach (Overlaycanvas canvas in allCanvases)
        {
            if (canvas == null)
                continue;

            if (canvas == canvasToActivate)
                continue;

            canvas.SetCanvasActive(false);
        }

        canvasToActivate.SetCanvasActive(true);
    }

    public static void ReturnToMatch()
    {
        Debug.Log("Returning to Match.");

        string overlayToUnload = currentOverlayScene;

        currentOverlayScene = "";

        currentCanvas = null;

        if (matchCanvas != null)
        {
            currentCanvas = matchCanvas;

            SetOnlyCanvasActive(matchCanvas);
        }

        if (!string.IsNullOrEmpty(overlayToUnload))
        {
            Scene scene = SceneManager.GetSceneByName(overlayToUnload);

            if (scene.isLoaded)
            {
                SceneManager.UnloadSceneAsync( overlayToUnload );
            }
        }
    }

    private static void CloseCurrentOverlay()
    {
        if (string.IsNullOrEmpty(currentOverlayScene))
            return;

        string sceneName = currentOverlayScene;

        currentOverlayScene = "";

        Scene scene = SceneManager.GetSceneByName(sceneName);

        if (scene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }

        currentCanvas = null;
    }

    private static void OnSceneUnloaded(Scene scene)
    {
        Debug.Log( "Scene unloaded: " +  scene.name );
    }

    private static bool TryGetCanvasType( string sceneName,  out Overlaycanvas.CanvasType type)
    {
        switch (sceneName)
        {
            case "Point":

                type = Overlaycanvas.CanvasType.Point;
                return true;

            case "Skill":

                type = Overlaycanvas.CanvasType.Skill;
                return true;

            case "Combine":

                type = Overlaycanvas.CanvasType.Combine;
                return true;

            case "Match":

                type = Overlaycanvas.CanvasType.Match;
                return true;
        }

        type = Overlaycanvas.CanvasType.Match;
        return false;
    }

    public static Overlaycanvas GetCurrentCanvas()
    {
        return currentCanvas;
    }

    public static Overlaycanvas GetMatchCanvas()
    {
        return matchCanvas;
    }

    public static bool IsOverlayOpen()
    {
        return !string.IsNullOrEmpty(currentOverlayScene);
    }

    public static string GetCurrentOverlayScene()
    {
        return currentOverlayScene;
    }
}