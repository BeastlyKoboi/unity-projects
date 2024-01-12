using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.TextCore.Text;
using UnityEngine;

/// <summary>
/// My thinking is that all player input will be filtered in from here, human or AI. 
/// The player's UI will have access to their player and the AI will have access 
/// to their players methods behind the scenes. Upon player activity the game manager 
/// will prompt and manage the actions (somehow). 
/// </summary>

public class Player : MonoBehaviour
{
    public static event Action OnUnitSummoned;
    public static event Action OnCardPlayed;

    [HeaderAttribute("Enemy Info")]
    public Player enemyPlayer;

    [HeaderAttribute("Game State Info")]
    public int maxMana = 5;
    public int currentMana = 5;
    public int actWins = 0;
    public int totalDepth = 0;
    public bool hasEndedTurn = false;

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
        while (handManager.selectedCard == null)
        {

            await Task.Yield();
        }

        Debug.Log("Card Selected");
    }

    public void PlayCard(CardModel card)
    {
        Debug.Log("Card Played: ${0}", card);
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

        return cardScript;
    }

}
