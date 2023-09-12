using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerControl : MonoBehaviour
{
    // 
    private Vector3 spawnPos = new Vector3(0, 0.64f, 0);
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
    private InputAction godModeAction;

    private void Awake()
    {
        // 
        moveAction = actions.FindActionMap("Mining").FindAction("move");
        godModeAction = actions.FindActionMap("Mining").FindAction("godmode");
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

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        if (godModeAction.WasPerformedThisFrame())
        {
            Debug.Log("God Mode Enabled!");
            speed = 20;
            drillStrength = 200;
        }

        // TODO: Find out if this should be in Fixed Update
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

        if (LayerMask.LayerToName(collision.gameObject.layer) != "Blocks")
            return;

        if (moveInput.x == 0 && moveInput.y >= 0)
            return;

        BlockInfo blockInfoScript = collision.gameObject.GetComponent<BlockInfo>();
        float drillDmg = drillStrength * Time.deltaTime;

        if (IsDrillingBlock(collision))
            blockInfoScript.Durability -= drillDmg;

        // Checks for ore destruction
        if (blockInfoScript.Durability < 0)
            gameManager.IncrementOre(collision.gameObject.tag);

    }

    /// <summary>
    /// Checks if the colliding block matches the drilling direction
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    private bool IsDrillingBlock(Collision2D collision)
    {
        return (
            // Is drilling block to left or right
            (IsBlockNextToPlayer(collision) &&
                ((moveInput.x < 0 && collision.transform.position.x < transform.position.x) ||
                (moveInput.x > 0 && collision.transform.position.x > transform.position.x)))
            ||
            // Is drilling block directly below player
            (moveInput.y < 0 && IsBlockBelowPlayer(collision))
        );
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

    public bool IsUnderground()
    {
        return transform.position.y < 0;
    }

    public void TeleportToSpawn()
    {
        transform.position = spawnPos;
    }

}
