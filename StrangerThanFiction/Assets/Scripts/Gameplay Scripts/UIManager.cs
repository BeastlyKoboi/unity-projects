using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HeaderAttribute("Game State")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BoardManager board;

    [HeaderAttribute("Animators")]
    [SerializeField] private Animator ActPopup;
    [SerializeField] private Animator RoundPopup;

    [HeaderAttribute("Player 1")]
    [SerializeField] private Player player1; // The human player
    [SerializeField] private TextMeshProUGUI deckLabelPlayer1;
    [SerializeField] private TextMeshProUGUI discardLabelPlayer1;
    [SerializeField] private TextMeshProUGUI manaPlayer1;
    [SerializeField] private TextMeshProUGUI powerPlayer1;
    [SerializeField] private TextMeshProUGUI frontPowerPlayer1;
    [SerializeField] private TextMeshProUGUI backPowerPlayer1;

    [HeaderAttribute("Player 2")]
    [SerializeField] private Player player2; // The AI eventually 
    [SerializeField] private TextMeshProUGUI deckLabelPlayer2;
    [SerializeField] private TextMeshProUGUI discardLabelPlayer2;
    [SerializeField] private TextMeshProUGUI manaPlayer2;
    [SerializeField] private TextMeshProUGUI powerPlayer2;
    [SerializeField] private TextMeshProUGUI frontPowerPlayer2;
    [SerializeField] private TextMeshProUGUI backPowerPlayer2;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void RoundStart(uint roundNum)
    {
        RoundPopup.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = $"Round {roundNum}";
        RoundPopup.SetTrigger("Popup");
    }

    public async void GameStart()
    {

    }

    public async void GameOver()
    {

    }

    public void UpdateDeck(Player player)
    {
        if (player == player1)
            deckLabelPlayer1.text = $"Deck {player1.Deck.Count}";
        else if (player == player2)
            deckLabelPlayer2.text = $"Deck {player2.Deck.Count}";
    }
    public void UpdateDiscard(Player player)
    {
        if (player == player1)
            discardLabelPlayer1.text = $"Disc {player1.Discard.Count}";
        else if (player == player2)
            discardLabelPlayer2.text = $"Disc {player2.Discard.Count}";
    }

    public void UpdateMana(Player player)
    {
        if (player == player1)
            manaPlayer1.text = $"{player1.CurrentMana}/{player1.MaxMana}";
        else if (player == player2)
            manaPlayer2.text = $"{player2.CurrentMana}/{player2.MaxMana}";

    }

    public void UpdateTotalPower()
    {
        uint frontPower = board.GetTotalFrontPower(gameManager.player1);
        uint backPower = board.GetTotalBackPower(gameManager.player1);

        powerPlayer1.text = (frontPower + backPower).ToString();
        frontPowerPlayer1.text = frontPower.ToString();
        backPowerPlayer1.text = backPower.ToString();

        frontPower = board.GetTotalFrontPower(gameManager.player2);
        backPower = board.GetTotalBackPower(gameManager.player2);

        powerPlayer2.text = (frontPower + backPower).ToString();
        frontPowerPlayer2.text = frontPower.ToString();
        backPowerPlayer2.text = backPower.ToString();
    }

}
