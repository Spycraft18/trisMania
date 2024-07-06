 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuInicial : MonoBehaviour
{
    public Pause pause {  get; private set; }
    public void Jugar()
    {
        SceneManager.LoadScene("Tetris");
    }
    
    public void Guia()
    {
        SceneManager.LoadScene("Guia"); 
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu Inicial");
        Time.timeScale = 1;
    }
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir...");
    }

}
