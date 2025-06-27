using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    // Singleton instance
    public static ReplayManager instance;

    private TextAsset jsonFile;

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
    private void InitializeReplaySystem()
    {
        
        Debug.Log("Replay system initialized.");
    }
}
