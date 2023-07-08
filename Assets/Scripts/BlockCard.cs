using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlockCard : MonoBehaviour
{
    public BlockData data;
    BlockPlacer placer;
    public Image image;
    public TMP_Text costText;

    private void Start()
    {
        placer = GameObject.Find("Placer").GetComponent<BlockPlacer>();
        image.sprite = data.img;
        costText.text = data.cost.ToString();
    }


    public void OnClick()
    {
        placer.CreateBlock(data);
    }
}