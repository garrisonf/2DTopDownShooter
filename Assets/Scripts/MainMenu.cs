using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        Debug.Log("Start New Game");
        GameObject.Find("IslandCompletionManager").GetComponent<IslandCompletionTracker>().startedNewGame = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("RyanHubWorld");
    }

    public void ContinueGame()
    {
        Debug.Log("Saving not implemented yet, this starts a new game right now");
        UnityEngine.SceneManagement.SceneManager.LoadScene("RyanHubWorld");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
