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

    private void CreatePlayer(Person person)
    {
        float yrotation = 0;
        if (person.PersonContext.MovementOrientation != null)
        {
            yrotation = person.PersonContext.MovementOrientation;
        }

        Quaternion rotation = Quaternion.Euler(0, person.PersonContext.MovementOrientation, 0);
        Vector3 position = new Vector3(person.Position[0], person.Position[1], person.Position[2]);
        GameObject playerObject = Instantiate(playerPrefab, position, rotation);

        PlayerInstance playerInstance = new PlayerInstance
        {
            Id = person.Id,
            PlayerObject = playerObject,
            Speed = person.Speed
        };
        playerInstances.Add(playerInstance);
    }

    public void UpdatePlayerPositions(JsonFormat jsonData)
    {
        foreach (var person in jsonData.Persons)
        {
            PlayerInstance player = GetPlayerById(person.Id);
            if (player != null)
            {
                Vector3 newPosition = new Vector3(person.Position[0], person.Position[1], person.Position[2]);
                player.PlayerObject.transform.position = newPosition;
                // Update rotation if needed
                if (person.PersonContext.MovementOrientation != null)
                {
                    Quaternion newRotation = Quaternion.Euler(0, person.PersonContext.MovementOrientation, 0);
                    player.PlayerObject.transform.rotation = newRotation;
                }
            }
            else
            {
                // Create player if doesnt exist, incase of substitution or whatever
                CreatePlayer(person);
            }
        }
    }

    // To easily find a player by ID
    public PlayerInstance GetPlayerById(int id)
    {
        return playerInstances.Find(player => player.Id == id);
    }
}

public class PlayerInstance
{
    public int Id;
    public GameObject PlayerObject;
    public float Speed;
}