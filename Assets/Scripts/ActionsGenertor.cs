using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ActionsGenertor : MonoBehaviour
{
    public ActionsUI actionsUI;



    public void Generate()
    {
        GameManager.Instance.actionListSaved.Clear();
        GameManager.Instance.actionList.Clear();
        int length = UnityEngine.Random.Range(GameManager.Instance.currentLevelData.minNumberOfAction, GameManager.Instance.currentLevelData.maxNumberOfAction);
        Array values = Enum.GetValues(typeof(ActionsTypes));
        for (int i = 0; i < length; i++)
        {
            GameManager.Instance.actionListSaved.Add(
                GameManager.Instance.currentLevelData.actions[UnityEngine.Random.Range(0, GameManager.Instance.currentLevelData.actions.Count)]
            );
        }

        GameManager.Instance.actionList = GameManager.Instance.actionListSaved.Select(a => a).ToList();
        actionsUI.PopulateList();
    }


    // Update is called once per frame
    void Update()
    {
    }
}
