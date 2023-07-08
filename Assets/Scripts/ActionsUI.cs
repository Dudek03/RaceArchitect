using System.Collections;
using System.Collections.Generic;
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
