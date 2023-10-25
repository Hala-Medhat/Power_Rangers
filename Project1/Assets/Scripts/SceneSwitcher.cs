using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class SceneSwitcher : MonoBehaviour
{
   


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void ResumeGameButton()
    {
        // This function is called when the button is clicked.
        LoadScene("Level1");

    }

    public void RestartGameButton()
    {
        // This function is called when the button is clicked.
        LoadScene("Level1");

    }

    public void MainMenuButton()
    {
        // This function is called when the button is clicked.
        LoadScene("MainMenu");

    }

    public void GameOverButton()
    {
        // This function is called when the button is clicked.
        LoadScene("GameOver");

    }

    public void QuitGame()
    {
        Debug.Log("gena");

        Application.Quit();
    }

   
}
