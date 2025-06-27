using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Singleton instance
    public static PlayerManager instance;
    private List<PlayerInstance> playerInstances = new List<PlayerInstance>();

    [SerializeField] private GameObject playerPrefab;

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

    public void InitializePlayers(JsonFormat jsonData)
    {
        foreach (var person in jsonData.Persons)
        {
            CreatePlayer(person);
        }
    }

    private void CreatePlayer(Persons persons)
    {
        float yrotation = 0;
        if (persons.MovementOrientation != null)
        {
            yrotation = persons.MovementOrientation;
        }

        Quaternion rotation = Quaternion.Euler(0, persons.MovementOrientation, 0);
        GameObject playerObject = Instantiate(playerPrefab, persons.Position, rotation);

        PlayerInstance playerInstance = new PlayerInstance
        {
            Id = persons.Id,
            PlayerObject = playerObject,
            Speed = persons.Speed
        };
        playerInstances.Add(playerInstance);
    }
}

public class PlayerInstance
{
    public int Id;
    public GameObject PlayerObject;
    public float Speed;
}