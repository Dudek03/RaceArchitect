using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public PointsMultiplication pointsMultiplication;
    public static GameManager Instance => _instance;
    public CarScript car;
    public GameState gameState = GameState.BUILDING;
    public List<ActionsTypes> actionListSaved;
    public List<ActionsTypes> actionList;
    public int targetGame = 500;
    public int points = 0;
    public int pointsMultiply = 1;
    public ProgressCounter progressCounter;
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
        progressCounter.UpdateMaxValue(targetGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && gameState == GameState.BUILDING)
        {
            gameState = GameState.RUN;
        }

        if (gameState != GameState.RUN) return;

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

    public void IncreaseTarget(int value)
    {
        targetGame += value;
        progressCounter.UpdateMaxValue(targetGame);
    }

    public void DecreaseTarget(int value)
    {
        targetGame -= value;
        progressCounter.UpdateMaxValue(targetGame);
    }

    public void AddPoints(int points)
    {
        this.points += points * pointsMultiply;
        progressCounter.UpdatePoints(this.points);
    }

    public void AddMultiply(int increase)
    {
        pointsMultiply += increase;
        if (pointsMultiply < 1)
        {
            pointsMultiply = 1;
        }

        if (pointsMultiply > 10)
        {
            pointsMultiply = 10;
        }
    }

    public void ResetMultiply()
    {
        pointsMultiply = 1;
    }

    public void GameOver()
    {
        gameState = GameState.BUILDING;
        GameManager.Instance.actionList = GameManager.Instance.actionListSaved.Select(a => a).ToList();
        FindObjectOfType<ActionsUI>().PopulateList();
        car.Reset();
    }
}