using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashMechanic : MonoBehaviour
{
    private Camera m_MainCamera;
    public PlayerCopterMovement movementScript;

    //for dash mechanic
    public float dashTime = 0;
    public float speedModifier = 1;
    public float dashCoolDown = 0;
    public bool dashButtonAllowed = true;
    public bool dashAllowed = true;

    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = Camera.main;

    }

    Vector2 rotateVector(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 forwardDash = m_MainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Dash")) && dashCoolDown >= 0.8f)
        //{
         //   dashTime = 0.1f;
         //  dashCoolDown = 0;
        //}
        
        if (Input.GetButtonDown("Dash") && dashCoolDown >= 0.8f && dashButtonAllowed && dashAllowed)
        {
            dashTime = 0.1f;
            dashCoolDown = 0;
            dashButtonAllowed = false;
            dashAllowed = false;
            StartCoroutine(waitForDash());
        }
        
        if(dashButtonAllowed && !Input.GetButton("Dash"))
        {
           dashAllowed = true;
        }
        
        if (dashTime > 0)
        {
            GetComponent<TrailRenderer>().material.color = new Color(0f, 0f, 200f);

            //GetComponent<Rigidbody2D>().AddForce(forwardDash * 30);
            movementScript.moveSpeed = 100f;
            speedModifier = 5f;
            dashTime -= Time.deltaTime;
        }
        /*if (Input.GetKeyDown("left shift"))
        {
            GetComponent<Rigidbody2D>().AddForce(forwardDash * 150);    
        }
        if (Input.GetKeyDown("q"))
        {
            GetComponent<Rigidbody2D>().AddForce(rotateVector(forwardDash, 90f) * 150);
        }
        if (Input.GetKeyDown("e"))
        {
            GetComponent<Rigidbody2D>().AddForce(rotateVector(forwardDash, -90f) * 150);
        }*/
        if (dashTime <= 0)
        {
            GetComponent<TrailRenderer>().material.color = Color.white;
            movementScript.moveSpeed = 10f;
            speedModifier = 1;
        }
        dashCoolDown += Time.deltaTime;

       
    }
    
    IEnumerator waitForDash()
    {
       yield return new WaitForSeconds(0.8f);
       dashButtonAllowed = true;
    }

}
