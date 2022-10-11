using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    //
    [SerializeField]
    private GameObject station;

    private Slider progress;
    private float timeLimit;
    public float timePassed;
    private bool started;
    private bool hasOperator;

    public float TimeLimit
    {
        set
        {
            timeLimit = value;
            progress.maxValue = timeLimit;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        progress = GetComponent<Slider>();
        progress.value = 0;
        started = false;
        timeLimit = station.GetComponent<Station>().timeLimit;
        progress.maxValue = timeLimit;
        timePassed = 0;
        hasOperator = false;
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
                station.GetComponent<Station>().PayPlayer();

                if (hasOperator)
                {
                    started = true;
                }
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
