using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void StartLevel()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1; // Resumes gameplay after level restart or next level loads
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Loads the next scene in the build index
        Time.timeScale = 1; // Resumes gameplay after level restart or next level loads
    }
}
