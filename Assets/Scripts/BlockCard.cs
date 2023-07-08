using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockCard : MonoBehaviour
{
    public BlockData data;
    BlockPlacer placer;
    public Image image;

    private void Start()
    {
        placer = GameObject.Find("Placer").GetComponent<BlockPlacer>();
        image.sprite = data.img;
    }


    public void OnClick()
    {
        placer.CreateBlock(data);
    }
}