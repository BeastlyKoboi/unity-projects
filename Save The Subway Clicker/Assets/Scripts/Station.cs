using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject progressBar;
    public float stationValue;
    public float timeLimit;
    public int stationLevel;
    private Stack<int> enhanceLevels;
    public bool hasOperator;

    // Start is called before the first frame update
    void Start()
    {
        timeLimit = 5;
        stationValue = 5;
        stationLevel = 1;
        hasOperator = false;

        enhanceLevels = new Stack<int>();
        enhanceLevels.Push(500);
        enhanceLevels.Push(450);
        enhanceLevels.Push(400);
        enhanceLevels.Push(350);
        enhanceLevels.Push(300);
        enhanceLevels.Push(250);
        enhanceLevels.Push(225);
        enhanceLevels.Push(200);
        enhanceLevels.Push(175);
        enhanceLevels.Push(150);
        enhanceLevels.Push(125);
        enhanceLevels.Push(100);
        enhanceLevels.Push(90);
        enhanceLevels.Push(80);
        enhanceLevels.Push(70);
        enhanceLevels.Push(60);
        enhanceLevels.Push(50);
        enhanceLevels.Push(40);
        enhanceLevels.Push(30);
        enhanceLevels.Push(20);
        enhanceLevels.Push(10);
    }

    //
    public void PayPlayer()
    {
        player.GetComponent<Player>().AddMoney(stationValue);
    }

    //
    public void UpgradeStation()
    {
        stationValue *= 1.05f;
        stationLevel++;

        if (stationLevel == enhanceLevels.Peek())
        {
            progressBar.GetComponent<ProgressBar>().TimeLimit = timeLimit;
            stationValue *= 1.5f;
            enhanceLevels.Pop();
        }
    }

}
