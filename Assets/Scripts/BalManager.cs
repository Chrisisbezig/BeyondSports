using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    // Normally I would create a Objectmanager where the player and ball managers would inherit from, but for simplicity and time I wont (maybe later)
    // I would use override methods

    // Singleton instance
    public static BallManager instance;
    private BallInstance ballInstance;

    [SerializeField] private GameObject ballPrefab;

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

    public void InitializeBall(JsonFormat jsonData)
    {
        CreateBall(jsonData.Ball);
    }

    private void CreateBall(Ball ball)
    {
        Vector3 position = new Vector3(ball.Position[0], ball.Position[1], ball.Position[2]);
        GameObject ballobject = Instantiate(ballPrefab, position, Quaternion.identity);

        BallInstance ballinstance = new BallInstance
        {
            Id = ball.Id,
            BallObject = ballobject,
            Speed = ball.Speed
        };
        ballInstance = ballinstance;
    }

    public void UpdateBallPositions(JsonFormat jsonData)
    {
        Vector3 newPosition = new Vector3(jsonData.Ball.Position[0], jsonData.Ball.Position[1], jsonData.Ball.Position[2]);
        ballInstance.BallObject.transform.position = newPosition;
    }
}

public class BallInstance
{
    public int Id;
    public GameObject BallObject;
    public float Speed;
}
