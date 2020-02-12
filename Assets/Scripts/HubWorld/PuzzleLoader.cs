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
  
  public void enterIsland(Animator transition_animation)
  {
    UnityEngine.Assertions.Assert.AreNotEqual(puzzle_scenes, null);
    
    total_puzzles = puzzle_scenes.Length;
    current_puzzle = -1;
    loadNextPuzzle(transition_animation);
  }
  
  public void loadNextPuzzle(Animator transition_animation)
  {
    if (++current_puzzle < total_puzzles)
    {
      StartCoroutine(loadScene(puzzle_scenes[current_puzzle], transition_animation));
      puzzles_completed = false;
    }
    else
    {
      StartCoroutine(loadScene(hubworld_scene, transition_animation));
      puzzles_completed = true;
    }
  }
  
  IEnumerator loadScene(string scene, Animator transition_animation)
  {    
    transition_animation.SetTrigger("end");
    yield return new WaitForSeconds(1.5f);
    UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
  }
}
