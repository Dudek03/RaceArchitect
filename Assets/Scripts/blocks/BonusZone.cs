using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusZone : MonoBehaviour
{
    public bool hasBonus = false;
    public LayerMask layerMask;
    public float bonus = 6;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == GameManager.Instance.car.gameObject)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(other.gameObject.transform.position, (transform.position - other.transform.position),
                    out hit, Mathf.Infinity, layerMask))
            {
                if (hit.transform == transform.parent)
                {
                    hasBonus = true;
                }
                else
                {
                    hasBonus = false;
                }
            }
        }
    }
}