using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPlane;

    ObjectsPooling objectsPooling; // Attach the obstacle object pool
    public float initialPosition = -25.0f;
    int[] objectToGenerate = new int[3];
    Vector3 nextSpawnPoint;
    private float zSpawn;
    public int numberOfPlanes = 0;
    PlayerController playerController;


    public List<GameObject> activeObject = new List<GameObject>();

    float orbX = 0.02f;
    float orbY = 2.63f;
    float orbZ = 0.0f;
    float obstacleX = 1.63f;
    float obstacelY = 0.84f;
    float obstacleZ = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        objectsPooling = GameObject.FindObjectOfType<ObjectsPooling>();
        playerController = GameObject.FindObjectOfType<PlayerController>();


        if (groundPlane != null)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnTile();
            }
        }
        if (objectsPooling.GetObjectFromPool("OrbRed") != null)
        {
            for (int i = 0; i < 3; i++)
            {
                ActivateObjects();
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
       

       /* if (playerController.getZPosition() % 100.0f <= 0.0f && playerController.getZPosition()>10.0f)
        {
        *//*    Debug.Log(playerController.getZPosition());
            Debug.Log(playerController.getZPosition() % 100);*//*
            ActivateObjects();
        }*/

    }

    public void SpawnTile()
    {

        GameObject plane = Instantiate(groundPlane, nextSpawnPoint, transform.rotation);
        nextSpawnPoint = plane.transform.GetChild(1).transform.position;
        numberOfPlanes++;


        /*        Debug.Log(objectToGenerate[0]+ objectToGenerate[1]+ objectToGenerate[2]);
        */
        ActivateObjects();

    }
   
    public void ActivateObjects()
    {
     
     
       
        for (int j = 0; j < 2; j++)
        {
            generateRandomObstacles();
            for (int i = 0; i < 3; i++)
            {
                

                int lane = i == 0 ? 0 : (i == 1 ? 20 : -20);

                if (objectToGenerate[i] == 1)
                {
                    GameObject obj = objectsPooling.GetObjectFromPool("OrbRed");
                    /*                Debug.Log(obj);
                    */
                    if (obj != null)
                    {

                        obj.transform.position = new Vector3(orbX + lane, orbY, initialPosition + orbZ);
                        activeObject.Add(obj);
                    }
                }
                else if (objectToGenerate[i] == 2)
                {

                    GameObject obj = objectsPooling.GetObjectFromPool("OrbBlue");
                    /*                Debug.Log(obj);
                    */
                    if (obj != null)
                    {

                        obj.transform.position = new Vector3(orbX + lane, orbY, initialPosition + orbZ);
                        activeObject.Add(obj);

                    }
                }
                else if (objectToGenerate[i] == 3)
                {

                    GameObject obj = objectsPooling.GetObjectFromPool("OrbGreen");
                    /*                Debug.Log(obj);
                    */
                    if (obj != null)
                    {

                        obj.transform.position = new Vector3(orbX + lane, orbY, initialPosition + orbZ);
                        activeObject.Add(obj);

                    }
                }
                else if (objectToGenerate[i] == 4)
                {
                    GameObject obj = objectsPooling.GetObjectFromPool("Obstacle");
                    /*                Debug.Log(obj);
                    */
                    if (obj != null)
                    {
                        Vector3 position = obj.transform.position;

                        obj.transform.position = new Vector3(obstacleX + lane, obstacelY, obstacleZ + initialPosition);
                        activeObject.Add(obj);

                    }
                }


            }
            initialPosition += 50;
        }
      
    }

    public void generateRandomObstacles()
{
   
    int zeroCount = 0;
    int fourCount = 0;
    System.Random random = new System.Random();

    for (int i = 0; i < 3; i++)
    {
        int randomNumber = random.Next(5);

        // Check if adding this number will keep the conditions valid
        if (randomNumber == 0 && zeroCount >= 2)
        {
            i--; // Decrement i and try again
        }
        else if (randomNumber == 4 && fourCount >= 2)
        {
            i--; // Decrement i and try again
        }
        else
        {
            objectToGenerate[i] = randomNumber;
            if (randomNumber == 0)
            {
                zeroCount++;
            }
            else if (randomNumber == 4)
            {
                fourCount++;
            }
        }
    }
    
}


    public void RemoveObjects()
    {
        foreach (GameObject g in activeObject)
        {

            activeObject.Remove(g);

        }

    }

    public List<GameObject> GetObjects()
    {
        return activeObject;
    }
}
