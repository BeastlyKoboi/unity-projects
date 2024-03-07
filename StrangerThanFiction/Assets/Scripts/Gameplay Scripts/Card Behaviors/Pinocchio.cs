using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class Pinocchio : CardModel
{
    public override string Title => "Pinnochio";
    public override string Description => "Every turn, create a Tall Tale in your hand.";
    public override string FlavorText => base.FlavorText;
    public override CardType Type => CardType.Unit;
    public override string PortraitPath => "Assets/Textures/CardPortraits/Pinocchio.png";

    public override uint BaseCost => 2;
    public override uint BasePower => 3;
    public override uint BasePlotArmor => 3;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

}
