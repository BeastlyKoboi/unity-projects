using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemyAI_V1 : MonoBehaviour
{
    public Player myPlayer;

    public Player opponent; // human player

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GetComponent<Player>();
        myPlayer.OnMyTurnStart += PlayTurn;

    }

    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayTurn()
    {
        Debug.Log("Enemy Turn PlayTurn is called.");

        CardModel cardToPlay = myPlayer.handManager.Hand[0];

        for (int cardIndex = 0; cardIndex < myPlayer.handManager.Hand.Count; cardIndex++)
        {
            if (myPlayer.handManager.Hand[cardIndex].Playable)
            {
                cardToPlay = myPlayer.handManager.Hand[cardIndex];
                break;
            }
        }

        if (cardToPlay.Type == CardType.Unit)
        {
            cardToPlay.SelectedArea = cardToPlay.Board.GetRandomEnemyRow();
        }

        myPlayer.handManager.playedCard = cardToPlay;

    }
}
