using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour
{
    // Fields to hold the cell type, position, and visited status
    private VertexType type;
    private Vector2Int mazePos;
    private bool visited;

    #region Properties for the fields
    /// <summary>
    /// Property to get and set the cell type
    /// </summary>
    public VertexType Type
    {
        get { return type; }
        set { type = value; }
    }

    /// <summary>
    /// Property to get and set X position
    /// </summary>
    public Vector2Int MazePos
    {
        get { return mazePos;}
        set { mazePos = value; }
    }

    /// <summary>
    /// Property to get and set whether it has been visited
    /// </summary>
    public bool Visited
    {
        get { return visited; }
        set { visited = value; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        type = VertexType.Wall;
        mazePos = new Vector2Int();
        visited = false;
    }

    public void SetMazePos()
    {

    }
}
