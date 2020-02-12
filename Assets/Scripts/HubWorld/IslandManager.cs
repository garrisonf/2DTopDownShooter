using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
  public IslandPuzzleType[] islands_order;
  public Animator transition_animation = null;
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
  
  private static void activateGreenWire(GameObject island)
  {
    foreach (Transform child in island.transform)
    {
      if (child.tag == "WireGreen")
      {
        child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        break;
      }
    }
  }
  
  private static void activateRedWire(GameObject island)
  {
    foreach (Transform child in island.transform)
    {
      if (child.tag == "WireRed")
      {
        child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        break;
      }
    }
  }
  
  void Start()
  {
    puzzle_manager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
    
    UnityEngine.Assertions.Assert.AreNotEqual(transition_animation, null);
    
    puzzle_manager.setTransitionAnimation(transition_animation);
    
    player_collider = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
    
    islands = new GameObject[islands_order.Length];
    int i = 0;
    int current_island = puzzle_manager.getCurrentIsland();
    foreach (IslandPuzzleType island_puzzle_type in islands_order)
    {
      islands[i] = GameObject.FindWithTag(island_puzzle_type.ToString());
      
      UnityEngine.Assertions.Assert.AreNotEqual(islands[i], null);
      
      islands[i].GetComponent<CircleCollider2D>().enabled = false;
      if (i < current_island)
      {
        hideLock(islands[i]);
        activateGreenWire(islands[i]);
      }
      ++i;
    }
    
    if (current_island < islands.Length)
    {
      hideLock(islands[current_island]);
      activateRedWire(islands[current_island]);
      islands[current_island].GetComponent<CircleCollider2D>().enabled = true;
    }

    if (current_island > 0 && puzzle_manager.isReturningFromIsland())
    {
      activateGreenWire(islands[current_island-1]);
      GameObject.Find("PlayerShip").transform.position = islands[current_island-1].transform.position;
    }
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
