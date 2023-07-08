using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCamera : MonoBehaviour
{
    private BlockPlacer placer;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        placer = GameObject.Find("Placer").GetComponent<BlockPlacer>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (placer.currentBlock != null)
        {
            transform.position = startPos + new Vector3(placer.currentBlock.getPos().x, 0, 0);
        }
    }
}