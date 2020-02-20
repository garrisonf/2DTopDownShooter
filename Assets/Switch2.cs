using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch2 : MonoBehaviour
{
    //Start is called before the first frame update
    Color originalColor = Color.white;
    void Start()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
    }

    // Update is called once per frame
    void Update()
    {
        // If the box collider is overlapping with the player's collider
        if(GetComponent<Collider2D>().IsTouching(GameObject.FindWithTag("Player").GetComponent<Collider2D>()))
        {
            // If the user presses space
            if(Input.GetKeyDown("space"))
            {
               StartCoroutine(waitForSwitch());
               //GameObject.FindWithTag("VerticalBar").GetComponent<Slider>().value += 0.2f;
               GameObject.FindWithTag("VerticalBar").GetComponent<VerticalBar>().IncrementProgress(0.1f);
            }
        }
    }
    
    IEnumerator waitForSwitch()
    {
       GetComponent<SpriteRenderer>().color = Color.green;
       yield return new WaitForSeconds(0.2f);
       GetComponent<SpriteRenderer>().color = originalColor;
    }
}
