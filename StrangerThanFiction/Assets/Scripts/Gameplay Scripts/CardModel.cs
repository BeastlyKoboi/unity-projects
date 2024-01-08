using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public enum CardType { Unit, Spell }

public class CardModel : MonoBehaviour
{
    public virtual string Title { get; }
    public virtual string Description { get; }
    public virtual string FlavorText { get; }
    public virtual CardType Type { get; set; }

    public virtual string CardbackPath { get; }
    public virtual string PortraitPath { get; }

    // Add reqs for targeting ally/enemy units, and ally cards.

    public virtual uint BaseCost { get; }
    public virtual uint BaseDepth { get; }
    public virtual uint BasePlotArmor { get; }

    public virtual uint CurrentCost { get; set; }
    public virtual uint CurrentDepth { get; set; }
    public virtual uint CurrentPlotArmor { get; set; }

    public event Action OnPlay;
    public event Action OnSummon;
    public event Action OnDraw;
    public event Action OnDiscard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual async Task Play(Player player) 
    { 
        OnPlay?.Invoke();
    }

    public virtual async Task Discard(Player player)
    {
        OnDiscard?.Invoke();
    }
}
