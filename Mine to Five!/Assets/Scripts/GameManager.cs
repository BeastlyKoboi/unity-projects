using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // The script attached to the player
    [SerializeField] private PlayerControl playerControl;

    // The cameras for gameplay and the stocks
    [SerializeField] private Camera[] cams;
    [SerializeField] private int currentCamIndex = 0;

    //-- Block Prefabs --//
    // Grass Block
    [SerializeField] private GameObject bedrockBlock;
    [SerializeField] private GameObject grassBlock;
    [SerializeField] private GameObject dirtBlock;
    [SerializeField] private GameObject coalBlock;
    [SerializeField] private GameObject copperBlock;

    // Map Limits
    private int halfMapWidth = 15;

    // Layer Depths
    private int grassHeight = 1;
    private int layerOneHeight = 10;
    private int layerTwoHeight = 10;

    // Gameplay UI
    private int currentHour = 8;
    private float currentTimePassed = 0.0f;
    private int hourIntervals = 15;
    [SerializeField] private TextMeshProUGUI workTimeText;
    private int coalOre = 0;
    [SerializeField] private TextMeshProUGUI coalText;

    // Paused UI
    [SerializeField] private GameObject pausedMenu;

    // Start is called before the first frame update
    void Start()
    {
        pausedMenu.SetActive(false);

        currentCamIndex = 0;

        CreateMap();
    }

    // Update is called once per frame
    void Update()
    {
        currentTimePassed += Time.deltaTime;

        if (currentTimePassed >= hourIntervals)
        {
            currentTimePassed -= hourIntervals;
            currentHour++;

            if (currentHour > 12)
            {
                currentHour = 1;
            }
            else if (currentHour == 5)
            {

            }

            workTimeText.text = currentHour + ":00";
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    /// <summary>
    /// Automatically generates the map to begin the game with several different layers of ore rarity.
    /// </summary>
    private void CreateMap()
    {
        float blockSize = 2 * 0.64f;

        int layerOffset = 0;

        // Calculate left and right bedrock
        float bedrockXLeft = -(halfMapWidth + 1) * blockSize;
        float bedrockXRight = (halfMapWidth + 1) * blockSize;

        // Add Bedrock Here
        Instantiate(bedrockBlock, new Vector3(bedrockXLeft, -blockSize / 2, 0), Quaternion.identity);

        // Make grass layer
        for (int count = -halfMapWidth; count <= halfMapWidth; count++)
        {
            GameObject block = Instantiate(grassBlock, new Vector3(count * blockSize, -blockSize / 2, 0), Quaternion.identity);

        }

        // Add Bedrock here
        Instantiate(bedrockBlock, new Vector3(bedrockXRight, -blockSize / 2, 0), Quaternion.identity);

        layerOffset += grassHeight;

        // Make first layer of ore
        for (int row = 0; row < layerOneHeight; row++)
        {
            float rowY = -blockSize * (layerOffset + row) - (blockSize / 2);

            // Add Bedrock Here
            Instantiate(bedrockBlock, new Vector3(bedrockXLeft, rowY, 0), Quaternion.identity);

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

            // Add Bedrock here
            Instantiate(bedrockBlock, new Vector3(bedrockXRight, rowY, 0), Quaternion.identity);

        }

        layerOffset += layerOneHeight;

        // Calculate bottom row y
        float bottomRowY = -blockSize * layerOffset - (blockSize / 2);

        // Make Bottom Bedrock layer
        for (int count = -halfMapWidth; count <= halfMapWidth; count++)
        {
            Instantiate(bedrockBlock, new Vector3(count * blockSize, bottomRowY, 0), Quaternion.identity);
        }
    }

    /// <summary>
    /// Increment the collected ore that was just destroyed
    /// </summary>
    /// <param name="tag"></param>
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

    /// <summary>
    /// Method to toggle the pause menu and the timescale.kj
    /// </summary>    
    public void TogglePause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pausedMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pausedMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Toggle to the next camera, possibly to the stock graphs or gameplay. 
    /// </summary>
    private void SwitchCamera()
    {
        currentCamIndex++;

        if (currentCamIndex < cams.Length)
        {
            cams[currentCamIndex - 1].enabled = false;
            cams[currentCamIndex].enabled = true;
        }
        else
        {
            cams[currentCamIndex - 1].enabled = false;
            currentCamIndex = 0;
            cams[currentCamIndex].enabled = true;
        }
    }

}
