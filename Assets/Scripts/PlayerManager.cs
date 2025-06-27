using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Singleton instance
    public static PlayerManager instance;
    private List<PlayerInstance> playerInstances = new List<PlayerInstance>();

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Material[] TeamColors;

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
        TeamColors[0].color = ReplayManager.instance.refereeColor;
        TeamColors[1].color = ReplayManager.instance.team1Color;
        TeamColors[2].color = ReplayManager.instance.team2Color;

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
        GameObject playerobject = Instantiate(playerPrefab, position, rotation);

        PlayerCustomizations(person, playerobject);

        PlayerInstance playerinstance = new PlayerInstance
        {
            Id = person.Id,
            PlayerObject = playerobject,
            Speed = person.Speed
        };
        playerInstances.Add(playerinstance);
    }

    private void PlayerCustomizations(Person person, GameObject personObject)
    {
        personObject.name = $"Player_{person.Id}";
        personObject.GetComponent<MeshRenderer>().material = TeamColors[person.TeamSide];
        personObject.GetComponentInChildren<TextMeshProUGUI>().text = person.JerseyNumber.ToString();
    }

    public void UpdatePlayerPositions(JsonFormat jsonData)
    {
        foreach (var person in jsonData.Persons)
        {
            PlayerInstance player = GetPlayerById(person.Id);
            if (player != null)
            {
                Vector3 newposition = new Vector3(person.Position[0], person.Position[1], person.Position[2]);
                player.PlayerObject.transform.position = newposition;
                // Update rotation if needed
                if (person.PersonContext.MovementOrientation != null)
                {
                    Quaternion newrotation = Quaternion.Euler(0, person.PersonContext.MovementOrientation, 0);
                    player.PlayerObject.transform.rotation = newrotation;
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