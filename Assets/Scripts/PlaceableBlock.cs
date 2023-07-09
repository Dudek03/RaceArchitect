using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableBlock : MonoBehaviour
{

    public bool isGhost = false;
    public bool isSelected = false;
    public bool selectable = true;
    private BlockPlacer placer;
    public BlockData blockData;
    public GameObject ghost;
    public GameObject selected;
    public GameObject hover;

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

    private void OnMouseEnter()
    {
        if (hover)
            hover.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (hover)
            hover.SetActive(false);
    }

    public void Move(Vector3 move)
    {
        transform.position += move;
    }

    public void Select()
    {
        ghost.SetActive(false);
        selected.SetActive(true);
        isSelected = true;
        isGhost = false;
    }

    public void Unselect()
    {
        if (isGhost)
        {
            if (placer == null)
            {
                placer = GameObject.Find("Placer").GetComponent<BlockPlacer>();
            }
            placer.DestroyBlock();
        }
        ghost.SetActive(false);
        selected.SetActive(false);
        isGhost = false;
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

    public void SetGhost()
    {
        ghost.SetActive(true);
        selected.SetActive(false);
        isSelected = true;
        isGhost = true;
    }

    public void Place()
    {
        ghost.SetActive(false);
        isGhost = false;
    }
}
