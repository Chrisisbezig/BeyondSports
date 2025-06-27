using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ReplayManager : MonoBehaviour
{
    // Singleton instance
    public static ReplayManager instance;
    private static TextAsset jsonFile;
    private static string[] frameData;

    [SerializeField] private float replaySpeedFps = 1.0f;

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
        // Split the Json into frames
        CalculateJsonFrames();

        string frame0 = frameData[0];
        JsonFormat startingframe = JsonUtility.FromJson<JsonFormat>(frame0);

        PlayerManager.instance.InitializePlayers(startingframe);

        StartCoroutine(ReplaySystem());
        Debug.Log("Replay system initialized.");
    }

    private IEnumerator ReplaySystem()
    {
        for (int i = 0; i < frameData.Length; i++)
        {
            yield return new WaitForSeconds(1 / replaySpeedFps);
            string frame0 = frameData[i];
            JsonFormat startingframe = JsonUtility.FromJson<JsonFormat>(frame0);
            

            PlayerManager.instance.UpdatePlayerPositions(startingframe);
        }
    }

    // Gets the JSON frame data for a specific frame index

    // String information
    // https://learn.microsoft.com/en-us/dotnet/api/system.string?view=net-9.0&redirectedfrom=MSDN
    // https://learn.microsoft.com/en-us/dotnet/csharp/how-to/parse-strings-using-split
    private void CalculateJsonFrames()
    {
        frameData = jsonFile.text.Split(new char[] { '\t', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
    }
}
