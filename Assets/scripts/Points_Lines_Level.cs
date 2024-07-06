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
    public SaveSystem saveSystem {  get; private set; }
    public TextMeshProUGUI ClearedLines;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Score;
    public int Levels = 1;
    public int Difficulty;
    public float speed = 0.75f;
    public int LinesToBeCleared;
    public int ScoreMultiplier = 100;
    public int Scores = 0;
    public int LastScores;
    public int oldLine;
    // Start is called before the first frame update
    void Start()
    {
        oldLine = board.LinesCleared;
        piece = FindObjectOfType<Piece>();
        //LevelIncreased = false;
        LinesToBeCleared = Difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        int LinesCleared = board.LinesCleared;
        ClearedLines.text = LinesCleared.ToString("D4");
        if (LinesCleared >= LinesToBeCleared)
        {
            Levels++;
            LinesToBeCleared += Difficulty;
            piece.stepdelay *= speed;
            piece.lockdelay *= speed;

        } 
        if (oldLine != LinesCleared)
        {
            Scores += Levels * ScoreMultiplier;
            Score.text = Scores.ToString("D6");
            //saveSystem.SaveData();
            oldLine = LinesCleared;
            //LevelIncreased = false;
        }
                Level.text = Levels.ToString("D2");

    }
    public void SaveScoresBeforeGameOver()
    {
        LastScores = Scores;
        Debug.Log("A ver" + Scores);
    }
}
