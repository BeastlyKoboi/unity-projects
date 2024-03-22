using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Manages the flow of game in a single match. 
/// </summary>
public class GameManager : MonoBehaviour
{
    // Basic gameplay events that objects can add to
    public static event Action OnGameStart;
    public static event Action OnRoundStart;
    public static event Action OnRoundEnd;
    public static event Action OnGameOver;

    [HeaderAttribute("The Players")]
    public Player player1; // The human player
    public Player player2; // The AI eventually 

    [HeaderAttribute("Managers")]
    public UIManager uiManager;
    public BoardManager boardManager;

    [HeaderAttribute("Game State Information")]
    public uint roundNumber = 0;
    public uint totalRounds = 9;

    [HeaderAttribute("Text Assets")]
    [SerializeField] private TextAsset StarterDecksJSON;
    public string[] Pinocchio;
    public string[] TheBigBadWolf;

    // Something for battlefield conditions
    //  - 


    // Start is called before the first frame update
    void Start()
    {
        // initialize all needed stuff for beginning of game 

        JsonUtility.FromJsonOverwrite(StarterDecksJSON.text, this);

        player1.PopulateDeck(Pinocchio, false);
        player2.PopulateDeck(Pinocchio, true);

        // Call on game start 
        StartGame();

    }

    /// <summary>
    /// Method to trigger the game loop.
    /// </summary>
    private void StartGame()
    {
        // Setup 


        GameLoop();
    }

    /// <summary>
    /// Method to proceed through the rounds, and confirm a winner. 
    /// </summary>
    private async void GameLoop()
    {
        await Task.Delay(1000);

        do
        {
            roundNumber++;
            await RoundActivity();

        } while (roundNumber != totalRounds);

        // Decide who has most power then grant win to player

        EndGame();
    }

    /// <summary>
    /// Method to go over the activity of a single round. 
    /// </summary>
    /// <returns></returns>
    private async Task RoundActivity()
    {
        uiManager.RoundStart(roundNumber);

        OnRoundStart?.Invoke();

        player1.CurrentMana = 0;
        player1.CurrentMana += player1.MaxMana;
        player2.CurrentMana = 0;
        player2.CurrentMana += player2.MaxMana;


        await DrawHands(); // ITF maybe put this in event with numCards to draw as a variable

        // Draw Cards
        do
        {
            await Task.Delay(1000);

            if (player1.CanDoSomething())
                await player1.PlayerTurn();

            await Task.Delay(1000);

            if (player2.CanDoSomething())
                await player2.PlayerTurn();

            await Task.Yield();

        } while (player1.CanDoSomething() || player2.CanDoSomething());
        // 

        await DiscardHands();

        OnRoundEnd?.Invoke();
    }

    private async Task DrawHands(int numCards = 5)
    {
        for (int i = 0; i < numCards; i++)
        {
            player1.DrawCard();

            player2.DrawCard();

            await Task.Delay(500);
        }

        // Draw additional cards here
    }

    private async Task DiscardHands()
    {
        int cardCount = player1.handManager.Hand.Count;
        CardModel[] cards = player1.handManager.Hand.ToArray();

        for (int i = 0; i < cards.Length; i++)
        {
            player1.DiscardCard(cards[i]);
        }

        cards = player2.handManager.Hand.ToArray();
        for (int i = 0; i < cards.Length; i++)
        {
            player2.DiscardCard(cards[i]);
        }

        await Task.Delay(2000);
    }

    private void EndGame()
    {

    }
}
