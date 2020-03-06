using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
  public IslandPuzzleType[] islandsOrder;
  public string[] lightPuzzleScenes;
  public string[] laserPuzzleScenes;
  public Dictionary<IslandPuzzleType, PuzzleLoader> PuzzleLoaders { get; private set; }
  Animator transitionAnimation;
  int currentIsland;
  bool returningFromIsland;
  Save saveData = null;
  bool continueGame = false;
  
  public PuzzleManager()
  {
    PuzzleLoaders = new Dictionary<IslandPuzzleType, PuzzleLoader>();
    transitionAnimation = null;
    currentIsland = 0;
    returningFromIsland = false;
  }
  
  public void updateCurrentIsland()
  {
    ++currentIsland;
    returningFromIsland = true;
  }
  
  public bool isReturningFromIsland()
  {
    return returningFromIsland;
  }
  
  public void setTransitionAnimation(Animator transAnimation)
  {
    transitionAnimation = transAnimation;
  }
  
  public int getCurrentIsland()
  {
    return currentIsland;
  }
  
  public void saveGame(Vector3 playerPosition)
  {
    SaveDataManager.save(playerPosition, currentIsland);
  }
  
  public void loadSaveData()
  {
    saveData = SaveDataManager.load();
    currentIsland = saveData.numLevelsCompleted;
    continueGame = true;
  }
  
  public void newGame()
  {
    currentIsland = 0;
  }
  
  public Vector3? savedPlayerPosition()
  {
    if (continueGame)
    {
      continueGame = false;
      return saveData.playerPosition;
    }
    return null;
  }
  
  public void loadPuzzle()
  {
    UnityEngine.Assertions.Assert.IsTrue(currentIsland < islandsOrder.Length);
    returningFromIsland = false;
    PuzzleLoaders[islandsOrder[currentIsland]].enterIsland(transitionAnimation);
  }
  
  void Awake()
  {
    if (FindObjectsOfType<PuzzleManager>().Length != 1)
      Destroy(gameObject);
    else
      DontDestroyOnLoad(gameObject);
  }
  
  void Start()
  {
    if (!SaveDataManager.saveExists())
      GameObject.Find("Continue").GetComponent<Button>().interactable = false;

    foreach (IslandPuzzleType islandPuzzleType in islandsOrder)
    {
      PuzzleLoaders.Add(islandPuzzleType, gameObject.AddComponent<PuzzleLoader>());
      PuzzleLoaders[islandPuzzleType].init(this);
    }
    
    PuzzleLoaders[IslandPuzzleType.LightPuzzleIsland].setScenes(lightPuzzleScenes);
    PuzzleLoaders[IslandPuzzleType.LaserPuzzleIsland].setScenes(laserPuzzleScenes);
  }
}
