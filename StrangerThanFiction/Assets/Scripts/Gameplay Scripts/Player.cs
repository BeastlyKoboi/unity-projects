using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.TextCore.Text;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

/// <summary>
/// My thinking is that all player input will be filtered in from here, human or AI. 
/// The player's UI will have access to their player and the AI will have access 
/// to their players methods behind the scenes. Upon player activity the game manager 
/// will prompt and manage the actions (somehow). 
/// </summary>

public class Player : MonoBehaviour
{
    public static event Action<CardModel> OnUnitSummoned;
    public static event Action OnCardPlayed;
    public event Action OnMyTurnStart;

    [HeaderAttribute("Game and Enemy Info")]
    public GameManager gameManager;
    public UIManager uiManager;
    public BoardManager board;
    public Player enemyPlayer;

    [HeaderAttribute("Game State Info")]
    private uint _maxMana = 5;
    public uint MaxMana
    {
        get { return _maxMana; }
        set 
        { 
            _maxMana = value;
            uiManager.UpdateMana(this);
        }
    }
    private uint _currentMana = 5;
    public uint CurrentMana
    {
        get { return _currentMana; }
        set
        {
            _currentMana = value;
            uiManager.UpdateMana(this);
        }
    }

    private uint _actWins = 0;
    public uint ActWins
    {
        get { return _actWins; }
        set
        {
            _actWins = value;
            uiManager.UpdateActWins(this);
        }
    }

    public uint totalDepth = 0;
    public bool hasEndedTurn = false;
    public bool hasEndedRound = false;

    [HeaderAttribute("The Cards")]
    // Eventually replace this with a deck manager
    public List<CardModel> deck;
    public GameObject deckGameObject;
    public HandManager handManager;
    public List<CardModel> discarded;

    [HeaderAttribute("Card Prefabs")]
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

    public void PopulateDeck(string[] cards, bool isHidden)
    {
        deck = new List<CardModel>();
        foreach (string card in cards)
        {
            deck.Add(CreateCard(card, isHidden));
        }
    }

    public async Task PlayerTurn()
    {
        OnMyTurnStart?.Invoke();

        while (handManager.playedCard == null)
        {
            await Task.Yield();
        }

        Debug.Log("Card Played");
        await PlayCard(handManager.playedCard);

    }

    public void DrawCard()
    {
        if (deck.Count > 0)
        {
            CardModel drawnCard;
            drawnCard = deck[deck.Count - 1];
            deck.RemoveAt(deck.Count - 1);
            handManager.AddCardToHandFromDeck(drawnCard);
        }
        else
        {
            ShuffleDiscardIntoDeck();
        }
    }

    public bool CanDoSomething()
    {
        bool actionsLeft = true;

        if (handManager.NumPlayableCards == 0)
        {
            actionsLeft = false;
        }

        return actionsLeft;
    }

    private async Task PlayCard(CardModel card)
    {
        await card.Play(this);
        handManager.RemoveCardFromHand(card);
    }

    private void ShuffleDiscardIntoDeck()
    {

    }

    private CardModel CreateCard(string cardName, bool isHidden, string creator = "") 
    {
        Type MyScriptType = System.Type.GetType(cardName + ",Assembly-CSharp");

        GameObject cardObj = new GameObject(cardName, typeof(RectTransform));
        cardObj.transform.SetParent(deckGameObject.transform, false);

        cardObj.AddComponent(MyScriptType);

        Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity).transform.SetParent(cardObj.transform, false);

        CardModel cardScript = cardObj.GetComponent<CardModel>();
        cardScript.IsHidden = isHidden;
        cardScript.Owner = this;
        cardScript.Board = board;

        if (cardScript.Type == CardType.Unit) 
            Instantiate(unitPrefab, new Vector3(0, 0, 0), Quaternion.identity).transform.SetParent(cardObj.transform, false);

        return cardScript;
    }



}
