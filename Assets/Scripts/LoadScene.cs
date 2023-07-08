using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void loadScene(int id)
    {

        SceneManager.LoadScene(id);
    }
}
