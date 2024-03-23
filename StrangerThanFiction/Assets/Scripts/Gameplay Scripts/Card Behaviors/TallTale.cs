using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TallTale : CardModel
{
    public override string Title => "Tall Tale";
    public override string Description => "The top card on deck get -1 cost.";
    public override string FlavorText => base.FlavorText;
    public override CardType Type => CardType.Spell;
    public override string PortraitPath => "Assets/Textures/CardPortraits/Tall_Tale.png";

    public override uint BaseCost => 0;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        OnPlay += () =>
        {
            CardModel topUnit = Owner.Deck[^1];

            topUnit.CurrentCost -= 1;
        };
    }

}
