﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class IslandManager : MonoBehaviour
{
  public Animator transitionAnimation = null;
  PuzzleManager puzzleManager;
  IslandPuzzleType[] islandsOrder;
  Collider2D playerCollider;
  bool loadingScene = false;
  GameObject[] islands;
  const string ActivationKey = "space";
  string[] timeLights = { "noonLight", "sunsetLight", "midnightLight", "dawnLight" };
  int[] turbineSpeeds = { 10, 50, 150, 200 };
  
  static void hideLock(GameObject island)
  {
    foreach (Transform child in island.transform)
    {
      if (child.CompareTag("IslandLock"))
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
      if (child.CompareTag("WireGreen"))
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
      if (child.CompareTag("WireRed"))
      {
        child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        break;
      }
    }
  }
  
  void setPlayerPosition()
  {
    Vector3? playerPosition = puzzleManager.savedPlayerPosition();
    if (playerPosition != null)
      GameObject.Find("PlayerShip").transform.position = playerPosition.Value;
  }

  void setTimeLight(int currentIsland)
  {
    UnityEngine.Assertions.Assert.IsFalse(currentIsland >= timeLights.Length);
    for (int i = 0; i < timeLights.Length; ++i)
    {
      if (i != currentIsland)
        GameObject.Find(timeLights[i]).SetActive(false);
    }

    if (timeLights[currentIsland] != "midnightLight")
    {
      GameObject.Find("Spot Light").SetActive(false);
      if (timeLights[currentIsland] != "sunsetLight")
        GameObject.Find("VolumetricLight").SetActive(false);
    }
  }

  void setTurbineSpeed(int currentIsland)
  {
    UnityEngine.Assertions.Assert.IsFalse(currentIsland >= turbineSpeeds.Length);
    GameObject.Find("TurbinePropellor").GetComponent<PropellorRotation>().rotateSpeed = turbineSpeeds[currentIsland];
  }
  
  void Start()
  {
    puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
    islandsOrder = puzzleManager.islandsOrder;
    playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
    UnityEngine.Assertions.Assert.AreNotEqual(transitionAnimation, null);
    puzzleManager.setTransitionAnimation(transitionAnimation);
    
    setPlayerPosition();
    
    islands = new GameObject[islandsOrder.Length];
    int i = 0;
    int currentIsland = puzzleManager.getCurrentIsland();
    foreach (IslandPuzzleType islandPuzzleType in islandsOrder)
    {
      islands[i] = GameObject.FindWithTag(islandPuzzleType.ToString());
      UnityEngine.Assertions.Assert.AreNotEqual(islands[i], null);
      islands[i].GetComponent<CircleCollider2D>().enabled = false;
      if (i < currentIsland)
      {
        hideLock(islands[i]);
        //activateGreenWire(islands[i]);
      }
      ++i;
    }
    
    if (currentIsland < islands.Length)
    {
      hideLock(islands[currentIsland]);
      //activateRedWire(islands[currentIsland]);
      islands[currentIsland].GetComponent<CircleCollider2D>().enabled = true;
    }

    if (currentIsland > 0 && puzzleManager.isReturningFromIsland())
    {
      //activateGreenWire(islands[currentIsland-1]);
      Vector3 newPosition = islands[currentIsland-1].transform.position;
      GameObject.Find("PlayerShip").transform.position = newPosition;
      puzzleManager.saveGame(newPosition);
    }
    
    if (currentIsland >= islands.Length)
    {
      GameObject.Find("Clouds1").SetActive(false);
      GameObject worldBorder = GameObject.FindWithTag("WorldBorder");
      worldBorder.GetComponent<EdgeCollider2D>().enabled = false;
      worldBorder.GetComponent<CircleCollider2D>().enabled = true;
      loadingScene = true;
    }
    
    setTimeLight(currentIsland);
    setTurbineSpeed(currentIsland);
  }

  void Update()
  {
    if (!loadingScene)
    {
      int currentIsland = puzzleManager.getCurrentIsland();
      if (currentIsland < islands.Length
          && playerCollider.IsTouching(islands[currentIsland].GetComponent<Collider2D>()))
      {
        if (Input.GetKeyDown(ActivationKey) || Input.GetButtonDown("Jump"))
        {
          loadingScene = true;
          puzzleManager.loadPuzzle();
        }
      }
    }
  }
}
