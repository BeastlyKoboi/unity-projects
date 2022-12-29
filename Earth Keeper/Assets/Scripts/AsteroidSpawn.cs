using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject earth;
    public GameObject asteroidPrefab;
    public float minSpawnDelay = 3f;
    public float maxSpawnDelay = 6f;
    public float spawnXLimit = 10f;
    public float spawnYLimit = 6f;
    public float timeTilSpeedUp;
    public float maxTimeTilSpeedUp;

    // Start is called before the first frame update
    void Start()
    {
        timeTilSpeedUp = maxTimeTilSpeedUp;
        Spawn();
    }

    //
    private void Update()
    {
        if (timeTilSpeedUp < 0)
        {
            minSpawnDelay *= .9f;
            maxSpawnDelay *= .9f;

            timeTilSpeedUp = maxTimeTilSpeedUp;
        }

        timeTilSpeedUp -= Time.deltaTime;
    }

    //
    void Spawn()
    {
        float randomSide = Random.Range(1, 5);
        float random = Random.Range(-spawnYLimit, spawnYLimit);
        Vector3 spawnPos;
        float theta;

        switch (randomSide)
        {
            // top side
            case 1:
                spawnPos = new Vector3(Random.Range(-spawnXLimit, spawnXLimit), spawnYLimit, 0f);
                break;

            // right side
            case 2:
                spawnPos = new Vector3(spawnXLimit, Random.Range(-spawnYLimit, spawnYLimit), 0f);
                break;
            
            // bottom side
            case 3:
                spawnPos = new Vector3(Random.Range(-spawnXLimit, spawnXLimit), -spawnYLimit, 0f);
                break;

            // left side
            case 4:
                spawnPos = new Vector3(-spawnXLimit, Random.Range(-spawnYLimit, spawnYLimit), 0f);
                break;
                
            // top side 
            default:
                spawnPos = new Vector3(Random.Range(-spawnXLimit, spawnXLimit), spawnYLimit, 0f);
                break;
        }

        theta = Mathf.Atan2((earth.transform.position.y - spawnPos.y) , (earth.transform.position.x - spawnPos.x));
        theta *= Mathf.Rad2Deg;

        GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.Euler(0, 0, theta));

        newAsteroid.GetComponent<AsteroidMove>().gameManager = gameManager;

        gameManager.AddAsteroid(newAsteroid);

        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
