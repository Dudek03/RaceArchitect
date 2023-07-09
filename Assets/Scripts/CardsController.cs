using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsController : MonoBehaviour
{
    List<BlockCard> cards = new List<BlockCard>();
    BlockPlacer placer;
    public GameObject cardPrefab;

    void Start()
    {
        placer = GameObject.Find("Placer").GetComponent<BlockPlacer>();
        GenerateCards();
    }

    void GenerateCards()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        cards.Clear();

        var blocks = GameManager.Instance.currentLevelData.blocks;
        foreach (var block in blocks)
        {
            Instantiate(cardPrefab, transform);
            BlockCard c = cardPrefab.GetComponent<BlockCard>();
            c.SetData(block);
            cards.Add(c);
        }
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
