using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
  private Collider2D light_puzzle_island;
  private PuzzleManager light_puzzle_loader;
  private readonly string activation_key = "space";
  
  void Start()
  {
    light_puzzle_island = GameObject.FindWithTag("LightPuzzleIsland").GetComponent<Collider2D>();
    light_puzzle_loader = GameObject.FindWithTag("LightPuzzleLoader").GetComponent<PuzzleManager>();
  }
  
  void Update()
  {
    if (GetComponent<Collider2D>().IsTouching(light_puzzle_island))
      if (Input.GetKeyDown(activation_key))
        light_puzzle_loader.enterIsland();
  }
}
