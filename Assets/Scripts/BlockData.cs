using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BlockData : ScriptableObject
{
    public GameObject prefab;
    public Sprite img;
    public Vector3 offset;
    public int cost=300;
    [TextArea(minLines:3,maxLines:5)]
    public string tooltip;

}
