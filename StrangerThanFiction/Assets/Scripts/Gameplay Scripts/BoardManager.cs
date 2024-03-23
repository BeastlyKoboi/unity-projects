using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public UnitRow player1FrontRow;
    public UnitRow player1BackRow;

    public UnitRow player2FrontRow;
    public UnitRow player2BackRow;

    // better functionality in the future may be achieved 
    // through UnitArea Scripts attached to a game object.

    public void SummonUnit(CardModel unit, UnitRow row)
    {
        unit.IsHidden = false;

        row.AddUnit(unit);

        unit.Owner.UnitSummoned(unit);
    }

    // Does not check if its for the right person, it now needs to
    // This will now be a more specific method
    public bool CheckPointerAboveArea(PointerEventData eventData, CardModel card)
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        // 
        UnitRow frontline;
        UnitRow backline;
        if (card.Owner == gameManager.player1)
        {
            frontline = player1FrontRow;
            backline = player1BackRow;
        }
        else
        {
            frontline = player2FrontRow;
            backline = player2BackRow;
        }

        for (int i = 0; i < raycastResults.Count; i++)
        {
            if (frontline.gameObject == raycastResults[i].gameObject)
            {
                card.SelectedArea = frontline;
                return true;
            }
            if (backline.gameObject == raycastResults[i].gameObject)
            {
                card.SelectedArea = backline;
                return true;
            }
        }

        return false;
    }

    public void RoundStart()
    {
        player1FrontRow.ForEach(unit =>
        {
            unit.RoundStart();
        });

        player1BackRow.ForEach(unit =>
        {
            unit.RoundStart();
        });

        player2FrontRow.ForEach(unit =>
        {
            unit.RoundStart();
        });

        player2BackRow.ForEach(unit =>
        {
            unit.RoundStart();
        });
    }

    public void RoundEnd()
    {

    }

    // Likely needs helper methods within that like GetWeakestEnemy etc

    public CardModel GetStrongestUnit(Player player)
    {
        UnitRow frontline;
        UnitRow backline;
        CardModel frontStrongest;
        CardModel backStrongest;
        if (player == gameManager.player1)
        {
            frontline = player1FrontRow;
            backline = player1BackRow;
        }
        else
        {
            frontline = player2FrontRow;
            backline = player2BackRow;
        }

        frontStrongest = frontline.GetStrongestUnit();
        backStrongest = backline.GetStrongestUnit();

        if (!frontStrongest && !backStrongest) return null;

        if (!frontStrongest)
        {
            return backStrongest;
        }
        if (!backStrongest)
        {
            return frontStrongest;
        }

        if (frontStrongest.CurrentPower > backStrongest.CurrentPower)
            return frontStrongest;
        else if (frontStrongest.CurrentPower == backStrongest.CurrentPower &&
            frontStrongest.CurrentPlotArmor >= backStrongest.CurrentPlotArmor)
            return frontStrongest;

        return backStrongest;
    }

    public CardModel GetWeakestUnit(Player player)
    {
        UnitRow frontline;
        UnitRow backline;
        CardModel frontWeakest;
        CardModel backWeakest;
        if (player == gameManager.player1)
        {
            frontline = player1FrontRow;
            backline = player1BackRow;
        }
        else
        {
            frontline = player2FrontRow;
            backline = player2BackRow;
        }

        frontWeakest = frontline.GetWeakestUnit();
        backWeakest = backline.GetWeakestUnit();

        if (!frontWeakest && !backWeakest) return null;

        if (!frontWeakest)
        {
            return backWeakest;
        }
        if (!backWeakest)
        {
            return frontWeakest;
        }

        if (frontWeakest.CurrentPower < backWeakest.CurrentPower)
            return frontWeakest;
        else if (frontWeakest.CurrentPower == backWeakest.CurrentPower &&
            frontWeakest.CurrentPlotArmor <= backWeakest.CurrentPlotArmor)
            return frontWeakest;

        return backWeakest;
    }

    public uint GetTotalFrontPower(Player player)
    {
        UnitRow frontline;
        if (player == gameManager.player1)
            frontline = player1FrontRow;
        else
            frontline = player2FrontRow;

        return frontline.GetTotalPower();
    }

    public uint GetTotalBackPower(Player player)
    {
        UnitRow backline;
        if (player == gameManager.player1)
            backline = player1BackRow;
        else
            backline = player2BackRow;

        return backline.GetTotalPower();
    }

    public uint GetTotalPower(Player player)
    {
        return GetTotalFrontPower(player) + GetTotalBackPower(player);
    }

    public UnitRow GetRandomEnemyRow()
    {
        return (Random.value > 0.5) ? player2BackRow : player2FrontRow;
    }

    public CardModel[] GetUnits(Player player)
    {
        UnitRow frontline;
        UnitRow backline;
        if (player == gameManager.player1)
        {
            frontline = player1FrontRow;
            backline = player1BackRow;
        }
        else
        {
            frontline = player2FrontRow;
            backline = player2BackRow;
        }

        CardModel[] frontUnits = frontline.GetUnits();
        CardModel[] backUnits = backline.GetUnits();

        List<CardModel> units = new List<CardModel>();
        units.AddRange(frontUnits);
        units.AddRange(backUnits);

        return units.ToArray();
    }
}
