using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public new GameObject[] gameObject;
    public static Pause instance;
    public bool movement = true;

    private void Start()
    {
        foreach (GameObject @object in gameObject)
        {

            @object.SetActive(false); 
        
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            movement = false;
            PauseMenu();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            movement = true;
            ResumeGame();
        }
        
    }

    public void PauseMenu()
    {
        foreach (GameObject obj in gameObject)
        {
            obj.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        foreach(GameObject obj in gameObject)
        {
            obj.SetActive(false);
        }
        Time.timeScale = 1;
        movement = true;
    }
}
