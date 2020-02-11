using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleLoader : MonoBehaviour
{
  private string[] puzzle_scenes;
  private int total_puzzles;
  private int current_puzzle;
  private readonly string hubworld_scene = "TestScene";
  
  public void setScenes(string[] puzzle_scenes_list)
  {
    puzzle_scenes = puzzle_scenes_list;
  }
  
  public void enterIsland()
  {
    total_puzzles = puzzle_scenes.Length;
    current_puzzle = -1;
    loadNextPuzzle();
  }
  
  public bool loadNextPuzzle()
  {
    if (++current_puzzle < total_puzzles)
    {
      StartCoroutine(loadScene(puzzle_scenes[current_puzzle]));
      return false;
    }
    else
    {
      StartCoroutine(loadScene(hubworld_scene));
      return true;
    }
  }
  
  IEnumerator loadScene(string scene)
  {    
    //transitionAnim.SetTrigger("end");
    yield return new WaitForSeconds(1.5f);
    UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
  }
}
