using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    public Image progressImage;

    public void Start()
    {
        UpdateProgress(0);
    }

    public void UpdateProgress(float progress)
    {
        progressImage.fillAmount = progress;
    }
}