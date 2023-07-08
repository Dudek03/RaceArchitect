using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsController : MonoBehaviour
{
    public List<BlockCard> cards;
    BlockPlacer placer;

    void Start()
    {
        placer = GameObject.Find("Placer").GetComponent<BlockPlacer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (placer.currentBlock == null)
            {
                cards[0].OnClick();
                return;
            }
            int idx = cards.FindIndex(e => e.data == placer.currentBlock.blockData);
            idx++;
            if (idx >= cards.Count)
            {
                idx = 0;
            }
            cards[idx].OnClick();
        }
    }

}
