using System;
using System.Collections;
using System.Collections.Generic;
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

        if (Input.GetKeyDown(KeyCode.R) && gameState == GameState.RUN)
        {
            car.Die();
        }

        if (gameState != GameState.RUN) return;
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
        GameManager.Instance.actionList = new List<ActionsTypes>(GameManager.Instance.actionListSaved);
        car.Reset();
    }
}
