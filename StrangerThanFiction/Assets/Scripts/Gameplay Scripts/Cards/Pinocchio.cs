using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class Pinocchio : CardModel
{
    public override string Title => "Pinnochio";
    public override string Description => "";
    public override string FlavorText => base.FlavorText;
    public override CardType Type => CardType.Unit;

    public override string PortraitPath => base.PortraitPath;
    public override string CardbackPath => base.CardbackPath;

    public override uint BaseCost => 3;
    public override uint BaseDepth => 3;
    public override uint BasePlotArmor => 3;

    public override uint CurrentCost { get => base.CurrentCost; set => base.CurrentCost = value; }
    public override uint CurrentDepth { get => base.CurrentDepth; set => base.CurrentDepth = value; }
    public override uint CurrentPlotArmor { get => base.CurrentPlotArmor; set => base.CurrentPlotArmor = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override Task Play(Player player)
    {
        return base.Play(player);
    }

    public override Task Discard(Player player)
    {
        return base.Discard(player);
    }
}
