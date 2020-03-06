using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WinSceneLoader : MonoBehaviour
{
  public Animator transitionAnimation = null;
  
  void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("WorldBorder"))
      StartCoroutine(LoadYouWinScene());
  }

  IEnumerator LoadYouWinScene()
  {
    transitionAnimation.SetTrigger("end");
    yield return new WaitForSeconds(1.5f);
    UnityEngine.SceneManagement.SceneManager.LoadScene("YouWin");
  }
}
