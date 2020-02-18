using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTile : MonoBehaviour
{
  public bool activated = false;
  BoxTile[] neighbors = new BoxTile[4];
  Collider2D player_collider;
  readonly string activation_key = "space";
  readonly Color activated_color = new Color(0f, 250f, 0f);
  readonly Color deactivated_color = new Color(0f, 0f, 0f);
  
  void Start()
  {
    player_collider = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
  }
  
  public void setNeighbors(BoxTile[] neighbors_list)
  {
    neighbors = neighbors_list;
  }
  
  void Update()
  {
    if (!LightPuzzle.loading_scene && GetComponent<Collider2D>().IsTouching(player_collider))
    {
      if (Input.GetKeyDown(activation_key))
      {
        activated = !activated;
        foreach (BoxTile neighbor in neighbors)
          if (neighbor != null)
            neighbor.activated = !neighbor.activated;
      }
    }
    
    GetComponent<SpriteRenderer>().color = activated ?
      activated_color : deactivated_color;
  }
}
