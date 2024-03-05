using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class UnitRow : MonoBehaviour
{
    public List<CardModel> units;
    public List<RectTransform> unitRects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddUnit(CardModel newUnit)
    {
        newUnit.transform.SetParent(transform);

        units.Add(newUnit);
        unitRects.Add(newUnit.GetComponent<RectTransform>());

        UpdateUnitPositions();
    }

    public void RemoveUnit(CardModel newUnit)
    {

    }

    public void UpdateUnitPositions() 
    {
        
        float unitWidth = units[0].unitView.transform.localScale.x * units[0].unitView.GetComponent<RectTransform>().rect.width;
        float unitCount = units.Count;

        float filledRowWidth = unitCount * unitWidth;

        float startingXPos = -filledRowWidth / 2 + unitWidth / 2;

        for (int index = 0; index < unitCount; index++)
        {
            unitRects[index].anchoredPosition = new Vector2(startingXPos, 0);
            unitRects[index].rotation = Quaternion.identity;
            startingXPos += unitWidth;
        }

    }
}
