using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cometPrefabs;
    private List<GameObject> cometPool;
    //private float spawnRangex = 34;
    private float spawnPosY = 8;
    private float spawnPosZ = 0;
    private float startDelay = 1.0f;

    private float spawnMinX = -8;
    private float spawnMaxX = 10;
    // Start is called before the first frame update
    void Start()
    {
        InitializeCometPool();
        StartCoroutine(SpawnComets());
    }

    IEnumerator SpawnComets()
    {
        Debug.Log("Coroutine started - initial delay");
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            Debug.Log("Spawning a new comet");
            SpawnRandomComet();
            float nextSpawnDelay = Random.Range(2f, 5f); //Establishes a random delay between 2 to 5 seconds
            Debug.Log($"Next spawn in {nextSpawnDelay} seconds");
            yield return new WaitForSeconds(nextSpawnDelay);

        }
    }

    void SpawnRandomComet()
    {
        GameObject comet = GetCometFromPool();
        if (comet != null)
        {
            int cometIndex = Random.Range(0, cometPrefabs.Length);
            Vector3 spawnPos = new Vector3(Random.Range(spawnMinX, spawnMaxX), spawnPosY, spawnPosZ);

            Instantiate(cometPrefabs[cometIndex], spawnPos, cometPrefabs[cometIndex].transform.rotation);
        }
    }

    void InitializeCometPool()
    {
        cometPool = new List<GameObject>();
        for(int i = 0; i < cometPrefabs.Length; i++)
        {
            GameObject obj = Instantiate(cometPrefabs[i]);
            obj.SetActive(false);
            cometPool.Add(obj);
        }
    }

    GameObject GetCometFromPool()
    {
        foreach(var comet in cometPool)
        {
            if (!comet.activeInHierarchy)
            {
                return comet;
            }
        }
        //Optionally expand the pool here if all comets are active and more are needed
        return null;
    }
}
