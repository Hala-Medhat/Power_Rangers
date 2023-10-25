using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    TMP_Text final_score;
    PlayerController playerController;
    public AudioSource oppeningAudio;


    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.mute)
        {
            oppeningAudio.Pause();
        }
        else
        {
            oppeningAudio.Play();
        }
            SetFinalScore();

    }

    // Update is called once per frame
    void Update()
    {

     
    }

    public void SetFinalScore()
    {
        Debug.Log("hh");
        int score = GameManager.Instance.GetScore();
        final_score.SetText("Final Score: " + score);

    }
}
