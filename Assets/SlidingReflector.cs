using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingReflector : MonoBehaviour
{
    public Rigidbody2D rb;
    public float originalX;
    public bool translationAllowed = true;
    public bool isLeft = true;
    SpriteRenderer sr;
    Color originalColor;
    
    //Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        originalX = transform.position.x;
    }

    //Update is called once per frame
    void Update()
    {
       if(transform.position.x > (3.01f + originalX))
       {
          rb.velocity = Vector2.zero;
          isLeft = false;
          transform.position = new Vector3(3.00f + originalX, transform.position.y, 0.0f);
       }
       
       if(transform.position.x < originalX)
       {
          rb.velocity = Vector2.zero;
          isLeft = true;
          transform.position = new Vector3(originalX, transform.position.y, 0.0f);
       }
    }
        
    public void OnCollisionEnter2D(Collision2D other)
    {
       sr.color = Color.cyan;
    }
    
    public void OnCollisionStay2D(Collision2D other)
    {
        //If the Reflector collider is touching the player's collider
        if(GetComponent<Collider2D>().IsTouching(GameObject.FindWithTag("Player").GetComponent<Collider2D>()))
        {
            //If the user presses space and translation is allowed (prevent spam)
            if(Input.GetKeyDown("space") && translationAllowed)
            {
                //prevent buffered/spammed tranlations
                translationAllowed = false;
                Debug.Log("Translating Reflector 1");
                //Set velocity to + or - 3.0f
                if(isLeft)
                   rb.velocity = new Vector2(3.0f, 0.0f);
                else
                   rb.velocity = new Vector2(-3.0f, 0.0f);
                StartCoroutine(waitForTranslation());
            }
        }
    }
    
    public void OnCollisionExit2D(Collision2D other)
    {
       sr.color = originalColor;
    }
    
    //Co-routine to wait 1 second before allowing next translation
    IEnumerator waitForTranslation()
    {
       yield return new WaitForSeconds(1);
       translationAllowed = true;
    }
}

