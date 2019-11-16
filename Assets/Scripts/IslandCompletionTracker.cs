using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IslandCompletionTracker : MonoBehaviour
{
 
    // booleans that keep track of what islands have been completed
    public bool LightPuzzleIslandCompleted = false;

    // the code in Awake() makes sure that the PuzzleIslandManager gameobject is not duplicated every time there is a scene transition
    void Awake()
    {
        int numCompletionTrackers = FindObjectsOfType<IslandCompletionTracker>().Length;

        if (numCompletionTrackers != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
