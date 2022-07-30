using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuController : MonoBehaviour
{
    public void OnRestartButton()
    {
        SceneManager.LoadScene("Level01");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}