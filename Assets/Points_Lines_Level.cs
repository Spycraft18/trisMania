using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Points_Lines_Level : MonoBehaviour
{
    public Board board;
    public TextMeshProUGUI ClearedLines;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Score;
    public int Levels = 1;
    public int Difficulty;
    public int ScoreMultiplier = 100;
    public int Scores = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int LinesCleared = board.LinesCleared;
        ClearedLines.text = LinesCleared.ToString("D4");
        if(LinesCleared % Difficulty != 0)
        {
            Levels++;
        }
        Level.text = Levels.ToString("D2");
        Scores += Levels * LinesCleared * ScoreMultiplier;
        Score.text = Scores.ToString("D6");
    }
}
