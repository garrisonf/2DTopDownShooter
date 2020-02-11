using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
  public PuzzleLoader light_puzzle_loader;
  public string[] light_puzzle_scenes;
  public bool light_puzzle_completed = false;
  
  void Awake()
  {
    if (FindObjectsOfType<PuzzleManager>().Length != 1)
      Destroy(gameObject);
    else
      DontDestroyOnLoad(gameObject);
  }
  
  void Start()
  {    
    light_puzzle_loader = gameObject.AddComponent<PuzzleLoader>();
    light_puzzle_loader.setScenes(light_puzzle_scenes);
  }
  
  public void loadLightPuzzle()
  {
    light_puzzle_loader.enterIsland();
  }
}
