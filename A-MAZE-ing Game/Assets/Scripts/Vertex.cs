using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour
{
    // Fields to hold the cell type, position, and visited status
    public VertexType type;
    public Vector2Int mazePos;
    public bool visited;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    #region Properties for the fields
    /// <summary>
    /// Property to get and set the cell type
    /// </summary>
    public VertexType Type
    {
        get { return type; }
        set 
        { 
            type = value; 
            
            switch(type)
            {
                case VertexType.Wall:
                    spriteRenderer.sprite = sprites[0];
                    break;
                case VertexType.Empty:
                    spriteRenderer.sprite = sprites[1];
                    break;
                case VertexType.Start:
                    spriteRenderer.sprite = sprites[3];
                    break;
                case VertexType.Exit:
                    spriteRenderer.sprite = sprites[4];
                    break;
                default:
                    spriteRenderer.sprite = sprites[0];
                    break;
            }
        }
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
        set 
        { 
            visited = value;
            if (type == VertexType.Empty)
            {
                if (visited)
                    spriteRenderer.sprite = sprites[2];
                else
                    spriteRenderer.sprite = sprites[1];
            }

        }
    }
    #endregion

    private void Awake()
    {
        type = VertexType.Wall;
        visited = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetMazePos()
    {

    }
}
