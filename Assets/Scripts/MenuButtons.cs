using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject CreditCanvas;
    public void PlayBtn()
    {
        SceneManager.LoadScene("Game");
    }

    public void TutorialBtn()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void CreditsBtn()
    {
        CreditCanvas.SetActive(true);
    }

    public void CreditsBackBtn()
    {
        CreditCanvas.SetActive(false);
    }
}
