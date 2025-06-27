using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    // Singleton instance
    public static ReplayManager instance;
    private static TextAsset jsonFile;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Loading the JSON file from Resources folder
        jsonFile = Resources.Load<TextAsset>("jsonfile(2)");
        Debug.Log($"JSON file loaded: {jsonFile.name}");

        InitializeReplaySystem();
    }

    // Initialize the replay system
    // Used https://docs.unity3d.com/ScriptReference/JsonUtility.FromJson.html
    private void InitializeReplaySystem()
    {
        string frame0 = GetJsonFrame(0);
        JsonFormat startingframe = JsonUtility.FromJson<JsonFormat>(frame0);



        Debug.Log("Replay system initialized.");
    }

    private string GetJsonFrame(int frameindex)
    {

        return "";
    }
}
