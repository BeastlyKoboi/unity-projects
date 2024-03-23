using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Donkey : CardModel
{
    public override string Title => "Donkey";
    public override string Description => "Whenever you summon another copy of me, grant me +1 power.";
    public override string FlavorText => base.FlavorText;
    public override CardType Type => CardType.Unit;
    public override string PortraitPath => "Assets/Textures/CardPortraits/Donkey.png";

    public override uint BaseCost => 1;
    public override uint BasePower => 1;
    public override uint BasePlotArmor => 1;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        OnPlay += () =>
        {
            
        };

        OnSummon += () =>
        {

        };

        
    }

}
