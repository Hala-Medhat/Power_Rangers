using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner growndSpawner;
    PlayerController playerController;
    ObjectsPooling objectsPooling;

    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        growndSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        objectsPooling = GameObject.FindObjectOfType<ObjectsPooling>();


    }

    private void OnTriggerExit(Collider other)
    {

        foreach (GameObject o in objectsPooling.objectPool)
        {
            if (o.transform.position.z  <= playerController.getZPosition() && o.activeSelf)
            {

                objectsPooling.ReturnObjectToPool(o);
            }
        }

        Destroy(gameObject, 2);
        growndSpawner.SpawnTile();





    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
