using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfo : MonoBehaviour
{
    [SerializeField]
    private float durabilityMax;
    private float durability;
    public SpriteRenderer blockRenderer;
    public List<Sprite> blockFrames;
    public int frame = 1;

    public float Durability
    {
        get
        {
            return durability;
        }
        set
        {            
            durability = value;

            if (durability < durabilityMax - frame * (durabilityMax / blockFrames.Count))
            {
                NextSpriteFrame();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (durabilityMax == 0)
            durabilityMax = 100f;

        durability = durabilityMax;

        blockRenderer = GetComponent<SpriteRenderer>();
    }

    // 
    private void NextSpriteFrame()
    {
        if (frame < blockFrames.Count)
        {
            blockRenderer.sprite = blockFrames[frame];
            frame++;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
