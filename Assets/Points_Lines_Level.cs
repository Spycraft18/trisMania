using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Points_Lines_Level : MonoBehaviour
{
    public Board board;
    public Piece piece {  get; private set; }
    public TextMeshProUGUI ClearedLines;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Score;
    public int Levels = 1;
    public int Difficulty;
    public int ScoreMultiplier = 100;
    public int Scores = 0;
    public int oldLine;
    // Start is called before the first frame update
    void Start()
    {
        oldLine = board.LinesCleared;
    }

    // Update is called once per frame
    void Update()
    {
        int LinesCleared = board.LinesCleared;
        int LinesToBeCleared = Difficulty;
        ClearedLines.text = LinesCleared.ToString("D4");
        if (LinesCleared >= LinesToBeCleared)
        {
            LinesToBeCleared += Difficulty;
            Levels++;
            piece.speed *= 0.75f;

        } 
        if (oldLine != LinesCleared)
        {
            Scores += Levels * ScoreMultiplier;
            Score.text = Scores.ToString("D6");
            oldLine = LinesCleared;
        }
                Level.text = Levels.ToString("D2");

    }
}
