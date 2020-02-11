using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleLoader : MonoBehaviour
{
  private string[] puzzle_scenes = null;
  private int total_puzzles;
  private int current_puzzle;
  public bool puzzles_completed;
  private readonly string hubworld_scene = "TestScene";
  
  public void setScenes(string[] puzzle_scenes_list)
  {
    puzzle_scenes = puzzle_scenes_list;
  }
  
  public void enterIsland()
  {
    UnityEngine.Assertions.Assert.AreNotEqual(puzzle_scenes, null);
    
    total_puzzles = puzzle_scenes.Length;
    current_puzzle = -1;
    loadNextPuzzle();
  }
  
  public void loadNextPuzzle()
  {
    if (++current_puzzle < total_puzzles)
    {
      StartCoroutine(loadScene(puzzle_scenes[current_puzzle]));
      puzzles_completed = false;
    }
    else
    {
      StartCoroutine(loadScene(hubworld_scene));
      puzzles_completed = true;
    }
  }
  
  IEnumerator loadScene(string scene)
  {    
    //transitionAnim.SetTrigger("end");
    yield return new WaitForSeconds(1.5f);
    UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
  }
}
