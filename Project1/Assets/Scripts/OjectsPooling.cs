using System.Collections.Generic;
using UnityEngine;

public class ObjectsPooling : MonoBehaviour
{
    public GameObject redOrbPrefab; // The prefab to be pooled
    public GameObject blueOrbPrefab;
    public GameObject greenOrbPrefab;
    public GameObject obstaclePrefab;
    public int poolSize = 6; // Number of objects to create in the pool

    public List<GameObject> objectPool = new List<GameObject>();

    void Start()
    {
        InitializePool(blueOrbPrefab);
        InitializePool(redOrbPrefab);
        InitializePool(greenOrbPrefab);
        InitializePool(obstaclePrefab);
        


    }

    void InitializePool(GameObject prefab)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool(string tag)
    {
        foreach (GameObject obj in objectPool)
        {
/*            Debug.Log(obj);
*/
            if (obj != null) { 
            if (!obj.activeSelf && obj.CompareTag(tag))
            {
                obj.SetActive(true);
                return obj;
            }
            }
        }
        return null; // No available objects in the pool
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void ReturnAllObjectsToPool()
    {
        foreach (GameObject obj in objectPool)
        {
            obj.SetActive(false);
        }
    }

    public void ReturnObtsaclesToPool()
    {
        foreach (GameObject obj in objectPool)
        {
            if(obj.activeSelf && obj.CompareTag("Obstacle"))
            obj.SetActive(false);
        }
    }
}
