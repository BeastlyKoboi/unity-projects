using UnityEngine;
using UnityEngine.UI; // Note this new line is needed for UI
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Parent objects for UI
    public Text asterScoreText;
    public Text timeElapsedText;
    public GameObject gameplayUI;
    public GameObject pausedUI;
    public GameObject gameOverUI;

    // GameObjects for instantiation and collision
    public GameObject earth;
    public GameObject hestia;

    // List of asteroids and missiles
    public List<GameObject> asteroids;
    public List<GameObject> missiles;
    public List<GameObject> missilesToBeDestroyed;

    // Prefabs
    public GameObject asteroidPrefab;
    public GameObject missilePrefab;

    // Mouse texture
    public Texture2D mouseCrosshair;

    // Page Limits
    public float xLimit = 10f;
    public float yLimit = 6f;

    // Keybindings
    KeyCode shootKey;
    KeyCode pauseKey;

    // Sounds
    public AudioSource gameMusic;
    public AudioClip shootSound;
    public AudioClip explosionSound;

    //
    float volume;
    float timeElapsed = 0;
    int asteroidsHit = 0;
    bool isPaused;

    private void Start()
    {
        asteroids = new List<GameObject>();
        missiles = new List<GameObject>();
        missilesToBeDestroyed = new List<GameObject>();

        Cursor.SetCursor(mouseCrosshair, new Vector2(16, 16), CursorMode.Auto);

        volume = PlayerPrefs.GetFloat("Volume", 1);
        gameMusic.volume = volume;

        switch (PlayerPrefs.GetInt("ShootKey", 0))
        {
            case 0:
                shootKey = KeyCode.Mouse0;
                break;
            case 1:
                shootKey = KeyCode.Mouse1;
                break;
            case 2:
                shootKey = KeyCode.Space;
                break; 
            default:
                shootKey = KeyCode.Mouse0;
                break; 
        }

        pauseKey = KeyCode.Escape;

        gameOverUI.SetActive(false);
        pausedUI.SetActive(false);
        isPaused = false;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        timeElapsedText.text = "Time Elapsed: " + Mathf.Ceil(timeElapsed) + "s";


        if (Input.GetKeyDown(shootKey) && !isPaused)
        {
            CreateMissile();
        }

        if (Input.GetKeyDown(pauseKey))
        {
            TogglePauseGame();
        }

        foreach (GameObject missile in missiles)
        {
            if (missile.transform.position.x < -xLimit || missile.transform.position.y < -yLimit ||
                missile.transform.position.x > xLimit || missile.transform.position.y > yLimit)
            {
                missilesToBeDestroyed.Add(missile);
            }
        }

        if (missilesToBeDestroyed.Count != 0)
        {
            foreach (GameObject missile in missilesToBeDestroyed)
            {
                missiles.Remove(missile);
                Destroy(missile);
            }

            missilesToBeDestroyed = new List<GameObject>();
        }
    }

    public void TogglePauseGame()
    {
        if (!gameOverUI.activeSelf)
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                isPaused = false;
                pausedUI.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
                pausedUI.SetActive(true);
            }
        }
        
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        gameOverUI.SetActive(true);

    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Options");
    }

    public void AddAsteroid(GameObject asteroid)
    {
        asteroids.Add(asteroid);
    }

    public bool RemoveAsteroid(GameObject asteroid)
    {
        gameMusic.PlayOneShot(explosionSound, volume);
        return asteroids.Remove(asteroid);
    }

    public void RemoveMissile(GameObject missile)
    {
        missilesToBeDestroyed.Add(missile);
    }

    private void CreateMissile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 spawnPos = hestia.transform.position;

        float theta = Mathf.Atan2((mousePos.y - spawnPos.y), (mousePos.x - spawnPos.x));
        theta *= Mathf.Rad2Deg;
        missiles.Add(Instantiate(missilePrefab, spawnPos, Quaternion.Euler(0, 0, theta)));

        gameMusic.PlayOneShot(shootSound, volume);
    }

    public int AsteroidsHit
    {
        get { return asteroidsHit; }
        set
        {
            asteroidsHit = value;
            asterScoreText.text = "Asteroids Destroyed: " + asteroidsHit;
        }
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
