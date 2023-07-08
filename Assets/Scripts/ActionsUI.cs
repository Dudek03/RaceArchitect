using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ActionsUI : MonoBehaviour
{
    [System.Serializable]
    public struct Action
    {
        public ActionsTypes type;
        public GameObject prefab;
    }

    public List<Action> prefabs;
    public float timeAction = 3;
    private float progressAction = 0;
    public Slider actionTimeSlider;

    private void Start()
    {
        progressAction = timeAction;
    }

    private void Update()
    {
        if (GameManager.Instance.gameState != GameState.RUN)
        {
            progressAction = 0;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakeAction();
        }

        if (progressAction <= 0)
        {
            MakeAction();
        }

        progressAction -= Time.deltaTime;
        // actionTimeSlider.value = (timeAction - progressAction) / timeAction;
    }

    private void MakeAction()
    {
        progressAction = timeAction;
        if (GameManager.Instance.actionList.Count != 0)
        {
            var action = GameManager.Instance.actionList.FirstOrDefault();
            switch (action)
            {
                case ActionsTypes.UP:
                    GameManager.Instance.car.ActionUp();
                    break;
                case ActionsTypes.DOWN:
                    GameManager.Instance.car.ActionDown();
                    break;
                case ActionsTypes.LEFT:
                    GameManager.Instance.car.ActionLeft();
                    break;
                case ActionsTypes.RIGHT:
                    GameManager.Instance.car.ActionRight();
                    break;
            }

            GameManager.Instance.actionList.Remove(action);
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    public void PopulateList()
    {
        foreach (Transform child in transform) { Destroy(child.gameObject); }

        foreach (ActionsTypes action in GameManager.Instance.actionList)
        {
            foreach (Action item in prefabs)
            {
                if (action == item.type) Instantiate(item.prefab, transform);
            }
        }
    }
}