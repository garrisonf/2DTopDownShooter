using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    RaycastHit2D hit;
    LineRenderer lineRend;
    bool laserAllowed = true;
    Vector2 laserHit;
    
    // Start is called before the first frame update
    void Start()
    {   
        lineRend = GetComponent<LineRenderer>();
        lineRend.enabled = false;
        laserHit = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       if(laserAllowed)
       {
          //Ray2D ray = new Ray2D(lineRend.GetPosition(lineRend.positionCount - 2), lineRend.GetPosition(lineRend.positionCount - 1));
          //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, detection, collisionMask);
          hit = Physics2D.Linecast(laserHit, lineRend.GetPosition(lineRend.positionCount - 1));
          //if(Physics.Raycast(ray, out hitPoint, Mathf.Infinity)
          if(hit.collider != null && hit.transform.tag == "Reflector")
          {
             StartCoroutine(waitForLaser());
             
             laserHit = hit.point;
             Vector2 normalOfReflector = hit.normal;
             
             Transform reflectorHit = hit.transform;
             
             (lineRend.positionCount)++;
             
             Vector3 p1 = lineRend.GetPosition(lineRend.positionCount - 3);
             Vector3 p2 = lineRend.GetPosition(lineRend.positionCount - 2);
             
             float xComponent = Mathf.Round(p2.x - p1.x);
             float yComponent = Mathf.Round(p2.y - p1.y);
             
             //bottom left to top right diagonal
             if(reflectorHit.eulerAngles.z == 45.0f || reflectorHit.eulerAngles.z == -135.0f)
             {
                //laser moving right
                if(xComponent > 0.0f && yComponent == 0.0f)
                {
                   lineRend.SetPosition(lineRend.positionCount - 1, new Vector3(p2.x, p2.y + 3.0f, 0));
                }
                //laser moving left
                else if(xComponent < 0.0f && yComponent == 0.0f)
                {
                   lineRend.SetPosition(lineRend.positionCount - 1, new Vector3(p2.x, p2.y - 3.0f, 0));
                }
                //laser moving up
                else if(xComponent == 0.0f && yComponent > 0.0f)
                {
                   lineRend.SetPosition(lineRend.positionCount - 1, new Vector3(p2.x + 3.0f, p2.y, 0));
                }
                //laser moving down
                else if(xComponent == 0.0f && yComponent < 0.0f)
                {
                   lineRend.SetPosition(lineRend.positionCount - 1, new Vector3(p2.x - 3.0f, p2.y, 0));
                }
             }
             //top left to bottom right diagonal
             if(reflectorHit.eulerAngles.z == -45.0f || reflectorHit.eulerAngles.z == 135.0f)
             {
                //laser moving right
                if(xComponent > 0.0f && yComponent == 0.0f)
                {
                   lineRend.SetPosition(lineRend.positionCount - 1, new Vector3(p2.x, p2.y - 3.0f, 0));
                }
                //laser moving left
                else if(xComponent < 0.0f && yComponent == 0.0f)
                {
                   lineRend.SetPosition(lineRend.positionCount - 1, new Vector3(p2.x, p2.y + 3.0f, 0));
                }
                //laser moving up
                else if(xComponent == 0.0f && yComponent > 0.0f)
                {
                   lineRend.SetPosition(lineRend.positionCount - 1, new Vector3(p2.x - 3.0f, p2.y, 0));
                }
                //laser moving down
                else if(xComponent == 0.0f && yComponent < 0.0f)
                {
                   lineRend.SetPosition(lineRend.positionCount - 1, new Vector3(p2.x + 3.0f, p2.y, 0));
                }
             }
          }
          
       }
        
    }
    
    public void OnCollisionStay2D(Collision2D other)
    {
       if(GetComponent<Collider2D>().IsTouching(GameObject.FindWithTag("Player").GetComponent<Collider2D>()))
       {
          if(Input.GetKeyDown("space"))
          {
             //lineRend.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
             //lineRend.SetPosition(1, new Vector3(r1.transform.position.x, r1.transform.position.y, 0);
             lineRend.enabled = true;
             //StartCoroutine(waitForLaser());
          }
       }
    }
    
    IEnumerator waitForLaser()
    {
       laserAllowed = false;
       yield return new WaitForSeconds(0.5f);
       laserAllowed = true;
    }
}
