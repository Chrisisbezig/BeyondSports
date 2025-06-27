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
        foreach (var player in jsonData.Players)
        {
            CreatePlayer(player);
        }
    }

    private void CreatePlayer(Player player)
    {
        float yrotation = 0;
        if (player.MovementOrientation != null)
        {
            yrotation = player.MovementOrientation;
        }

        Quaternion rotation = Quaternion.Euler(0, player.MovementOrientation, 0);
        GameObject playerObject = Instantiate(playerPrefab, player.Position, rotation);

        PlayerInstance playerInstance = new PlayerInstance
        {
            Id = player.Id,
            PlayerObject = playerObject,
            Speed = player.Speed
        };
    }
}

public class PlayerInstance
{
    public int Id;
    public GameObject PlayerObject;
    public float Speed;
}