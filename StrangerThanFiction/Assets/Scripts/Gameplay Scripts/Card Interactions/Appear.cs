using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Appear : MonoBehaviour
{
    public RectTransform rectTransform;
    public Vector2 endPos;
    public Quaternion endRotation;
    public float moveSpeed = 1000;
    public float rotSpeed = 50;
    public bool isMoving = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        endPos = new Vector2();
        endRotation = new Quaternion();
        isMoving = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
            return;

        if (rectTransform.localRotation != endRotation)
        {
            rectTransform.localRotation = Quaternion.RotateTowards(rectTransform.localRotation, endRotation, rotSpeed * Time.deltaTime);
              
        }

        if (Vector2.Distance(rectTransform.anchoredPosition, endPos) < 0.01f)
        {
            rectTransform.anchoredPosition = endPos;
            isMoving = false;
            GetComponent<Hoverable>().enabled = true;
            GetComponent<Draggable>().enabled = true;
        }
        else
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, endPos, moveSpeed * Time.deltaTime);
        }
    }

    public void RefreshTarget(Vector2 endPos, float zRotation)
    {
        this.endPos = endPos;
        endRotation = Quaternion.Euler(0, 0, zRotation);
        isMoving = true;

        GetComponent<Hoverable>().enabled = false;
        GetComponent<Draggable>().enabled = false;
    }

}
