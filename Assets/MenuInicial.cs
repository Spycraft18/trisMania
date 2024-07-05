 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void Guia()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); 
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu Inicial");
    }
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir...");
    }

}
