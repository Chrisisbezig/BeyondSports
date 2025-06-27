using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

        Debug.Log($"Frame={startingframe.FrameCount}, Time={startingframe.TimestampUTC}"); // WORKS
        Debug.Log($"Players={startingframe.Players.Count}"); // Doesnt work

        PlayerManager.instance.InitializePlayers(startingframe);

        Debug.Log("Replay system initialized.");
    }

    // Gets the JSON frame data for a specific frame index

    // String information
    // https://learn.microsoft.com/en-us/dotnet/api/system.string?view=net-9.0&redirectedfrom=MSDN
    // https://learn.microsoft.com/en-us/dotnet/csharp/how-to/parse-strings-using-split
    private string GetJsonFrame(int frameindex)
    {
        string[] frameData = jsonFile.text.Split(new char[] { ' ' , '\t', '\n' }, System.StringSplitOptions.None);
        Debug.Log($"Data: {frameData[0]}");
        return frameData[0];
    }
}
