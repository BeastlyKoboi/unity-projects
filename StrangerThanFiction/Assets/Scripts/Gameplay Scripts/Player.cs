using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// My thinking is that all player input will be filtered in from here, human or AI. 
/// The player's UI will have access to their player and the AI will have access 
/// to their players methods behind the scenes. Upon player activity the game manager 
/// will prompt and manage the actions (somehow). 
/// </summary>

public class Player : MonoBehaviour
{
    public Player enemyPlayer;

    public static event Action OnUnitSummoned;
    public static event Action OnCardPlayed;

    // act wins, the deck, and the hand, 

    public int maxMana = 5;
    public int currentMana = 5;

    public int actWins = 0;

    public List<CardModel> deck;
    public HandManager handManager;
    public List<CardModel> discarded;

    public int totalDepth = 0;

    public bool hasEndedTurn = false;

    public GameObject cardPrefab;
    public GameObject unitPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateDeck(string[] cards)
    {
        deck = new List<CardModel>();
        foreach (string card in cards)
        {
            Type MyScriptType = System.Type.GetType(card + ",Assembly-CSharp");

            GameObject cardObj = new GameObject(card, typeof(RectTransform));
            cardObj.transform.SetParent(handManager.gameObject.transform, false);

            cardObj.AddComponent(MyScriptType);

            Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity).transform.SetParent(cardObj.transform, false);

            deck.Add(cardObj.GetComponent<CardModel>());
        }
    }

    public async Task PlayerTurn()
    {
        while (handManager.selectedCard == null)
        {

            await Task.Yield();
        }

        Debug.Log("Card Selected");
    }

    public async void DrawCard()
    {

    }


}
