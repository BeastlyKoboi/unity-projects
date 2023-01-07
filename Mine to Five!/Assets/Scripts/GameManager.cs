using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Map Limits
    private int halfMapWidth;

    //-- Block Prefabs --//
    // Grass Block
    public GameObject grassBlock;
    public GameObject dirtBlock;
    public GameObject coalBlock;

    // Layer Depths
    private int grassHeight;
    private int layerOneHeight;
    private int layerTwoHeight;


    // Start is called before the first frame update
    void Start()
    {
        halfMapWidth = 15;

        grassHeight = 1;
        layerOneHeight = 10;
        layerTwoHeight = 10;

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

        int layerOffset = 0;
        
        // Make grass layer
        for (int count = -halfMapWidth; count <= halfMapWidth; count++)
        {
            GameObject block = Instantiate(grassBlock, new Vector3(count * blockSize, -blockSize / 2, 0), Quaternion.identity);

        }
        layerOffset += grassHeight;

        // Make first layer of ore
        for (int row = 0; row < layerOneHeight; row++)
        {
            float rowY = -blockSize * (layerOffset + row) - (blockSize / 2);
            
            for (int count = -halfMapWidth; count <= halfMapWidth; count++)
            {
                if (Random.value > .8)
                {
                    Instantiate(coalBlock, new Vector3(count * blockSize, rowY, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(dirtBlock, new Vector3(count * blockSize, rowY, 0), Quaternion.identity);
                }
            } 
        }

    }


}
