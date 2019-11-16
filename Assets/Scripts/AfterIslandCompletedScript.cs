using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages what happens as a result of islands being completed

public class AfterIslandCompletedScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If puzzle island is completed
        if (GameObject.Find("IslandCompletionManager").GetComponent<IslandCompletionTracker>().LightPuzzleIslandCompleted)
        {
            // disable the trigger collider so that the player cannot interact with the island again
            GameObject.Find("LightPuzzleIsland").GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
