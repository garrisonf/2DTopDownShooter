using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
   public float fillSpeed = 0.5f;
   
   private Slider slider;
   private float targetProgress = 0;
   
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
       IncrementProgress(0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        //getCurrentFill();
        if(slider.value < targetProgress)
        {
           slider.value += fillSpeed * Time.deltaTime;
        }
    }
    
    //void getCurrentFill()
    //{
       //float currentOffset = current - minimum;
       //float maximumOffset = maximum - minimum;
       //float fillAmount = currentOffset / maximumOffset;
       //mask.fillAmount = fillAmount;
    //}
    
    public void IncrementProgress(float newProgress)
    {
       targetProgress = slider.value + newProgress;
    }
}
