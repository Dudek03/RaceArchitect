using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    public PlaceableBlock currentBlock;
    public List<PlaceableBlock> allBlocks;

    float timerH = 0;
    int countH = 0;
    float timerV = 0;
    int countV = 0;

    public float firstTime = 0.3f;
    public float secondTime = 0.1f;
    public float nextTime = 0.05f;
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
        if (GameManager.Instance.gameState != GameState.BUILDING)
        {
            if (currentBlock != null)
            {
                currentBlock.Unselect();
                currentBlock = null;
            }

            return;
        }

        if (currentBlock != null)
        {
            MoveBlock();
            if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
            {
                DestroyBlock();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentBlock.Place();
                CreateBlock(currentBlock.blockData);
            }
        }
    }

    public void CreateBlock(BlockData blockData)
    {
        Vector3 startPos;
        if (currentBlock == null)
        {
            startPos = new Vector3(-1, 0, 0);
        }
        else if (currentBlock.isGhost)
        {
            startPos = currentBlock.getPos() - Vector3.right - currentBlock.blockData.offset;
            currentBlock.Unselect();
        }
        else
        {
            startPos = currentBlock.getPos();
            currentBlock.Unselect();
        }
        startPos += blockData.offset;
        if (isOutOfBound(startPos))
        {
            startPos -= blockData.offset;
        }
        GameObject obj = Instantiate(blockData.prefab, startPos, Quaternion.identity, transform);
        currentBlock = obj.GetComponent<PlaceableBlock>();
        allBlocks.Add(currentBlock);
        currentBlock.SetGhost();
        currentBlock.blockData = blockData;
        Move(Vector3.right);
        GameManager.Instance.IncreaseTarget(blockData.cost);
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
            GameManager.Instance.targetCamera.MoveTo(currentBlock.getPos());
        }
    }

    public void DestroyBlock()
    {
        allBlocks.Remove(currentBlock);
        currentBlock.SelfDestroy();
        GameManager.Instance.DecreaseTarget(currentBlock.blockData.cost);
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
            countH = 0;
        }

        if (Input.GetAxis("Vertical") > axisesThreshold)
        {
            CheckMoveV(Vector3.up / 2);
        }
        else if (Input.GetAxis("Vertical") < -axisesThreshold)
        {
            CheckMoveV(Vector3.down / 2);
        }
        else
        {
            timerV = 0;
            countV = 0;
        }
    }


    void CheckMoveH(Vector3 move)
    {
        if (timerH - getTotalCountedTime(countH) >= 0)
        {
            Move(move);
            countH++;
        }
        timerH += Time.deltaTime;
    }

    void CheckMoveV(Vector3 move)
    {
        if (timerV - getTotalCountedTime(countV) >= 0)
        {
            Move(move);
            countV++;
        }
        timerV += Time.deltaTime;

    }

    float getTotalCountedTime(int count)
    {
        if (count == 0)
        {
            return 0;
        }
        else if (count == 1)
        {
            return firstTime;
        }
        else if (count == 2)
        {
            return firstTime + secondTime;
        }
        else
        {
            return firstTime + secondTime + (count - 2) * nextTime;
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
        } while (!isOneOnSpot(currentBlock));

        GameManager.Instance.targetCamera.MoveTo(currentBlock.getPos());
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


    public void Clear()
    {
        if (currentBlock != null)
        {
            currentBlock.Unselect();
        }
        currentBlock = null;

        List<PlaceableBlock> toDestroy = new List<PlaceableBlock>();
        foreach (PlaceableBlock block in allBlocks)
        {
            if (block.selectable)
            {
                toDestroy.Add(block);
            }
        }
        foreach (PlaceableBlock block in toDestroy)
        {
            currentBlock = block;
            DestroyBlock();
        }

        GameManager.Instance.targetCamera.MoveTo(Vector3.zero);
    }
}
