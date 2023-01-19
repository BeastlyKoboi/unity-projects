using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreStockGenerator : MonoBehaviour
{
    [SerializeField] List<float> priceHistory = new List<float>();
    [SerializeField] float[] priceScales = new float[5];



    // Start is called before the first frame update
    void Start()
    {
        priceScales[0] = 32;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (priceHistory.Count < 10)
            {
                priceHistory.Add(priceScales[0] * Mathf.PerlinNoise(Time.time * 1.0f, 0.0f));
            }
            else
            {
                priceHistory.RemoveAt(0);
                priceHistory.Add(priceScales[0] * Mathf.PerlinNoise(Time.time * 1.0f, 0.0f));
            }
        }
        

    }
}
