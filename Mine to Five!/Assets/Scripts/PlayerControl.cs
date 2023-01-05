using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // 
    public float speed;

    public bool leftCollided;
    public bool rightCollided;

    private Rigidbody2D rigidbody2d;
    private float horizontalMoveAxis;

    // Start is called before the first frame update
    void Start()
    {
        speed = .4f;
        horizontalMoveAxis = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMoveAxis = Input.GetAxis("Horizontal");


        /*if (leftCollided)
        {

        }*/

    }

    private void FixedUpdate()
    {
        float oldXPos = transform.position.x;

        transform.Translate(horizontalMoveAxis * speed, 0, 0);

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        float blockSize = 2 * 0.64f;

        if (collision.gameObject.CompareTag("Block") && horizontalMoveAxis != 0)
        {
            if (collision.transform.position.y > transform.position.y - blockSize / 4 && collision.transform.position.y < transform.position.y + blockSize / 4)
            {
                if (horizontalMoveAxis < 0 && collision.transform.position.x < transform.position.x)
                {
                    collision.gameObject.GetComponent<Grass>().Durability -= .5f;
                }
                else if (horizontalMoveAxis > 0 && collision.transform.position.x > transform.position.x)
                {
                    collision.gameObject.GetComponent<Grass>().Durability -= .5f;
                }
            }
        }
    }

}
