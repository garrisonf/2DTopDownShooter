using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
  public int width;
  public int height;
  
  void Awake()
  {
    Transform box_group = this.gameObject.transform.Find("Boxes");
    
    for (int i = 0; i < box_group.childCount; ++i)
    {
      BoxTile[] neighbors = new BoxTile[4]{null, null, null, null};
      int j = 0;
      int row = i / height;
      int col = i - row * width;
      
      if (row > 0)        // add up
        neighbors[j++] = box_group.GetChild(i-width).GetComponent<BoxTile>();
      if (row < height-1) // add down
        neighbors[j++] = box_group.GetChild(i+width).GetComponent<BoxTile>();
      if (col > 0)        // add left
        neighbors[j++] = box_group.GetChild(i-1).GetComponent<BoxTile>();
      if (col < width-1) // add right
        neighbors[j++] = box_group.GetChild(i+1).GetComponent<BoxTile>();
      
      box_group.GetChild(i).GetComponent<BoxTile>().setNeighbors(neighbors);
    }
  }
}
