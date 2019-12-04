using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckCounter : MonoBehaviour
{
    public Animator transitionAnim;

    public Counter1Script counter1;
    public Counter2Script counter2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (counter1.counter == 80 && counter2.counter == 90)
        {

           // GameObject.Find("IslandCompletionManager").GetComponent<IslandCompletionTracker>().LightPuzzleIslandCompleted = true;

            //transitions back to the hubworld
            StartCoroutine(LoadScene());

        }

    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
//        UnityEngine.SceneManagement.SceneManager.LoadScene("RyanHubWorld");
    }
}
