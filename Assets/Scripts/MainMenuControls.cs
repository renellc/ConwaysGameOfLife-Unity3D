using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControls : MonoBehaviour
{
    public void LoadBoardScene()
    {
        SceneManager.LoadScene("BoardScene");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
