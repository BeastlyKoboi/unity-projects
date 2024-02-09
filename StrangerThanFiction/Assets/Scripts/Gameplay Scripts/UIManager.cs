using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HeaderAttribute("Animators")]
    [SerializeField] private Animator ActPopup;
    [SerializeField] private Animator RoundPopup;

    [HeaderAttribute("Player 1")]
    [SerializeField] private Player player1; // The human player
    [SerializeField] private TextMeshProUGUI deckLabelPlayer1;
    [SerializeField] private TextMeshProUGUI discardLabelPlayer1;
    [SerializeField] private TextMeshProUGUI manaPlayer1;
    [SerializeField] private TextMeshProUGUI winsPlayer1;

    [HeaderAttribute("Player 2")]
    [SerializeField] private Player player2; // The AI eventually 
    [SerializeField] private TextMeshProUGUI deckLabelPlayer2;
    [SerializeField] private TextMeshProUGUI discardLabelPlayer2;
    [SerializeField] private TextMeshProUGUI manaPlayer2;
    [SerializeField] private TextMeshProUGUI winsPlayer2;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ActStart(uint actNum)
    {
        ActPopup.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = $"Act {actNum}";
        ActPopup.SetTrigger("Popup");
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
            deckLabelPlayer1.text = $"Deck {player1.deck.Count}";
        else if (player == player2)
            deckLabelPlayer2.text = $"Deck {player2.deck.Count}";
    }
    public void UpdateDiscard(Player player)
    {
        if (player == player1)
            discardLabelPlayer1.text = $"Disc {player1.discarded.Count}";
        else if (player == player2)
            discardLabelPlayer2.text = $"Disc {player2.discarded.Count}";

    }

    public void UpdateMana(Player player)
    {
        if (player == player1)
            manaPlayer1.text = $"{player1.CurrentMana}/{player1.MaxMana}";
        else if (player == player2)
            manaPlayer2.text = $"{player2.CurrentMana}/{player2.MaxMana}";

    }
    public void UpdateActWins(Player player)
    {
        if (player == player1)
            winsPlayer1.text = $"{player1.ActWins}";
        else if (player == player2)
            winsPlayer2.text = $"{player2.ActWins}";

    }
}
