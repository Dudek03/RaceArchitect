using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;
    public CarScript car;
    public GameState gameState = GameState.BUILDING;

    float timerH = 0;
    float timerV = 0;
    public float releaseTime = 1; //TODO: bigger first threshold
    public float axisesThreshold = 0.1f;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && gameState == GameState.BUILDING)
        {
            gameState = GameState.RUN;
        }

        if (Input.GetAxis("Horizontal") > axisesThreshold)
        {
            if (timerH <= 0)
            {
                car.ActionRight();
                timerH += Time.deltaTime;
            }
            else if (timerH >= releaseTime)
            {
                timerH = 0;
            }
            else
            {
                timerH += Time.deltaTime;
            }
        }
        else if (Input.GetAxis("Horizontal") < -axisesThreshold)
        {
            if (timerH <= 0)
            {
                car.ActionLeft();
                timerH += Time.deltaTime;
            }
            else if (timerH >= releaseTime)
            {
                timerH = 0;
            }
            else
            {
                timerH += Time.deltaTime;
            }
        }
        else
        {
            timerH = 0;
        }

        if (Input.GetAxis("Vertical") > axisesThreshold)
        {
            if (timerV <= 0)
            {
                car.ActionUp();
                timerV += Time.deltaTime;
            }
            else if (timerV >= releaseTime)
            {
                timerV = 0;
            }
            else
            {
                timerV += Time.deltaTime;
            }
        }
        else if (Input.GetAxis("Vertical") < -axisesThreshold)
        {
            if (timerV <= 0)
            {
                car.ActionDown();
                timerV += Time.deltaTime;
            }
            else if (timerV >= releaseTime)
            {
                timerV = 0;
            }
            else
            {
                timerV += Time.deltaTime;
            }
        }
        else
        {
            timerV = 0;
        }
    }
}
