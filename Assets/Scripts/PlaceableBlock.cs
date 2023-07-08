using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableBlock : MonoBehaviour
{

    public bool isSelected = false;
    public bool selectable = true;
    private BlockPlacer placer;
    public BlockData blockData;

    void Start()
    {
        placer = GameObject.Find("Placer").GetComponent<BlockPlacer>();
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.gameState != GameState.BUILDING) return;
        if (!isSelected)
        {
            placer.SelectBlock(this);
        }
    }

    public void Move(Vector3 move)
    {
        transform.position += move;
    }

    public void Select()
    {
        isSelected = true;
    }
    public void Unselect()
    {
        isSelected = false;
    }

    internal Vector3 getPos()
    {
        return transform.position;
    }

    internal void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
