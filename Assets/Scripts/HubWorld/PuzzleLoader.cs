using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleLoader : MonoBehaviour
{
  string[] puzzleScenes = null;
  int totalPuzzles;
  int currentPuzzle;
  bool puzzlesCompleted;
  PuzzleManager puzzleManager;
  Animator transitionAnimation;
  readonly string hubworldScene = "HubWorld";
  
  public void init(PuzzleManager puzzleManagerCreator)
  {
    puzzleManager = puzzleManagerCreator;
  }
  
  public void setScenes(string[] puzzleScenesList)
  {
    puzzleScenes = puzzleScenesList;
  }
  
  public void enterIsland(Animator transAnimation)
  {
    UnityEngine.Assertions.Assert.AreNotEqual(puzzleScenes, null);
    transitionAnimation = transAnimation;
    totalPuzzles = puzzleScenes.Length;
    currentPuzzle = -1;
    loadNextPuzzle();
  }
  
  public void loadNextPuzzle(Animator transAnimation)
  {
    transitionAnimation = transAnimation;
    loadNextPuzzle();
  }
  
  public void loadNextPuzzle()
  {
    if (++currentPuzzle < totalPuzzles)
    {
      StartCoroutine(loadScene(puzzleScenes[currentPuzzle]));
    }
    else
    {
      puzzleManager.updateCurrentIsland();
      StartCoroutine(loadScene(hubworldScene));
    }
  }
  
  IEnumerator loadScene(string scene)
  {    
    transitionAnimation.SetTrigger("end");
    yield return new WaitForSeconds(1.5f);
    UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
  }
}
