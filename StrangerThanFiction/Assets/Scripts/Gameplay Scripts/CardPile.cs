using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public CardModel[] ToArray()
    {
        return cards.ToArray();
    }

    public void ForEach(Action<CardModel> action)
    {
        cards.ForEach(action);
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

        OnChange?.Invoke();
        OnCardRemoved?.Invoke();
    }
    public void Shuffle()
    {

        int n = cards.Count;
        System.Random rng = new System.Random();
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            CardModel value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }

        OnShuffle?.Invoke();
    }


}
