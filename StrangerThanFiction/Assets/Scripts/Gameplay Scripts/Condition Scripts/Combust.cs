using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combust : ICondition
{
    private readonly CardModel card;
    public int amount;

    public Combust(CardModel card, int amount)
    {
        this.card = card;
        this.amount = amount;
    }

    public static string GetName()
    {
        return "Combust";
    }

    public void OnAdd()
    {

    }
    public void OnTrigger()
    {
        
    }
    public void OnSurplus(ICondition surplus)
    {
        
    }
    public void OnRemove()
    {
        
    }
}
