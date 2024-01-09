using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    RectTransform rectTransform;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // offset = rectTransform.anchoredPosition - eventData.position;
        offset = this.transform.position - new Vector3(eventData.position.x, eventData.position.y, 0);

    }

    public void OnDrag(PointerEventData eventData)
    {
        // Offset is finally!!! working right
        // rectTransform.anchoredPosition = eventData.position + offset;
        this.transform.position = eventData.position + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
