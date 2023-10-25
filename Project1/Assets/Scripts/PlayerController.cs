using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    Rigidbody rb;

    [SerializeField]
    float forwardSpeed = 20.0f;


    Transform t;
    private int upVector = 6;
    private int lane = 1; // 1 middle , 0 left, 2 right
    int laneSlideDistance = 20;




    int upSpeed =2;
    [SerializeField]
    int slideSpeed = 4;

    public Color redColor;
    public Color greenColor;
    public Color blueColor;
    public Color whiteColor;
    public Color blueColorShield;

    Color playerColor;

    private Renderer playerRenderer;
    [SerializeField]

    TMP_Text energy_score;

    [SerializeField]
    TMP_Text red_score;

    [SerializeField]
    TMP_Text green_score;

    [SerializeField]
    TMP_Text blue_score;

    public int score = 0;
    int redScore = 0; 
    int greenScore = 0; 
    int blueScore = 0;

    public AudioSource orbCollectAudio;
    public AudioSource errorAudio;
    public AudioSource powerUpAudio;
    public AudioSource changeFormAudio;
    public AudioSource collideAudio;
    public AudioSource oppeningAudio;
    public AudioSource backgroundAudio;

    ObjectsPooling objectsPooling;
    GroundSpawner growndSpawner;
    SceneSwitcher sceneSwitcher;


    bool greenPowerActive = false;
    bool redPowerActive = false;
    bool bluePowerActive = false;

    public GameObject shield;
    public GameObject greenPowerup;

    public GameObject pause;

    bool paused = false;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
        playerRenderer = GetComponent<Renderer>();
        objectsPooling = GameObject.FindObjectOfType<ObjectsPooling>();
        growndSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        sceneSwitcher = GameObject.FindObjectOfType<SceneSwitcher>();
        if (GameManager.Instance.mute)
        {
            backgroundAudio.Pause();
            
        }
        else
        {
            backgroundAudio.Play();
        }
        Time.timeScale = 1;
        






    }

    // Update is called once per frame
    void Update()
    {


        MoveUsingKeyBoard();
        ChangeLanes();
        ChangeForm();
        ActivatePower();
        PauseGame();
        ForwardMovement();
        IncreaseScore();
        GameManager.Instance.score = score;





    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Time.timeScale = 0;
                pause.SetActive(true);
                paused = true;
                if (!GameManager.Instance.mute)
                { 
                    backgroundAudio.Pause();
                    oppeningAudio.Play();
            }

            }
            else
            {
                pause.SetActive(false);

                Time.timeScale = 1;
                paused = false;
                if (!GameManager.Instance.mute)
                {
                    backgroundAudio.Play();
                    oppeningAudio.Pause();
                }
            }
        }
    }

    void ForwardMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
        /*        rb.AddForce(Vector3.forward * Time.fixedDeltaTime * forwardSpeed);
        */
    }

    void MoveUsingKeyBoard()
    {
        if (Input.GetKeyDown("right") || Input.GetKeyDown(KeyCode.A))
        {
            lane++;
            if (lane == 3)
            {
                lane = 2;
            }
            
        }

        else if (Input.GetKeyDown("left") || Input.GetKeyDown(KeyCode.D))
        {
            lane--;
            if (lane == -1)
            {
                lane = 0;
            }
        }
        else if (Input.GetKeyDown("up"))
        {
            Vector3 newPosition = transform.position;
            newPosition.y = upVector * upSpeed;
            transform.position = newPosition;
        }


    }

    void ChangeForm ()
    {
        if (playerRenderer != null)
        {
            // Get the color of the player's material
            playerColor = playerRenderer.material.color;
        }

        string playerColorText = GetColor(playerColor);
        if (Input.GetKeyDown(KeyCode.J) && playerColorText != "red" )
        {
            if (redScore == 5)
            {
                if (playerRenderer != null)
                {
                    // Change the material color to the newColor
                    playerRenderer.material.color = redColor;
                    if (!GameManager.Instance.mute)
                    {
                        changeFormAudio.Play();
                    }
                    redScore--;
                    red_score.SetText("Red Energy: " + redScore);
                    bluePowerActive = false;
                    redPowerActive = false;
                    greenPowerActive = false;

                }
            }
            else

            {
                if (!GameManager.Instance.mute)
                {
                    errorAudio.Play();
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.L) && playerColorText != "green")
        {
            if (greenScore == 5)
            {
                if (playerRenderer != null)
                {
                    // Change the material color to the newColor
                    playerRenderer.material.color = greenColor;
                    if (!GameManager.Instance.mute)
                    {
                        changeFormAudio.Play();
                    }
                    greenScore--;
                    green_score.SetText("Green Energy: " + greenScore);
                    bluePowerActive = false;
                    redPowerActive = false;
                    greenPowerActive = false;

                }
            }
            else
            {
                if (!GameManager.Instance.mute)
                {
                    errorAudio.Play();
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.K) && playerColorText != "blue")
        {
            if (blueScore == 5)
            {
                if (playerRenderer != null)
                {
                    // Change the material color to the newColor
                    playerRenderer.material.color = blueColor;
                    if (!GameManager.Instance.mute)
                    {
                        changeFormAudio.Play();
                    }
                    blueScore--;
                    blue_score.SetText("Blue Energy: " + blueScore);
                    bluePowerActive = false;
                    redPowerActive = false;
                    greenPowerActive = false;

                }
            }
            else
            {
                if (!GameManager.Instance.mute)
                {
                    errorAudio.Play();
                }
            }

        }

    }

    void ActivatePower()
    {
          if (Input.GetKeyDown("space"))
        {

            playerColor = playerRenderer.material.color;
            string playerColorText = GetColor(playerColor);
            if (playerColorText=="red" & redPowerActive == false)
            {
                if(redScore < 5 && redScore>0)
                {
                    redScore--;
                    red_score.SetText("Red Energy: " + redScore);
                    redPowerActive = true;
                    if (!GameManager.Instance.mute)
                    {
                        powerUpAudio.Play();
                    }
                    objectsPooling.ReturnObtsaclesToPool();
                }
                if(redScore==0)
                {
                    redPowerActive = false;
                    playerRenderer.material.color = whiteColor;
                }
            }

            

            else if (playerColorText == "blue" && bluePowerActive==false)
            {
                if (blueScore < 5 && blueScore>0)
                {
                    bluePowerActive = true;
                    blueScore--;
                    blue_score.SetText("Blue Energy: " + blueScore);
                    if (!GameManager.Instance.mute)
                    {
                        powerUpAudio.Play();
                    }
                    shield.SetActive(true);


                }
              
            }

            else if (playerColorText == "green" && greenPowerActive==false)
            {
                if (greenScore < 5 && greenScore>0)
                {
                    greenPowerActive = true;
                    greenScore--;
                    green_score.SetText("Green Energy: " + greenScore);
                    if (!GameManager.Instance.mute)
                    {
                        powerUpAudio.Play();
                    }
                    greenPowerup.SetActive(true);


                }
               
            }
            else
            {
                if (!GameManager.Instance.mute)
                {
                    errorAudio.Play();
                }
            }
        }
    }

    public void resetPowers()
    {
        if((greenPowerActive==true && greenScore == 0) ||
            (bluePowerActive == true && blueScore == 0) ||
            (redPowerActive == true && redScore == 0))
        {
            greenPowerActive = false;
            redPowerActive = false;
            bluePowerActive = false;
            playerRenderer.material.color = whiteColor;
         

        }
        shield.SetActive(false);
        greenPowerup.SetActive(false);

    }

    void ChangeLanes()
    {

        Vector3 playerPosition = t.position.z * t.forward + t.position.y * t.up;
        if (lane == 0)
        {
            playerPosition += Vector3.left * laneSlideDistance;
        }
        else if (lane == 2)
        {
            playerPosition += Vector3.right * laneSlideDistance;
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, slideSpeed * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (playerRenderer != null)
        {
            // Get the color of the player's material
            playerColor = playerRenderer.material.color;
        }

        string playerColorText = GetColor(playerColor);
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!GameManager.Instance.mute)
            {
                collideAudio.Play();
            }

            if (bluePowerActive == true)
            {
                bluePowerActive = false;
               
                resetPowers();
                

                objectsPooling.ReturnObjectToPool(collision.gameObject);

            }
            else if(playerColorText=="red" || playerColorText == "green" || playerColorText=="blue")
            {
                playerRenderer.material.color = whiteColor;
                objectsPooling.ReturnObjectToPool(collision.gameObject);
            }
           else {
                sceneSwitcher.LoadScene("GameOver");

            }

           

/*            Destroy(collision.gameObject);
*/        }

       

    }


    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("OrbRed"))
        {
            objectsPooling.ReturnObjectToPool(other.gameObject);

/*            Destroy(other.gameObject);
*/            AdjustScore("red");
            if (!GameManager.Instance.mute)
            {
                orbCollectAudio.Play();
            }
           
            /* if (playerRenderer != null)
             {
                 // Change the material color to the newColor
                 playerRenderer.material.color = redColor;
             }
 */
        }

       else if (other.gameObject.CompareTag("OrbGreen"))
        {
            objectsPooling.ReturnObjectToPool(other.gameObject);

/*            Destroy(other.gameObject);
*/            AdjustScore("green");
            if (!GameManager.Instance.mute)
            {
                orbCollectAudio.Play();
            }



        }

        else if (other.gameObject.CompareTag("OrbBlue"))
        {
            objectsPooling.ReturnObjectToPool(other.gameObject);
            AdjustScore("blue");
            if (!GameManager.Instance.mute)
            {
                orbCollectAudio.Play();
            }
/*            Destroy(other.gameObject);
*/


        }
    }

    public void IncreaseScore()
    {
       if(Input.GetKeyDown(KeyCode.I))
        {
            AdjustScore("red");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            AdjustScore("green");

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            AdjustScore("blue");
        }

    }

    public void AdjustScore (string color)
        
    {
       
        if (playerRenderer != null)
        {
            // Get the color of the player's material
            playerColor = playerRenderer.material.color;
        }

        string playerColorText = GetColor(playerColor);

        
        if (playerColorText!=color && color == "red")
        {
            if (redScore < 5)
            {
                if ( greenPowerActive == true)
                {
                    redScore+=2;
                    if (redScore > 5)
                    {
                        redScore = 5;
                    }

                
                    
                   
                }
                else
                {
                    redScore++;
                }
                red_score.SetText("Red Energy: " + redScore);



            }

        }
        else if (playerColorText != color && color == "blue")
        {
            if (blueScore < 5)
            {
                if ( greenPowerActive == true)
                {
                    blueScore+=2;
                    if (blueScore > 5)
                    {
                        blueScore = 5;
                    }
              

                }
                else
                {
                    blueScore++;
                }
                blue_score.SetText("Blue Energy: " + blueScore);

            }

        }
        else if (playerColorText != color && color == "green")
        {
            if (greenScore < 5)
            {
                if (greenPowerActive == false)
                {

                    greenScore++;
                    green_score.SetText("Green Energy: " + greenScore);
                }
                
            }

        }


        if (playerColorText == color && greenPowerActive==false )
        {
            score+=2;
        }
        else if(playerColorText == "green" && greenPowerActive==true)
        {
            resetPowers();
            if (color == "green") {
               

                score += 10;
              }
            else
            {

                score += 5;
            }
            resetPowers();
            greenPowerActive = false;
        }
        else
        {
            score++;
        }
        energy_score.SetText("Score: " + score);




    }

    public string GetColor (Color playerColor)
    {
        if (playerColor.r == 1.0f && playerColor.b == 0.0f && playerColor.g == 0.0f)
            return "red";
        else if (playerColor.r == 0.0f && playerColor.b == 1.0f && playerColor.g == 0.0f)
            return "blue";
        if (playerColor.r == 0.0f && playerColor.b == 0.0f && playerColor.g == 1.0f)
            return "green";

        return "white";

    }
    
    public float getZPosition()
    {
        return transform.position.z;
    }

    public void InstantiateGame()
    {
         greenPowerActive = false;
         redPowerActive = false;
         bluePowerActive = false;
        int score = 0;
        int redScore = 0;
        int greenScore = 0;
        int blueScore = 0;
        growndSpawner.initialPosition = -25.0f;
        growndSpawner.numberOfPlanes = 0;

        growndSpawner.activeObject = new List<GameObject>();
        objectsPooling.ReturnAllObjectsToPool();
    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;
        pause.SetActive(false);
    }
    public void MainMenu()
    {
        paused = false;
        Time.timeScale = 1;
        pause.SetActive(false);
        sceneSwitcher.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        paused = false;
        Time.timeScale = 1;
        pause.SetActive(false);
        sceneSwitcher.LoadScene("Level1");
    }


}
