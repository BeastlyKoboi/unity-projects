using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public List<CardModel> hand;
    public List<CardModel> nonInteractiveCards;

    public Vector2 handBounds;
    public int rotationBounds;
    public int cardGap;
    public Vector2 handCenter;

    public CardModel hoveredCard; 
    public CardModel selectedCard;

    // Start is called before the first frame update
    void Start()
    {
        hand = new List<CardModel>();
        nonInteractiveCards = new List<CardModel>();

        handBounds.x = 500;
        handBounds.y = 20;
        rotationBounds = 20;
        cardGap = 100;
        handCenter.x = 0;
        handCenter.y = -500;
    }

    private void Update()
    {

    }

    public void AddCardToHandFromDeck(CardModel card)
    {
        card.AddComponent<Appear>();
        card.AddComponent<Hoverable>();
        card.AddComponent<Draggable>();
        card.transform.SetParent(transform);
        hand.Insert(0, card);
        UpdateTargetTransforms();

        // Add hover and drag scripts 
    }

    public void RemoveCardFromHand(CardModel card)
    {
        hand.Remove(card);
        // remove hover and drag scripts
    }

    private void UpdateTargetTransforms()
    {
        float startingXPos = -(hand.Count + 1) * cardGap / 2;
        Vector2 cardPos = new Vector2(startingXPos, handCenter.y - handBounds.y);
        float startingRot = rotationBounds;
        int middleCardIndex = (int)Math.Ceiling(hand.Count / 2.0f);
        float yPosIncrement = (2 * handBounds.y) / middleCardIndex;

        for (int cardIndex = 0; cardIndex < hand.Count; cardIndex++)
        {
            if (cardIndex < middleCardIndex)
            {
                cardPos += new Vector2(cardGap, yPosIncrement);
            }
            else
            {
                cardPos += new Vector2(cardGap, -yPosIncrement);
            }

            startingRot -= 2 * rotationBounds / hand.Count;

            hand[cardIndex].GetComponent<Appear>().RefreshTarget(cardPos, startingRot);
        }

    }

    /// <summary>
    /// This will eventually be called every time an action is 
    /// taken that can change whether a card is playable. It should 
    /// check each card's play requirements and make sure that they are 
    /// met, and if not disable their draggable component. 
    /// </summary>
    public void RefreshPlayableCards()
    {

    }

}
