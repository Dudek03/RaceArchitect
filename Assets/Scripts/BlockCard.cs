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
    public TMP_Text tooltipText;
    private void Start()
    {
        placer = GameObject.Find("Placer").GetComponent<BlockPlacer>();
    }

    public void SetData(BlockData d)
    {
        data = d;
        image.overrideSprite = data.img;
        costText.text = data.cost.ToString();
        tooltipText.text = data.tooltip;
    }


    public void OnClick()
    {
        placer.CreateBlock(data);
    }
}
