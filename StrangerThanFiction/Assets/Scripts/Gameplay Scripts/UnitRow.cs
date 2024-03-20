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

    // ----------------------------------------------------------------------------
    // Methods used to query specific units 
    // ----------------------------------------------------------------------------

    /// <summary>
    /// Returns the strongest unit in this unit row, by power then plot armor.
    /// </summary>
    /// <returns></returns>
    public CardModel GetStrongestUnit()
    {
        if (units.Count == 0) return null;

        CardModel[] unitsArr = units.ToArray();
        CardModel strongest = unitsArr[0];

        for (int i = 0; i < unitsArr.Length; i++)
        {
            if (strongest.CurrentPower < unitsArr[i].CurrentPower)
                strongest = unitsArr[i];
            else if (strongest.CurrentPower == unitsArr[i].CurrentPower &&
                strongest.CurrentPlotArmor < unitsArr[i].CurrentPlotArmor)
                strongest = unitsArr[i];
        }
        return strongest;
    }

    /// <summary>
    /// Returns the weakest unit in this unit row, by power then plot armor.
    /// </summary>
    /// <returns></returns>
    public CardModel GetWeakestUnit()
    {
        if (units.Count == 0) return null;

        CardModel[] unitsArr = units.ToArray();
        CardModel weakest = unitsArr[0];

        for (int i = 0; i < unitsArr.Length; i++)
        {
            if (weakest.CurrentPower > unitsArr[i].CurrentPower)
                weakest = unitsArr[i];
            else if (weakest.CurrentPower == unitsArr[i].CurrentPower &&
                weakest.CurrentPlotArmor > unitsArr[i].CurrentPlotArmor)
                weakest = unitsArr[i];
        }
        return weakest;
    }

    /// <summary>
    /// Calculates and returns the total power of units in this row. 
    /// </summary>
    /// <returns></returns>
    public uint GetTotalPower()
    {
        uint total = 0;

        units.ForEach(unit => { total += unit.CurrentPower; });

        return total;
    }
}
