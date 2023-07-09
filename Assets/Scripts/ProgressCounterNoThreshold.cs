using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressCounterNoThreshold : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CurrentCounter;
    private int currentValue = 0;

    void Start()
    {
        UpdateVisual();
    }

    public void UpdateMaxValue(int value)
    {
        UpdateVisual();
    }

    public void Add(int val)
    {
        currentValue += val;
        UpdateVisual();
    }

    public void Subtract(int val)
    {
        currentValue -= val;
        if (currentValue < 0)
        {
            currentValue = 0;
        }

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        CurrentCounter.text = $"{currentValue}";
    }

    public void UpdatePoints(int points)
    {
        currentValue = points;
        if (currentValue < 0)
        {
            currentValue = 0;
        }

        UpdateVisual();
    }
}