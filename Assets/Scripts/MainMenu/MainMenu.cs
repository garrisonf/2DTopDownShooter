using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>().newGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene("HubWorld for Sin");
    }

    public void ContinueGame()
    {
        GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>().loadSaveData();
        UnityEngine.SceneManagement.SceneManager.LoadScene("HubWorld for SIn");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
