using UnityEngine;
using UnityEngine.UI; // Note this new line is needed for UI
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Labels for UI
    public Text asterScoreText;

    // GameObjects for instantiation and collision
    public GameObject asteroidPrefab;
    public GameObject earth;
    public List<GameObject> asteroids;
    List<GameObject> toBeDestroyed;

    //
    float timeElapsed;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void AddAsteroid(GameObject asteroid)
    {
        asteroids.Add(asteroid);
    }

    public bool RemoveAsteroid(GameObject asteroid)
    {
        return asteroids.Remove(asteroid);
    }

    /*
    public Text scoreText;
    public Text gameOverText;
    public Text timeText;
    CollisionDetector collisionDetector;
    public List<GameObject> rocks;
    public List<GameObject> antirocks;
    public GameObject rocket;
    ShipControl shipControl;
    GameObject antirock;
    GameObject rock;
    public GameObject orbPrefab;
    public List<GameObject> orbs;
    int xUnitBorder = 15;
    List<GameObject> toBeDestroyed;
    float timeElapsed = 0;

    int playerScore = 0;

    bool rockOut;

    void Start()
    {
        collisionDetector = gameObject.GetComponent<CollisionDetector>();
        rocks = new List<GameObject>();
        antirocks = new List<GameObject>();
        shipControl = rocket.GetComponent<ShipControl>();
        toBeDestroyed = new List<GameObject>();
    }


    void Update()
    {
        //Exercise 15 requires that you check for collisions between MeMoRocks and the Rocket

        //check to see whether any rock has rolled onto the rocket
        foreach (GameObject orb in orbs)
        {
            if (collisionDetector.AABBTest(rocket, orb))
                PlayerDied();
        }

        rockOut = false;

        foreach (GameObject antirck in antirocks)
        {
            foreach (GameObject rck in rocks)
            {
                if (collisionDetector.AABBTest(antirck, rck))
                {
                    rockOut = true;
                    antirock = antirck;
                    rock = rck;
                    break;
                }
            }

            if (rockOut)
                break;
        }

        if (rockOut)
        {
            //Exercise 15 requires that you modify this code as such:
            //a new GameObject called a MeMoRock should be instantiated with the same position and velocity as the Rock that collided with the AntiRock
            AddOrbToList(Instantiate(orbPrefab, rock.transform.position, Quaternion.identity));
            RemoveRockFromList(rock);
            RemoveAntiRockFromList(antirock);
            Destroy(rock);
            Destroy(antirock);
            AddScore();
        }

        //Exercise 15 requires that you remove MeMoRocks that have gone out of bounds, to the left of the Rockeet
      
        
        foreach (GameObject orb in orbs)
        {
            if (orb.transform.position.x < -xUnitBorder)
            {
                toBeDestroyed.Add(orb);
            }
        }

        if (toBeDestroyed.Count > 0)
        {
            foreach(GameObject orb in toBeDestroyed)
            {
                RemoveOrbFromList(orb);
                Destroy(orb);
            }
        }

        timeElapsed += Time.deltaTime;
        timeText.text = "Total Time: " + Mathf.Ceil(timeElapsed) + "s";
    }

    //Exercise 15 requires that you implement this method, using the formula derived in Case Study 15
    public float calculateTheta(Vector3 pos)
    {
        float A, B, C, t;
        float p1, p2, q1, q2;
        float w, s;
        float discriminant;
        float theta;

        theta = 0f;  //initial value

        w = 2f; //NOTE: could obtain this value through RockMover.speed
        s = 10f; //NOTE: could obtain this through AntiRock.speed

        p1 = rocket.transform.position.x;
        p2 = rocket.transform.position.y;
        q1 = pos.x;
        q2 = pos.y;

        A = s * s - w * w;
        B = 2 * w * (q1 - p1);
        C = -(Mathf.Pow(q1 - p1, 2) + Mathf.Pow(q2 - p2, 2));

        t = (-B + Mathf.Sqrt(B * B - 4 * A * C)) / (2 * A);

        theta = Mathf.Asin((q2 - p2) / (t * s));

        return theta;
    }

    public void AddRockToList(GameObject rock)  //Note this is where calculateTheta() is called, followed by call to purSeek(theta)
    {
        float theta;
        rock.GetComponent<Animator>().enabled = false;  //this turns off the Rock spinning
        rocks.Add(rock);
        theta = calculateTheta(rock.transform.position);
        shipControl.purSeek(theta);
    }

    public bool RemoveRockFromList(GameObject rock)
    {
        return rocks.Remove(rock);
    }

    public void AddAntiRockToList(GameObject antirock)
    {
        antirocks.Add(antirock);
    }

    public bool RemoveAntiRockFromList(GameObject antirock)
    {
        return antirocks.Remove(antirock);
    }


    public void AddScore()
    {
        playerScore++;
        //This converts the score (a number) into a string
        scoreText.text = "MeMoRocks Formed: " + playerScore.ToString();
    }

    public void PlayerDied()
    {
        gameOverText.enabled = true;

        // This freezes the game
        Time.timeScale = 0;
    }

    public void AddOrbToList(GameObject orb)
    {
        orbs.Add(orb);
    }

    public bool RemoveOrbFromList(GameObject orb)
    {
        return orbs.Remove(orb);
    }*/
}
