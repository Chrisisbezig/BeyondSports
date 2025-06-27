using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonFormat
{
    public int FrameCount;
    public int TimestampUTC;
    public List<Player> Players = new List<Player>();

    public GameClockContext GameClock;
    public MatchScoreContext MatchScore;
}

public class Player
{
    public int Id;
    public Vector3 Position;
    public int Speed;
    public float MovementOrientation;
    public int TeamSide;
    public int JerseyNumber;
}

public class MatchScoreContext
{
    public int HomeScore;
    public int AwayScore;
}

public class GameClockContext
{
    private int Period;
    private int Minute;
    private int Second;
    private int InjuryTime;
}
