using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    PlayerController playerController;
   
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        playerController.RestartGame();
    }

    public void ResumeGame()
    {
        playerController.ResumeGame();
    }

    public void MainMenu()
    {
        playerController.MainMenu();
    }
}
