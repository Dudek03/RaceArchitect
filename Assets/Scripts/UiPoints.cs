using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiPoints : MonoBehaviour
{

    public TextMeshProUGUI current;
    public TextMeshProUGUI total;


    void Update()
    {
        current.text = GameManager.Instance.points + "";
        total.text = GameManager.Instance.GetTotalPoints() + "";
    }
}
