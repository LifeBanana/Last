using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Overlaycontroller : MonoBehaviour
{
    public static Overlaycontroller Instance;

    private static Overlaycanvas currentCanvas;
    private static Overlaycanvas matchCanvas;

    private static readonly List<Overlaycanvas> registeredCanvases = new List<Overlaycanvas>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void RegisterCanvas(Overlaycanvas canvas)
    {
        if (canvas == null)
            return;

        if (!registeredCanvases.Contains(canvas))
            registeredCanvases.Add(canvas);

        if (canvas.canvasType == Overlaycanvas.CanvasType.Match)
        {
            matchCanvas = canvas;

            if (currentCanvas == null)
            {
                currentCanvas = matchCanvas;
                SetOnlyCanvasActive(matchCanvas);
            }
            else
            {
                canvas.SetCanvasActive(false);
            }
        }
        else
        {
            canvas.SetCanvasActive(false);
        }
    }

    public static void UnregisterCanvas(Overlaycanvas canvas)
    {
        if (canvas == null)
            return;

        registeredCanvases.Remove(canvas);

        if (currentCanvas == canvas)
            currentCanvas = null;

        if (matchCanvas == canvas)
            matchCanvas = null;
    }

    public static void OpenOverlayScene(string sceneName)
    {
        Debug.Log("Opening overlay scene: " + sceneName);

        Scene scene = SceneManager.GetSceneByName(sceneName);

        if (!scene.isLoaded)
        {
            SceneManager.LoadScene(  sceneName, LoadSceneMode.Additive );

            return;
        }

        SelectCanvasFromScene(scene);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeSceneCallback()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SelectCanvasFromScene(scene);
    }

    private static void SelectCanvasFromScene(Scene scene)
    {
        Overlaycanvas[] canvases = Object.FindObjectsByType<Overlaycanvas>(  FindObjectsInactive.Include, FindObjectsSortMode.None );

        Overlaycanvas foundCanvas = null;

        foreach (Overlaycanvas canvas in canvases)
        {
            if (canvas == null)
                continue;

            if (canvas.gameObject.scene != scene)
                continue;

            if (canvas.canvasType == Overlaycanvas.CanvasType.Match)
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

    public static void ReturnToMatch()
    {

        if (matchCanvas == null)
        {
            currentCanvas = null;
            return;
        }

        currentCanvas = matchCanvas;

        SetOnlyCanvasActive(matchCanvas);
    }

    private static void SetOnlyCanvasActive(
        Overlaycanvas canvasToActivate)
    {
        if (canvasToActivate == null)
            return;

        for (int i = registeredCanvases.Count - 1; i >= 0; i--)
        {
            Overlaycanvas canvas = registeredCanvases[i];

            if (canvas == null)
            {
                registeredCanvases.RemoveAt(i);
                continue;
            }

            canvas.SetCanvasActive(false);
        }

        canvasToActivate.SetCanvasActive(true);
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
        return currentCanvas != null && currentCanvas.canvasType != Overlaycanvas.CanvasType.Match;
    }

    public static void DebugCanvases()
    {
        foreach (Overlaycanvas canvas in registeredCanvases)
        {
            if (canvas == null)
                continue;

            Debug.Log(canvas.canvasName + " " + canvas.canvasType + "  " + canvas.gameObject.scene.name +  " " + canvas.gameObject.activeSelf );
        }
    }
}