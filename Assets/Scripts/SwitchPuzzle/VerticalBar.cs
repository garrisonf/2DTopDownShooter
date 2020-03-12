using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//[ExecuteInEditMode()]
public class VerticalBar : MonoBehaviour
{
   public float fillSpeed = 0.5f;
   public float expectedProgress = 0.0f;
   
   public Animator transitionAnim;
   public bool loadingScene { get; private set; }
   PuzzleLoader puzzleLoader;
   //public bool adjustProgress;
   
   private Slider slider;
   private float targetProgress = 0.0f;
   private float maxProgress = 0.80f;
   
   private void Awake()
   {
      slider = gameObject.GetComponent<Slider>();
   }
   
   //#if UNITY_EDITOR
   //[MenuItem("GameObject/UI/Linear Progress Bar")]
   //public static void addLinearProgressBar()
   //{
      //GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
      //obj.transform.SetParent(Selection.activeGameObject.transform, false);
   //}
   //#endif
   
   //public int minimum;
   //public int maximum;
   //public int current;
   //public Image mask;
   //public Image fill;
   //public Color color;
   
   
    // Start is called before the first frame update
    void Start()
    {
       //slider = gameObject.GetComponent<Slider>();
       slider.value = 0;
       //IncrementProgress(0.75f);
       slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.yellow;
       
       loadingScene = false;
       puzzleLoader = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>().PuzzleLoaders[IslandPuzzleType.SwitchPuzzleIsland];
    }

    // Update is called once per frame
    void Update()
    {
       if (!loadingScene)
       {
          //getCurrentFill();
          if(slider.value < targetProgress)
          {
             slider.value += fillSpeed * Time.deltaTime;
          }
        
          //adjust progress if decimal is not exact
          if(slider.value > targetProgress)
          {
             slider.value = expectedProgress;
          }
        
          if(slider.value > maxProgress && slider.value != expectedProgress)
          {
             StartCoroutine(waitForBar());
          }
 
          //If the user presses r
          if(Input.GetKeyDown("r") || ((Input.GetButtonDown("Reset1")) && (Input.GetButtonDown("Reset2")) && (Input.GetButtonDown("Reset3")) && (Input.GetButtonDown("Reset4")) ))
          {
             Debug.Log("Reset Puzzle!");
             resetProgress();
          }
          
          if(slider.value < (maxProgress + 0.01f) && slider.value > (maxProgress - 0.01f) && slider.value == expectedProgress)
          {
             slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.cyan;
             loadingScene = true;
             puzzleLoader.loadNextPuzzle(transitionAnim);
          }
       }
    }
    
    public void IncrementProgress(float newProgress)
    {
       targetProgress = slider.value + newProgress;
       expectedProgress = targetProgress;
    }
    
    public void resetProgress()
    {
       targetProgress = 0;
       expectedProgress = 0;
       slider.value = 0;
       slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.yellow;
    }
    
    IEnumerator waitForBar()
    {
       slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.red;
       yield return new WaitForSeconds(0.5f);
       resetProgress();
    }
}
