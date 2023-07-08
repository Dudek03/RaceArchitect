using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusZone : MonoBehaviour
{
    public bool hasBonus = false;

    public float bonus = 6;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.Instance.car.gameObject)
        {
            // RaycastHit hit;
            // if (Physics.Linecast (transform.position, other.gameObject.transform.position, out hit)) {
            //     if(hit.transform.tag == "player"){
                    hasBonus = true;
            //     }
            // }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.Instance.car.gameObject)
        {
            hasBonus = false;
        }
    }
}