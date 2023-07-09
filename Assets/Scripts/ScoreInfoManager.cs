using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreInfoManager : MonoBehaviour
{
    public TMP_Text scoreText;

    public TMP_Text bonusMasterText;
    public TMP_Text bonusZoneText;
    public TMP_Text bonusFlipText;

    public GameObject scoreObject;
    public GameObject bonusZoneObject;
    public GameObject bonusFlipBonus;

    public BonusElementInfo infoAddBonus;
    private int lastScore = 0;

    public void UpdateScore(int score)
    {
        lastScore = score;
        if (score <= 0)
        {
            if(gameObject.activeInHierarchy)
                StartCoroutine(HideScore());
        }
        else
        {
            scoreText.text = score.ToString();
            scoreObject.SetActive(true);
        }
    }

    IEnumerator HideScore()
    {
        yield return new WaitForSeconds(10);
        if (lastScore <= 0)
        {
            scoreObject.SetActive(false);
        }
    }

    public void UpdateBonus(int bonus)
    {
        bonusMasterText.text = bonus.ToString();
    }

    public void UpdateBonusZone(int bonus)
    {
        bonusZoneText.text = bonus.ToString();
        bonusZoneObject.SetActive(bonus > 0);
    }

    public void UpdateBonusFlip(int bonus)
    {
        bonusFlipText.text = bonus.ToString();
        bonusFlipBonus.SetActive(bonus > 0);
    }

    public void Reset()
    {
        UpdateScore(0);
    }
}