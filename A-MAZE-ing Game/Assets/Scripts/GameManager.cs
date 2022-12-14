using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Fields to hold the maze
    public Maze maze;
    public Camera cam;

    // Fields to define movement speed
    private int movementTimer;
    private int movementCooldown;

    // Maze offset to center maze
    public Vector2 offset;

    // Number of tiles in maze
    public Vector2Int mazeSize;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize starting movement values
        movementTimer = 0;
        movementCooldown = 5;

        offset.x = -1f;
        offset.y = -4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            maze.CreateDFSMaze(53, 53, -3f, 5f);
        }

        if (Input.GetKey(KeyCode.W) && movementTimer == 0)
        {
            maze.MovePlayerUp();
            movementTimer += movementCooldown;
        }
        else if (Input.GetKey(KeyCode.A) && movementTimer == 0)
        {
            maze.MovePlayerLeft();
            movementTimer += movementCooldown;
        }
        else if (Input.GetKey(KeyCode.S) && movementTimer == 0)
        {
            maze.MovePlayerDown();
            movementTimer += movementCooldown;
        }
        else if (Input.GetKey(KeyCode.D) && movementTimer == 0)
        {
            maze.MovePlayerRight();
            movementTimer += movementCooldown;
        }

        if (movementTimer > 0)
        {
            movementTimer--;
        }

       

    }


}
