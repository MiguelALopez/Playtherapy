using UnityEngine;
using System.Collections;
using System;

public class GameSession
{
    private string date;
    private int score;
    private int repetitions;
    private int time;
    private string level;
    private string minigame_id;

    public GameSession(string minigame_id)
    {
        date = DateTime.Now.ToString("yyyy-MM-dd");
        this.minigame_id = minigame_id;
		Debug.Log ("fecha: " + date);
    }

    public string Date
    {
        get
        {
            return date;
        }

        set
        {
            date = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public int Repetitions
    {
        get
        {
            return repetitions;
        }

        set
        {
            repetitions = value;
        }
    }

    public int Time
    {
        get
        {
            return time;
        }

        set
        {
            time = value;
        }
    }

    public string Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    public string Minigame_id
    {
        get
        {
            return minigame_id;
        }

        set
        {
            minigame_id = value;
        }
    }
}
