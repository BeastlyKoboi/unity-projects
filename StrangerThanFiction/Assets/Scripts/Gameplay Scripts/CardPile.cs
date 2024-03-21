using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CardPile
{
    public List<CardModel> cards = new List<CardModel>();

    public event Action OnChange;
    public event Action OnCardAdded;
    public event Action OnCardRemoved;
    public event Action OnShuffle;
    public CardModel this[int index]
    {
        // get and set accessors
        get => cards[index];
        set => cards[index] = value;
    }

    public int Count 
    {
        get { return cards.Count; }
    }

    public void Add(CardModel card)
    {
        cards.Add(card);

        OnChange?.Invoke();
        OnCardAdded?.Invoke();
    }
    public void Insert(int index, CardModel card)
    {
        cards.Insert(index, card);

        OnChange?.Invoke();
        OnCardAdded?.Invoke();
    }
    public void Remove(CardModel card)
    {
        cards.Remove(card);

        OnChange?.Invoke();
        OnCardRemoved?.Invoke();
    }
    public void RemoveAt(int index)
    {
        cards.RemoveAt(index);

        OnChange?.Invoke();
        OnCardRemoved?.Invoke();
    }
    public void Clear()
    {
        cards.Clear();
    }
    public void Shuffle()
    {

        OnShuffle?.Invoke();
    }


}
