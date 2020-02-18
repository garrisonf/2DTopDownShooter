using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleLoader : MonoBehaviour
{
  string[] puzzle_scenes = null;
  int total_puzzles;
  int current_puzzle;
  bool puzzles_completed;
  PuzzleManager puzzle_manager;
  Animator transition_animation;
  readonly string hubworld_scene = "HubWorld";
  
  public void init(PuzzleManager puzzle_manager_creator)
  {
    puzzle_manager = puzzle_manager_creator;
  }
  
  public void setScenes(string[] puzzle_scenes_list)
  {
    puzzle_scenes = puzzle_scenes_list;
  }
  
  public void enterIsland(Animator trans_animation)
  {
    UnityEngine.Assertions.Assert.AreNotEqual(puzzle_scenes, null);
    transition_animation = trans_animation;
    total_puzzles = puzzle_scenes.Length;
    current_puzzle = -1;
    loadNextPuzzle();
  }
  
  public void loadNextPuzzle(Animator trans_animation)
  {
    transition_animation = trans_animation;
    loadNextPuzzle();
  }
  
  public void loadNextPuzzle()
  {
    if (++current_puzzle < total_puzzles)
    {
      StartCoroutine(loadScene(puzzle_scenes[current_puzzle]));
    }
    else
    {
      puzzle_manager.updateCurrentIsland();
      StartCoroutine(loadScene(hubworld_scene));
    }
  }
  
  IEnumerator loadScene(string scene)
  {    
    transition_animation.SetTrigger("end");
    yield return new WaitForSeconds(1.5f);
    UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
  }
}
