using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckAllBoxes : MonoBehaviour
{
    public Animator transitionAnim;


    public Box1Script box1;
    public Box2Script box2;
    public Box3Script box3;
    public Box4Script box4;
    public Box5Script box5;
    public Box6Script box6;
    public Box7Script box7;
    public Box8Script box8;
    public Box9Script box9;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (box1.activated && box2.activated && box3.activated && box4.activated && box5.activated && box6.activated && box7.activated && box8.activated && box9.activated)
        {

            GameObject.Find("IslandCompletionManager").GetComponent<IslandCompletionTracker>().LightPuzzleIslandCompleted = true;

            //transitions back to the hubworld
            StartCoroutine(LoadScene());
            
        }

    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("RyanHubWorld");
    }
}
