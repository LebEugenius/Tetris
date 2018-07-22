using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scores : MonoBehaviour {

    public Text[] highScoreStrings;
    public int[] highScores;
    int scorePlace = 0;
    void Start()
    {
        highScores = new int[10];
        LoadScores();
        UpdateBoard();
    }

    public void UpdateScores(int score)
    {
        for(int i = highScores.Length - 1; i >= 0; i--)
        {
            if(score > highScores[i])
            {
                scorePlace = i + 1;
            }
        }
        Debug.Log("Place: " + scorePlace);
        if (scorePlace <= 10)    
        {
            for (int i = highScores.Length - 1; i > scorePlace - 1; i--)
            {
                highScores[i] = highScores[i - 1];
            }

            highScores[scorePlace - 1] = score;
        }
        UpdateBoard();
        SaveScores();
    }

    public void UpdateBoard()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            if (highScores[i] != 0)
                highScoreStrings[i].text = (i + 1) + ". " + highScores[i].ToString();
            else
                highScoreStrings[i].text = string.Empty;
        }
    }

    public void SaveScores()
    {
        for(int i = 0; i < highScores.Length; i++)
        {
            PlayerPrefs.SetInt("Place " + (i + 1), highScores[i]);
        }
    }

    public void LoadScores()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i] = PlayerPrefs.GetInt("Place " + (i + 1), highScores[i]);
        }
    }
}
