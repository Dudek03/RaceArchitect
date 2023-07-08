using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCard : MonoBehaviour
{
    public BlockData data;
    BlockPlacer placer;

    private void Start()
    {
        placer = GameObject.Find("Placer").GetComponent<BlockPlacer>();
    }


    public void OnClick()
    {
        placer.CreateBlock(data);
    }
}
