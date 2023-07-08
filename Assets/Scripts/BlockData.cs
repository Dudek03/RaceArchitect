using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BlockData : ScriptableObject
{
    public GameObject prefab;
    public Sprite img;
    public Vector3 offset;
    public float cost=300;
}
