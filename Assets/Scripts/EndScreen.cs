using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public List<TextMeshProUGUI> levels;
    public TextMeshProUGUI total;
    public TextMeshProUGUI highscore;


    void Update()
    {
        foreach (TextMeshProUGUI t in levels)
        {
            t.text = "-";
        }
        foreach (GameManager.LevelPoints l in GameManager.Instance.totalPointsArray)
        {
            if (l.idx < levels.Count)
                levels[l.idx].text = l.points + "";
        }
        total.text = GameManager.Instance.GetTotalPoints() + "";
        highscore.text = PlayerPrefs.GetInt("highscore", 0) + "";
    }
}
