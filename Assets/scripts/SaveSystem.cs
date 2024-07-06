using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveSystem : MonoBehaviour
{

    public TextMeshProUGUI TopScore;
    public void SaveData()
    {
        PlayerPrefs.SetString("TOP", TopScore.text);
    }

    public void LoadData()
    {
        TopScore.text = PlayerPrefs.GetString("TOP");
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
}
