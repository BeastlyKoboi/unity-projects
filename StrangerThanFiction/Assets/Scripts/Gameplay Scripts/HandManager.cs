using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public List<CardModel> hand;
    public List<CardModel> nonInteractiveCards;

    public List<Vector3> targetPositions;
    public List<Vector3> targetRotations;

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
        targetPositions = new List<Vector3>();
        targetRotations = new List<Vector3>();

        handBounds.x = 500;
        handBounds.y = 20;
        rotationBounds = 20;
        cardGap = 100;
        handCenter.x = 0;
        handCenter.y = -500;
    }

    private void Update()
    {
        if (nonInteractiveCards.Count != 0)
        {
            for (int i = 0; i < nonInteractiveCards.Count; i++)
            {
                //Vector3 direction = targetPositions[i] - nonInteractiveCards[i].gameObject.transform.position;
                //direction.Normalize();

                //nonInteractiveCards[i].gameObject.transform.position += 
                //    direction * 100 * Time.deltaTime;

                int cardIndex = hand.IndexOf(nonInteractiveCards[i]);

                RectTransform rect = hand[cardIndex].gameObject.GetComponent<RectTransform>();

                rect.anchoredPosition += (new Vector2(targetPositions[cardIndex].x, targetPositions[cardIndex].y) - rect.anchoredPosition) * 1 * Time.deltaTime;

                if (Vector3.Distance(hand[cardIndex].transform.position, targetPositions[cardIndex]) < .001f)
                {
                    nonInteractiveCards.RemoveAt(i);
                    // targetPositions.RemoveAt(i);
                    // targetRotations.RemoveAt(i);
                }
            }
        }
    }

    public void AddCardToHandFromDeck(CardModel card)
    {
        Debug.Log(hand.Count);
        hand.Add(card);
        targetPositions.Add(new Vector3());
        targetRotations.Add(new Vector3());
        UpdateTargetTransforms();

        nonInteractiveCards.Add(card);
        // Add hover and drag scripts 
    }

    public void RemoveCardFromHand(CardModel card)
    {
        hand.Remove(card);
        // remove hover and drag scripts
    }

    private void UpdateTargetTransforms()
    {
        int numCards = hand.Count;

        float startingXPos = -(numCards + 1) * cardGap / 2;
        Vector3 cardPos = new Vector3(startingXPos, handCenter.y - handBounds.y, 0);
        float startingRot = rotationBounds;
        int middleCardIndex = (int)Math.Ceiling(hand.Count / 2.0f);
        float yPosIncrement = (2 * handBounds.y) / middleCardIndex;
        

        for (int cardIndex = 0; cardIndex < hand.Count; cardIndex++)
        {

            if (cardIndex < middleCardIndex)
            {
                cardPos += new Vector3(cardGap, yPosIncrement, 1);
            }
            else
            {
                cardPos += new Vector3(cardGap, -yPosIncrement, 1);
            }

            targetPositions[cardIndex] = cardPos;

            startingRot -= 2 * rotationBounds / hand.Count;
            targetRotations[cardIndex] = new Vector3(0, 0, startingRot);
        }

    }

    //private IEnumerator MoveCardFromDeckToHand(CardModel card)
    //{

    //}


}
