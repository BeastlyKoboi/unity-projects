using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfo : MonoBehaviour
{
    [SerializeField]
    private float durability;

    public SpriteRenderer blockRenderer;

    public List<Sprite> blockFrames;

    public int frame;

    public float Durability
    {
        get
        {
            return durability;
        }
        set
        {
            durability = value;

            if (durability < 100.0 - frame * (100.0 / blockFrames.Count))
            {
                NextSpriteFrame();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        durability = 100f;
        blockRenderer = GetComponent<SpriteRenderer>();
        frame = 1;
    }

    // Update is called once per frame
    void Update()
    {

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
