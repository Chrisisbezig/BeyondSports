using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonFormat
{
    public int FrameCount;
    public int TimestampUTC;
    public Persons[] Persons;
    public Ball Ball;
    public GameClockContext GameClock;
    public MatchScoreContext MatchScore;
}

public class Persons
{
    public int Id;
    public Vector3 Position;
    public float Speed;
    public float MovementOrientation;
    public int TeamSide;
    public int JerseyNumber;
}

public class Ball
{
    public Vector3 Position;
    public int Speed;
    public float MovementOrientation;
}

public class MatchScoreContext
{
    public int HomeScore;
    public int AwayScore;
}

public class GameClockContext
{
    public int Period;
    public int Minute;
    public int Second;
    public int InjuryTime;
}
