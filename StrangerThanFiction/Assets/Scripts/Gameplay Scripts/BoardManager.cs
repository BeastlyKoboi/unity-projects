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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void SummonUnit(CardModel unit, UnitRow row)
    {
        unit.IsHidden = false;

        row.AddUnit(unit);

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

    // Likely needs helper methods within that like GetWeakestEnemy etc

    public UnitRow GetRandomEnemyRow()
    {
        return (Random.value > 0.5) ? player2BackRow: player2FrontRow;
    }
}
