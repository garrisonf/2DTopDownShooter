using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
  public IslandPuzzleType[] islands_order;
  private PuzzleManager puzzle_manager;
  private Collider2D player_collider;
  private bool loading_scene = false;
  private GameObject[] islands;
  private readonly string activation_key = "space";
  
  private static void hideLock(GameObject island)
  {
    foreach (Transform child in island.transform)
    {
      if (child.tag == "IslandLock")
      {
        child.gameObject.SetActive(false);
        break;
      }
    }
  }
  
  void Start()
  {
    puzzle_manager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
    player_collider = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
    
    islands = new GameObject[islands_order.Length];
    int i = 0;
    int current_island = puzzle_manager.getCurrentIsland();
    foreach (IslandPuzzleType island_puzzle_type in islands_order)
    {
      islands[i] = GameObject.FindWithTag(island_puzzle_type.ToString());
      
      UnityEngine.Assertions.Assert.AreNotEqual(islands[i], null);
      
      islands[i].GetComponent<CircleCollider2D>().enabled = false;
      if (i <= current_island)
        hideLock(islands[i]);
      ++i;
    }
    islands[current_island].GetComponent<CircleCollider2D>().enabled = true;
  }

  void Update()
  {
    if (!loading_scene)
    {
      int current_island = puzzle_manager.getCurrentIsland();
      if (current_island < islands.Length
          && player_collider.IsTouching(islands[current_island].GetComponent<Collider2D>()))
      {
        if (Input.GetKeyDown(activation_key))
        {
          loading_scene = true;
          puzzle_manager.loadPuzzle();
        }
      }
    }
  }
}
