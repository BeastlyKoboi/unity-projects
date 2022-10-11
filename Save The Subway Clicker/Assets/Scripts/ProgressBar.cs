using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    //
    [SerializeField]
    private GameObject player;
    private Slider progress;
    public float timeLimit;
    public float timePassed;
    private bool started;
    public int buttonValue; 

    // Start is called before the first frame update
    void Start()
    {
        progress = GetComponent<Slider>();
        progress.value = 0;
        started = false;
        progress.maxValue = timeLimit;
        timePassed = 0;
        buttonValue = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            timePassed += Time.deltaTime;
            

            if (timePassed >= progress.maxValue)
            {
                started = false;
                timePassed = 0;
                progress.value = 0;
                player.GetComponent<Player>().AddMoney(buttonValue);
            }
            else
            {
                progress.value = timePassed;
            }
        }
    }

    //
    public void ProgressStarted()
    {
        if (!started)
        {
            started = true;
        }
    }

}
