using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
  public string[] light_puzzle_scenes;
  public string[] laser_puzzle_scenes;
  private Animator transition_animation = null;
  public Dictionary<IslandPuzzleType, PuzzleLoader> puzzle_loaders = new Dictionary<IslandPuzzleType, PuzzleLoader>();
  private IslandPuzzleType[] islands_order;
  private int current_island = 0;
  private bool returning_from_island = false;
  
  public int getCurrentIsland()
  {
    return current_island;
  }
  
  public void updateCurrentIsland()
  {
    ++current_island;
    returning_from_island = true;
  }
  
  public bool isReturningFromIsland()
  {
    return returning_from_island;
  }
  
  public void setTransitionAnimation(Animator trans_animation)
  {
    transition_animation = trans_animation;
  }
  
  void Awake()
  {
    if (FindObjectsOfType<PuzzleManager>().Length != 1)
      Destroy(gameObject);
    else
      DontDestroyOnLoad(gameObject);
  }
  
  void Start()
  {
    islands_order = GameObject.Find("IslandManager").GetComponent<IslandManager>().islands_order;
    
    foreach (IslandPuzzleType island_puzzle_type in islands_order)
    {
      puzzle_loaders.Add(island_puzzle_type, gameObject.AddComponent<PuzzleLoader>());
      puzzle_loaders[island_puzzle_type].init(this);
    }
    
    puzzle_loaders[IslandPuzzleType.LightPuzzleIsland].setScenes(light_puzzle_scenes);
    puzzle_loaders[IslandPuzzleType.LaserPuzzleIsland].setScenes(laser_puzzle_scenes);
  }
  
  public void loadPuzzle()
  {
    UnityEngine.Assertions.Assert.IsTrue(current_island < islands_order.Length);
    
    returning_from_island = false;
    puzzle_loaders[islands_order[current_island]].enterIsland(transition_animation);
  }
}
