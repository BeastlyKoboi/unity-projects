using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBlueFairy : CardModel
{
    public override string Title => "The Blue Fairy";
    public override string Description => "When you play a card with a cost reduction, draw a card. ";
    public override string FlavorText => base.FlavorText;
    public override CardType Type => CardType.Unit;
    public override string PortraitPath => "CardPortraits/The_Blue_Fairy.png";

    public override int BaseCost => 2;
    public override int BasePower => 2;
    public override int BasePlotArmor => 1;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        Owner.OnCardPlayed += (card) =>
        {
            if (card.BaseCost > card.CurrentCost)
            {
                card.Owner.DrawCard();
            }
        };
    }
}
