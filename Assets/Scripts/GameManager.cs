using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using blocks;
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
    public ProgressCounterNoThreshold ProgressCounterNoThreshold;
    public TargetCamera targetCamera;
    public Winner meta;

    public LevelsData levelData;
    public Level currentLevelData => levelData.levels[currentLevel];


    public List<LevelPoints> totalPointsArray = new List<LevelPoints>();

    public int currentLevel = 0;
    public ActionsGenertor actionsGenertor;
    public CardsController cardsController;
    public BlockPlacer blockPlacer;

    public struct LevelPoints
    {
        public int idx;
        public int points;
    }

    public ScoreInfoManager scoreInfoManager;
    private int scoreInFly = 0;

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
        SetupNewLevel();
        Reset();
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
        ProgressCounterNoThreshold.UpdateMaxValue(targetGame);
    }

    public void DecreaseTarget(int value)
    {
        targetGame -= value;
        progressCounter.UpdateMaxValue(targetGame);
        ProgressCounterNoThreshold.UpdateMaxValue(targetGame);
    }

    public void AddPoints(int points)
    {
        if (Instance.gameState != GameState.RUN) return;
        float mul = 0;
        foreach (var zone in FindObjectsOfType<BonusZone>())
        {
            if (zone.hasBonus)
            {
                mul += zone.bonus;
            }
        }

        var addingPoints = (int)(points * (pointsMultiply + mul));
        this.points += addingPoints;
        scoreInFly += addingPoints;
        scoreInfoManager.UpdateScore(scoreInFly);
        scoreInfoManager.UpdateBonus((int)(pointsMultiply + mul));
        scoreInfoManager.UpdateBonusZone((int)mul);
        scoreInfoManager.UpdateBonusFlip(pointsMultiply);
        progressCounter.UpdatePoints(this.points);
        ProgressCounterNoThreshold.UpdatePoints(this.points);
    }

    public void AddMultiply(int increase)
    {
        pointsMultiply += increase;
        if (pointsMultiply < 1)
        {
            pointsMultiply = 1;
        }

        if (pointsMultiply > 20)
        {
            pointsMultiply = 20;
        }
    }

    public void ResetMultiply()
    {
        pointsMultiply = 1;
        scoreInFly = 0;
        scoreInfoManager.UpdateScore(scoreInFly);
        scoreInfoManager.Reset();
    }

    public void GameOver()
    {
        gameState = GameState.BUILDING;
        actionList = actionListSaved.Select(a => a).ToList();
        points = 0;
        ResetMultiply();
        progressCounter.UpdatePoints(points);
        ProgressCounterNoThreshold.UpdatePoints(points);
        FindObjectOfType<ActionsUI>().PopulateList();
        car.Reset();
        targetCamera.MoveTo(Vector3.zero);
    }

    void Reset()
    {
        points = 0;
        progressCounter.UpdateMaxValue(targetGame);
        ProgressCounterNoThreshold.UpdateMaxValue(targetGame);
        gameState = GameState.BUILDING;
        ResetMultiply();
        car.Reset();
        progressCounter.UpdatePoints(points);
        ProgressCounterNoThreshold.UpdatePoints(points);
        targetCamera.MoveTo(Vector3.zero);
        FindObjectOfType<ActionsUI>().PopulateList();
    }

    public void Win()
    {
        LevelPoints l = new LevelPoints();
        l.points = points;
        l.idx = currentLevel;

        int i = totalPointsArray.FindIndex(e => e.idx == currentLevel);
        if (i < 0)
        {
            totalPointsArray.Add(l);
        }
        else
        {
            totalPointsArray[i] = l;
        }
    }

    public int GetTotalPoints()
    {
        int sum = 0;
        foreach (LevelPoints levelPoints in totalPointsArray)
        {
            sum += levelPoints.points;
        }

        return sum;
    }

    private void SetupNewLevel()
    {
        actionsGenertor.Generate();
        cardsController.GenerateCards();
        blockPlacer.Clear();
        targetGame = currentLevelData.startTarget;
        meta.SetPos(levelData.levels[currentLevel].metaMaxPos1, levelData.levels[currentLevel].metaMaxPos2);
        if (currentLevelData.pointsTarget)
        {
            progressCounter.gameObject.SetActive(true);
            ProgressCounterNoThreshold.gameObject.SetActive(false);
        }
        else
        {
            progressCounter.gameObject.SetActive(false);
            ProgressCounterNoThreshold.gameObject.SetActive(true);
        }
    }

    public void NextLevel()
    {
        if (currentLevel + 1 < levelData.levels.Count)
        {
            currentLevel++;
            SetupNewLevel();
            Reset();
        }
    }
}
