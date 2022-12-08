using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject asteroidPrefab;
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 3f;
    public float spawnXLimit = 10f;
    public float spawnYLimit = 6f;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    //
    void Spawn()
    {
        /*
        // Create a rock at a random y position
        float random = Random.Range(-spawnYLimit, spawnYLimit);
        Vector3 spawnPos = transform.position + new Vector3(0, random, 0f);
        gameManager.AddRockToList(Instantiate(rock, spawnPos, Quaternion.identity));
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
        */
    }
}
