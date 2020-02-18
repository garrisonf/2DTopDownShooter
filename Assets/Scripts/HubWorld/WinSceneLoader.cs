using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneLoader : MonoBehaviour
{
  public Animator transition_animation = null;
  
  void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.tag == "CloudBoundary")
      StartCoroutine(LoadYouWinScene());
  }

  IEnumerator LoadYouWinScene()
  {
    transition_animation.SetTrigger("end");
    yield return new WaitForSeconds(1.5f);
    UnityEngine.SceneManagement.SceneManager.LoadScene("YouWin");
  }
}
