using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Map Limits
    private int halfMapWidth;
    private int mapHeight;

    //-- Block Prefabs --//
    // Grass Block
    public GameObject grassBlock;

    // Start is called before the first frame update
    void Start()
    {
        halfMapWidth = 15;
        mapHeight = 30;

        CreateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    private void CreateMap()
    {
        float blockSize = 2 * 0.64f; 
        // 
        
        for (int count = -halfMapWidth; count <= halfMapWidth; count++)
        {
            GameObject block = Instantiate(grassBlock, new Vector3(count * blockSize, -blockSize / 2, 0), Quaternion.identity);

        }
        
    }


}
