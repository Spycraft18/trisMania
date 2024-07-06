using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveSystem : MonoBehaviour
{

    public TMP_InputField inputField;
    public TextMeshProUGUI TopScore;
    public void SaveData()
    {
        PlayerPrefs.SetString("Nombre", inputField.text);
        PlayerPrefs.SetString("TOP", TopScore.text);
    }

    public void LoadData()
    {
        inputField.text = PlayerPrefs.GetString("Nombre");
        TopScore.text = PlayerPrefs.GetString("TOP");
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
}
