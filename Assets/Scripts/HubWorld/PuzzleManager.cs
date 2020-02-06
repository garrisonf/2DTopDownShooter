using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
  public string[] puzzle_scenes;
  private int times = 0;
  
  void Awake()
  {
    DontDestroyOnLoad(this.gameObject);
  }
  
  public void enterIsland()
  {
    print("in island " + times);
    StartCoroutine(loadScene(times++));
    
    
  }
  
  IEnumerator loadScene(int scene)
  {
    //Scene current_scene = SceneManager.GetActiveScene();
    //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(puzzle_scenes[scene], LoadSceneMode.Additive);
    //while (!asyncLoad.isDone)
    //{
      //yield return null;
    //}
    //SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetSceneByName(puzzle_scenes[scene]));
    //SceneManager.UnloadSceneAsync(current_scene);
    
    
    //transitionAnim.SetTrigger("end");
    yield return new WaitForSeconds(1.5f);
    UnityEngine.SceneManagement.SceneManager.LoadScene(puzzle_scenes[scene]);
  }
}
