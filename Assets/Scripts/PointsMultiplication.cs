using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PointsMultiplication : ScriptableObject
{
    public int pointsInAirTime = 20;
    public int frontFlipIncrease = 2;
    public int backFlipIncrease = 2;
    public float speedMultiply = 2;
    public int pointsOnBooster = 30;
    public int pointsOnGround = 20;
}
