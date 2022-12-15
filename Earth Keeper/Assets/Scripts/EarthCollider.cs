using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCollider : MonoBehaviour
{
    public GameManager gameManager;
    public bool collidesPlayer; 

    // Start is called before the first frame update
    void Start()
    {
        collidesPlayer = Convert.ToBoolean(PlayerPrefs.GetInt("HestiaEarthCollides", 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroids") ||
            (collision.gameObject.CompareTag("Player") && collidesPlayer))
        {
            gameManager.EndGame();
        }
    }
}
