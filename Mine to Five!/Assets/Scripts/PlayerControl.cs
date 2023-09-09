using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    // 
    private float speed = 10;
    private float jumpingForce = 20;

    public Vector2 moveInput;

    private float blockSize = 2 * 0.64f;

    private float drillStrength = 30f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private GameManager gameManager;
    private Rigidbody2D rb;

    [SerializeField] private InputActionAsset actions;
    private InputAction moveAction;

    private void Awake()
    {
        // 
        moveAction = actions.FindActionMap("Mining").FindAction("move");
    }

    private void OnEnable()
    {
        actions.FindActionMap("Mining").Enable();
    }

    private void OnDisable()
    {
        actions.FindActionMap("Mining").Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        if (moveInput.y > 0 && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingForce);
        }

    }

    private void FixedUpdate()
    {

        if (moveInput.y < 0)
        {
            rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y + moveInput.y);
        }
        else
        {
            rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.DEPOT_TAG))
        {
            gameManager.ToggleDepotMenu();
        }
        else if (collision.CompareTag(TagManager.ALTAR_TAG))
        {
            gameManager.ToggleAltarMenu();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.DEPOT_TAG))
        {
            gameManager.ToggleDepotMenu();
        }
        else if (collision.CompareTag(TagManager.ALTAR_TAG))
        {
            gameManager.ToggleAltarMenu();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.BEDROCK_TAG)) 
            return;

        // Finds out if collision reduces blocks durability
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Blocks")
        {
            float drillDmg = 0;
            BlockInfo blockInfoScript = collision.gameObject.GetComponent<BlockInfo>();

            if (collision.gameObject.CompareTag(TagManager.GRASS_TAG))
            {
                drillDmg = drillStrength * Time.deltaTime;
            }
            else if (collision.gameObject.CompareTag(TagManager.DIRT_TAG))
            {
                drillDmg = drillStrength * Time.deltaTime;
            }
            else if (collision.gameObject.CompareTag(TagManager.COAL_TAG))
            {
                drillDmg = drillStrength * Time.deltaTime;
            }

            // Checks for blocks to the left and right of player
            if (moveInput.x != 0 && IsBlockNextToPlayer(collision))
            {
                // If to the left and left is pressed, lower durability
                if (moveInput.x < 0 && collision.transform.position.x < transform.position.x)
                {
                    blockInfoScript.Durability -= drillDmg;
                }
                // If to the right and right is pressed, lower durability
                else if (moveInput.x > 0 && collision.transform.position.x > transform.position.x)
                {
                    blockInfoScript.Durability -= drillDmg;
                }
            }
            // Checks for blocks below player
            else if (moveInput.y < 0 && IsBlockBelowPlayer(collision))
            {
                blockInfoScript.Durability -= drillDmg;
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
