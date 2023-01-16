using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // 
    private float speed = 10;
    private float jumpingForce = 20;

    private float horizontalMoveAxis;
    private float verticalMoveAxis;
    private float blockSize = 2 * 0.64f;

    private float drillStrength = 1.5f;
    private float energyMax = 100;
    private float energyLeft = 100;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private GameManager gameManager;
    private Rigidbody2D rb;

    // 
    public float EnergyLeft
    {
        get { return energyLeft; }
    }

    public float EnergyMax
    {
        get { return energyMax; }
        set { energyMax = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMoveAxis = Input.GetAxis("Horizontal");
        verticalMoveAxis = Input.GetAxis("Vertical");
        
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingForce);
        }

    }

    private void FixedUpdate()
    {
        //transform.Translate(horizontalMoveAxis * speed, verticalMoveAxis * speed, 0);
        
        

        if (verticalMoveAxis < 0)
        {
            rb.velocity = new Vector2(horizontalMoveAxis * speed, rb.velocity.y + verticalMoveAxis);
        }
        else
        {
            rb.velocity = new Vector2(horizontalMoveAxis * speed, rb.velocity.y);
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Finds out if collision reduces blocks durability
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Blocks")
        {
            float drillDmg = 0;
            BlockInfo blockInfoScript = collision.gameObject.GetComponent<BlockInfo>();

            if (collision.gameObject.CompareTag("Grass"))
            {
                drillDmg = drillStrength;
            }
            else if (collision.gameObject.CompareTag("Dirt"))
            {
                drillDmg = drillStrength;
            }
            else if (collision.gameObject.CompareTag("Coal"))
            {
                drillDmg = drillStrength * .6f;
            }

            // Checks for blocks to the left and right of player
            if (horizontalMoveAxis != 0 && IsBlockNextToPlayer(collision))
            {
                // If to the left and left is pressed, lower durability
                if (horizontalMoveAxis < 0 && collision.transform.position.x < transform.position.x)
                {
                    blockInfoScript.Durability -= drillDmg;
                    energyLeft -= .1f;
                }
                // If to the right and right is pressed, lower durability
                else if (horizontalMoveAxis > 0 && collision.transform.position.x > transform.position.x)
                {
                    blockInfoScript.Durability -= drillDmg;
                    energyLeft -= .1f;
                }
            }
            // Checks for blocks below player
            else if (verticalMoveAxis < 0 && IsBlockBelowPlayer(collision))
            {
                blockInfoScript.Durability -= drillDmg;
                energyLeft -= .1f;
            }

            // Checks for ore destruction
            if (blockInfoScript.Durability < 0)
            {
                gameManager.IncrementOre(collision.gameObject.tag);
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

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .5f, groundLayer);
    }

}