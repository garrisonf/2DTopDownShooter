using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class LightPuzzle : MonoBehaviour
{
  public int width;
  public int height;
  public Animator transitionAnimation = null;
  public static bool loadingScene;
  BoxTile[] boxes;
  PuzzleLoader puzzleLoader;
  readonly string resetKey = "r";
  
  void Start()
  {
    boxes = new BoxTile[width * height];
    Transform boxGroup = this.gameObject.transform.Find("Boxes");
    
    UnityEngine.Assertions.Assert.AreEqual(width * height, boxGroup.childCount);
    UnityEngine.Assertions.Assert.AreNotEqual(transitionAnimation, null);
    
    for (int i = 0; i < boxGroup.childCount; ++i)
    {
      BoxTile[] neighbors = new BoxTile[4]{null, null, null, null};
      int j = 0;
      int row = i / height;
      int col = i - row * width;
      
      if (row > 0)        // add up
        neighbors[j++] = boxGroup.GetChild(i-width).GetComponent<BoxTile>();
      if (row < height-1) // add down
        neighbors[j++] = boxGroup.GetChild(i+width).GetComponent<BoxTile>();
      if (col > 0)        // add left
        neighbors[j++] = boxGroup.GetChild(i-1).GetComponent<BoxTile>();
      if (col < width-1) // add right
        neighbors[j++] = boxGroup.GetChild(i+1).GetComponent<BoxTile>();
      
      boxes[i] = boxGroup.GetChild(i).GetComponent<BoxTile>();
      boxes[i].setNeighbors(neighbors);
    }
    
    loadingScene = false;
    puzzleLoader = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>().PuzzleLoaders[IslandPuzzleType.LightPuzzleIsland];
  }
  
  void Update()
  {
    if (!loadingScene && boxes.All(x => x.activated))
    {
      loadingScene = true;
      puzzleLoader.loadNextPuzzle(transitionAnimation);
    }
    
    if (!loadingScene && Input.GetKeyDown(resetKey))
      foreach (BoxTile box in boxes)
        box.activated = false;
    
    if (!loadingScene && Input.GetKeyDown("p")) // REMOVE THIS
      foreach (BoxTile box in boxes)
        box.activated = true;
  }
}
