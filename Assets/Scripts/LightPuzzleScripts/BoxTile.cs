using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTile : MonoBehaviour
{
  public bool activated = false;
  private BoxTile[] neighbors = new BoxTile[4];
  
  public void setNeighbors(BoxTile[] neighbors_list)
  {
    neighbors = neighbors_list;
  }
  
  void Update()
  {
    if (GetComponent<Collider2D>().IsTouching(LightPuzzle.player_collider))
    {
      if (Input.GetKeyDown(LightPuzzle.activation_key))
      {
        activated = !activated;
        foreach (BoxTile neighbor in neighbors)
          if (neighbor != null)
            neighbor.activated = !neighbor.activated;
      }
    }
    
    GetComponent<SpriteRenderer>().color = activated ?
      LightPuzzle.activated_color : LightPuzzle.deactivated_color;
  }
}
