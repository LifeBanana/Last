using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    //save the data to be accessed in different across scenes and loaded
    public static SaveManager Instance;

    public SaveData Data = new SaveData();

    string savePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            savePath = Path.Combine( Application.persistentDataPath,  "SaveFile.json");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(Data, true);

        File.WriteAllText(savePath, json);

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
            return;

        string json = File.ReadAllText(savePath);

        Data = JsonUtility.FromJson<SaveData>(json);

        Debug.Log("Game Loaded");
    }
}
