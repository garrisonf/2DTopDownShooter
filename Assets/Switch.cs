using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    //Start is called before the first frame update
    Color originalColor = Color.white;
    public float incrementAmount;
    public bool inputAllowed;
    void Start()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
        inputAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If the box collider is overlapping with the player's collider
        if(GetComponent<Collider2D>().IsTouching(GameObject.FindWithTag("Player").GetComponent<Collider2D>()))
        {
            // If the user presses space
            if(Input.GetKeyDown("space") && inputAllowed)
            {
               StartCoroutine(waitForSwitch());
               //GameObject.FindWithTag("VerticalBar").GetComponent<Slider>().value += 0.2f;
               GameObject.FindWithTag("VerticalBar").GetComponent<VerticalBar>().IncrementProgress(incrementAmount);
            }
        }
    }
    
    IEnumerator waitForSwitch()
    {
       inputAllowed = false;
       GetComponent<SpriteRenderer>().color = Color.green;
       yield return new WaitForSeconds(0.5f);
       GetComponent<SpriteRenderer>().color = originalColor;
       inputAllowed = true;
    }
}
