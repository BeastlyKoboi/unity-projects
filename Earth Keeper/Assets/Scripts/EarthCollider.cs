using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCollider : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag + "?=" + "Asteroids");
        if (collision.gameObject.CompareTag("Asteroids"))
        {
            gameManager.RemoveAsteroid(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
