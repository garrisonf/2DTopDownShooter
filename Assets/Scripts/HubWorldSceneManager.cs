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
            // If the user presses space
            if (Input.GetKeyDown("space"))
            {
                // go to light puzzle scene
                StartCoroutine(LoadScene());
            }
        }
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("RyanLightPuzzle");
    }


}
