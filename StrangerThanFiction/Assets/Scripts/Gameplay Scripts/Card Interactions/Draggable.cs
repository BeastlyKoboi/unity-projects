using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CardModel card;
    private RectTransform rectTransform;
    private Vector2 offset;
    public bool isSelected; 

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        card = GetComponent<CardModel>();
        isSelected = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // offset = rectTransform.anchoredPosition - eventData.position;
        offset = this.transform.position - new Vector3(eventData.position.x, eventData.position.y, 0);
        isSelected = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Offset is finally!!! working right
        // rectTransform.anchoredPosition = eventData.position + offset;
        this.transform.position = eventData.position + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isSelected = false;

        // Needs to check if intersecting with player board areas,
        // or if a spell, the center of the screen
        card.Owner.PlayCard(card);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
