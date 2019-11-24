using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages what happens as a result of islands being completed

public class AfterIslandCompletedScript : MonoBehaviour
{
    public GameObject island1;
    public GameObject island2;
    

    // Start is called before the first frame update
    void Start()
    {
        // if player started new game
        if (GameObject.Find("IslandCompletionManager").GetComponent<IslandCompletionTracker>().startedNewGame)
        {
            GameObject.Find("IslandCompletionManager").GetComponent<IslandCompletionTracker>().LightPuzzleIslandCompleted = false;
            GameObject.Find("IslandCompletionManager").GetComponent<IslandCompletionTracker>().LaserPuzzleIslandCompleted = false;

            GameObject.Find("IslandCompletionManager").GetComponent<IslandCompletionTracker>().startedNewGame = false;
        }

        // If light puzzle island is completed
        if (GameObject.Find("IslandCompletionManager").GetComponent<IslandCompletionTracker>().LightPuzzleIslandCompleted)
        {
            // Save the player's current position and how many islands have been completed
            //SaveDataManager.save(gameObject.transform.position, 1);

            // disable the trigger collider so that the player cannot interact with the island again
            island1.GetComponent<CircleCollider2D>().enabled = false;


            //make the player spawn where the light puzzle island is
            GameObject.Find("PlayerShip").transform.position = GameObject.Find("LightPuzzleIsland").transform.position;

            // remove lock icon from island 2
            GameObject.Find("Island2Lock").SetActive(false);

            // activate collider of second island, making it playable
            island2.GetComponent<CircleCollider2D>().enabled = true;
        }

        if (GameObject.Find("IslandCompletionManager").GetComponent<IslandCompletionTracker>().LaserPuzzleIslandCompleted)
        {
            // disable the trigger collider so that the player cannot interact with the island again
            island2.GetComponent<CircleCollider2D>().enabled = false;

            //make the player spawn where the laser puzzle island is
            GameObject.Find("PlayerShip").transform.position = GameObject.Find("LasertPuzzleIsland").transform.position;
        }


        // if all islands in the game are completed, remove the clouds, let player leave
        if (GameObject.Find("IslandCompletionManager").GetComponent<IslandCompletionTracker>().LightPuzzleIslandCompleted)
        {
            GameObject.Find("Clouds1").SetActive(false);
            GameObject.Find("CloudBoundaryContainer").GetComponent<EdgeCollider2D>().enabled = false;
            GameObject.Find("CloudBoundaryContainer").GetComponent<CircleCollider2D>().enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
