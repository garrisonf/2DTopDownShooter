using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
  public string[] light_puzzle_scenes;
  public Dictionary<IslandPuzzleType, PuzzleLoader> puzzle_loaders = new Dictionary<IslandPuzzleType, PuzzleLoader>();
  private IslandPuzzleType[] islands_order;
  private int current_island = 0;
  
  public int getCurrentIsland()
  {
    return current_island;
  }
  
  public void updateCurrentIsland()
  {
    ++current_island;
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
      puzzle_loaders.Add(island_puzzle_type, gameObject.AddComponent<PuzzleLoader>());
    
    puzzle_loaders[IslandPuzzleType.LightPuzzleIsland].setScenes(light_puzzle_scenes);
  }
  
  public void loadPuzzle()
  {
    UnityEngine.Assertions.Assert.IsTrue(current_island < islands_order.Length);
    
    puzzle_loaders[islands_order[current_island]].enterIsland();
  }
}
