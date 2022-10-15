using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    // Fields for containing the current maze and special vertices
    private GameObject mazeVertices;
    private Vertex[,] vertexScripts;
    private Vertex startVertex;
    private Vertex finalVertex;

    // Fields for Display Info
    private const int VERTEX_PXL_SIZE = 10;
    private Color[] VERTEX_COLORS = { Color.blue, Color.white, Color.black, Color.red };
    private Vector2Int offset;

    // Random obj for rng


    // Location of player
    private Vector2Int playerLoc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateDFSMaze(int mazeSizeX, int mazeSizeY, int originX, int originY, int screenWidth, int screenHeight)
    {
        //// Overwrites the size of the maze
        //this.mazeSizeX = mazeSizeX;
        //this.mazeSizeY = mazeSizeY;

        //// Offsets (for centering)
        //offset.X = originX + (screenWidth - VERTEX_PXL_SIZE * mazeSizeX) / 2;
        //offset.Y = originY + (screenHeight - VERTEX_PXL_SIZE * mazeSizeY) / 2;

        //// Set up the Vertices
        //mazeVertices = new Vertex[mazeSizeX, mazeSizeY];
        //for (int y = 0; y < mazeSizeY; y++)
        //{
        //    for (int x = 0; x < mazeSizeX; x++)
        //    {
        //        // Set up the data to represent an empty space
        //        CellType currentType = CellType.Wall;

        //        // Set up this Vertex and check for start/end
        //        mazeVertices[x, y] = new Vertex(currentType, x, y);
        //    }
        //}

        //// Saves the start and exit
        //startVertex = mazeVertices[1, 1];
        //startVertex.Type = CellType.Start;
        //finalVertex = mazeVertices[mazeSizeX - 2, mazeSizeY - 2];
        //finalVertex.Type = CellType.Exit;

        //// Saves the player origin
        //playerLoc.X = startVertex.X;
        //playerLoc.Y = startVertex.Y;

        //// Declare currentVertex and initialize new stack
        //Vertex currentVertex;
        //Stack<Vertex> stack = new Stack<Vertex>();

        //// Push start vertex and mark it visited
        //startVertex.Visited = true;
        //stack.Push(startVertex);

        //// Loop creates paths to exit and dead ends,
        //// throughout entire maze 
        //while (stack.Count != 0)
        //{
        //    // Saves the new current vertex
        //    currentVertex = stack.Pop();

        //    // If exit is not found, keep going
        //    if (currentVertex.Type != CellType.Exit)
        //    {
        //        // create list of unvisited neighbors
        //        List<Vertex> neighbors = new List<Vertex>();

        //        // Checks for neighbors that are not visited
        //        if (TileExists(currentVertex.X - 2, currentVertex.Y) &&
        //            !mazeVertices[currentVertex.X - 2, currentVertex.Y].Visited)
        //            neighbors.Add(mazeVertices[currentVertex.X - 2, currentVertex.Y]);

        //        if (TileExists(currentVertex.X, currentVertex.Y - 2) &&
        //            !mazeVertices[currentVertex.X, currentVertex.Y - 2].Visited)
        //            neighbors.Add(mazeVertices[currentVertex.X, currentVertex.Y - 2]);

        //        if (TileExists(currentVertex.X + 2, currentVertex.Y) &&
        //            !mazeVertices[currentVertex.X + 2, currentVertex.Y].Visited)
        //            neighbors.Add(mazeVertices[currentVertex.X + 2, currentVertex.Y]);

        //        if (TileExists(currentVertex.X, currentVertex.Y + 2) &&
        //            !mazeVertices[currentVertex.X, currentVertex.Y + 2].Visited)
        //            neighbors.Add(mazeVertices[currentVertex.X, currentVertex.Y + 2]);

        //        // If there are remaining neighbors,
        //        // push current vert, make neighbor empty,
        //        // then push the neighbor too
        //        if (neighbors.Count != 0)
        //        {
        //            // Declare vertex to hold random vertex
        //            Vertex randVertex;

        //            // Add current vertex to stack
        //            stack.Push(currentVertex);

        //            // Picks random neighbor and saves it
        //            randVertex = neighbors[rng.Next(neighbors.Count)];

        //            // Makes wall in between current and rand an empty cell and visited too
        //            mazeVertices[currentVertex.X + (randVertex.X - currentVertex.X) / 2,
        //                currentVertex.Y + (randVertex.Y - currentVertex.Y) / 2].Visited = true;
        //            mazeVertices[currentVertex.X + (randVertex.X - currentVertex.X) / 2,
        //                currentVertex.Y + (randVertex.Y - currentVertex.Y) / 2].Type = CellType.Empty;

        //            randVertex.Visited = true;

        //            if (randVertex.Type == CellType.Wall)
        //            {
        //                randVertex.Type = CellType.Empty;
        //            }

        //            // Add the random vertex to stack
        //            stack.Push(randVertex);
        //        }
        //    }
        //}
        //// end of while
        //ResetAllVertices();
    }

    #region Methods to move player
    /// <summary>
    /// Moves player upwards once if possible
    /// </summary>
    public void MovePlayerUp()
    {
        if (TileExists(playerLoc.x, playerLoc.y - 1) &&
            vertexScripts[playerLoc.x, playerLoc.y - 1].Type != VertexType.Wall)
        {
            playerLoc.y -= 1;

            if (!vertexScripts[playerLoc.x, playerLoc.y].Visited)
            {
                vertexScripts[playerLoc.x, playerLoc.y].Visited = true;
            }
        }
    }

    /// <summary>
    /// Moves player downwards once if possible
    /// </summary>
    public void MovePlayerDown()
    {
        if (TileExists(playerLoc.x, playerLoc.y + 1) &&
            vertexScripts[playerLoc.x, playerLoc.y + 1].Type != VertexType.Wall)
        {
            playerLoc.y += 1;

            if (!vertexScripts[playerLoc.x, playerLoc.y].Visited)
            {
                vertexScripts[playerLoc.x, playerLoc.y].Visited = true;
            }
        }
    }

    /// <summary>
    /// Moves player to the left once if possible
    /// </summary>
    public void MovePlayerLeft()
    {
        if (TileExists(playerLoc.x - 1, playerLoc.y) &&
            vertexScripts[playerLoc.x - 1, playerLoc.y].Type != VertexType.Wall)
        {
            playerLoc.y -= 1;

            if (!vertexScripts[playerLoc.x, playerLoc.y].Visited)
            {
                vertexScripts[playerLoc.x, playerLoc.y].Visited = true;
            }
        }
    }

    /// <summary>
    /// Moves player to the right once if possible
    /// </summary>
    public void MovePlayerRight()
    {
        if (TileExists(playerLoc.x + 1, playerLoc.y) &&
            vertexScripts[playerLoc.x + 1, playerLoc.y].Type != VertexType.Wall)
        {
            playerLoc.y += 1;

            if (!vertexScripts[playerLoc.x, playerLoc.y].Visited)
            {
                vertexScripts[playerLoc.x, playerLoc.y].Visited = true;
            }
        }
    }
    #endregion

    /// <summary>
    /// Sets all Vertices to "not visited" and return the start Vertex
    /// </summary>
    public void ResetAllVertices()
    {
        for (int x = 0; x < vertexScripts.GetLength(0); x++)
        {
            for (int y = 0; y < vertexScripts.GetLength(1); y++)
            {
                // Reset the Vertex
                vertexScripts[x, y].Visited = false;
            }
        }

        playerLoc.x = startVertex.MazePos.x;
        playerLoc.y = startVertex.MazePos.y;
    }

    /// <summary>
    /// Returns true if x and y are within bounds of maze
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private bool TileExists(int x, int y)
    {
        // Valid indices?
        return (y >= 0 && x >= 0 &&
            y < vertexScripts.GetLength(0) &&
            x < vertexScripts.GetLength(1));
    }
}
