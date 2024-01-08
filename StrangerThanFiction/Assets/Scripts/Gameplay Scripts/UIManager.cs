using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator ActPopup;
    public Animator RoundPopup;

    public Player player1; // The human player
    public TextMeshProUGUI deckLabelPlayer1; 
    public TextMeshProUGUI discardLabelPlayer1;
    public Slider manaSliderPlayer1;
    public Slider winSliderPlayer1;

    public Player player2; // The AI eventually 
    public TextMeshProUGUI deckLabelPlayer2;
    public TextMeshProUGUI discardLabelPlayer2;
    public Slider manaSliderPlayer2;
    public Slider winSliderPlayer2;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateUI()
    {
        deckLabelPlayer1.text = $"Deck {player1.deck.Count}";
        discardLabelPlayer1.text = $"Disc {player1.discarded.Count}";
        manaSliderPlayer1.value = player1.currentMana;
        winSliderPlayer1.value = player1.actWins;

        deckLabelPlayer2.text = $"Deck {player2.deck.Count}";
        discardLabelPlayer2.text = $"Disc {player2.discarded.Count}";
        manaSliderPlayer2.value = player2.currentMana;
        winSliderPlayer2.value = player2.actWins;

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
}
