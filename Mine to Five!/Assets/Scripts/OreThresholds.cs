using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OreThresholds
{
    public float Dirt { get; set; }
    public float Coal { get; set; }
    public float Copper { get; set; }


    public OreThresholds(float _dirt, float _coal, float _copper)
    {
        Dirt = _dirt;
        Coal = _coal;
        Copper = _copper;
    }

    public override string ToString()
    {
        return $@"Dirt Threshold: {Dirt}, 
            Coal Threshold: {Coal}, 
            Copper Threshold: {Copper} ";

    }
}
