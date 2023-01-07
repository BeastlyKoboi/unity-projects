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
    private float verticalMoveAxis;
    private float blockSize = 2 * 0.64f;

    private float drillDamage;

    // Start is called before the first frame update
    void Start()
    {
        speed = .4f;
        horizontalMoveAxis = 0;
        drillDamage = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMoveAxis = Input.GetAxis("Horizontal");
        verticalMoveAxis = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        transform.Translate(horizontalMoveAxis * speed, verticalMoveAxis * speed, 0);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Finds out if collision reduces blocks durability
        if (collision.gameObject.CompareTag("Block"))
        {
            // Checks for blocks to the left and right of player
            if (horizontalMoveAxis != 0 && IsBlockNextToPlayer(collision))
            {
                // If to the left and left is pressed, lower durability
                if (horizontalMoveAxis < 0 && collision.transform.position.x < transform.position.x)
                {
                    collision.gameObject.GetComponent<BlockInfo>().Durability -= drillDamage;
                }
                // If to the right and right is pressed, lower durability
                else if (horizontalMoveAxis > 0 && collision.transform.position.x > transform.position.x)
                {
                    collision.gameObject.GetComponent<BlockInfo>().Durability -= drillDamage;
                }
            }
            // Checks for blocks below player
            else if (verticalMoveAxis < 0 && IsBlockBelowPlayer(collision))
            {
                collision.gameObject.GetComponent<BlockInfo>().Durability -= drillDamage;
            }
        }
    }

    /// <summary>
    /// Checks if block is to the left or right of player
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    private bool IsBlockNextToPlayer(Collision2D collision)
    {
        return (collision.transform.position.y > transform.position.y - blockSize / 2 && 
            collision.transform.position.y < transform.position.y + blockSize / 2);
    }

    /// <summary>
    /// Checks if block is below the player
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    private bool IsBlockBelowPlayer(Collision2D collision)
    {
        return (collision.transform.position.x > transform.position.x - blockSize / 2 && 
            collision.transform.position.x < transform.position.x + blockSize / 2 && 
            collision.transform.position.y < transform.position.y);
    }

}
