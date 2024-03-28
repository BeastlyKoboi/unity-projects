using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// My thinking is that all player input will be filtered in from here, human or AI. 
/// The player's UI will have access to their player and the AI will have access 
/// to their players methods behind the scenes. Upon player activity the game manager 
/// will prompt and manage the actions (somehow). 
/// </summary>

public class Player : MonoBehaviour
{
    public event Action OnGameStart;
    public event Action OnRoundStart;
    public event Action OnRoundEnd;
    public event Action OnGameOver;

    public event Action<CardModel> OnUnitSummoned;
    public event Action<CardModel> OnCardPlayed;
    public event Action OnMyTurnStart;

    [HeaderAttribute("Game and Enemy Info")]
    public GameManager gameManager;
    public UIManager uiManager;
    public BoardManager board;
    public Player enemyPlayer;

    private int _maxMana = 5;
    public int MaxMana
    {
        get { return _maxMana; }
        set
        {
            _maxMana = value;
            uiManager.UpdateMana(this);
        }
    }
    private int _currentMana = 5;
    public int CurrentMana
    {
        get { return _currentMana; }
        set
        {
            _currentMana = value;
            uiManager.UpdateMana(this);
        }
    }

    [HeaderAttribute("Game State Info")]
    public int totalDepth = 0;
    public bool hasEndedTurn = false;
    public bool hasEndedRound = false;

    [HeaderAttribute("The Cards")]
    public HandManager handManager;
    public CardPile Deck { get; set; }
    public GameObject deckGameObject;

    public CardPile Discard { get; set; }
    public GameObject discardGameObject;

    [HeaderAttribute("Card Prefabs")]
    public GameObject cardPrefab;
    public GameObject unitPrefab;

    // Start is called before the first frame update
    void Start()
    {
        OnRoundStart += handManager.RoundStart;

    }

    public void PopulateDeck(string[] cards, bool isHidden)
    {
        Deck = new CardPile();
        //Deck.OnChange += UpdateDeck;
        Deck.OnChange += () =>
        {
            uiManager.UpdateDeck(this);
        };

        foreach (string card in cards)
        {
            Deck.Add(CreateCard(card, isHidden, deckGameObject));
        }

        Deck.Shuffle();

        Discard = new CardPile();
        Discard.OnChange += () =>
        {
            uiManager.UpdateDiscard(this);
        };
    }

    public async Task PlayerTurn()
    {
        hasEndedTurn = false;

        handManager.RefreshPlayableCards();

        OnMyTurnStart?.Invoke();

        handManager.RefreshPlayableCards();

        while (handManager.playedCard == null && !hasEndedTurn)
        {
            await Task.Yield();
        }

        if (handManager.playedCard)
        {
            Debug.Log("Card Played");
            await PlayCard(handManager.playedCard);

            uiManager.UpdateTotalPower();
        }

        handManager.LockCards();
    }

    public void DrawCard()
    {
        if (Deck.Count == 0)
            ShuffleDiscardIntoDeck();

        if (Deck.Count == 0)
            return;

        CardModel drawnCard;
        drawnCard = Deck[Deck.Count - 1];
        Deck.RemoveAt(Deck.Count - 1);
        handManager.AddCardToHandFromDeck(drawnCard);
    }

    public void DiscardCard(CardModel card)
    {
        handManager.RemoveCardFromHand(card);
        Discard.Add(card);
        card.gameObject.transform.SetParent(discardGameObject.transform, true);
        RectTransform cardRect = card.gameObject.GetComponent<RectTransform>();
        cardRect.anchoredPosition = new Vector2(0, 0);
        cardRect.rotation = Quaternion.identity;
    }

    public async void DestroyCard(CardModel card)
    {
        handManager.RemoveCardFromHand(card);
        await card.Destroy();
    }

    public void PassTurn()
    {
        hasEndedTurn = true;
    }

    public bool CanDoSomething()
    {
        bool actionsLeft = true;

        handManager.RefreshPlayableCards();

        if (handManager.NumPlayableCards == 0)
        {
            actionsLeft = false;
        }

        return actionsLeft;
    }

    private async Task PlayCard(CardModel card)
    {
        OnCardPlayed?.Invoke(card);

        await card.Play(this);

        if (card.Type == CardType.Unit)
            handManager.RemoveCardFromHand(card);
        else
        {
            if (card.HasCondition(Combust.GetName())) 
                DestroyCard(card);
            else 
                DiscardCard(card);
        }

        handManager.playedCard = null;
    }

    private void ShuffleDiscardIntoDeck()
    {
        for (int i = 0; i < Discard.Count; i++)
        {
            Deck.Add(Discard[i]);
            Discard[i].gameObject.transform.SetParent(deckGameObject.transform, true);
            Discard[i].GetComponent<RectTransform>().anchoredPosition = new Vector2();
        }

        Discard.Clear();
        Deck.Shuffle();
    }

    public void AddCardToDeck()
    {

    }

    public void RoundStart()
    {
        OnRoundStart?.Invoke();
    }

    public void UnitSummoned(CardModel unit)
    {
        OnUnitSummoned?.Invoke(unit);
    }

    public CardModel CreateCard(string cardName, bool isHidden, GameObject parent, string creator = "")
    {
        Type MyScriptType = System.Type.GetType(cardName);

        GameObject cardObj = new GameObject(cardName, typeof(RectTransform));
        cardObj.transform.SetParent(parent.transform, false);

        cardObj.AddComponent(MyScriptType);

        Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity).transform.SetParent(cardObj.transform, false);

        CardModel cardScript = cardObj.GetComponent<CardModel>();
        cardScript.IsHidden = isHidden;
        cardScript.Owner = this;
        cardScript.Board = board;

        cardScript.OverwriteCardPrefab();

        if (cardScript.Type == CardType.Unit)
        {
            Instantiate(unitPrefab, new Vector3(0, 0, 0), Quaternion.identity).transform.SetParent(cardObj.transform, false);
            cardScript.OverwriteUnitPrefab();
        }

        return cardScript;
    }

}
