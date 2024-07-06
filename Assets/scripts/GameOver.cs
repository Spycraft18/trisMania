using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Points_Lines_Level points { get; private set; }
    public int ActiveScene = 0;
    public TMP_InputField inputField;
    public TextMeshProUGUI finalscore;
    // Start is called before the first frame update

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
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            points = FindObjectOfType<Points_Lines_Level>();
            ActiveScene = 1;
        }
        if (inputField != null)
        {
            inputField.onValueChanged.AddListener(ValueChanged);
        }
    }

    private void Update()
    {

        if (ActiveScene == 1)
        {
            ActiveScene = 0; // Resetea la bandera para evitar repetición
            Debug.Log("finalmente");
            if (points != null)
            {
                finalscore.text = points.LastScores.ToString("D6");
            }
            else
            {
                Debug.LogError("Points_Lines_Level no encontrado.");
            }
        }
    }
    void OnDestroy()
    {
        if (inputField != null)
        {
            inputField.onValueChanged.RemoveListener(ValueChanged);
        }
    }
}   
