using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Fields to hold the maze
    public Maze maze;

    // Fields to define movement speed
    private int movementTimer;
    private int movementCooldown;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize starting movement values
        movementTimer = 0;
        movementCooldown = 5;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.N))
        {
            maze.CreateDFSMaze(50, 50, .1f, .1f);
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
