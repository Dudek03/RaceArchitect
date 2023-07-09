using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialsBacks : MonoBehaviour
{
    [SerializeField] private GameObject MainCanvas, BasicCanvas, InputCanvas, BlockCanvas;
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void TurnOffAllCanvases()
    {
        MainCanvas.SetActive(false);
        BasicCanvas.SetActive(false);
        InputCanvas.SetActive(false);
        BlockCanvas.SetActive(false);
    }

    public void BackToMainTutorial()
    {
        TurnOffAllCanvases();
        MainCanvas.SetActive(true);
    }

    public void GoToBasicCanvas()
    {
        TurnOffAllCanvases();
        BasicCanvas.SetActive(true);
    }
    public void GoToInputCanvas()
    {
        TurnOffAllCanvases();
        InputCanvas.SetActive(true);
    }

    public void GoToBlockCanvas()
    {
        TurnOffAllCanvases();
        BlockCanvas.SetActive(true);
    }
}
