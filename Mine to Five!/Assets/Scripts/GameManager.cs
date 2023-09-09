using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // The script attached to the player
    [SerializeField] private PlayerControl playerControl;

    //-- Block Prefabs --//
    // Grass Block
    [SerializeField] private GameObject bedrockBlock;
    [SerializeField] private GameObject grassBlock;
    [SerializeField] private GameObject dirtBlock;
    [SerializeField] private GameObject coalBlock;
    [SerializeField] private GameObject copperBlock;

    // Map & Block Info
    private int halfMapWidth = 15;
    float blockSize = 2 * 0.64f;

    // Layer Depths
    private int grassHeight = 1;
    private int layerOneHeight = 10;
    private int layerTwoHeight = 10;
    private OreThresholds oreThresholds = new OreThresholds(0.7f, 0.9f, 0.95f);

    // Collections of created blocks, to destroy upon resets
    [SerializeField]
    private List<GameObject> mapBlocks = new List<GameObject>();

    // Gameplay UI
    private int currentHour = 9;
    private float currentTimePassed = 0.0f;
    private int hourIntervals = 5;
    [SerializeField] private TextMeshProUGUI workTimeText;
    private int coalOre = 0;
    [SerializeField] private TextMeshProUGUI coalText;
    private int copperOre = 0;
    [SerializeField] private TextMeshProUGUI copperText;

    [SerializeField] private TextMeshProUGUI playerCashText;

    // Paused UI
    [SerializeField] private GameObject pausedMenu;
    [SerializeField] private GameObject depotMenu;
    [SerializeField] private GameObject altarMenu;

    //
    [SerializeField] private InputActionAsset actions;
    private InputAction pauseAction; 

    // Gameplay Variables
    [SerializeField] private float playerCash;

    public float PlayerCash
    {
        get { return playerCash; }
        set 
        {
            playerCash = value;
            playerCashText.text = "$" + playerCash;
        }
    }

    private void Awake()
    {
        pauseAction = actions.FindActionMap("Mining").FindAction("pause");
    }

    // Start is called before the first frame update
    void Start()
    {
        pausedMenu.SetActive(false);

        CreateMap();
    }

    // Update is called once per frame
    void Update()
    {
        bool paused = pauseAction.WasReleasedThisFrame();

        if (paused)
        {
            TogglePause();
        }

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
                // Work day ends
            }

            workTimeText.text = currentHour + ":00";
        }


    }

    /// <summary>
    /// Automatically generates the map to begin the game with several different layers of ore rarity.
    /// </summary>
    private void CreateMap()
    {
        int layerOffset = 0;

        // Calculate left and right bedrock
        float bedrockXLeft = -(halfMapWidth + 1) * blockSize;
        float bedrockXRight = (halfMapWidth + 1) * blockSize;

        // Add grass layer left bedrock
        Instantiate(bedrockBlock, new Vector3(bedrockXLeft, -blockSize / 2, 0), Quaternion.identity);

        // Make grass layer
        for (int count = -halfMapWidth; count <= halfMapWidth; count++)
        {
            GameObject block = Instantiate(grassBlock, new Vector3(count * blockSize, -blockSize / 2, 0), Quaternion.identity);

        }

        // Add grass layer right bedrock
        Instantiate(bedrockBlock, new Vector3(bedrockXRight, -blockSize / 2, 0), Quaternion.identity);

        layerOffset += grassHeight;

        // Make first layer of ore
        InitializeLayer(layerOffset, layerOneHeight);

        layerOffset += layerOneHeight;

        // Calculate bottom row y
        float bottomRowY = -blockSize * layerOffset - (blockSize / 2);

        // Make Bottom Bedrock layer
        for (int count = -halfMapWidth; count <= halfMapWidth; count++)
        {
            Instantiate(bedrockBlock, new Vector3(count * blockSize, bottomRowY, 0), Quaternion.identity);
        }
    }

    // Layer y start, layer length, ore chances,  
    private void InitializeLayer(int layerOffset, int layerDepth) 
    {
        // Plan 
        // CreateMap will...
        // Bring up loading screen 
        // Delete all previous destructable blocks if any.
        // run InitializeLayer() on all layers needed
        // run InitializeBottomBedrock() 
        // Brong Down loading screen

        // InitializeLayer() will...
        // 

        // Calculate left and right bedrock
        float bedrockXLeft = -(halfMapWidth + 1) * blockSize;
        float bedrockXRight = (halfMapWidth + 1) * blockSize;

        float chance = 0.0f;
        GameObject blockPrefab = dirtBlock;

        // Make first layer of ore
        for (int row = 0; row < layerDepth; row++)
        {
            float rowY = -blockSize * (layerOffset + row) - (blockSize / 2);

            // Add Bedrock Here
            Instantiate(bedrockBlock, new Vector3(bedrockXLeft, rowY, 0), Quaternion.identity);

            for (int count = -halfMapWidth; count <= halfMapWidth; count++)
            {
                chance = Random.value;

                if (chance < oreThresholds.Dirt)
                    blockPrefab = dirtBlock;
                else if (chance < oreThresholds.Coal)
                    blockPrefab = coalBlock; 
                else if (chance < oreThresholds.Copper)
                    blockPrefab = copperBlock;

                mapBlocks.Add(Instantiate(blockPrefab, new Vector3(count * blockSize, rowY, 0), Quaternion.identity));
            }

            // Add Bedrock here
            Instantiate(bedrockBlock, new Vector3(bedrockXRight, rowY, 0), Quaternion.identity);

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
            case "Copper":
                copperOre++;
                copperText.text = copperOre.ToString();
                break;

            default: 
                break; 
        }
    }

    ///
    public void DecrementOre(string tag, int quantity)
    {
        switch (tag)
        {
            case "Coal":
                coalOre -= quantity;
                coalText.text = coalOre.ToString();
                break;
            case "Copper":
                copperOre -= quantity;
                copperText.text = copperOre.ToString();
                break;

            default:
                break;
        }
    }

    //
    public void SellOre(int quantity)
    {
        if (coalOre >= quantity)
        {
            DecrementOre("Coal", quantity);
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

    // 
    public void ToggleDepotMenu()
    {
        depotMenu.SetActive(!depotMenu.activeInHierarchy); 
    }

    public void ToggleAltarMenu()
    {
        altarMenu.SetActive(!altarMenu.activeInHierarchy);
    }

}
