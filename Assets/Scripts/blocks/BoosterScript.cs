using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class booster : MonoBehaviour
{
    public float acceleration = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == GameManager.Instance.car.gameObject)
        {
            if (GameManager.Instance.car.rightArrowActivate)
            {
                GameManager.Instance.car.IncreaseSpeed(acceleration * Time.deltaTime * 2);
            }
            else
            {
                GameManager.Instance.car.IncreaseSpeed(acceleration * Time.deltaTime);
            }
        }
    }
}