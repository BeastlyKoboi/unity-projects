using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GrowthSpurt : CardModel
{
    public override string Title => "Growth Spurt";
    public override string Description => "Grant your weakest unit +4 power.";
    public override string FlavorText => base.FlavorText;
    public override CardType Type => CardType.Spell;
    public override string PortraitPath => "CardPortraits/Affable_Nipper.png";

    public override int BaseCost => 2;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        OnPlay += () =>
        {
            CardModel weakestUnit = Board.GetWeakestUnit(Owner);

            if (weakestUnit)
            {
                weakestUnit.GrantPower(4);
            }
        };
    }
}
