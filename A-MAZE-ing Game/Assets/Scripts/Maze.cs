using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    // Fields for containing the current maze and special vertices
    private GameObject[,] mazeVertices;
    private Vertex[,] vertexScripts;
    public Vertex startVertex;
    public Vertex finalVertex;
    public GameObject prefab;

    // Fields for Display Info
    private const int VERTEX_PXL_SIZE = 10;
    private float vertexSize;
    private Color[] VERTEX_COLORS = { Color.blue, Color.white, Color.black, Color.red };
    private Vector2 offset;

    // Random obj for rng


    // Location of player
    private Vector2Int playerLoc;

    // Start is called before the first frame update
    void Start()
    {

        vertexSize = prefab.GetComponent<SpriteRenderer>().bounds.size.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateDFSMaze(int mazeSizeX, int mazeSizeY, float originX, float originY)
    {
        // int mazeSizeX, int mazeSizeY, int originX, int originY, int screenWidth, int screenHeight

        // Offsets (for centering)
        //offset.x = originX + (screenWidth - VERTEX_PXL_SIZE * mazeSizeX) / 2;
        //offset.y = originY + (screenHeight - VERTEX_PXL_SIZE * mazeSizeY) / 2;

        if (mazeVertices != null)
        {
            for (int y = 0; y < mazeSizeY; y++)
            {
                for (int x = 0; x < mazeSizeX; x++)
                {
                    Destroy(mazeVertices[x, y]);
                }
            }
        }

        // calculates the center from the offset
        offset.x = originX + ((9 - originX) * 100 - (mazeSizeX * 16)) / 200;
        offset.y = originY - ((5 + originY) * 100 - (mazeSizeY * 16)) / 200;

        // Set up the Vertices
        mazeVertices = new GameObject[mazeSizeX, mazeSizeY];
        vertexScripts = new Vertex[mazeSizeX, mazeSizeY];

        for (int y = 0; y < mazeSizeY; y++)
        {
            for (int x = 0; x < mazeSizeX; x++)
            {
                // Set up this Vertex and check for start/end
                mazeVertices[x, y] = Instantiate(prefab,
                    new Vector3(offset.x + x * vertexSize, offset.y - y * vertexSize, 0),
                    Quaternion.identity);
                
            }
        }

        for (int y = 0; y < mazeSizeY; y++)
        {
            for (int x = 0; x < mazeSizeX; x++)
            {
                // Set up this Vertex and check for start/end
                
                vertexScripts[x, y] = mazeVertices[x, y].GetComponent<Vertex>();
                vertexScripts[x, y].mazePos = new Vector2Int(x, y);
            }
        }

        // Saves the start and exit
        startVertex = vertexScripts[1, 1];
        startVertex.Type = VertexType.Start;
        finalVertex = vertexScripts[mazeSizeX - 2, mazeSizeY - 2];
        finalVertex.Type = VertexType.Exit;

        // Saves the player origin
        playerLoc.x = startVertex.mazePos.x;
        playerLoc.y = startVertex.mazePos.y;

        // Declare currentVertex and initialize new stack
        Vertex currentVertex;
        Stack<Vertex> stack = new Stack<Vertex>();

        // Push start vertex and mark it visited
        startVertex.Visited = true;
        stack.Push(startVertex);

        // Loop creates paths to exit and dead ends,
        // throughout entire maze 
        while (stack.Count != 0)
        {
            // Saves the new current vertex
            currentVertex = stack.Pop();

            // If exit is not found, keep going
            if (currentVertex.Type != VertexType.Exit)
            {
                // create list of unvisited neighbors
                List<Vertex> neighbors = new List<Vertex>();

                // Checks for neighbors that are not visited
                if (TileExists(currentVertex.mazePos.x - 2, currentVertex.mazePos.y) &&
                    !vertexScripts[currentVertex.mazePos.x - 2, currentVertex.mazePos.y].Visited)
                neighbors.Add(vertexScripts[currentVertex.mazePos.x - 2, currentVertex.mazePos.y]);

                if (TileExists(currentVertex.mazePos.x, currentVertex.mazePos.y - 2) &&
                    !vertexScripts[currentVertex.mazePos.x, currentVertex.mazePos.y - 2].Visited)
                neighbors.Add(vertexScripts[currentVertex.mazePos.x, currentVertex.mazePos.y - 2]);

                if (TileExists(currentVertex.mazePos.x + 2, currentVertex.mazePos.y) &&
                    !vertexScripts[currentVertex.mazePos.x + 2, currentVertex.mazePos.y].Visited)
                neighbors.Add(vertexScripts[currentVertex.mazePos.x + 2, currentVertex.mazePos.y]);

                if (TileExists(currentVertex.mazePos.x, currentVertex.mazePos.y + 2) &&
                    !vertexScripts[currentVertex.mazePos.x, currentVertex.mazePos.y + 2].Visited)
                neighbors.Add(vertexScripts[currentVertex.mazePos.x, currentVertex.mazePos.y + 2]);

                Debug.Log(neighbors.Count);
                //Debug.Break();

                // If there are remaining neighbors,
                // push current vert, make neighbor empty,
                // then push the neighbor too
                if (neighbors.Count != 0)
                {
                    // Declare vertex to hold random vertex
                    Vertex randVertex;

                    // Add current vertex to stack
                    stack.Push(currentVertex);

                    // Picks random neighbor and saves it
                    randVertex = neighbors[Random.Range(0, neighbors.Count)];

                    // Makes wall in between current and rand an empty cell and visited too
                    vertexScripts[currentVertex.mazePos.x + (randVertex.mazePos.x - currentVertex.mazePos.x) / 2,
                        currentVertex.mazePos.y + (randVertex.mazePos.y - currentVertex.mazePos.y) / 2].Visited = true;
                    vertexScripts[currentVertex.mazePos.x + (randVertex.mazePos.x - currentVertex.mazePos.x) / 2,
                        currentVertex.mazePos.y + (randVertex.mazePos.y - currentVertex.mazePos.y) / 2].Type = VertexType.Empty;

                    randVertex.Visited = true;

                    if (randVertex.Type == VertexType.Wall)
                    {
                        randVertex.Type = VertexType.Empty;
                    }

                    // Add the random vertex to stack
                    stack.Push(randVertex);
                }
            }

        }
        ResetAllVertices();
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
            y < vertexScripts.GetLength(1) &&
            x < vertexScripts.GetLength(0));
    }
}
