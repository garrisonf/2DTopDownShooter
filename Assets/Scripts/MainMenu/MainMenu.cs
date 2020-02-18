using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("HubWorld");
    }

    public void ContinueGame()
    {
        // load save here
        UnityEngine.SceneManagement.SceneManager.LoadScene("HubWorld");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
