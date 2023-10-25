using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.mute)
        {
            oppeningAudio.Pause();
        }
        else
        {
            if(!oppeningAudio.isPlaying)
                oppeningAudio.Play();
        }
    }

    public void MuteSound()
    {
        GameManager.Instance.MuteSound();
    }
    }
