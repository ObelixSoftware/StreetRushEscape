using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Slider pursuitSlider;
    public Slider timeSlider;

    public float pursuitLevel = 0f;
    public int score = 0;

    //Time
    public float startingTime = 600;
    public float globalTime;

    // Start is called before the first frame update
    void Start()
    {
        globalTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (globalTime > 0)
        {
            globalTime -= Time.deltaTime;
        }
        else
        {
            GlobalTimerFinished();
        }

        if (pursuitLevel > 0)
        {
            pursuitLevel -= Time.deltaTime;
        }
        timeSlider.value = globalTime;
        pursuitSlider.value = pursuitLevel;

        if (Input.GetKey(KeyCode.P))
        {
            pursuitLevel += 0.1f;
        }
    }

    void GlobalTimerFinished()
    {
        Debug.Log("Global Timer Finished");
    }

    internal void IncreasePursuit(float adjustment)
    {
        if (pursuitLevel < 100)
        {
            if (pursuitLevel + adjustment <= 100)
            {
                pursuitLevel += adjustment;
            } 
            else 
            {
                pursuitLevel = 100f;
            }
        }
        Debug.Log(pursuitLevel);
    }
}