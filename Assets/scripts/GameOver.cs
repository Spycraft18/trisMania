using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Points_Lines_Level Points {  get; private set; }

    public TMP_InputField inputField;
    public TextMeshProUGUI finalscore;
    // Start is called before the first frame update
    void Start()
    {
        if(inputField != null)
        {
            inputField.onValueChanged.AddListener(ValueChanged);
        }
    }

    void ValueChanged(string text)
    {
        if(text.Length >= 3)
        {
            inputField.caretWidth = 0;
            inputField.caretBlinkRate = 0;
        }
        else
        {
            inputField.caretWidth = 1;
            inputField.caretBlinkRate = 0.85f;
        }
    }
    void OnDestroy()
    {
        if (inputField != null)
        {
            inputField.onValueChanged.RemoveListener(ValueChanged);
        }
    }
    public void GameOverScreen()
    {
        SceneManager.LoadScene("GameOver");
        finalscore.text = Points.Scores.ToString("D6");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
