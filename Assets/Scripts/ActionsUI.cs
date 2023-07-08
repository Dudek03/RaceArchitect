using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    private void Start()
    {
        progressAction = timeAction;
    }

    private void Update()
    {
        if (GameManager.Instance.gameState != GameState.RUN) return;
        print("sdasa");
        if (progressAction <= 0)
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
                //TODO
                // while (transform.childCount > 0) Destroy(transform.GetChild(0).gameObject);
                // PopulateList();
            }
        }

        progressAction -= Time.deltaTime;
    }

    public void PopulateList()
    {
        foreach (ActionsTypes action in GameManager.Instance.actionList)
        {
            foreach (Action item in prefabs)
            {
                if (action == item.type) Instantiate(item.prefab, transform);
            }
        }
    }
}