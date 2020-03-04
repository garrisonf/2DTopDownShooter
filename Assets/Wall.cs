using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Rigidbody2D rb;
    public int currentAngle = -45;
    public bool rotationAllowed = true;
    SpriteRenderer sr;
    Color originalColor;
    
    //Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    //Update is called once per frame
    void Update()
    {}
        
    public void OnCollisionEnter2D(Collision2D other)
    {
       //sr.color = Color.green;
    }
    
    public void OnCollisionStay2D(Collision2D other)
    {
        //If the Reflector collider is touching the player's collider
        if(GetComponent<Collider2D>().IsTouching(GameObject.FindWithTag("Player").GetComponent<Collider2D>()))
        {
            //If the user presses space and rotation is allowed (prevent spam)
            if(Input.GetKeyDown("space") && rotationAllowed)
            {
                //prevent buffered/spammed rotations while rotating for 1 second
                rotationAllowed = false;
                Debug.Log("Rotating Reflector 1");
                //Set angularVelocity to 90 (rotating CCW)
                rb.angularVelocity = 90;
                StartCoroutine(waitForRotation());
            }
        }
    }
    
    public void OnCollisionExit2D(Collision2D other)
    {
       sr.color = originalColor;
    }
    
    //Co-routine to wait 1 second before stopping rotation animation
    //and to auto-correct the currentAngle to be exactly diagonal
    IEnumerator waitForRotation()
    {
       //wait 1 second before stopping the rotation
       yield return new WaitForSeconds(1);
       rb.angularVelocity = 0;
       //Auto-correction to 45, 135,-135, -45
       //depends on previous currentAngle
       switch(currentAngle)
       {
          case -45:
            currentAngle = 45;
            break;
          case 45:
            currentAngle = 135;
            break;
          case 135:
            currentAngle = -135;
            break;
          case -135:
            currentAngle = -45;
            break;
          default:
            currentAngle = -45;
            break;
       }
       transform.eulerAngles = new Vector3(0, 0, currentAngle);
       rotationAllowed = true;
    }
}
