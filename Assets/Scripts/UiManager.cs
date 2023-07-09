using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private static UiManager _instance;
    public static UiManager Instance => _instance;
    public GameObject runUI;
    public GameObject buildUI;
    public GameObject winScreen;
    public GameObject endScreen;

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

    void Start()
    {
        winScreen.SetActive(false);
    }

    void Update()
    {

        if (GameManager.Instance.gameState == GameState.RUN)
        {
            runUI.SetActive(true);
            buildUI.SetActive(false);
        }
        else if (GameManager.Instance.gameState == GameState.BUILDING)
        {
            runUI.SetActive(false);
            buildUI.SetActive(true);
        }
        else
        {
            runUI.SetActive(true);
            buildUI.SetActive(false);
        }
    }

    public void ShowWin()
    {
        if (GameManager.Instance.currentLevel >= GameManager.Instance.levelData.levels.Count - 1)
        {
            int pt = GameManager.Instance.GetTotalPoints();
            if (PlayerPrefs.GetInt("highscore", 0) < pt)
            {
                PlayerPrefs.SetInt("highscore", pt);
            }
            endScreen.SetActive(true);
        }
        else
        {
            winScreen.SetActive(true);
        }
    }

    public void HideWin()
    {
        endScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    public void EnterRunMode()
    {
        GameManager.Instance.gameState = GameState.RUN;
    }
}
