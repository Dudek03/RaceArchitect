using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressCounter : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI CurrentCounter;
    [SerializeField] private TextMeshProUGUI MaxCounter;
    [SerializeField] private int maxValue;
    private int currentValue = 0;

    void Start()
    {
        UpdateVisual();
    }

    public void UpdateMaxValue(int value)
    {
        this.maxValue = value;
        UpdateVisual();
    }

    public void Add(int val)
    {
        currentValue += val;
        if (currentValue > maxValue)
        {
            currentValue = maxValue;
        }

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
        fill.fillAmount = Normalise();
        CurrentCounter.text = $"{currentValue}";
        MaxCounter.text = $"{maxValue}";
    }

    private float Normalise()
    {
        return (float)currentValue / maxValue;
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