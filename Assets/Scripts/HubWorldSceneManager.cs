using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubWorldSceneManager : MonoBehaviour
{
    public Animator transitionAnim;
 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Collider2D>().IsTouching(GameObject.FindWithTag("LightPuzzleIsland").GetComponent<Collider2D>()))
        {
            // If the user presses space while over Light Puzzle Island
            if (Input.GetKeyDown("space"))
            {
                /*//trigger dialogue
                FindObjectOfType<GeneratedDialogue>().StartDialogue();
                if (GameObject.Find("DialogueManager").GetComponent<GeneratedDialogue>().dialogueFinished)
                {
                    // if dialogue is over change scene
                    
                }
                */
                StartCoroutine(LoadLightPuzzleScene());
            }
        }

        if (GetComponent<Collider2D>().IsTouching(GameObject.FindWithTag("LaserPuzzleIsland").GetComponent<Collider2D>()))
        {
            // If the user presses space while over Light Puzzle Island
            if (Input.GetKeyDown("space"))
            {
                
                StartCoroutine(LoadLaserPuzzleScene());
            }
        }
    }

    // if player exits the cloud boundary after all islands are complete
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "CloudBoundaryContainer")
        {
            StartCoroutine(LoadYouWinScene());
        }
    }

    IEnumerator LoadLightPuzzleScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("RyanLightPuzzle");
    }

    IEnumerator LoadLaserPuzzleScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("JPScene");
    }

    IEnumerator LoadYouWinScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("YouWin");
    }

    


}
