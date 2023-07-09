using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCamera : MonoBehaviour
{
    private BlockPlacer placer;
    private Vector3 startPos;
    float timerV = 0;
    int countV = 0;

    public float firstTime = 0.3f;
    public float secondTime = 0.1f;
    public float nextTime = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        placer = GameObject.Find("Placer").GetComponent<BlockPlacer>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState == GameState.BUILDING)
        {
            bool q = Input.GetKey(KeyCode.Q);
            bool e = Input.GetKey(KeyCode.E);
            if (q && !e)
            {
                CheckMoveV(Vector3.left);
            }
            else if (e && !q)
            {
                CheckMoveV(Vector3.right);
            }
            else
            {
                timerV = 0;
                countV = 0;
            }
        }
        else if (GameManager.Instance.gameState == GameState.RUN)
        {
            transform.position = startPos + new Vector3(GameManager.Instance.car.GetPos().x, 0, 0);
        }


    }

    public void MoveTo(Vector3 pos)
    {
        transform.position = startPos + new Vector3(pos.x, 0, 0);
    }

    void CheckMoveV(Vector3 move)
    {
        if (timerV - getTotalCountedTime(countV) >= 0)
        {
            MoveTo(transform.position + move - startPos);
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
}
