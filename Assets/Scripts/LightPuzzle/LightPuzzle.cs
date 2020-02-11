using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
  public int width;
  public int height;
  private BoxTile[] boxes;
  private PuzzleManager puzzle_manager;
  public static bool loading_scene = false;
  public readonly string reset_key = "r";
  
  void Start()
  {
    puzzle_manager = GameObject.FindWithTag("PuzzleManager").GetComponent<PuzzleManager>();
    
    boxes = new BoxTile[width * height];
    Transform box_group = this.gameObject.transform.Find("Boxes");
    
    UnityEngine.Assertions.Assert.AreEqual(width * height, box_group.childCount,
      "LightPuzzle: width*height does not match number of boxes" +
      " (width=" + width + " height=" + height + " boxes=" + box_group.childCount + ")");
    
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
      
      boxes[i] = box_group.GetChild(i).GetComponent<BoxTile>();
      boxes[i].setNeighbors(neighbors);
    }
    
    loading_scene = false;
  }
  
  void Update()
  {
    if (!loading_scene && boxes.All(x => x.activated))
    {
      loading_scene = true;
      puzzle_manager.light_puzzle_completed = puzzle_manager.light_puzzle_loader.loadNextPuzzle();
    }
    
    if (!loading_scene && Input.GetKeyDown(reset_key))
      foreach (BoxTile box in boxes)
        box.activated = false;
    
    if (!loading_scene && Input.GetKeyDown("p")) // REMOVE THIS
      foreach (BoxTile box in boxes)
        box.activated = true;
  }
}
