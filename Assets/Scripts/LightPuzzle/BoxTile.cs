using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTile : MonoBehaviour
{
  public bool activated = false;
  BoxTile[] neighbors = new BoxTile[4];
  Collider2D playerCollider;
  readonly string activationKey = "space";
  readonly Color activatedColor = new Color(0f, 250f, 0f);
  readonly Color deactivatedColor = new Color(0f, 0f, 0f);
  
  void Start()
  {
    playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
  }
  
  public void setNeighbors(BoxTile[] neighborsList)
  {
    neighbors = neighborsList;
  }
  
  void Update()
  {
    if (!LightPuzzle.loadingScene && GetComponent<Collider2D>().IsTouching(playerCollider))
    {
      if (Input.GetKeyDown(activationKey))
      {
        activated = !activated;
        foreach (BoxTile neighbor in neighbors)
          if (neighbor != null)
            neighbor.activated = !neighbor.activated;
      }
    }
    
    GetComponent<SpriteRenderer>().color = activated ?
      activatedColor : deactivatedColor;
  }
}
