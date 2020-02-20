using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
  public Animator transition_animation = null;
  PuzzleManager puzzle_manager;
  IslandPuzzleType[] islands_order;
  Collider2D player_collider;
  bool loading_scene = false;
  GameObject[] islands;
  GameObject cloud_boundary = null;
  readonly string activation_key = "space";
  
  static void hideLock(GameObject island)
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
  
  static void activateGreenWire(GameObject island)
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
  
  static void activateRedWire(GameObject island)
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
  
  void setPlayerPosition()
  {
    Vector3 player_position = puzzle_manager.savedPlayerPosition();
    if (player_position != null)
      GameObject.Find("PlayerShip").transform.position = player_position;
  }
  
  void Start()
  {
    puzzle_manager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
    islands_order = puzzle_manager.islands_order;
    player_collider = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
    UnityEngine.Assertions.Assert.AreNotEqual(transition_animation, null);
    puzzle_manager.setTransitionAnimation(transition_animation);
    
    setPlayerPosition();
    
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
      Vector3 new_position = islands[current_island-1].transform.position;
      GameObject.Find("PlayerShip").transform.position = new_position;
      puzzle_manager.saveGame(new_position);
    }
    
    if (current_island >= islands.Length)
    {
      GameObject.Find("Clouds1").SetActive(false);
      cloud_boundary = GameObject.FindWithTag("CloudBoundary");
      cloud_boundary.GetComponent<EdgeCollider2D>().enabled = false;
      cloud_boundary.GetComponent<CircleCollider2D>().enabled = true;
      loading_scene = true;
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
