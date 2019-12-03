using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {

        GameObject.Find("IslandCompletionManager").GetComponent<IslandCompletionTracker>().startedNewGame = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("RyanHubWorld");
    }

    public void ContinueGame()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("RyanHubWorld");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
