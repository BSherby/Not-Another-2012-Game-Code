using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSpawnManager : MonoBehaviour
{
    public GameObject meteorPrefab;

    public float spawnPosY = 7;
    public float spawnPosZ = 0;
    public float startDelay = 0.5f;
    public float spawnMinX = -8;
    public float spawnMaxX = 10f;

    private void Start()
    {
        StartCoroutine(SpawnMeteors());
    }

    IEnumerator SpawnMeteors()
    {
        Debug.Log("Coroutine started - initial delay");
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            Debug.Log("Spawning a new meteor");
            SpawnMeteor();
            float nextSpawnDelay = Random.Range(0.5f, 2f);
            Debug.Log($"Next spawn in {nextSpawnDelay} seconds");
            yield return new WaitForSeconds(nextSpawnDelay);
        }
    }

    void SpawnMeteor()
    {
        GameObject meteor = Instantiate(meteorPrefab);
        meteor.transform.position = GetSpawnPosition();
        meteor.tag = "Meteor";
    }

    Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(spawnMinX, spawnMaxX), spawnPosY, spawnPosZ);
    }
}
