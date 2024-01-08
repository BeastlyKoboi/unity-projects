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

    public Player player1; // The human player
    public Player player2; // The AI eventually 

    public UIManager uiManager;
    public BoardManager boardManager;

    public uint roundNumber = 0;
    public uint actNumber = 0;
    
    public enum GameState { Player1Turn, Player2Turn }

    [SerializeField] private TextAsset StarterDecksJSON;
    public string[] Pinocchio;
    public string[] TheBigBadWolf;

    // Something for battlefield conditions in each act

    

    // Start is called before the first frame update
    void Start()
    {
        // initialize all needed stuff for beginning of game 

        JsonUtility.FromJsonOverwrite(StarterDecksJSON.text, this);

        player1.PopulateDeck(Pinocchio);

        // Call on game start 
        StartGame();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // in game start method, after run, call round start (game start should select who goes first.)
    // after round start, ping pong between player until both decide to end the round/or cannot do anything.
    // maybe in a round activity method with a do while 
    // should call round end (increment round num and then act stuff if necessary), which should chain to round start
    // completing the loop, until during round end win condition is met, then call game end instead. 

    private void StartGame()
    {
        // Setup 
        

        GameLoop();
    }

    private async void GameLoop()
    {

        do
        {
            actNumber++;
            await ActActivity();

            // shuffle discard into deck
            
        } while (actNumber != 3 || player1.actWins == 2 || player2.actWins == 2);

        EndGame();
    }

    private async Task ActActivity()
    {
        uiManager.ActStart(actNumber);

        do
        {
            roundNumber++;
            await RoundActivity();

        } while (roundNumber % 3 != 0);

        // Decide who has most power then grant win to player

    }

    private async Task RoundActivity()
    {
        uiManager.RoundStart(roundNumber);

        // Draw Cards

        do
        {


            await player1.PlayerTurn();

            await player2.PlayerTurn();

            await Task.Yield();

        } while (!(player1.hasEndedTurn && player2.hasEndedTurn));

        // 
    }

    private void EndGame()
    {

    }

}
