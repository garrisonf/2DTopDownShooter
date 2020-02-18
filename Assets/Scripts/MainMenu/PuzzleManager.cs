using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
  public IslandPuzzleType[] islands_order;
  public string[] light_puzzle_scenes;
  public string[] laser_puzzle_scenes;
  public Dictionary<IslandPuzzleType, PuzzleLoader> puzzle_loaders { get; private set; }
  Animator transition_animation;
  int current_island;
  bool returning_from_island;
  
  public PuzzleManager()
  {
    puzzle_loaders = new Dictionary<IslandPuzzleType, PuzzleLoader>();
    transition_animation = null;
    current_island = 0;
    returning_from_island = false;
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
  
  public int getCurrentIsland()
  {
    return current_island;
  }
  
  public void loadPuzzle()
  {
    UnityEngine.Assertions.Assert.IsTrue(current_island < islands_order.Length);
    returning_from_island = false;
    puzzle_loaders[islands_order[current_island]].enterIsland(transition_animation);
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
    foreach (IslandPuzzleType island_puzzle_type in islands_order)
    {
      puzzle_loaders.Add(island_puzzle_type, gameObject.AddComponent<PuzzleLoader>());
      puzzle_loaders[island_puzzle_type].init(this);
    }
    
    puzzle_loaders[IslandPuzzleType.LightPuzzleIsland].setScenes(light_puzzle_scenes);
    puzzle_loaders[IslandPuzzleType.LaserPuzzleIsland].setScenes(laser_puzzle_scenes);
  }
}
