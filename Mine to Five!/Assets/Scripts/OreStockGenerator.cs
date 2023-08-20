using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OreStockGenerator : MonoBehaviour
{
    private Vector3 startPoint = new Vector3(0, 0, 0);
    private float lineWidth = 1.5f;

    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] List<float> priceHistory = new List<float>();
    [SerializeField] float[] priceScales = new float[5];

    
    // Start is called before the first frame update
    void Start()
    {
        priceScales[0] = 32;

        lineRenderer.SetPosition(0, startPoint);
    }
    
    // Update is called once per frame
    void Update()
    {        

    }

    //
    public void ProgressStocks()
    {
        float rngValue = Mathf.PerlinNoise(Time.time * 0.05f, Time.time * 0.05f);
        

        if (priceHistory.Count < 8)
        {
            priceHistory.Add(priceScales[0] * rngValue);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(priceHistory.Count, 
                new Vector3(priceHistory.Count * lineWidth, 8 * rngValue, 0) + startPoint);
        }
        else
        {
            priceHistory.RemoveAt(0);
            priceHistory.Add(priceScales[0] * rngValue);

            Vector3 temp = new Vector3((priceHistory.Count + 1) * lineWidth, 8 * rngValue, 0) + startPoint;
            Vector3 temp2;
            for (int index = priceHistory.Count; index >= 0; index--)
            {
                temp2 = lineRenderer.GetPosition(index);
                temp.x -= lineWidth;
                lineRenderer.SetPosition(index, temp);
                temp = temp2;
            }

        }
    }


    // 
    public float GetStockPrice()
    {
        return priceHistory[priceHistory.Count - 1];
    }
}
