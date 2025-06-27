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

    [Range(0.1f, 5f)] public float replaySpeed = 1.0f;

    [SerializeField] public Color refereeColor;
    [SerializeField] public Color team1Color;
    [SerializeField] public Color team2Color;

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
        BallManager.instance.InitializeBall(startingframe);
        ScoreManager.instance.InitializeScore(startingframe);

        StartCoroutine(ReplaySystem());
        Debug.Log("Replay system initialized.");
    }

    // Using a coroutine to handle the timings of the replay means that I can control the speed of the replay
    private IEnumerator ReplaySystem()
    {
        for (int i = 0; i < frameData.Length; i++)
        {
            yield return new WaitForSeconds(0.01667f / replaySpeed);
            string frame0 = frameData[i];
            JsonFormat startingframe = JsonUtility.FromJson<JsonFormat>(frame0);
            
            BallManager.instance.UpdateBallPositions(startingframe);
            PlayerManager.instance.UpdatePlayerPositions(startingframe);
            ScoreManager.instance.UpdateScore(startingframe);
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
