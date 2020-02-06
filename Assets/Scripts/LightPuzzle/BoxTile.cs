using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTile : MonoBehaviour
{
  public bool activated = false;
  private BoxTile[] neighbors = new BoxTile[4];
  public static Collider2D player_collider;
  private readonly string activation_key = "space";
  private readonly Color activated_color = new Color(0f, 250f, 0f);
  private readonly Color deactivated_color = new Color(0f, 0f, 0f);
  
  public void setNeighbors(BoxTile[] neighbors_list)
  {
    neighbors = neighbors_list;
  }
  
  void Update()
  {
    if (GetComponent<Collider2D>().IsTouching(player_collider))
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
