using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hoverable : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler
{
    public RectTransform rectTransform;
    public Vector2 startPos;
    public Vector2 endPos;
    public Quaternion startRotation;
    public Quaternion endRotation;
    public float moveSpeed = 1000;
    public float rotSpeed = 50;

    public bool isHovered;

    private int orderIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHovered)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, endPos, moveSpeed * Time.deltaTime);
            rectTransform.localRotation = Quaternion.RotateTowards(rectTransform.localRotation, endRotation, rotSpeed * Time.deltaTime);
        }
        else
        {

            if (rectTransform.anchoredPosition != startPos)
            {
                rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, startPos, moveSpeed * Time.deltaTime);

            }
            if (rectTransform.localRotation != startRotation)
            {
                rectTransform.localRotation = Quaternion.RotateTowards(rectTransform.localRotation, startRotation, rotSpeed * Time.deltaTime);

            }
        }
    }
    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
        endPos = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 200);
        startRotation = rectTransform.localRotation;
        endRotation = Quaternion.Euler(0, 0, 0);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        isHovered = true;
        rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y, 5);
        orderIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {

    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        isHovered = false;
        rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y, 0);
        transform.SetSiblingIndex(orderIndex);
    }
}
