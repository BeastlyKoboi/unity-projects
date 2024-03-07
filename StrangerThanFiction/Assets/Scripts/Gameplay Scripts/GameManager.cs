using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameStart;
    public static event Action OnRoundStart;
    public static event Action OnRoundEnd;
    public static event Action OnGameOver;
    // Also add some for act beginnings and ends

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

    private void StartGame()
    {
        // Setup 
        

        GameLoop();
    }

    private async void GameLoop()
    {
        do
        {
            roundNumber++;
            await RoundActivity();

        } while (roundNumber != totalRounds);

        // Decide who has most power then grant win to player

        EndGame();
    }

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

            await player1.PlayerTurn();

            await Task.Delay(1000);

            await player2.PlayerTurn();

            await Task.Yield();

        } while (player1.CanDoSomething() || player2.CanDoSomething());
        // 

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
    }

    private void EndGame()
    {

    }
}
