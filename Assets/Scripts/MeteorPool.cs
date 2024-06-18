using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorPool : MonoBehaviour
{
    public GameObject meteorPrefab;
    public int poolSize = 25;

    private Queue<GameObject> meteorPool;

    void Awake()
    {
        meteorPool = new Queue<GameObject>();

        for (int i = 0; i <poolSize; i++)
        {
            GameObject meteor = Instantiate(meteorPrefab);
            meteor.SetActive(false);
            meteorPool.Enqueue(meteor);
        }
    }

    public GameObject GetMeteor()
    {
        if (meteorPool.Count > 0)
        {
            GameObject meteor = meteorPool.Dequeue();
            meteor.SetActive(true);
            return meteor;
        }
        else
        {
            Debug.LogWarning("Meteor pool is empty. Creating a new meteor.");
            GameObject meteor = Instantiate(meteorPrefab);
            return meteor;
        }
    }

    public void ReturnMeteor (GameObject meteor)
    {
        meteor.SetActive(false);
        meteorPool.Enqueue(meteor);
    }
}
