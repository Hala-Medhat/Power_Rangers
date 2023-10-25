using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Create a static instance of the GameManager
    public static GameManager Instance;
    PlayerController playerController;
    public bool mute = false;
    public int score = 0;

    private void Awake()
    {
        // Ensure there is only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();

    }



    public int GetScore()
    {
        return score;
    }

    public void MuteSound()
    {
        mute = !mute;
        Debug.Log("mute");
    }

    

}
