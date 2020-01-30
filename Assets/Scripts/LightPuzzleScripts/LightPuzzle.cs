using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
  public int width;
  public int height;
  public BoxTile[,] boxes;
  
  void Start()
  {
    boxes = new BoxTile[height, width];
    Transform box_group = this.gameObject.transform.Find("Boxes");
    
    for (int i = 0; i < box_group.childCount; ++i)
      print(box_group.GetChild(i).GetComponent<BoxTile>().box_name);

    box_group.GetChild(8).GetComponent<BoxTile>().box_name = "LOOOOOOOL";
    
    box_group = this.gameObject.transform.Find("Boxes");
    for (int i = 0; i < box_group.childCount; ++i)
      print(box_group.GetChild(i).GetComponent<BoxTile>().box_name);
    
    
        //boxes = GetComponentsInChildren<BoxTile>();

        //foreach (BoxTile b in boxes)
            //print(b.box_name);
  }
}
