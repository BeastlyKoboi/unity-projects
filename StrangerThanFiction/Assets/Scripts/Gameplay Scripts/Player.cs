using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public HandManager hand;
    public List<CardModel> discarded;

    public int totalDepth = 0;

    public bool hasEndedTurn = false;

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

            GameObject cardObj = new GameObject();
            cardObj.AddComponent(MyScriptType);
            Debug.Log(MyScriptType);
            deck.Add(cardObj.GetComponent<CardModel>());
        }
    }

    public async Task PlayerTurn()
    {
        while (hand.selectedCard == null)
        {

            await Task.Yield();
        }

        Debug.Log("Card Selected");
    }

    public async void DrawCard()
    {

    }


}