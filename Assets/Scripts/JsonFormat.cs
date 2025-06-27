using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonFormat
{
    public int FrameCount;
    public int TimestampUTC;
    public List<Person> Persons = new List<Person>();
    public Ball Ball;
    public GameClockContext GameClock;
    public MatchScoreContext MatchScore;
}

[System.Serializable]
public class Person
{
    public int Id;
    public float[] Position;
    public float Speed;
    public int TeamSide;
    public int JerseyNumber;
    public PersonContext PersonContext;
}

[System.Serializable]
public class PersonContext
{
    public float MovementOrientation;
}

[System.Serializable]
public class Ball
{
    public int Id;
    public float[] Position;
    public int Speed;
    public float MovementOrientation;
}

[System.Serializable]
public class MatchScoreContext
{
    public int HomeScore;
    public int AwayScore;
}

[System.Serializable]
public class GameClockContext
{
    public int Period;
    public int Minute;
    public int Second;
    public int InjuryTime;
}
