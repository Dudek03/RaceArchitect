using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsGenertor : MonoBehaviour
{
    public ActionsUI actionsUI;
    public int minNumberOfAction;
    public int maxNumberOfAction;
    void Start()
    {
        generate();
        actionsUI.PopulateList();
    }

    void generate()
    {
        GameManager.Instance.actionList.Clear();
        int length = UnityEngine.Random.Range(minNumberOfAction, maxNumberOfAction);
        Array values = Enum.GetValues(typeof(ActionsTypes));
        for (int i = 0; i < length; i++)
        {
            GameManager.Instance.actionList.Add((ActionsTypes)values.GetValue(UnityEngine.Random.Range(0, values.Length)));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
