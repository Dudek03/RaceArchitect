using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    public GameObject placeholder; //TODO: Remove
    public PlaceableBlock currentBlock;
    public List<PlaceableBlock> allBlocks;


    float timerH = 0;
    float timerV = 0;
    public float releaseTime = 1; //TODO: bigger first threshold
    public float axisesThreshold = 0.1f;
    public Transform maxBottom;
    public Transform maxLeft;
    public Transform maxTop;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentBlock != null)
        {
            MoveBlock();
            if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
            {
                DestroyBlock();
            }
        }
        if (Input.GetKeyDown("1"))
        {
            CreateBlock(placeholder);
        }
    }

    public void CreateBlock(GameObject block)
    {
        if (currentBlock != null)
        {
            currentBlock.Unselect();
        }
        GameObject obj = Instantiate(block, new Vector3(-1, 0, 0), Quaternion.identity, transform);
        currentBlock = obj.GetComponent<PlaceableBlock>();
        allBlocks.Add(currentBlock);
        currentBlock.Select();
        Move(Vector3.right);
    }

    public void SelectBlock(PlaceableBlock block)
    {
        if (!block.selectable) return;
        if (currentBlock != null)
        {
            currentBlock.Unselect();
        }
        currentBlock = block;
        if (currentBlock != null)
        {
            currentBlock.Select();
        }
    }

    public void DestroyBlock()
    {
        allBlocks.Remove(currentBlock);
        currentBlock.SelfDestroy();
        currentBlock = null;
    }

    void MoveBlock()
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
        do
        {
            currentBlock.Move(move);
            if (isOutOfBound(currentBlock.getPos()))
            {
                currentBlock.Move(-move);
                break;
            }
        }
        while (!isOneOnSpot(currentBlock));
    }


    bool isOneOnSpot(PlaceableBlock block)
    {
        foreach (PlaceableBlock b in allBlocks)
        {
            if (b != block && b.getPos() == block.getPos())
            {
                return false;
            }
        }
        return true;
    }

    bool isOutOfBound(Vector3 pos)
    {
        return pos.x < maxLeft.position.x || pos.y > maxTop.position.y || pos.y < maxBottom.position.y;
    }

}
