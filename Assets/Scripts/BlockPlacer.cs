using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    public PlaceableBlock currentBlock;
    public Vector3 currentPos;
    public GameObject[] allBlocks;


    public float timerH = 0;
    public float timerV = 0;
    public float releaseTime = 1; //TODO: bigger first threshold
    public float axisesThreshold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentBlock != null)
        {
            if (Input.GetAxis("Horizontal") > axisesThreshold)
            {
                CheckMoveH(Vector3.right);
            }
            else if (Input.GetAxis("Horizontal") < -axisesThreshold)
            {
                CheckMoveH(Vector3.left);
            }
            else
            {
                timerH = 0;
            }

            if (Input.GetAxis("Vertical") > axisesThreshold)
            {
                CheckMoveV(Vector3.up);
            }
            else if (Input.GetAxis("Vertical") < -axisesThreshold)
            {
                CheckMoveV(Vector3.down);
            }
            else
            {
                timerV = 0;
            }
        }
    }


    void CheckMoveH(Vector3 move)
    {
        if (timerH <= 0)
        {
            Move(move);
            timerH += Time.deltaTime;
        }
        else if (timerH >= releaseTime)
        {
            timerH = 0;
        }
        else
        {
            timerH += Time.deltaTime;
        }
    }

    void CheckMoveV(Vector3 move)
    {
        if (timerV <= 0)
        {
            Move(move);
            timerV += Time.deltaTime;
        }
        else if (timerV >= releaseTime)
        {
            timerV = 0;
        }
        else
        {
            timerV += Time.deltaTime;
        }

    }

    void Move(Vector3 move)
    {
        currentBlock.Move(move);
    }
}
