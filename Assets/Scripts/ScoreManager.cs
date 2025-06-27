using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Microsoft.Unity.VisualStudio.Editor;

public class ScoreManager : MonoBehaviour
{
    // Singleton instance
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI team1score;
    [SerializeField] private TextMeshProUGUI team2score;

    [SerializeField] private GameObject team1color;
    [SerializeField] private GameObject team2color;

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

    public void InitializeScore(JsonFormat jsonData)
    {
        UpdateScore(jsonData);
        team1color.GetComponent<UnityEngine.UI.Image>().color = ReplayManager.instance.team1Color;
        team2color.GetComponent<UnityEngine.UI.Image>().color = ReplayManager.instance.team2Color;
    }

    public void UpdateScore(JsonFormat jsonData)
    {
        team1score.text = jsonData.MatchScore.HomeScore.ToString();
        team2score.text = jsonData.MatchScore.AwayScore.ToString();
    }
}
