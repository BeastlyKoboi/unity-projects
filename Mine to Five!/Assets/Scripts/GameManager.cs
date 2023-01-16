using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Map Limits
    private int halfMapWidth;

    //
    [SerializeField] private PlayerControl playerControl;

    //-- Block Prefabs --//
    // Grass Block
    [SerializeField] private GameObject grassBlock;
    [SerializeField] private GameObject dirtBlock;
    [SerializeField] private GameObject coalBlock;
    [SerializeField] private GameObject copperBlock;

    // Layer Depths
    private int grassHeight;
    private int layerOneHeight;
    private int layerTwoHeight;

    //
    [SerializeField] private TextMeshProUGUI energyText;

    private int coalOre;
    [SerializeField] private TextMeshProUGUI coalText;


    // Start is called before the first frame update
    void Start()
    {
        halfMapWidth = 15;

        grassHeight = 1;
        layerOneHeight = 10;
        layerTwoHeight = 10;

        coalOre = 0;

        CreateMap();
    }

    // Update is called once per frame
    void Update()
    {

        int energyLeft = (int)Mathf.Ceil(playerControl.EnergyLeft);
        energyText.text = energyLeft.ToString() + "/" + playerControl.EnergyMax;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
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

    //
    public void IncrementOre(string tag)
    {
        switch (tag)
        {
            case "Coal":
                coalOre++;
                coalText.text = coalOre.ToString();
                break;

            default: 
                break; 
        }
    }

    // 
    public void TogglePause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

}