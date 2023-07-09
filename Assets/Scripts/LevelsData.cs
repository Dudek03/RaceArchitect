using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelsData : ScriptableObject
{
    public List<Level> levels;
}

[System.Serializable]
public class Level
{
    public Vector3 metaMaxPos1;
    public Vector3 metaMaxPos2;

    public int startTarget;
    public bool pointsTarget;
    public List<BlockData> blocks;
    public List<ActionsTypes> actions;
    public int minNumberOfAction;
    public int maxNumberOfAction;
}
