using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Load Practise Scene
    public void Practise ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Back to Main Menu 
    public void GoBack ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Exit TheSmartCubeTrainer
    public void Exit ()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
