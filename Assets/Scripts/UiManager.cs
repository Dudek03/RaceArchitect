using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject runUI;
    public GameObject buildUI;

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
    }

    public void EnterRunMode()
    {
        GameManager.Instance.gameState = GameState.RUN;
    }
}
