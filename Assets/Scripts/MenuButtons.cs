using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject CreditCanvas;
    public void PlayBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void TutorialBtn()
    {
        //todo
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
