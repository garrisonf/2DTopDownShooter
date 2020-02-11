using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    private PuzzleManager puzzle_manager;
    private Collider2D player_collider;
    private bool loading_scene = false;
    private Collider2D light_puzzle_island;
    private readonly string activation_key = "space";
    
    void Start()
    {
      player_collider = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
      puzzle_manager = GameObject.FindWithTag("PuzzleManager").GetComponent<PuzzleManager>();
      light_puzzle_island = GameObject.FindWithTag("LightPuzzleIsland").GetComponent<Collider2D>();
    }

    void Update()
    {
      if (!loading_scene && !puzzle_manager.light_puzzle_completed && player_collider.IsTouching(light_puzzle_island))
      {
        if (Input.GetKeyDown(activation_key))
        {
          loading_scene = true;
          puzzle_manager.loadLightPuzzle();
        }
      }
    }
}
