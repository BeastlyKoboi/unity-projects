using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HestiaControl : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject antiRock;
    public float speed = 5f;
    public float xLimit = 8;
    public float yLimit = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player left and right
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        transform.Translate(xInput * speed * Time.deltaTime, yInput * speed * Time.deltaTime, 0f);

        //clamp the ship's x position
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -xLimit, xLimit);
        position.y = Mathf.Clamp(position.y, -yLimit, yLimit);
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroids")
        {
            Debug.Log("Hit");
        }
    }
}
